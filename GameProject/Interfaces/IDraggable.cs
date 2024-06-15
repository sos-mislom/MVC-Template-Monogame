using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

internal interface IDraggable
{
    internal Rectangle Rectangle { get; set; }
    internal event OnDragEventHandler OnDrag;
}

public delegate void OnDragEventHandler(params object[] args);