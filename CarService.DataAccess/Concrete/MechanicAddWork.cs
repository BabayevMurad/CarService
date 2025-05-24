using CarService.DataAccess.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        public async Task RejectMechanic(int id)
        {
            var mechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic != null)
            {
                _context.Mechanics.Remove(mechanic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Mechanic>> GetAllMechanics()
        {
            var mechanics = await _context.Mechanics.Where(m => m.IsAccepted == true).ToListAsync();
            return mechanics;
        }

        public async Task<List<Mechanic>> GetAllMechanicsWork()
        {
            var mechanics = await _context.Mechanics.Where(m => m.IsAccepted == false).ToListAsync();
            return mechanics;
        }

        public async Task<Mechanic> GetMechanicById(int id)
        {
            var mechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);
            return mechanic!;
        }

        public async Task AddInfoMenhanic(Mechanic mechanic)
        {
            var existingMechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.Username == mechanic.Username);
            
            existingMechanic!.Name = mechanic.Name;
            existingMechanic.Surname = mechanic.Surname;
            existingMechanic.WorkType = mechanic.WorkType;


            await _context.SaveChangesAsync();
        }
    }
}
