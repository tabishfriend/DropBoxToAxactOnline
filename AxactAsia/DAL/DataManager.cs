using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEdmx;

namespace DAL
{

    public static class DataManager
    {
        /// <summary>
        /// Get Database context
        /// </summary>
        /// <returns></returns>
        public static DropBoxEntities GetContext()
        {
            return new DropBoxEntities();
        }
    }
}
