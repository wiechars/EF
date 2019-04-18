<%@ Page Title="Order Complete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderComplete.aspx.cs" Inherits="Elinic.OrderComplete" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <div class="container">
            <h1><%: Title %></h1>
            <br />
        </div>
        <hr />
    </hgroup>
    <div class="" style="margin: 0 auto;">
        <div class="col-xs-12 text-center">
            Thank you for submitting your design request, our designer will contact you shortly with some images and 
                suggestions and price.
            
            <br />
            <br />
            <button id="btnSubmit" class="btn btn-success light" runat="server" onserverclick="btnConfigure_Click">
                <i class="fa fa-check-circle"></i>&nbsp;Configure Another Project
            </button>
            <button id="btnHome2" class="btn btn-primary light" runat="server" onserverclick="btnGoHome_Click">
                <i class="fa fa-home"></i>&nbsp;Home
            </button>
        </div>

    </div>


    <br />

</asp:Content>
