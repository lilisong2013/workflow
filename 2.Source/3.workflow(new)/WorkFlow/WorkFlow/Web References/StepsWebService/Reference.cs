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

namespace WorkFlow.StepsWebService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="stepsBLLserviceSoap", Namespace="http://saron.workflowservice.org/")]
    public partial class stepsBLLservice : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private SecurityContext securityContextValueField;
        
        private System.Threading.SendOrPostCallback AddStepOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddStepAndAllInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlowMaxOrderNumOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlowStepListByFlowIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFlowStepListByAppIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetV_StepsModelOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteStepOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public stepsBLLservice() {
            this.Url = global::WorkFlow.Properties.Settings.Default.WorkFlow_StepsWebService_stepsBLLservice;
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
        public event AddStepCompletedEventHandler AddStepCompleted;
        
        /// <remarks/>
        public event AddStepAndAllInfoCompletedEventHandler AddStepAndAllInfoCompleted;
        
        /// <remarks/>
        public event GetFlowMaxOrderNumCompletedEventHandler GetFlowMaxOrderNumCompleted;
        
        /// <remarks/>
        public event GetFlowStepListByFlowIDCompletedEventHandler GetFlowStepListByFlowIDCompleted;
        
        /// <remarks/>
        public event GetFlowStepListByAppIDCompletedEventHandler GetFlowStepListByAppIDCompleted;
        
        /// <remarks/>
        public event GetV_StepsModelCompletedEventHandler GetV_StepsModelCompleted;
        
        /// <remarks/>
        public event DeleteStepCompletedEventHandler DeleteStepCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/AddStep", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddStep(stepsModel stepmodel, int userID, out string msg) {
            object[] results = this.Invoke("AddStep", new object[] {
                        stepmodel,
                        userID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddStepAsync(stepsModel stepmodel, int userID) {
            this.AddStepAsync(stepmodel, userID, null);
        }
        
        /// <remarks/>
        public void AddStepAsync(stepsModel stepmodel, int userID, object userState) {
            if ((this.AddStepOperationCompleted == null)) {
                this.AddStepOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddStepOperationCompleted);
            }
            this.InvokeAsync("AddStep", new object[] {
                        stepmodel,
                        userID}, this.AddStepOperationCompleted, userState);
        }
        
        private void OnAddStepOperationCompleted(object arg) {
            if ((this.AddStepCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddStepCompleted(this, new AddStepCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/AddStepAndAllInfo", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddStepAndAllInfo(stepsModel stepmodel, int userID, out string msg) {
            object[] results = this.Invoke("AddStepAndAllInfo", new object[] {
                        stepmodel,
                        userID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AddStepAndAllInfoAsync(stepsModel stepmodel, int userID) {
            this.AddStepAndAllInfoAsync(stepmodel, userID, null);
        }
        
        /// <remarks/>
        public void AddStepAndAllInfoAsync(stepsModel stepmodel, int userID, object userState) {
            if ((this.AddStepAndAllInfoOperationCompleted == null)) {
                this.AddStepAndAllInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddStepAndAllInfoOperationCompleted);
            }
            this.InvokeAsync("AddStepAndAllInfo", new object[] {
                        stepmodel,
                        userID}, this.AddStepAndAllInfoOperationCompleted, userState);
        }
        
        private void OnAddStepAndAllInfoOperationCompleted(object arg) {
            if ((this.AddStepAndAllInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddStepAndAllInfoCompleted(this, new AddStepAndAllInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetFlowMaxOrderNum", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetFlowMaxOrderNum(int flowID, out string msg) {
            object[] results = this.Invoke("GetFlowMaxOrderNum", new object[] {
                        flowID});
            msg = ((string)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlowMaxOrderNumAsync(int flowID) {
            this.GetFlowMaxOrderNumAsync(flowID, null);
        }
        
        /// <remarks/>
        public void GetFlowMaxOrderNumAsync(int flowID, object userState) {
            if ((this.GetFlowMaxOrderNumOperationCompleted == null)) {
                this.GetFlowMaxOrderNumOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlowMaxOrderNumOperationCompleted);
            }
            this.InvokeAsync("GetFlowMaxOrderNum", new object[] {
                        flowID}, this.GetFlowMaxOrderNumOperationCompleted, userState);
        }
        
        private void OnGetFlowMaxOrderNumOperationCompleted(object arg) {
            if ((this.GetFlowMaxOrderNumCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlowMaxOrderNumCompleted(this, new GetFlowMaxOrderNumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetFlowStepListByFlowID", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetFlowStepListByFlowID(int flowID, out string msg) {
            object[] results = this.Invoke("GetFlowStepListByFlowID", new object[] {
                        flowID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlowStepListByFlowIDAsync(int flowID) {
            this.GetFlowStepListByFlowIDAsync(flowID, null);
        }
        
        /// <remarks/>
        public void GetFlowStepListByFlowIDAsync(int flowID, object userState) {
            if ((this.GetFlowStepListByFlowIDOperationCompleted == null)) {
                this.GetFlowStepListByFlowIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlowStepListByFlowIDOperationCompleted);
            }
            this.InvokeAsync("GetFlowStepListByFlowID", new object[] {
                        flowID}, this.GetFlowStepListByFlowIDOperationCompleted, userState);
        }
        
        private void OnGetFlowStepListByFlowIDOperationCompleted(object arg) {
            if ((this.GetFlowStepListByFlowIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlowStepListByFlowIDCompleted(this, new GetFlowStepListByFlowIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetFlowStepListByAppID", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetFlowStepListByAppID(int appID, out string msg) {
            object[] results = this.Invoke("GetFlowStepListByAppID", new object[] {
                        appID});
            msg = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetFlowStepListByAppIDAsync(int appID) {
            this.GetFlowStepListByAppIDAsync(appID, null);
        }
        
        /// <remarks/>
        public void GetFlowStepListByAppIDAsync(int appID, object userState) {
            if ((this.GetFlowStepListByAppIDOperationCompleted == null)) {
                this.GetFlowStepListByAppIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFlowStepListByAppIDOperationCompleted);
            }
            this.InvokeAsync("GetFlowStepListByAppID", new object[] {
                        appID}, this.GetFlowStepListByAppIDOperationCompleted, userState);
        }
        
        private void OnGetFlowStepListByAppIDOperationCompleted(object arg) {
            if ((this.GetFlowStepListByAppIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFlowStepListByAppIDCompleted(this, new GetFlowStepListByAppIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/GetV_StepsModel", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public v_stepsModel GetV_StepsModel(int stepID, out string msg) {
            object[] results = this.Invoke("GetV_StepsModel", new object[] {
                        stepID});
            msg = ((string)(results[1]));
            return ((v_stepsModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetV_StepsModelAsync(int stepID) {
            this.GetV_StepsModelAsync(stepID, null);
        }
        
        /// <remarks/>
        public void GetV_StepsModelAsync(int stepID, object userState) {
            if ((this.GetV_StepsModelOperationCompleted == null)) {
                this.GetV_StepsModelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetV_StepsModelOperationCompleted);
            }
            this.InvokeAsync("GetV_StepsModel", new object[] {
                        stepID}, this.GetV_StepsModelOperationCompleted, userState);
        }
        
        private void OnGetV_StepsModelOperationCompleted(object arg) {
            if ((this.GetV_StepsModelCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetV_StepsModelCompleted(this, new GetV_StepsModelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("SecurityContextValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://saron.workflowservice.org/DeleteStep", RequestNamespace="http://saron.workflowservice.org/", ResponseNamespace="http://saron.workflowservice.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool DeleteStep(int stepID, out string msg) {
            object[] results = this.Invoke("DeleteStep", new object[] {
                        stepID});
            msg = ((string)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteStepAsync(int stepID) {
            this.DeleteStepAsync(stepID, null);
        }
        
        /// <remarks/>
        public void DeleteStepAsync(int stepID, object userState) {
            if ((this.DeleteStepOperationCompleted == null)) {
                this.DeleteStepOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteStepOperationCompleted);
            }
            this.InvokeAsync("DeleteStep", new object[] {
                        stepID}, this.DeleteStepOperationCompleted, userState);
        }
        
        private void OnDeleteStepOperationCompleted(object arg) {
            if ((this.DeleteStepCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteStepCompleted(this, new DeleteStepCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public partial class v_stepsModel {
        
        private int s_idField;
        
        private string s_nameField;
        
        private string f_nameField;
        
        private string step_type_nameField;
        
        private int order_noField;
        
        private System.Nullable<int> app_idField;
        
        private int f_idField;
        
        /// <remarks/>
        public int s_id {
            get {
                return this.s_idField;
            }
            set {
                this.s_idField = value;
            }
        }
        
        /// <remarks/>
        public string s_name {
            get {
                return this.s_nameField;
            }
            set {
                this.s_nameField = value;
            }
        }
        
        /// <remarks/>
        public string f_name {
            get {
                return this.f_nameField;
            }
            set {
                this.f_nameField = value;
            }
        }
        
        /// <remarks/>
        public string step_type_name {
            get {
                return this.step_type_nameField;
            }
            set {
                this.step_type_nameField = value;
            }
        }
        
        /// <remarks/>
        public int order_no {
            get {
                return this.order_noField;
            }
            set {
                this.order_noField = value;
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
        
        /// <remarks/>
        public int f_id {
            get {
                return this.f_idField;
            }
            set {
                this.f_idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1015")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://saron.workflowservice.org/")]
    public partial class stepsModel {
        
        private int idField;
        
        private string nameField;
        
        private string remarkField;
        
        private System.Nullable<int> flow_idField;
        
        private System.Nullable<int> step_type_idField;
        
        private System.Nullable<int> repeat_countField;
        
        private bool invalidField;
        
        private int order_noField;
        
        private bool deletedField;
        
        private System.DateTime created_atField;
        
        private int created_byField;
        
        private string created_ipField;
        
        private System.DateTime updated_atField;
        
        private int updated_byField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> flow_id {
            get {
                return this.flow_idField;
            }
            set {
                this.flow_idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> step_type_id {
            get {
                return this.step_type_idField;
            }
            set {
                this.step_type_idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> repeat_count {
            get {
                return this.repeat_countField;
            }
            set {
                this.repeat_countField = value;
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
        public int order_no {
            get {
                return this.order_noField;
            }
            set {
                this.order_noField = value;
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
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AddStepCompletedEventHandler(object sender, AddStepCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddStepCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddStepCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddStepAndAllInfoCompletedEventHandler(object sender, AddStepAndAllInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddStepAndAllInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddStepAndAllInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetFlowMaxOrderNumCompletedEventHandler(object sender, GetFlowMaxOrderNumCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlowMaxOrderNumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlowMaxOrderNumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetFlowStepListByFlowIDCompletedEventHandler(object sender, GetFlowStepListByFlowIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlowStepListByFlowIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlowStepListByFlowIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetFlowStepListByAppIDCompletedEventHandler(object sender, GetFlowStepListByAppIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFlowStepListByAppIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFlowStepListByAppIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetV_StepsModelCompletedEventHandler(object sender, GetV_StepsModelCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetV_StepsModelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetV_StepsModelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public v_stepsModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((v_stepsModel)(this.results[0]));
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
    public delegate void DeleteStepCompletedEventHandler(object sender, DeleteStepCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteStepCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteStepCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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