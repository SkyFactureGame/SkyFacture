namespace SkyFacture.Drawing;

public abstract class GLObj
{
	public readonly i32 Handle;
	public GLObj(i32 handle)
	{
		Handle = handle;
#if DEBUG
		System.Console.WriteLine($"Named new {this.GetType().Name} with id {handle}");
#endif
	}
}