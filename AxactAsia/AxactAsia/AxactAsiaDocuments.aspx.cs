using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AxactAsia.BaseClass;
using DropBoxApi;
using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.Delegates;
using Example.Service;

namespace AxactAsia
{
    public partial class AxactAsiaDocuments : BasePage
    {
        private static readonly ExactOnlineOAuthClient OAuthClient = new ExactOnlineOAuthClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            MakeSessionPersistent();

            if (AuthorizeClient())
            {
                InitializeGrid();
            }
        }

        private void MakeSessionPersistent()
        {
            if (Session.Keys.Count == 0)
            {
                // Initialize a session object so the session remains persistent. 
                // Otherwise DotNetOpenAuth will throw a Protocolexception on ProcessUserAuthorization.
                // http://stackoverflow.com/questions/2874078/asp-net-session-sessionid-changes-between-requests
                Session["Init"] = 0;
            }
        }

        Boolean AuthorizeClient()
        {
            OAuthClient.Authorize(Session, "AxactAsiaDocuments.aspx");
            return (OAuthClient.Authorization != null);
        }

        void InitializeGrid()
        {
            var o = new AxactManager(OAuthClient.Authorization.AccessToken);
            o.InsertAllFiles();

            grdMapping.DataSource = o.GetAllMappingDocuments();
            grdMapping.DataBind();
        }


    }
}