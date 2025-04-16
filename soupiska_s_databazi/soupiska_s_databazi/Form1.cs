using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SoupiskaApp
{
    public partial class Form1 : Form
    {
        string connString = "Data Source=players.db";

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            InitDatabase();
            LoadPlayers();
            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
        }

        private void InitDatabase()
        {
             var conn = new SQLiteConnection(connString);
            conn.Open();
            string sql = @"CREATE TABLE IF NOT EXISTS Players (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Position TEXT NOT NULL)";
             var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        private void LoadPlayers()
        {
            var conn = new SQLiteConnection(connString);
            conn.Open();
            string sql = "SELECT * FROM Players";
            var da = new SQLiteDataAdapter(sql, conn);
            var dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string position = cmbPosition.Text;

            if (name == "" || position == "")
            {
                MessageBox.Show("Vyplň jméno a pozici.");
                return;
            }

            var conn = new SQLiteConnection(connString);
            conn.Open();
            string sql = "INSERT INTO Players (Name, Position) VALUES (@name, @position)";
                var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@position", position);
            cmd.ExecuteNonQuery();

            txtName.Clear();
            cmbPosition.SelectedIndex = -1;

            LoadPlayers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vyber řádek ke smazání.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

            var conn = new SQLiteConnection(connString);
            conn.Open();
            string sql = "DELETE FROM Players WHERE Id = @id";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            LoadPlayers();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbPosition.Items.AddRange(new string[]
            {
        "Brankář",
        "Obránce",
        "Záložník",
        "Útočník"
            });
        }
    }
}
