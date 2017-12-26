using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace GoogleMapsSample
{
   /// <summary>
   ///  Downloads a Redmond map from Google Maps, saves it as a local file and opens the default viewer.
   /// </summary>
   class Program
   {
      static string _address = "http://maps.googleapis.com/maps/api/staticmap?center=Redmond,WA&zoom=11&size=800x800&sensor=false";

      static async void RunClient()
      {
         var client = new HttpClient();

         // Send asynchronous request
         var response = await client.GetAsync(_address);

         // Check that response was successful or throw exception
         response.EnsureSuccessStatusCode();

         // Read response asynchronously and save asynchronously to file
         using (var fileStream = new FileStream("output.png", FileMode.Create, FileAccess.Write, FileShare.None))
         {
            await response.Content.CopyToAsync(fileStream);
         }

         var process = new Process { StartInfo = { FileName = "output.png" } };
         process.Start();
      }

      static void Main(string[] args)
      {
         RunClient();

         Console.WriteLine("Hit ENTER to exit...");
         Console.ReadLine();
      }
   }
}
