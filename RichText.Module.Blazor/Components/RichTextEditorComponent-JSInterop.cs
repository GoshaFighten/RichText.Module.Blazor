using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RichText.Module.Blazor.Components {
    public partial class RichTextEditorComponent : ComponentBase {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }
        private string ScriptFilePath { get { return "_content/RichText.Module.Blazor/scripts.js"; } }
        public TaskCompletionSource<bool> LoadedTcs { get; } = new TaskCompletionSource<bool>();
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            builder.OpenElement(0, "script");
            builder.AddAttribute(1, "src", ResolveUrl(NavigationManager, ScriptFilePath));
            builder.AddAttribute(2, "async", true);
            builder.AddAttribute(3, "onload", EventCallback.Factory.Create<ProgressEventArgs>(this, (args) => LoadedTcs.TrySetResult(true)));
            builder.CloseElement();

            builder.OpenElement(4, "div");
            builder.AddAttribute(5, "id", "rich-container");
            builder.CloseElement();
        }
        public string ResolveUrl(NavigationManager navigationManager, string url) {
            var sb = new StringBuilder();
            sb.Append(navigationManager.BaseUri);
            sb.Append(url);
            url = navigationManager.ToBaseRelativePath(sb.ToString());
            url = navigationManager.ToAbsoluteUri(url).PathAndQuery;
            return url;
        }
        protected async Task InvokeJsAsync(string methodName, params object[] args) {
            await JsRuntime.InvokeAsync<string>(methodName, args);
        }
        protected async Task InitClientSideAsync(bool firstRender) {
            if (firstRender) {
                await InvokeJsAsync("RichTextEditorComponent.Init", DotNetObjectReference.Create(this));
            }
        }
        protected async Task ClientDisposeAsync() {
            await InvokeJsAsync("RichTextEditorComponent.Dispose");
        }
    }
}
