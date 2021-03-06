// autogenerated: do NOT edit manually
using Platinum.Configuration;
using System;
using System.ComponentModel;
using System.Configuration;
using KvConfig = Platinum.Configuration.KeyValueConfigurationElement;
using NullableString = Platinum.Configuration.NullableString;

namespace Gold
{
    /// <summary />
    public partial class ModelConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets the configuration instance for section 'gold/model'
        /// </summary>
        public static ModelConfiguration Current
        {
            get { return AppConfiguration.SectionGet<ModelConfiguration>( "gold/model" ); }
        }


        /// <summary />
        [ConfigurationProperty( "library", IsDefaultCollection = false )]
        [ConfigurationCollection( typeof( ModelAddConfiguration ), AddItemName = "add" )]
        public ConfigurationElementCollection<ModelAddConfiguration> Library
        {
            get { return (ConfigurationElementCollection<ModelAddConfiguration>) base[ "library" ]; }
        }
    }


    /// <summary />
    public partial class ModelAddConfiguration : ConfigurationElement
    {

        /// <summary />
        [ConfigurationProperty( "type", IsKey = true, IsRequired = true )]
        public string Moniker
        {
            get { return (string) this[ "type" ]; }
            set { this[ "type" ] = value; }
        }
    }

}

/* eof */
