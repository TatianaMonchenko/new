using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static readonly SqlConnection conn = new SqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString
        };
        string connectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }
        DataSet set;
        SqlDataAdapter da;
        //SqlCommandBuilder cmd;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                //Filter = "Graphics File|*.bmp;*.gif;*.jpg; *.png";
            };
            
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _fileName = openFile.FileName;
                    var bytes = CreateCopyImage(_fileName);
                        conn.Open();
                    if(toolStripTextBox1.Text?.Length)
                        if (index == -1) return;
                    comm.Parameters.Add("@bookid",SqlDbType.Int).Value = index;
                    comm.Parameters.Add("@name",SqlDbType.NVarChar, 255).
                    Value = fileName;
                    comm.Parameters.Add("@picture",SqlDbType.Image, bytes.Length).
                    Value = bytes;
                    comm.ExecuteNonQuery();
                    conn.Close();


                }
                private byte[] CreateCopyImage(string fileName)
                {
                    var img = Image.FromFile(fileName);
                    int maxWidth = 300;
                    int maxHeight = 300;
                    int newWidth = (int)(img.Width * (double)maxWidth / img.Width);
                    int newHeight = (int)(img.Height * (double)maxHeight / img.Height);

                    var imageRation = new Bitmap(newWidth, newHeight);
                    
                    var g = Graphics.FromImage(imageRation);
                    g.DrawImage(img, 0, 0, newWidth, newHeight);
                    using (var stream = new MemoryStream())
                    using (var reader = new BinaryReader(stream))
                    {
                        imageRation.Save(stream, Image)
                    }
                    //поток для ввода|вывода байт из памяти
                   
                    return buf;
                }
                private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        //    private void button1_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            SqlConnection conn = new SqlConnection(connectionString);
        //            set = new DataSet();
        //            da = new SqlDataAdapter(textBox1.Text, conn);
        //            var cmd = new SqlCommandBuilder(da);

        //            //SqlCommand UpdateCmd = new SqlCommand("Update Books set Price = @pPrice where id = @pId", conn);
        //            //UpdateCmd.Parameters.Add(new SqlParameter("@pPrice", SqlDbType.Int));
        //            //UpdateCmd.Parameters["@pPrice"].SourceVersion = DataRowVersion.Current;
        //            //UpdateCmd.Parameters["@pPrice"].SourceColumn = "Price";
        //            //UpdateCmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
        //            //UpdateCmd.Parameters["@pId"].SourceVersion = DataRowVersion.Original;
        //            //UpdateCmd.Parameters["@pId"].SourceColumn = "id";

        //            SqlCommand updateCommand = new SqlCommand("UpdateBooks", conn)
        //            {
        //                CommandType = CommandType.StoredProcedure
        //            };
        //            //создадим параметры для хранимой процедуры
        //            //для этого создадим ссылку типа коллекции
        //            //Parameters и свяжем ее со свойством Parameters
        //            //созданного объекта updateCommand
        //            //через такую ссылку будет удобно добавлять параметры
        //            SqlParameterCollection cparams = updateCommand.Parameters;
        //            //добавляем параметры для хранимой процедуры
        //            cparams.Add("@pid", SqlDbType.Int, 0, "id");
        //            cparams["@pid"].SourceVersion = DataRowVersion.Original;
        //            cparams.Add("@pAuthorId", SqlDbType.Int, 8, "AuthorId");
        //            cparams.Add("@pTitle", SqlDbType.NChar, 100, "Title");
        //            cparams.Add("@pPrice", SqlDbType.Int, 8, "Price");
        //            cparams.Add("@pPages", SqlDbType.Int, 8, "Pages");
        //            da.UpdateCommand = updateCommand;

        //            da.Fill(set, "mybook");
        //            dataGridView1.DataSource = set.Tables["mybook"];
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //        }
        //    }


        //    private void button2_Click(object sender, EventArgs e)
        //    {
        //    da.Update(set, "mybook");
        //}

        //    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //    {

        //    }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SqlCommand comm = new SqlCommand
        //    {
        //        CommandText = textBox1.Text,//select * from Books//select * from Authors
        //        Connection = conn
        //    };
        //    conn.Open();
        //    DataTable table = new DataTable();
        //    using (var reader = comm.ExecuteReader())
        //    {
        //        do
        //        {
        //            for (int i = 0; i < reader.FieldCount; i++)
        //                table.Columns.Add(reader.GetName(i));
        //            {
        //                while (reader.Read())
        //                {
        //                    DataRow row = table.NewRow();
        //                    for (int i = 0; i < reader.FieldCount; i++)
        //                    {
        //                        row[i] = reader[i]; 
        //                    }
        //                    table.Rows.Add(row);
        //                }
        //            }
        //        } while (reader.NextResult());
        //        dataGridView1.DataSource = table;
        //    }
        //    conn.Close();
        //}
    }

}
            
