﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18444 版自动生成。
// 
#pragma warning disable 1591

namespace superuser.WebReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DALWebServiceSoap", Namespace="http://202.114.18.229:2000")]
    public partial class DALWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDocListOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnsureDownloadOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpTimeOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpMoneyOperationCompleted;
        
        private System.Threading.SendOrPostCallback SuperCheckOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnsureCheckOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public DALWebService() {
            this.Url = global::superuser.Properties.Settings.Default.superuser_WebReference_DALWebService;
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
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event GetDocListCompletedEventHandler GetDocListCompleted;
        
        /// <remarks/>
        public event EnsureDownloadCompletedEventHandler EnsureDownloadCompleted;
        
        /// <remarks/>
        public event UpTimeCompletedEventHandler UpTimeCompleted;
        
        /// <remarks/>
        public event UpMoneyCompletedEventHandler UpMoneyCompleted;
        
        /// <remarks/>
        public event SuperCheckCompletedEventHandler SuperCheckCompleted;
        
        /// <remarks/>
        public event EnsureCheckCompletedEventHandler EnsureCheckCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/Login", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Login(string CustomerId, string password) {
            object[] results = this.Invoke("Login", new object[] {
                        CustomerId,
                        password});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string CustomerId, string password) {
            this.LoginAsync(CustomerId, password, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string CustomerId, string password, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        CustomerId,
                        password}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/GetDocList", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Document[] GetDocList(string shoperId) {
            object[] results = this.Invoke("GetDocList", new object[] {
                        shoperId});
            return ((Document[])(results[0]));
        }
        
        /// <remarks/>
        public void GetDocListAsync(string shoperId) {
            this.GetDocListAsync(shoperId, null);
        }
        
        /// <remarks/>
        public void GetDocListAsync(string shoperId, object userState) {
            if ((this.GetDocListOperationCompleted == null)) {
                this.GetDocListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDocListOperationCompleted);
            }
            this.InvokeAsync("GetDocList", new object[] {
                        shoperId}, this.GetDocListOperationCompleted, userState);
        }
        
        private void OnGetDocListOperationCompleted(object arg) {
            if ((this.GetDocListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDocListCompleted(this, new GetDocListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/EnsureDownload", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EnsureDownload(int ID, string state) {
            object[] results = this.Invoke("EnsureDownload", new object[] {
                        ID,
                        state});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EnsureDownloadAsync(int ID, string state) {
            this.EnsureDownloadAsync(ID, state, null);
        }
        
        /// <remarks/>
        public void EnsureDownloadAsync(int ID, string state, object userState) {
            if ((this.EnsureDownloadOperationCompleted == null)) {
                this.EnsureDownloadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnsureDownloadOperationCompleted);
            }
            this.InvokeAsync("EnsureDownload", new object[] {
                        ID,
                        state}, this.EnsureDownloadOperationCompleted, userState);
        }
        
        private void OnEnsureDownloadOperationCompleted(object arg) {
            if ((this.EnsureDownloadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnsureDownloadCompleted(this, new EnsureDownloadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/UpTime", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpTime(int ID, string time) {
            object[] results = this.Invoke("UpTime", new object[] {
                        ID,
                        time});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpTimeAsync(int ID, string time) {
            this.UpTimeAsync(ID, time, null);
        }
        
        /// <remarks/>
        public void UpTimeAsync(int ID, string time, object userState) {
            if ((this.UpTimeOperationCompleted == null)) {
                this.UpTimeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpTimeOperationCompleted);
            }
            this.InvokeAsync("UpTime", new object[] {
                        ID,
                        time}, this.UpTimeOperationCompleted, userState);
        }
        
        private void OnUpTimeOperationCompleted(object arg) {
            if ((this.UpTimeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpTimeCompleted(this, new UpTimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/UpMoney", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpMoney(int ID, string money) {
            object[] results = this.Invoke("UpMoney", new object[] {
                        ID,
                        money});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpMoneyAsync(int ID, string money) {
            this.UpMoneyAsync(ID, money, null);
        }
        
        /// <remarks/>
        public void UpMoneyAsync(int ID, string money, object userState) {
            if ((this.UpMoneyOperationCompleted == null)) {
                this.UpMoneyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpMoneyOperationCompleted);
            }
            this.InvokeAsync("UpMoney", new object[] {
                        ID,
                        money}, this.UpMoneyOperationCompleted, userState);
        }
        
        private void OnUpMoneyOperationCompleted(object arg) {
            if ((this.UpMoneyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpMoneyCompleted(this, new UpMoneyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/SuperCheck", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Document[] SuperCheck(string check) {
            object[] results = this.Invoke("SuperCheck", new object[] {
                        check});
            return ((Document[])(results[0]));
        }
        
        /// <remarks/>
        public void SuperCheckAsync(string check) {
            this.SuperCheckAsync(check, null);
        }
        
        /// <remarks/>
        public void SuperCheckAsync(string check, object userState) {
            if ((this.SuperCheckOperationCompleted == null)) {
                this.SuperCheckOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSuperCheckOperationCompleted);
            }
            this.InvokeAsync("SuperCheck", new object[] {
                        check}, this.SuperCheckOperationCompleted, userState);
        }
        
        private void OnSuperCheckOperationCompleted(object arg) {
            if ((this.SuperCheckCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SuperCheckCompleted(this, new SuperCheckCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://202.114.18.229:2000/EnsureCheck", RequestNamespace="http://202.114.18.229:2000", ResponseNamespace="http://202.114.18.229:2000", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool EnsureCheck(int ID, string check) {
            object[] results = this.Invoke("EnsureCheck", new object[] {
                        ID,
                        check});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void EnsureCheckAsync(int ID, string check) {
            this.EnsureCheckAsync(ID, check, null);
        }
        
        /// <remarks/>
        public void EnsureCheckAsync(int ID, string check, object userState) {
            if ((this.EnsureCheckOperationCompleted == null)) {
                this.EnsureCheckOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnsureCheckOperationCompleted);
            }
            this.InvokeAsync("EnsureCheck", new object[] {
                        ID,
                        check}, this.EnsureCheckOperationCompleted, userState);
        }
        
        private void OnEnsureCheckOperationCompleted(object arg) {
            if ((this.EnsureCheckCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnsureCheckCompleted(this, new EnsureCheckCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://202.114.18.229:2000")]
    public partial class Document {
        
        private long idField;
        
        private string customerIdField;
        
        private long addressIdField;
        
        private string shoperIdField;
        
        private string documentNameField;
        
        private string sendWayField;
        
        private string stateField;
        
        private string addressField;
        
        private string paperTypeField;
        
        private string copiesField;
        
        private string printModeField;
        
        private string remarkField;
        
        private string costField;
        
        private System.DateTime updateTimeField;
        
        private System.DateTime sureTimeField;
        
        private string documentUrlField;
        
        private string nameField;
        
        private string shopNameField;
        
        private string phoneField;
        
        private string checkDateField;
        
        /// <remarks/>
        public long ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string CustomerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
            }
        }
        
        /// <remarks/>
        public long AddressId {
            get {
                return this.addressIdField;
            }
            set {
                this.addressIdField = value;
            }
        }
        
        /// <remarks/>
        public string ShoperId {
            get {
                return this.shoperIdField;
            }
            set {
                this.shoperIdField = value;
            }
        }
        
        /// <remarks/>
        public string DocumentName {
            get {
                return this.documentNameField;
            }
            set {
                this.documentNameField = value;
            }
        }
        
        /// <remarks/>
        public string sendWay {
            get {
                return this.sendWayField;
            }
            set {
                this.sendWayField = value;
            }
        }
        
        /// <remarks/>
        public string State {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }
        
        /// <remarks/>
        public string address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        public string PaperType {
            get {
                return this.paperTypeField;
            }
            set {
                this.paperTypeField = value;
            }
        }
        
        /// <remarks/>
        public string Copies {
            get {
                return this.copiesField;
            }
            set {
                this.copiesField = value;
            }
        }
        
        /// <remarks/>
        public string PrintMode {
            get {
                return this.printModeField;
            }
            set {
                this.printModeField = value;
            }
        }
        
        /// <remarks/>
        public string Remark {
            get {
                return this.remarkField;
            }
            set {
                this.remarkField = value;
            }
        }
        
        /// <remarks/>
        public string Cost {
            get {
                return this.costField;
            }
            set {
                this.costField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime UpdateTime {
            get {
                return this.updateTimeField;
            }
            set {
                this.updateTimeField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime SureTime {
            get {
                return this.sureTimeField;
            }
            set {
                this.sureTimeField = value;
            }
        }
        
        /// <remarks/>
        public string DocumentUrl {
            get {
                return this.documentUrlField;
            }
            set {
                this.documentUrlField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string ShopName {
            get {
                return this.shopNameField;
            }
            set {
                this.shopNameField = value;
            }
        }
        
        /// <remarks/>
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        public string CheckDate {
            get {
                return this.checkDateField;
            }
            set {
                this.checkDateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void GetDocListCompletedEventHandler(object sender, GetDocListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDocListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDocListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Document[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Document[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void EnsureDownloadCompletedEventHandler(object sender, EnsureDownloadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnsureDownloadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnsureDownloadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UpTimeCompletedEventHandler(object sender, UpTimeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpTimeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpTimeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void UpMoneyCompletedEventHandler(object sender, UpMoneyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpMoneyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpMoneyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SuperCheckCompletedEventHandler(object sender, SuperCheckCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SuperCheckCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SuperCheckCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Document[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Document[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void EnsureCheckCompletedEventHandler(object sender, EnsureCheckCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnsureCheckCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnsureCheckCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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