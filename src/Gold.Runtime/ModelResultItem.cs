using Gold.Runtime.Properties;
using Platinum;
using System;
using System.Globalization;
using System.Resources;

namespace Gold.Runtime
{
    public class ModelResultItem
    {
        /// <summary>
        /// Initializes a shape related result item.
        /// </summary>
        /// <param name="shapeCode">Shape identifier. (Optional).</param>
        /// <param name="itemId">Item identifier.</param>
        /// <param name="args">Item arguments.</param>
        public ModelResultItem( string shapeCode, string itemId, params object[] args )
        {
            #region Validations

            if ( itemId == null )
                throw new ArgumentNullException( nameof( itemId ) );

            #endregion

            ModelResultItemData data = LoadFromRm( itemId, args );

            this.Actor = data.Actor;
            this.Code = data.Code;
            this.Description = data.Description;
            this.ItemType = data.ItemType;
            this.ShapeId = shapeCode;
        }


        /// <summary>
        /// Gets the (unique) shape identifier to which this item related to.
        /// </summary>
        /// <remarks>
        /// If null, then the item does not refer to any specific shape and refers
        /// to the page.
        /// </remarks>
        public string ShapeId
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the actor which issued the item.
        /// </summary>
        public string Actor
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the code which identifies the item.
        /// </summary>
        public int Code
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the user-readable description for the item.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the item type.
        /// </summary>
        public ModelResultItemType ItemType
        {
            get;
            private set;
        }


        /// <summary>
        /// Loads the data for the instance from the resource manager.
        /// </summary>
        /// <param name="itemId">Item identifier.</param>
        /// <param name="args">Argument list.</param>
        /// <returns>Item data.</returns>
        /// <remarks>
        /// This method must *NOT* throw any exceptions. By design, this method must
        /// always succeed in constructing an instance of ModelResultItemData. If there
        /// is any problem during the processing, then the ModelResultItemData class
        /// should be initialized and returned but with a payload that illustrates the
        /// internal error that occurred.
        /// </remarks>
        private static ModelResultItemData LoadFromRm( string itemId, params object[] args )
        {
            #region Validations

            if ( itemId == null )
                throw new ArgumentNullException( "itemId" );

            #endregion

            /*
             * 
             */
            CultureInfo ic = CultureInfo.InvariantCulture;


            /*
             * 
             */
            string code = RM.GetString( string.Concat( itemId, "_Code" ) );
            string actor = RM.GetString( string.Concat( itemId, "_Actor" ) );
            string desc = RM.GetString( string.Concat( itemId, "_Message" ) );
            string itemType = RM.GetString( string.Concat( itemId, "_ItemType" ) );

            if ( string.IsNullOrEmpty( code ) == true )
            {
                return new ModelResultItemData()
                {
                    Code = -3001,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_CodeNotDefined, itemId ),
                    ItemType = ModelResultItemType.Error
                };
            }

            if ( string.IsNullOrEmpty( actor ) == true )
            {
                return new ModelResultItemData()
                {
                    Code = -3002,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_ActorNotDefined, itemId ),
                    ItemType = ModelResultItemType.Error
                };
            }

            if ( string.IsNullOrEmpty( desc ) == true )
            {
                return new ModelResultItemData()
                {
                    Code = -3003,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_DescriptionNotDefined, itemId ),
                    ItemType = ModelResultItemType.Error
                };
            }

            if ( string.IsNullOrEmpty( itemType ) == true )
            {
                return new ModelResultItemData()
                {
                    Code = -3004,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_ItemTypeNotDefined, itemId ),
                    ItemType = ModelResultItemType.Error
                };
            }



            /*
             * 
             */
            ModelResultItemData data = new ModelResultItemData();
            data.Actor = actor;



            /*
             * .Code
             */
            try
            {
                data.Code = int.Parse( code, ic );
            }
            catch ( FormatException )
            {
                return new ModelResultItemData()
                {
                    Code = -3005,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_CodeNotInteger, itemId, code ),
                    ItemType = ModelResultItemType.Error
                };
            }
            catch ( OverflowException )
            {
                return new ModelResultItemData()
                {
                    Code = -3006,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_CodeNotInteger, itemId, code ),
                    ItemType = ModelResultItemType.Error
                };
            }


            /*
             * .ItemType
             */
            try
            {
                data.ItemType = Enumerate.Parse<ModelResultItemType>( itemType );
            }
            catch ( ActorException )
            {
                return new ModelResultItemData()
                {
                    Code = -3007,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_ItemTypeNotEnum, itemId, itemType ),
                    ItemType = ModelResultItemType.Error
                };
            }


            /*
             * .Description
             */
            try
            {
                data.Description = string.Format( ic, desc, args );
            }
            catch ( FormatException )
            {
                return new ModelResultItemData()
                {
                    Code = -3008,
                    Actor = "ModelResultItem.Internal",
                    Description = string.Format( ic, Resources.ResultItem_DescriptionFormatFail, itemId, desc, args.Length ),
                    ItemType = ModelResultItemType.Error
                };
            }

            return data;
        }


        private struct ModelResultItemData
        {
            internal int Code;
            internal string Actor;
            internal string Description;
            internal ModelResultItemType ItemType;
        }


        private static ResourceManager _rm;
        private static object _lock = new object();

        private static ResourceManager RM
        {
            get
            {
                if ( _rm == null )
                {
                    lock ( _lock )
                    {
                        if ( _rm == null )
                        {
                            Type rt = Type.GetType( "Gold.Runtime.R,Gold.Runtime" );
                            ResourceManager rm = new ResourceManager( rt );

                            _rm = rm;
                        }
                    }
                }

                return _rm;
            }
        }
    }
}

/* eof */