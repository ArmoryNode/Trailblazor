using Microsoft.AspNetCore.Components;

namespace Trailblazor.Client.Shared.Components
{
    public sealed class ContextMenuAction : ComponentBase, IDisposable
    {
        [Parameter]
        public string Text { get; set; } = string.Empty;

        [Parameter]
        public Action<ContextMenu> Action { get; set; } = async (context) => await context.CloseMenu(null);

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [CascadingParameter]
        private ContextMenu ContextMenu { get; set; } = null!;

        protected override void OnInitialized()
        {
            ContextMenu.AddAction(this);
        }

        public void Dispose()
        {
            ContextMenu.RemoveAction(this);
        }
    }
}
