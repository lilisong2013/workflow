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

namespace WorkFlow.FlowsWebService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="flowsBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class flowsBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback ExistsFlowNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddFlowOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetListOfFlowsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetListOfFlowsByNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteFlowOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateFlowOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlowModelOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public flowsBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_FlowsWebService_flowsBLLservice;
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
        public event ExistsFlowNameCompletedEventHandler ExistsFlowNameCompleted;
        
        /// <remarks/>
        public event AddFlowCompletedEventHandler AddFlowCompleted;
        
        /// <remarks/>
        public event GetListOfFlowsCompletedEventHandler GetListOfFlowsCompleted;
        
        /// <remarks/>
        public event GetListOfFlowsByNameCompletedEventHandler GetListOfFlowsByNameCompleted;
        
        /// <remarks/>
        public event DeleteFlowCompletedEventHandler DeleteFlowCompleted;
        
        /// <remarks/>
        public event UpdateFlowCompletedEventHandler UpdateFlowCompleted;
        
        /// <remarks/>
        public event GetFlowModelCompletedEventHandler GetFlowModelCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/ExistsFlowName", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ExistsFlowName(string name, int appID, out string msg) {
            object[] results = this.Invoke("ExistsFlowName", new object[] {
                        name,
                        appID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ExistsFlowNameAsync(string name, int appID) {
            this.ExistsFlowNameAsync(name, appID, null);
        }
        
        /// <remarks/>
        public void ExistsFlowNameAsync(string name, int appID, object userState) {
            if ((this.ExistsFlowNameOperationCompleted == null)) {
                this.ExistsFlowNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExistsFlowNameOperationCompleted);
            }
            this.InvokeAsync("ExistsFlowName", new object[] {
                        name,
                        appID}, this.ExistsFlowNameOperationCompleted, userState);
        }
        
        private void OnExistsFlowNameOperationCompleted(object arg) {
            if ((this.ExistsFlowNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ExistsFlowNameCompleted(this, new ExistsFlowNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/AddFlow", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddFlow(flowsModel model, out string msg) {
            object[] results = this.Invoke("AddFlow", new object[] {
                        model});
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddFlowAsync(flowsModel model) {
            this.AddFlowAsync(model, null);
        }
        
        /// <remarks/>
        public void AddFlowAsync(flowsModel model, object userState) {
            if ((this.AddFlowOperationCompleted == null)) {
                this.AddFlowOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddFlowOperationCompleted);
            }
            this.InvokeAsync("AddFlow", new object[] {
                        model}, this.AddFlowOperationCompleted, userState);
        }
        
        private void OnAddFlowOperationCompleted(object arg) {
            if ((this.AddFlowCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddFlowCompleted(this, new AddFlowCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetListOfFlows", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetListOfFlows(int appID, out string msg) {
            object[] results = this.Invoke("GetListOfFlows", new object[] {
                        appID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetListOfFlowsAsync(int appID) {
            this.GetListOfFlowsAsync(appID, null);
        }
        
        /// <remarks/>
        public void GetListOfFlowsAsync(int appID, object userState) {
            if ((this.GetListOfFlowsOperationCompleted == null)) {
                this.GetListOfFlowsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListOfFlowsOperationCompleted);
            }
            this.InvokeAsync("GetListOfFlows", new object[] {
                        appID}, this.GetListOfFlowsOperationCompleted, userState);
        }
        
        private void OnGetListOfFlowsOperationCompleted(object arg) {
            if ((this.GetListOfFlowsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListOfFlowsCompleted(this, new GetListOfFlowsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetListOfFlowsByName", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetListOfFlowsByName(string flowName, int appID, out string msg) {
            object[] results = this.Invoke("GetListOfFlowsByName", new object[] {
                        flowName,
                        appID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetListOfFlowsByNameAsync(string flowName, int appID) {
            this.GetListOfFlowsByNameAsync(flowName, appID, null);
        }
        
        /// <remarks/>
        public void GetListOfFlowsByNameAsync(string flowName, int appID, object userState) {
            if ((this.GetListOfFlowsByNameOperationCompleted == null)) {
                this.GetListOfFlowsByNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListOfFlowsByNameOperationCompleted);
            }
            this.InvokeAsync("GetListOfFlowsByName", new object[] {
                        flowName,
                        appID}, this.GetListOfFlowsByNameOperationCompleted, userState);
        }
        
        private void OnGetListOfFlowsByNameOperationCompleted(object arg) {
            if ((this.GetListOfFlowsByNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListOfFlowsByNameCompleted(this, new GetListOfFlowsByNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/DeleteFlow", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteFlow(int id, out string msg) {
            object[] results = this.Invoke("DeleteFlow", new object[] {
                        id});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteFlowAsync(int id) {
            this.DeleteFlowAsync(id, null);
        }
        
        /// <remarks/>
        public void DeleteFlowAsync(int id, object userState) {
            if ((this.DeleteFlowOperationCompleted == null)) {
                this.DeleteFlowOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteFlowOperationCompleted);
            }
            this.InvokeAsync("DeleteFlow", new object[] {
                        id}, this.DeleteFlowOperationCompleted, userState);
        }
        
        private void OnDeleteFlowOperationCompleted(object arg) {
            if ((this.DeleteFlowCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteFlowCompleted(this, new DeleteFlowCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/UpdateFlow", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateFlow(flowsModel flowModel, out string msg) {
            object[] results = this.Invoke("UpdateFlow", new object[] {
                        flowModel});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateFlowAsync(flowsModel flowModel) {
            this.UpdateFlowAsync(flowModel, null);
        }
        
        /// <remarks/>
        public void UpdateFlowAsync(flowsModel flowModel, object userState) {
            if ((this.UpdateFlowOperationCompleted == null)) {
                this.UpdateFlowOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateFlowOperationCompleted);
            }
            this.InvokeAsync("UpdateFlow", new object[] {
                        flowModel}, this.UpdateFlowOperationCompleted, userState);
        }
        
        private void OnUpdateFlowOperationCompleted(object arg) {
            if ((this.UpdateFlowCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateFlowCompleted(this, new UpdateFlowCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetFlowModel", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public flowsModel GetFlowModel(int id, out string msg) {
            object[] results = this.Invoke("GetFlowModel", new object[] {
                        id});
            msg = ((string)(results[1]));
            return ((flowsModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlowModelAsync(int id) {
            this.GetFlowModelAsync(id, null);
        }
        
        /// <remarks/>
        public void GetFlowModelAsync(int id, object userState) {
            if ((this.GetFlowModelOperationCompleted == null)) {
                this.GetFlowModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlowModelOperationCompleted);
            }
            this.InvokeAsync("GetFlowModel", new object[] {
                        id}, this.GetFlowModelOperationCompleted, userState);
        }
        
        private void OnGetFlowModelOperationCompleted(object arg) {
            if ((this.GetFlowModelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlowModelCompleted(this, new GetFlowModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://saron.workflowservice.org/")]
    public partial class flowsModel {
        
        private int idField;
        
        private string nameField;
        
        private string remarkField;
        
        private bool invalidField;
        
        private bool deletedField;
        
        private System.DateTime created_atField;
        
        private int created_byField;
        
        private string created_ipField;
        
        private System.DateTime updated_atField;
        
        private int updated_byField;
        
        private string updated_ipField;
        
        private System.Nullable<int> app_idField;
        
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
        public bool deleted {
            get {
                return this.deletedField;
            }
            set {
                this.deletedField = value;
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
        public int created_by {
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
        public System.DateTime updated_at {
            get {
                return this.updated_atField;
            }
            set {
                this.updated_atField = value;
            }
        }
        
        /// <remarks/>
        public int updated_by {
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
        public System.Nullable<int> app_id {
            get {
                return this.app_idField;
            }
            set {
                this.app_idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ExistsFlowNameCompletedEventHandler(object sender, ExistsFlowNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExistsFlowNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ExistsFlowNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddFlowCompletedEventHandler(object sender, AddFlowCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddFlowCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddFlowCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetListOfFlowsCompletedEventHandler(object sender, GetListOfFlowsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListOfFlowsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListOfFlowsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetListOfFlowsByNameCompletedEventHandler(object sender, GetListOfFlowsByNameCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListOfFlowsByNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListOfFlowsByNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DeleteFlowCompletedEventHandler(object sender, DeleteFlowCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteFlowCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteFlowCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UpdateFlowCompletedEventHandler(object sender, UpdateFlowCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateFlowCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateFlowCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetFlowModelCompletedEventHandler(object sender, GetFlowModelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlowModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlowModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public flowsModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((flowsModel)(this.results[0]));
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