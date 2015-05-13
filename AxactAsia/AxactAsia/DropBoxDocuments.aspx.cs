using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AxactAsia.BaseClass;
using DropBoxApi;
using Utility;

namespace AxactAsia
{
    public partial class DropBoxDocuments : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();

        }


        /// <summary>
        /// Check for the valid access token
        /// if nto exists then redirect to the default page
        /// </summary>
        void InitPage()
        {
            try
            {
                var m = new Manager();
                m.UpdateAccessToken();
                LoadData();
            }
            catch
            {
                Response.Redirect("default.aspx?error=1");
            }
        }


        /// <summary>
        /// Load data from Drop box
        /// </summary>
        void LoadData()
        {
            var obj = new DropBoxDataManager();
            AllFilesFromDropBox.DataSource = obj.GetAllFiles(Common.GetValueFromAppSetting(Constants.DropBoxDirectoryPath));
            AllFilesFromDropBox.DataBind();
        }
    }
}