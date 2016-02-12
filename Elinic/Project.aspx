<%@ Page Title="Project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Elinic.Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
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
    </style>

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pageContent" runat="server">
        <hgroup class="title">
            <h1><%: Title %></h1>
            <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" Style="float: right;" />
            <hr />
            <asp:Label ID="lblDescription" runat="server" Text="" Style="display: block; text-align: justify;"></asp:Label>
        </hgroup>
        <div id="layoutsDiv" runat="server">
            <h2>Standard Layouts</h2>
        </div>
        <div id="layoutsDivContent" runat="server" style="padding: 0px 0px 0px 0px;">
            <div id="main" style="clear: both;">
                <ul runat="server" id="tiles" class="tiles">
                </ul>
                <ul runat="server" id="tiles_small" class="tiles_small" style="overflow: auto">
                </ul>
            </div>
        </div>

        <div id="ideasDiv" runat="server">
            <h2>Ideas</h2>
            <div id="main2">
                <ul runat="server" id="tiles_ideas">
                </ul>
            </div>
        </div>



        <div class="col-xs-12 col-sm-3 col-sm-offset-1">
            &nbsp;&nbsp;
            <div id="selectedLayout" class="col-xs-12 col-sm-8">
                <ul runat="server" id="layout">
                </ul>
            </div>
        </div>
        <div id="selectedComponent" class="col-xs-12 col-sm-8" runat="server">
            <div class="col-xs-12 col-sm-8">
                <label class="customize-title col-xs-12">Customize your components by selecting from below.</label>
            </div>
            <ul runat="server" id="comp_small" class="col-xs-12">
            </ul>
        </div>
        <div id="notes" class="col-xs-12 input-form" runat="server">
            <div class="col-xs-12 text-center ">
                <div>
                    Total Price :
                        <asp:Label ID="lblTotalPrice" runat="server" Style="display: inline-block;">N/A</asp:Label>
                </div>
            </div>
            <div class="col-xs-12 input-form text-center">
                <asp:Button ID="btnOrder" runat="server" Text="Select" OnClick="btnOrder_Click" />

            </div>
            <div class="col-xs-12" style="text-align: center;">
                <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label></div>

        </div>


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

    </div>
    <script>

        $(window).on('load', function () {
            //Hide Containers until everything is loaded.
            //$('#main').hide();
            //$('#main2').hide();
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
            nextId = parseInt(li.prop('id').match(/\d+/g), 10) + 1;
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
                            if ($('#Comp' + nextId).hasClass('configured')) {
                                nextId++;
                            } else {
                                alert('Please configure all instances of this component before adding additional');
                                break;
                            }
                        } else {
                            $('#liAddComponent' + nextId).show();
                            $('#Comp' + nextId).show();
                            found = true;
                        }
                    }
                }
            } else {
                alert('Please configure all instances of this component before adding additional');
            }
        });

        $("[id ^= 'Remove']").click(function () {
            id = this.id.replace(/[^\d.]/g, '');

            $('#liAddComponent' + id).hide();
            $('#Comp' + id).css('visibility', 'hidden');

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
                autoResize: false, // This will auto-update the layout when the browser window is resized.
                container: $('#main, #main2'), // Optional, used for some extra CSS styling
                align: 'left',
                offset: 5//, // Optional, the distance between grid items
                // outerOffset: 10, // Optional, the distance to the containers border
                //  itemWidth: 400 // Optional, the width of a grid item
            };

            $('#MainContent_tiles, #MainContent_tiles_small, #MainContent_tiles_ideas').imagesLoaded(function () {
                //Show Hidden Containers
                //$('#main').show();
                //$('#main2').show();
                // Call the layout function.
                handler.wookmark(options);
                handler2.wookmark(options);

                // Capture clicks on grid items.
                handler.click(function () {

                    // Randomize the height of the clicked item.
                    //var newHeight = $('img', this).height() + Math.round(Math.random() * 300 + 30);
                    //$(this).css('height', newHeight + 'px');

                    // Update the layout.
                    handler.wookmark();
                });
            }).progress(function (instance, image) {
                // Update progress bar after each image load
                loadedImages++;
                if (loadedImages == handler.length)
                    $('.progress-bar').hide();
                else
                    $('.progress-bar').width((loadedImages / handler.length * 100) + '%');
            });
        })(jQuery);
    </script>
</asp:Content>





