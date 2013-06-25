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

namespace WorkFlow.AppsWebService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="appsBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class appsBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback ExistsAppNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddOperationCompleted;
        
        private System.Threading.SendOrPostCallback SuperAdminUpdateAppOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetModelOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetInvalidAppsListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetValidAppsListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetValidAppCountOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetInValidAppCountOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public appsBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_AppsWebService_appsBLLservice;
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
        public event ExistsAppNameCompletedEventHandler ExistsAppNameCompleted;
        
        /// <remarks/>
        public event AddCompletedEventHandler AddCompleted;
        
        /// <remarks/>
        public event SuperAdminUpdateAppCompletedEventHandler SuperAdminUpdateAppCompleted;
        
        /// <remarks/>
        public event DeleteCompletedEventHandler DeleteCompleted;
        
        /// <remarks/>
        public event GetModelCompletedEventHandler GetModelCompleted;
        
        /// <remarks/>
        public event GetInvalidAppsListCompletedEventHandler GetInvalidAppsListCompleted;
        
        /// <remarks/>
        public event GetValidAppsListCompletedEventHandler GetValidAppsListCompleted;
        
        /// <remarks/>
        public event GetValidAppCountCompletedEventHandler GetValidAppCountCompleted;
        
        /// <remarks/>
        public event GetInValidAppCountCompletedEventHandler GetInValidAppCountCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/ExistsAppName", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ExistsAppName(string appName, out string msg) {
            object[] results = this.Invoke("ExistsAppName", new object[] {
                        appName});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ExistsAppNameAsync(string appName) {
            this.ExistsAppNameAsync(appName, null);
        }
        
        /// <remarks/>
        public void ExistsAppNameAsync(string appName, object userState) {
            if ((this.ExistsAppNameOperationCompleted == null)) {
                this.ExistsAppNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExistsAppNameOperationCompleted);
            }
            this.InvokeAsync("ExistsAppName", new object[] {
                        appName}, this.ExistsAppNameOperationCompleted, userState);
        }
        
        private void OnExistsAppNameOperationCompleted(object arg) {
            if ((this.ExistsAppNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExistsAppNameCompleted(this, new ExistsAppNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Add", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Add(appsModel model, out string msg) {
            object[] results = this.Invoke("Add", new object[] {
                        model});
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddAsync(appsModel model) {
            this.AddAsync(model, null);
        }
        
        /// <remarks/>
        public void AddAsync(appsModel model, object userState) {
            if ((this.AddOperationCompleted == null)) {
                this.AddOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddOperationCompleted);
            }
            this.InvokeAsync("Add", new object[] {
                        model}, this.AddOperationCompleted, userState);
        }
        
        private void OnAddOperationCompleted(object arg) {
            if ((this.AddCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddCompleted(this, new AddCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/SuperAdminUpdateApp", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SuperAdminUpdateApp(appsModel model, out string msg) {
            object[] results = this.Invoke("SuperAdminUpdateApp", new object[] {
                        model});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SuperAdminUpdateAppAsync(appsModel model) {
            this.SuperAdminUpdateAppAsync(model, null);
        }
        
        /// <remarks/>
        public void SuperAdminUpdateAppAsync(appsModel model, object userState) {
            if ((this.SuperAdminUpdateAppOperationCompleted == null)) {
                this.SuperAdminUpdateAppOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSuperAdminUpdateAppOperationCompleted);
            }
            this.InvokeAsync("SuperAdminUpdateApp", new object[] {
                        model}, this.SuperAdminUpdateAppOperationCompleted, userState);
        }
        
        private void OnSuperAdminUpdateAppOperationCompleted(object arg) {
            if ((this.SuperAdminUpdateAppCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SuperAdminUpdateAppCompleted(this, new SuperAdminUpdateAppCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Delete", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Delete(int id, out string msg) {
            object[] results = this.Invoke("Delete", new object[] {
                        id});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAsync(int id) {
            this.DeleteAsync(id, null);
        }
        
        /// <remarks/>
        public void DeleteAsync(int id, object userState) {
            if ((this.DeleteOperationCompleted == null)) {
                this.DeleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteOperationCompleted);
            }
            this.InvokeAsync("Delete", new object[] {
                        id}, this.DeleteOperationCompleted, userState);
        }
        
        private void OnDeleteOperationCompleted(object arg) {
            if ((this.DeleteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCompleted(this, new DeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetModel", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public appsModel GetModel(int id, out string msg) {
            object[] results = this.Invoke("GetModel", new object[] {
                        id});
            msg = ((string)(results[1]));
            return ((appsModel)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetInvalidAppsList", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetInvalidAppsList(out string msg) {
            object[] results = this.Invoke("GetInvalidAppsList", new object[0]);
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetInvalidAppsListAsync() {
            this.GetInvalidAppsListAsync(null);
        }
        
        /// <remarks/>
        public void GetInvalidAppsListAsync(object userState) {
            if ((this.GetInvalidAppsListOperationCompleted == null)) {
                this.GetInvalidAppsListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetInvalidAppsListOperationCompleted);
            }
            this.InvokeAsync("GetInvalidAppsList", new object[0], this.GetInvalidAppsListOperationCompleted, userState);
        }
        
        private void OnGetInvalidAppsListOperationCompleted(object arg) {
            if ((this.GetInvalidAppsListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetInvalidAppsListCompleted(this, new GetInvalidAppsListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetValidAppsList", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetValidAppsList(out string msg) {
            object[] results = this.Invoke("GetValidAppsList", new object[0]);
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetValidAppsListAsync() {
            this.GetValidAppsListAsync(null);
        }
        
        /// <remarks/>
        public void GetValidAppsListAsync(object userState) {
            if ((this.GetValidAppsListOperationCompleted == null)) {
                this.GetValidAppsListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetValidAppsListOperationCompleted);
            }
            this.InvokeAsync("GetValidAppsList", new object[0], this.GetValidAppsListOperationCompleted, userState);
        }
        
        private void OnGetValidAppsListOperationCompleted(object arg) {
            if ((this.GetValidAppsListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetValidAppsListCompleted(this, new GetValidAppsListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetValidAppCount", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetValidAppCount(out string msg) {
            object[] results = this.Invoke("GetValidAppCount", new object[0]);
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetValidAppCountAsync() {
            this.GetValidAppCountAsync(null);
        }
        
        /// <remarks/>
        public void GetValidAppCountAsync(object userState) {
            if ((this.GetValidAppCountOperationCompleted == null)) {
                this.GetValidAppCountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetValidAppCountOperationCompleted);
            }
            this.InvokeAsync("GetValidAppCount", new object[0], this.GetValidAppCountOperationCompleted, userState);
        }
        
        private void OnGetValidAppCountOperationCompleted(object arg) {
            if ((this.GetValidAppCountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetValidAppCountCompleted(this, new GetValidAppCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetInValidAppCount", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetInValidAppCount(out string msg) {
            object[] results = this.Invoke("GetInValidAppCount", new object[0]);
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetInValidAppCountAsync() {
            this.GetInValidAppCountAsync(null);
        }
        
        /// <remarks/>
        public void GetInValidAppCountAsync(object userState) {
            if ((this.GetInValidAppCountOperationCompleted == null)) {
                this.GetInValidAppCountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetInValidAppCountOperationCompleted);
            }
            this.InvokeAsync("GetInValidAppCount", new object[0], this.GetInValidAppCountOperationCompleted, userState);
        }
        
        private void OnGetInValidAppCountOperationCompleted(object arg) {
            if ((this.GetInValidAppCountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetInValidAppCountCompleted(this, new GetInValidAppCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public partial class appsModel {
        
        private int idField;
        
        private string nameField;
        
        private string codeField;
        
        private string urlField;
        
        private string remarkField;
        
        private bool invalidField;
        
        private System.DateTime created_atField;
        
        private string created_ipField;
        
        private System.Nullable<System.DateTime> updated_atField;
        
        private System.Nullable<int> updated_byField;
        
        private string updated_ipField;
        
        private System.Nullable<System.DateTime> apply_atField;
        
        private System.Nullable<System.DateTime> approval_atField;
        
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
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string url {
            get {
                return this.urlField;
            }
            set {
                this.urlField = value;
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
        public bool invalid {
            get {
                return this.invalidField;
            }
            set {
                this.invalidField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime created_at {
            get {
                return this.created_atField;
            }
            set {
                this.created_atField = value;
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> apply_at {
            get {
                return this.apply_atField;
            }
            set {
                this.apply_atField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> approval_at {
            get {
                return this.approval_atField;
            }
            set {
                this.approval_atField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ExistsAppNameCompletedEventHandler(object sender, ExistsAppNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExistsAppNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExistsAppNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddCompletedEventHandler(object sender, AddCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SuperAdminUpdateAppCompletedEventHandler(object sender, SuperAdminUpdateAppCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SuperAdminUpdateAppCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SuperAdminUpdateAppCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DeleteCompletedEventHandler(object sender, DeleteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public appsModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((appsModel)(this.results[0]));
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
    public delegate void GetInvalidAppsListCompletedEventHandler(object sender, GetInvalidAppsListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetInvalidAppsListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetInvalidAppsListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetValidAppsListCompletedEventHandler(object sender, GetValidAppsListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetValidAppsListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetValidAppsListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetValidAppCountCompletedEventHandler(object sender, GetValidAppCountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetValidAppCountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetValidAppCountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetInValidAppCountCompletedEventHandler(object sender, GetInValidAppCountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetInValidAppCountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetInValidAppCountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591