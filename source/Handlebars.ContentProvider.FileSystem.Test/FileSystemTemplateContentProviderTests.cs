using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Handlebars.Core;
using Handlebars.ContentProvider.FileSystem;

namespace Handlebars.ContentProvider.FileSystem.Test
{
    public class FileSystemTemplateContentProviderTests
    {
        [Fact]
        public void CanLoadAViewWithALayout()
        {
            //Given a layout in a subfolder
            var configuration = new HandlebarsConfiguration
            {
                TemplateContentProvider = new FileSystemTemplateContentProvider()
            };

            //When a viewengine renders that view
            var handlebars = new HandlebarsEngine(configuration);
            var template = handlebars.CompileView("hbs\\views\\sub\\someview1.hbs");
            var output = template.Render(null);
            
            //Then the correct output should be rendered
            Assert.Equal(
                NormalizeNewLine("layout start\r\nThis is the body\r\nlayout end"),
                NormalizeNewLine(output));
        }
        [Fact]
        public void CanLoadAViewWithALayoutInTheRoot()
        {
            //Given a layout in the root
            var configuration = new HandlebarsConfiguration
            {
                TemplateContentProvider = new FileSystemTemplateContentProvider()
            };

            //When a viewengine renders that view
            var handlebars = new HandlebarsEngine(configuration);
            var template = handlebars.CompileView("hbs\\views\\sub\\someview2.hbs");
            var output = template.Render(null);

            //Then the correct output should be rendered
            Assert.Equal(
                NormalizeNewLine("layout start\r\nThis is the body\r\nlayout end"),
                NormalizeNewLine(output));
        }

        private static string NormalizeNewLine(string text)
        {
            text = text.Replace("\r", "");
            return text.Replace("\n", Environment.NewLine);
        }
    }
}
