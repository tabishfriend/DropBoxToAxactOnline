using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Dropbox.Api;

namespace DropBoxApi
{
    public class DropBoxDataManager
    {
        DropboxApi objApi;
        public DropBoxDataManager()
        {
            var objManager = new Manager();
            objApi = objManager.GetAuthorizedObject();
        }


        #region Read Data From DropBox

        /// <summary>
        /// Read all files and folder from specified path
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public List<File> GetAllFiles(string Path)
        {
            var AllFiles = objApi.GetFiles(Constants.DropBoxRootFolder, Path);
            return AllFiles.Contents.ToList();
        }

        /// <summary>
        /// Downlaod Files
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public FileSystemInfo DownlaodFile(string Path)
        {
           return  objApi.DownloadFile(Constants.DropBoxRootFolder, Path);
        }

        #endregion

    }
}
