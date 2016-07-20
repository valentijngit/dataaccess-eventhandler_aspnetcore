using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using Digipolis.DataAccess.Context;
using Digipolis.DataAccess.Options;
using SampleApi.Entities;

namespace SampleApi.DataAccess
{
 
        public class EntityContext : EntityContextBase
        {
            public EntityContext(IOptions<EntityContextOptions> options) : base(options)
            {
            }


           public DbSet<MyDemoEntity> KeyTypes { get; set; }
        


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

                base.OnModelCreating(modelBuilder);

                // PostgreSQL uses schema 'main' - not dbo.
                modelBuilder.HasDefaultSchema("main");

            


            }
        }
    }