using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class CarRepair
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
        public int UserId { get; set; }
}
}
