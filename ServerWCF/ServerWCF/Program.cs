using System;
using System.ServiceModel;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.Threading;
using ServerWCF;

namespace SecureWCFService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!IsUserAdministrator())
            {
                Console.WriteLine("Please run the application as an administrator.");
                Console.ReadLine();
                return;
            }

            string address = "http://localhost:8080/SecureService";
            Uri[] baseAddresses = { new Uri(address) };

            SecureService service = new SecureService();
            ServiceHost host = new ServiceHost(service, baseAddresses);

            var binding = new WSHttpBinding(SecurityMode.Message);

            host.AddServiceEndpoint(typeof(ISecureService), binding, "");

            host.Opened += (sender, e) => Console.WriteLine("Service is running...");
            try
            {
                host.Open();
                Console.WriteLine("Press Enter to stop the service...");
                Console.ReadLine();
            }
            catch (AddressAccessDeniedException)
            {
                Console.WriteLine("Access denied. Please run the application as an administrator.");
                Console.ReadLine();
            }
            finally
            {
                if (host.State == CommunicationState.Opened)
                {
                    host.Close();
                }
            }
        }

        private static bool IsUserAdministrator()
        {
            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}