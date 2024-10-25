using System.ServiceModel;

[ServiceContract]
public interface ISecureService
{
    [OperationContract]
    string GetSecureMessage();
}