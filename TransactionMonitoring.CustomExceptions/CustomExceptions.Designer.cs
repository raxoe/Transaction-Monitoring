//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransactionMonitoring.CustomExceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CustomExceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomExceptions() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TransactionMonitoring.CustomExceptions.CustomExceptions", typeof(CustomExceptions).Assembly);
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
        ///   Looks up a localized string similar to CurrencyCode cannot be more than 3 characters.
        /// </summary>
        internal static string CurrencyCodeTooLong {
            get {
                return ResourceManager.GetString("CurrencyCodeTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Transaction.
        /// </summary>
        internal static string InvalidTransaction {
            get {
                return ResourceManager.GetString("InvalidTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Transaction Date.
        /// </summary>
        internal static string InvalidTransactionDate {
            get {
                return ResourceManager.GetString("InvalidTransactionDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Transaction Information.
        /// </summary>
        internal static string InvalidTransactionInfo {
            get {
                return ResourceManager.GetString("InvalidTransactionInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Transaction Status.
        /// </summary>
        internal static string InvalidTransactionStatus {
            get {
                return ResourceManager.GetString("InvalidTransactionStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Transaction Avaliable.
        /// </summary>
        internal static string NoTransactionAvaliable {
            get {
                return ResourceManager.GetString("NoTransactionAvaliable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CurrencyCode is required.
        /// </summary>
        internal static string RequiredCurrencyCode {
            get {
                return ResourceManager.GetString("RequiredCurrencyCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TransactionIdentificator is required.
        /// </summary>
        internal static string RequiredTransactionIdentificator {
            get {
                return ResourceManager.GetString("RequiredTransactionIdentificator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Success.
        /// </summary>
        internal static string Success {
            get {
                return ResourceManager.GetString("Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transaction Already Exists.
        /// </summary>
        internal static string TransactionAlreadyExist {
            get {
                return ResourceManager.GetString("TransactionAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TransactionIdentificator cannot be more than 50 characters.
        /// </summary>
        internal static string TransactionIdentificatorTooLong {
            get {
                return ResourceManager.GetString("TransactionIdentificatorTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal Error has occured. Please contact the administrator!.
        /// </summary>
        internal static string UnknownException {
            get {
                return ResourceManager.GetString("UnknownException", resourceCulture);
            }
        }
    }
}
