<%@ Page Title="Configurator Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Elinic.Help" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .btn.text-white {
            color: #fff !important;
        }
        .bg-light {
            background-color:#f8f9fa;
        }
        .rounded {
            border-radius: 2rem;
        }
        .p-5 {
            padding:3rem !important;
        }
    </style>
    <hgroup class="title">
        <div class="container">
            <h1><%: Title %></h1>
            <a id="btnHome2" class="btn btn-primary text-white" href="/" style="float: right; width: 110px;">
                <i class="fa fa-home"></i>&nbsp;Home
            </a>
        </div>
        <hr />

    </hgroup>
    <div class="container p-5 border rounded bg-light">
        <p class="px-5 pt-3" runat="server" ID="helpText">
        </p>
        <div class="text-center">
            <a href="Project.aspx" class="btn btn-primary text-white">Start Configuring</a>
        </div>
    </div>
</asp:Content>