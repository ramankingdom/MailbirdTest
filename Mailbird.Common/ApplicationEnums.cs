using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailbird.Common
{
    public enum ConnectionType
    {
        IMAP,
        POP3
    }
    public enum EncryptionType
    {
        UNENCRYPTED,
        SST_TLS,
        STARTTLS
    }
    
}
