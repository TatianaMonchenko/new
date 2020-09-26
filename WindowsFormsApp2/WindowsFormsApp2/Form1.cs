using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        DbConnection conn = null;
        DbProviderFactory fact = null;

        public Form1()
        {
            InitializeComponent();
            //button1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable t = DbProviderFactories.GetFactoryClasses();
                dataGridView1.DataSource = t;
                comboBox1.Items.Clear();

                foreach (DataRow dr in t.Rows)
                {
                    comboBox1.Items.Add(dr["InvariantName"]);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = GetConnectionStringByProvider(comboBox1.SelectedItem.ToString());

                fact = DbProviderFactories.GetFactory(comboBox1.SelectedItem.ToString());
                conn = fact.CreateConnection();
                conn.ConnectionString = textBox1.Text;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private string GetConnectionStringByProvider(string providerName)
        {
            var settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.ProviderName == providerName)
                        return cs.ConnectionString;
                }
            }
            return string.Empty;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var adapter = fact.CreateDataAdapter();
                adapter.SelectCommand = conn.CreateCommand();
                adapter.SelectCommand.CommandText = textBox2.Text.ToString();
                var set = new DataSet();
                adapter.Fill(set);

                DataViewManager dvm = new DataViewManager(set);
                dvm.DataViewSettings["Books"].RowFilter = $"id < 10";
                dvm.DataViewSettings["Books"].Sort = "Title ASC";
                var view = dvm.CreateDataView(set.Tables["Books"]);

                dataGridView1.DataSource = view;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            const string AsyncEnabled = "Asynchronous Processing=true";
            if (!textBox1.Text.Contains(asyncEnable))
            {
                textBox1.Text = String.Format("{0}; {1}", textBox1.Text, asyncEnable);
            }
            conn.ConnectionString = textBox1.Text;
            conn.Open();
            using (var comm = (conn as SqlConnection).CreateCommand()
            {
                comm.CommandText = $ "WAITFOR DELAY '00:00:05'; {textBox2.Text};";
                comm.CommandType = CommandType.Text;

                comm.BeginExecuteReader(callback, comm);
                MessageBox.Show("Added thread is working...");
            }
        }
        DataTable table = null;

        private void Callback(IAsyncResult result)
        {
           // SqlDataReader reader = null;
            try
            {
                SqlCommand command = (SqlCommand)result.AsyncState;
               
                var dataReader = command.EndExecuteReader(result);
               
                table = new DataTable();
                int line = 0;
                do
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                        table.Columns.Add(dataReader.GetName(i));
                    while (dataReader.Read())
                    {
                        var row = table.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            row[i] = dataReader[i];
                        }
                        table.Rows.Add(row);
                    };
                }
                while (dataReader.NextResult());
                DgvAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("From Callback 1:" + ex.Message);
            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("From Callback 2:" + ex.Message);
                }
            }
        }private void ShowData()
        {
            if (dataGridView1.InvokeRequired)

        }
    }
}

