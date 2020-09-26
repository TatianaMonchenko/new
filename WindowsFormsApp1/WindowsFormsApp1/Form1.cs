using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //static readonly SqlConnection conn = new SqlConnection
        //{
        //    ConnectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString
        //};
        //string connectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString;

        DataSet _set;
        SqlDataAdapter _adapter;
        SqlCommandBuilder _sqlCommandBuilder;
        string _fileName;

        SqlConnection conn = new SqlConnection
        {
            ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library; Integrated Security=SSPI;"
        };
        public Form1()
        {
            InitializeComponent();
            this.Text = "Picture Library";
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                //Filter = "Graphics File|*.bmp;*.gif;*.jpg;*.png",
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _fileName = openFile.FileName;
                    var bytes = CreateCopyImage(_fileName);
                    conn.Open();

                    if ((toolStripTextBox1.Text?.Length ?? 0) != 0 &&
                        int.TryParse(toolStripTextBox1.Text, out int index))
                    {
                        var comm = new SqlCommand("Insert into Pictures (bookid, name, picture) values (@bookid, @name, @picture)", conn);
                        comm.Parameters.AddWithValue("@bookid", index);
                        comm.Parameters.AddWithValue("@name", Path.GetFileName(_fileName));
                        comm.Parameters.AddWithValue("@picture", bytes);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("Image was saved to DB");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }

        }
        private byte[] CreateCopyImage(string fileName)
        {
            var img = Image.FromFile(fileName);
            int maxWidht = 300;
            int maxHight = 300;
            int newWidth = (int)(img.Width * (double)maxWidht / img.Width);
            int newHeight = (int)(img.Height * (double)maxHight / img.Height);

            var imageRation = new Bitmap(newWidth, newHeight);
            var g = Graphics.FromImage(imageRation);
            g.DrawImage(img, 0, 0, newWidth, newHeight);

            using (var stream = new MemoryStream())
            using (var reader = new BinaryReader(stream))
            {
                imageRation.Save(stream, ImageFormat.Jpeg);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                return reader.ReadBytes((int)stream.Length);
            }
        }
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBox1.Text?.Length ?? 0) != 0 &&
                               int.TryParse(toolStripTextBox1.Text, out int index))
            {
                _adapter = new SqlDataAdapter("select picture from Pictures where id=@id", conn);
                _adapter.SelectCommand.Parameters.AddWithValue("@id", index);
                _adapter.TableMappings.Add("Table", "Pictures");
                var cmdBuider = new SqlCommandBuilder(_adapter);

                _set = new DataSet();
                _adapter.Fill(_set);

                using (var stream = new MemoryStream((byte[])_set.Tables["Pictures"].Rows[0]["picture"]))
                {
                    this.pictureBox1.Image = Image.FromStream(stream);
                }
            }
            else
            {
                MessageBox.Show("Load ID!!!");
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            try
            {
                _adapter = new SqlDataAdapter("select * from Pictures; ", conn);
                SqlCommandBuilder cmb = new SqlCommandBuilder(_adapter);
                _set = new DataSet();
                _adapter.Fill(_set, "picture");
                this.dataGridView1.DataSource = _set.Tables["picture"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
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




