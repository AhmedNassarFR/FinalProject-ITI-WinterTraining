using Microsoft.Data.SqlClient;

namespace backend;

public class Class1
{
    public static string _connectionString = "Data Source=Mo3taz\\SQLEXPRESS;Initial Catalog=project;Integrated Security=True;TrustServerCertificate=True;";

    // static public void TestingConnection()
    // {
    //     try
    //     {
    //         using (SqlConnection conn = new SqlConnection(_connectionString))
    //         {
    //             conn.Open();
    //             if (conn.State == System.Data.ConnectionState.Open)
    //             {
    //                 Console.WriteLine("Database connected");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("error in connection");
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"An error occurred: {ex.Message}");
    //     }
    // }

    public void AddDepartment(string departmentName)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Departments (DepartmentName) VALUES (@DepartmentName)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("Department added successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void AddEmployee(string employeeName, int departmentId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Check if the provided DepartmentID exists
                string checkQuery = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                    int departmentExists = (int)checkCmd.ExecuteScalar();

                    if (departmentExists == 0)
                    {
                        // If the department does not exist, set departmentId to NULL (or handle as needed)
                        departmentId = -1; // Use -1 as an indicator (assuming no department has this ID)
                    }
                }

                // Insert employee
                string query = departmentId == -1
                    ? "INSERT INTO Employees (EmployeeName, DepartmentID) VALUES (@EmployeeName, NULL)"
                    : "INSERT INTO Employees (EmployeeName, DepartmentID) VALUES (@EmployeeName, @DepartmentID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);

                    if (departmentId != -1)
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine(departmentId == -1
                        ? "Employee added successfully with 'N/A' as department."
                        : "Employee added successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occurred while adding the employee.\nMake sure the department ID is correct or the department exists.");
        }
    }

    public void UpdateDepartment(int departmentId, string newName)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Check if the department exists
                string checkQuery = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                    int departmentExists = (int)checkCmd.ExecuteScalar();

                    if (departmentExists == 0)
                    {
                        Console.WriteLine("Department not found. No updates will be made.");
                        conn.Close();
                        return;
                    }
                }

                // Proceed with the update if the department exists
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("No updates provided.");
                    conn.Close();
                    return;
                }

                string updateQuery = "UPDATE Departments SET DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", newName);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine(rowsAffected > 0 ? "Department updated successfully." : "Update failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void UpdateEmployee(int employeeId, string newName, int newDepartmentId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                List<string> updateFields = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(newName))
                {
                    updateFields.Add("EmployeeName = @EmployeeName");
                    parameters.Add(new SqlParameter("@EmployeeName", newName));
                }

                if (newDepartmentId > 0)
                {
                    updateFields.Add("DepartmentID = @DepartmentID");
                    parameters.Add(new SqlParameter("@DepartmentID", newDepartmentId));
                }

                if (updateFields.Count == 0)
                {
                    Console.WriteLine("No updates provided.");
                    return;
                }

                string query = $"UPDATE Employees SET {string.Join(", ", updateFields)} WHERE EmployeeID = @EmployeeID";
                parameters.Add(new SqlParameter("@EmployeeID", employeeId));

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine(rowsAffected > 0 ? "Employee updated successfully." : "Employee not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void DeleteDepartment(int departmentId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine(rowsAffected > 0 ? "Department deleted successfully." : "Department not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void DeleteEmployee(int employeeId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine(rowsAffected > 0 ? "Employee deleted successfully." : "Employee not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void GetAllEmployees()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT e.EmployeeID, e.EmployeeName, d.DepartmentName FROM Employees e LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Employees:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["EmployeeName"]}, Department: {(reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"] : "N/A")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employees found.");
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void GetAllDepartments()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Departments";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Departments:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["DepartmentID"]}, Name: {reader["DepartmentName"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No departments found.");
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    public void GetEmployeesByDepartment(int departmentId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT EmployeeID, EmployeeName FROM Employees WHERE DepartmentID = @DepartmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine($"\nEmployees in Department {departmentId}:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["EmployeeName"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No employees found in Department {departmentId}.");
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public bool DepartmentExists(int departmentId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Departments WHERE DepartmentID = @DepartmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while checking department existence: {ex.Message}");
            return false;
        }
    }

    public bool EmployeeExists(int employeeId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while checking employee existence: {ex.Message}");
            return false;
        }
    }

    public void SearchDepartments(string dName)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Departments WHERE DepartmentName LIKE @SearchTerm";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + dName + "%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("\nSearch Results for Departments:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["DepartmentID"]}, Name: {reader["DepartmentName"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No departments found matching the search term.");
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while searching for departments: {ex.Message}");
        }
    }

    public void SearchEmployees(string eName)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT e.EmployeeID, e.EmployeeName, d.DepartmentName FROM Employees e LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID WHERE e.EmployeeName LIKE @SearchTerm";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + eName + "%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("\nSearch Results for Employees:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["EmployeeID"]}, Name: {reader["EmployeeName"]}, Department: {(reader["DepartmentName"] != DBNull.Value ? reader["DepartmentName"] : "N/A")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No employees found matching the search term.");
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while searching for employees: {ex.Message}");
        }
    }
}



