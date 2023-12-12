using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class UI_Button : UI_Popup
{
    


    enum Buttons
    {
        PointButton,
        
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        Debug.Log("Start"+GetButton((int)Buttons.PointButton));

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;

        

        GetText((int)Texts.ScoreText).text = "테스트";
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }


    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("점수업");
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }

    





}
