using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDUsingMySqlAndCSharp
{
    public partial class Form1 : Form
    {
        private PersonDatabaseConnection personDatabaseConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.personDatabaseConnection = new PersonDatabaseConnection("SERVER=localhost;DATABASE=csharpdb;UID=root;PASSWORD=");
            this.dataGridView1.DataSource = this.personDatabaseConnection.GetAll();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtAge.ReadOnly = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Text == "Update")
            {
                btnUpdate.Text = "Save";
                btnDelete.Visible = false;
                txtFirstName.ReadOnly = false;
                txtLastName.ReadOnly = false;
                txtAge.ReadOnly = false;
            }
            else
            {            
                var row = dataGridView1.SelectedRows[0];
                int age = 0;
                if(int.TryParse(txtAge.Text, out age))
                {
                    var person = new Person
                    {
                        Id = int.Parse(row.Cells[0].Value.ToString()),
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Age = age
                    };
                    this.personDatabaseConnection.Update(person);
                    btnUpdate.Text = "Update";
                    btnDelete.Visible = true;
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    txtAge.ReadOnly = true;
                    this.dataGridView1.DataSource = this.personDatabaseConnection.GetAll();
                }
                else
                {
                    MessageBox.Show("Invalid age.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show
                (
                string.Format("Are you sure you want to remove {0} {1}", txtFirstName.Text, txtLastName.Text),
                "Remove Person",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );

            if (dialogResult == DialogResult.Yes)
            {
                var row = dataGridView1.SelectedRows[0];
                this.personDatabaseConnection.Delete(int.Parse(row.Cells[0].Value.ToString()));
                this.dataGridView1.DataSource = this.personDatabaseConnection.GetAll();
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtAge.Text = string.Empty;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var insertForm = new Form2();
            insertForm.ShowDialog();
            this.dataGridView1.DataSource = this.personDatabaseConnection.GetAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.SelectedRows[0];
            btnUpdate.Text = "Update";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnDelete.Visible = true;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtAge.ReadOnly = true;
            txtFirstName.Text = row.Cells[1].Value.ToString();
            txtLastName.Text = row.Cells[2].Value.ToString();
            txtAge.Text = row.Cells[3].Value.ToString();

        }
    }
}
