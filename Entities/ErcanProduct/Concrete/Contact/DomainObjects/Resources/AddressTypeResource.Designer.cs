﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Magnus.Server.DomainObjects.Contact.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AddressTypeResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AddressTypeResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Magnus.Server.DomainObjects.Contact.Resources.AddressTypeResource", typeof(AddressTypeResource).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adres tipi 10 karakterden fazla olamaz..
        /// </summary>
        public static string AddressTypeCodeMaxLength {
            get {
                return ResourceManager.GetString("AddressTypeCodeMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adres tipi gerekli..
        /// </summary>
        public static string AddressTypeCodeRequired {
            get {
                return ResourceManager.GetString("AddressTypeCodeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} kodu başka bir kayıt tarafından kullanılmaktadır..
        /// </summary>
        public static string AddressTypeCodeUsing {
            get {
                return ResourceManager.GetString("AddressTypeCodeUsing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adres tip adı 50 karakterden fazla olamaz..
        /// </summary>
        public static string AddressTypeNameMaxLength {
            get {
                return ResourceManager.GetString("AddressTypeNameMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Adres tip adı gerekli..
        /// </summary>
        public static string AddressTypeNameRequired {
            get {
                return ResourceManager.GetString("AddressTypeNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} adı başka bir kayıt tarafından kullanılmaktadır..
        /// </summary>
        public static string AddressTypeNameUsing {
            get {
                return ResourceManager.GetString("AddressTypeNameUsing", resourceCulture);
            }
        }
    }
}
