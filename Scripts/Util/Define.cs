using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{

    public enum Scene
    {
        Unknown,
        Start,
        Story,
        Forge,
        Battle,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit,
    }

    public enum MouseEvent
    {
        Press,
        Click,
    }

}
