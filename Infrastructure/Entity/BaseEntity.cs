using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Entity
{
    public class BaseEntity
    {
        [NotMapped]
        public object BaseId
        {
            get
            {
                var property =
                    this.GetType()
                         .GetProperties().FirstOrDefault(prop => Attribute.IsDefined((MemberInfo)prop, typeof(KeyAttribute)));
                return property == null ? 0 : property.GetValue(this, null);
            }
        }
    }
}