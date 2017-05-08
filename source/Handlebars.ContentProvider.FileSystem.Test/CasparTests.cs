﻿using Xunit;
using Handlebars.Core;

namespace Handlebars.ContentProvider.FileSystem.Test
{
    public class CasparTests
    {
        [Fact]
        public void CanRenderCasparIndexTemplate()
        {
            var configuration = new HandlebarsConfiguration
            {
                TemplateContentProvider = new FileSystemTemplateContentProvider(new FileSystemConfiguration()
                {
                    PartialsSubPath = "partials" 
                })
            };

            var handlebars = new HandlebarsEngine(configuration);
            AddHelpers(handlebars);
            var renderView = handlebars.CompileView("hbs/casper/index.hbs");
            var output = renderView.Render(new
            {
                blog = new
                {
                    url = "http://someblog.com",
                    title = "This is the blog title"
                },
                posts = new[]
                {
                    new
                    {
                        title = "My Post Title",
                        image = "/someimage.png",
                        post_class = "somepostclass"
                    }
                }
            });
            var cq = CsQuery.CQ.CreateDocument(output);
            Assert.Equal("My Post Title", cq["h2.post-title a"].Text());
        }
        [Fact]
        public void CanRenderCasparPostTemplate()
        {
            var configuration = new HandlebarsConfiguration
            {
                TemplateContentProvider = new FileSystemTemplateContentProvider()
            };

            var handlebars = new HandlebarsEngine(configuration);
            AddHelpers(handlebars);
            var renderView = handlebars.CompileView("hbs/casper/post.hbs");
            var output = renderView.Render(new
            {
                blog = new
                {
                    url = "http://someblog.com",
                    title = "This is the blog title"
                },
                post = new
                {
                    title = "My Post Title",
                    image = "/someimage.png",
                    post_class = "somepostclass"
                }
            });
            var cq = CsQuery.CQ.CreateDocument(output);
            Assert.Equal("My Post Title", cq["h1.post-title"].Html());
        }

        private static void AddHelpers(IHandlebarsEngine handlebars)
        {
            handlebars.RegisterHelper("asset",
                (writer, context, arguments) => writer.Write("asset:" + string.Join("|", arguments)));
            handlebars.RegisterHelper("date",
                (writer, context, arguments) => writer.Write("date:" + string.Join("|", arguments)));
            handlebars.RegisterHelper("tags",
                (writer, context, arguments) => writer.Write("tags:" + string.Join("|", arguments)));
            handlebars.RegisterHelper("encode",
                (writer, context, arguments) => writer.Write("encode:" + string.Join("|", arguments)));
            handlebars.RegisterHelper("url", (writer, context, arguments) => writer.Write("url:" + string.Join("|", arguments)));
            handlebars.RegisterHelper("excerpt", (writer, context, arguments) => writer.Write("url:" + string.Join("|", arguments)));
        }

        [Fact]
        public void CanRenderCasparPostNoLayoutTemplate()
        {
            var configuration = new HandlebarsConfiguration
            {
                TemplateContentProvider = new FileSystemTemplateContentProvider()
            };

            var handlebars = new HandlebarsEngine(configuration);

            AddHelpers(handlebars);
            var renderView = handlebars.CompileView("hbs/casper/post-no-layout.hbs");
            var output = renderView.Render(new
            {
                post = new
                {
                    title = "My Post Title",
                    image = "/someimage.png",
                    post_class = "somepostclass"
                }
            });
            var cq = CsQuery.CQ.CreateDocument(output);
            Assert.Equal("My Post Title", cq["h1.post-title"].Html());
        }
    }
}
