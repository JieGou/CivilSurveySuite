﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _3DS_CivilSurveySuite.C3D2017.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Tree [no trunk]")]
        public string Tree_Replace_Base_Symbol {
            get {
                return ((string)(this["Tree_Replace_Base_Symbol"]));
            }
            set {
                this["Tree_Replace_Base_Symbol"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Trunk")]
        public string Tree_Replace_Trunk_Symbol {
            get {
                return ((string)(this["Tree_Replace_Trunk_Symbol"]));
            }
            set {
                this["Tree_Replace_Trunk_Symbol"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Tree_Code {
            get {
                return ((string)(this["Tree_Code"]));
            }
            set {
                this["Tree_Code"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int Tree_Trunk_Parameter {
            get {
                return ((int)(this["Tree_Trunk_Parameter"]));
            }
            set {
                this["Tree_Trunk_Parameter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int Tree_Spread_Parameter {
            get {
                return ((int)(this["Tree_Spread_Parameter"]));
            }
            set {
                this["Tree_Spread_Parameter"] = value;
            }
        }
    }
}