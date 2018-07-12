using Mailbird.Common.ModelClasses;
using MailBird.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailbird.Core
{
    public class MailDownloader
    {
        private ConcurrentQueue<MailBirdEmail> _workItems;

        public EventHandler<MailBirdEmail> EmailDownloaded;

        public MailDownloader()
        {
            _workItems = new ConcurrentQueue<MailBirdEmail>();
        }


        public void AddWork(MailBirdEmail email)
        {
            _workItems.Enqueue(email);
        }

        private void DownloadEmail(MailBirdEmail email)
        {
            CustomClient client;
            if (MailConnectionManager.TryGetClient(out client))
            {

                  

                if (this.EmailDownloaded != null)
                {
                    EmailDownloaded(this, email);
                }
            }
        }

        public void Process()
        {
            MailBirdEmail email;
            _workItems.TryDequeue(out email);
            if (email != null && !email.isBodyDownloaded)
            {
                DownloadEmail(email);
            }
        }

    }
}
