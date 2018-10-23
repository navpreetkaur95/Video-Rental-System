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


namespace Video_Rental_System
{
    public partial class Form1 : Form
    {
        DatabaseHelper dh;
        public Form1()
        {
            InitializeComponent();

            dh = new DatabaseHelper();
            loaddata();
        }
        private void loaddata()
        {
            if (rentedRb.Checked==true) {
               
                DataSet ds = dh.LoadAlldata();
                dataGridView1.DataSource = ds.Tables["Customer"].DefaultView;
                dataGridView2.DataSource = ds.Tables["Movies"].DefaultView;
                dataGridView3.DataSource = ds.Tables["RentedMovies"].DefaultView;
                
            }
            else
            {

                DataSet ds = dh.LoadRentedOutData();
                dataGridView1.DataSource = ds.Tables["Customer"].DefaultView;
                dataGridView2.DataSource = ds.Tables["Movies"].DefaultView;
                dataGridView3.DataSource = ds.Tables["RentedMovies"].DefaultView;
            }
        }
        private void clearcustomer()
        {
            txtcid.Text = "";
            txtfn.Text = "";
            txtln.Text = "";
            txtph.Text = "";
            txtaddr.Text = "";
        }
        private void clearmovies()
        {
            txtmid.Text = "";
            txtplot.Text = "";
            txttitle.Text = "";
            txtrating.Text = "";
            txtrc.Text = "";
            txtcopy.Text = "";
            txtgenre.Text = "";
            txtyear.Text = "";

             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcid.Text != "")
                {


                    bool result = dh.UpdateCustomer(txtcid.Text, txtfn.Text, txtln.Text, txtaddr.Text, txtph.Text);
                    if (result)
                    {

                        MessageBox.Show("Customer Updated");
                    }
                    else
                    {
                        MessageBox.Show("Some Error");
                    }
                    clearcustomer();
                  
                    loaddata();

                }
                else
                {
                    MessageBox.Show("Please select customer to update");
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error: "+ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {

                bool result=dh.AddCustomer(txtfn.Text, txtln.Text, txtaddr.Text, txtph.Text);
                if (result)
                {
                    MessageBox.Show("Customer Added");
                }
                else{
                    MessageBox.Show("Something went wrong");
                }
                clearcustomer();
                loaddata();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            txtcid.Text = row.Cells[0].Value.ToString();
            txtfn.Text = row.Cells[1].Value.ToString();
            txtln.Text = row.Cells[2].Value.ToString();
            txtaddr.Text = row.Cells[3].Value.ToString();
            txtph.Text = row.Cells[4].Value.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtcid.Text != "")
            {
                bool result = dh.DeleteCustomer(txtcid.Text);
                if (result)
                {
                    MessageBox.Show("Customer Deleted");
                }
                else {
                    MessageBox.Show("Some Error has occured");
                }
                clearcustomer();
                loaddata();
                clearcustomer();
            }
            else
            {
                MessageBox.Show("Please select customer to delete");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = dh.Addmovie(txttitle.Text, txtrating.Text, txtyear.Text, Double.Parse(txtrc.Text), txtcopy.Text, txtplot.Text, txtgenre.Text);
                if (result)
                {
                    MessageBox.Show("Movie Added");
                }
                else
                {
                    MessageBox.Show("Some error has occured");
                }
                clearmovies();
                loaddata();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtmid.Text != "")
                {
                    bool result = dh.UpdateMovie(txtmid.Text, txttitle.Text, txtrating.Text, txtyear.Text, Double.Parse(txtrc.Text), txtcopy.Text, txtplot.Text, txtgenre.Text);
                    if (result) {
                        MessageBox.Show("Movie Updated");
                    }
                    else
                    {
                        MessageBox.Show("Some error has occured");
                    }
                    clearmovies();
                    loaddata();

                }
                else
                {
                    MessageBox.Show("Please select customer to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtmid.Text != "")
            {
                bool result = dh.DeleteMovie(txtmid.Text);
                if (result)
                {
                    MessageBox.Show("Movie Deleted");
                }
                else
                {
                    MessageBox.Show("Some Error has occured");
                }
                clearcustomer();
                loaddata();
                clearcustomer();
            }
            else
            {
                MessageBox.Show("Please select movie to delete");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];
            txtmid.Text = row.Cells[0].Value.ToString();
            txtrating.Text = row.Cells[1].Value.ToString();
            txttitle.Text = row.Cells[2].Value.ToString();
            txtyear.Text = row.Cells[3].Value.ToString();
            txtrc.Text = row.Cells[4].Value.ToString();
            txtcopy.Text = row.Cells[5].Value.ToString();
            txtgenre.Text = row.Cells[7].Value.ToString();
            txtplot.Text = row.Cells[6].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtrmid.Text.Trim() != "")
            {
                bool result = dh.ReturnMovie(txtrmid.Text);
                if (result)
                {
                    MessageBox.Show("Movie Returned");
                }
                else {
                    MessageBox.Show("Some error has occured");
                }
                loaddata();

            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            loaddata();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            loaddata();
      
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txtcid.Text == "")
            {
                MessageBox.Show("Select customer who is renting the movie");
                return;

            }
            if (txtmid.Text == "")
            {
                MessageBox.Show("Select the movie to be rented");
                return;
            }

            double rentCharge = dh.RentCharge(txtmid.Text);
            if (rentCharge == 0)
            {
                MessageBox.Show("Error in calculating rent charge cannot proceed further");
                return;
            }
            DialogResult result = MessageBox.Show("Rent Charge is $" + rentCharge, "Warning",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool res = dh.RentMovie(txtcid.Text, txtmid.Text);
                if (res)
                {
                    MessageBox.Show("Movie Issued Successfully");
                }
                else
                {
                    MessageBox.Show("There was some error");
                }
                loaddata();
            }
        }
    }
}
