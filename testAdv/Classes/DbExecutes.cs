using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using testAdv.Models;

namespace testAdv.Classes
{
    public class DbExecutes
    {

        public static void DbClear()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    using (var db = new ChatContext())
                    {
                        db.Database.ExecuteSqlCommand("Delete from dbo.Messages where Date < DATEADD(DAY, -1, GETDATE())");
                    }
                    Thread.Sleep(1800);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}