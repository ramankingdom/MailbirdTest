using Limilabs.Client.IMAP;
using Mailbird.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBird.Core
{
    public class MailProxy
    {
        private EncryptionType _encryptioType = EncryptionType.UNENCRYPTED;
        private string _host;
        private  CustomClient _imapClient;

        public MailProxy(string host, EncryptionType encryption,ref CustomClient client)
        {
            _host = host;
            _encryptioType = encryption;
            _imapClient = client;
        }

        public bool Login(string userName, string password)
        {
            try
            {
                _imapClient.ConnectSSL(_host);
                _imapClient.Login(userName, password);
            }
            catch (Exception)
            {

                throw;
            }
            return (_imapClient != null && _imapClient.Connected);
        }

        public  async Task<List<long>> PopulateEmailHeadersIDAsync()
        {
            List<long> allUids = null;

            try
            {
                allUids = await Task.Factory.StartNew(() => _imapClient.GetAll());
                
            }
            catch (Exception)
            {

                throw;
            }
            return allUids;
        }


        public async Task<List<FolderInfo>> GetEmailFoldersAsync()
        {
            List<FolderInfo> folderInfo;
            try
            {
                folderInfo = await Task.Run(() => _imapClient.GetFolders(SubFolders.All));
            }
            catch (Exception)
            {
                throw;
            }
            return folderInfo;

        }

        public async Task<BodyStructure> GetEmailBodyStructureAsync(long uid)
        {
            BodyStructure bodyStructure = null;

            try
            {
                bodyStructure = await Task.Run(() => _imapClient.GetBodyStructureByUID(uid));
            }
            catch (Exception)
            {

                throw;
            }
            return bodyStructure;
        }
        public async Task<byte[]> GetEmailHeaderAsync(long uid)
        {
            byte[] header = null;

            try
            {
                header = await Task.Run(() => _imapClient.GetHeadersByUID(uid));

            }
            catch (Exception)
            {
                throw;
            }
            return header;
        }



        public void SetWorkingEmailFolder(string folderName)
        {
            _imapClient.Examine(folderName);
        }

    }
}
