using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject forgeUI;

    public void StartBtn()
    {
        SceneManager.LoadScene("Story");
    }

    // 스토리 스킵 버튼
    public void testForgeBtn()
    {
        SceneManager.LoadScene("Forge");
    }

    public void BattleBtn()
    {
        SceneManager.LoadScene("Battle");
    }

    // 가챠버튼
    public void GOTCHA(GameObject gameObject)
    {
        gameObject.SetActive(false);
        // 가챠 BOX 애니메이션 활성화 하는곳
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void OnOffpopUp(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    // 어떻게 인벤토리 내용물을 가져올까?
    // Cancel은 OnOffpopUp 사용
    public void ConfirmModal(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void OnForge()
    {
        forgeUI.SetActive(true);
        //blacksmithObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnOffOther(GameObject other)
    {
        OnOffpopUp(other);
        //OnOffpopUp(this.gameObject);
        //OnOffpopUp(blacksmithObject);
    }
}
