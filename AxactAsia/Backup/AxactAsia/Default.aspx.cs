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
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var m = new Manager();
            if (!m.IsRecordExistsInDB)
            {
                Response.Redirect(m.DoAuthentication());
            }

            Response.Redirect("AccountInfo.aspx");
        }
    }
}
