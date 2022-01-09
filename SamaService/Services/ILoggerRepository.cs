using System;
using System.IO;

namespace SamaService.Services
{
    public interface ILoggerRepository
    {
        void WriteErrorLog(Exception ex, string partName);
        void WriteMessageLog(string message);
        void WriteErrorSaveToDatabase(string message);
        void WriteMessageSenderLog(string message);
    }

    public class LoggerRepository : ILoggerRepository
    {


        public void WriteErrorLog(Exception ex, string partName)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(partName + ": " + DateTime.Now + ": " + ex.Source.Trim() + "; " +
                             ex.Message.Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // ignored
            }
        }

        public void WriteMessageLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\MessageLog.txt", true);
                sw.WriteLine(DateTime.Now + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // igroned
            }
        }

        public void WriteErrorSaveToDatabase(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\MessageLog.txt", true);
                //sw.WriteLine(DateTime.Now.Convert_PersianCalender() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // igroned
            };
        }

        public void WriteMessageSenderLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\MessageSenderLog.txt", true);
                //sw.WriteLine(DateTime.Now.Convert_PersianCalender() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                // igroned
            }
        }
    }
}