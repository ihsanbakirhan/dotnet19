using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Objects
{
    public class Variant
    {
        public Int64 variantId { get; set; }
        public String variantName { get; set; }
        public Decimal unitPrice { get; set; }
        public Decimal cost { get; set; }
        public Int16 stock { get; set; }

        public Variant(Int64 variantId, String variantName, Decimal unitPrice, Decimal cost, Int16 stock)
        {
            this.variantId = variantId;
            this.variantName = variantName;
            this.unitPrice = unitPrice;
            this.cost = cost;
            this.stock = stock;
        }
    }
}
