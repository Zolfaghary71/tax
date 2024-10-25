﻿using Fintranet.TaxCalculator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fintranet.TaxCalculator.Domain.DomainServices.Contracts
{
    public interface ICongestionTaxStratgy
    {
        Task<IEnumerable<Pass>> CalculateTaxAsync(IEnumerable<Pass> passes);
    }
}