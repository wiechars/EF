<%@ Page Title="Order Complete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderComplete.aspx.cs" Inherits="Elinic.OrderComplete" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
        <br />
             <hr />
    </hgroup>
    <div class="" style="margin: 0 auto;">
        <div class="col-xs-12">
            <p align="center">
                <br /> Thank you for submitting your design request, our designer will contact you shortly with some images and 
                suggestions and price.
            </p>
            <br />
        </div>

    </div>


        <br />
        <div align="center" class="">
            <asp:Button ID="btnConfigure" runat="server" OnClick="btnConfigure_Click" Text="Configure Another Project" />
            <asp:Button ID="btnHome" runat="server" OnClick="btnGoHome_Click" Text="Home" />

        </div>

</asp:Content>
