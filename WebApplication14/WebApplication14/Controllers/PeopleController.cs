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


        List<Person> listPerson = new List<Person>();



        string path = "data_table.db";
        //string cs = @"Data Source=C:\Users\ramunasr\Documents\visual studio 2015\Projects\WindowsFormsApplicationSQLite001\WindowsFormsApplicationSQLite001\bin\Debug\data_table.db";
        string cs = @"Data Source=C:\VisualStudioProjects\WindowsFormsApplicationSQLite001\WindowsFormsApplicationSQLite001\bin\Debug\data_table.db"; //data_table.db

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
        [Route("api/People/GetHistory/{userid:int}/{laikas:int}/{duomenys}")]
        //[Route("api/People/GetFirstNames/{userid:int}/{laikas:int}/{duomenys:int}")]
        //[Route("api/People/GetFirstNames/{userid:int}/{laikas:int}/{duomenys:int}")]
        [HttpGet]
       // public string GetFirstNames(int erid, string age, string gautaa_eilutee)
         public string GetHistory(int userid, int laikas, string duomenys)
        //public List<string> GetFirstNames(int userid, int age)
        {




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
            return userid.ToString() + "    " + laikas.ToString() + "    " + duomenys.ToString();
        }

        // GET: api/People
        //public List<Person> Get()
        public List<Person> Get()
        {
            //int count = 19;



            SelectFromDb();
     


            return listPerson;
            


            //return people;
            //return count;
        }

        // GET: api/People/5
        public Person Get(int id)
        {
            return people.Where(x => x.Id == id).FirstOrDefault();
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
            UpdateDbRecord(putt.Id, putt.FirstName, putt.Comment);

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

            WriteToDb(val.Id, val.FirstName, val.Comment);

            //irasymasDB(val.Id, val.FirstName, val.LastName);

        }

        //WriteToDb
        //CreateDb
        //void WriteToDb(int gautasid, string gautas_laikas, string gauta_eilute)

        void WriteToDb(int gautasid, string gautas_laikas, string gauta_eilute)
        {


            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES('vardas','id_numeris')";
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES(" + gautas_laikas + ", '" + gauta_eilute + "', '" + gauta_eilute + "')";
                //cmd.CommandText = "INSERT INTO test(name,id) VALUES('" + gautas_laikas + "', '" + gauta_eilute + "')";
                cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + gautas_laikas + "', '" + gauta_eilute + "')";
                cmd.ExecuteNonQuery();
                //

            }
            catch (Exception)
            {
                Console.WriteLine("cannot insert data");
                return;
            }
           
            cmd.Dispose();
            con.Close();
            //INSERT PABAIGA

        }

        int SelectFromDb()
        {
            int count0 = 0;

            var con = new SQLiteConnection(cs);
            con.Open();

            //string stm = "SELECT * FROM test";
            //var cmd = new SQLiteCommand(stm, con);
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM test";

            SQLiteDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                count0++;
                listPerson.Add(new Person { FirstName = reader.GetString(1), Comment = reader.GetString(2), Id = reader.GetInt16(0) });

            }

            return count0;
        

        }

        void UpdateDbRecord(int gautasid, string gautas_laikas, string gauta_eilute)
        {

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
                //cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + gautas_laikas + "', '" +  + "')";
                //cmd.CommandText = "UPDATE test SET name = " + gautas_laikas + "', '" + gauta_eilute + " WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"' WHERE id = " + gautasid.ToString();
                cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"', surname = '"+ gauta_eilute + "'" + " WHERE id = " + gautasid.ToString();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\nupdate vyksta\n\n");


            }
            catch (Exception)
            {
                Console.WriteLine("cannot update data");
                return;
            }

            cmd.Dispose();
            con.Close();


        }


        void DeleteFromTable(int gautasid)
        {

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
                //cmd.CommandText = "INSERT INTO test(name,surname) VALUES('" + gautas_laikas + "', '" +  + "')";
                //cmd.CommandText = "UPDATE test SET name = " + gautas_laikas + "', '" + gauta_eilute + " WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "UPDATE test SET name = " + "'" + gautas_laikas +"' WHERE id = " + gautasid.ToString();
                cmd.CommandText = "DELETE FROM test WHERE id = " + gautasid.ToString();
                //cmd.CommandText = "DELETE FROM test WHERE id = 4";

                cmd.ExecuteNonQuery();
                Console.WriteLine("\n\n  delete vyksta \n\n");


            }
            catch (Exception)
            {
                Console.WriteLine("cannot update data");
                return;
            }

            cmd.Dispose();
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
