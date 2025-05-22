using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class AdminChatUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int AdminId { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}
