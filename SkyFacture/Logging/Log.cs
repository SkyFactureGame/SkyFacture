using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace SkyFacture.Logging;
public static class Log
{
	public static void ShowConsole()
		=> DesktopConsole.ShowWindow(DesktopConsole.GetConsoleWindow(), DesktopConsole.SW_SHOW);
	public static void HideConsole()
		=> DesktopConsole.ShowWindow(DesktopConsole.GetConsoleWindow(), DesktopConsole.SW_HIDE);
	private static class DesktopConsole
	{
		[DllImport("kernel32.dll")]
		public static extern nint GetConsoleWindow();

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(nint hWnd, int nCmdShow);

		public const int SW_HIDE = 0;
		public const int SW_SHOW = 5;
	}
}
