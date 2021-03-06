﻿<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016_MidTerm_2002265211.TodoDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Todo Details</h1>
                <h5>All fields are Required</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="TodoNameTextBox">Todo Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNameTextBox" placeholder="Todo Name" required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="NotesTextBox">Notes:</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" Columns="5" Rows="6" CssClass="form-control" ID="NotesTextBox" placeholder=" " required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="DoneCheckBox">Done?</label>
                    <asp:CheckBox id="DoneCheckBox" AutoPostBack=True Text="" Checked=False OnCheckedChanged="DoneCheckBox_CheckedChanged" runat="server"/>
                </div>
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" CssClass="tbn btn-warning btn-lg" runat="server" UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" CssClass="tbn btn-primary btn-lg" runat="server" OnClick="SaveButton_Click" />
                </div>
        </div>
        </div>
    </div>
</asp:Content>
