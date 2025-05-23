using CarService.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarService.DataAccess.Concrete
{
    public class MechanicAddWork : IMechanicAddWork
    {
        private readonly AppDataContext _context;

        public MechanicAddWork(AppDataContext context)
        {
            _context = context;
        }
        public async Task AcceptMechanic(int id)
        {
            var mechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);

            mechanic!.IsAccepted = true;

            await _context.SaveChangesAsync();
        }

    }
}
