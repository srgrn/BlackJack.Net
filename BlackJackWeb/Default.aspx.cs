using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJackWeb.BJServiceRef;

namespace BlackJackWeb
{
    public partial class Default : System.Web.UI.Page
    {
        BJServiceClient service = new BJServiceClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && txt_password.Text != string.Empty)
            {
                txt_password.Attributes["value"] = txt_password.Text;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (service.getUser(txt_username.Text) == null){
                lbl_warning.Text = "Warning no such user";
                }
            else
            {
                UserWcf user = service.login(txt_username.Text, txt_password.Attributes["value"]);
                if (user != null)
                {
                    Session["user"] = user;
                    Response.Redirect("~/UserPage.aspx");
                }

            }
        }
    }
}