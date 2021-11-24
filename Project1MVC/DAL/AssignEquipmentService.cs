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

        public bool Assign(AssignedEquipment obj){
            
            if(obj.SerialNo == null) return Assign(new List<AssignedEquipment>() { obj });
            else
            {
                string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
                string opType = "Insert";
                bool status = false;

                using (SqlConnection conn = DAL.GetConnection())
                {
                    if (conn != null)
                    {
                        string sql =
                            @"
                        INSERT INTO [dbo].[EquipmentEmployee]([EquipId],[EmpId],[DateAssigned],[AssignorId],[SerialNo])
                        VALUES (@equipmentId,@employeeId,GETDATE(),@assignorId, @serialNo)
                        ";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        try
                        {
                                cmd.Parameters.AddWithValue("@employeeId", obj.EmployeeId);
                                cmd.Parameters.AddWithValue("@equipmentId", obj.EquipmentId);
                                cmd.Parameters.AddWithValue("@assignorId", obj.AssignorId);
                                cmd.Parameters.AddWithValue("@serialNo", obj.SerialNo);
                            if (cmd.ExecuteNonQuery() >= 1)
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
        }

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
                        @"
                        SELECT es.[SerialNo], eq.[EquipID], eq.[Type], eq.[Brand], eq.[Model], eq.[Description], eq.[ReStockThreshold]
                        INTO #EquipmentInCurrentStock
                        FROM [EquipmentInStock] es JOIN [Equipment] eq ON es.[EquipID] = eq.[EquipID]
                        WHERE es.[SerialNo] NOT IN (
                            SELECT [SerialNo]
                            FROM [EquipmentEmployee]
                            WHERE [DateReturned] IS NULL
                        )
                        DECLARE @SerialNo INT =  (SELECT TOP(1) SerialNo FROM #EquipmentInCurrentStock WHERE EquipId = @equipmentId)

                        INSERT INTO [dbo].[EquipmentEmployee]([EquipId],[EmpId],[DateAssigned],[AssignorId],[SerialNo])
                        VALUES (@equipmentId,@employeeId,GETDATE(),@assignorId, @SerialNo)
                        ";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        foreach (var equipment in obj)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@employeeId", equipment.EmployeeId);
                            cmd.Parameters.AddWithValue("@equipmentId", equipment.EquipmentId);
                            cmd.Parameters.AddWithValue("@assignorId", equipment.AssignorId);
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
                          AND [SerialNo] = @serialNo
                          AND [DateReturned]  is NULL;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        foreach (var equipment in obj)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@employeeId", equipment.EmployeeId);
                            cmd.Parameters.AddWithValue("@serialNo", equipment.SerialNo);

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

                    string sql = @"SELECT [EmpId], [EquipId], [DateAssigned], [AssignorId], [SerialNo]
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
                                list.Add(new AssignedEquipment(employeeId: reader["EmpId"].ToInt(), equipmentId: reader["EquipId"].ToInt(), dateAssigned: reader["DateAssigned"].ToDateTime(), assignorId: reader["AssignorId"].ToInt() , serialNo: reader["SerialNo"].ToInt()));
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

        public List<AssignedEquipment> ViewAllAssignedEquipments()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "View Assigned";
            List<AssignedEquipment> list = new List<AssignedEquipment>();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {

                    string sql = @"SELECT [EmpId], [EquipId], [DateAssigned], [AssignorId], [SerialNo]
                                   FROM [EquipmentEmployee]
                                   WHERE [DateReturned] is NULL";
                    

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                list.Add(new AssignedEquipment(employeeId: reader["EmpId"].ToInt(), equipmentId: reader["EquipId"].ToInt(), dateAssigned: reader["DateAssigned"].ToDateTime(), assignorId: reader["AssignorId"].ToInt(), serialNo: reader["SerialNo"].ToInt()));
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
        public List<int> GetAvailableSerialNo(int equipId)
        {
            List<int> serialNos = new List<int>();

            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Get Available Serial Nos";

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {

                    string sql = @"
                        SELECT es.[SerialNo]
                        FROM [EquipmentInStock] es
                        WHERE es.[EquipId] = @equipId
                        AND es.[SerialNo] NOT IN (
                            SELECT [SerialNo]
                            FROM [EquipmentEmployee]
                            WHERE [DateReturned] IS NULL
                        )
                        ";


                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@equipId", equipId);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                serialNos.Add(reader["SerialNo"].ToInt());
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

            return serialNos;

        }
    }
}