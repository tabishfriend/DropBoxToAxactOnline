<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="DropBoxDocuments.aspx.cs" Inherits="AxactAsia.DropBoxDocuments" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:GridView ID="AllFilesFromDropBox" runat="server"></asp:GridView>
    </div>

</asp:Content>
