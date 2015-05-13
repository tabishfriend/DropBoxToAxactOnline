using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelEdmx;

namespace DAL
{
    public class MappingDataProvider : DataProviderBase
    {
        #region Private variables
        List<AxactToDropBoxMapping> Documents;
        Guid UserId;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="userId"></param>
        public MappingDataProvider(List<AxactToDropBoxMapping> documents, Guid userId)
        {
            Documents = documents;
            UserId = userId;
        }

        /// <summary>
        /// Insert document(s)
        /// </summary>
        public void Insert()
        {
            var StoredDocuments = ReadDocuments();

            foreach (var Document in Documents)
            {
                if (IsNew(StoredDocuments, Document))
                {
                    DataContext.AxactToDropBoxMappings.AddObject(Document);
                }
            }
            SaveChanges();
        }

        /// <summary>
        /// Check document is New or existing one
        /// </summary>
        /// <param name="StoredDocuments"></param>
        /// <param name="Record"></param>
        /// <returns></returns>
        bool IsNew(List<AxactToDropBoxMapping> StoredDocuments, AxactToDropBoxMapping Record)
        {
            return StoredDocuments.Where(i => i.DropBoxRev == Record.DropBoxRev && i.DropBoxPath == Record.DropBoxPath).Count() > 0 ? false : true;
        }

        /// <summary>
        /// Read all documents for a User
        /// </summary>
        /// <returns></returns>
        public List<AxactToDropBoxMapping> ReadDocuments()
        {
            return (from D in DataContext.AxactToDropBoxMappings where D.UserID == UserId select D).ToList();
        }
    }
}
