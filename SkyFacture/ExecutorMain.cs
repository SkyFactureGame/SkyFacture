using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkyFacture.Drawing;
using SkyFacture.Drawing.Shading;
using SkyFacture.Drawing.Sprites;
using System;

namespace SkyFacture;

public unsafe class ExecutorMain
{
	internal ExecutorMain() { }
	private static readonly GameWindow window = new(
		new()
		{
			RenderFrequency = 480,
			UpdateFrequency = 20,
		},
		new()
		{
			API = ContextAPI.OpenGL,
			APIVersion = new(3, 3),
			Size = new(720, 480),
			Title = SF.GameName
		}
		);
#pragma warning disable CS8618
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
	private static Region2D Sand;
#pragma warning restore CS8618
	protected internal static void Load()
	{
		Console.WriteLine("Max Texture Binding: " + GL.GetInteger(GetPName.MaxTextureImageUnits));
		Console.WriteLine("OpenGL Version: " + GL.GetInteger(GetPName.MajorVersion) + "." + GL.GetInteger(GetPName.MinorVersion));
		Blend.Enable();
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


		const int size = 25;
		for (int x = -size; x < size; x++)
		{
			for (int y = -size; y < size; y++)
			{
				Draw.Region(Sand, new(x, y), Cam, true);
			}
		}
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
		KeyboardState kstate = window.KeyboardState;
		bool sizeP, sizeM;
		sizeP = kstate.IsKeyDown(Keys.Equal);
		sizeM = kstate.IsKeyDown(Keys.Minus);
		if (sizeM && !sizeP || sizeP && !sizeM)
		{
			Cam.Scale += (sizeP ? 1f : -1f) * (float)Time.UpdateDelta;
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
		else if (args.Key == Keys.F10)
		{
			window.VSync = window.VSync != VSyncMode.On ? VSyncMode.On : VSyncMode.Off;
			Console.WriteLine(window.VSync);
		}
	}
}
