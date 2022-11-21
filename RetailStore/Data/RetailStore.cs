using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStorePrj.Data
{
    public class RetailStore
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(80)]
        public string Address { get; set; }
        [StringLength(80)]
        public string IpAddress { get; set; }
        public DateTime? OpenningDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsTel { get; set; }
        [ForeignKey("CurrentParent")]
        public Guid? ParentId { get; set; }
        public RetailStore CurrentParent { get; set; }
        public  List<RetailStore> Childs { get; set; }
    }
}
