using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entity.Models
{
    public partial class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
