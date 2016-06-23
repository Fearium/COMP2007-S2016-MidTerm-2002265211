using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_2002265211.Models;
using System.Web.ModelBinding;

/* 
 * @author: Jesse Baril (200226521)
 * @purpose: Enterprise Computing Midterm assignment
 * @Date: 2016/06/03
 * @Time: 5:50 PM
 * 
 */

namespace COMP2007_S2016_MidTerm_2002265211
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method gets the Todo data from the database
         * </summary>
         * @method GetDepartments
         * @return {void}
         * */
        protected void GetTodos()
        {
            // Populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // Connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // Populate a Todo object instance with the TodoID from the URL Parameter
                Todo updatedTodo = (from todo in db.Todos
                                                where todo.TodoID == TodoID
                                                select todo).FirstOrDefault();

                // Map the Todo properties to the form controls
                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    NotesTextBox.Text = updatedTodo.TodoNotes;
                    DoneCheckBox.Checked = updatedTodo.Completed ?? false;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the Todos page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {

            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // Use the Todo model to create a new Todo object and
                // Save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // URL has a TodoID in it
                {
                    // Get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // Get the current Todo from EF DB
                    newTodo = (from todo in db.Todos
                                     where todo.TodoID == TodoID
                                     select todo).FirstOrDefault();
                }

                // Add form data to the new Todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = NotesTextBox.Text;
                newTodo.Completed = DoneCheckBox.Checked;

                // Use LINQ to ADO.NET to add / insert new Todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // Save changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated Todos page
                Response.Redirect("~/TodoList.aspx");
            }
        }

        protected void DoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // Use the Todo model to create a new Todo object and
                // Save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // URL has a TodoID in it
                {
                    // Get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // Get the current Todo from EF DB
                    newTodo = (from todo in db.Todos
                               where todo.TodoID == TodoID
                               select todo).FirstOrDefault();
                }

                // Add form data to the new Todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = NotesTextBox.Text;
                newTodo.Completed = DoneCheckBox.Checked;

                // Use LINQ to ADO.NET to add / insert new Todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // Save changes - also updates and inserts
                db.SaveChanges();
            }
        }
    }
}