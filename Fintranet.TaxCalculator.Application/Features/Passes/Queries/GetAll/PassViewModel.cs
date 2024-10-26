namespace Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetAll
{
    public class PassViewModel
    {
        public Guid Id { get; set; }
        public decimal Tax { get; set; }
        public DateTime DatePassed { get; set; }
    }
}