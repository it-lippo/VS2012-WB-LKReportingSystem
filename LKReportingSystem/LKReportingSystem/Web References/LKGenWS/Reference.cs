﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace LKReportingSystem.LKGenWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SQLServerServiceSoap", Namespace="http://tempuri.org/")]
    public partial class SQLServerService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CheckLoginADOperationCompleted;
        
        private System.Threading.SendOrPostCallback deleteSQLLoginService2OperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckLoginADStatusOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SQLServerService() {
            this.Url = global::LKReportingSystem.Properties.Settings.Default.LKReportingSystem_LKGenWS_SQLServerService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CheckLoginADCompletedEventHandler CheckLoginADCompleted;
        
        /// <remarks/>
        public event deleteSQLLoginService2CompletedEventHandler deleteSQLLoginService2Completed;
        
        /// <remarks/>
        public event CheckLoginADStatusCompletedEventHandler CheckLoginADStatusCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckLoginAD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckLoginAD(string domainName, string userName, string password) {
            object[] results = this.Invoke("CheckLoginAD", new object[] {
                        domainName,
                        userName,
                        password});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CheckLoginADAsync(string domainName, string userName, string password) {
            this.CheckLoginADAsync(domainName, userName, password, null);
        }
        
        /// <remarks/>
        public void CheckLoginADAsync(string domainName, string userName, string password, object userState) {
            if ((this.CheckLoginADOperationCompleted == null)) {
                this.CheckLoginADOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckLoginADOperationCompleted);
            }
            this.InvokeAsync("CheckLoginAD", new object[] {
                        domainName,
                        userName,
                        password}, this.CheckLoginADOperationCompleted, userState);
        }
        
        private void OnCheckLoginADOperationCompleted(object arg) {
            if ((this.CheckLoginADCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckLoginADCompleted(this, new CheckLoginADCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/deleteSQLLoginService2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet deleteSQLLoginService2(string loginName) {
            object[] results = this.Invoke("deleteSQLLoginService2", new object[] {
                        loginName});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void deleteSQLLoginService2Async(string loginName) {
            this.deleteSQLLoginService2Async(loginName, null);
        }
        
        /// <remarks/>
        public void deleteSQLLoginService2Async(string loginName, object userState) {
            if ((this.deleteSQLLoginService2OperationCompleted == null)) {
                this.deleteSQLLoginService2OperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteSQLLoginService2OperationCompleted);
            }
            this.InvokeAsync("deleteSQLLoginService2", new object[] {
                        loginName}, this.deleteSQLLoginService2OperationCompleted, userState);
        }
        
        private void OndeleteSQLLoginService2OperationCompleted(object arg) {
            if ((this.deleteSQLLoginService2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteSQLLoginService2Completed(this, new deleteSQLLoginService2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckLoginADStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CheckLoginADStatus() {
            object[] results = this.Invoke("CheckLoginADStatus", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckLoginADStatusAsync() {
            this.CheckLoginADStatusAsync(null);
        }
        
        /// <remarks/>
        public void CheckLoginADStatusAsync(object userState) {
            if ((this.CheckLoginADStatusOperationCompleted == null)) {
                this.CheckLoginADStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckLoginADStatusOperationCompleted);
            }
            this.InvokeAsync("CheckLoginADStatus", new object[0], this.CheckLoginADStatusOperationCompleted, userState);
        }
        
        private void OnCheckLoginADStatusOperationCompleted(object arg) {
            if ((this.CheckLoginADStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckLoginADStatusCompleted(this, new CheckLoginADStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CheckLoginADCompletedEventHandler(object sender, CheckLoginADCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckLoginADCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckLoginADCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void deleteSQLLoginService2CompletedEventHandler(object sender, deleteSQLLoginService2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deleteSQLLoginService2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal deleteSQLLoginService2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CheckLoginADStatusCompletedEventHandler(object sender, CheckLoginADStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckLoginADStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckLoginADStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591