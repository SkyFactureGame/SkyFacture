﻿
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkyFacture.Drawing;
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
#pragma warning restore CS8618
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
	private static int VBO, VAO;
	public static readonly float[] Vert = new float[]
	{
		-0.505f, -0.505f, 0, 0,    0, 0, 0, 1,
		 0.505f, -0.505f, 1, 0,    0, 0, 0, 1,
		 0.505f,  0.505f, 1, 1,    0, 0, 0, 1,
		 0.505f,  0.505f, 1, 1,    0, 0, 0, 1,
		-0.505f,  0.505f, 0, 1,    0, 0, 0, 1,
		-0.505f, -0.505f, 0, 0,    0, 0, 0, 1,
	};
	public static readonly float[] VertColor = new float[]
	{
		1, 1, 1, 1,
		1, 0, 1, 1,
		1, 1, 1, 1,
		1, 1, 0, 1,
		1, 0, 1, 1,
		1, 1, 1, 1
	};
	protected internal static void Load()
	{
		Console.WriteLine("Max Texture Binding: " + GL.GetInteger(GetPName.MaxTextureImageUnits));

		Blend.Enable();

		Atlas.LoadInternalRegions();

		Andrew = new(File.OpenRead("GameContent/Textures/andrew.png"));
		Rainbow = new(File.OpenRead("GameContent/Textures/rainbow.png"));

		White = Atlas.Region("white")!;

		VAO = GL.GenVertexArray();
		GL.BindVertexArray(VAO);

		VBO = GL.GenBuffer();
		GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
		GL.BufferData(BufferTarget.ArrayBuffer, Vert.Length * sizeof(float), Vert, BufferUsageHint.StreamDraw);

		Camera = new();
		Camera.Select();
	}
	protected internal static void GraphicUpdate(FrameEventArgs args)
	{
		Time.RenderDelta = args.Time;
		Time.RenderTime += args.Time;

		GL.ClearColor(Palette.Black);
		GL.Clear(ClearBufferMask.ColorBufferBit);

		Shaders.DefShader.SetDefaults();

		Shaders.DefShader.Use();
		GL.BindVertexArray(VAO);

		Rainbow.Use(TextureUnit.Texture0);
		Andrew.Use(TextureUnit.Texture1);

		GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
		for (int i = 0; i < 6; i++)
			GL.BufferSubData(
				BufferTarget.ArrayBuffer,
				(IntPtr)(4 * sizeof(float) + i * 8 * sizeof(float)),
				sizeof(float) * 4,
				new float[4] { Palette.Disco10.R /255, Palette.Disco10.G /255, Palette.Disco10.B /255, 1f });

		mat4 ident = mat4.Identity;
		ident *= Camera.GetTranslation();
		ident *= mat4.CreateScale(225f, 225f, 1f);
		ident *= Camera.GetView();

		Shaders.DefShader.Texture(TextureUnit.Texture1);
		Shaders.DefShader.Matrix(ident);
		GL.EnableVertexAttribArray(0);
		GL.EnableVertexAttribArray(1);
		GL.EnableVertexAttribArray(2);
		GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
		GL.DisableVertexAttribArray(0);
		GL.DisableVertexAttribArray(1);
		GL.DisableVertexAttribArray(2);

		//Draw.TextureDrawer.Draw(Andrew, default, new(200, 200), default, vec2.One, Palette.White, Palette.White, Palette.White, Palette.White, Camera);

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
