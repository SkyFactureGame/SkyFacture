
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
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
using Attribute = SkyFacture.Drawing.Buffers.Attribute;

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
			API = ContextAPI.OpenGL,
			APIVersion = new(3, 3),
			Size = new(640, 320),
			NumberOfSamples = 2,
			Title = GameName
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
	private static Region2D White, Black;
	private static Buffer<float> VBO, VBO_Pos, VBO_UV;
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
	public static readonly float[] Pos = new float[]
	{
		-0.5f, -0.5f,
		 0.5f, -0.5f,
		 0.5f,  0.5f,
		 0.5f,  0.5f,
		-0.5f,  0.5f,
		-0.5f, -0.5f,
	};
	public static readonly float[] UV = new float[]
	{
		0, 0,
		1, 0,
		1, 1,
		1, 1,
		0, 1,
		0, 0,
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
		Black = Atlas.Region("black")!;

		VAO = new(Shaders.DefShader);

		VBO = new(BufferTarget.ArrayBuffer, sizeof(float) * 4);
		VBO
			.Bind()
			.Init(Vert.Length * sizeof(float), 6, Vert);

		VBO_Pos = new(BufferTarget.ArrayBuffer, sizeof(float) * 2);
		VBO_Pos.Bind();
		VBO_Pos.Init(Pos.Length * sizeof(float), 6, Pos);
		VBO_UV = new(BufferTarget.ArrayBuffer, sizeof(float) * 2);
		VBO_UV.Bind();
		VBO_UV.Init(UV.Length * sizeof(float), 6, UV);

		//1 - buffer
		//VAO.BindAttribute(DefaultShader.vec2_Position, VBO);
		//VAO.BindAttribute(DefaultShader.vec2_UV, VBO);
		//2 - buffers
		VAO.BindAttribute(VBO_Pos.CreateAttribute(0, VertexAttribPointerType.Float), VBO_Pos);
		VAO.BindAttribute(DefaultShader.vec2_UV, VBO_UV);

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
		White.Use(TextureUnit.Texture2);
		
		mat4 ident = mat4.Identity;
		ident *= Camera.GetTranslation();
		ident *= mat4.CreateScale(150f, 150f, 1f);
		ident *= Camera.GetView();

		Draw.Color(Palette.Disco10.Invert());
		Shaders.DefShader.Texture(TextureUnit.Texture1);
		Shaders.DefShader.Matrix(ident);
		
		VAO.Draw(PrimitiveType.Triangles, 0, 6);

		Draw.Color(Palette.Disco5);
		Draw.Texture.Region(White, new((float)MathHelper.DegreesToRadians(MathA.Range(Time.RenderTime * 12, 360.0)), 0f, 0f), new(1f, 0f), vec2.One, Camera);
		Draw.Texture.Region(Black, new((float)MathHelper.DegreesToRadians(MathA.Range(-Time.RenderTime * 12, 360.0)), 0f, 0f), new(-1f, 0f), vec2.One, Camera);

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
