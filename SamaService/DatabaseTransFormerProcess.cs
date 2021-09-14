﻿using System;
using System.Linq;
using SamaService.Models;

namespace SamaService
{
    public class DatabaseTransFormerProcess
    {
        /// <summary>
        /// پردازش و انتقال بین دو سرور و ثبت و امحای تردد های ثبت شده
        /// </summary>
        public static void TransformDataBase()
        {
            using (var dbx = new ApplicationDbContext())
            {
                using (var trans = dbx.Database.BeginTransaction())
                {
                    try
                    {
                        var listForDisableinMySql = MySqlClass.ReaderSQL();// لیست تگ های ثبت نشده
                        foreach (var tagList in listForDisableinMySql)// ثبت در بانک اطلاعاتی اس کیوال
                        {
                            dbx.TagRecorders.Add(new TagRecorder()
                            {
                                TagID = tagList.Tag.Remove(tagList.Tag.Length - 1),
                                DateTimeRegister = tagList.dateRegister,
                                MysqlID = tagList.ID,
                                SMS = false,
                                Type = Convert.ToBoolean(tagList.TypeImport),
                                Enables = true,
                            });
                        }
                        dbx.SaveChanges();
                        var resultMysql = MySqlClass.UpdateTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                        if (resultMysql)
                        {

                            trans.Commit();
                        }
                        else
                        {
                            MySqlClass.rollbackTagRecordList(listForDisableinMySql.Select(x => x.ID).ToList());
                            Logger.WriteMessageLog("Error TransformDataBase");
                            trans.Rollback();

                        }

                    }
                    catch (Exception e)
                    {
                        Logger.WriteErrorLog(e, "TransformDataBase");
                        trans.Rollback();
                    }
                }
            }
        }
    }
}
