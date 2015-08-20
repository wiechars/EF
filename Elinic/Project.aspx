<%@ Page Title="Project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Elinic.Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
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
    <hgroup class="title">
        <h1><%: Title %></h1>
        <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" Style="float: right;" />
        <hr />
        <h4> <asp:Label id ="lblDescription" runat="server" Text=""></asp:Label></h4>
    </hgroup>
    <div id="layoutsDiv" runat="server">
        <h2>Standard Layouts</h2>
    </div>
    <div id="layoutsDivContent" runat="server" style="padding: 0px 0px 20px 0px;">
        <div id="main" style="clear: both;">
            <ul runat="server" id="tiles">
            </ul>
            <ul runat="server" id="tiles_small" style="overflow: auto">
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



    <div>
        <div id="selectedLayout" class="span4" style="display: inline-block;">
            <ul runat="server" id="layout" style="text-align: center">
            </ul>
        </div>
        <div id="selectedComponent" class="span5" runat="server">
            <div class="customize-title">Customize your components by selecting from below.</div>
            <ul runat="server" id="comp_small">
            </ul>
        </div>
        <div id="custom_values" class="span5">
            <label runat="server" id="values"></label>
            <div id="notes" class="row-fluid input-form" runat="server" style="margin-top: 10px;">
                <div class="row-fluid input-form">
                    Total Price:
            <asp:Label ID="lblTotalPrice" runat="server" Style="display: inline-block;">N/A</asp:Label>
                </div>
                <!-- <textarea name="Text1" id="txtNotes" runat="server" rows="4" class="input-form"></textarea>
                Notes:<br /> -->
                <asp:Button ID="btnOrder" runat="server" Text="Select" />
                <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label>

            </div>

        </div>
        <!-- ModalPopupExtender -->
        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnOrder"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <h3>Order Summary</h3>
            <div id="order_values" class="span4">
                <label runat="server" id="orderValues"></label>
                <br />
                <b>Total Price:</b>
                <asp:Label ID="lblOrderPrice" runat="server" Style="display: inline-block">N/A</asp:Label><br />
                <br />
                <b>Notes:</b>
                <textarea name="Text1" id="orderNotes" runat="server" rows="4" style="width: 50%;"></textarea>
                <br />
                <br />
            </div>
            What happens next?....<br />
            <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" />
            <asp:Button ID="btnClose" runat="server" Text="Cancel" />
        </asp:Panel>
        <!-- ModalPopupExtender -->


    </div>
    <div class="span8" style="display: inline-block">
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


    <script>
        $(document).ready(function () {
            $('#gallery-container').sGallery({
                fullScreenEnabled: true
            });
        });
    </script>
    <script type="text/javascript">

        $('#gridIcon').click(function () {
            $('#MainContent_gallery_large').hide();
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
                offset: 5//, // Optional, the distance between grid items
                // outerOffset: 10, // Optional, the distance to the containers border
                //  itemWidth: 400 // Optional, the width of a grid item
            };

            $('#MainContent_tiles, #MainContent_tiles_small, #MainContent_tiles_ideas').imagesLoaded(function () {
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





