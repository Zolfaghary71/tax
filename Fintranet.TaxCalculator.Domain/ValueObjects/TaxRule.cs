using Fintranet.TaxCalculator.Domain.Entities;

namespace Fintranet.TaxCalculator.Domain.ValueObjects
{
    public class TaxRule
    {
        public City City { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Amount { get; set; }
    }
}