using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Subscribe<T>(Action<T> handler) where T : struct
    {
        if (handler == null) return;
        var type = typeof(T);
        if (!_handlers.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _handlers[type] = list;
        }
        if (!list.Contains(handler)) list.Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler) where T : struct
    {
        if (handler == null) return;
        if (!_handlers.TryGetValue(typeof(T), out var list)) return;
        list.Remove(handler);
        if (list.Count == 0) _handlers.Remove(typeof(T));
    }

    public void Publish<T>(T evt) where T : struct
    {
        if (!_handlers.TryGetValue(typeof(T), out var list) || list.Count == 0) return;

        var sub = list.ToArray();
        for (int i = 0; i < sub.Length; i++)
        {
            try { ((Action<T>)sub[i]).Invoke(evt); }
            catch { }
        }
    }

    public void Clear() => _handlers.Clear();
}