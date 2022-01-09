using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.ServiceReference1;

namespace DM.Infrastructure
{
    public interface IUnitOfWorkClass
    {
        tsmsServiceClient SMSClient { get; }
    }

    public class UnitOfWorkClass : IUnitOfWorkClass
    {
        private tsmsServiceClient _smsClient;
        public UnitOfWorkClass()
        {

        }

        public virtual tsmsServiceClient SMSClient => _smsClient ?? (_smsClient = new tsmsServiceClient());
    }
}
