using System;
using System.Collections.Generic;
using DM.DTO;
using MySql.Data.MySqlClient;

namespace DM.Services
{
    public interface IMySqlServiceRepository
    {
        bool UpdateTagRecord(int id);
        List<TagListDTO> ReaderSQL();
        bool UpdateTagRecordList(List<int> listDisabel);
        bool rollbackTagRecordList(List<int> listDisabel);

    }

    public class MySqlServiceRepository : IMySqlServiceRepository
    {
        private MySqlConnection _mySqlConnection;

        public MySqlServiceRepository()
        {
            var strConnMySql = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";
            _mySqlConnection = new MySqlConnection(strConnMySql);
        }
        public bool UpdateTagRecord(int id)
        {

            try
            {
                _mySqlConnection.Open();
                var cmdString = $"update  schooldb.tagrecive set Registered = '0' where ID = '{id}' ;";
                var cmd = new MySqlCommand(cmdString, _mySqlConnection);
                var result = cmd.ExecuteNonQuery();
                _mySqlConnection.Clone();
                return Convert.ToBoolean(result);
            }
            catch
            {
                //Logger.WriteErrorLog(e, "UpdateTagRecord");
                _mySqlConnection.Clone();
                return false;
            }

        }

        public List<TagListDTO> ReaderSQL()
        {
            var list = new List<TagListDTO>();

            try
            {
                _mySqlConnection.Open();
                var cmdString = "SELECT * FROM schooldb.tagrecive where Registered = '1'";
                var cmd = new MySqlCommand(cmdString, _mySqlConnection);
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    list.Add(new TagListDTO()
                    {
                        ID = result.GetInt32(0),
                        Tag = result.GetString(1),
                        dateRegister = result.GetDateTime(2),
                        Reg = result.GetInt32(3),
                        TypeImport = result.GetInt32(4),
                    });
                }
                _mySqlConnection.Clone();
                return list;
            }
            catch
            {
                //Logger.WriteErrorLog(e, "ReaderSQL");
                _mySqlConnection.Clone();
                return null;
            }

        }

        public bool UpdateTagRecordList(List<int> listDisabel)
        {

            try
            {
                _mySqlConnection.Open();
                foreach (var id in listDisabel)
                {
                    var cmdString = $"update  schooldb.tagrecive set Registered = '0' where ID = '{id}' ;";
                    var cmd = new MySqlCommand(cmdString, _mySqlConnection);
                    var result = cmd.ExecuteNonQuery();
                }
                _mySqlConnection.Clone();
                return true;
            }
            catch
            {
                // Logger.WriteErrorLog(e, "UpdateTagRecordList");
                _mySqlConnection.Clone();
                return false;
            }

        }

        public bool rollbackTagRecordList(List<int> listDisabel)
        {

            try
            {
                _mySqlConnection.Open();
                foreach (var id in listDisabel)
                {
                    var cmdString = $"update  schooldb.tagrecive set Registered = '1' where ID = '{id}' ;";
                    var cmd = new MySqlCommand(cmdString, _mySqlConnection);
                    var result = cmd.ExecuteNonQuery();
                }
                _mySqlConnection.Clone();
                return true;
            }
            catch
            {
                //Logger.WriteErrorLog(e, "rollbackTagRecordList");
                _mySqlConnection.Clone();
                return false;
            }

        }
    }
}