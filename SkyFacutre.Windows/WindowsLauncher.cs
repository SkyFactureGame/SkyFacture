using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SkyFacture.Content;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SkyFacture.Windows;

public class WindowsLauncher : ClientLauncher
{
	public static void Main(string[] args)
	{
		// Some magic in future :P
		DateTime launchTime = DateTime.Now.AddSeconds(-1);
		try
		{
#if !DEBUG
			NativeWindow.ShowWindow(NativeWindow.GetConsoleWindow(), DisplayType.Hide);
#endif

			WindowOptions options = new()
			{
				API = new GraphicsAPI(ContextAPI.OpenGL, new(3,3)),
				Title = "Sky Facture",
				Size = new Vector2D<int>(400, 400),
				FramesPerSecond = 500,
				UpdatesPerSecond = 50,
				VSync = args.Contains("-vsync"),
			};
			WindowsLauncher wl = new(args, options);

		}
		catch (Exception ex)
		{
			DateTime dt = DateTime.Now;
			string crashFileName = $"crash\\{dt:yy-MM-dd HH.mm.ss}.log";
			Directory.CreateDirectory(Const.CrashFolder);
			using FileStream fs = File.Open(crashFileName, FileMode.OpenOrCreate);
			using StreamWriter sw = new(fs);

			sw.Write("Sky Facture for Desktop crash log\n");
			sw.Write($"Crash time: {dt:yy-MM-dd HH.mm.ss}\n");
			TimeSpan runtime = dt - launchTime;
			string runtimeString
				= runtime.Hours > 0 ? $"{(int)runtime.TotalHours:00}h {runtime.Minutes:00}m {runtime.Seconds:00}s"
				: runtime.Minutes > 0 ? $"{runtime.Minutes:00}m {runtime.Seconds:00}s"
				: runtime.Seconds.ToString("0s");
			sw.Write($"Runtime: {runtimeString}\n");
			sw.Write($"Exception: {ex.GetType()}\n");
			sw.Write($"Message: {ex.Message}\n");
			sw.Write($"StackTrace:\n{ex.StackTrace}");
			string logPath = Path.Combine(Environment.ProcessPath!, crashFileName);
			string message =
				$"""
				An error occurred while running the game
				
				Crash log saved by path {logPath}			

				Click ok to open the crash log
				""";
			if (NativeWindow.MessageBox(IntPtr.Zero, message, "Sky Facture crash handler", MessageType.OkCancel | MessageType.ErrorIcon) == MessageButtonID.Ok)
			{
				sw.Dispose();
				fs.Dispose();
				Process.Start("explorer.exe", crashFileName).Dispose();
			}
		}
	}
	public unsafe WindowsLauncher(string[] args, WindowOptions windowOptions)
	{
		Core.FM = new Desktop.IO.DesktopFileManager(typeof(_resourceHandle).Assembly);

		IWindow window = Window.Create(windowOptions);
		Core.View = window;
		window.Initialize();
		Core.Gl = GL.GetApi(window);
		Image<Rgba32> image = Image.Load<Rgba32>("wicon.png");
		Span<byte> bytes = new(new byte[image.Width * image.Height * 4]);
		image.CopyPixelDataTo(bytes);

		RawImage wicon = new(image.Width, image.Height, new Memory<byte>(bytes.ToArray()));
		window.SetWindowIcon(ref wicon);
		image.Dispose();
		GC.Collect();
		window.Run();
	}
}
