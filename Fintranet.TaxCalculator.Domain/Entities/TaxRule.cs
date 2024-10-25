using Fintranet.TaxCalculator.Domain.Common;
using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.Entities
{
    public class TaxRule:BaseEntity
    {
        public City City { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Amount { get; set; }
    }
}