using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class RepairHistory
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime RepairDate { get; set; }
        public decimal Cost { get; set; }
        public virtual Car Car { get; set; }
    }
}
