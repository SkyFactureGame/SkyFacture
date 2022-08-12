﻿using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkyFacture.Content;
using SkyFacture.Scenes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SkyFacture.Windows;

public unsafe class WindowsLauncher : ClientLauncher
{
	public static void Main(string[] args)
	{
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
				FramesPerSecond = Environment.UserName == "NiTiSon" ? 5000 : 500,
				UpdatesPerSecond = 50,
				VSync = args.Contains("-vsync"),
			};
			WindowsLauncher wl = new(args, options);

		}
		catch (Exception ex)
		{
			DateTime dt = DateTime.Now;
			string crashFileName = $"crash\\{dt:yy-MM-dd HH.mm.ss}.log";
			Directory.CreateDirectory("crash");
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
			sw.Dispose();
			fs.Dispose();

			if (NativeWindow.MessageBox(IntPtr.Zero, message, "Sky Facture crash handler", MessageType.OkCancel | MessageType.ErrorIcon) == MessageButtonID.Ok)
			{
				Process.Start("explorer.exe", crashFileName).Dispose();
			}
		}
	}
	private readonly IWindow window;
	public WindowsLauncher(string[] args, WindowOptions windowOptions)
	{
		Core.FM = new Desktop.IO.DesktopFileManager(typeof(_resourceHandle).Assembly);
		Core.SL = new WindowsSpriteLoader();
		Core.SM = new Scenes.SceneManager();

		window = Window.Create(windowOptions);
		window.Load += Load;
		window.Closing += Unload;
		window.Render += Render;
		window.Update += Update;
		window.Resize += Resize;
		Core.View = window;
		window.Initialize();
		
		window.Run();
	}
	private void Load()
	{
		Core.Gl = window.CreateOpenGL();
		Core.SM.ChangeScene(new LoadingScene());

		IInputContext inp = window.CreateInput();
		foreach (IKeyboard board in inp.Keyboards)
		{
			board.KeyDown += (kb, k, a) =>
			{
				if (k is Key.F11)
				{
					window.WindowState = window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
				}
			};
		}
	}
	private void Update(double delta)
	{
		Core.SM.Update(delta);
	}
	private void Render(double delta)
	{
		Core.Gl.ClearColor(Color.Black);
		Core.Gl.Clear(ClearBufferMask.ColorBufferBit);

		Core.SM.Render(delta);
		window.SwapBuffers();
	}
	private void Resize(Vector2D<int> newSize)
	{
		Core.Gl.Viewport(newSize);
	}
	private void Unload()
	{
		Core.SM.Dispose();
	}
}
