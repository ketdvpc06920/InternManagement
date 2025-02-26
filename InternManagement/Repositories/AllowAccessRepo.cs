using InternManagement.Data;
using InternManagement.Models;

namespace InternManagement.Repositories
{
    public class AllowAccessRepo
    {
        private readonly AppDbContext _context;

        public AllowAccessRepo(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<AllowAccess> GetAllowAccesss()
        {
            return _context.AllowAccesses.ToList();
        }

        public AllowAccess GetAllowAccessById(long id)
        {
            return _context.AllowAccesses.FirstOrDefault(s => s.RoleId == id);
        }

        public AllowAccess CreateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Add(allowAccess);
            _context.SaveChanges();
            return allowAccess;
        }

        public AllowAccess UpdateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Update(allowAccess);
            _context.SaveChanges();
            return allowAccess;
        }

        public bool DeleteAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Remove(allowAccess);
            _context.SaveChanges();
            return true;
        }
    }
}
