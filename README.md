# Handlebars.ContentProvider.FileSystem [![Build Status](https://travis-ci.org/esskar/handlebars-contentprovider-filesystem.svg?branch=master)](https://travis-ci.org/esskar/handlebars-core)

A file system based template content provider for  [Handlebars.Core](https://github.com/esskar/handlebars-core).

## Install

    nuget install Handlebars.ContentProvider.FileSystem

## Usage
```
var engine = new HandlebarsEngine();
engine.Configuration.TemplateContentProvider = new FileSystemTemplateContentProvider();

var template = engine.CompileView("foo.hbs");
```

To provide backward compatibility to the ViewEngine concept of the [original Handlebars.Net library](https://github.com/rexm/Handlebars.Net),
you need to set `PartialsSubPath`:

```
var fileSystemTemplateContentProvider = new FileSystemTemplateContentProvider();
fileSystemTemplateContentProvider.PartialsSubPath = "partials";
```

This will allow you to keep your views to be in the /Views folder like so:

```
Views\layout.hbs                |<--shared as in \Views            
Views\partials\somepartial.hbs   <--shared as in \Views\partials
Views\{Controller}\{Action}.hbs 
Views\{Controller}\{Action}\partials\somepartial.hbs 
```

But it will also find partials if there are at the same level as the as the actual template file:

```
Views\layout.hbs           
Views\someotherpartial.hbs
```

## Contributing

Pull requests are welcome! The guidelines are pretty straightforward:
- Only add capabilities that are already in the Mustache / Handlebars specs
- Avoid dependencies outside of the .NET BCL
- Maintain cross-platform compatibility (.NET/Mono; Windows/OSX/Linux/etc)
- Follow the established code format