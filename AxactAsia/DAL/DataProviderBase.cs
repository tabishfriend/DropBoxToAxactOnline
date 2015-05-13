using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEdmx;

namespace DAL
{
    public abstract class DataProviderBase
    {
        #region Private variables
        public DropBoxEntities DataContext;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public DataProviderBase()
        {
            DataContext = DataManager.GetContext();
        }

        /// <summary>
        /// Save changes in to DB
        /// </summary>
        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

    }
}
