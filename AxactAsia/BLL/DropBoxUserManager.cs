using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEdmx;
using DAL;

namespace BLL
{
    public class DropBoxUserManager
    {
        #region Private variables
        DropBoxUser User;
        DropBoxDataProvider Data;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obj"></param>
        public DropBoxUserManager(DropBoxUser obj)
        {
            User = obj;
            Data = new DropBoxDataProvider(User);
        }

        /// <summary>
        /// Insert new User
        /// </summary>
        public void Insert()
        {
            Data.Insert();
        }

        /// <summary>
        /// Insert new User if new otherwise update
        /// </summary>
        public void InsertUpdate()
        {
            Data.InsertUpdate();
        }

        /// <summary>
        /// Retrieve User by ID
        /// </summary>
        /// <returns></returns>
        public DropBoxUser ReadUser()
        {
            return Data.ReadUser();
        }

        /// <summary>
        /// Update user
        /// </summary>
        public void UpdateUser()
        {
              Data.UpdateUser();
        }

        /// <summary>
        /// Check If user already exists
        /// </summary>
        /// <returns></returns>
        public bool IsUserExists()
        {
            if (Data.ReadUser() != null)
                return true;

            return false;
        }

        /// <summary>
        /// Check If user Access token exists
        /// </summary>
        /// <returns></returns>
        public bool IsUserAccessTokenExists()
        {
            var objUser = Data.ReadUser();
            if (objUser != null && objUser.IsAccessToken)
            {
                return true;
            }

            return false;
        }
    }
}
