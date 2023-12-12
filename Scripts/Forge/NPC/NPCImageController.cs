using UnityEngine;
using UnityEngine.UI;
public class NPCImageController : MonoBehaviour
{
    public SpriteRenderer orderWeaponSpriteRenderer;
    private int weaponId;

    public void Initialize(int weaponId)
    {
        ItemSO weaponItem = DataManager.Instance.GetItem(weaponId);
        if (weaponItem != null)
        {
            SetOrderWeaponImage(weaponItem.itemSprite);
        }
    }

    public void SetOrderWeaponImage(Sprite itemSprite)
    {
        if (orderWeaponSpriteRenderer != null && itemSprite != null)
        {
            orderWeaponSpriteRenderer.sprite = itemSprite;
        }
    }
}
