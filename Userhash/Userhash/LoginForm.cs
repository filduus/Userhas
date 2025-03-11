using System;
using System.Linq;
using System.Windows.Forms;

public class LoginForm : Form
{
    private TextBox txtUsername = new TextBox();
    private TextBox txtPassword = new TextBox { PasswordChar = '*' };
    private Button btnLogin = new Button { Text = "Login" };
    private UserDatabase userDb = UserDatabase.Load();

    public LoginForm()
    {
        Text = "Login";
        Controls.Add(txtUsername);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        btnLogin.Click += BtnLogin_Click;
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        User user = userDb.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
        if (user != null && user.VerifyPassword(txtPassword.Text))
        {
            if (user is Admin)
            {
                new AdminForm(userDb).Show();
            }
            else
            {
                new UserForm(user).Show();
            }
            Hide();
        }
        else
        {
            MessageBox.Show("Invalid login");
        }
    }
}
