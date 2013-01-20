<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="BlackJackWeb.UserPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="row"><br /></div>
<div class="row">
<div class="span2 pagination-centered">
Username:
</div>
<div class="span6 pagination-centered">
<asp:TextBox runat="server" ID="txt_username"></asp:TextBox>
</div>
</div>
<div class="row">
<div class="span2 pagination-centered">
Money:
</div>
<div class="span6 pagination-centered">
<asp:TextBox runat="server" ID="txt_money"></asp:TextBox>
</div>
</div>
<div class="row">
<div class="span2 pagination-centered">
Number Of Games:
</div>
<div class="span6 pagination-centered">
<asp:TextBox runat="server" ID="txt_num_of_games"></asp:TextBox>
</div>
</div>
<div class="row">
<div class="span2 pagination-centered">
<asp:label runat="server" ID="lbl_Admin">Admin:</asp:label>
</div>
<div class="span6 pagination-centered">
<asp:CheckBox runat="server" ID="chk_admin"></asp:CheckBox>
</div>
</div>
<div class="row">
<asp:Button ID="submit" runat="server" Text="Update" onclick="submit_Click"  /></div>
</asp:Content>
