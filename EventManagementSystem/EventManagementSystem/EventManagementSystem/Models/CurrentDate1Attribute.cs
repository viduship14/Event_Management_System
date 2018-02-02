using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class CurrentDate1Attribute: ValidationAttribute
    {
        public CurrentDate1Attribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt < DateTime.Now)
            {
                return false;
            }
            return true;
        }




    }
}