namespace Fintranet.TaxCalculator.Application.Features.Pass.Queries
{
    public class PassDto
    {
        public Guid Id { get; set; }
        public decimal Tax { get; set; }
        public DateTime DatePassed { get; set; }
    }
}