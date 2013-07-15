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

namespace WorkFlow.V_PrivilegesWebService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="v_privilegesBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class v_privilegesBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback UserIsItemPrivilegeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserItemPrivilegeListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllPrivilegesListByAppIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAppIDByNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSysUserIDByLoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback SysUserLoginValidatorOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public v_privilegesBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_V_PrivilegesWebService_v_privilegesBLLservice;
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
        public event UserIsItemPrivilegeCompletedEventHandler UserIsItemPrivilegeCompleted;
        
        /// <remarks/>
        public event GetUserItemPrivilegeListCompletedEventHandler GetUserItemPrivilegeListCompleted;
        
        /// <remarks/>
        public event GetAllPrivilegesListByAppIDCompletedEventHandler GetAllPrivilegesListByAppIDCompleted;
        
        /// <remarks/>
        public event GetAppIDByNameCompletedEventHandler GetAppIDByNameCompleted;
        
        /// <remarks/>
        public event GetSysUserIDByLoginCompletedEventHandler GetSysUserIDByLoginCompleted;
        
        /// <remarks/>
        public event SysUserLoginValidatorCompletedEventHandler SysUserLoginValidatorCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/UserIsItemPrivilege", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UserIsItemPrivilege(int userID, string item_Code, string pt_Name, int appID, out string msg) {
            object[] results = this.Invoke("UserIsItemPrivilege", new object[] {
                        userID,
                        item_Code,
                        pt_Name,
                        appID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UserIsItemPrivilegeAsync(int userID, string item_Code, string pt_Name, int appID) {
            this.UserIsItemPrivilegeAsync(userID, item_Code, pt_Name, appID, null);
        }
        
        /// <remarks/>
        public void UserIsItemPrivilegeAsync(int userID, string item_Code, string pt_Name, int appID, object userState) {
            if ((this.UserIsItemPrivilegeOperationCompleted == null)) {
                this.UserIsItemPrivilegeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserIsItemPrivilegeOperationCompleted);
            }
            this.InvokeAsync("UserIsItemPrivilege", new object[] {
                        userID,
                        item_Code,
                        pt_Name,
                        appID}, this.UserIsItemPrivilegeOperationCompleted, userState);
        }
        
        private void OnUserIsItemPrivilegeOperationCompleted(object arg) {
            if ((this.UserIsItemPrivilegeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserIsItemPrivilegeCompleted(this, new UserIsItemPrivilegeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetUserItemPrivilegeList", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetUserItemPrivilegeList(int userID, out string msg) {
            object[] results = this.Invoke("GetUserItemPrivilegeList", new object[] {
                        userID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserItemPrivilegeListAsync(int userID) {
            this.GetUserItemPrivilegeListAsync(userID, null);
        }
        
        /// <remarks/>
        public void GetUserItemPrivilegeListAsync(int userID, object userState) {
            if ((this.GetUserItemPrivilegeListOperationCompleted == null)) {
                this.GetUserItemPrivilegeListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserItemPrivilegeListOperationCompleted);
            }
            this.InvokeAsync("GetUserItemPrivilegeList", new object[] {
                        userID}, this.GetUserItemPrivilegeListOperationCompleted, userState);
        }
        
        private void OnGetUserItemPrivilegeListOperationCompleted(object arg) {
            if ((this.GetUserItemPrivilegeListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserItemPrivilegeListCompleted(this, new GetUserItemPrivilegeListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetAllPrivilegesListByAppID", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAllPrivilegesListByAppID(int appID, out string msg) {
            object[] results = this.Invoke("GetAllPrivilegesListByAppID", new object[] {
                        appID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllPrivilegesListByAppIDAsync(int appID) {
            this.GetAllPrivilegesListByAppIDAsync(appID, null);
        }
        
        /// <remarks/>
        public void GetAllPrivilegesListByAppIDAsync(int appID, object userState) {
            if ((this.GetAllPrivilegesListByAppIDOperationCompleted == null)) {
                this.GetAllPrivilegesListByAppIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllPrivilegesListByAppIDOperationCompleted);
            }
            this.InvokeAsync("GetAllPrivilegesListByAppID", new object[] {
                        appID}, this.GetAllPrivilegesListByAppIDOperationCompleted, userState);
        }
        
        private void OnGetAllPrivilegesListByAppIDOperationCompleted(object arg) {
            if ((this.GetAllPrivilegesListByAppIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllPrivilegesListByAppIDCompleted(this, new GetAllPrivilegesListByAppIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetAppIDByName", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetAppIDByName(string appName, out string msg) {
            object[] results = this.Invoke("GetAppIDByName", new object[] {
                        appName});
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetAppIDByNameAsync(string appName) {
            this.GetAppIDByNameAsync(appName, null);
        }
        
        /// <remarks/>
        public void GetAppIDByNameAsync(string appName, object userState) {
            if ((this.GetAppIDByNameOperationCompleted == null)) {
                this.GetAppIDByNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAppIDByNameOperationCompleted);
            }
            this.InvokeAsync("GetAppIDByName", new object[] {
                        appName}, this.GetAppIDByNameOperationCompleted, userState);
        }
        
        private void OnGetAppIDByNameOperationCompleted(object arg) {
            if ((this.GetAppIDByNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAppIDByNameCompleted(this, new GetAppIDByNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetSysUserIDByLogin", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetSysUserIDByLogin(string userLogin, int appID, out string msg) {
            object[] results = this.Invoke("GetSysUserIDByLogin", new object[] {
                        userLogin,
                        appID});
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetSysUserIDByLoginAsync(string userLogin, int appID) {
            this.GetSysUserIDByLoginAsync(userLogin, appID, null);
        }
        
        /// <remarks/>
        public void GetSysUserIDByLoginAsync(string userLogin, int appID, object userState) {
            if ((this.GetSysUserIDByLoginOperationCompleted == null)) {
                this.GetSysUserIDByLoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSysUserIDByLoginOperationCompleted);
            }
            this.InvokeAsync("GetSysUserIDByLogin", new object[] {
                        userLogin,
                        appID}, this.GetSysUserIDByLoginOperationCompleted, userState);
        }
        
        private void OnGetSysUserIDByLoginOperationCompleted(object arg) {
            if ((this.GetSysUserIDByLoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSysUserIDByLoginCompleted(this, new GetSysUserIDByLoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/SysUserLoginValidator", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SysUserLoginValidator(string userName, string password, int appID, out string msg) {
            object[] results = this.Invoke("SysUserLoginValidator", new object[] {
                        userName,
                        password,
                        appID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SysUserLoginValidatorAsync(string userName, string password, int appID) {
            this.SysUserLoginValidatorAsync(userName, password, appID, null);
        }
        
        /// <remarks/>
        public void SysUserLoginValidatorAsync(string userName, string password, int appID, object userState) {
            if ((this.SysUserLoginValidatorOperationCompleted == null)) {
                this.SysUserLoginValidatorOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSysUserLoginValidatorOperationCompleted);
            }
            this.InvokeAsync("SysUserLoginValidator", new object[] {
                        userName,
                        password,
                        appID}, this.SysUserLoginValidatorOperationCompleted, userState);
        }
        
        private void OnSysUserLoginValidatorOperationCompleted(object arg) {
            if ((this.SysUserLoginValidatorCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SysUserLoginValidatorCompleted(this, new SysUserLoginValidatorCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void UserIsItemPrivilegeCompletedEventHandler(object sender, UserIsItemPrivilegeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserIsItemPrivilegeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserIsItemPrivilegeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetUserItemPrivilegeListCompletedEventHandler(object sender, GetUserItemPrivilegeListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserItemPrivilegeListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserItemPrivilegeListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetAllPrivilegesListByAppIDCompletedEventHandler(object sender, GetAllPrivilegesListByAppIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllPrivilegesListByAppIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllPrivilegesListByAppIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetAppIDByNameCompletedEventHandler(object sender, GetAppIDByNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAppIDByNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAppIDByNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
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
    public delegate void GetSysUserIDByLoginCompletedEventHandler(object sender, GetSysUserIDByLoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSysUserIDByLoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSysUserIDByLoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
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
    public delegate void SysUserLoginValidatorCompletedEventHandler(object sender, SysUserLoginValidatorCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SysUserLoginValidatorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SysUserLoginValidatorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591