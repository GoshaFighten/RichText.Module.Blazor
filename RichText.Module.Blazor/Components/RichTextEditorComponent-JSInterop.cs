﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace RichText.Module.Blazor.Components {
    public partial class RichTextEditorComponentBase : ComponentBase {
        [Inject] IJSRuntime JsRuntime { get; set; }
        protected async Task InvokeJsAsync(string methodName, params object[] args) {
            await JsRuntime.InvokeAsync<string>(methodName, args);
        }
        protected async Task InitClientSideAsync(bool firstRender) {
            if (firstRender) {
                await InvokeJsAsync("RichTextEditorComponent.Init", GetId(), ComponentModel.ReadOnly, ComponentModel.Value, DotNetObjectReference.Create(this));
            }
        }
        protected async Task ClientDisposeAsync() {
            await InvokeJsAsync("RichTextEditorComponent.Dispose", GetId());
        }
        [JSInvokable]
        public Task SaveDocument(string document) {
            ComponentModel.SetValueFromUI(document);
            return Task.CompletedTask;
        }
    }
}
