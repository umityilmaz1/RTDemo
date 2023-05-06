using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EFContext<TContext> where TContext : DbContext, new()
    {
        private static TContext _context;
        private EFContext()
        {

        }
        public static TContext GetInstance()
        {
            if (_context == null) 
                _context = new TContext();
            return _context;
        }
    }
}
