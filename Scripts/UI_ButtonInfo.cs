using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class UI_ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public GameObject UI_Info;
    public TextMeshProUGUI text_Info;

    [SerializeField] RectTransform rectTransform;

    private Camera mainCamera;
    private bool isActive = false;

    private float screenHeight;
    private float screenWidth;
    private float popupHeight;
    private float popupWidth;

    private float popupPos_x;
    private float popupPos_y;
    private float cameraPos_z;

    private Dictionary<string, string> textInfos;

    private void Awake()
    {
        popupHeight = rectTransform.rect.height; 
        popupWidth = rectTransform.rect.width;

        mainCamera = Camera.main;

        screenHeight = Screen.height;
        screenWidth = Screen.width;
        cameraPos_z = -mainCamera.transform.position.z;

        textInfos = new Dictionary<string, string>();
    }

    private void Start()
    {
        InitInfos();
    }

    public void Update()
    {
        Update_PopupPos();
    }

    private void InitInfos()
    {
        textInfos.Add("Button_Blacksmith", "대장간으로");
        textInfos.Add("Button_Inventory", "인벤토리");
        textInfos.Add("Button_ExploreSet", "모험 떠나기");
        textInfos.Add("Button_Upgrade", "업그레이드");
        textInfos.Add("Button_Save", "저장하기");
        textInfos.Add("Button_Rest", "휴식하기");
        textInfos.Add("Button_Forge", "돌아가기");
        textInfos.Add("Button_Forging", "작업하기");
        textInfos.Add("Button_NPC", "의뢰 수락");

        textInfos.Add("Button_Sword", "검 제작");
        textInfos.Add("Button_Bow", "활 제작");
        textInfos.Add("Button_Axe", "도끼 제작");
        textInfos.Add("Button_Shield", "방패 제작");
        textInfos.Add("Button_Materials", "화로 확인");
        textInfos.Add("Button_Next", "다음 단계");
        textInfos.Add("Button_Submaterial", "보조 재료");
        textInfos.Add("Button_Cooper", "구리 삽입");
        textInfos.Add("Button_Tin", "주석 삽입");
        textInfos.Add("Button_Iron", "철 삽입");
        textInfos.Add("Button_Mithril", "미스릴 삽입");
        textInfos.Add("Button_Orichalcum", "오리할콘 삽입");
        textInfos.Add("Button_Solarium", "솔라리움 삽입");
        textInfos.Add("Button_Temp", "온도 상승");
        textInfos.Add("Button_Hammer", "두드리기");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isActive = true;

        UI_Info.SetActive(true);

        string uiName = this.gameObject.name;

        text_Info.text = textInfos[uiName].ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isActive = false;
        CloseUI_Info();
    }

    private void Update_PopupPos()
    {
        if (isActive)
        {
            Vector2 mousePos = Input.mousePosition;

            if (mousePos.x + popupWidth + 5f > screenWidth)
            {
                popupPos_x = mousePos.x - popupWidth - 5f;
            }
            else
            {
                popupPos_x = mousePos.x + 5f;
            }

            if (mousePos.y + popupHeight + 5f > screenHeight)
            {
                popupPos_y = mousePos.y - popupHeight - 5f;
            }
            else
            {
                popupPos_y = mousePos.y + 5f;
            }
            UI_Info.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(popupPos_x, popupPos_y, cameraPos_z));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        CloseUI_Info();
    }

    public void CloseUI_Info()
    {
        UI_Info.SetActive(false);
    }
}
