using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Security;

namespace Utility
{
    public static class Common
    {
        /// <summary>
        /// Read value from the app section of the config file
        /// If key is invalid then it will return EMPTY value
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetValueFromAppSetting(string Key)
        {
            if (ConfigurationManager.AppSettings[Key] != null)
                return ConfigurationManager.AppSettings[Key];

            return string.Empty;
        }

        /// <summary>
        /// Get current login user
        /// </summary>
        /// <returns></returns>
        public static MembershipUser GetCurrentLoginUser()
        {
            return Membership.GetUser();
        }

        /// <summary>
        /// Get current user login id
        /// </summary>
        /// <returns></returns>
        public static Guid GetCurrentLoginUserID()
        {
            var TempGuid = new Guid();
            try
            {
                var Key = Membership.GetUser().ProviderUserKey;
              
                Guid.TryParse(Key.ToString(), out TempGuid);
            }
            catch { }
            return TempGuid;
        }


        /// <summary>
        /// Convert string to Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  static int StringToInt(string obj)
        {
            int temp = 0;
            int.TryParse(obj, out temp);
            return temp;
        }

        /// <summary>
        /// Conver string to GUid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid StringToGuid(string obj)
        {
            Guid temp = Guid.Empty;
            Guid.TryParse(obj, out temp);
            return temp;
        }

        /// <summary>
        /// Read file name from the path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameFromPath(string path)
        {
            string FileName = string.Empty;

            var SplitPath = path.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (SplitPath.Count() > 0)
            {
                FileName = SplitPath[SplitPath.Length - 1];
            }

            return FileName;
        }
    }
}
