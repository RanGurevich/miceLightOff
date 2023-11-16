using System;
using System.Threading;
using CoreAudioApi;
namespace SparkSnake
{

   
    class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            LogitechGSDK.LogiLedInit();
            LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_RGB);
            LogitechGSDK.LogiLedSetLighting(0, 0, 0);
            Console.WriteLine("mice light shutdown.\npress 1 to turn off the light again\npress 2 to start Popcorn Time\npress 3 to start YouTube\npress 4 to start Sdarot\npress any other key to close\npress 5 to turn on the light\npress 6 to glitter the mice");
            shutdownlight(0);

        }

        public static void shutdownlight(int currentRGB)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo pressed = Console.ReadKey();
            if (pressed.Key == ConsoleKey.NumPad1 || pressed.Key == ConsoleKey.D1 || pressed.Key == ConsoleKey.End)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                LogitechGSDK.LogiLedInit();
                LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_RGB);
                LogitechGSDK.LogiLedSetLighting(0, 0, 0);
                Console.WriteLine("\nLight shutdown.");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.NumPad2 || pressed.Key == ConsoleKey.D2 || pressed.Key == ConsoleKey.DownArrow)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Popcorn Time\\PopcornTimeDesktop.exe");
                Console.WriteLine("\nPopcorn Time started.");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.NumPad3 || pressed.Key == ConsoleKey.D3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Diagnostics.Process.Start("www.youtube.com");
                Console.WriteLine("\nYouTube started.");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.NumPad4 || pressed.Key == ConsoleKey.D4)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                System.Diagnostics.Process.Start("http://www.zira.ninja/");
                Console.WriteLine("\nSdarot started.");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.NumPad5 || pressed.Key == ConsoleKey.D5)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                LogitechGSDK.LogiLedRestoreLighting();
                Console.WriteLine("\nMice iight turn on.");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.NumPad6 || pressed.Key == ConsoleKey.D6)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMic glitter started");
                Random rnd = new Random();
                int rnd1 = rnd.Next(0, 55);
                int rnd2 = rnd.Next(0, 20);
                int rnd3 = rnd.Next(0, 70);

                for (int i = 0; i < 999999; i++)
                {
                    LogitechGSDK.LogiLedSetLighting(i%45+rnd1, i%80+rnd2, i%30+rnd3);
                }
                LogitechGSDK.LogiLedSetLighting(0, 0, 0);
                Console.WriteLine("\nDone");
                shutdownlight(currentRGB);
                return;
            }
            if (pressed.Key == ConsoleKey.VolumeUp)
            {
                if (currentRGB < 254)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    currentRGB++;
                    LogitechGSDK.LogiLedInit();
                    LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_RGB);
                    LogitechGSDK.LogiLedSetLighting(currentRGB, currentRGB, currentRGB);
                    Console.WriteLine("Mouse light incresed, the mouse light is now: "+currentRGB);                 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Current light is on max");
                }
                SetVol(GetVol() - 1);
                shutdownlight(currentRGB);
                return;
                
            }
            if (pressed.Key == ConsoleKey.VolumeDown)
            {
                if (currentRGB > 0)
                {    
                    Console.ForegroundColor = ConsoleColor.Blue;
                    currentRGB--;
                    LogitechGSDK.LogiLedInit();
                    LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_RGB);
                    LogitechGSDK.LogiLedSetLighting(currentRGB, currentRGB, currentRGB);
                    Console.WriteLine("Mouse light decresed, the mouse light is now: " + currentRGB);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Current light is already on 0");
                }
                SetVol(GetVol() + 1);
                shutdownlight(currentRGB);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAre You sure you want to quit? N to cancel, yes any key");
            pressed = Console.ReadKey();
            if (pressed.Key == ConsoleKey.N)
            {
                Console.WriteLine("\n Exit canceled.");
                shutdownlight(currentRGB);
            }             
            Console.Clear();
            Console.WriteLine("~-~-Bye~-~-");
            for (int i = 0; i < 25; i++)
            {
                Console.Write("\n");
                for (int j = (i*2)-1; j > 0; j--)
                {
                    Console.Write("{0}",j%3==0?"+":j%2==0?"-":"*");
                }
            }
            for (int i = 0; i < 99999999; i++)
            {

            }
            return;
         }

        public static int GetVol()
        {
            int MasterMin = 0;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            MMDevice device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            int Vol = 0;

            {
                var withBlock = device.AudioEndpointVolume;
                Vol = System.Convert.ToInt32(withBlock.MasterVolumeLevelScalar * 100);
                if (Vol < MasterMin)
                    Vol = MasterMin /100;
            }
            return Vol;
        }
        public static void SetVol(int Svol)
        {
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            MMDevice device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = Svol / 100.0F;
        }

    }
}
