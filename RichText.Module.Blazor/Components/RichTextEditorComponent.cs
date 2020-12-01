using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using RichText.Module.Blazor.Components.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RichText.Module.Blazor.Components {
    public partial class RichTextEditorComponent : ComponentBase, IDisposable {
        [Parameter]
        public RichTextEditorComponentModel ComponentModel { get; set; }
        protected bool Rendered { get; private set; }
        public static RenderFragment Create(RichTextEditorComponentModel componentModel) => builder => {
            builder.OpenComponent<RichTextEditorComponent>(0);
            builder.AddAttribute(1, nameof(ComponentModel), componentModel);
            builder.CloseComponent();
        };
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (await LoadedTcs.Task) {
                Rendered = true;
                await InitClientSideAsync(firstRender);
            }
        }
        protected async void OnDisposeCore() {
            if (Rendered) {
                await ClientDisposeAsync();
            }
        }

        void IDisposable.Dispose() => OnDisposeCore();
    }
}
