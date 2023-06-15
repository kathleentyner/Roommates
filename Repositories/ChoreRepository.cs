using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Roommates.Models;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Roommates.Repositories

{   
    public class ChoreRepository : BaseRepository
    {
        
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<Chore> GetAll()
        {
            using (SqlConnection conn = Connection)
            {

                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand()) //where I tell C# to create a new SQL query

                {
                    cmd.CommandText = "SELECT Id, Name from Chore"; //what is the SQL i want? you cab test it by doing a SQL query.

                    SqlDataReader reader = cmd.ExecuteReader(); //read what I queried

                    List<Chore> chores = new List<Chore>();

                    while (reader.Read()) // loop through my sql command as long as there is more data
                    {

                        int idColumnPosition = reader.GetOrdinal("Id"); //getordinal means what column is it
                        int idValue = reader.GetInt32(idColumnPosition); //what does the int say
                        int nameColumnPosition = reader.GetOrdinal("Name");//what is the column position
                        string nameValue = reader.GetString(nameColumnPosition); //what does the string say

                        Chore chore = new Chore //new instance of a chore
                        {//make a new object
                            Id = idValue,
                            Name = nameValue

                        };
                        //add the object to the list
                        chores.Add(chore);

                    }
                    //close the reader
                    reader.Close();

                    return chores; //return the list of chores
                }
            }
        }

        ///  Returns a single chore with the given id.
        public Chore GetById(int id)
        {
            using (SqlConnection connection = Connection)
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())//build command
                {
                    cmd.CommandText = "Select Id, Name from Chore where Id = @id";//@ means we are using a parameter in the command we are building
                    cmd.Parameters.AddWithValue("id", id);//We need to add a key/value pair to the parameter table in our command so it knows what we are talking about

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Chore chore = null;
                        //Returning 1 object, don't need a loop.
                        if (reader.Read())
                        {
                            chore = new Chore()
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                        }

                        return chore;
                    }
                }
            }
        }
    
            /// <summary>
            ///  Add a new chore to the database
            ///   NOTE: This method sends data to the database,
            ///   it does not get anything from the database, so there is nothing to return.
            /// </summary>

           public void Insert(Chore chore)
            {

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"insert into chore (Name) output inserted.Id values(@name)";
                        cmd.Parameters.AddWithValue("@name", chore.Name);
                        int id = (int)cmd.ExecuteScalar();

                        chore.Id = id;
                    }


                }
            }

           
        }

    }

//Add a method to ChoreRepository called GetUnassignedChores.
//It should not accept any parameters and should return a list of chores that don't have any roommates already assigned to them.
//After implementing this method, add an option to the menu so the user can see the list of unassigned chores.