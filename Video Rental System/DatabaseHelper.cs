using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Rental_System
{

    public class DatabaseHelper
    {
        private SqlConnection cnn;
        private String connetionString;


        public DatabaseHelper()
        {
            // connection string to change the database connection
            //connetionString = @"Data Source=LAPTOP-QGHMLVET\SQLEXPRESS (LAPTOP-QGHMLVET\nav);Initial Catalog=videorentals;Integrated Security=True;Pooling=False";

            connetionString = @"Data Source=LAPTOP-QGHMLVET\SQLEXPRESS;Integrated Security=True;Initial Catalog=videorentals;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            cnn = new SqlConnection(connetionString);
            cnn.Open();// to open the connection

        }
        public DatabaseHelper(String conString)
        {
            // connection string to change the database connection
            connetionString = conString;
            cnn = new SqlConnection(connetionString);
            cnn.Open();// to open the connection
        }
        private void OpenConnection()
        {
            cnn = new SqlConnection(connetionString);
            if (!(cnn.State == ConnectionState.Open))
            {
                cnn.Open();
            }
            else
            {

            }

        }
        public String DatabaseConnection()
        {
            if (cnn.State == ConnectionState.Open)
            {
                return "Connected";
            }
            else
            {
                return "Not Connected";
            }
        }
        private void CloseConnection()
        {
            if (cnn.State == ConnectionState.Open)
            {
                cnn.Close();
            }
        }
        public DataSet LoadAlldata()
        {
            OpenConnection();
            //to select from view
            SqlDataAdapter dac = new SqlDataAdapter("SELECT * FROM customer", cnn);
            // to select from tables
            SqlDataAdapter dam = new SqlDataAdapter("SELECT * FROM Movies", cnn);

            SqlDataAdapter darm = new SqlDataAdapter("SELECT * FROM RentedMovies", cnn);
            DataSet ds = new DataSet();
            dac.Fill(ds, "Customer");
            dam.Fill(ds, "Movies");
            darm.Fill(ds, "RentedMovies");
            CloseConnection();
            return ds;// to return the dataset
        }
        public DataSet LoadRentedOutData()
        {

            OpenConnection();
            SqlDataAdapter dac = new SqlDataAdapter("SELECT * FROM Customer", cnn);
            SqlDataAdapter darm = new SqlDataAdapter("SELECT * FROM RentedMovies where datereturned IS  NULL", cnn);
            SqlDataAdapter dam = new SqlDataAdapter("SELECT Movieid,Rating,Title,Year,Rental_Cost,Copies,Plot,Genre FROM Movies,RentedMovies where Movies.movieid=rentedmovies.MovieIDFK and  datereturned IS NULL ", cnn);
            DataSet ds = new DataSet();
            dac.Fill(ds, "Customer");
            dam.Fill(ds, "Movies");
            darm.Fill(ds, "RentedMovies");
            CloseConnection();

            return ds;// to return the datasets
        }
        /*
         To add new customer to databasse
             */
        public Boolean AddCustomer(String fn, String ln, String addr, String ph)
        {

            OpenConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "insert into Customer(FirstName,LastName,Address,Phone) values ('" + fn + "','" + ln + "','" + addr + "','" + ph + "')"; // set
            cmd.Connection = cnn;
            if (cmd.ExecuteNonQuery() == 1)
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }
        /*
           To add new movie to database
             */
        public Boolean Addmovie(String title, String rating, String year, double rentalcost, String copies, String plot, String genre)
        {

            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn; // set the connection to instance of SqlCommand
            cmd.CommandText = "insert into movies(title,rating,year,Rental_Cost,Copies,Plot,Genre) values ('" + title + "','" + rating + "','" + year + "'," + rentalcost + ",'" + copies + "','" + plot + "','" + genre + "' )"; // set
            if (cmd.ExecuteNonQuery() == 1)
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }

        /*
            To update customer record in database
         */

        public Boolean UpdateCustomer(string cid, String fn, String ln, String addr, String ph)
        {

            OpenConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "update Customer set FirstName='" + fn + "',LastName='" + ln + "',Address='" + addr + "',Phone='" + ph + "' where CustID=" + cid;
            cmd.Connection = cnn;
            if (cmd.ExecuteNonQuery() == 1)
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }
        /*
            To update movies record in database
         */

        public Boolean UpdateMovie(String mid, String title, String rating, String year, double rentalcost, String copies, String plot, String genre)
        {

            OpenConnection();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "update Movies set rating='" + rating + "',title='" + title + "',year='" + year + "',Rental_Cost=" + rentalcost + ",copies='" + copies + "',plot='" + plot + "',genre='" + genre + "' where movieid='" + mid + "'";
            cmd.Connection = cnn;
            if (cmd.ExecuteNonQuery() == 1)
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }


        /*
            Delete Customer
         */
        public Boolean DeleteCustomer(String cid)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from Customer where CustID=" + cid; // set
                cmd.Connection = cnn;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    CloseConnection();
                    return true;
                }
                CloseConnection();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }

        }

        /*
            Delete Movie
         */

        public Boolean DeleteMovie(String mid)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from movies where movieid=" + mid;
                cmd.Connection = cnn;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    CloseConnection();
                    return true;
                }
                CloseConnection();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }
        }

        
        /*
            To get rent charge
             */
        public int CalculateCharge(int year) {
            if ((DateTime.Now.Year - year) > 5)
            {
                return 5;
            }
            else
            {

                return 2;
            }
        }
        /*
         To find rent charge based on publication year
             */
        public double RentCharge(String mid)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT year FROM Movies where movieid=" + mid;
                Console.WriteLine("SELECT year FROM Movies where movieid=" + mid);
                cmd.Connection = cnn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        string year = (string)(reader["year"]);
                        return CalculateCharge(Int32.Parse(year));
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return 0;
            }



        }

        /*
         To place the rent request for the movie
             */
        public bool RentMovie(String cid, String mid)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into rentedMovies(MovieIDFK,CustIDFK,DateRented) values ('" + mid + "','" + cid + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss") + "')"; // set
                cmd.Connection = cnn;
                Console.WriteLine("insert into rentedMovies(MovieIDFK,CustIDFK,DateRented) values ('" + mid + "','" + cid + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss") + "')");
                if (cmd.ExecuteNonQuery() == 1)
                {
                    CloseConnection();
                    return true;
                }
                CloseConnection();
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }

        }
        /*
         To add the retrun date of the movie issued
             */
        public bool ReturnMovie(String rmid)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update RentedMovies set DateReturned='"+DateTime.Now.ToString("MM -dd-yyyy hh:mm:ss") + "' where RMID="+rmid; // set
                cmd.Connection = cnn;
               
                if (cmd.ExecuteNonQuery() == 1)
                {
                    CloseConnection();
                    return true;
                }
                CloseConnection();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return false;
            }

        }
    }
    }
