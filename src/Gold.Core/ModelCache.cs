using System;
using System.Collections.Generic;

namespace Gold
{
    public static class ModelCache
    {
        private static Dictionary<string, IModelDefinition> _cache;
        private static object _lock = new object();

        public static IModelDefinition Load( string name )
        {
            #region Validations

            if ( name == null )
                throw new ArgumentNullException( nameof( name ) );

            #endregion


            /*
             * 
             */
            if ( _cache == null )
            {
                lock ( _lock )
                {
                    if ( _cache == null )
                    {
                        var cache = new Dictionary<string, IModelDefinition>();

                        foreach ( var m in ModelConfiguration.Current.Library )
                        {
                            var md = Platinum.Activator.Create<IModelDefinition>( m.Moniker );
                            cache.Add( md.Name, md );

                            _cache = cache;
                        }
                    }
                }
            }


            /*
             * 
             */
            IModelDefinition value;

            if ( _cache.TryGetValue( name, out value ) == false )
                return null;

            return value;
        }
    }
}
