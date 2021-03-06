﻿<%@ Page Title="Component" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Component.aspx.cs" Inherits="Elinic.Component" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <style type="text/css">
      .modalBackground {
      background-color: Black;
      filter: alpha(opacity=90);
      opacity: 0.8;
      }
      .modalPopup {
      width:80%;
      height:80%;
      background-color: #FFFFFF;
      border-width: 3px;
      border-style: solid;
      border-color: black;
      text-align: center;
      padding-top: 10px;
      padding-left: 5px;
      padding-right: 5px;
      overflow-y: scroll;
      }
   </style>
   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <div id="pageContent" runat="server">
      <hgroup class="title clearfix row mw-100 overflow-hidden">
         <div class="col-xs-offset-1 col-sm-11">
            <div class="">
               <h1><%: Title %></h1>
               <button id="btnHome2" class="btn btn-primary" runat="server" onserverclick="btnHome_Click"
                  style="float: right; width: 110px;">
               <i class="fa fa-home"></i>&nbsp;Home
               </button>
               <a target="_blank" href="/Help.aspx" class="btn  dark" runat="server" style="float: right; width: 110px;">
               <i class="fa fa-info-circle"></i>&nbsp;Help
               </a>
            </div>
         </div>
         <hr class="col-12"/>
         <div class="col-sm-11">
            <div class="container">
               <h4 runat="server" ID="H1" class="mb-3"></h4>
            </div>
         </div>
      </hgroup>
      <h4 runat="server" ID="index" class="col-sm-offset-1 mb-3"></h4>
      <div class="col-xs-offset-1 col-xs-10">
         <asp:Label ID="lblDescription" runat="server" Text="" Style="display: block; text-align: justify;"></asp:Label>
      </div>
      <div class="col-xs-offset-1 col-xs-11">
         <h4>
            <asp:Label ID="styleHeader" Text="" runat="server"></asp:Label>
         </h4>
         <div id="main" style="visibility: hidden;">
            <ul runat="server" id="tiles" class="tiles">
            </ul>
            <ul runat="server" id="tiles_small" class="tiles_small">
            </ul>
         </div>
         <h4>
            <asp:Label ID="styleHeader2" Text="" runat="server"></asp:Label>
         </h4>
         <div id="main2" style="visibility: hidden;">
            <ul runat="server" id="tiles_small2" class="tiles_small">
            </ul>
         </div>
         <h4>
            <asp:Label ID="styleHeader3" Text="" runat="server"></asp:Label>
         </h4>
         <div id="main3" style="visibility: hidden;">
            <ul runat="server" id="tiles_small3" class="tiles_small">
            </ul>
         </div>
         <h4>
            <asp:Label ID="styleHeader4" Text="" runat="server"></asp:Label>
         </h4>
         <div id="main4" style="visibility: hidden;">
            <ul runat="server" id="tiles_small4" class="tiles_small">
            </ul>
         </div>
         <h4>
            <asp:Label ID="styleHeader5" Text="" runat="server"></asp:Label>
         </h4>
         <div id="main5" style="visibility: hidden;">
            <ul runat="server" id="tiles_small5" class="tiles_small">
            </ul>
         </div>
      </div>
      <div id="selectedComponent" class="col-xs-4 col-sm-3 col-sm-offset-1 col-xs-offset-5">
         <ul runat="server" id="comp">
         </ul>
      </div>
      <div id="orderForm" class="col-xs-12 col-sm-8" runat="server">
         <div class="col-xs-12 input-form">
            <label class="col-xs-4 text-right">Width:</label>
            <div class="col-xs-8 col-md-3 col-lg-2" style="margin-top: 5px;">
               <asp:DropDownList ID="compWidth" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
            </div>
         </div>
         <div class="col-xs-12 input-form">
            <label class="col-xs-4 text-right">Depth:</label>
            <div class="col-xs-8 col-md-3 col-lg-2" style="margin-top: 5px;">
               <asp:DropDownList ID="compDepth" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
            </div>
         </div>
         <div class="col-xs-12 input-form">
            <label class="col-xs-4 text-right">Height:</label>
            <div class="col-xs-8 col-md-3 col-lg-2" style="margin-top: 5px;">
               <asp:DropDownList ID="compHeight" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
            </div>
         </div>
         <div class="col-xs-12 input-form" id="divDoors" runat="server">
            <label class="col-xs-4 text-right">Door Hinge Side:</label>
            <div class="col-xs-8 col-md-3 col-lg-2" style="margin-top: 5px;">
               <asp:DropDownList ID="compDoors" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
            </div>
         </div>
         <%--            <div class="col-xs-12 input-form">
            <label class="col-xs-4 text-right">Material:</label>
            <div class="col-xs-8" style="margin-top: 5px;">
                <asp:DropDownList ID="compMaterial" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
            </div>
            </div>--%>
         <!-- <div id="notes" class="row-fluid input-form" runat="server">
            Notes:<textarea name="Text1" cols="40" rows="3" class="input-form" style="width: 80%;"></textarea>
            </div> -->
         <div class="col-xs-offset-4 col-xs-8 input-form text-center">
            <div>
               Price :
               <asp:Label ID="price" runat="server" Style="display: inline-block;">N/A</asp:Label>
            </div>
            <asp:HiddenField ID="compPrice" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="compWidthVal" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="compHeightVal" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="compDepthVal" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="compDoorsVal" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="compMaterialVal" runat="server"></asp:HiddenField>
         </div>
         <div class="col-xs-offset-4 col-xs-8 text-center">
            <div class="mx-auto row" style="max-width:475px">
               <button id="btnSelect" visible="false" class="btn btn-success" runat="server" onserverclick="btnSelect_Click">
               <i class="fa fa-edit"></i>&nbsp;Select
               </button>
                   <button id="btnBack" visible="false" class="btn-outline-primary float-right  mt-0 bg-white text-primary" runat="server" onserverclick="btnGoBack_Click">
                   <i class="fa fa-undo"></i>&nbsp;Back To Set Layout
                    </button>
                   <button id="btnConfig" visible="false" class="btn btn-primary text-white " runat="server" onserverclick="btnConfig_Click">
                   <i class="fa fa-edit"></i>&nbsp;Select
                   </button>
            </div>
         </div>
         <div class="col-xs-12" style="text-align: center;">
            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label>
         </div>
         <div>
            <asp:Label ID="numShelves" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numDoors" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="formula" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numDrawers" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numHandles" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="faceDoorCoverage" runat="server" Style="display: none;"></asp:Label>
         </div>
         <!-- ModalPopupExtender -->
         <%--        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnOrder"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>--%>
         <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <h3>What Happens Next</h3>
            <div id="orderSummary" class="span4">
               <%--                <div id="order_values" class="customized-values">
                  <label runat="server" id="orderValues"></label>
                  <asp:HiddenField ID="hdnOrderValues" runat="server"></asp:HiddenField>
                  </div>--%>
               <br />
               <%--                <h4><b>Total Price:</b>
                  <asp:Label ID="lblOrderPrice" runat="server" Style="display: inline-block">N/A</asp:Label><br />
                  <br />--%>
               <b>Notes:</b></h4>
               <textarea name="Text1" id="orderNotes" runat="server" rows="4" style="width: 94%;"></textarea>
               <br />
               <br />
            </div>
            <div>
               <asp:Button ID="btnNext" runat="server" Visible="false" Text="What Happens Next?" Style="width: 94%!important;" />
            </div>
            <asp:Button ID="btnSend" runat="server" Text="Send" />
            <asp:Button ID="btnClose" runat="server" Text="Cancel" />
         </asp:Panel>
         <!-- ModalPopupExtender -->
         <!-- ModalPopupExtender -->
      </div>
      <div class="col-xs-12">
         <div id="gallery-container">
            <ul runat="server" class="items--small" id="gallery">
            </ul>
            <ul runat="server" class="items--big" id="gallery_large">
            </ul>
            <div class="controls">
               <span class="control icon-arrow-left" data-direction="previous"></span>
               <span class="control icon-arrow-right" data-direction="next"></span>
               <span class="grid icon-grid" id="gridIcon"></span>
               <%--<span class="fs-toggle icon-fullscreen"></span>--%>
            </div>
         </div>
      </div>
   </div>
   <script>
     
      $(window).on('load', function () {
      });
      
      $(window).load(function () {
          $('#gallery-container').sGallery({
              fullScreenEnabled: true
          });
          $("#MainContent_pageContent").show();
          var compID = getUrlVars()["CompId"];
          if (compID != null) {
              CalculatePrice();
          }
      
      });

       function CalculatePrice() {
           //** Get Formula Values **//
           var w = parseInt($("#MainContent_compWidth option:selected").text().replace('"', ''));
           $('#MainContent_compWidthVal').val(w);
           var h = parseInt($("#MainContent_compHeight option:selected").text().replace('"', ''));
           $('#MainContent_compHeightVal').val(h);
           var d = parseInt($("#MainContent_compDepth option:selected").text().replace('"', ''));
           $('#MainContent_compDepthVal').val(d);
           $('#MainContent_compMaterialVal').val($("#MainContent_compMaterial option:selected").text());
           var numShelves = isNaN(parseInt($("#MainContent_numShelves").text())) ? 0 : parseInt($("#MainContent_numShelves").text());
           var numDoors = isNaN(parseInt($("#MainContent_numDoors").text())) ? 0 : parseInt($("#MainContent_numDoors").text());
           var formula = isNaN(parseInt($("#MainContent_formula").text())) ? 0 : parseInt($("#MainContent_formula").text());
           var numDrawers = isNaN(parseInt($("#MainContent_numDrawers").text())) ? 0 : parseInt($("#MainContent_numDrawers").text());
           var numHandles = isNaN(parseInt($("#MainContent_numHandles").text())) ? 0 : parseInt($("#MainContent_numHandles").text());
           var faceDoorCoverage = isNaN(parseFloat($("#MainContent_faceDoorCoverage").text())) ? 0 : parseFloat($("#MainContent_faceDoorCoverage").text());

            var obj = {
                       'w': w, 'h': h, 'd': d, 'numDrawers': numDrawers,
                       'numHandles': numHandles,
                       'numDoors': numDoors,
                       'numShelves': numShelves,
                       'faceDoorCoverage': faceDoorCoverage,
                       'formula': formula
            }
           var json = Sys.Serialization.JavaScriptSerializer.serialize(obj);
           $.ajax({
               type: "Post",
               url: 'Project.aspx/GetComponentPrice',
               data: json,
               dataType: 'json',
               contentType: 'application/json; charset=utf-8',
               success: function (data) {
                   price = data.d;
                   $('#MainContent_price').text("$" + price.toFixed(2));
                   $('#MainContent_compPrice').val("$" + price.toFixed(2));
                   $('#MainContent_lblOrderPrice').text("$" + price.toFixed(2));
                   $('#MainContent_hdnOrderPrice').text("$" + price.toFixed(2));

                   var compID = getUrlVars()["CompId"];
                   var doors = "N/A"
                   if (numDoors != 0) {
                       doors = $('#MainContent_compDoors').val();
                       $('#MainContent_compDoorsVal').val(doors)
                   }

                   $('#MainContent_orderValues').html("<b>Component ID :</b>" + compID + "&nbsp; <b>W:</b>" + $('#MainContent_compWidth').val() +
                       " <b>D:</b>" + $('#MainContent_compDepth').val() + " <b>H:</b>" + $('#MainContent_compHeight').val() +
                       " <b>Doors:</b>" + doors + " <b>Material:</b>" + $('#MainContent_compMaterialVal').val() + "</div>");

                   $('#MainContent_hdnOrderValues').text("<b>Component ID :</b>" + +" &nbsp; <b>W:</b>" + $('#MainContent_compWidthVal').val() +
                       " <b>D:</b>" + $('#MainContent_compDepthVal').val() + " <b>H:</b>" + $('#MainContent_compHeightVal').val() +
                       " <b>Doors:</b>" + doors + " <b>Material:</b>" + $('#MainContent_compMaterialVal').val() + "</div>");
               }
           });
       }

      $('#gridIcon').click(function () {
          $('#MainContent_gallery_large').hide();
      });
      
      $("li[id^='gallery_small']").click(function () {
          $('#MainContent_gallery_large').show();
      });
      
      $('#MainContent_compHeight').change(function () {
          CalculatePrice();
      });
      $('#MainContent_compWidth').change(function () {
          CalculatePrice();
      });
      $('#MainContent_compDepth').change(function () {
          CalculatePrice();
      });
      $('#MainContent_compMaterial').change(function () {
          CalculatePrice();
      });
      $('#MainContent_compDoors').change(function () {
          CalculatePrice();
      });
   </script>
   <!-- Once the page is loaded, initalize the plug-in. -->
   <script type="text/javascript">
      (function ($) {
          //Get Handlers for lists
          handler = $('#MainContent_tiles li, #MainContent_tiles_small li, #MainContent_component li'); // Get a reference to your grid items.
          handler2 = $('#MainContent_tiles_small2 li'); // Get a reference to your grid items.
          handler3 = $('#MainContent_tiles_small3 li'); // Get a reference to your grid items.
          handler4 = $('#MainContent_tiles_small4 li'); // Get a reference to your grid items.
          handler5 = $('#MainContent_tiles_small5 li'); // Get a reference to your grid items.
      
          /**************************************************/
          /******   Styles  ********************************/
          /*************************************************/
          $('#MainContent_tiles, #MainContent_tiles_small', '#MainContent_tiles_small2').imagesLoaded(function () {
      
              // Prepare layout options.
              var options = {
                  autoResize: true, // This will auto-update the layout when the browser window is resized.
                  container: $('#main'), // Optional, used for some extra CSS styling
                  align: 'left',
                  offset: 5//, // Optional, the distance between grid items
              };
              var options2 = {
                  autoResize: true, // This will auto-update the layout when the browser window is resized.
                  container: $('#main2'), // Optional, used for some extra CSS styling
                  align: 'left',
                  offset: 5//, // Optional, the distance between grid items
      
              };
              var options3 = {
                  autoResize: true, // This will auto-update the layout when the browser window is resized.
                  container: $('#main3'), // Optional, used for some extra CSS styling
                  align: 'left',
                  offset: 5//, // Optional, the distance between grid items
      
      
              };
              var options4 = {
                  autoResize: true, // This will auto-update the layout when the browser window is resized.
                  container: $('#main4'), // Optional, used for some extra CSS styling
                  align: 'left',
                  offset: 5//, // Optional, the distance between grid items
      
              };
              var options5 = {
                  autoResize: true, // This will auto-update the layout when the browser window is resized.
                  container: $('#main5'), // Optional, used for some extra CSS styling
                  align: 'left',
                  offset: 5//, // Optional, the distance between grid items
              };
              // Call the layout function.
              handler.wookmark(options);
              handler2.wookmark(options2);
              handler3.wookmark(options3);
              handler4.wookmark(options4);
              handler5.wookmark(options5);
      
              //Show panel
              //Show Hidden Containers
              $('#main').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
              $('#main2').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
              $('#main3').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
              $('#main4').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
              $('#main5').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
          });
      
      
      
      })(jQuery);
      
      // Read a page's GET URL variables and return them as an associative array.
      function getUrlVars() {
          var vars = [], hash;
          var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
          for (var i = 0; i < hashes.length; i++) {
              hash = hashes[i].split('=');
              vars.push(hash[0]);
              vars[hash[0]] = hash[1];
          }
          return vars;
      }
   </script>
</asp:Content>