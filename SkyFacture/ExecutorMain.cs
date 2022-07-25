
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
	public static ResourceLoader ResourceLoader { get; protected internal set; } 
	public static GameVersion Version => new(V: 0, r: 1);
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
	private static Camera Cam;
	private static Region2D White, Black, Sand;
#pragma warning restore CS8618
	protected internal static void Load()
	{
		Console.WriteLine("Max Texture Binding: " + GL.GetInteger(GetPName.MaxTextureImageUnits));
		Console.WriteLine("OpenGL Version: " + GL.GetInteger(GetPName.MajorVersion) + "." + GL.GetInteger(GetPName.MinorVersion));
		Blend.Enable();
		Atlas.LoadInternalRegions();
		White = Atlas.Region("white")!;
		Black = Atlas.Region("black")!;
		Sand = Atlas.Region("sand")!;

		Cam = new(new(0, 0, 1f));
		GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
	}
	protected internal static void GraphicUpdate(FrameEventArgs args)
	{
		Time.RenderDelta = args.Time;
		Time.RenderTime += args.Time;

		GL.ClearColor(Palette.Disco10);
		GL.Clear(ClearBufferMask.ColorBufferBit);

		Draw.Color(Palette.Disco10);
		Draw.Region(Black, new(-1f, 0f), Cam, true);
		Draw.Region(White, new((float)MathHelper.DegreesToRadians(MathA.Range(-Time.RenderTime * 12, 360.0)), 0f, 0f), new(-1f, 0f), vec2.One, Cam, true);
		Draw.Region(Sand, Cam, true);


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
			window.WindowState = WindowState.Fullscreen != window.WindowState ? WindowState.Fullscreen : WindowState.Normal;
		}
	}
}
