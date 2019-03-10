<%@ Page Title="Select the type of your project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Elinic.Project" %>

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
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            text-align: center;
            padding-top: 10px;
            padding-left: 5px;
            padding-right: 5px;
            overflow-y: auto;
            height: auto;
        }
        .card .btn {
            color:#fff;
        }
    </style>

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pageContent" runat="server">
        <hgroup class="title">
            <h1><%: Title %></h1>
            <button id="btnHome2" class="btn btn-primary" runat="server" onserverclick="btnHome_Click" style="float: right; width: 110px;">
                <i class="fa fa-home"></i>&nbsp;Home
            </button>
            <a id="btnHelp" href="/Help.aspx" class="btn  dark" runat="server" style="float: right; width: 110px;">
                <i class="fa fa-info-circle"></i>&nbsp;Help
            </a>
            <hr />

        </hgroup>
        <div class="col-sm-offset-1 col-sm-10">
            <asp:Label ID="lblDescription" runat="server" Text="" Style="display: block; text-align: justify;"></asp:Label>
            <asp:Label ID="lblLayoutDescription" runat="server" Text="" class="customize-title">Layout Description.</asp:Label>
        </div>
        <div class="col-sm-offset-1 col-sm-10">
            <div class="col-xs-12 col-sm-offset-8 col-sm-3  col-md-offset-9 col-md-2">
                <asp:Button ID="btnBackToProjects" runat="server" Text="Back To Projects" Visible="false" OnClick="btnBackToProjects_click" />
            </div>
        </div>

        <div class="col-sm-11">
            <div id="layoutsDiv" runat="server" class="container">
                <h2>Standard Layouts</h2>
            </div>
            <div id="layoutsDivContent" runat="server" style="padding: 0px 0px 0px 0px;" class="container">
                <div id="main" style="clear: both; visibility: hidden;;">
                    <ul runat="server" id="tiles" class="row">
                    </ul>
                    <ul runat="server" id="tiles_small" class="tiles_small row" style="overflow: auto">
                    </ul>
                </div>
            </div>

            <div id="ideasDiv" runat="server" class="container">
                <h2>Ideas</h2>
                <div id="main2" style="visibility: hidden;">
                    <ul runat="server" id="tiles_ideas" class="row">
                    </ul>
                </div>
            </div>

        </div>

        <div class="col-xs-12">
            <div class="col-xs-12 col-sm-12 col-lg-6 ">
                &nbsp;&nbsp;
             <div class="col-xs-12 row ">
                 <div id="selectedLayout" class="col-sm-4 col-lg-4">
                     <ul runat="server" id="layout">
                     </ul>
                 </div>
                 <div id="selectedMaterial" class="col-sm-8 col-lg-8" runat="server">
                     <label class="customize-title col-xs-12" style="margin-bottom: 5px !important;">Select Your Material Finish</label>
                     <div class="col-xs-12">
                         <div class="col-xs-4 text-center">
                             <img id="imgMaterial" style="max-width: 60%; max-height: 60%; border: 1px solid #dedede; border-radius: 10px;"
                                 src="" runat="server" />
                         </div>
                         <div class="col-xs-8 ">
                             <%-- <label class="col-xs-4 text-right">Material:</label>--%>
                             <div class="col-xs-12" style="margin-top: 5px;">
                                 <asp:DropDownList ID="compMaterial" runat="server" class="input-form" Style="width: 100%;"
                                     AutoPostBack="true" OnSelectedIndexChanged="MaterialChanged">
                                 </asp:DropDownList>
                             </div>
                             <%--  <label class="col-xs-4 text-right">Lacquer Finish:</label>--%>
                             <div class="col-xs-12" style="margin-top: 5px;">
                                 <asp:DropDownList ID="compFinish" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
                             </div>
                             <%-- <label class="col-xs-4 text-right">Stain:</label>--%>
                             <div class="col-xs-12" style="margin-top: 5px;">
                                 <asp:DropDownList ID="compStain" runat="server" class="input-form" Style="width: 100%;" runat="server"></asp:DropDownList>
                             </div>
                         </div>
                     </div>
                     <label class="customize-title col-xs-12" style="margin-bottom: 10px !important; margin-top: 5px !important;">Select Your Handle</label>
                     <div class="col-xs-12">
                         <div class="col-xs-4 text-center">
                             <img id="imgHandle" style="max-width: 60%; max-height: 60%; border: 1px solid #dedede; border-radius: 10px;"
                                 src="" runat="server" />
                         </div>
                         <div class="col-xs-8 ">
                             <%-- <label class="col-xs-4 text-right">Material:</label>--%>
                             <div class="col-xs-12" style="margin-top: 5px;">
                                 <asp:DropDownList ID="compHandle" runat="server" class="input-form" Style="width: 100%;"
                                     AutoPostBack="true" OnSelectedIndexChanged="HandleChanged">
                                 </asp:DropDownList>
                             </div>
                         </div>
                     </div>
                 </div>
             </div>
            </div>
            <div id="selectedComponent" class="col-xs-12  col-lg-6" runat="server">
                <div class="">
                    <label class="customize-title col-xs-12 p-3">
                        Configure and add components to your project. <br />
                        Click on the image(or a little gear button) to configure.</label>
                </div>
                <ul runat="server" id="comp_small" class="">
                </ul>
            </div>
        </div>
    </div>
    <div id="notes" class="col-xs-12 input-form" runat="server">
        <div class="col-xs-12 text-center ">
            <div>
                Total Price :
                        <asp:Label ID="lblTotalPrice" runat="server" Style="display: inline-block;">N/A</asp:Label>
            </div>
        </div>
        <div class="col-xs-12  text-center">
            <button id="btnSubmit" class="btn btn-success light" runat="server" onserverclick="btnSubmit_Click">
                <i class="fa fa-check-circle"></i>&nbsp;Finish Configuring
            </button>
            <button id="Button1" class="btn btn-primary light" runat="server" onserverclick="btnGoBack_Click">
                <i class="fa fa-undo"></i>&nbsp;Back to Layouts & Ideas
            </button>

        </div>
        <div class=" col-xs-3">
        </div>
        <div class="col-xs-12" style="text-align: center;">
            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label>
        </div>

    </div>

    <!-- ModalPopupExtender -->
    <%--            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID=""
                CancelControlID="btnCancelNext" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none" >
                <div id="Div1" class="">
                        
                    <p>
                        <br />
                        &nbsp;&nbsp;Configuration not complete.  Do you want to submit order as is or go back?<br />
                        <br />
                    </p>
                </div>
                <div>
            <asp:Button ID="Button1" runat="server" Style=" background-color: green;" Text="Submit" OnClick="btnOrder_Click" />
            <asp:Button ID="Button2" runat="server" Text="Go Back" />
                </div>

            </asp:Panel>--%>
    <!-- ModalPopupExtender -->



    <!-- ModalPopupExtender -->
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <h3>Order Summary</h3>
        <div id="order_values" class="span4">
            <label runat="server" id="orderValues"></label>
            <br />
            <h4><b>Total Price:</b>
                <asp:Label ID="lblOrderPrice" runat="server" Style="display: inline-block">N/A</asp:Label><br />
                <br />
                <b>Notes:</b></h4>
            <textarea name="Text1" id="orderNotes" runat="server" rows="4" style="width: 94%;"></textarea>
            <br />
            <br />
        </div>
        <div>
        </div>
        <div>
            <asp:Button ID="btnNext" runat="server" Text="What Happens Next?" Style="width: 94%!important;" />
        </div>
        <asp:Button ID="btnClose" runat="server" Text="Cancel" />
    </asp:Panel>
    <!-- ModalPopupExtender -->


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
    <!-- ModalPopupExtender -->
    <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div id="Div1" class="text-left">
            <h3>Help</h3>
            <hr />
            <p>
                <br />
                <asp:Label runat="server" ID="helpText"></asp:Label>
            </p>
        </div>
        <div>
            <button id="btnCloseModal" class="btn  dark" onclick="$find(mp2).hide();">
                <i class="fa fa-times"></i>&nbsp;Close
            </button>
            </br>
        </div>

    </asp:Panel>
    <!-- ModalPopupExtender -->

    <script>

        $(window).on('load', function () {
        });

        $(window).load(function () {
            $('#gallery-container').sGallery({
                fullScreenEnabled: true
            });
            $("#MainContent_pageContent").show();
        });
    </script>
    <script type="text/javascript">
        var nextId = 1;



        $('#gridIcon').click(function () {
            $('#MainContent_gallery_large').hide();
        });

        //Clone List Item
        $("[id ^= 'Add']").click(function () {
            // $("[id ^= 'Add']").hide();
            id = this.id.replace(/[^\d.]/g, '');
            var li = $('#li' + this.id);
            var comp = $('#Comp' + this.id);
            nextId = parseInt(li.prop('id').match(/\d+/g), 10) + 1;



            // alert('nextId ' + nextId);
            var found = false;

            //Don't allow creating another <li> element
            //unless the current one is configured
            if ($('#Comp' + id).hasClass('configured')) {
                while (!found) {
                    if (nextId % 5 === 1) {
                        alert("Cannot add more of this component!");
                        found = true;
                    } else {

                        if ($('#liAddComponent' + nextId).is(":visible")) {
                            nextId++;
                        } else {
                            var clone;
                            //  $('#liAddComponent' + nextId).empty();]
                            //              $('#liAddComponent' + nextId).show();
                            //            $('#liAddComponent' + nextId).empty();
                            //          $('#li' + this.id).children().clone().appendTo('#liAddComponent' + nextId);
                            //                            $('#Comp' + nextId).show();


                            //Update Session
                            $.ajax({
                                type: "POST",
                                url: 'Project.aspx/CloneSession',
                                data: "{ srcId :" + id + ", destId :" + nextId + "}",
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    location.reload();
                                    // comp.clone().appendTo('#Comp' + nextId);
                                }
                            });
                            //$('#Comp' + nextId).show();
                            found = true;
                        }
                    }
                }
            } else {
                alert('Let us suggest - Please configure this component before adding another one of the same kind. Click on the image or on the little dark blue gear button. Thank you!');
            }
        });

        //Remove item and clear configuration
        $("[id ^= 'Remove']").click(function () {
            id = this.id.replace(/[^\d.]/g, '');
            $.ajax({
                type: "POST",
                url: 'Project.aspx/RemoveSession',
                data: "{ id :" + id + "}",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    //$('#liAddComponent' + id).hide();
                    //$('#Comp' + id).css('visibility', 'hidden');
                    location.reload();

                }
            });

        });

        $("[id ^= 'Redo']").click(function () {

            id = this.id.replace(/[^\d.]/g, '');
            $.ajax({
                type: "POST",
                url: 'Project.aspx/RemoveSession',
                data: "{ id :" + id + "}",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if ($('#Comp' + id).hasClass('configured')) {
                        $('#Comp' + id).removeClass('configured');
                    }
                    $('#Comp' + id).hide();
                }
            });

        });

        $("li[id^='gallery_small']").click(function () {
            $('#MainContent_gallery_large').show();
        });

        (function ($) {

            var loadedImages = 0, // Counter for loaded images
                handler = $('#MainContent_tiles li, #MainContent_tiles_small li'); // Get a reference to your grid items.
            handler2 = $('#MainContent_tiles_ideas li'); // Get a reference to your grid items.
            // Prepare layout options.
            var options = {
                autoResize: true, // This will auto-update the layout when the browser window is resized.
                container: $('#main'), // Optional, used for some extra CSS styling
                align: 'left',
                offset: 5//, // Optional, the distance between grid items
                // outerOffset: 10, // Optional, the distance to the containers border
                //  itemWidth: 400 // Optional, the width of a grid item
            };

            var options2 = {
                autoResize: true, // This will auto-update the layout when the browser window is resized.
                container: $('#main2'), // Optional, used for some extra CSS styling
                align: 'left',
                offset: 5//, // Optional, the distance between grid items

            };

            $('#MainContent_tiles, #MainContent_tiles_small, #MainContent_tiles_ideas').imagesLoaded(function () {

                // Call the layout function.
                //handler.wookmark(options);
                handler2.wookmark(options2);

                //Show Hidden Containers
                $('#main').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);

                if ($('#MainContent_selectedComponent').length) {
                    $('#main').css({ height: "1.5em" });
                }


                $('#main2').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 1000);
            });



        })(jQuery);

        function isConfigurationComplete() {
            for (i = 1; i < 100; i++) {
                if ($('#liAddComponent' + i).length) {
                    if (!$('#liAddComponent' + i).is(":hidden")) {
                        if (!$('#Comp' + i).hasClass('configured')) {
                            alert('Please finish configuring components');
                            return false;
                        }
                    }
                }

            }
            return true;
        }
    </script>
</asp:Content>





