using System.IO;
using Microsoft.Xna.Framework.Input;

namespace GameProject;

public class Globals
{
    public static int ScreenW { get; set; } = 900;
    public static int ScreenH { get; set; } = 900;

    public static EnumScenes CurrentScene { get; set; } = EnumScenes.Start;
    public static bool Exit = false;
    public static bool IsGameActive = true;

    public static MouseState MouseState;
    public static MouseState OldMouseState;

}