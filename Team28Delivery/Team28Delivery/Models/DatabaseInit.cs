using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Team28Delivery.Models
{
    public class DatabaseInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {


    }
}