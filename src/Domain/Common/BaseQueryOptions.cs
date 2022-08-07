using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trailblazor.Domain.Common
{
    public record BaseQueryOptions
    {
        public Guid? Id { get; init; }
        public string? SearchText { get; init; }
    }
}
