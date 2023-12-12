using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        //씬 이름
        SceneType = Define.Scene.Forge;
        GameManager.UI.ShowSceneUI<UI_Forge>();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
