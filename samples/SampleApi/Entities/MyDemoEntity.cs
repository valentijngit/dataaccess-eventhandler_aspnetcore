using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.DataAccess.Entities;

namespace SampleApi.Entities
{
    public class MyDemoEntity : EntityBase
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
