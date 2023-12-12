using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Start_Button : MonoBehaviour
{

    public void OnMouseEnter()
    {
        gameObject.GetComponent<Animator>().SetTrigger("on");
    }
    public void OnMouseExit()
    {
        gameObject.GetComponent<Animator>().SetTrigger("off");
    }

}
