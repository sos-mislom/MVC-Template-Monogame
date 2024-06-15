using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

internal interface IClickable
{
    internal Rectangle Rectangle { get; set; }
    internal event OnClickEventHandler OnClick;
}

public delegate void OnClickEventHandler(params object[] args);