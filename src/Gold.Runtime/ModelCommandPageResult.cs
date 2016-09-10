using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gold.Runtime
{
    public class ModelCommandPageResult
    {
        private List<ModelResultItem> _items;
        private bool _success;

        /// <summary>
        /// Initializes a new instance of ModelCommandPageResult class.
        /// </summary>
        /// <param name="name">Name of the page.</param>
        public ModelCommandPageResult( string name )
        {
            #region Validations

            if ( name == null )
                throw new ArgumentNullException( "name" );

            #endregion

            _items = new List<ModelResultItem>();
            _success = true;

            this.Name = name;
            this.Processed = true;
        }


        /// <summary>
        /// Gets the name of the page.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets whether the command over the current page was successful.
        /// </summary>
        public bool Success
        {
            get
            {
                return _success;
            }
        }


        /// <summary>
        /// Gets whether the page was actually processed during the command
        /// execution, or not.
        /// </summary>
        public bool Processed
        {
            get;
            internal set;
        }


        /// <summary>
        /// Gets the list of all result items associated with the current page.
        /// </summary>
        public ReadOnlyCollection<ModelResultItem> Items
        {
            get
            {
                return _items.AsReadOnly();
            }
        }


        public ModelResultItem Add( string itemId, params object[] args )
        {
            ModelResultItem item = new ModelResultItem( null, itemId, args );

            _items.Add( item );

            if ( item.ItemType == ModelResultItemType.Error )
                _success = false;

            return item;
        }


        public ModelResultItem AddFor( string shapeId, string itemId, params object[] args )
        {
            ModelResultItem item = new ModelResultItem( shapeId, itemId, args );

            _items.Add( item );

            if ( item.ItemType == ModelResultItemType.Error )
                _success = false;

            return item;
        }
    }
}

/* eof */