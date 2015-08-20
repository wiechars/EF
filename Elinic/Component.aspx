<%@ Page Title="Component" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Component.aspx.cs" Inherits="Elinic.Component" %>

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

    <div id="main">
        <ul runat="server" id="tiles">
        </ul>
        <ul runat="server" id="tiles_small">
        </ul>
    </div>
    <div id="selectedComponent" class="span4">
        <ul runat="server" id="comp">
        </ul>
    </div>
    <div id="orderForm" class="span5" runat="server">
        <div class="row-fluid input-form">
            Width:<asp:DropDownList ID="compWidth" runat="server" class="input-form" Style="width: 80%;"></asp:DropDownList>
        </div>
        <div class="row-fluid input-form">
            Depth:<asp:DropDownList ID="compDepth" runat="server" class="input-form" Style="width: 80%;"></asp:DropDownList>
        </div>
        <div class="row-fluid input-form">
            Height:<asp:DropDownList ID="compHeight" runat="server" class="input-form" Style="width: 80%;"></asp:DropDownList>
        </div>
        <div class="row-fluid input-form" id ="divDoors" runat="server">
            Doors:<asp:DropDownList ID="compDoors" runat="server" class="input-form" Style="width: 80%;"></asp:DropDownList>
        </div>
        <div class="row-fluid input-form">
            Material:<asp:DropDownList ID="compMaterial" runat="server" class="input-form" Style="width: 80%;"></asp:DropDownList>
        </div>

        <!-- <div id="notes" class="row-fluid input-form" runat="server">
            Notes:<textarea name="Text1" cols="40" rows="3" class="input-form" style="width: 80%;"></textarea>
        </div> -->

        <div class="row-fluid input-form">
            Total Price : 
            <asp:Label ID="price" runat="server" Style="display: inline-block;">N/A</asp:Label>
            <asp:HiddenField ID="compPrice" runat="server"></asp:HiddenField>
        </div>
        <div class="row-fluid input-form">
            <asp:Button ID="btnOrder" runat="server" Text="Select" />
            <asp:Button ID="btnConfigure" runat="server" Text="Select" Visible="false" OnClick="btnConfigure_Click" />
            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" Visible="false" OnClick="btnGoBack_Click" />
            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label>

        </div>
        <div>
            <asp:Label ID="numShelves" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numDoors" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numDrawers" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="numHandles" runat="server" Style="display: none;"></asp:Label>
            <asp:Label ID="faceDoorCoverage" runat="server" Style="display: none;"></asp:Label>

        </div>

        <!-- ModalPopupExtender -->
        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnOrder"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
            <h3>Order Summary</h3>
            <div id="orderSummary" class="span4">
                <div id="order_values" class="customized-values">
                    <label runat="server" id="orderValues"></label>
                    <asp:HiddenField ID="hdnOrderValues" runat="server"></asp:HiddenField>
                </div>
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

    <div id="gallery-container" style="width: 100%">
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



    <script>
        var WASTE_FACTOR = 1.2;
        var PANEL_PRICE_PER_SQ_INCH = 80 / 48 / 96;
        var ONE_DRAWER_PRICE = 40;
        var ONE_HANDLE_PRICE = 4;
        var ONE_HINGE_PRICE = 5;
        var FINISH_PRICE_PER_SQ_INCH = 20.0 / 2588;
        var EDGE_PRICE_PER_SQ_INCH = 0.007
        var GLUE_FASTENERS_SCREWS = 0.002

        $(document).ready(function () {

            $('#gallery-container').sGallery({
                fullScreenEnabled: true
            });
            var compID = getUrlVars()["CompId"];
            if (compID != null) {
                CalculatePrice();


            }
        });

        function CalculatePrice() {
            var w = $("#MainContent_compWidth option:selected").text().replace('"', '');
            var h = $("#MainContent_compHeight option:selected").text().replace('"', '');
            var d = $("#MainContent_compDepth option:selected").text().replace('"', '');
            var numShelves = $("#MainContent_numShelves").text();
            var numDoors = $("#MainContent_numDoors").text();
            var numDrawers = $("#MainContent_numDrawers").text();
            var numHandles = $("#MainContent_numHandles").text();
            var faceDoorCoverage = $("#MainContent_faceDoorCoverage").text();

            var area = w * d * (2 + parseInt(numShelves)) + 2 * h * d + w * h * (1 + parseFloat(faceDoorCoverage));
            var materialUsed = area * WASTE_FACTOR;
            var panelPrice = materialUsed * PANEL_PRICE_PER_SQ_INCH;
            var drawerPrice = ONE_DRAWER_PRICE * numDrawers;
            var handlePrice = ONE_HANDLE_PRICE * numHandles;
            var edgePrice = EDGE_PRICE_PER_SQ_INCH * area;
            var hingePrice = ONE_HINGE_PRICE * numDoors;
            var finishPrice = FINISH_PRICE_PER_SQ_INCH * area;
            var otherPrice = GLUE_FASTENERS_SCREWS * area;
            var price = (panelPrice + drawerPrice + handlePrice + edgePrice + hingePrice + finishPrice + otherPrice) * 2;


            $('#MainContent_price').text("$" + price.toFixed(2));
            $('#MainContent_compPrice').val("$" + price.toFixed(2));
            $('#MainContent_lblOrderPrice').text("$" + price.toFixed(2));
            $('#MainContent_hdnOrderPrice').text("$" + price.toFixed(2));

            var compID = getUrlVars()["CompId"];
            var doors = "N/A"
            if (numDoors != 0) {
                doors = $('#MainContent_compDoors').val();
            }

            $('#MainContent_orderValues').html("<b>Component ID :</b>"+compID+"&nbsp; <b>W:</b>" + $('#MainContent_compWidth').val() +
                 " <b>D:</b>" + $('#MainContent_compDepth').val() + " <b>H:</b>" + $('#MainContent_compHeight').val() +
                 " <b>Doors:</b>" + doors + " <b>Material:</b>" + $('#MainContent_compMaterial').val() + "</div>");

            $('#MainContent_hdnOrderValues').Text("<b>Component ID :</b>"+ +" &nbsp; <b>W:</b>" + $('#MainContent_compWidth').val() +
                 " <b>D:</b>" + $('#MainContent_compDepth').val() + " <b>H:</b>" + $('#MainContent_compHeight').val() +
                 " <b>Doors:</b>" + doors + " <b>Material:</b>" + $('#MainContent_compMaterial').val() + "</div>");

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
    </script>

    <!-- Once the page is loaded, initalize the plug-in. -->
    <script type="text/javascript">
        (function ($) {

            var loadedImages = 0, // Counter for loaded images
                handler = $('#MainContent_tiles li, #MainContent_tiles_small li, #MainContent_component li'); // Get a reference to your grid items.
            // Prepare layout options.
            var options = {
                autoResize: true, // This will auto-update the layout when the browser window is resized.
                container: $('#main'), // Optional, used for some extra CSS styling
                offset: 5//, // Optional, the distance between grid items
                // outerOffset: 10, // Optional, the distance to the containers border
                //  itemWidth: 400 // Optional, the width of a grid item
            };

            $('#MainContent_tiles, #MainContent_tiles_small').imagesLoaded(function () {
                // Call the layout function.
                handler.wookmark(options);

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





