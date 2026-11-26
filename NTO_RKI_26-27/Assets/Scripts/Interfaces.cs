using System;
using UnityEngine;

public interface IEventBus
{
    void Subscribe<T>(Action<T> handler) where T : struct;
    void Unsubscribe<T>(Action<T> handler) where T : struct;
    void Publish<T>(T evt) where T : struct;
    void Clear();
}
public interface IDamage
{
    void TakeDamage(int damage);
}
