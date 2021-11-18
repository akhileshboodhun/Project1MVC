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
    public sealed class OrderDAL : IModelDAL<OrderWrapper>
    {
        private OrderDAL() { }

        public static OrderDAL Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to lazily instantiate us
            static Nested() { }

            internal static readonly OrderDAL instance = new OrderDAL();
        }

        public bool Add(OrderWrapper obj)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Insert";
            bool status = false;
            int? lastInsertedId = null;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sqlInsertInOrder =
                        "INSERT INTO [Order] (OrderDate, IsOrderComplete, SupplierId, EffectiveDate) " +
                        "VALUES (@OrderDate, @IsOrderComplete, @SupplierId, @EffectiveDate);";

                    SqlCommand cmdInsertInOrder = new SqlCommand(sqlInsertInOrder, conn);
                    cmdInsertInOrder.Parameters.AddWithValue("@OrderDate", obj.OrderProp.OrderDate);
                    cmdInsertInOrder.Parameters.AddWithValue("@IsOrderComplete", obj.OrderProp.IsOrderComplete);
                    cmdInsertInOrder.Parameters.AddWithValue("@SupplierId", obj.OrderProp.SupplierId);
                    cmdInsertInOrder.Parameters.AddWithValue("@EffectiveDate", obj.OrderProp.EffectiveDate);

                    try
                    {
                        if (cmdInsertInOrder.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }

                        string sqlGetLastInsertId = "SELECT IDENT_CURRENT('Order') AS LastId;";

                        SqlCommand cmdGetLastInsertedId = new SqlCommand(sqlGetLastInsertId, conn);

                        try
                        {
                            using (SqlDataReader reader = cmdGetLastInsertedId.ExecuteReader())
                            {
                                Logger.Log($"SUCCESS: {opType} {modelName}");

                                while (reader.Read())
                                {
                                    lastInsertedId = reader["LastId"].ToInt();
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

            using (SqlConnection conn = DAL.GetConnection())
            {
                if(conn != null && lastInsertedId != null)
                {
                    string sqlInsertInEquipmentOrder =
                                                    "INSERT INTO EquipmentOrder (EquipId, OrderId, UnitPrice, Qty) " +
                                                     "VALUES (@EquipId, @OrderId, @UnitPrice, @Qty);";

                    foreach (var item in obj.EquipmentOrderProp)
                    {
                        SqlCommand cmdInsertInEquipmentOrder = new SqlCommand(sqlInsertInEquipmentOrder, conn);
                        cmdInsertInEquipmentOrder.Parameters.AddWithValue("@EquipId", item.EquipmentId);
                        cmdInsertInEquipmentOrder.Parameters.AddWithValue("@OrderId", lastInsertedId);
                        cmdInsertInEquipmentOrder.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        cmdInsertInEquipmentOrder.Parameters.AddWithValue("@Qty", item.Qty);

                        try
                        {
                            if (cmdInsertInEquipmentOrder.ExecuteNonQuery() == 1)
                            {
                                status = true;
                                Logger.Log($"SUCCESS: {opType} {modelName}");
                            }

                        }

                        catch (Exception ex)
                        {
                            Logger.Log($"FAILED: {opType} {modelName}");
                            Logger.Log($"{ex.ToString()}");
                        }
                    }

                    if (obj.OrderProp.IsOrderComplete == true)
                    {
                        OrderComplete(conn, lastInsertedId);
                    }

                    Logger.Log("Closing the SqlConnection" + Environment.NewLine);

                    conn.Close();

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
                    string sql = "DELETE FROM [Order] WHERE [OrderId] = @Id;";

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

        public OrderWrapper Get(int id)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select";
            OrderWrapper orderWrapper = new OrderWrapper();

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT o.OrderId, o.OrderDate, o.IsOrderComplete, o.SupplierId, o.EffectiveDate, s.Name FROM [Order] o, [Supplier] s " +
                                 "WHERE o.SupplierId = s.SupplierId " +
                                 "AND o.OrderId = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            { 
                                Order order = new Order(reader["OrderId"].ToInt(), Convert.ToBoolean(reader["IsOrderComplete"].ToString()), Convert.ToDateTime(reader["OrderDate"].ToString()), reader["Name"].ToString(), Convert.ToDateTime(reader["EffectiveDate"].ToString()));
                                order.SupplierId = reader["SupplierId"].ToInt();
                                orderWrapper.OrderProp = order;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    finally
                    {
                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                }
            }


            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT eo.EquipId, eo.UnitPrice, eo.Qty, e.Brand, e.Model  " +
                                 "FROM [EquipmentOrder] eo, [Equipment] e " +
                                  "WHERE eo.EquipId = e.EquipId " +
                                  "AND eo.OrderId = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        List<EquipmentOrder> equipmentOrdersList = new List<EquipmentOrder>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                EquipmentOrder equipOrder = new EquipmentOrder(reader["Qty"].ToInt(), Convert.ToDouble(reader["UnitPrice"]), reader["Brand"].ToString(), reader["Model"].ToString());
                                equipOrder.EquipmentId = reader["EquipId"].ToInt();
                                equipmentOrdersList.Add(equipOrder);
                                
                            }

                            orderWrapper.EquipmentOrderProp = equipmentOrdersList.ToArray();
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    finally
                    {
                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                }
            }

            return orderWrapper;
        }

        public List<OrderWrapper> GetAll()
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Select All";
            List<OrderWrapper> list = new List<OrderWrapper>();



            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql = "SELECT o.OrderId, o.OrderDate, o.IsOrderComplete, s.SupplierId, s.Name, o.EffectiveDate FROM [Order] o, [Supplier] s " +
                                  "WHERE o.SupplierId = s.SupplierId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                            while (reader.Read())
                            {
                                OrderWrapper orderWrapper = new OrderWrapper();
                                orderWrapper.OrderProp = new Order(reader["OrderId"].ToInt(), Convert.ToBoolean(reader["IsOrderComplete"].ToString()), Convert.ToDateTime(reader["OrderDate"].ToString()), reader["Name"].ToString(), Convert.ToDateTime(reader["EffectiveDate"].ToString()));
                                orderWrapper.OrderProp.SupplierId = reader["SupplierId"].ToInt();
                                list.Add(orderWrapper);
                            }

                        }
                        conn.Close();
                        foreach(var orderWrapper in list)
                        {
                            if (orderWrapper.OrderProp.EffectiveDate < DateTime.Now && !orderWrapper.OrderProp.IsOrderComplete)
                            {
                                orderWrapper.OrderProp.IsOrderComplete = true;
                                Update(orderWrapper);
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    //finally
                    //{
                    //    Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                    //    conn.Close();
                    //}
                }
            }

            return list;
        }

        public bool UpdateEquipment(int orderId, EquipmentOrder equipmentOrder)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "UPDATE [EquipmentOrder] " +
                        "SET UnitPrice=@UnitPrice, Qty=@Qty " +
                        "WHERE EquipId = @EquipId " +
                        "AND OrderId = @OrderId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UnitPrice", equipmentOrder.UnitPrice);
                    cmd.Parameters.AddWithValue("@Qty", equipmentOrder.Qty);
                    cmd.Parameters.AddWithValue("@EquipId", equipmentOrder.EquipmentId);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    finally
                    {
                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                }
            }

            return status;

        }

        public bool DeleteEquipment(int orderId, int equipmentId)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "DELETE FROM [EquipmentOrder] " +
                        "WHERE EquipId = @EquipId " +
                        "AND OrderId = @OrderId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@EquipId", equipmentId);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }

                    finally
                    {
                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                }
            }

            return status;

        }

        private bool OrderComplete(SqlConnection conn, int? orderId)
        {
            string sqlGetAllEquipIdsAndQty = "SELECT [EquipId], [Qty] FROM [EquipmentOrder] WHERE [OrderId] = @OrderId";
            SqlCommand cmdGetAllEquipIdsAndQty = new SqlCommand(sqlGetAllEquipIdsAndQty, conn);
            cmdGetAllEquipIdsAndQty.Parameters.AddWithValue("@OrderId", orderId);
            List<EquipmentOrder> equipmentOrdersList = new List<EquipmentOrder>();

            try
            {
                using (SqlDataReader reader = cmdGetAllEquipIdsAndQty.ExecuteReader())
                { 
                    while (reader.Read())
                    {
                        EquipmentOrder equipmentOrder = new EquipmentOrder();
                        equipmentOrder.EquipmentId = reader["EquipId"].ToInt();
                        equipmentOrder.Qty = reader["Qty"].ToInt();
                        equipmentOrdersList.Add(equipmentOrder);
                    }
                }

                foreach(var equipmentOrder in equipmentOrdersList)
                {
                    int quantity = equipmentOrder.Qty;
                    int equipmentId = equipmentOrder.EquipmentId;
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [EquipmentInStock] ([EquipId]) VALUES(@EquipId)", conn);
                    sqlCommand.Parameters.AddWithValue("@EquipId", equipmentId);

                    for (int i = 1;  i <= quantity; i++)
                    {
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"FAILED: OrderComplete");
                Logger.Log($"{ex.ToString()}");
                return false;
            }

            return true;
        }

        public bool Update(OrderWrapper orderWrapper)
        {
            string modelName = MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("DAL", "");
            string opType = "Update";
            bool status = false;

            if(orderWrapper.OrderProp.EffectiveDate < DateTime.Now && !orderWrapper.OrderProp.IsOrderComplete)
            {
                orderWrapper.OrderProp.IsOrderComplete = true;
            }

            using (SqlConnection conn = DAL.GetConnection())
            {
                if (conn != null)
                {
                    string sql =
                        "UPDATE [Order] " +
                        "SET OrderDate=@OrderDate, IsOrderComplete=@IsOrderComplete, SupplierId=@SupplierId, EffectiveDate=@EffectiveDate " +
                        "WHERE OrderId = @Id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", orderWrapper.OrderProp.Id);
                    cmd.Parameters.AddWithValue("@OrderDate", orderWrapper.OrderProp.OrderDate);
                    cmd.Parameters.AddWithValue("@IsOrderComplete", orderWrapper.OrderProp.IsOrderComplete);
                    cmd.Parameters.AddWithValue("@SupplierId", orderWrapper.OrderProp.SupplierId);
                    cmd.Parameters.AddWithValue("@EffectiveDate", orderWrapper.OrderProp.EffectiveDate);

                    try
                    {
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            status = true;
                            Logger.Log($"SUCCESS: {opType} {modelName}");

                        }

                        //TODO: Insert into EquipmentInStock if order complete is true
                        if (orderWrapper.OrderProp.IsOrderComplete == true)
                        {
                            OrderComplete(conn, orderWrapper.OrderProp.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"FAILED: {opType} {modelName}");
                        Logger.Log($"{ex.ToString()}");
                    }
                    finally
                    {
                        Logger.Log("Closing the SqlConnection" + Environment.NewLine);
                        conn.Close();
                    }
                }
            }

            return status;
        }

    }
}