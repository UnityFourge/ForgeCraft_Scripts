using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        //씬 이름
        SceneType = Define.Scene.Battle;
        GameManager.UI.ShowSceneUI<UI_Battle>();


    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
