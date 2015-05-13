using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Net;
using System.IO;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Delegates;
using Utility;
using BLL;
using DataModelEdmx;

namespace DropBoxApi
{
    public class AxactManager
    {
        #region Private variables

        static string AccessToken;
        ExactOnlineClient client;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objToken"></param>
        public AxactManager(string objToken)
        {
            AccessToken = objToken;
        }

        /// <summary>
        /// Insert all files
        /// </summary>
        public void InsertAllFiles()
        {

            var UserID = Common.GetCurrentLoginUserID();
            var Documents = new List<AxactToDropBoxMapping>();

            InitConnection();
            var DropBox = new DropBoxDataManager();
            foreach (var F in DropBox.GetAllFiles(Common.GetValueFromAppSetting(Constants.DropBoxDirectoryPath)))
            {
                //Skip directory or folder
                if (F.IsDirectory)
                    continue;

                //Creating new document object
                Document document = GetNewDocumentObject(F.Path);

                //Create AxactOnline document 
                bool created = client.For<Document>().Insert(ref document);

                //if document created successfully then downlaod document
                //and attach with the newly creted document
                #region Download document from DropBox and Upload to Axactonline
                if (created)
                {
                    //Download file from DropBox
                    var DownlaodedFile = DropBox.DownlaodFile(F.Path);

                    //Create new attachment object
                    DocumentAttachment Attachement = new DocumentAttachment
                    {
                        Document = document.ID,
                        FileName = Common.GetFileNameFromPath(F.Path),
                        Attachment = DownlaodedFile.Data,
                        FileSize = DownlaodedFile.Data.Length
                    };

                    //Save in to Axact onlie
                    created = client.For<DocumentAttachment>().Insert(ref Attachement);
                }
                #endregion


                ///Create mapping record
                #region Create mapping record Dropbox to Axact online
                if (created)
                {
                    var obj = new AxactToDropBoxMapping();
                    obj.ID = Guid.NewGuid();
                    obj.UserID = UserID;
                    obj.AxactDocumentID = document.ID;
                    obj.DropBoxRev = F.Revision;
                    obj.DropBoxPath = F.Path;
                    Documents.Add(obj);
                }
                #endregion
            }


            var objManager = new MappingUserManager(Documents, UserID);
            objManager.Insert();

        }


        /// <summary>
        /// Creating new document object
        /// </summary>
        /// <returns></returns>
        Document GetNewDocumentObject(string Path)
        {
            Document document = new Document
            {
                Subject = string.Format(Common.GetValueFromAppSetting(Constants.AxactDocumentSubject), Common.GetFileNameFromPath(Path)),
                Body = Common.GetValueFromAppSetting(Constants.AxactDocumentBody),
                Category = Common.StringToGuid(Common.GetValueFromAppSetting(Constants.AxactDocumentCategory)),
                Type = Common.StringToInt(Common.GetValueFromAppSetting(Constants.AxactDocumentType)),
                DocumentDate = DateTime.Now.Date,
            };

            return document;
        }

        

        /// <summary>
        /// Get All mapping documents
        /// </summary>
        /// <returns></returns>
        public List<AxactToDropBoxMapping> GetAllMappingDocuments()
        {

            var UserID = Common.GetCurrentLoginUserID();
            var objManager = new MappingUserManager(null, UserID);
            return objManager.ReadDocuments();

        }


        #region Connection
        /// <summary>
        /// Initiliaze connection to Axact online
        /// </summary>
        void InitConnection()
        {
            string apiEndPoint = Common.GetValueFromAppSetting(Constants.AxactBaseUri);
            AccessTokenManagerDelegate AccessTokenDelegate = new AccessTokenManagerDelegate(Notify);
            client = new ExactOnlineClient(apiEndPoint, AccessTokenDelegate);

        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        static string Notify()
        {
            return AccessToken;
        }
        #endregion
    }
}
