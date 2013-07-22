﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.1008 版自动生成。
// 
#pragma warning disable 1591

namespace WorkFlow.Privileges_RoleWebService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="privilege_roleBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class privilege_roleBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback ExistsOperationCompleted;
        
        private System.Threading.SendOrPostCallback Privilege_RoleCountByRoleIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteByRoleIDOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public privilege_roleBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_Privileges_RoleWebService1_privilege_roleBLLservice;
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
        public event ExistsCompletedEventHandler ExistsCompleted;
        
        /// <remarks/>
        public event Privilege_RoleCountByRoleIDCompletedEventHandler Privilege_RoleCountByRoleIDCompleted;
        
        /// <remarks/>
        public event AddCompletedEventHandler AddCompleted;
        
        /// <remarks/>
        public event DeleteCompletedEventHandler DeleteCompleted;
        
        /// <remarks/>
        public event DeleteByRoleIDCompletedEventHandler DeleteByRoleIDCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Exists", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Exists(int role_id, int privilege_id, out string msg) {
            object[] results = this.Invoke("Exists", new object[] {
                        role_id,
                        privilege_id});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ExistsAsync(int role_id, int privilege_id) {
            this.ExistsAsync(role_id, privilege_id, null);
        }
        
        /// <remarks/>
        public void ExistsAsync(int role_id, int privilege_id, object userState) {
            if ((this.ExistsOperationCompleted == null)) {
                this.ExistsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExistsOperationCompleted);
            }
            this.InvokeAsync("Exists", new object[] {
                        role_id,
                        privilege_id}, this.ExistsOperationCompleted, userState);
        }
        
        private void OnExistsOperationCompleted(object arg) {
            if ((this.ExistsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExistsCompleted(this, new ExistsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Privilege_RoleCountByRoleID", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Privilege_RoleCountByRoleID(int role_id) {
            object[] results = this.Invoke("Privilege_RoleCountByRoleID", new object[] {
                        role_id});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void Privilege_RoleCountByRoleIDAsync(int role_id) {
            this.Privilege_RoleCountByRoleIDAsync(role_id, null);
        }
        
        /// <remarks/>
        public void Privilege_RoleCountByRoleIDAsync(int role_id, object userState) {
            if ((this.Privilege_RoleCountByRoleIDOperationCompleted == null)) {
                this.Privilege_RoleCountByRoleIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPrivilege_RoleCountByRoleIDOperationCompleted);
            }
            this.InvokeAsync("Privilege_RoleCountByRoleID", new object[] {
                        role_id}, this.Privilege_RoleCountByRoleIDOperationCompleted, userState);
        }
        
        private void OnPrivilege_RoleCountByRoleIDOperationCompleted(object arg) {
            if ((this.Privilege_RoleCountByRoleIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Privilege_RoleCountByRoleIDCompleted(this, new Privilege_RoleCountByRoleIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Add", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Add(privilege_roleModel model) {
            object[] results = this.Invoke("Add", new object[] {
                        model});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddAsync(privilege_roleModel model) {
            this.AddAsync(model, null);
        }
        
        /// <remarks/>
        public void AddAsync(privilege_roleModel model, object userState) {
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/Delete", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Delete(int role_id, int privilege_id) {
            object[] results = this.Invoke("Delete", new object[] {
                        role_id,
                        privilege_id});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAsync(int role_id, int privilege_id) {
            this.DeleteAsync(role_id, privilege_id, null);
        }
        
        /// <remarks/>
        public void DeleteAsync(int role_id, int privilege_id, object userState) {
            if ((this.DeleteOperationCompleted == null)) {
                this.DeleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteOperationCompleted);
            }
            this.InvokeAsync("Delete", new object[] {
                        role_id,
                        privilege_id}, this.DeleteOperationCompleted, userState);
        }
        
        private void OnDeleteOperationCompleted(object arg) {
            if ((this.DeleteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCompleted(this, new DeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/DeleteByRoleID", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteByRoleID(int role_id) {
            object[] results = this.Invoke("DeleteByRoleID", new object[] {
                        role_id});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteByRoleIDAsync(int role_id) {
            this.DeleteByRoleIDAsync(role_id, null);
        }
        
        /// <remarks/>
        public void DeleteByRoleIDAsync(int role_id, object userState) {
            if ((this.DeleteByRoleIDOperationCompleted == null)) {
                this.DeleteByRoleIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteByRoleIDOperationCompleted);
            }
            this.InvokeAsync("DeleteByRoleID", new object[] {
                        role_id}, this.DeleteByRoleIDOperationCompleted, userState);
        }
        
        private void OnDeleteByRoleIDOperationCompleted(object arg) {
            if ((this.DeleteByRoleIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteByRoleIDCompleted(this, new DeleteByRoleIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://saron.workflowservice.org/")]
    public partial class privilege_roleModel {
        
        private int role_idField;
        
        private int privilege_idField;
        
        /// <remarks/>
        public int role_id {
            get {
                return this.role_idField;
            }
            set {
                this.role_idField = value;
            }
        }
        
        /// <remarks/>
        public int privilege_id {
            get {
                return this.privilege_idField;
            }
            set {
                this.privilege_idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ExistsCompletedEventHandler(object sender, ExistsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExistsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExistsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void Privilege_RoleCountByRoleIDCompletedEventHandler(object sender, Privilege_RoleCountByRoleIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Privilege_RoleCountByRoleIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Privilege_RoleCountByRoleIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
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
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void DeleteByRoleIDCompletedEventHandler(object sender, DeleteByRoleIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteByRoleIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteByRoleIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591