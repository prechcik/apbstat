using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace Prechcik.ApbStat
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "217.144.197.78";
            database = "prechcik_apb";
            uid = "prechcik";
            password = "dawidek151";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();

            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool checkaccount(string user, string password)
        {
            if (this.OpenConnection() == true)
            {

                string query = "SELECT COUNT(*) FROM users WHERE (username='" + user + "' AND password='" + password + "')";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int Count = int.Parse(cmd.ExecuteScalar() + "");
                if (Count >= 1)
                {
                    this.CloseConnection();
                    return true;

                }
                else
                {
                    this.CloseConnection();
                    return false;

                }
            }
            else
            {
                return false;
            }

        }

        public bool insertData(string user, string kills, string assists, string medals)
        {
            if (this.OpenConnection() == true)
            {
                string query = "UPDATE users SET kills = kills + "+kills+", assists = assists + "+assists+", medals = medals + "+medals+" WHERE username='"+user+"'";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                return true;
            }
            else
            {
                MessageBox.Show("Could not connect to the database!");
                return false;
            }
        }

        

    }
}
