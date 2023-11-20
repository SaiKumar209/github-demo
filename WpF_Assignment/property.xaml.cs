using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpF_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataAdapter da;
        private DataSet ds;
        public MainWindow()
        {
            conn = new SqlConnection("database=master; server=kumar-4777;trusted_connection=true");
            command = new SqlCommand();
            da = new SqlDataAdapter();
            ds = new DataSet();


            InitializeComponent();
            LoadData(); 
        }
        public void LoadData()
        {
            try
            {
                conn.Open();
                command.CommandText = "select * from Property_Detail";
                command.Connection = conn;

                da.SelectCommand = command;
                da.Fill(ds, "property");

                Txt_id.Text = ds.Tables[0].Rows[0].ToString();
                Txt_Name.Text = ds.Tables[0].Rows[1].ToString();
                Txt_Type.Text = ds.Tables[0].Rows[2].ToString();
                Txt_Add.Text= ds.Tables[0].Rows[3].ToString();
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "insert into Property_Detail values(@p1,@p2,@p3,@p4)";
                command.Parameters.AddWithValue("@p1", Txt_id.Text);
                command.Parameters.AddWithValue("@p2", Txt_Name.Text);
                command.Parameters.AddWithValue("@p3", Txt_Type.Text);
                command.Parameters.AddWithValue("@p4", Txt_Add.Text);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("record inserted");
                }
                else
                {
                    MessageBox.Show("record not inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "update Property_Details set propertyname=@p1, propertytype=@p2,Address=@p3 where personid=@p4";
                command.Parameters.AddWithValue("@p1", Txt_Name.Text);
                command.Parameters.AddWithValue("@p2", Txt_Type.Text);
                command.Parameters.AddWithValue("@p3", Txt_Add.Text);
                command.Parameters.AddWithValue("@p4", Txt_id.Text);
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("record updated");
                }
                else
                {
                    MessageBox.Show("record not not updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "delete from Property_Detail where personid=@p1";
                command.Parameters.AddWithValue("@p1", Txt_id.Text);
              
                int res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("record deleted");
                }
                else
                {
                    MessageBox.Show("record not deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "select * from Property_Detail where propertid= @p1";
                command.Parameters.AddWithValue("@p1", Txt_id.Text);
                da.SelectCommand = command;
                da.Fill(ds, "property");


               
                if (ds.Tables[0].Rows.Count>0)
                {
                    Txt_id.Text = ds.Tables[0].Rows[0].ToString();
                    Txt_Name.Text = ds.Tables[0].Rows[1].ToString();
                    Txt_Type.Text = ds.Tables[0].Rows[2].ToString();
                    Txt_Add.Text = ds.Tables[0].Rows[3].ToString();
                }
                else
                {
                    MessageBox.Show("record not not found ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();

            }

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Txt_id.Clear();
            Txt_Name.Clear();
            Txt_Type.Clear();
            Txt_Add.Clear();

        }
    }
    }

