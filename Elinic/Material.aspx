<%@ Page Title="Materials and Hardware" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="Elinic.Material" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <hgroup class="title">
      <h1><%: Title %></h1>
      <a id="btnHome2" class="btn btn-primary" href="/" style="float: right; width: 110px;">
      <i class="fa fa-home"></i>&nbsp;Home
      </a>
      <hr />
   </hgroup>

   <div class="container">
      <div class="rounded shadow p-5 bg-light">
          <a class="btn btn-primary text-white" runat="server" ID="backToProject"><i class="fa fa-arrow-left mr-3"></i>Back to Layout</a>
         <div class="m-5">
            <div>
               <div id="selectedMaterial" class="" runat="server">
                  <div class="bg-white p-5 rounded">
                     <h2 class="text-center border-bottom mb-3 pb-3 text-primary mt-0">Wood and Finish</h2>
                     <div class="row">
                        <div class="col-md-6 col-12 align-top d-flex justify-content-center align-items-center">
                           <img id="imgMaterial" class="img-fluid" style="max-height:300px"
                              src="" runat="server" />
                        </div>
                        <div class="col-md-6 col-12 mt-3">
                            <div class="ml-3 p-5 rounded bg-light">
                               <div class="text-center input-group align-top my-3">
                                  <asp:DropDownList ID="compMaterial" runat="server" class="form-control">
                                  </asp:DropDownList>
                               </div>
                               <div class="text-center input-group my-3">
                                  <asp:DropDownList ID="compFinish" runat="server" class="form-control"></asp:DropDownList>
                               </div>
                               <div class="text-center input-group my-3">
                                  <asp:DropDownList ID="compStain" runat="server" class="form-control" Style="width: 100%;" runat="server"></asp:DropDownList>
                               </div>
                            </div>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
            <div class="bg-white p-5 rounded mt-3 d-block">
               <h2 class="text-center border-bottom mb-3 pb-3 text-primary mt-0">Materials and Hardware</h2>
               <div class="row">
                  <div class="col-md-6 col-12 d-flex">
                     <img id="imgHandle" class="img-fluid"
                        src="" style="max-height:300px" runat="server" />
                  </div>
                  <div class="col-md-6 col-12">
                     <div class="text-center p-5 rounded bg-light ml-3 input-group align-top my-3">
                        <asp:DropDownList ID="compHandle" runat="server" class="form-control"
                           >
                        </asp:DropDownList>
                     </div>
                  </div>
               </div>
            </div>
             <div class="text-center mt-3 text-white">
          <button type="submit" class="btn btn-primary btn-lg" >
              <i class="fa fa-save mr-3"></i> Save Materials
          </button>
             </div>
         </div>
      </div>
           </form>
   </div>
    <script>
        $(window).ready(function ($) {
            $("#MainContent_compMaterial").change(function () {
                var materialImage = JSON.parse($(this).find("option:selected")[0].value).ImagePath;
                $("#MainContent_imgMaterial").attr('src', '/Images/' + materialImage);
                
            })
            $("#MainContent_compHandle").change(function () {
                var handle  = $(this).find("option:selected")[0].value;
                $("#MainContent_imgHandle").attr('src', '/Images/' + handle);
            })
        })
    </script>
</asp:Content>