(function () {
    debugger;
    alert('1');
})();
alert('5');
window.RichTextEditorComponent = window.RichTextEditorComponent || {
    Init: function () {
        alert('2');
    },
    Dispose: function () {
        alert('3');
    }
}