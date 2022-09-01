using System;
using System.Collections.Generic;

namespace SkyFacture;

public class Event : IEquatable<Event>
{
	private readonly int code;
	public Event(int code)
		=> this.code = code;
	public bool Equals(Event? other)
		=> ReferenceEquals(this, other);
	public override bool Equals(object? obj)
		=> ReferenceEquals(this, obj);

	private static readonly Dictionary<Event, List<Action>> events;
	public static void Invoke(Event evnt)
	{
		if (!events.ContainsKey(evnt))
			return;

		List<Action> actions = events[evnt];
		actions.ForEach(action => action());
	}
	public static void On(Event evnt, Action action)
	{
		if (!events.ContainsKey(evnt))
		{
			events[evnt] = new(8);
		}
		events[evnt].Add(action);
	}
	public static bool Remove(Event evnt, Action action)
	{
		if (events.ContainsKey(evnt))
		{
			List<Action> actions = events[evnt];

			return actions.Remove(action);
		}

		return false;
	}
	static Event()
	{
		events = new(32);
	}
}