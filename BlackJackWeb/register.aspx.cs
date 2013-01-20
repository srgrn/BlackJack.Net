using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJackWeb.BJServiceRef;

namespace BlackJackWeb
{
    public partial class register : System.Web.UI.Page
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
            UserWcf u = service.getUser(txt_username.Text);
            if (u == null)
            {
                u = new UserWcf();
                u.Username = txt_username.Text;
                service.addUser(u, txt_password.Attributes["value"]);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lbl_warning.Text = "User already exsits";
            }
        }
    }
}