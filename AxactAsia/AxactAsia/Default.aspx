<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AxactAsia._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function OpenPage(strval) {
            window.open(strval);
            decrementCounter();
            return false;
        }

        var timeLeft = <%= Counter %>;
        function decrementCounter() {
            $("#WaitDiv").show();
            if (timeLeft > 0) {
                document.getElementById('countDown').innerHTML = "Redirecting in " + timeLeft + "...";
                timeLeft--;
                setTimeout("decrementCounter()", 1000);
            }
            else {
                window.location = "DropBoxDocuments.aspx";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    <h2>Welcome to Dropbox to Axact online linkage application
    </h2>
    <p>
        Click here to show documents from DropBox
        <asp:LinkButton ID="lbDropBox" runat="server">Login To Drop box</asp:LinkButton>
        (you need to login only once then it will be auto login)
    </p>

    <h1>
        <div id="WaitDiv" style="display:none">
            <div id="countDown"></div>
            <a href="DropBoxDocuments.aspx">Process </a>
        </div>
    </h1>

</asp:Content>




