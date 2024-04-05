// See https://aka.ms/new-console-template for more information
using FB_Connector;

Console.WriteLine("Hello, World!");

FBService fBService = new FBService();
await fBService.getFBPostsAsync();
Console.WriteLine("End");