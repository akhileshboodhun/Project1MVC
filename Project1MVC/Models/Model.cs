using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.Models
{
    public abstract class Model<T>
    {
        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(T);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }

            set
            {
                Type myType = typeof(T);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
    }
}