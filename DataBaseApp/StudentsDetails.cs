using DataBaseApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseApp
{
    public partial class StudentsDetails : Form
    {
        Student student = new Student();
        //2
        string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
        //3
        private SqlConnection sqlConnection;
             
        public StudentsDetails()
        {
            InitializeComponent();
            try
            {
                DepartmentComboBox.DataSource = GetDepartmentCombo();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            student.Name = nameTextBox.Text;
            student.RegNo = regNoTextBox.Text;
            student.Address = addressTextBox.Text;
            student.ContactNo = conatactNoTextBox.Text;
            student.DepartmentId = Convert.ToInt32(DepartmentComboBox.Text);

            bool isSaved = Add(student);
            if(isSaved)
                { 
                    MessageBox.Show("Saved"); 
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
       
        }

        private bool Add(Student student)
        {
            bool isSuccess = false;
            try
            {
                sqlConnection = new SqlConnection(connectionString);

                //4
                //string query = @"INSERT INTO Students(Name,Address,CityID,Mobile)VALUES ('" + name + "','" + address + "'," + cityId + ",'" + mobile + "')";
                string query = @"INSERT INTO Students(Name, RegNo , Address, ContactNo,DepartmentId)VALUES ('" + student.Name + "', '" + student.RegNo + "', '" + student.Address + "','" + student.ContactNo + "'," + student.DepartmentId + ")";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    //MessageBox.Show("Saved");
                    isSuccess = true;
                }
                else
                {
                    //MessageBox.Show("Not Saved");
                    isSuccess = false;
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

            return isSuccess;

        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = GetAll();
            showDataGridView.DataSource = dataTable;
        }

        private DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            try
            {

                sqlConnection = new SqlConnection(connectionString);

                //4
                string query = @"SELECT * FROM StudentsView";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);


                

                //if (!String.IsNullOrEmpty(dataTable.ToString()))
                //{
                //    showDataGridView.DataSource = dataTable;
                //}
                //else
                //{
                //    showDataGridView.DataSource = null;
                //}

                sqlConnection.Close();
                

            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
            return dataTable;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            bool isDeleted = Delete(student);
            if (isDeleted)
            {
                MessageBox.Show("Deleted");
            }
            else
            {
                MessageBox.Show("Not Deleted");
            }
        }

        private bool Delete(Student student)
        {
            bool isSuccess = false;

            student.Id = Convert.ToInt32(idTextBox.Text);
            try
            {

                 sqlConnection = new SqlConnection(connectionString);

                //4

                 string query = @"DELETE FROM Students WHERE ID =" + student.Id + " ";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    //MessageBox.Show("Deleted");
                    isSuccess = true;
                }
                else
                {
                    //MessageBox.Show("Not Deleted");
                    isSuccess = false;
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
            return isSuccess;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            bool isUpdate = Update(student);
            if(isUpdate)
            {
                MessageBox.Show("Updated");
            }
            else
            {
                MessageBox.Show("Not Updated");
            }


        }
        private bool Update(Student student)
        {
            bool isSuccess = false;
            //1
            student.Name = nameTextBox.Text;
            student.RegNo = regNoTextBox.Text;
            student.Address = addressTextBox.Text;
            student.ContactNo = conatactNoTextBox.Text;
            student.DepartmentId = Convert.ToInt32(DepartmentComboBox.Text);

            student.Id = Convert.ToInt32(idTextBox.Text);



            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4
                //string query = @"INSERT INTO Students(Name,Address,CityID,Mobile)VALUES ('" + name + "','" + address + "'," + cityId + ",'" + mobile + "')";
                string query = @"UPDATE Students SET Name='" + student.Name + "', Address='" + student.Address + "', ContactNo = '" + student.ContactNo + "', RegNo='" + student.RegNo + "', DepartmentId = " + student.DepartmentId + " WHERE ID = " + student.Id + "";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    //MessageBox.Show("Updated");
                    isSuccess = true;
                }
                else
                {
                    //MessageBox.Show("Not Updated");
                    isSuccess = false;
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
            return isSuccess;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {

        }

        private void DepartmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            student.DepartmentId = Convert.ToInt32(DepartmentComboBox.SelectedValue);
            StudentComboBox.DataSource = GetStudentCombo(student);
        }

        private DataTable GetDepartmentCombo()
        {
            sqlConnection = new SqlConnection(connectionString);

            //4
            string query = @"SELECT ID,Name FROM Departments";

            //5
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            //6
            sqlConnection.Open();
            //7
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }

        private DataTable GetStudentCombo(Student student)
        {
            sqlConnection = new SqlConnection(connectionString);

            //4
            string query = @"SELECT ID,Name FROM Students Where DepartmentId = "+student.DepartmentId+"";

            //5
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            //6
            sqlConnection.Open();
            //7
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
}
