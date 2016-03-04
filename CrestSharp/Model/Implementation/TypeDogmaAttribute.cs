using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrestSharp.Model.Implementation
{
    public class TypeDogmaAttribute : ITypeDogmaAttribute
    {
        public ICrestDogmaAttribute Attribute { get; set; }

        public double? Value { get; set; }
    }
}
