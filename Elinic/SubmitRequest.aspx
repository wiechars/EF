<%@ Page Title="Submit Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitRequest.aspx.cs" Inherits="Elinic.SubmitRequest" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>Submit Request</h1>
        <br />
        <h2>What happens Next?</h2>
        <hr />
    </hgroup>
    <div class="col-xs-12" style="margin: 0 auto;">
        <div class="col-xs-12">

            <p align="justify">
                <br />
                &nbsp;&nbsp;Once you click Send, we instantly receive this design information.<br />
                <br />
                &nbsp;Our design technician will then pull the required components from our drawing library in a CAD design package, resize them to your dimensions, and align them in a set accordingly. The designer will verify that all component dimensions, materials, and colours make sense and work together. If not, they will suggest changes, and will create renders to help you visualize the alterations.<br />
                <br />
                &nbsp;We'd like to make it clear that nothing gets built until we confirm every detail with you. We will show you your design, rendered in scale according to your dimensions, materials, and colour, and only when you approve will we start building. Even then, we will stay in contact with you to further verify things.  We are working with you throughout the entire process! We promise!<br />
                <br />
                &nbsp;In most cases, simple email (and a phone call) correspondence is sufficient. However, we are happy to come to you in order to discuss/measure in person. If you changed you mind and want to cancel or decided to change the selection, it is not a problem at all! Just send an email to let us know or write us a comment in the Note box if you are sending a revised design. We welcome your ideas! So please, experiment away!<br />
                <br />
                &nbsp;*Please double check your email address to make sure it is correct. **Click Submit.
            </p>
            <br />
        </div>

    </div>
        <div class="row">
            <div class="form-group col-xs-12">
                <label for="InputFieldEmail" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Components:</label>
                <div class="col-lg-10 col-md-12 col-xs-12">
                    <textarea name="Text1" id="txtAreaComponents" runat="server" rows="10" placeholder="Order Configuration." style="background-color: #eee!important;" disabled></textarea>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-xs-12 col-sm-12 ">
                <label for="lblPrice" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Total Price:</label>
                <div class="col-lg-10 col-md-12 col-xs-12">
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-xs-12 col-sm-12 ">
                <label for="InputFieldEmail" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Email:</label>
                <div class="col-lg-10 col-md-12 col-xs-12">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Example: user@email.com"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-xs-12 col-sm-12 ">
                <label for="InputFieldNotes" class="col-lg-2 col-md-12 col-xs-12 pull-right-lg">Notes:</label>
                <div class="col-lg-10 col-md-12 col-xs-12">
                    <textarea name="Text1" id="orderNotes" runat="server" rows="6" placeholder="Click and add notes about your order."></textarea>
                </div>
            </div>
        </div>

        <br />
        <div align="center" class="">
            <asp:Button ID="btnOrder" runat="server" OnClick="btnSend_Click" Text="Submit Set For Review" />
            <asp:Button ID="btnClose" runat="server" OnClick="btnGoBack_Click" Text="Go Back" />

        </div>
        <div align="center">
            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="display: inline-block;"></asp:Label>
        </div>


</asp:Content>
