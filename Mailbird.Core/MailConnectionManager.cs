using Limilabs.Client.IMAP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBird.Core
{
    public static class MailConnectionManager
    {
        static readonly int MaxConnections = 5;
        static readonly object _lockObject = new object();
        static readonly List<CustomClient> _clients;
        static MailConnectionManager()
        {
            _clients = new List<CustomClient>();
        }

        private static CustomClient GetClient()
        {
            CustomClient client = null;
            if (_clients.Count() >= 0)
            {
                client = _clients.Where(x => !x.IsBusy).FirstOrDefault();
                if (client == null && _clients.Count < MaxConnections)
                {
                    client = new CustomClient(Guid.NewGuid());
                    _clients.Add(client);
                }
             }
            return client;
        }



        public static bool TryGetClient(out CustomClient client)
        {
            bool isSuccess = false;
            try
            {
                lock (_lockObject)
                {
                    client = GetClient();
                    if (client != null) //doubleCheck
                    {
                        client.IsBusy = true;
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                return isSuccess;
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            client = null;
            return isSuccess;
        }

    }


    public class CustomClient : Imap
    {

        public Guid Id { get; private set; }
        public volatile bool IsBusy;
        public CustomClient(Guid id)
        {
            Id = id;
        }
        
        public void FreeClient()
        {
            IsBusy = false;
        }
    }
}
