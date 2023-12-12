using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

public class POPUP_Manage : MonoBehaviour
{
    [SerializeField] private Slot_Manage slotPrefab;
    [SerializeField] private Transform slotParent_Manage;
    private Slot_Manage[] slots_Manage;
    
    [SerializeField] private Image imageExpedition;
    [SerializeField] private Slot_Equip[] slot_Equips;
    [SerializeField] private TextMeshProUGUI[] textStatus;
    [SerializeField] private UI_EquipInventory equipInventory;

    List<Slot_Manage> pool;

    private void Awake()
    {
        slots_Manage = slotParent_Manage.GetComponentsInChildren<Slot_Manage>();

        pool = new List<Slot_Manage>();
    }

    private void OnEnable()
    {

        FreshSlotManage();
    }

    private void Start()
    {
        slots_Manage[0].InitSelectExpedition();
    }

    private void Init()
    {
        for (int i = 0; i < slots_Manage.Length; i++)
        {
            slots_Manage[i].gameObject.SetActive(false);
        }

        int len = Player.Instance.characterList.roster.characters.Count;

        slots_Manage = new Slot_Manage[len];

        for (int i = 0; i < len; i++)
        {
            slots_Manage[i] = Get();
            slots_Manage[i].PopupManage = this;
            slots_Manage[i].EquipInventory = equipInventory;
        }
    }

    private Slot_Manage Get()
    {
        Slot_Manage select = null;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                select = pool[i];
                select.gameObject.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(slotPrefab, slotParent_Manage);
            pool.Add(select);
        }

        return select;
    }

    public void FreshSlotManage()
    {
        Init();

        int n = Player.Instance.characterList.roster.characters.Count;

        for (int i = 0; i < n; i++)
        {
            slots_Manage[i].Character = Player.Instance.characterList.roster.characters[i];
        }
    }

    public void ChangeImageExpedition(CharacterSO characterSO)
    {
        if (imageExpedition == null || characterSO == null) return;

        imageExpedition.color = new Color(1, 1, 1, 1);
        imageExpedition.sprite = characterSO.CharacterSprite;
    }

    public void ChangeImageEquip(ItemSO[] itemSO)
    {
        if (slot_Equips == null) return;

        for (int i = 0; i < slot_Equips.Length; i++)
        {
            slot_Equips[i].SetItemEquip(itemSO[i]);
        }
    }

    public void ChanageTextStatus(float[] status)
    {
        if (textStatus == null || status == null) return;

        for (int i = 0; i < status.Length; i++)
        {
            textStatus[i].text = status[i].ToString();
        }
    }
}
