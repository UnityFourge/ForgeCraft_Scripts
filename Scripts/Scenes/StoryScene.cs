using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Story;

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q)) 
        {
            SceneManager.LoadScene("Start");
        }
    }

    public override void Clear()
    {
        //Debug.Log("LoginScene Clear!");
    }
}
