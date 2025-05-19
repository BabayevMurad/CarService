using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Entities.Dto_s
{
    public class CarRepairDto
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int CarId { get; set; }
        public int IssueId { get; set; }
        public int UserId { get; set; }
    }
}
