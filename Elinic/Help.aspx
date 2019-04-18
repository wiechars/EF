<%@ Page Title="Configurator Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Elinic.Help" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <div class="container">
            <h1><%: Title %></h1>
            <a id="btnHome2" class="btn btn-primary text-white" href="/" style="float: right; width: 110px;">
                <i class="fa fa-home"></i>&nbsp;Home
            </a>
        </div>
        <hr />

    </hgroup>
    <div class="card container">
      <div class="card-body">
        <p class="px-5 pt-3" runat="server" ID="helpText">
        </p>
        <a href="Project.aspx" class="btn btn-primary text-light">Start Configuring</a>
      </div>
    </div>
</asp:Content>