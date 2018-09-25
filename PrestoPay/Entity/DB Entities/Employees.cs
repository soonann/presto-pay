using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string Employee_Email { get; set; }
        public int Employee_Pay { get; set; }
        public int Employee_View { get; set; }
        public string busi_id { get; set; }

    }
}