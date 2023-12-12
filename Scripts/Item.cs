using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemID;
    //public float itemHealth;
    //public float itemAttack;
    //public float itemAttackDelay;
    //public float itemDefence;
    //public float itemAttackRange;
    public float[] itemStatus;
    public SpriteRenderer spriteRenderer; // 게임 오브젝트에 사용될 SpriteRenderer
    public Image image; // UI에 사용될 Image

    public void ItemInit()
    {
        ItemSO Select = DataManager.Instance.items[0]; // 수정
        Select = DataManager.Instance.items[0];
        itemName = Select.itemName;
        itemID = Select.itemID;
        //itemHealth = Select.itemHealth;
        //itemAttack = Select.itemAttack;      
        //itemAttackDelay = Select.itemAttackDelay;
        //itemDefence = Select.itemDefence;
        //itemAttackRange = Select.itemAttackRange;
        itemStatus = Select.itemStatus;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = Select.itemSprite; // SpriteRenderer에 스프라이트 설정
        }

        if (image != null)
        {
            image.sprite = Select.itemSprite; // Image에 스프라이트 설정
        }
    }
}
