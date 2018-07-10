using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailbird.Common.ModelClasses
{
    public class MailBirdEmail
    {
        public long UId { get; set; }
        public bool isHeaderDownloaded { get; set; }
        public bool isBodyDownloaded { get; set; }
        public byte[] HeaderInfo { get; set; }

        public byte[] BodyInfo { get; set; }
        
    }
}
