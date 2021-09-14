﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SamaService
{
    public static class MySqlClass
    {
        /// <summary>
        /// تغییر وصعیت رکورد ثبت شده
        /// </summary>
        /// <param name="id">شناسه رکورد</param>
        /// <returns></returns>
        public static bool UpdateTagRecord(int id)
        {
            string cs = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";
            using (var conn = new MySqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    var cmdString = $"update  schooldb.tagrecive set Registered = '0' where ID = '{id}' ;";
                    var cmd = new MySqlCommand(cmdString, conn);
                    var result = cmd.ExecuteNonQuery();
                    conn.Clone();
                    return Convert.ToBoolean(result);
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLog(e, "UpdateTagRecord");
                    conn.Clone();
                    return false;
                }
            }
        }
        /// <summary>
        /// خواندن ثبت جدید از بانک مای اس کیوال
        /// </summary>
        /// <returns></returns>
        public static List<View_TagList> ReaderSQL()
        {
            string cs = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";
            var list = new List<View_TagList>();
            using (var conn = new MySqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    var cmdString = "SELECT * FROM schooldb.tagrecive where Registered = '1'";
                    var cmd = new MySqlCommand(cmdString, conn);
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        list.Add(new View_TagList()
                        {
                            ID = result.GetInt32(0),
                            Tag = result.GetString(1),
                            dateRegister = result.GetDateTime(2),
                            Reg = result.GetInt32(3),
                            TypeImport = result.GetInt32(4),
                        });
                    }
                    conn.Clone();
                    return list;
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLog(e, "ReaderSQL");
                    conn.Clone();
                    return null;
                }
            }
        }
        /// <summary>
        /// تغییر وضعیت لیستی از رکوردها در مای اس کیو ال
        /// </summary>
        /// <param name="listDisabel">لیست شناسه ها</param>
        /// <returns></returns>
        public static bool UpdateTagRecordList(List<int> listDisabel)
        {
            string cs = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";
            using (var conn = new MySqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    foreach (var id in listDisabel)
                    {
                        var cmdString = $"update  schooldb.tagrecive set Registered = '0' where ID = '{id}' ;";
                        var cmd = new MySqlCommand(cmdString, conn);
                        var result = cmd.ExecuteNonQuery();
                    }
                    conn.Clone();
                    return true;
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLog(e, "UpdateTagRecordList");
                    conn.Clone();
                    return false;
                }
            }
        }
        /// <summary>
        /// تغییر وضعیت لیستی از رکوردها در مای اس کیو ال
        /// </summary>
        /// <param name="listDisabel">لیست شناسه ها</param>
        /// <returns></returns>
        public static bool rollbackTagRecordList(List<int> listDisabel)
        {
            string cs = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";
            using (var conn = new MySqlConnection(cs))
            {
                try
                {
                    conn.Open();
                    foreach (var id in listDisabel)
                    {
                        var cmdString = $"update  schooldb.tagrecive set Registered = '1' where ID = '{id}' ;";
                        var cmd = new MySqlCommand(cmdString, conn);
                        var result = cmd.ExecuteNonQuery();
                    }
                    conn.Clone();
                    return true;
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLog(e, "rollbackTagRecordList");
                    conn.Clone();
                    return false;
                }
            }
        }
    }
}