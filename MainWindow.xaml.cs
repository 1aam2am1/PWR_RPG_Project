using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = new MySqlConnection("server=localhost; user=root; database=UNITY");
        MySqlCommand command;
        string askSQl = "";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void CAccUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void SignInClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                askSQl = string.Format("select count(Id) from account where Login = '{0}' and Password = '{1}'",
                    LogInUser.Text, LogInPass.Text);
                command = new MySqlCommand(askSQl, connection);
                int value = Convert.ToInt32(command.ExecuteScalar());
                if (value == 0)
                    MessageBox.Show("Brak uzytkownika o tej nazwie", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (value > 0)
                     new Window2().ShowDialog();
                 
            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem", ex.Message);
                MessageBox.Show(byk, "bład", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        void Rejerstracja(string login, string password, bool active)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                //sprawdzenie istnienia
                askSQl = string.Format("select count(Id) from account where Login = '{0}'", login);
                command = new MySqlCommand(askSQl, connection);
                int wartosc = Convert.ToInt32(command.ExecuteScalar());
                if (wartosc == 1)
                {
                    MessageBox.Show(string.Format("Uzytkownik z loginem '{0}' juz istnieje", login),
                        "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    CAccUser.SelectAll();
                }
                else
                {
                    askSQl = string.Format("insert into account (Login, Password, Active) values ('{0}', '{1}', '{2}')",
                        login, password, Convert.ToInt32(active));
                    command.CommandText = askSQl;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Uzytkownik dodany", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                string byk = string.Format("Problem", ex.Message);
                MessageBox.Show(byk, "bład", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                connection.Close();
            }
        }


        private void CAccClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CAccUser.Text) || string.IsNullOrWhiteSpace(CAccPass.Text))
            {
                MessageBox.Show("Podaj login i haslo", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                Rejerstracja(CAccUser.Text, CAccPass.Text, true);
            }
        }

       
    }
}
