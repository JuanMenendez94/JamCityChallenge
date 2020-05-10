using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IObservable : ITintable
{
    void RegisterObserverCallback(Action<IObservable> f);
    void UnRegisterObserverCallback(Action<IObservable> f);
    List<Action<IObservable>> ObservedCallbacks { get; }
}
