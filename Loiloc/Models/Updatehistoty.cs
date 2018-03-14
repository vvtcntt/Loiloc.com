using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loiloc.Models;
namespace Loiloc.Models
{
    public class Updatehistoty
    {
        public static void UpdateHistory(string task,string FullName,string UserID)
        {
            LoilocContext db = new LoilocContext();
             tblHistoryLogin tblhistorylogin = new tblHistoryLogin();
            tblhistorylogin.FullName = FullName;
            tblhistorylogin.Task = task;
            tblhistorylogin.idUser = int.Parse(UserID);
            tblhistorylogin.DateCreate = DateTime.Now;
            tblhistorylogin.Active = true;
            
            db.tblHistoryLogins.Add(tblhistorylogin);
            db.SaveChanges();
           
        }
    }
}