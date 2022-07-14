// The NiTiS-Dev licenses this file to you under the MIT license.
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkyFacture.Drawing;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;
using System;
using System.IO;

namespace SkyFacture;


public unsafe class ExecutorMain
{
	internal ExecutorMain() { }
	private static readonly GameWindow window = new(
		new()
		{
			RenderFrequency = 144,
			UpdateFrequency = 60,
		},
		new()
		{
			API = OpenTK.Windowing.Common.ContextAPI.OpenGL,
			APIVersion = new(3, 3),
			Size = new(640, 320),
			Title = GameName,
		}
		);
#pragma warning disable CS8618 
	public static LoadingProgress LoadingProgress { get; private set; }
	private static Scenes.Scene? scene;
	public static Scenes.Scene Scene
	{
		get => scene!;
		set
		{
			scene?.Destroy();
			scene = value;
			scene.Init();
		}
	}
	public static GameVersion Version => new(V:0, r:1);
	public static SystemType SystemType { get; internal protected set; }
	public const string GameName = "Sky Facture";
	public const string Package = "NiTiS.Dev.SkyFacture";
	public const string PackageLowerCase = "nitis.dev.skyfacture";
#pragma warning restore CS8618
	internal protected static void Launch()
	{
		window.Load += Load;
		window.RenderFrame += GraphicUpdate;
		window.UpdateFrame += Update;
		window.Resize += Resize;
		window.KeyDown += KeyDown;
		window.KeyUp += KeyUp;

		window.Run();
	}
	private static Camera Camera; 
	private static Texture2D Andrew, Rainbow;
	private static int VBO, VAO;
	public static readonly float[] Vert = new float[]
	{
		-0.5f, -0.5f, 0, 0,
		 0.5f, -0.5f, 1, 0,
		 0.5f,  0.5f, 1, 1,
		 0.5f,  0.5f, 1, 1,
		-0.5f,  0.5f, 0, 1,
		-0.5f, -0.5f, 0, 0,
	};
	internal protected static void Load()
	{
		Console.WriteLine("Max Texture Binding: " + GL.GetInteger(GetPName.MaxTextureImageUnits));

		Blend.Enable();

		Andrew = new(File.OpenRead("GameContent/Textures/andrew.png"));
		Rainbow = new(File.OpenRead("GameContent/Textures/rainbow.png"));

		VAO = GL.GenVertexArray();
		GL.BindVertexArray(VAO);

		VBO = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
		GL.BufferData(BufferTarget.ArrayBuffer, Vert.Length * sizeof(float), Vert, BufferUsageHint.StaticDraw);

		Camera = new();
		Camera.Select();
	}
	internal protected static void GraphicUpdate(FrameEventArgs args)
	{
		Time.RenderDelta = args.Time;
		Time.RenderTime += args.Time;

		GL.ClearColor(Palette.Disco10);
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		Shaders.DefShader.SetDefaults();

		Shaders.DefShader.Use();

		Rainbow.Use(TextureUnit.Texture0);

		mat4 ident = mat4.Identity;
		ident *= Camera.GetTranslation();
		ident *= mat4.CreateScale(40f);
		ident *= Camera.GetView();

		_ = Atlas.Transperent;

		Shaders.DefShader.Texture(TextureUnit.Texture0);
		GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
		
		window.Context.SwapBuffers();
	}
	internal protected static void Resize(ResizeEventArgs args)
	{
		GL.Viewport(0, 0, args.Width, args.Height);
		Screen.Width = args.Width;
		Screen.Height = args.Height;
	}
	internal protected static void Update(FrameEventArgs args)
	{
		Time.UpdateDelta = args.Time;
		Time.UpdateTime += args.Time;

		Scene?.Update();

		Screen.InFocuse = window.IsFocused;
		if (!window.IsFocused) // Check to see if the window is focused
		{
			return;
		}
	}
	internal protected static void KeyUp(KeyboardKeyEventArgs args)
	{

	}
	internal protected static void KeyDown(KeyboardKeyEventArgs args)
	{
		if (args.Key == Keys.F11)
		{
			window.WindowState = WindowState.Normal == window.WindowState ? WindowState.Fullscreen : WindowState.Normal;
		}
	}
}
public enum SystemType : byte
{
	Unknown = 0,
	Windows = 1,
	Android = 2,
	Linux = 3,
	OhX = 20,
	OhPhone = 21,
}
