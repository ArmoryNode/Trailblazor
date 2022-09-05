using System;
using System.Linq;

namespace Trailblazor.Domain.Common
{
    public interface ISearchable
    {
        public const string delimiter = "|";

        string SearchText { get; }

        void UpdateSearchText();
    }
}
