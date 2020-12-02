using DevExpress.Spreadsheet;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RichText.Module.Blazor.Helper {
    [HtmlTargetElement("body")]
    class LinkTagHelperComponent : TagHelperComponent {
        private string style = "<link href=\"_content/RichText.Module.Blazor/styles.css\" rel=\"stylesheet\" />";
        private string script = "<script src=\"_content/RichText.Module.Blazor/scripts.js\" ></script>";
        public override int Order => 2;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase)) {
                output.PostContent.AppendHtml(style);
                output.PostContent.AppendLine("\r\n");
                output.PostContent.AppendHtml(script);
                //output.PostContent.AppendHtml(script);
            }
            return Task.CompletedTask;
        }
    }
}
