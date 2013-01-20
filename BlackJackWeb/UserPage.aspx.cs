using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJackWeb.BJServiceRef;

namespace BlackJackWeb
{
    public partial class UserPage : System.Web.UI.Page
    {
        BJServiceClient service = new BJServiceClient();
        static UserWcf currentUser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Server.Transfer("~/Default.aspx");
            currentUser = (UserWcf)Session["user"];
            populateFields();
        }

        private void populateFields()
        {
            txt_username.Attributes["disabled"]= "true";
            if (!currentUser.isAdmin)
            {
                lbl_Admin.Visible = false;
                chk_admin.Visible = false;
                txt_money.Attributes["disabled"]= "true";
                txt_num_of_games.Attributes["disabled"]= "true";
                submit.Visible = false;
                
            }
            else
            {
                lbl_Admin.Visible = true;
                chk_admin.Visible = true;
                submit.Visible = true;
                txt_money.Attributes.Remove("disabled");
                txt_num_of_games.Attributes.Remove("disabled");

            }
            txt_money.Text = currentUser.money.ToString();
            txt_username.Text = currentUser.Username;
            txt_num_of_games.Text = currentUser.numOfGames.ToString();
            chk_admin.Checked = currentUser.isAdmin;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
                  
        
        }
    }
}