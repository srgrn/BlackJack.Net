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
        static UserWcf selectedUser = null;
        UserWcf[] users = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Server.Transfer("~/Default.aspx");
            currentUser = (UserWcf)Session["user"];
            if (!Page.IsCallback && !Page.IsPostBack)
            {
                selectedUser = currentUser;
                if (currentUser.isAdmin)
                {
                    users = service.getUsers();
                    rbl_users.DataSource = users;
                    rbl_users.DataTextField = "Username";
                    rbl_users.DataValueField = "Username";
                    rbl_users.DataBind();
                    Session["userlist"] = users;
                }
                populateFields();
            }
            if (Page.IsPostBack)
            {
                txt_username.Text = selectedUser.Username;
                selectedUser.money = int.Parse(txt_money.Text);
                selectedUser.numOfGames = int.Parse(txt_num_of_games.Text);
                selectedUser.isAdmin = chk_admin.Checked;
                
            }
            
            
            
        }

        private void populateFields()
        {

            if (!currentUser.isAdmin)
            {
                lbl_Admin.Visible = false;
                lbl_userlist.Visible = false;
                chk_admin.Visible = false;
                txt_money.Attributes["disabled"]= "true";
                txt_num_of_games.Attributes["disabled"]= "true";
                rbl_users.Attributes["disabled"] = "true";
                submit.Visible = false;
                txt_money.Text = currentUser.money.ToString();
                txt_username.Text = currentUser.Username;
                txt_num_of_games.Text = currentUser.numOfGames.ToString();
                chk_admin.Checked = currentUser.isAdmin;
            }
            else
            {
                lbl_Admin.Visible = true;
                lbl_userlist.Visible = true;
                chk_admin.Visible = true;
                submit.Visible = true;
                txt_money.Attributes.Remove("disabled");
                txt_num_of_games.Attributes.Remove("disabled");
                rbl_users.Attributes.Remove("disabled");
                txt_money.Text = selectedUser.money.ToString();
                txt_username.Text = selectedUser.Username;
                txt_num_of_games.Text = selectedUser.numOfGames.ToString();
                chk_admin.Checked = selectedUser.isAdmin;

            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            //basicly there is no real check here i simply send an update request to the webservice
            UserWcf u = new UserWcf();
            u = new UserWcf();
            u.Username = txt_username.Text;
            u.isAdmin = chk_admin.Checked;
            // again not testing of values i can put here a regex but too tired or use tryparse;
            u.money = int.Parse(txt_money.Text);
            u.numOfGames = int.Parse(txt_num_of_games.Text);
            u.ID = selectedUser.ID;
            if (service.updateUser(u))
                notification.Text = "Updated user " + u.Username;
            users = service.getUsers();
            Session["userlist"] = users;
            rbl_users.DataBind();
        }

        protected void rbl_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((RadioButtonList)sender).SelectedValue != selectedUser.Username)
            {
                notification.Text = "";
                if (Session["userlist"] != null)
                {
                    users = (UserWcf[])Session["userlist"];
                    selectedUser = users.Single<UserWcf>(x => x.Username == rbl_users.SelectedValue);
                    populateFields();
                }
            }
        }
    }
}