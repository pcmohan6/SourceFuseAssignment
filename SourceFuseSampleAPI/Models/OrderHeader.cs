using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Mysqlx.Crud;

namespace SourceFuseSampleAPI.Models
{
    public class OrderHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderID { get; set; }

        [NotNull]
        public string OrderNum { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductID { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OrderAmount { get; set; }
    
    }
}
