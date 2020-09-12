using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    class Program
    {
        static readonly SqlConnection conn = new SqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString
        };
        //static readonly SqlConnection conn1 = new SqlConnection
        //{
        //    ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI;"
        //};
        //void InsertQ()
        //{
        //    try
        //    {
        //        conn.Open();

        //       // string insertString = @"insert into Authors (FirstName, LastName) values ('Dante', 'Aligeri')";
        //       // string insertString = @"insert into Books (AuthorId, Title, PRICE, PAGES) values ('3', 'No name', '700', '300')";
        //       //string insertString = @"insert into Books (AuthorId, Title, PRICE, PAGES) values ('5', 'The sea wolf', '600', '350')";
        //       // string insertString = @"insert into Books (AuthorId, Title, PRICE, PAGES) values ('7', 'La Divina Commedia', '900', '350')";
        //        SqlCommand cmd = new SqlCommand
        //        {
        //            Connection = conn,
        //            CommandText = insertString
        //        };
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine(exp.Message);

        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        void SelectQ()
        
        {
            try
            {
                conn.Open();
                var cmd = new SqlCommand("select * from Authors;select * from Books", conn);
                var rdr = cmd.ExecuteReader();

                do
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Console.Write(rdr.GetName(i) + "\t");
                    Console.WriteLine();
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                            Console.Write(rdr[i] + "\t");
                        Console.WriteLine();
                    }
                } while (rdr.NextResult()) ;


                    //        conn1.Open();
                    //        var cmd2 = new SqlCommand
                    //        {
                    //            CommandText = $"select Title from Books where AuthorId = {rdr["@p1"]} ",
                    //            Connection = conn1
                    //        };
                    //        cmd.Parameters.Add("@p1", SqlDbType.Int).Value = rdr["Id"];

                    //        var readB = cmd2.ExecuteReader();
                    //        while (readB.Read())
                    //            Console.WriteLine($"\t - {readB[0]}");
                    //        conn1.Close();
                    //        readB.Close();
                    //    }
                    //    rdr.Close();
                    //}
                    //    for (int i = 0; i < rdr.FieldCount; i++)
                    //        Console.Write(rdr.GetName(i) + "\t");
                    //Console.WriteLine();
                    //while (rdr.Read())
                    //{
                    //    for (int i = 0; i < rdr.FieldCount; i++)
                    //        Console.Write(rdr[i] + "\t");
                    //    Console.WriteLine();

                    //    conn1.Open();
                    //    var cmd2 = new SqlCommand
                    //    {
                    //        CommandText = $"select Title from Books where AuthorId = {rdr["@p1"]} ",
                    //        Connection = conn1
                    //    };
                    //    cmd.Parameters.Add("@p1", SqlDbType.Int).Value = rdr["Id"];

                    //    var readB = cmd2.ExecuteReader();
                    //    while (readB.Read())
                    //    Console.WriteLine($"\t - {readB[0]}");
                    //    conn1.Close();
                    //    readB.Close();
                    //}
                    rdr.Close();
                }
            catch (Exception exp)
            {
                Console.WriteLine(exp);

            }
            finally
            {
                conn.Close();

            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.SelectQ();
        }
    }
}
