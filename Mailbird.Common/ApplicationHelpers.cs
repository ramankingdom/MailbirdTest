using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailbird.Common
{
    public static class ApplicationHelpers
    {
        /// <summary>
        /// Converts bytes array to string in UTF encoding. USe for bigger bytes array
        /// </summary>
        /// <param name="byteArray">Array of bytes to convert</param>
        /// <returns>string</returns>
        public static async Task<string> ConvertBytesToStringAsync(byte[] byteArray)
        {
           var result =  await Task.Factory.StartNew(() => Encoding.UTF8.GetString(byteArray));
            return result;
            
        }
        /// <summary>
        /// Converts bytes of array to string.Use for small lenght array instead use async version
        /// </summary>
        /// <param name="byteArray">Array of bytes to convert</param>
        /// <returns>string</returns>
        public static string ConvertBytesToString(byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}
