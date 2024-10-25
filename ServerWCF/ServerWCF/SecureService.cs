using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SecureService : ISecureService
    {
        public string GetSecureMessage()
        {
            return "This is a secure message!";
        }
    }
}
