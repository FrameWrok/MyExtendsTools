using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class MarkDownTools
    {
        public string MarkDownTextToHtml(string markdownText)
        {
            // 创建Markdown转换器
            var pipeline = new MarkdownPipelineBuilder().Build();

            // 将Markdown文本转换为HTML
            string html = Markdig.Markdown.ToHtml(markdownText, pipeline);

            return html;
        }
    }
}
