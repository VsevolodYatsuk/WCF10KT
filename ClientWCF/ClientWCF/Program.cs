using System;
using System.ServiceModel;

namespace SecureWCFClient
{
    [ServiceContract]
    public interface ISecureService
    {
        [OperationContract]
        string GetSecureMessage();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var binding = new WSHttpBinding(SecurityMode.Message);
            var endpoint = new EndpointAddress("http://localhost:8080/SecureService");

            ChannelFactory<ISecureService> factory = new ChannelFactory<ISecureService>(binding, endpoint);
            ISecureService client = factory.CreateChannel();

            try
            {
                string message = client.GetSecureMessage();
                Console.WriteLine("Secure Message: " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                ((IClientChannel)client).Close();
                factory.Close();
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}