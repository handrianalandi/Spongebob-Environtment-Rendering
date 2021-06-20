using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace UASGrafkom
{
    class Program
    {
        static void Main(string[] args)
        {

            var ourWindow = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 800),
                Title = "UAS Grafkom Felicia Laksana C14190054, Levina Charin C14190145, Handrian Alandi C14190231"
            };

            using (var win = new Windows(GameWindowSettings.Default, ourWindow))
            {
                win.Run();
            }

        }
    }
}
