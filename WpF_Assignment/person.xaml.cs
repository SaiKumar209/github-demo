using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpF_Assignment
{
    /// <summary>
    /// Interaction logic for person.xaml
    /// </summary>
    public partial class person : Window
    {
        int t = 0;
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataAdapter da;
        private DataSet ds;
        public person()
        {
            conn = new SqlConnection("database=master; server=kumar-4777;trusted_connection=true");
            command = new SqlCommand();
            da = new SqlDataAdapter();
            ds = new DataSet();

            InitializeComponent();
            //LoadData();
        }
        public void LoadData()
        {
            try
            {
                conn.Open();
                command.CommandText = "select * from person_detail";
                command.Connection = conn;

                da.SelectCommand = command;
                da.Fill(ds, "property");

                Txt_Id.Text = ds.Tables[0].Rows[0].ToString();
                Txt_First.Text = ds.Tables[0].Rows[1].ToString();
                Txt_Last.Text = ds.Tables[0].Rows[2].ToString();
                Txt_Add.Text = ds.Tables[0].Rows[3].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
                ds.Clear();
                command.Parameters.Clear();

            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "insert into person_detail values(@p1,@p2,@p3,@p4)";
                command.Parameters.AddWithValue("@p1", Txt_Id.Text);
                command.Parameters.AddWithValue("@p2", Txt_First.Text);
                command.Parameters.AddWithValue("@p3", Txt_Last.Text);
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "delete from person_detail where personid=@p1";
                command.Parameters.AddWithValue("@p1", Txt_Id.Text);

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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "update person_detail set FirstName=@p1, LastName=@p2,Address=@p3 where personid=@p4";
                command.Parameters.AddWithValue("@p1", Txt_First.Text);
                command.Parameters.AddWithValue("@p2", Txt_Last.Text);
                command.Parameters.AddWithValue("@p3", Txt_Add.Text);
                command.Parameters.AddWithValue("@p4", Txt_Id.Text);
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

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Txt_Id.Clear();
            Txt_First.Clear();
            Txt_Last.Clear();
            Txt_Add.Clear();

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                command.Connection = conn;
                command.CommandText = "select * from person_detail where personid=@p1";
                command.Parameters.AddWithValue("@p1", Txt_Id.Text);
                da.SelectCommand = command;
                da.Fill(ds, "property");



                if (ds.Tables[0].Rows.Count > 0)
                {
                    Txt_Id.Text = ds.Tables[0].Rows[0].ToString();
                    Txt_First.Text = ds.Tables[0].Rows[1].ToString();
                    Txt_Last.Text = ds.Tables[0].Rows[2].ToString();
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

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            t = t - 1;
            if (t < 0)
            {
                MessageBox.Show("first record");
            }
            else
            {
                Txt_Id.Text = ds.Tables[0].Rows[t][0].ToString();
                Txt_First.Text = ds.Tables[0].Rows[t][1].ToString();
                Txt_Last.Text = ds.Tables[0].Rows[t][2].ToString();
                Txt_Add.Text = ds.Tables[0].Rows[t][3].ToString();
            }



        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            t = t + 1;
            if (t > ds.Tables[0].Rows.Count-1)
            {
                MessageBox.Show("last record");
            }
            else
            {
                Txt_Id.Text = ds.Tables[0].Rows[t][0].ToString();
                Txt_First.Text = ds.Tables[0].Rows[t][1].ToString();
                Txt_Last.Text = ds.Tables[0].Rows[t][2].ToString();
                Txt_Add.Text = ds.Tables[0].Rows[t][3].ToString();
            }


        }

        private void First_record_Click(object sender, RoutedEventArgs e)
        {
            Txt_Id.Text = ds.Tables[0].Rows[0][0].ToString();
            Txt_First.Text = ds.Tables[0].Rows[0][1].ToString();
            Txt_Last.Text = ds.Tables[0].Rows[0][2].ToString();
            Txt_Add.Text = ds.Tables[0].Rows[0][3].ToString();

        }

        private void Last_record_Click(object sender, RoutedEventArgs e)
        {
            int temp = ds.Tables[0].Rows.Count - 1;
            Txt_Id.Text = ds.Tables[0].Rows[temp][0].ToString();
            Txt_First.Text = ds.Tables[0].Rows[temp][1].ToString();
            Txt_Last.Text = ds.Tables[0].Rows[temp][2].ToString();
            Txt_Add.Text = ds.Tables[0].Rows[temp][3].ToString();
            t = temp;


        }

        private void Grid_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
