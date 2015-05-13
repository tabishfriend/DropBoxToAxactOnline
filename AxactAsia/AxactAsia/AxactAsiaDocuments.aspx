<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AxactAsiaDocuments.aspx.cs" Inherits="AxactAsia.AxactAsiaDocuments" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:GridView ID="grdMapping" runat="server"></asp:GridView>
    </div>

</asp:Content>
