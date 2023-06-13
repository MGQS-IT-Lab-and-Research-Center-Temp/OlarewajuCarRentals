using CarRentals.Context;
using CarRentals.Repository.Interface;
using CarRentals.Repository.Interfaces;

namespace CarRentals.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarRentalsContext _context;
        private bool _disposed = false;
        public IRoleRepository Roles { get; }
        public IUserRepository Users { get; }
        public ICategoryRepository Categories { get; }
        public ICarRepository Cars { get; }
        public ICommentRepository Comments { get; }

        public IBookingRepository Bookings { get; }

        public UnitOfWork(
            CarRentalsContext context,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICarRepository carRepository,
            ICommentRepository commentRepository,
            IBookingRepository bookings)
        {
            _context = context;
            Roles = roleRepository;
            Users = userRepository;
            Categories = categoryRepository;
            Cars = carRepository;
            Comments = commentRepository;
            Bookings = bookings;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
