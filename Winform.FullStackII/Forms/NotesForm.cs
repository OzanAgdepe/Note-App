using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winform.FullStackII.Entities;

namespace Winform.FullStackII.Forms
{
    public partial class NotesForm : Form
    {
        private AppUser user;
        private List<UserNotes> userNotes;
        private UserNotes selectedNote;
        public NotesForm(AppUser user)
        {
            InitializeComponent();
            this.user = user;
            GetNotes();
            Reload();

        }

        private void GetNotes()
        {
            SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=OdevDb; integrated security = true;");
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from UserNotes where UserId = @id";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@id", user.Id);
            connection.Open();
            var reader = command.ExecuteReader();
            userNotes = new List<UserNotes>();
            while (reader.Read())
            {
                UserNotes note = new UserNotes();
                note.Id = Convert.ToInt32(reader[0]);
                note.UserId = Convert.ToInt32(reader[1]);
                note.Notes = Convert.ToString(reader[2]);
                userNotes.Add(note);
            }
            lblUser.Text = user.Username;

            listNote.DataSource = userNotes;
            listNote.DisplayMember = "Notes";
            listNote.ValueMember = "Id";
        }

        private void NotesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void NotesForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=OdevDb; integrated security = true;");
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "insert into UserNotes values (@userid , @notes)";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@userid", user.Id);
            command.Parameters.AddWithValue("@notes", txtNote.Text);
            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result != 0)
                MessageBox.Show("Yeni not eklendi");
            else
                MessageBox.Show("Not eklenemedi");

            Reload();
        }

        private void Reload()
        {
            GetNotes();
            txtNote.Text = "";
            listNote.ClearSelected();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text))
            {
                var a = listNote.SelectedItem as UserNotes;
                int b = a.Id;
                SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=OdevDb; integrated security = true;");
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "update  UserNotes set Note=@notes where Id=@Id";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@notes", txtNote.Text);
                command.Parameters.AddWithValue("@Id", b);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                    MessageBox.Show("İlgili not güncellendi");
                else
                    MessageBox.Show("güncelleme hatası");

                Reload();
            }
        }

        private void listNote_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var a = listNote.SelectedItem as UserNotes;
            int b = a.Id;
            SqlConnection connection = new SqlConnection("server=.\\SQLExpress; database=OdevDb; integrated security = true;");
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete  from UserNotes where Id=@Id";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Id", b);
            connection.Open();
            var result = command.ExecuteNonQuery();
            if (result != 0)
                MessageBox.Show("İlgili not silindi");
     

            Reload();
        }

        private void lblTest_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
