using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using Winform.FullStackII.Entities;
using Winform.FullStackII.Forms;

namespace Winform.FullStackII
{
    public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            // çalışıyor 
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text)) 
            {

                SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=OdevDb; integrated security = true;");
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from Users where Username=@username and Password=@password";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@username", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);
                connection.Open();
                var reader = command.ExecuteReader();
                AppUser user = null;
                while (reader.Read())
                {
                    user = new AppUser();
                    user.Id = Convert.ToInt32(reader[0]);
                    user.Username = Convert.ToString(reader[1]);
                    user.Password= Convert.ToString(reader[2]);
                    break;
                }
                
                if (user != null)
                {
                    NotesForm form = new NotesForm(user);
                    this.Hide();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış");
                    return;
                }

            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş geçilemez");
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
