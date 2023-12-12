using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotchaResult : MonoBehaviour
{
    public Image GotchaResultImage;
    public Image Gold;
    public Sprite gold;
    public int ResultGold;
    public Text Goldtxt;
    private void Start()
    {
        gold = Gold.sprite;
        ResultGold = 0;
    }
    public void CharacterInit(PickUpTable _character, GameObject gold)
    {
        gameObject.SetActive(true);
        gold.SetActive(false);

        GotchaResultImage = GetComponent<Image>();
        GotchaResultImage.sprite = _character.Character.CharacterIcon;
    }
    public void GoldInit(PickUpTable pick, GameObject gold)
    {
        gold.SetActive(true);

        GotchaResultImage = GetComponent<Image>();
        GotchaResultImage.sprite = pick.Character.CharacterIcon;


        switch (pick.Grade)
        {
            case Enums.CharacterGrade.S:
                Goldtxt.text = " + 100";
                ResultGold += 100;
                break;
            case Enums.CharacterGrade.A:
                Goldtxt.text = " + 50";
                ResultGold += 50;
                break;
            case Enums.CharacterGrade.B:
                Goldtxt.text = " + 25";
                ResultGold += 100;
                break;
            case Enums.CharacterGrade.C:
                Goldtxt.text = " + 10";
                ResultGold += 50;
                break;
        }
    }
}
