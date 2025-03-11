using System;
using System.Linq;
using System.Windows.Forms;

public class AdminForm : Form
{
    private ListBox userList = new ListBox();
    private Button btnResetPassword = new Button { Text = "Reset Password" };
    private UserDatabase userDb;

    public AdminForm(UserDatabase db)
    {
        userDb = db;
        Text = "Admin Panel";
        Controls.Add(userList);
        Controls.Add(btnResetPassword);
        btnResetPassword.Click += BtnResetPassword_Click;
        LoadUsers();
    }

    private void LoadUsers()
    {
        userList.Items.Clear();
        foreach (var user in userDb.Users.Where(u => !(u is Admin)))
        {
            userList.Items.Add(user.Username);
        }
    }

    private void BtnResetPassword_Click(object sender, EventArgs e)
    {
        if (userList.SelectedItem != null)
        {
            string username = userList.SelectedItem.ToString();
            User user = userDb.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.PasswordHash = User.HashPassword("newpassword");
                userDb.Save();
                MessageBox.Show("Password reset to 'newpassword'");
            }
        }
    }
}
