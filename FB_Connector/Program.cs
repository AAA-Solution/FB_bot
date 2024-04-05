// See https://aka.ms/new-console-template for more information
using FB_Connector;

Console.WriteLine("Hello, World!");

FBService fBService = new FBService();
var abc = fBService.getFBPosts();
Console.WriteLine("End");