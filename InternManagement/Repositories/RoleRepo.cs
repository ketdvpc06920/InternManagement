using InternManagement.Data;
using InternManagement.Models;

namespace InternManagement.Repositories
{
    public class RoleRepo
    {
        private readonly AppDbContext _context;

        public RoleRepo(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(long id)
        {
            return _context.Roles.FirstOrDefault(s => s.RoleId == id);
        }

        public Role CreateRole(Role major)
        {
            _context.Roles.Add(major);
            _context.SaveChanges();
            return major;
        }

        public Role UpdateRole(Role major)
        {
            _context.Roles.Update(major);
            _context.SaveChanges();
            return major;
        }

        public bool DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}
