using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJackWeb.BJServiceRef;
using System.ServiceModel;

namespace BlackJackWeb
{
    public partial class Default : System.Web.UI.Page
    {
        private static ServiceClient service = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && txt_password.Text != string.Empty)
            {
                txt_password.Attributes["value"] = txt_password.Text;
            }
            if (!IsPostBack && !IsCallback)
            {
                service = new BJServiceRef.ServiceClient(new InstanceContext(new emptyCallback()), "HttpBinding");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (service.getUser(txt_username.Text) == null)
            {
                lbl_warning.Text = "Warning no such user";
            }
            else
            {
                UserWcf me = service.loginWeb(txt_username.Text, txt_password.Attributes["value"]);
                if (me != null)
                {
                    Session["user"] = me;
                    Response.Redirect("~/UserPage.aspx");
                }
            }
        }
    }
}
