﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.296 版自动生成。
// 
#pragma warning disable 1591

namespace WorkFlow.Base_UserWebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="base_userBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class base_userBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginValidatorOperationCompleted;
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback UpdateOperationCompleted;
        
        private System.Threading.SendOrPostCallback ModifyPasswordOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetModelOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetModelByLoginOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public base_userBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_Base_UserWebService_base_userBLLservice;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public SecurityContext SecurityContextValue {
            get {
                return this.securityContextValueField;
            }
            set {
                this.securityContextValueField = value;
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
        public event LoginValidatorCompletedEventHandler LoginValidatorCompleted;
        
        /// <remarks/>
        public event UpdateCompletedEventHandler UpdateCompleted;
        
        /// <remarks/>
        public event ModifyPasswordCompletedEventHandler ModifyPasswordCompleted;
        
        /// <remarks/>
        public event GetModelCompletedEventHandler GetModelCompleted;
        
        /// <remarks/>
        public event GetModelByLoginCompletedEventHandler GetModelByLoginCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/LoginValidator", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool LoginValidator(string login, string password) {
            object[] results = this.Invoke("LoginValidator", new object[] {
                        login,
                        password});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LoginValidatorAsync(string login, string password) {
            this.LoginValidatorAsync(login, password, null);
        }
        
        /// <remarks/>
        public void LoginValidatorAsync(string login, string password, object userState) {
            if ((this.LoginValidatorOperationCompleted == null)) {
                this.LoginValidatorOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginValidatorOperationCompleted);
            }
            this.InvokeAsync("LoginValidator", new object[] {
                        login,
                        password}, this.LoginValidatorOperationCompleted, userState);
        }
        
        private void OnLoginValidatorOperationCompleted(object arg) {
            if ((this.LoginValidatorCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginValidatorCompleted(this, new LoginValidatorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Update", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Update(base_userModel model, out string msg) {
            object[] results = this.Invoke("Update", new object[] {
                        model});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateAsync(base_userModel model) {
            this.UpdateAsync(model, null);
        }
        
        /// <remarks/>
        public void UpdateAsync(base_userModel model, object userState) {
            if ((this.UpdateOperationCompleted == null)) {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                        model}, this.UpdateOperationCompleted, userState);
        }
        
        private void OnUpdateOperationCompleted(object arg) {
            if ((this.UpdateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCompleted(this, new UpdateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/ModifyPassword", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ModifyPassword(string login, string password, out string msg) {
            object[] results = this.Invoke("ModifyPassword", new object[] {
                        login,
                        password});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ModifyPasswordAsync(string login, string password) {
            this.ModifyPasswordAsync(login, password, null);
        }
        
        /// <remarks/>
        public void ModifyPasswordAsync(string login, string password, object userState) {
            if ((this.ModifyPasswordOperationCompleted == null)) {
                this.ModifyPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnModifyPasswordOperationCompleted);
            }
            this.InvokeAsync("ModifyPassword", new object[] {
                        login,
                        password}, this.ModifyPasswordOperationCompleted, userState);
        }
        
        private void OnModifyPasswordOperationCompleted(object arg) {
            if ((this.ModifyPasswordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ModifyPasswordCompleted(this, new ModifyPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetModel", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public base_userModel GetModel(int id, out string msg) {
            object[] results = this.Invoke("GetModel", new object[] {
                        id});
            msg = ((string)(results[1]));
            return ((base_userModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetModelAsync(int id) {
            this.GetModelAsync(id, null);
        }
        
        /// <remarks/>
        public void GetModelAsync(int id, object userState) {
            if ((this.GetModelOperationCompleted == null)) {
                this.GetModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetModelOperationCompleted);
            }
            this.InvokeAsync("GetModel", new object[] {
                        id}, this.GetModelOperationCompleted, userState);
        }
        
        private void OnGetModelOperationCompleted(object arg) {
            if ((this.GetModelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetModelCompleted(this, new GetModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetModelByLogin", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public base_userModel GetModelByLogin(string login, out string msg) {
            object[] results = this.Invoke("GetModelByLogin", new object[] {
                        login});
            msg = ((string)(results[1]));
            return ((base_userModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetModelByLoginAsync(string login) {
            this.GetModelByLoginAsync(login, null);
        }
        
        /// <remarks/>
        public void GetModelByLoginAsync(string login, object userState) {
            if ((this.GetModelByLoginOperationCompleted == null)) {
                this.GetModelByLoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetModelByLoginOperationCompleted);
            }
            this.InvokeAsync("GetModelByLogin", new object[] {
                        login}, this.GetModelByLoginOperationCompleted, userState);
        }
        
        private void OnGetModelByLoginOperationCompleted(object arg) {
            if ((this.GetModelByLoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetModelByLoginCompleted(this, new GetModelByLoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://saron.workflowservice.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://saron.workflowservice.org/", IsNullable=false)]
    public partial class SecurityContext : System.Web.Services.Protocols.SoapHeader {
        
        private string userNameField;
        
        private string passWordField;
        
        private int appIDField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string PassWord {
            get {
                return this.passWordField;
            }
            set {
                this.passWordField = value;
            }
        }
        
        /// <remarks/>
        public int AppID {
            get {
                return this.appIDField;
            }
            set {
                this.appIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://saron.workflowservice.org/")]
    public partial class base_userModel {
        
        private int idField;
        
        private string loginField;
        
        private string passwordField;
        
        private string nameField;
        
        private string mobile_phoneField;
        
        private string mailField;
        
        private string remarkField;
        
        private bool adminField;
        
        private bool invalidField;
        
        private bool deletedField;
        
        private System.Nullable<System.DateTime> created_atField;
        
        private System.Nullable<int> created_byField;
        
        private string created_ipField;
        
        private System.Nullable<System.DateTime> updated_atField;
        
        private System.Nullable<int> updated_byField;
        
        private string updated_ipField;
        
        /// <remarks/>
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string login {
            get {
                return this.loginField;
            }
            set {
                this.loginField = value;
            }
        }
        
        /// <remarks/>
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string mobile_phone {
            get {
                return this.mobile_phoneField;
            }
            set {
                this.mobile_phoneField = value;
            }
        }
        
        /// <remarks/>
        public string mail {
            get {
                return this.mailField;
            }
            set {
                this.mailField = value;
            }
        }
        
        /// <remarks/>
        public string remark {
            get {
                return this.remarkField;
            }
            set {
                this.remarkField = value;
            }
        }
        
        /// <remarks/>
        public bool admin {
            get {
                return this.adminField;
            }
            set {
                this.adminField = value;
            }
        }
        
        /// <remarks/>
        public bool invalid {
            get {
                return this.invalidField;
            }
            set {
                this.invalidField = value;
            }
        }
        
        /// <remarks/>
        public bool deleted {
            get {
                return this.deletedField;
            }
            set {
                this.deletedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> created_at {
            get {
                return this.created_atField;
            }
            set {
                this.created_atField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> created_by {
            get {
                return this.created_byField;
            }
            set {
                this.created_byField = value;
            }
        }
        
        /// <remarks/>
        public string created_ip {
            get {
                return this.created_ipField;
            }
            set {
                this.created_ipField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> updated_at {
            get {
                return this.updated_atField;
            }
            set {
                this.updated_atField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> updated_by {
            get {
                return this.updated_byField;
            }
            set {
                this.updated_byField = value;
            }
        }
        
        /// <remarks/>
        public string updated_ip {
            get {
                return this.updated_ipField;
            }
            set {
                this.updated_ipField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void LoginValidatorCompletedEventHandler(object sender, LoginValidatorCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginValidatorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginValidatorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void UpdateCompletedEventHandler(object sender, UpdateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        
        /// <remarks/>
        public string msg {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ModifyPasswordCompletedEventHandler(object sender, ModifyPasswordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ModifyPasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ModifyPasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        
        /// <remarks/>
        public string msg {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetModelCompletedEventHandler(object sender, GetModelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public base_userModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((base_userModel)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string msg {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetModelByLoginCompletedEventHandler(object sender, GetModelByLoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetModelByLoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetModelByLoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public base_userModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((base_userModel)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string msg {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591