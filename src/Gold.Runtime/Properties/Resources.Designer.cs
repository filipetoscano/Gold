﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gold.Runtime.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gold.Runtime.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load _Actor resource string for result item &apos;{0}&apos;..
        /// </summary>
        internal static string ResultItem_ActorNotDefined {
            get {
                return ResourceManager.GetString("ResultItem_ActorNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load _Code resource string for result item &apos;{0}&apos;..
        /// </summary>
        internal static string ResultItem_CodeNotDefined {
            get {
                return ResourceManager.GetString("ResultItem_CodeNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Code for item &apos;{0}&apos; is not a valid integer: value was &apos;{1}&apos;..
        /// </summary>
        internal static string ResultItem_CodeNotInteger {
            get {
                return ResourceManager.GetString("ResultItem_CodeNotInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to format description for item type &apos;{0}&apos;. Args=&apos;{2}&apos;, Format={1}.
        /// </summary>
        internal static string ResultItem_DescriptionFormatFail {
            get {
                return ResourceManager.GetString("ResultItem_DescriptionFormatFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load _Message resource string for result item &apos;{0}&apos;..
        /// </summary>
        internal static string ResultItem_DescriptionNotDefined {
            get {
                return ResourceManager.GetString("ResultItem_DescriptionNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load _ItemType resource string for result item &apos;{0}&apos;..
        /// </summary>
        internal static string ResultItem_ItemTypeNotDefined {
            get {
                return ResourceManager.GetString("ResultItem_ItemTypeNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item type for item &apos;{0}&apos; is not a valid value for the enumerate: value was &apos;{1}&apos;..
        /// </summary>
        internal static string ResultItem_ItemTypeNotEnum {
            get {
                return ResourceManager.GetString("ResultItem_ItemTypeNotEnum", resourceCulture);
            }
        }
    }
}
