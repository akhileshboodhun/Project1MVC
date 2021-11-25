﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Project1MVC.Models;
using Project1MVC.Services;

namespace Project1MVC.DAL
{
    public sealed class EmployeeDAL : IModelDAL<Employee>
    {
        private EmployeeDAL() { }

        public static EmployeeDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly EmployeeDAL instance = new EmployeeDAL();
        }

        public bool Add(Employee obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        @"	INSERT INTO [dbo].[User](Fname, LName, Email, Salt, HashedPassword, UserRoleId) VALUES (@FName, @LName, @Email, @Salt,
																					  @HashedPassword, @UserRoleId);
	                        DECLARE @EmpId int;
	                        SET @EmpId = @@IDENTITY;
	                        INSERT INTO [dbo].[Employee](EmpId, DOB, Address, PhoneNo, IsActive) VALUES (@EmpId, @DOB, @Address, @PhoneNo, @IsActive);
                            INSERT INTO [dbo].[Manager]([EmpId], [MgrId]) VALUES(@EmpId, @MgrId)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@FName", obj.FName);
                    cmd.Parameters.AddWithValue("@LName", obj.LName);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Salt", obj.Salt);
                    cmd.Parameters.AddWithValue("@HashedPassword", obj.HashedPassword);
                    cmd.Parameters.AddWithValue("@UserRoleId", obj.UserRoleId);
                    cmd.Parameters.AddWithValue("@DOB", obj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                    cmd.Parameters.AddWithValue("@MgrId", obj.MgrId);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }

        public bool Delete(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Delete";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "DELETE FROM [Employee] WHERE [EmpId] = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }
        public bool Terminate(int id)
        {
            bool status = false;
            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    try
                    {
                    status = this.Terminate(conn, id);

                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                    conn.Close();
                    }
                }
            }
            return status;
        }
        bool Terminate(SqlConnection conn, int? id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Terminate";
            bool status = false;

            string sql = @"
                            BEGIN TRANSACTION
                                BEGIN TRY
                                    UPDATE [Employee]
                                    SET [IsActive] = 0
                                    WHERE [EmpId] = @Id

                                    UPDATE [EquipmentEmployee]
                                    SET [DateReturned] = GETDATE()
                                    WHERE [EmpId] = @Id
                                    AND [DateReturned] is NULL
                                        
                                    COMMIT

                                END TRY
                                BEGIN CATCH
                                    ROLLBACK
                                END CATCH

             ";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        if (cmd.ExecuteNonQuery() >= 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }

            return status;
        }


        public Employee Get(int id) => Get(id.ToString());
        public Employee Get(string query)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            Employee Employee = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    var byId = "[UserId] = @Id";
                    var byEmail = "[Email] = @Email";
                    int id;
                    var byCondition = (int.TryParse(query, out id)) ? byId : byEmail;
                    string sql = @" SELECT u.[UserId], u.[FName], u.[LName], u.[Email], u.[Salt], u.[HashedPassword], ur.[UserRoleId], ur.[RoleName], emp.[DOB], emp.[Address], emp.[PhoneNo], emp.[IsActive] 
                                    FROM ([User] u LEFT JOIN [UserRole] ur ON  u.UserRoleId = ur.UserRoleId) 
                                    INNER JOIN [Employee] emp ON u.UserId = emp.EmpId
                                    WHERE " + byCondition;

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Email", query);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                Employee = new Employee(reader["UserId"].ToInt(),
                                                        reader["FName"].ToString(),
                                                        reader["LName"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Salt"].ToString(),
                                                        reader["HashedPassword"].ToString(),
                                                        reader["UserRoleId"].ToInt(),
                                                        reader["RoleName"].ToString(),
                                                        reader["DOB"].ToDateTime(),
                                                        reader["Address"].ToString(),
                                                        reader["PhoneNo"].ToString(),
                                                        reader["IsActive"].ToBoolean());
                            }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return Employee;
        }

        

        public List<Employee> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<Employee> list = new List<Employee>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = @" SELECT u.[UserId], u.[FName], u.[LName], u.[Email], u.[Salt], u.[HashedPassword], ur.[UserRoleId], ur.[RoleName], emp.[DOB], emp.[Address], emp.[PhoneNo], emp.[IsActive] 
                                    FROM ([User] u LEFT JOIN [UserRole] ur ON  u.UserRoleId = ur.UserRoleId) 
                                    INNER JOIN [Employee] emp ON u.UserId = emp.EmpId
                                    ORDER BY u.[FName], u.[LName] ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(
                                    new Employee(reader["UserId"].ToInt(),
                                                        reader["FName"].ToString(),
                                                        reader["LName"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Salt"].ToString(),
                                                        reader["HashedPassword"].ToString(),
                                                        reader["UserRoleId"].ToInt(),
                                                        reader["RoleName"].ToString(),
                                                        reader["DOB"].ToDateTime(),
                                                        reader["Address"].ToString(),
                                                        reader["PhoneNo"].ToString(),
                                                        reader["IsActive"].ToBoolean())
                                    );  }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return list;
        }

        public bool Update(Employee obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "UpdateEmployee";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@FName", obj.FName);
                    cmd.Parameters.AddWithValue("@LName", obj.LName);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Salt", obj.Salt);
                    cmd.Parameters.AddWithValue("@HashedPassword", obj.HashedPassword);
                    cmd.Parameters.AddWithValue("@UserRoleId", obj.UserRoleId);
                    cmd.Parameters.AddWithValue("@DOB", obj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@PhoneNo", obj.PhoneNo);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

                    try
                    {
                        if (cmd.ExecuteNonQuery() >= 1)
                        {
                            status = true;
                            if(obj.IsActive == false)
                            {
                                status = Terminate(conn, obj.UserId);
                            }
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return status;
        }

        public List<Employee> GetAllManagers(int? id = null)
        {
            string query = "";
            if(id != null)
            {
                query = "AND mgr.[EmpId] != " + id;
            }

            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<Employee> list = new List<Employee>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = @" SELECT u.[UserId], u.[FName], u.[LName], u.[Email], u.[Salt], u.[HashedPassword], ur.[UserRoleId], ur.[RoleName], emp.[DOB], emp.[Address], emp.[PhoneNo], emp.[IsActive] 
                                    FROM ([User] u LEFT JOIN [UserRole] ur ON  u.UserRoleId = ur.UserRoleId) 
                                    INNER JOIN [Employee] emp ON u.UserId = emp.EmpId
                                    LEFT JOIN [Manager] mgr ON emp.EmpId = mgr.EmpId 
                                    WHERE ur.[RoleName] = 'Manager'" + query + @"
                                    ORDER BY u.[FName], u.[LName] ASC";
                   
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(
                                    new Employee(reader["UserId"].ToInt(),
                                                        reader["FName"].ToString(),
                                                        reader["LName"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Salt"].ToString(),
                                                        reader["HashedPassword"].ToString(),
                                                        reader["UserRoleId"].ToInt(),
                                                        reader["RoleName"].ToString(),
                                                        reader["DOB"].ToDateTime(),
                                                        reader["Address"].ToString(),
                                                        reader["PhoneNo"].ToString(),
                                                        reader["IsActive"].ToBoolean())
                                    );
                            }
                        }

                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                }
            }

            return list;
        }
    }
}