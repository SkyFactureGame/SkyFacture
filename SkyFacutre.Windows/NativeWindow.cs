using System.Runtime.InteropServices;

namespace SkyFacture.Windows;
public static class NativeWindow
{
	[DllImport("kernel32.dll")]
	public static extern nint GetConsoleWindow();
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(nint hWnd, DisplayType nCmdShow);

	[DllImport("user32.dll")]
	public static extern MessageButtonID MessageBox(nint hWnd, string? lpText, string? lpCaption, MessageType type);
}
public enum DisplayType : int
{
	Hide = 0,
	Show = 5,
}
public enum MessageType : uint
{
	AbortRetryIgnore =
	0x00000002,
	CancelTryContinue =
	0x00000006,
	Help =
	0x00004000,
	Ok =
	0x00000000,
	OkCancel =
	0x00000001,
	YesNo =
	0x00000004,
	InfoIcon =
	0x00000040,
	WarnIcon =
	0x00000030,
	ErrorIcon =
	0x00000010,
}
public enum MessageButtonID : int
{
	Ok = 1,
	Cancel = 2,
	Abort = 3,
	Retry = 4,
	Ignore = 5,
	Yes = 6,
	No = 7,
	TryAgain = 10
}
