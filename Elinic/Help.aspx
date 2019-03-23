<%@ Page Title="Configurator Help" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Elinic.Help" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
        <a id="btnHome2" class="btn btn-primary" href="/" style="float: right; width: 110px;">
            <i class="fa fa-home"></i>&nbsp;Home
        </a>
        <hr />

    </hgroup>
    <div class="card container">
      <div class="card-body">
        <p class="px-5 pt-3" runat="server" ID="helpText">
        </p>
        <a href="Project.aspx" class="btn btn-primary">Start Configuring</a>
      </div>
    </div>
</asp:Content>