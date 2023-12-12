using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        //씬 이름
        SceneType = Define.Scene.Start;
        GameManager.UI.ShowSceneUI<UI_Start>();
        

    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}

