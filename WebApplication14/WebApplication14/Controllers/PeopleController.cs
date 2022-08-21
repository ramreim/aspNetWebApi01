using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication14.Models;
using System.Data;


using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;

namespace WebApplication14.Controllers
{

    /// <summary>
    /// This is where I give you all the information abaut my peeps.
    /// </summary>

    public class PeopleController : ApiController
    {
        List<Person> people = new List<Person>();


        List<string> sarasas = new List<string>();


        List<Person> listCustomer = new List<Person>();

        List<Person> listCustomerById = new List<Person>();

        List<History> historyList = new List<History>();




        string path = "data_table.db";
        //string cs = @"Data Source=C:\Users\ramunasr\Documents\visual studio 2015\Projects\WindowsFormsApplicationSQLite001\WindowsFormsApplicationSQLite001\bin\Debug\data_table.db";
        string cs = @"Data Source=C:\VisualStudioProjects\WindowsFormsApplicationSQLite001\WindowsFormsApplicationSQLite001\bin\Debug\data_table.db;foreign keys=true;"; //data_table.db

        //string cs = "Data Source=:memory:Version=3;New=True";


        //string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;



        public PeopleController()
        {
            people.Add(new Person { FirstName = "vardas1", Comment = "pavarde", Id = 1 });
            people.Add(new Person { FirstName = "vardas2", Comment = "pavarde", Id = 2 });
            people.Add(new Person { FirstName = "vardas3", Comment = "pavarde", Id = 3 });



      

            

            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;


        }



        /// <summary>
        /// Gets a list of the first of all users.
        /// </summary>
        /// <param name="userid">The unique identifier for this person .</param>
        /// <param name="age">We want to know how old they are.</param>
        /// <returns>A list of first names ... duh</returns>

        //[Route("api/Peeps/GetFirstNames")]
        //[Route("api/People/GetFirstNames")]
        //[Route("api/People/GetHistory/{userid:int}/{laikas:int}/{duomenys}")]
        //[Route("api/People/GetFirstNames/{userid:int}/{laikas:int}/{duomenys:int}")]
        //[Route("api/People/GetFirstNames/{userid:int}/{laikas:int}/{duomenys:int}")]

        [Route("api/People/GetHistory/{userid:int}")]
        [HttpGet]
       // public string GetFirstNames(int erid, string age, string gautaa_eilutee)
        public List<History>GetHistory(int userid)
        //public string GetHistory(int userid, int laikas, string duomenys)
        //public List<string> GetFirstNames(int userid, int age)
        {
           int gautass =  SelectHistoryFromDb(userid);

            int gautas2 = gautass;

            // irasymasDB(erid, age, gautaa_eilutee);
            //irasymasDB(10, "laikass", "gautaa_eilutee");


            // people.Add(new Person { FirstName = "Tim4", LastName = "Corey4", Id = 4 });

            /*List<string> output = new List<string>();

            
            foreach (var p in people)
            {
                output.Add(p.FirstName);
            }
            return output;
            */
            return historyList;
        }

        // GET: api/People
        //public List<Person> Get()
        public List<Person> Get()
        {
            //int count = 19;



            SelectFromDb();
     


            return listCustomer;
            


            //return people;
            //return count;
        }

        // GET: api/People/5
        //public Person Get(int id)
          public List<Person> Get(int id)
         {

            SelectFromDbById(id);

            return listCustomerById;
         }

        // POST: api/People
        /*public void Post(Person val)
        {
            people.Add(val);
        }*/







        // PUT: api/People/5
        //[Route("api/People/GetFirstNames/{userid:int}/{age}/{gautaa_eilutee}")]
        //[Route("api/People/GetFirstNames/{userid:int}/{laikas:int}/{duomenys:int}")]
       // [HttpGet]
        //public string Put(int id, [FromBody]string Firstname)
        public string Put(Person putt)
        {
            UpdateDbRecord(putt.Id, putt.FirstName, putt.Age, putt.Comment);

            return putt.Id +  "  " + putt.FirstName + "   " + putt.Comment;
        }

        // DELETE: api/People/5
        public string Delete(Person dell)
        {

            DeleteFromTable(dell.Id);
            return "delete vyksta";

        }
        //SqlConnection conn = new SqlConnection("Server=192.168.0.105;Database=uvsDB;Integrated Security=True;");



        public void Post(Person val)
        {
            //people.Add(val);
            //WriteToDb(5, "eilute1", "eilute2");

            WriteToDb(val.Id, val.FirstName, val.Age, val.Comment);


            //irasymasDB(val.Id, val.FirstName, val.LastName);

        }

        //WriteToDb
        //CreateDb
        //void WriteToDb(int gautasid, string gautas_laikas, string gauta_eilute)

        int WriteToDb(int gautasid, string Namee, int Age, string Commentt)
        {
            int lastCustomerId = 1;


            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd0 = new SQLiteCommand(con);
            var cmd1 = new SQLiteCommand(con);
            var cmd2 = new SQLiteCommand(con);

            try
            {
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES('vardas','id_numeris')";
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES(" + gautas_laikas + ", '" + gauta_eilute + "', '" + gauta_eilute + "')";
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES('" + gautas_laikas + "', '" + gauta_eilute + "')";
                //cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + Namee + "', '" + Commentt + "')";
                //cmd.CommandText = "INSERT INTO CustomerTable(Name, Age, Comment) VALUES('vardas01', 18, 'komentaras 001')";
                cmd0.CommandText = "INSERT INTO CustomerTable(Name, Age, Comment) VALUES('" + Namee + "', "+ Age +", '" + Commentt + "')";

                cmd1.CommandText = "SELECT * FROM CustomerTable ORDER BY CustomerId DESC LIMIT 1";
                //cmd1.CommandText = "SELECT * FROM CustomerTable";

                


                cmd0.ExecuteNonQuery();
                                
                SQLiteDataReader reader002 = cmd1.ExecuteReader();
                                
                while (reader002.Read())
                {
                    lastCustomerId = reader002.GetInt16(0);
                }

                string DateTimee = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


                //cmd2.CommandText = "INSERT INTO HistoryTable(State, DateTime, CustomerHistoryId) VALUES('Priregistruotas', '2022-12-22 19:27:55.765',  " + lastId22 + ")";
                cmd2.CommandText = "INSERT INTO HistoryTable(State, DateTime, CustomerHistoryId) VALUES('Priregistruotas', '" + DateTimee + "',  " + lastCustomerId + ")";

                cmd2.ExecuteNonQuery();

                cmd0.Dispose();
                cmd1.Dispose();
                cmd2.Dispose();

                con.Close();


                GC.Collect();
                GC.WaitForPendingFinalizers();

                
                return 26;

            }
            catch (Exception)
            {
                Console.WriteLine("cannot insert data");
                return 55;
            }
           
   
            //INSERT PABAIGA

        }


        int selectLastCustomerId()
        {

            int paskutinis3 = 0;
            int tarpinis = 0;


            /*


            cmd.CommandText = "SELECT * FROM CustomerTable ORDER BY CustomerId DESC LIMIT 1";

            SQLiteDataReader reader = cmd.ExecuteReader();

            lastId = reader.GetInt16(0);
            Console.WriteLine(cmd.CommandText);

            while (reader.Read())
            {

                lastId = reader.GetInt16(0);
            }
            */
            //int count0 = 0;

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            con.Open();

            //string stm = "SELECT * FROM test";
            //var cmd = new SQLiteCommand(stm, con);

            //cmd.CommandText = "SELECT * FROM CustomerTable";

            cmd.CommandText = "SELECT * FROM CustomerTable ORDER BY CustomerId DESC LIMIT 1";
            



            SQLiteDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                //count0++;
                //listCustomer.Add(new Person { Id = reader.GetInt16(0), FirstName = reader.GetString(1), Age = reader.GetInt16(2), Comment = reader.GetString(3) });
                paskutinis3 = reader.GetInt16(0);
                tarpinis = paskutinis3;

            }

            return paskutinis3;












            return 0;

        }


        int SelectFromDb()
        {
            int count0 = 0;

            var con = new SQLiteConnection(cs);
            con.Open();

            //string stm = "SELECT * FROM test";
            //var cmd = new SQLiteCommand(stm, con);
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM CustomerTable";

            SQLiteDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                count0++;
                listCustomer.Add(new Person { Id = reader.GetInt16(0), FirstName = reader.GetString(1), Age = reader.GetInt16(2), Comment = reader.GetString(3) });

            }

            cmd.Dispose();
            con.Close();

            return count0;       

        }




        int SelectFromDbById(int getId)
        {
            int count0 = 0;

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM CustomerTable WHERE CustomerId = " + getId;

            SQLiteDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                count0++;
                listCustomerById.Add(new Person { Id = reader.GetInt16(0), FirstName = reader.GetString(1), Age = reader.GetInt16(2), Comment = reader.GetString(3) });

            }

            cmd.Dispose();
            con.Close();

            return count0;

        }




        int SelectHistoryFromDb(int getUserId)
        {
            int maksimalus = 0;

            var con = new SQLiteConnection(cs);
            con.Open();

            //string stm = "SELECT * FROM test";
            //var cmd = new SQLiteCommand(stm, con);
            var cmd = new SQLiteCommand(con);
            //cmd.CommandText = "SELECT * FROM HistoryTable WHERE CustomerHistoryId =" + getUserId;

            //cmd.CommandText = "SELECT * FROM HistoryTable INNER JOIN CustomerTable ON CustomerTable.CustomerId = HistoryTable.CustomerHistoryId";


            cmd.CommandText = "SELECT * FROM HistoryTable INNER JOIN CustomerTable ON CustomerTable.CustomerId = HistoryTable.CustomerHistoryId WHERE CustomerHistoryId =" + getUserId;



            //cmd.CommandText = "SELECT MAX(CustomerID) FROM CustomerTable";

            //cmd.CommandText = "SELECT * FROM HistoryTable WHERE CustomerHistoryId =" + getUserId;

            SQLiteDataReader reader = cmd.ExecuteReader();

            

            while (reader.Read())
            {
                historyList.Add(new History { EventId = reader.GetInt16(0), Status = reader.GetString(1), DateTimee = reader.GetString(2), CustomerId = reader.GetInt16(3), FirstName = reader.GetString(5)});

                //var kazkas = reader.GetString(5);
                //var kazkas2 = kazkas;
                //maksimalus = reader.GetInt16(0);

            }
            //                listCustomer.Add(new Person { Id = reader.GetInt16(0), FirstName = reader.GetString(1), Age = reader.GetInt16(2), Comment = reader.GetString(3) });

            //e(State, DateTime, CustomerHistoryId) 

            return maksimalus;


        }


        void UpdateDbRecord(int getId, string getName, int getAge, string getComment)
        {

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);
            var cmdChangeStatus = new SQLiteCommand(con);

            string DateTimee = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


            try
            {
                //cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + gautas_laikas + "', '" +  + "')";
                //cmd.CommandText = "UPDATE test SET name = " + gautas_laikas + "', '" + gauta_eilute + " WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"' WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"', surname = '"+ gauta_eilute + "'" + " WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE CustomerTable SET Name = 'pakeistas3', Age = 99, Comment = 'komennn' WHERE CustomerId = " + 7;
                cmd.CommandText = "UPDATE CustomerTable SET Name = '" + getName + "', Age = " + getAge + ", Comment = '" + getComment + "' WHERE CustomerId = " + getId.ToString();
                cmdChangeStatus.CommandText = "INSERT INTO HistoryTable(State, DateTime, CustomerHistoryId) VALUES('Redaguotas', '" + DateTimee + "',  " + getId + ")";



                cmd.ExecuteNonQuery();
                cmdChangeStatus.ExecuteNonQuery();

                Console.WriteLine("\n\nupdate vyksta\n\n");


            }
            catch (Exception)
            {
                Console.WriteLine("cannot update data");
                return;
            }

            cmd.Dispose();
            cmdChangeStatus.Dispose();
            con.Close();


        }


        void DeleteFromTable(int getCustomerId)
        {

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);
            var cmdChangeStatus = new SQLiteCommand(con);


            string DateTimee = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


            try
            {
                //cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + gautas_laikas + "', '" +  + "')";
                //cmd.CommandText = "UPDATE test SET name = " + gautas_laikas + "', '" + gauta_eilute + " WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"' WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "DELETE FROM test WHERE id = " + gautasid.ToString();

                cmd.CommandText = "DELETE FROM CustomerTable WHERE CustomerId = " + getCustomerId.ToString();
                cmdChangeStatus.CommandText = "INSERT INTO HistoryTable(State, DateTime, CustomerHistoryId) VALUES('Istrintas', '" + DateTimee + "',  " + getCustomerId + ")";




                cmd.ExecuteNonQuery();
                cmdChangeStatus.ExecuteNonQuery();
                Console.WriteLine("\n\n  deleting \n\n");


            }
            catch (Exception)
            {
                Console.WriteLine("cannot update data");
                return;
            }

            cmd.Dispose();
            cmdChangeStatus.Dispose();
            con.Close();



        }



        static void irasymasDB(int gautasid, string gautas_laikas, string gauta_eilute)
        {


            SqlConnection conn = new SqlConnection("Server=192.168.0.105;Database=uvsDB;Integrated Security=True;");


            conn.Open();


            //INSERT PRADZIA

            //INSERT INTO table_name(column1, column2, column3, ...) VALUES(value1, value2, value3, ...);
            //sakinys = "INSERT INTO salestable(ThreadID, Timee, Data) VALUES('VARDAS0" + "hjk" + "')";

            string sakinys = "";
            sakinys = "INSERT INTO sakosTable(ThreadID, Timee, Data) VALUES(" + gautasid + ", '" + gautas_laikas + "', '" + gauta_eilute + "')";


            SqlCommand cmd = new SqlCommand(sakinys, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();


            conn.Close();
            //INSERT PABAIGA



        }
    }
}
