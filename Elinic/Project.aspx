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
            width: 80%;
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
            color: #fff;
        }
    </style>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pageContent" runat="server">
        <hgroup class="title">
            <h1><%: Title %></h1>
            <button id="btnHome2" class="btn btn-primary" runat="server" onserverclick="btnHome_Click" style="float: right; width: 110px;">
                <i class="fa fa-home"></i>&nbsp;Home
            </button>
            <a target="_blank" href="/Help.aspx" class="btn  dark" runat="server" style="float: right; width: 110px;">
                <i class="fa fa-info-circle"></i>&nbsp;Help
            </a>
            <hr />

        </hgroup>
        <h4 runat="server" ID="index" class="col-sm-offset-1 mb-3"></h4>
        <div class="col-sm-offset-1 col-sm-10">
            <asp:Label ID="lblDescription" runat="server" Text="" Style="display: block; text-align: justify;"></asp:Label>
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

        <div class="col-xs-12 row">
            <div class=" col-sm-offset-1  col-sm-6 col-xs-12 ">
                <asp:Label ID="lblLayoutDescription" runat="server" Text="" class="customize-title p-3 col-xs-12">Layout Description.</asp:Label>
                 <div id="selectedLayout">
                     <ul runat="server" id="layout" >
                     </ul>
                 </div>
             </div>
             <div id="selectedComponent" class="col-xs-12  col-sm-offset-1 col-sm-10  row" runat="server">
                <div class="">
                    <label class="customize-title p-3 col-xs-12 text-center">
                        Configure and add components to your project. 
                        Click on the image(or a little gear button) to configure.</label>
                </div>
                <ul runat="server" id="comp_small" class="">
                    <li class="h-auto" style="width:100%">
                        <div>
                            <h3 class="pb-3 mb-3 border-bottom">Materials</h3>
                            <div runat="server" ID="MaterialsContainer">

                            </div>
                            <div class="mt-auto">
                                
                            <a runat="server" ID="CustomizeMaterial" class="btn btn-primary btn-fluid py-3 text-light mt-3">
                                <i class="fa fa-wrench fa-lg mr-2"></i>
                                <h4 class="m-0 d-inline-block align-middle">Select</h4>
                            </a>
                            </div>
                        </div>
                    </li>
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





