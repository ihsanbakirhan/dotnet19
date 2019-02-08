using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Objects
{
    public class Product
    {
        public Int64 productId { get; set; }
        public String productName { get; set; }
        public String description { get; set; }
        public int unitId { get; set; }
        public int supplierId { get; set; }


        public Product(Int64 productId, String productName, String description, int unitId, int supplierId)
        {
            this.productId = productId;
            this.productName = productName;
            this.description = description;
            this.unitId = unitId;
            this.supplierId = supplierId;
        }
    }
}
