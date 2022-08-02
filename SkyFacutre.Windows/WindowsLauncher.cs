using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkyFacture;
using SkyFacture.Content;
using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SkyFacture.Windows;

public class WindowsLauncher : ClientLauncher
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
	public unsafe WindowsLauncher(string[] args, WindowOptions windowOptions)
	{
		Core.FM = new Desktop.IO.DesktopFileManager(typeof(_resourceHandle).Assembly);
		Core.SL = new WindowsSpriteLoader();

		window = Window.Create(windowOptions);
		window.Load += Load;
		window.Closing += Unload;
		window.Render += Render;
		window.Update += Update;
		window.Resize += Resize;
		Core.View = window;
		window.Initialize();
		Core.Gl = GL.GetApi(window);
		
		window.Run();
	}
	private ShaderProgram shader;
	private Buffer<float> vbo;
	private Buffer<uint> ebo;
	private Graphics.Memory.VertexArray vao;
	private readonly float[] Vert = new float[]
	{
		-0.5f, -0.5f,
		 0.5f, -0.5f,
		 0.5f,  0.5f,
		-0.5f,  0.5f
	};
	private readonly uint[] Index = new uint[]
	{
		0, 1, 2,
		1, 2, 3
	};
	private void Load()
	{
		shader = new(Core.FM.InternalRead("SkyFacture/Content/Shaders/default.vert"), Core.FM.InternalRead("SkyFacture/Content/Shaders/default.frag"));
		
		vao = new();
		vao.Bind();

		ebo = new(BufferTargetARB.ElementArrayBuffer);
		ebo.Bind();
		ebo.Data((uint)Index.Length, Index);

		vbo = new(BufferTargetARB.ArrayBuffer);
		vbo.Bind();
		vbo.Data((uint)Vert.Length, Vert);

		vao.AttributePointer<float>(0, 2, VertexAttribPointerType.Float, sizeof(float) * 2, 0);
	}
	private void Update(double delta)
	{

	}
	private void Render(double delta)
	{
		Core.Gl.ClearColor(Color.Turquoise);
		Core.Gl.Clear(ClearBufferMask.ColorBufferBit);
		vao.Bind();
		Core.Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);

		window.SwapBuffers();	
	}
	private void Resize(Vector2D<int> newSize)
	{
		Core.Gl.Viewport(newSize);
	}
	private void Unload()
	{
	}
}
