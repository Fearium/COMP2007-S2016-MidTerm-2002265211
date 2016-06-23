<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_2002265211.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todo</a>

                <div class="form-group">
                    <label class="control-label" for="TodoTotal">Total Todos:</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoTotal" placeholder="Total" ReadOnly="true" required="false"></asp:TextBox>
                </div>

                <div>
                    <label for="PageSizeDropDownList"></label>
                    <asp:DropDownList runat="server" ID="PageSizeDropDownList" AutoPostBack="true"
                        CssClass="btn btn-default bt-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="10000" />
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                    </asp:DropDownList>
                </div>

                <asp:GridView ID="TodoGridView" runat="server" CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10000" onrowcreated="TodoGridView_RowCreated" OnPageIndexChanging="TodoGridView_PageIndexChanging"
                    DataKeyNames="TodoID" OnRowDeleting="TodoGridView_RowDeleting" AllowSorting="true"
                    OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound" PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Notes" Visible="true" SortExpression="TodoNotes" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="ListItemCompleted" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("Completed")) %>' 
                                    OnCheckedChanged="ListItemCompleted_CheckedChanged" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-encil-square-o fa-lg'></i> Edit"
                            NavigateUrl="TodoDetails.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" DataNavigateUrlFields="TodoID"
                            runat="server" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true" ButtonType="Link"
                            ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
