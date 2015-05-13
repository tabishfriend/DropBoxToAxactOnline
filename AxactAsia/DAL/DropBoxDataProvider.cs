using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEdmx;

namespace DAL
{
    public class DropBoxDataProvider:DataProviderBase
    {
        #region Private varaibels
        DropBoxUser User;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj"></param>
        public DropBoxDataProvider(DropBoxUser obj)
        {
            User = obj;
        }

        /// <summary>
        /// Insert user
        /// </summary>
        public void Insert()
        {
            DataContext.DropBoxUsers.AddObject(User);
            SaveChanges();
        }

        /// <summary>
        /// Insert if not exists other wise Update
        /// </summary>
        public void InsertUpdate()
        {
            var objUser = ReadUser();
            if (objUser == null)
            {
                Insert();
            }
            else
            {
                UpdateUser();
            }
        }

        /// <summary>
        /// Read User 
        /// </summary>
        /// <returns></returns>
        public DropBoxUser ReadUser()
        {
            var obj = (from D in DataContext.DropBoxUsers where D.UserID==User.UserID select D).FirstOrDefault(); 
            return obj;
        }

        /// <summary>
        /// Update user
        /// </summary>
        public void UpdateUser()
        {
            var obj = (from D in DataContext.DropBoxUsers where D.UserID == User.UserID select D).FirstOrDefault();
            if (obj != null)
            {
                obj.IsAccessToken = User.IsAccessToken;
                obj.Secret = User.Secret;
                obj.Token = User.Token;
            }
            SaveChanges();
        }
    }
}
