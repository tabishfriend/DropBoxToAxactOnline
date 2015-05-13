using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DataModelEdmx;

namespace BLL
{
    public class MappingUserManager
    {

        #region Private variables
        MappingDataProvider Data;
        #endregion

        /// <summary>
        /// Construcot
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="userId"></param>
        public MappingUserManager(List<AxactToDropBoxMapping> documents, Guid userId)
        {
            
            Data = new MappingDataProvider(documents, userId);
        }

       /// <summary>
       /// Inser Document(s)
       /// </summary>
       public void Insert()
       {
           Data.Insert();
       }

        /// <summary>
        /// Read All user documents
        /// </summary>
        /// <returns></returns>
        public List<AxactToDropBoxMapping> ReadDocuments()
        {
            return Data.ReadDocuments();
        }
    }
}
