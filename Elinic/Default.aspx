<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Elinic._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
<%--    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Welcome to Elinic Furniture, the home of all your custom furniture needs.</h2>
            </hgroup>
            <p>
                Please visit our Project or Component links to being customizing your furniture layout needs.
            </p>
        </div>
    </section>--%>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <h4>Select & Customize By:</h4>

    <div>
        <div class="span3">
            <asp:Button ID="btnComponent" runat="server" Text="Component" PostBackUrl="~/Component.aspx"/>
        </div>
        <div class="span3">
            <asp:Button ID="btnProjects" runat="server" Text="Projects" PostBackUrl="~/Project.aspx" />
        </div>
    </div>




    <%--    <ol class="round">
        <li class="one">
            <h5>Project</h5>
            Navigate through our numerous furniture layouts, to all customize an entire room layout.
            <asp:Button ID="btnProjects" runat="server" Text="Projects" PostBackUrl="~/Project.aspx" />
        </li>
        <li class="two">
            <h5>Components</h5>
            Search through a list of available custom components
            <asp:Button ID="btnComponent" runat="server" Text="Component" PostBackUrl="~/Component.aspx" />
        </li>
    </ol>--%>
</asp:Content>
