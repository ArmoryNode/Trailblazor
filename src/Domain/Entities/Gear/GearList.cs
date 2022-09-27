using System.Text;
using Trailblazor.Domain.Common;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Domain.Entities.Gear
{
    public record GearList : AuditableEntity, ISoftDeletableEntity, ISearchable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int Order { get; set; }
        public bool Favorite { get; set; }

        public WeightUnit PreferredUnit { get; set; } = WeightUnit.Pounds;

        public List<GearCollection> GearCollections { get; set; } = new();

        public DateTimeOffset? DeletedOn { get; set; }

        public string SearchText { get; private set; } = string.Empty;

        public GearList()
        {
        }

        public GearList(GearListViewModel viewModel)
        {
            UpdateFrom(viewModel);
        }

        public void UpdateFrom(GearListViewModel viewModel)
        {
            if (Name != viewModel.Name)
                Name = viewModel.Name;

            if (Description != viewModel.Description)
                Description = viewModel.Description;

            if (Order != viewModel.Order)
                Order = viewModel.Order;

            if (Favorite != viewModel.Favorite)
                Favorite = viewModel.Favorite;

            if (!GearCollections.SequenceEqual(viewModel.GearCollections))
                GearCollections = viewModel.GearCollections;

            if (PreferredUnit != viewModel.PreferredUnit)
                PreferredUnit = viewModel.PreferredUnit;
        }

        public void UpdateSearchText()
        {
            var delimiter = ISearchable.delimiter;
            var searchText = new StringBuilder(delimiter);

            if (!string.IsNullOrWhiteSpace(Name))
                searchText.Append(Name + delimiter);

            if (!string.IsNullOrWhiteSpace(Description))
                searchText.Append(Description + delimiter);

            if (!string.IsNullOrWhiteSpace(CreatedBy))
                searchText.Append(CreatedBy + delimiter);

            if (!string.IsNullOrWhiteSpace(LastModifiedBy))
                searchText.Append(LastModifiedBy + delimiter);

            SearchText = searchText.ToString();
        }
    }
}
