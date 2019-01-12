<%@ Page Title="Component" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Component.aspx.cs" Inherits="Elinic.Component" %>

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
            <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" Style="float: right; width: 110px;" />
            <hr />
        </hgroup>
        <div class="col-xs-offset-1 col-xs-10">
            <asp:Label ID="lblDescription" runat="server" Text="" Style="display: block; text-align: justify;"></asp:Label>
        </div>
        <div class="col-xs-offset-1 col-xs-11">
            <h4>
                <asp:Label ID="styleHeader" Text="" runat="server"></asp:Label></h4>
            <div id="main" style="visibility: hidden;">
                <ul runat="server" id="tiles" class="tiles">
                </ul>
                <ul runat="server" id="tiles_small" class="tiles_small">
                </ul>
            </div>
            <h4>
                <asp:Label ID="styleHeader2" Text="" runat="server"></asp:Label></h4>
            <div id="main2" style="visibility: hidden;">
                <ul runat="server" id="tiles_small2" class="tiles_small">
                </ul>
            </div>
            <h4>
                <asp:Label ID="styleHeader3" Text="" runat="server"></asp:Label></h4>
            <div id="main3" style="visibility: hidden;">
                <ul runat="server" id="tiles_small3" class="tiles_small">
                </ul>
            </div>
            <h4>
                <asp:Label ID="styleHeader4" Text="" runat="server"></asp:Label></h4>
            <div id="main4" style="visibility: hidden;">
                <ul runat="server" id="tiles_small4" class="tiles_small">
                </ul>
            </div>
            <h4>
                <asp:Label ID="styleHeader5" Text="" runat="server"></asp:Label></h4>
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
                <div class="col-xs-8" style="margin-top: 5px;">
                    <asp:DropDownList ID="compWidth" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 input-form">
                <label class="col-xs-4 text-right">Depth:</label>
                <div class="col-xs-8" style="margin-top: 5px;">
                    <asp:DropDownList ID="compDepth" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 input-form">
                <label class="col-xs-4 text-right">Height:</label>
                <div class="col-xs-8" style="margin-top: 5px;">
                    <asp:DropDownList ID="compHeight" runat="server" class="input-form" Style="width: 100%;"></asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 input-form" id="divDoors" runat="server">
                <label class="col-xs-4 text-right">Door Hinge Side:</label>
                <div class="col-xs-8" style="margin-top: 5px;">
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

            <div class="col-xs-12 input-form text-center ">
                <div>
                    Total Price :
            <asp:Label ID="price" runat="server" Style="display: inline-block;">N/A</asp:Label>
                </div>
                <asp:HiddenField ID="compPrice" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="compWidthVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="compHeightVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="compDepthVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="compDoorsVal" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="compMaterialVal" runat="server"></asp:HiddenField>
            </div>
            <div class="col-xs-12 input-form text-center">
                <asp:Button ID="btnOrder" runat="server" Text="Select" OnClick="btnOrder_Click" />
                <asp:Button ID="btnConfigure" runat="server" Text="Select" Visible="false" OnClick="btnConfigure_Click" />
                <asp:Button ID="btnGoBack" runat="server" Text="Back To Set Layout" Visible="false" OnClick="btnGoBack_Click" />


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
            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel2" TargetControlID="btnNext"
                CancelControlID="btnCancelNext" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none; width: 80%;">
                <div id="Div1" class="span4" style="font-size: 1.5em; text-align: justify; text-justify: inter-word">
                    <p>
                        <br />
                        &nbsp;&nbsp;Once you click Send, we instantly receive this design information.<br />
                        <br />
                        &nbsp; 
                    Our design technician will then pull the required components from our drawing library in a CAD design package, resize them to your dimensions, and align them in a set accordingly. The designer will verify that all component dimensions, materials, and colours make sense and work together. If not, they will suggest changes, and will create renders to help you visualize the alterations.<br />
                        <br />
                        &nbsp;
                    We'd like to make it clear that nothing gets built until we confirm every detail with you. We will show you your design, rendered in scale according to your dimensions, materials, and colour, and only when you approve will we start building. Even then, we will stay in contact with you to further verify things.  We are working with you throughout the entire process! We promise!<br />
                        <br />
                        &nbsp;
                    In most cases, simple email (and a phone call) correspondence is sufficient. However, we are happy to come to you in order to discuss/measure in person. If you changed you mind and want to cancel or decided to change the selection, it is not a problem at all! Just send an email to let us know or write us a comment in the Note box if you are sending a revised design. We welcome your ideas! So please, experiment away!<br />
                        <br />
                        &nbsp;
                    *Please double check your email address to make sure it is correct. **Click Send.
                    </p>
                </div>
                <div>
                    <button id="btnCancelNext" class="Cancel">Close</button>
                </div>

            </asp:Panel>
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
        var WASTE_FACTOR = 1.2;
        var DESK_MOLDING_FOOT_PRICE = 1.5;
        var PANEL_PRICE_PER_SQ_INCH = 80 / 48 / 96;
        var ONE_DRAWER_PRICE = 40;
        var ONE_HANDLE_PRICE = 4;
        var ONE_HINGE_PRICE = 5;
        var FINISH_PRICE_PER_SQ_INCH = 20.0 / 2588;
        var EDGE_PRICE_PER_SQ_INCH = 0.007
        var GLUE_FASTENERS_SCREWS = 0.002

        $(window).on('load', function () {
            //Hide Containers until everything is loaded.
            //$('#main').hide();
            //$('#main2').hide();
            //$('#main3').hide();
            //$('#main4').hide();
            //$('#main5').hide();
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
            var faceDoorCoverage = isNaN(parseInt($("#MainContent_faceDoorCoverage").text())) ? 0 : parseInt($("#MainContent_faceDoorCoverage").text());

            //** Common Formula **//
            var drawerPrice = ONE_DRAWER_PRICE * numDrawers;
            var handlePrice = ONE_HANDLE_PRICE * numHandles;
            var hingePrice = ONE_HINGE_PRICE * numDoors;


            //** Initialize area and price **//
            var area = 0;
            var price = 0;


            if (formula == '1') {  //Components table has "Formula" column with a value of 1
                var desktopMolding = DESK_MOLDING_FOOT_PRICE * (w + d + d);
                area = (w * d) + (3 * d * h) + (20 * w);
                var edgePrice = EDGE_PRICE_PER_SQ_INCH * area;
                var finishPrice = FINISH_PRICE_PER_SQ_INCH * area;
                var otherPrice = GLUE_FASTENERS_SCREWS * area;
                price = ((area * WASTE_FACTOR * PANEL_PRICE_PER_SQ_INCH) + desktopMolding + (drawerPrice * 1.1) + handlePrice + edgePrice + hingePrice + finishPrice + otherPrice) * 2;

            } else {
                area = w * d * (2 + parseInt(numShelves)) + 2 * h * d + w * h * (1 + parseFloat(faceDoorCoverage));
                var edgePrice = EDGE_PRICE_PER_SQ_INCH * area;
                var finishPrice = FINISH_PRICE_PER_SQ_INCH * area;
                var otherPrice = GLUE_FASTENERS_SCREWS * area;
                price = ((area * WASTE_FACTOR * PANEL_PRICE_PER_SQ_INCH) + drawerPrice + handlePrice + edgePrice + hingePrice + finishPrice + otherPrice) * 2;

            }

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

            $('#MainContent_hdnOrderValues').Text("<b>Component ID :</b>" + +" &nbsp; <b>W:</b>" + $('#MainContent_compWidthVal').val() +
                " <b>D:</b>" + $('#MainContent_compDepthVal').val() + " <b>H:</b>" + $('#MainContent_compHeightVal').val() +
                " <b>Doors:</b>" + doors + " <b>Material:</b>" + $('#MainContent_compMaterialVal').val() + "</div>");

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
        $('#MainContent_btnCancelNext').click(function () {
            var mpu = $find('mp2');
            mpu.hide();
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





