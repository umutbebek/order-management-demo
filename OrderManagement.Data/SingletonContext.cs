
using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Entity.Orders;

// ReSharper disable InconsistentNaming

namespace OrderManagement.Data
{
    public class SingletonContext : IDisposable
    {
        private static readonly object _lock = new ();
        private static SingletonContext? _instance;
        private readonly OrderManagementContext _dbContext;
        private bool _disposed;

        private SingletonContext()
        {
            var options = new DbContextOptionsBuilder<OrderManagementContext>()
                .UseInMemoryDatabase(databaseName: "OrderManagementContextDb")
                .Options;

            _dbContext = new OrderManagementContext(options);

            AddSampleData();
        }

        public static SingletonContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonContext();
                        }
                    }
                }
                return _instance;
            }
        }

        public OrderManagementContext DbContext
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(SingletonContext));
                }
                return _dbContext;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SingletonContext()
        {
            Dispose(false);
        }

        private void AddSampleData()
        {
            DbContext.Orders.Add(new Order
            {
                ProductName = "Product 1",
                Price = 12.15m,
                Status = "Approved"
            });
            DbContext.Orders.Add(new Order
            {
                ProductName = "Product 2",
                Price = 32.60m,
                Status = "Rejected"
            });
            DbContext.Orders.Add(new Order
            {
                ProductName = "Product 3",
                Price = 25.00m,
                Status = "Approved"
            });
            DbContext.SaveChanges();
        }
    }
}
