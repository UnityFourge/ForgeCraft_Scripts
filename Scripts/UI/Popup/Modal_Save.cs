using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modal_Save : MonoBehaviour
{
    [SerializeField] private Button button_Confirm;

    private void Awake()
    {
        button_Confirm.onClick.AddListener(() => SoundManager.Instance.SfxPlay(Enums.SFX.Button));
    }
}
