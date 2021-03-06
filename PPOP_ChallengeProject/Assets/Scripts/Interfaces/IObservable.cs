﻿using System.Collections;
using System.Collections.Generic;
using PathFinding;
using System;
using UnityEngine;

public interface IObservable : ITransform
{
    void RegisterObserverCallback(Action<IObservable> f);
    void UnRegisterObserverCallback(Action<IObservable> f);
    List<Action<IObservable>> ObservedCallbacks { get; }
}
