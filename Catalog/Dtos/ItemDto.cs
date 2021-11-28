using System;

namespace Catalog.Dtos{
    public record ItemDto{
        public Guid ID {
             get;
             init;
        }
        public string Name {
            get;
            set;
        }
        public decimal Price{
            get;
            init;
        }
        public DateTimeOffset CreatedDate{
            get;
            init;
        }
     }
}