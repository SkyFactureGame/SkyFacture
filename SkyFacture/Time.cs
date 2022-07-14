﻿// The NiTiS-Dev licenses this file to you under the MIT license.

namespace SkyFacture;
public static class Time
{
	public static double RenderTime { get; internal set; }
	public static double UpdateTime { get; internal set; }
	public static double RenderDelta { get; internal set; }
	public static double UpdateDelta { get; internal set; }
}
