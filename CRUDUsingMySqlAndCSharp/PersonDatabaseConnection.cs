using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDUsingMySqlAndCSharp
{
    public class PersonDatabaseConnection
    {
        private MySqlConnection connection;

        public PersonDatabaseConnection(string connectionString)
        {
            this.connection = new MySqlConnection(connectionString);
        }

        public void Insert(Person person)
        {       
            string query = string.Format("INSERT INTO person (firstname, lastname, age) VALUES('{0}', '{1}', '{2}')", person.FirstName, person.LastName, person.Age);
            this.ExecuteNonQuery(query);
        }

        public void Update(Person person)
        {
            string query = string.Format("UPDATE person SET firstname = '{0}', lastname = '{1}',  age = {2} WHERE id = {3}", person.FirstName, person.LastName, person.Age, person.Id);
            this.ExecuteNonQuery(query);
        }

        public void Delete(int id)
        {
            string query = string.Format("DELETE FROM person WHERE id={0}", id);
            this.ExecuteNonQuery(query);
        }

        private void ExecuteNonQuery(string query)
        {
            try
            {
                this.connection.Open();
                var command = new MySqlCommand(query, this.connection);
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }


        public List<Person> GetAll()
        {
            var result = new List<Person>();
            try
            {
                var query = "Select * From person";
                this.connection.Open();
                var command = new MySqlCommand(query, this.connection);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    result.Add(new Person
                    {
                        Id = dataReader.GetInt32("id"),
                        FirstName = dataReader.GetString("firstname"),
                        LastName = dataReader.GetString("lastname"),
                        Age = dataReader.GetInt32("age")
                    });
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return result;
        }

    }
}
