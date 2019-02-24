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
    public partial class StudentInformationUi : Form
    {
        public StudentInformationUi()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //1
            string name = nameTextBox.Text;
            string regNo = regNoTextBox.Text;
            string address = addressTextBox.Text;
            string contactNo = conatactNoTextBox.Text;
            int departmentId = Convert.ToInt32(departmentIdTextBox.Text);

            if (Exists(regNo))
            {
                MessageBox.Show("Exists");
                return;
            }


            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                
                //4
                //string query = @"INSERT INTO Students(Name,Address,CityID,Mobile)VALUES ('" + name + "','" + address + "'," + cityId + ",'" + mobile + "')";
                string query = @"INSERT INTO Students(Name, RegNo , Address, ContactNo,DepartmentId)VALUES ('" + name + "', '" + regNo + "', '" + address + "','" + contactNo +"',"+ departmentId+ ")";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted>0)
                {
                    MessageBox.Show("Saved");
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }


        }

        private void ShowButton_Click(object sender, EventArgs e)
        {

            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4
                string query = @"SELECT * FROM StudentsView";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);


                showDataGridView.DataSource = dataTable;

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

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idTextBox.Text);
            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4

                string query = @"DELETE FROM Students WHERE ID ="+id+" ";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Deleted");
                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            //1
            string name = nameTextBox.Text;
            string regNo = regNoTextBox.Text;
            string address = addressTextBox.Text;
            string contactNo = conatactNoTextBox.Text;
            int departmentId = Convert.ToInt32(departmentIdTextBox.Text);

            int id = Convert.ToInt32(idTextBox.Text);



            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4
                //string query = @"INSERT INTO Students(Name,Address,CityID,Mobile)VALUES ('" + name + "','" + address + "'," + cityId + ",'" + mobile + "')";
                string query = @"UPDATE Students SET Name='"+ name +"', Address='"+address+"', ContactNo = '"+contactNo+"', RegNo='"+regNo+"', DepartmentId = "+ departmentId +" WHERE ID = "+id+"";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Updated");
                }
                else
                {
                    MessageBox.Show("Not Updated");
                }

                //8
                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

        }

        private void FindButton_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(idTextBox.Text);
            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4
                string query = @"SELECT * FROM StudentsView WHERE ID = " + id + "";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);


                showDataGridView.DataSource = dataTable;

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
        }

        private bool Exists(string regNo)
        {

            bool isExists = false;

            try
            {

                //2
                string connectionString = @"Server=PC-301-13\SQLEXPRESS; Database=StudentsDb; Integrated Security = true";
                //3
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                //4
                string query = @"SELECT * FROM StudentsView WHERE RegNo = '" + regNo + "'";

                //5
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                //6
                sqlConnection.Open();
                //7
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                string data = "";
                if (sqlDataReader.Read())
                {
                    data = sqlDataReader["ID"].ToString();
                }

                if (!String.IsNullOrEmpty(data))
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }


                sqlConnection.Close();


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

            return isExists;
        }
    }
}
