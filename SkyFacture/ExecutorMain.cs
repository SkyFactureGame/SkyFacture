﻿
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkyFacture.Drawing;
using SkyFacture.Drawing.Buffers;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;
using SkyFacture.Unical;
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
			NumberOfSamples = 2,
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
	public static ResourceLoader ResourceLoader { get; protected internal set; } 
	public static GameVersion Version => new(V: 0, r: 1);
	public static SystemType SystemType { get; protected internal set; }
	public const string GameName = "Sky Facture";
	public const string Package = "NiTiS.Dev.SkyFacture";
	public const string PackageLowerCase = "nitis.dev.skyfacture";
	protected internal static void Launch()
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
	private static Region2D White;
	private static Buffer<float> VBO;
	private static VertexArray VAO;
#pragma warning restore CS8618
	public static readonly float[] Vert = new float[]
	{
		-0.5f, -0.5f, 0, 0,
		 0.5f, -0.5f, 1, 0,
		 0.5f,  0.5f, 1, 1,
		 0.5f,  0.5f, 1, 1,
		-0.5f,  0.5f, 0, 1,
		-0.5f, -0.5f, 0, 0,
	};
	protected internal static void Load()
	{
		Console.WriteLine("Max Texture Binding: " + GL.GetInteger(GetPName.MaxTextureImageUnits));
		Console.WriteLine("OpenGL Version: " + GL.GetInteger(GetPName.MajorVersion) + "." + GL.GetInteger(GetPName.MinorVersion));
		Blend.Enable();
		Atlas.LoadInternalRegions();
		Andrew = new(File.OpenRead("GameContent/Textures/andrew.png"));
		Rainbow = new(File.OpenRead("GameContent/Textures/rainbow.png"));
		White = Atlas.Region("white")!;


		VAO = new(Shaders.DefShader);
		
		VBO = new(BufferTarget.ArrayBuffer, sizeof(float) * 4);
		VBO
			.Bind()
			.Init(Vert.Length * sizeof(float), 6, Vert);

		VAO.BindAllAttributes(VBO);

		Camera = new();
		Camera.Select();
		Camera.Position -= new vec3(0, 0, 1f);
		GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
	}
	protected internal static void GraphicUpdate(FrameEventArgs args)
	{
		Time.RenderDelta = args.Time;
		Time.RenderTime += args.Time;

		GL.ClearColor(Palette.Disco10);
		GL.Clear(ClearBufferMask.ColorBufferBit);

		Shaders.DefShader.SetDefaults();

		VAO.Bind();

		Rainbow.Use(TextureUnit.Texture0);
		Andrew.Use(TextureUnit.Texture1);

		GL.BindBuffer(BufferTarget.ArrayBuffer, VBO.handle);

		Shaders.DefShader.Color(Palette.Disco10.Move(180f));

		mat4 ident = mat4.Identity;
		ident *= Camera.GetTranslation();
		ident *= mat4.CreateScale(225f, 225f, 1f);
		ident *= Camera.GetView();


		Shaders.DefShader.Texture(TextureUnit.Texture1);
		Shaders.DefShader.Matrix(ident);
		VBO.Bind();
		VAO.Bind();
		VAO.Draw(PrimitiveType.Triangles, 0, 6);

		window.Context.SwapBuffers();
	}
	protected internal static void Resize(ResizeEventArgs args)
	{
		GL.Viewport(0, 0, args.Width, args.Height);
		Screen.Width = args.Width;
		Screen.Height = args.Height;
	}
	protected internal static void Update(FrameEventArgs args)
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
	protected internal static void KeyUp(KeyboardKeyEventArgs args)
	{

	}
	protected internal static void KeyDown(KeyboardKeyEventArgs args)
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
