using System;
using System.Windows.Forms;

public class UserForm : Form
{
    private User user;
    private TextBox txtNewPassword = new TextBox { PasswordChar = '*' };
    private Button btnChangePassword = new Button { Text = "Change Password" };
    private UserDatabase userDb = UserDatabase.Load();

    public UserForm(User user)
    {
        this.user = user;
        Text = "User Panel";
        Controls.Add(txtNewPassword);
        Controls.Add(btnChangePassword);
        btnChangePassword.Click += BtnChangePassword_Click;
    }

    private void BtnChangePassword_Click(object sender, EventArgs e)
    {
        user.PasswordHash = User.HashPassword(txtNewPassword.Text);
        userDb.Save();
        MessageBox.Show("Password changed");
    }
}
