using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface IMechanicAddWork
    {
        Task AcceptMechanic(int id);
        Task RejectMechanic(int id);
        Task<List<Mechanic>> GetAllMechanics();
        Task<List<Mechanic>> GetAllMechanicsWork();
        Task<Mechanic> GetMechanicById(int id);
        Task AddInfoMenhanic(Mechanic mechanic);
    }
}
