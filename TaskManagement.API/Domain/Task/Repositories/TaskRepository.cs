using System.Data.SqlClient;
using System.Data;
using TaskManagement.API.Domain.User.Entities;
using TaskManagement.API.Domain.Task.Entities;

namespace TaskManagement.API.Domain.Task.Repositories
{
    public class TaskRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ConnectionString:DefaultConnection");
        }

        public List<TaskEntity> FindAll()
        {
            List<TaskEntity> data = new List<TaskEntity>();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Task_GetAll", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        adap.SelectCommand = command;
                        adap.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];

                            foreach (DataRow row in dt.Rows)
                            {
                                var item = new TaskEntity
                                {
                                    TaskId = new Guid(row["TaskId"].ToString()),
                                    Title = row["Title"].ToString(),
                                    Description = row["Description"].ToString(),
                                    Assignee = row["Assignee"].ToString(),
                                    Duedate = Convert.ToDateTime(row["Duedate"].ToString()),
                                    Priority = row["Priority"].ToString(),
                                    Status = row["Status"].ToString(),
                                    Progress = Convert.ToInt32(row["Progress"].ToString()),
                                };
                                data.Add(item);
                            }
                        }
                     }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return data;
        }

        public void Create(TaskEntity data)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Task_Create", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Title", data.Title);
                        command.Parameters.AddWithValue("@Description", data.Description);
                        command.Parameters.AddWithValue("@Assignee", data.Assignee);
                        command.Parameters.AddWithValue("@DueDate", data.Duedate);
                        command.Parameters.AddWithValue("@Priority", data.Priority);
                        command.Parameters.AddWithValue("@Status", data.Status);
                        command.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public void Update(TaskEntity data)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Task_Update", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TaskId", data.TaskId);
                        command.Parameters.AddWithValue("@Title", data.Title);
                        command.Parameters.AddWithValue("@Description", data.Description);
                        command.Parameters.AddWithValue("@Assignee", data.Assignee);
                        command.Parameters.AddWithValue("@DueDate", data.Duedate);
                        command.Parameters.AddWithValue("@Priority", data.Priority);
                        command.Parameters.AddWithValue("@Status", data.Status);
                        command.Parameters.AddWithValue("@Progress", data.Progress);
                        command.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public void Delete(Guid taskId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Task_Delete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TaskId", taskId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public async Task<TaskEntity> FindById(Guid TaskId) 
        {
            var data = new TaskEntity();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Task_FindById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TaskId", TaskId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                data.TaskId = new Guid(reader["TaskId"].ToString());

                                data.Title = reader["Title"].ToString();
                                data.Description = reader["Description"].ToString();
                                data.Assignee = reader["Assignee"].ToString();
                                data.Duedate = Convert.ToDateTime(reader["Duedate"].ToString());
                                data.Priority = reader["Priority"].ToString();
                                data.Status = reader["Status"].ToString();
                                data.Progress = Convert.ToInt32(reader["Progress"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return data;
        }
    }
}
