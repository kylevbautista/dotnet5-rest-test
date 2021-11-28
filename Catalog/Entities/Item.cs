using System;

namespace Catalog.Entities{
     public record Item{
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