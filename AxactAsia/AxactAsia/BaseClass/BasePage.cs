using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utility;

namespace AxactAsia.BaseClass
{
    public class BasePage:System.Web.UI.Page
    {

        public BasePage()
        {
            this.Page.PreInit += Page_PreInit;

        
        }

        void Page_PreInit(object sender, EventArgs e)
        {

            if (Common.GetCurrentLoginUserID() == Guid.Empty)
                Response.Redirect("Account/Login.aspx");
        }
    }
}