﻿using DevExpress.Spreadsheet;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RichText.Module.Blazor.Helper {
    [HtmlTargetElement("head")]
    class LinkTagHelperComponent : TagHelperComponent {
        private string style = "<link href=\"_content/RichText.Module.Blazor/styles.css\" rel=\"stylesheet\" />";
        private string script = "<script src=\"_content/RichText.Module.Blazor/scripts.js\" ></script>";
        private string styleRich = "<link href=\"_content/RichText.Module.Blazor/dx.richedit.css\" rel=\"stylesheet\" />";
        private string scriptRich = "<script src=\"_content/RichText.Module.Blazor/dx.richedit.min.js\" ></script>";
        public override int Order => 2;

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase)) {
                output.PostContent.AppendHtml(styleRich);
                output.PostContent.AppendLine("\r\n");
                output.PostContent.AppendHtml(style);
                output.PostContent.AppendLine("\r\n");
                output.PostContent.AppendHtml(scriptRich);
                output.PostContent.AppendLine("\r\n");
                output.PostContent.AppendHtml(script);
            }
            return Task.CompletedTask;
        }
    }
}
