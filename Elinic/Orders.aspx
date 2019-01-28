<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Elinic.Orders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="pageContent" runat="server">
        <div>
            <hgroup class="title">
                <h1><%: Title %></h1>
                <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" Style="float: right;" />

                <hr />
            </hgroup>
        </div>
        <div class="row">
            <div class="col-xs-12">

                <table id="sampleTable" class="col-xs-12">
                    <thead>
                        <tr>
                            <th>Order #</th>
                            <th>Email Address</th>
                            <th>Order Date</th>
                            <th>Price</th>
                            <th>Notes</th>
                            <th>Order Detail</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row">
            <div id="OrderSummary" class="col-xs-12 ">
                <hgroup class="title">
                   
                <h3><strong>
                    <label id="lblOrderSummary"> *Select Order From Above</label>
                </strong></h3>
                    <%--<asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" Style="float: right;" />
                    --%>
                    <hr />
                </hgroup>
            </div>
            <div class="row">
                <div class="form-group col-xs-11">
                    <label for="InputFieldEmail" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Components:</label>
                    <div class="col-lg-10 col-md-12 col-xs-12">
                        <textarea name="Text1" id="txtAreaComponents" runat="server" rows="6" placeholder="" style="background-color: #eee!important;" disabled></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-xs-11 col-sm-11 ">
                    <label for="lblPrice" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Total Price:</label>
                    <div class="col-lg-10 col-md-12 col-xs-12">
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-xs-11 col-sm-11 ">
                    <label for="InputFieldEmail" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Email:</label>
                    <div class="col-lg-10 col-md-12 col-xs-12">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-xs-11 col-sm-11 ">
                    <label for="InputFieldNotes" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Notes:</label>
                    <div class="col-lg-10 col-md-12 col-xs-12">
                        <textarea name="Text1" id="txtNotes" runat="server" rows="2" placeholder="" style="background-color: #eee!important;" disabled />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-xs-11 col-sm-11 ">
                    <label for="InputFieldNotes" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Order Date:</label>
                    <div class="col-lg-10 col-md-12 col-xs-12">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var table = $("#sampleTable").dataTable({
                "oLanguage": {
                    "sZeroRecords": "No records to display",
                    "sSearch": "Search: "
                },
                "aLengthMenu": [[25, 50, 100], [25, 50, 100]],
                "iDisplayLength": 25,
                "bSortClasses": false,
                "bStateSave": false,
                "bPaginate": true,
                "bAutoWidth": false,
                "bProcessing": true,
                "bServerSide": true,
                "bDestroy": true,
                "sAjaxSource": "OrdersWebService.asmx/GetTableData",
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "bDeferRender": true,
                "columnDefs": [
                    {
                        "targets": [3],
                        "width": "30%",
                        "searchable": false
                    },
                    {
                        "targets": [4],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [5],
                        "visible": false
                    }
                ],
                "fnServerData": function (sSource, aoData, fnCallback) {
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success":
                            function (msg) {
                                        
                                        var json = jQuery.parseJSON(msg.d);
                                        fnCallback(json);
                                        $("#sampleTable").show();
                                    }
                    });
                }
            });


            $('#sampleTable tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }

                $('html, body').animate({
                    scrollTop: $("#OrderSummary").offset().top
                }, 1000);

                var values = table.api().row(this).data();
                $('#lblOrderSummary').text("Order # " + values[0]);
                $('#MainContent_txtAreaComponents').val(values[5]);
                $('#MainContent_txtPrice').val(values[3]);
                $('#MainContent_txtEmail').val(values[1]);
                $('#MainContent_txtNotes').val(values[4]);
                $('#MainContent_txtDate').val(values[2]);
            });

        });

    </script>
</asp:Content>





