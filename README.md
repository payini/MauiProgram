# Table of Contents

- [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [MAUIProgram](#mauiprogram)
  - [Prerequisites](#prerequisites)
    - [Visual Studio 2022 Preview](#visual-studio-2022-preview)
    - [Mobile Development with .NET Workload](#mobile-development-with-net-workload)
    - [MAUI Templates Missing](#maui-templates-missing)
  - [Demo](#demo)
    - [Create a MAUI Application](#create-a-maui-application)
    - [MauiProgram.cs](#mauiprogramcs)
      - [ASP.NET Core Web App and in ASP.NET Core Web API's Program.cs file](#aspnet-core-web-app-and-in-aspnet-core-web-apis-programcs-file)
      - [.NET MAUI App (Preview) MainProgram.cs file using .NET 6](#net-maui-app-preview-mainprogramcs-file-using-net-6)
      - [MauiProgram.cs Explained](#mauiprogramcs-explained)
      - [So, what else can we do in MauiProgram.cs?](#so-what-else-can-we-do-in-mauiprogramcs)
        - [Hosting Extension Methods](#hosting-extension-methods)
        - [Services, Logging and Configuration](#services-logging-and-configuration)
        - [Adding a Service](#adding-a-service)
        - [Consuming a Service](#consuming-a-service)
        - [Add Logging](#add-logging)
  - [Complete Code](#complete-code)
  - [Resources](#resources)

## Introduction

In this demo we are going to cover what is in the MauiProgram class, how does it work, why it works, and what are the benefits of using it.

## MAUIProgram

MAUI applications are bootstrapped using a _host_. The _host_ allows applications to be initialized from a single location, as well as configure services, libraries, resources, etc. just like in .NET Core applications.

The _host_ also encapsulates app's resources, and lifetime like Dependency Injection, Logging, Configuration, and App shutdown.

In the case of MAUI applications, the specific _host_ utilized is a [.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host), which is also used in other type of applications.

In the demo, we will cover how the _host_ works and how to configure it, as well as understanding of why this components work and what is needed to be able to use them.

## Prerequisites

The following prerequisites are needed for this demo.

### Visual Studio 2022 Preview

For this demo, we are going to use the latest version of [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/community/).

In my case, I already have [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/community/) installed, so the first thing I am going to do, is update it from Preview 4.0 to 6.0.

![Visual Studio 2022 Preview Update](images/edd98283ce6ae5c38457b3b158e956c6c51e1bdc01321833298d09cb72b5ba15.png)  

>:memo: Everything covered in this demo, is also available in Preview 1, but I like to update to the latest for good measure.

### Mobile Development with .NET Workload

If you have been following [The .NET Show](https://www.youtube.com/playlist?list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_) you know that in order to build MAUI apps, the Mobile Development with .NET Workload workload needs to be installed, as well as the .NET MAUI (Preview), so if you do not have that installed you can do that now.

![Mobile Development with .NET Workload](images/ecad71fd4493d0cfcd86e932e21abfe7f483422e0d5e44491d2fc8f226b8b797.png)  

### MAUI Templates Missing

As I tried to create a MAUI Application, I noticed that the MAUI templates were missing:

![Missing MAUI Templates](images/02e1c7c388f3dc7892fde5f18e86ba08777c280725afb9065d82c5995e2b1989.png)  

>:point_up: If the MAUI Templates are missing, they can be installed running ```dotnetcli dotnet new -i Microsoft.Maui.Templates```

So, first I listed all installed templates by running ```dotnetcli dotnet new --list```, this was the output:

```dotnetcli
dotnet new --list
These templates matched your input:

Template Name                                 Short Name           Language    Tags
--------------------------------------------  -------------------  ----------  -------------------------------------
ASP.NET Core Empty                            web                  [C#],F#     Web/Empty
ASP.NET Core gRPC Service                     grpc                 [C#]        Web/gRPC
ASP.NET Core Web API                          webapi               [C#],F#     Web/WebAPI
ASP.NET Core Web App                          webapp,razor         [C#]        Web/MVC/Razor Pages
ASP.NET Core Web App (Model-View-Controller)  mvc                  [C#],F#     Web/MVC
ASP.NET Core with Angular                     angular              [C#]        Web/MVC/SPA
ASP.NET Core with React.js                    react                [C#]        Web/MVC/SPA
ASP.NET Core with React.js and Redux          reactredux           [C#]        Web/MVC/SPA
Blazor Server App                             blazorserver         [C#]        Web/Blazor
Blazor WebAssembly App                        blazorwasm           [C#]        Web/Blazor/WebAssembly/PWA
Class Library                                 classlib             [C#],F#,VB  Common/Library
Console App                                   console              [C#],F#,VB  Common/Console
dotnet gitignore file                         gitignore                        Config
Dotnet local tool manifest file               tool-manifest                    Config
EditorConfig file                             editorconfig                     Config
global.json file                              globaljson                       Config
MSTest Test Project                           mstest               [C#],F#,VB  Test/MSTest
MVC ViewImports                               viewimports          [C#]        Web/ASP.NET
MVC ViewStart                                 viewstart            [C#]        Web/ASP.NET
NuGet Config                                  nugetconfig                      Config
NUnit 3 Test Item                             nunit-test           [C#],F#,VB  Test/NUnit
NUnit 3 Test Project                          nunit                [C#],F#,VB  Test/NUnit
Protocol Buffer File                          proto                            Web/gRPC
Protocol Buffer File                          proto                            Web/gRPC
Razor Class Library                           razorclasslib        [C#]        Web/Razor/Library/Razor Class Library
Razor Component                               razorcomponent       [C#]        Web/ASP.NET
Razor Page                                    page                 [C#]        Web/ASP.NET
Solution File                                 sln                              Solution
Web Config                                    webconfig                        Config
Windows Forms App                             winforms             [C#],VB     Common/WinForms
Windows Forms Class Library                   winformslib          [C#],VB     Common/WinForms
Windows Forms Control Library                 winformscontrollib   [C#],VB     Common/WinForms
Worker Service                                worker               [C#],F#     Common/Worker/Web
WPF Application                               wpf                  [C#],VB     Common/WPF
WPF Class library                             wpflib               [C#],VB     Common/WPF
WPF Custom Control Library                    wpfcustomcontrollib  [C#],VB     Common/WPF
WPF User Control Library                      wpfusercontrollib    [C#],VB     Common/WPF
xUnit Test Project                            xunit                [C#],F#,VB  Test/xUnit
```

Then, I installed the missing MAUI templates by running ```dotnetcli dotnet new -i Microsoft.Maui.Templates```, and this was the output:

```dotnetcli
dotnet new -i Microsoft.Maui.Templates
The following template packages will be installed:
   Microsoft.Maui.Templates

Success: Microsoft.Maui.Templates::6.0.300-rc.2.5513 installed the following templates:
Template Name                                  Short Name        Language  Tags
---------------------------------------------  ----------------  --------  ---------------------------------------------------------
.NET MAUI App (Preview)                        maui              [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen
.NET MAUI Blazor App (Preview)                 maui-blazor       [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Blazor
.NET MAUI Class Library (Preview)              mauilib           [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen
.NET MAUI ContentPage (C#) (Preview)           maui-page-csharp  [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentPage (XAML) (Preview)         maui-page-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentView (C#) (Preview)           maui-view-csharp  [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentView (XAML) (Preview)         maui-view-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ResourceDictionary (XAML) (Preview)  maui-dict-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Xaml/Code
```

:exclamation: Just like that, MAUI templates are back!

![MAUI Templates Installed](images/60c7f22f3523fc664ca5a92b3a8643d015c43a1058a719cc860e2ff625163174.png)  

Now we can proceed with the demo.

## Demo

The following demo is a MAUI application where I will dive in to the MAUIProgram.cs file, and show you why this is important, and what can you do with it.

The first step, as usual, is to create our demo application.

### Create a MAUI Application

![Create a new MAUI Application project](images/83e0873d11ac7a7ad94f4b6b0b2d37a137b324ba2f9dfaccaf70a47ce2b0e44c.png)  

![Configure your new project](images/dda78b72379639040f37e7b5f1ab9556da96a9d20b306822f1df81229ea4fc68.png)  

They first thing we notice, is that our project will not build.

![Project won't build](images/e870be90aae36d664ac9c90568f28b22fd1a63168b7f101c62ec55b3a33a9f24.png)

:warning: If you followed my previous video [Native AOT in .NET 7. The .NET Show with Carl Franklin Ep 21](https://www.youtube.com/watch?v=4THfSynZLq8&list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_&index=21) you may run into the same issue, so follow along.

After spending a couple of hours trying to get my environment back in a working state, I finally figured out the reason, the .NET 7 SDK 7.0.100-preview.3.22179.4 needs to be uninstalled. The easiest way to do that, is to run the installer used to install it and click Uninstall.

![Uninstall .NET SDK 7.0.100-preview.3.22179.4](images/cbb06a071717e354ee77f2503d9b3303fb5cfe93bb1e11b003270ea4f33d1a39.png)

![.NET SDK 7.0 Uninstall Successfully Completed](images/20e712d43e5a9f0734143cd6417b5ac4438b15851237271f88ca43882b82e3a5.png)  

Now you should be able to build the solution with no errors.

![Build Succeeded](images/09e67b090ebaf9aaf7ec40e4b71a3673d7667f9fd27e8f77e87f397d2d059916.png)  

### MauiProgram.cs

Traditionally, the _host_ is being created in the Program.cs file, as you may remember it from ASP.NET Core Web App as well as in ASP.NET Core Web API applications prior to .NET 6.

The following Program.cs file was generated using .NET Core 3.1, but the same code will be in applications built for .NET 5.

#### ASP.NET Core Web App and in ASP.NET Core Web API's Program.cs file

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

>:point_up: Notice the use of Startup (which is declared in Startup.cs) to configure services.

#### .NET MAUI App (Preview) MainProgram.cs file using .NET 6

In the case of MAUI applications, the _host_ is being created in the `MainProgram.cs` file.

You can see that a `MauiAppBuilder` is being created to be able to configure fonts, resources and services, here rather than in the Startup class, which is not included anymore.

```csharp
namespace MauiProgram;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
```

All this is available thanks to the MAUI Template including the Microsoft.Maui.Extensions NuGet package, which includes dependencies to .NET6, Android, iOS, Mac Catalyst, Tizen, and Windows. Those dependencies allow us to use Dependency Injection, Logging, and Interop services, as mentioned before.

![Dependencies](images/2c5bb5fc953b94dbc22a99e65594c6bc8904841111e3f5d6645674402943867a.png)

>:exclamation: Is important to mention than the _host_ has a container with a collection of hosted services, so when the _host_ starts, it calls `IHostedService.StartAsync` on any class implementing the `IHostedService` interface.

#### MauiProgram.cs Explained

I order to explain the MauiProgram.cs I added comments to the code below.

```csharp
namespace MauiProgram;

public static class MauiProgram
{
	// This method returns the entry point (a MauiApp) and is called from each platform's entry point.
	public static MauiApp CreateMauiApp()
	{
		// MauiApp.CreateBuilder returns a MauiAppBuilder, which is used to configure fonts, resources, and services.
		var builder = MauiApp.CreateBuilder();

		builder
			// We give it our main App class, which derives from Application. App is defined in App.xaml.cs.
			.UseMauiApp<App>()
			// Default font configuration.
			.ConfigureFonts(fonts =>
			{
				// AddFont takes a required filename (first parameter) and an optional alias for each font.
				// When using these fonts in XAML you can use them either by filename (without the extension,) or the alias.
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// The MauiAppBuilder returns a MauiApp application.
		return builder.Build();
	}
}
```

If you look at the references for CreateMauiApp, you will see how all 5 platforms entry points, are calling this method to get the MauiApp returned.

![CreateMauiApp References](images/9183c0304b8fbf23518a1eb3949ccde1c49c5289ea2139af0f03f3eda3e365e9.png)  

Below is a graphical representation of the dependencies:

![CreateMauiApp Diagram for a Windows Application](images/6ebbb45b9bb2241e63681a0f8b8625330be209350e1c63567941630e786c81bb.png)  

#### So, what else can we do in MauiProgram.cs?

Well, for starters you can configure animations, a container, dispatching, effects, essentials, fonts, image sources, and MAUI handlers, as you can see in the image below.

##### Hosting Extension Methods

![Hosting Extension Methods](images/de9a4498e04c80861da9006293a4659d3d7ff7e9db1c469a13d6a10f629355cc.png)  

With the exception of `ConfigureContainer`, they are all extension methods provided by the `Microsoft.Maui.Hosting` namespace.

You can also register Services, Logging and Configuration via each of those properties.

##### Services, Logging and Configuration

![Services, Logging and Configuration](images/a8ae16e29bb7ffce9c730b49779fe94d583e1a845a7432ad3177d88aeca5baeb.png)  

##### Adding a Service

Let's imagine we need an service that calls some API to get some donuts data. Let's add a file `ApiService.cs` and simply return some Json hard-coded data.

```csharp
namespace MauiProgram
{
    public class ApiService : IApiService
    {
        public string GetDonuts()
        {
			return @"{
						""id"": 0001,
						""type"": ""donut"",
						""name"": ""Cake"",
						""ppu"": 0.55,
						""batters"":
							{
								""batter"":
									[
										{ ""id"": 1001, ""type"": ""Regular"" },
										{ ""id"": 1002, ""type"": ""Chocolate"" },
										{ ""id"": 1003, ""type"": ""Blueberry"" },
										{ ""id"": 1004, ""type"": ""Devil's Food"" }
									]
							},
						""topping"":
							[
								{ ""id"": 5001, ""type"": ""None"" },
								{ ""id"": 5002, ""type"": ""Glazed"" },
								{ ""id"": 5005, ""type"": ""Sugar"" },
								{ ""id"": 5007, ""type"": ""Powdered Sugar"" },
								{ ""id"": 5006, ""type"": ""Chocolate with Sprinkles"" },
								{ ""id"": 5003, ""type"": ""Chocolate"" },
								{ ""id"": 5004, ""type"": ""Maple"" }
							]
					}".Replace("\t", string.Empty).Replace("\r\n", string.Empty); // Remove tabs and enters.
        }
    }
}
```

As you may know, .NET Core provides Dependency Injection out-of-the box, and Services are added using Dependency injection, so let's extract an interface for the `ApiService` and call it `IApiService.cs`.

![Extract Interface](images/539708ff4618ad12beae4cd7fc8f37966eca6f515c8c38daed5f2c5318f77146.png)  

![Extract Interface Dialog](images/67c473814860876a4169614b773662f0e634d155f3000c9ef47b620981c900e5.png)  

That will create the `IApiService` interface, and also will make ApiService implement it.

Now all you have to do is to append the following code to our `MauiProgram.cs` file, in the builder.

```csharp
.Services
				.AddSingleton<MainPage>()
				.AddSingleton<IApiService, ApiService>()
```

The complete file looks like this:

```csharp
namespace MauiProgram;

public static class MauiProgram
{
	// This method returns the entry point (a MauiApp) and is called from each platform's entry point.
	public static MauiApp CreateMauiApp()
	{
		// MauiApp.CreateBuilder returns a MauiAppBuilder, which is used to configure fonts, resources, and services.
		var builder = MauiApp.CreateBuilder();

		builder
			// We give it our main App class, which derives from Application. App is defined in App.xaml.cs.
			.UseMauiApp<App>()
			// Default font configuration.
			.ConfigureFonts(fonts =>
			{
				// AddFont takes a required filename (first parameter) and an optional alias for each font.
				// When using these fonts in XAML you can use them either by filename (without the extension,) or the alias.
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			// Register services
			.Services
				.AddSingleton<MainPage>()
				.AddSingleton<IApiService, ApiService>();

		// The MauiAppBuilder returns a MauiApp application.
		return builder.Build();
	}
}
```

Remove the image from `MainPage.xaml` and repurpose the Counter button.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiProgram.MainPage">

    <ScrollView>
        <VerticalStackLayout Spacing="25" Padding="30">

            <Label 
                Text="Hello, World!"
                SemanticProperties.HeadingLevel="Level1"
                FontSize="32"
                HorizontalOptions="Center" />

            <Label 
                Text="Welcome to .NET Multi-platform App UI"
                SemanticProperties.HeadingLevel="Level1"
                SemanticProperties.Description="Welcome to dot net Multi platform App U I"
                FontSize="18"
                HorizontalOptions="Center" />

            <Label 
                Text="Donut Data: "
                FontSize="18"
                x:Name="CounterLabel"
                HorizontalOptions="Center" />

            <Button 
                Text="Click me"
                FontAttributes="Bold"
                SemanticProperties.Hint="Get data"
                Clicked="OnGetDataClicked"
                HorizontalOptions="Center" />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

##### Consuming a Service

In order to consume the service we need to use Dependency Injection to inject the service we registered in the step before.

In `MainPage.xaml.cs` make the following changes:

```csharp
namespace MauiProgram;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;

	public MainPage(IApiService apiService)
	{
		_apiService = apiService;
		InitializeComponent();
	}

    private void OnGetDataClicked(object sender, EventArgs e)
	{
		CounterLabel.Text = $"Donut Data: {_apiService?.GetDonuts()}";

		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}
```

Now you can run the application and see how the ApiService gets injected and can be used to retrieve the data.

![API Service](images/068eb3e6f0d63adca4553bb908dfa918dd0193ffe1b0493536756f99a3751a83.png)  

![Data](images/3a0a0a59faa4e758d006ad9f33d5586b69c85eb3ff5fae1f71aaa85b6d2c1bf5.png)  

![Get Data](images/9f56d687e5c8ca6c65577c03fe8f5cc1f9ba1b73b7fd11e6aa561f81551529aa.png)  

##### Add Logging


## Complete Code

- <https://github.com/payini/MauiProgram>

## Resources

| Resource Title                                     | Url                                                                                                            |
| -------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| The .NET Show with Carl Franklin                   | <https://www.youtube.com/playlist?list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_>                                     |
| MAUI Templates Missing                             | <https://github.com/dotnet/maui/issues/5355?msclkid=6320df98ce5011ec9343dac76b4764f4>                          |
| Configure fonts, services, and handlers at startup | https://docs.microsoft.com/en-us/dotnet/maui/fundamentals/app-startup?msclkid=932cab7dce7511ec85837ed885f1ad6a |
| .NET Generic Host                                  | https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host                                           |