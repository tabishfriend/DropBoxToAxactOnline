using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DropBoxApi;
using AxactAsia.BaseClass;

namespace AxactAsia
{
    public partial class AccountInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var O = new AxactManager();
            O.test1(Request.QueryString["code"]);

            //var m = new Manager();
            //m.UpdateAccessToken();
            //LoadData();
        }


        void LoadData()
        { 
            var obj = new DropBoxDataManager();
          AllFilesFromDropBox.DataSource=  obj.GetAllFiles(string.Empty);
          AllFilesFromDropBox.DataBind();

          
        }
    }
}