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
			UpdateFrequency = 50,
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
		window.Title = $"Sky Facture Draw+T:{Draw.Texture.SW.ElapsedMilliseconds}ms, ";
		Draw.Texture.SW.Reset();

		GL.ClearColor(Palette.Disco10);
		GL.Clear(ClearBufferMask.ColorBufferBit);


		const int size = 45;
		for (int x = -size; x < size; x++)
		{
			for (int y = -size; y < size; y++)
			{
				if (x % 2 == 0 && y % 2 != 0 || y % 2 == 0 && x % 2 != 0)
					Draw.Region(Sand, new(x, y), Cam);
				else
					Draw.Region(Atlas.Black, new(x, y), Cam);
			}
		}
		window.Context.SwapBuffers();
	}
	protected internal static void Resize(ResizeEventArgs args)
	{
		GL.Viewport(0, 0, args.Width, args.Height);
		Screen.Width = args.Width;
		Screen.Height = args.Height;
		Cam.UpdateProjMatrix(args.Width, args.Height);
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
		float move = kstate.IsKeyDown(Keys.LeftShift) ? 6f : 2.1f;
		vec3 moveDelta = default;
		if (kstate.IsKeyDown(Keys.A))
		{
			moveDelta.X -= move * (float)Time.UpdateDelta;
		}
		if (kstate.IsKeyDown(Keys.D))
		{
			moveDelta.X += move * (float)Time.UpdateDelta;
		}
		if (kstate.IsKeyDown(Keys.W))
		{
			moveDelta.Y += move * (float)Time.UpdateDelta;
		}
		if (kstate.IsKeyDown(Keys.S))
		{
			moveDelta.Y -= move * (float)Time.UpdateDelta;
		}
		Cam.Position -= moveDelta;
	}
	protected internal static void KeyUp(KeyboardKeyEventArgs args)
	{

	}
	protected internal static void KeyDown(KeyboardKeyEventArgs args)
	{
		if (args.Key == Keys.F11)
		{
			if (window.WindowState != WindowState.Fullscreen)
			{
				window.WindowState = WindowState.Fullscreen;
			}
			else
			{
				window.WindowState = WindowState.Normal;
				window.Size = window.MinimumSize ?? new(720, 480);
				window.CenterWindow();
			}
		}
		else if (args.Key == Keys.F10)
		{
			window.VSync = window.VSync != VSyncMode.On ? VSyncMode.On : VSyncMode.Off;
			Console.WriteLine(window.VSync);
		}
	}
}
