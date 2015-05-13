using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DropBoxApi;

namespace AxactAsia
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var obj = new AxactManager();
            var URL = obj.test();
           Response.Redirect( URL);

        }
    }
}
