<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BlackJackWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Enter your credentials:</h3>
    <div class="row">
        <div class="span2 pagination-centered">
            <asp:Label runat="server" ID="lbl_txtlogin">Username:</asp:Label>
        </div>
        <div class="span8">
            <asp:TextBox runat="server" AutoPostBack="true" ID="txt_username"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="span2 pagination-centered">
            <asp:Label runat="server" ID="lbl_txtPassword">Password:</asp:Label></div>
        <div class="span8">
            <asp:TextBox runat="server" AutoPostBack="true" ID="txt_password" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="row pagination-centered">
    <asp:Button runat="server" ID="Submit" Text="Login" onclick="Submit_Click" /><asp:Label runat="server" ID="lbl_warning"/>
    </div>
</asp:Content>
