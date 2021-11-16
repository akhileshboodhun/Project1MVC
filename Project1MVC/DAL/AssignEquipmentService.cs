using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.DAL
{
    public sealed class AssignEquipmentService
    {
        private AssignEquipmentService() { }

        public static AssignEquipmentService Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly AssignEquipmentService instance = new AssignEquipmentService();
        }

        public bool Assign(AssignedEquipment obj) => Assign(new List<AssignedEquipment>() { obj });

        public bool Assign(List<AssignedEquipment> obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        @"INSERT INTO [dbo].[EquipmentEmployee]([EmpId],[EquipId],[DateAssigned],[AssignorId])
                            VALUES(@employeeId, @equipmentId, GETDATE(), @assignorId)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        foreach (var equipment in obj)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@employeeId", equipment.EmployeeId);
                            cmd.Parameters.AddWithValue("@equipmentId", equipment.EquipmentId);
                            cmd.Parameters.AddWithValue("@assignorId", equipment.AssignorId);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                status = true;
                                Logger.Log($"SUCCESS: {opType} {modelName}");
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

            return status;
        }

        public bool Return(AssignedEquipment obj) => Return(new List<AssignedEquipment> { obj });
        public bool Return(List<AssignedEquipment> obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        @"UPDATE [dbo].[EquipmentEmployee]
                          SET [DateReturned] = GETDATE()
                          WHERE [EmpId] = @employeeId
                          AND [EquipId] = @equipmentId
                          AND [DateReturned]  is NULL;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        foreach (var equipment in obj)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@employeeId", equipment.EmployeeId);
                            cmd.Parameters.AddWithValue("@equipmentId", equipment.EquipmentId);

                            if (cmd.ExecuteNonQuery() >= 1)
                            {
                                status = true;
                                Logger.Log($"SUCCESS: {opType} {modelName}");
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

            return status;
        }


        public List<AssignedEquipment> ViewAssigned(int employeeId)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "View Assigned";
            List<AssignedEquipment> list = new List<AssignedEquipment>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {

                    string sql = @"SELECT [EmpId], [EquipId], [DateAssigned], [AssignorId]
                                   FROM [EquipmentEmployee]
                                   WHERE [DateReturned] is NULL
                                   AND [EmpId] = @employeeId";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new AssignedEquipment(employeeId: reader["EmpId"].ToInt(), equipmentId: reader["EquipId"].ToInt(), dateAssigned: reader["DateAssigned"].ToDateTime(), assignorId: reader["AssignorId"].ToInt()));
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