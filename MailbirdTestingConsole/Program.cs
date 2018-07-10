using Mailbird.Common;
using MailBird.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MailbirdTestingConsole
{
    class Program
    {
        static List<long> _allUIds = new List<long>();
        static int _batchStartIndex = 0;
        static int _batchLength = 10;
        static void Main(string[] args)
        {



            //MailInteractionManager proxy = new MailInteractionManager("imap.gmail.com", EncryptionType.UNENCRYPTED);
            CustomClient client = null;
            if (MailConnectionManager.TryGetClient(out client))
            {
                var proxy = new MailProxy("imap.gmail.com", EncryptionType.UNENCRYPTED, ref client);

                if (!client.Connected) { proxy.Login("ramankingdom", "letusc"); }
                var folders = proxy.GetEmailFoldersAsync().GetAwaiter().GetResult();
                proxy.SetWorkingEmailFolder("Inbox");
                _allUIds = proxy.PopulateEmailHeadersIDAsync().GetAwaiter().GetResult();
                client.FreeClient();
            }

            Timer timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            timer.Enabled = true;

            Console.ReadLine();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var processUIDs = _allUIds.GetRange(_batchStartIndex, _batchLength);
            Console.WriteLine("Timer Processing", processUIDs);



            Parallel.ForEach(processUIDs, async uid => {
                CustomClient client = null;
                if (MailConnectionManager.TryGetClient(out client))
                {
                    Console.WriteLine(client.Id);
                    var proxy = new MailProxy("imap.gmail.com", EncryptionType.UNENCRYPTED, ref client);
                    if (!client.Connected) { proxy.Login("ramankingdom", "letusc"); }
                    proxy.SetWorkingEmailFolder("Inbox");
                    var header = await proxy.GetEmailHeaderAsync(uid);
                    Console.WriteLine(ApplicationHelpers.ConvertBytesToString(header));
                    client.FreeClient();
                }
            }
            );

        }
    }
}