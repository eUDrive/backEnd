using eUDrive.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUDrive.Domains.Entities.Order
{
    public class OrderData
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderTypes Type { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
