using System;
using System.Linq;
using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.ViewModels
{
    public class GearListViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int Order { get; set; }
        public bool Favorite { get; set; }

        public List<GearCollection> GearCollections { get; set; } = new();
    }
}
