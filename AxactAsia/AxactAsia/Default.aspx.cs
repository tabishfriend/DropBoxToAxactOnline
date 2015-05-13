using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DropBoxApi;
using AxactAsia.BaseClass;
using Utility;

namespace AxactAsia
{
    public partial class _Default : BasePage
    {
        public string Counter { get { return Common.GetValueFromAppSetting(Constants.WaitCounter); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
           
        }

        /// <summary>
        /// If user is first time login
        /// then read Key and save into DB
        /// </summary>
        void InitPage()
        {
            bool IsError = false;
            if (Request.QueryString["error"] != null)
            {
                lblError.Text = "Invalid Drop box Account";
                IsError = true;
            }

            var m = new Manager();
            if (!m.IsUserAccessTokenExistsInDB)
            {
                lbDropBox.Attributes.Add("onClick", " return OpenPage('" + m.DoAuthentication() + "');");
               
            }
            else if(!IsError)
            {
                Response.Redirect("DropBoxDocuments.aspx");
            }

        }
    }
}
