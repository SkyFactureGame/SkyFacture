using Silk.NET.Core;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkyFacture;
using SkyFacture.Content;
using SkyFacture.Graphics.Memory;
using SkyFacture.Graphics.Shaders;
using SkyFacture.Graphics.Textures;
using SkyFacture.Scenes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

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

	private ShaderProgram shader;
	private Sprite sprite;
	private Buffer<uint> EBO;
	private Buffer<float> VBO;
	private Graphics.Memory.VertexArray VAO;
	private readonly float[] Vert = new float[]
	{
		//X    Y      Z     U   V
		 0.5f,  0.5f, 0.0f, 1f, 0f, // 0
		 0.5f, -0.5f, 0.0f, 1f, 1f, // 1
		-0.5f, -0.5f, 0.0f, 0f, 1f, // 2
		-0.5f,  0.5f, 0.5f, 0f, 0f, // 3
	};
	private readonly uint[] Index = new uint[]
	{
		0, 1, 3,
		1, 2, 3
	};
	private void Load()
	{
		Core.Gl = window.CreateOpenGL();

		shader = new(Core.FM.InternalRead("SkyFacture/Content/Shaders/default.vert"), Core.FM.InternalRead("SkyFacture/Content/Shaders/default.frag"));
		using (Stream spriteStream = Core.FM.Internal("SkyFacture/Content/Sprites/Special/debug.png").OpenForRead())
		{
			sprite = new(spriteStream);
		}
		EBO = new(BufferTargetARB.ElementArrayBuffer);
		EBO.Bind();
		EBO.Data(Index);

		VBO = new(BufferTargetARB.ArrayBuffer);
		VBO.Bind();
		VBO.Data(Vert);

		VAO = new();
		VAO.Bind();

		EBO.Bind();

		VAO.AttributePointer(0, 3, VertexAttribPointerType.Float, 5 * sizeof(float), 0);
		VAO.AttributePointer(1, 2, VertexAttribPointerType.Float, 5 * sizeof(float), 3 * sizeof(float));

		Core.SM.ChangeScene(new LoadingScene());
	}
	private void Update(double delta)
	{

	}
	private void Render(double delta)
	{
		Core.Gl.ClearColor(Color.Black);
		Core.Gl.Clear(ClearBufferMask.ColorBufferBit);

		VAO.Bind();
		shader.Use();

		sprite.Bind(TextureUnit.Texture0);
		shader.UniformInt("uTex", 0);
		Core.Gl.DrawElements(PrimitiveType.Triangles, (uint)Index.Length, DrawElementsType.UnsignedInt, null);

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
