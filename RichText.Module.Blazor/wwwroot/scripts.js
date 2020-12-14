window.RichTextEditorComponent = window.RichTextEditorComponent || {
    _riches: {},
    Init: function (id, readOnly, documentAsBase64, dotnetHelper) {
        const options = DevExpress.RichEdit.createOptions();
        options.confirmOnLosingChanges.enabled = false;
        options.readOnly = readOnly;
        options.width = '100%';
        options.height = '900px';
        options.events.saving = function (s, e) {
            e.handled = true;
            dotnetHelper.invokeMethodAsync('SaveDocument', e.base64);
        };
        options.events.documentChanged = function (s, e) {
            s.saveDocument(DevExpress.RichEdit.DocumentFormat.OpenXml);
        };
        var richElement = document.getElementById(id);
        const richEdit = DevExpress.RichEdit.create(richElement, options);
        window.RichTextEditorComponent._riches[id] = { richEdit, dotnetHelper };
        if (documentAsBase64) {
            richEdit.openDocument(
                documentAsBase64,
                'DocumentName',
                DevExpress.RichEdit.DocumentFormat.OpenXml
            );
        }
    },
    Dispose: function (id) {
        if (window.RichTextEditorComponent._riches[id]) {
            for (const comp in window.RichTextEditorComponent._riches[id]) {
                window.RichTextEditorComponent._riches[id][comp].dispose();
            }
            delete window.RichTextEditorComponent._riches[id];
        }
    }
}