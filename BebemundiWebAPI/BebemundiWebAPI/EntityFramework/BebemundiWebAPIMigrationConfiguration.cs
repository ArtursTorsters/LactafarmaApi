using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.EntityFramework
{
    public class BebemundiWebAPIMigrationConfiguration: DbMigrationsConfiguration<BebemundiWebAPIContext>
    {
        public BebemundiWebAPIMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }
    }
}