﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5472
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolPrint.DALService {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DALService.DALWebServiceSoap")]
    public interface DALWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        bool Login(string customerId, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetFileList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet GetFileList(string customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EnsureDownload", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        bool EnsureDownload(string documentId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface DALWebServiceSoapChannel : SchoolPrint.DALService.DALWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class DALWebServiceSoapClient : System.ServiceModel.ClientBase<SchoolPrint.DALService.DALWebServiceSoap>, SchoolPrint.DALService.DALWebServiceSoap {
        
        public DALWebServiceSoapClient() {
        }
        
        public DALWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DALWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DALWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DALWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Login(string customerId, string password) {
            return base.Channel.Login(customerId, password);
        }
        
        public System.Data.DataSet GetFileList(string customerId) {
            return base.Channel.GetFileList(customerId);
        }
        
        public bool EnsureDownload(string documentId) {
            return base.Channel.EnsureDownload(documentId);
        }
    }
}