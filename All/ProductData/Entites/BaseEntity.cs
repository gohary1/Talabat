using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites
{
    public class BaseEntity<T>
    {
        public T id { get; set; }
    }
}
