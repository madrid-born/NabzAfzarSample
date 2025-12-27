
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace NabzAfzarSample.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NabzAfzarSample.App_DataAccess.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}