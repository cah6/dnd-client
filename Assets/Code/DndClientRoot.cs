using System;
using UnityEngine;
using strange.extensions.context.impl;
 
public class DndClientRoot : ContextView
{
    void Awake() {
        context = new DndClientContext(this);
    }
}