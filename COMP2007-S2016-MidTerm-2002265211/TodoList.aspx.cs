using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_2002265211.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/* 
 * @author: Jesse Baril (200226521)
 * @purpose: Enterprise Computing Midterm assignment
 * @Date: 2016/06/03
 * @Time: 5:30 PM
 * 
 */ 

namespace COMP2007_S2016_MidTerm_2002265211
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // If loading page for the first time, populate the Todo grid, if not don't repopulate
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; // Default sort column
                Session["SortDirection"] = "ASC"; // Default sort direction

                // Get the Department data
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method gets the Todo data from the database
         * </summary>
         * @method GetTodos
         * @return {void}
         * */
        protected void GetTodos()
        {
            // Connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                // Query the Todo table using EF and LINQ
                var Todos = (from allTodos in db.Todos select allTodos);

                // Bind results to gridview
                TodoGridView.DataSource = Todos.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }
        }

        /**
         * <summary>
         * This method changes the amount of Todos displayed per page when a different index is selected in the dropdown
         * </summary>
         * @method PageSizeDropDownList_SelectedIndexChanged
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         * */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // Refresh
            this.GetTodos();
        }

        /**
         * <summary>
         * This event handler allows pagination for the TodoList page
         * </summary>
         * @method TodoGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         * */
        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // Refresh the grid
            this.GetTodos();
        }

        /**
         * <summary>
         * This event handler deletes a Todo from the databse using EF
         * </summary>
         * @method TodoGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewDeleteEventArgs}
         * @returns {void}
         * */
        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Store which row was clicked
            int selectedRow = e.RowIndex;

            // Get the selected TodoID using the grids datakey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // Use ef to find the selelcted Todo and delete it
            using (TodoConnection db = new TodoConnection())
            {
                // Create object of the Todo class and store the query string inside of it
                Todo deletedTodo = (from TodoRecords in db.Todos
                                                where TodoRecords.TodoID == TodoID
                                                select TodoRecords).FirstOrDefault();

                // Remove the selected Todo from the db
                db.Todos.Remove(deletedTodo);

                // Save db changes
                db.SaveChanges();

                // Refresh gridview
                this.GetTodos();

            }
        }

        /**
         * <summary>
         * This even handles sorting
         * </summary>
         * @method TodoGridView_Sorting
         * @param
         * @returns {void}
         * */
        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the grid
            this.GetTodos();

            // Toggle the direction from ASC and DSC
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DSC" : "ASC";
        }

        /**
         * <summary>
         * This method adds carots to sorting
         * </summary>
         * @method TodoGridView_RowDataBound
         * @param {object} sender
         * @param {GridViewRowEventArgs} e
         * @returns {void}
         * */
        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)// If header row is clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = "<i class='fa fa-caret-up fa-lg'> </i>";
                            }
                            else
                            {
                                linkButton.Text = "<i class='fa fa-caret-down fa-lg'> </i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                    }
                }
            }

        }

        protected void ListItemCompleted_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}