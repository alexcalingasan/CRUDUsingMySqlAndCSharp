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
    public partial class Form2 : Form
    {
        private PersonDatabaseConnection personDatabaseConnection;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.personDatabaseConnection = new PersonDatabaseConnection("SERVER=localhost;DATABASE=csharpdb;UID=root;PASSWORD=");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int age = 0;
            if(!string.IsNullOrWhiteSpace(txtFirstName.Text) && !string.IsNullOrWhiteSpace(txtLastName.Text) && int.TryParse(txtAge.Text, out age))
            {
                var person = new Person
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = age
                };
                this.personDatabaseConnection.Insert(person);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid inputs.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
