using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetailStorePrj.Data
{
    public class Workstation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CurrentRetailStore")]
        public Guid RetailStoreId { get; set; }
        public RetailStore CurrentRetailStore { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
