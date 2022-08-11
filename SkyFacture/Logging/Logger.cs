using System;

namespace SkyFacture.Logging;
public abstract class Logger : IDisposable
{
	public abstract void Info(string from, string message);
	public abstract void Warn(string from, string message);
	public abstract void Error(string from, string message);
	public abstract void Fatal(string from, string message);
	public abstract void Dispose();
}
