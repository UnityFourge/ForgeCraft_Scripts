using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public int itemID;
        public Enums.ItemType itemType;
        public int itemRank;
        public int itemScore;
        public int stack;

        public InventoryItem(int itemID = -1, Enums.ItemType itemType = default, int itemScore = 0, int stack = 1)
        {
            this.itemID = itemID;
            this.itemType = itemType;
            this.itemScore = itemScore;
            if (itemScore >= 90) this.itemRank = 3;
            else if (itemScore >= 70) this.itemRank = 2;
            else if (itemScore >= 20) this.itemRank = 1;
            else this.itemRank = 0;

            this.stack = stack; 
        }
    }

    public List<InventoryItem> GetEquipItems(Enums.ItemType type) 
    {
        return bag.items.Where(x => DataManager.Instance.GetItem(x.itemID).itemType == type).ToList();
    }

    public class Bag
    {
        public List<InventoryItem> items = new List<InventoryItem>();
    }

    public Bag bag;

    public void Init()
    {
        bag = new Bag();

        SceneManager.sceneLoaded += TestInitInventory; // Test
    }

    public void AddItem(ItemSO itemSO)
    {
        var item = bag.items.Where(x => x.itemID == itemSO.itemID).FirstOrDefault();
        if (item != null && item.stack < itemSO.maxStack)
        {
            item.stack++;
        }
        else
        {
            bag.items.Add(new InventoryItem(itemSO.itemID, itemSO.itemType));
        }
    }

    public void AddItem(ItemSO itemSO, int count)
    {
        var item = bag.items.Where(x => x.itemID == itemSO.itemID).FirstOrDefault();

        if (item != null)
        {
            if (item.stack + count <= itemSO.maxStack) item.stack += count;
            else
            {
                item.stack = itemSO.maxStack;
                bag.items.Add(new InventoryItem(itemSO.itemID, itemSO.itemType, 0, item.stack + count - itemSO.maxStack));
            }
        }
        else
        {
            bag.items.Add(new InventoryItem(itemSO.itemID, itemSO.itemType, 0, count));
        }
    }

    public void AddWeapon(InventoryItem item)
    {
        bag.items.Add(item);
    }

    public void SubItem(ItemSO itemSO)
    {
        var item = bag.items.Where(x => x.itemID == itemSO.itemID).FirstOrDefault();

        if (item != null)
        {
            if (item.stack > 1) item.stack--;
            else
            {
                bag.items.Remove(item);
            }
        }
    }

    public void SubItem(InventoryItem _item)
    {
        var item = bag.items.Where(x => x.itemID == _item.itemID).FirstOrDefault();

        if (item != null)
        {
            if (item.stack > 1) item.stack--;
            else
            {
                bag.items.Remove(item);
            }
        }
    }

    public void SubItem(ItemSO itemSO, int count)
    {
        var item = bag.items.Where(x => x.itemID == itemSO.itemID).FirstOrDefault();

        if (item != null )
        {
            if (item.stack > count) item.stack--;
            else if (item.stack == count) bag.items.Remove(item);
            else Debug.LogError("Not enough items!");
        }
    }

    public void TestInitInventory(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Forge" && Player.Instance.InitFlagInventory)
        {
            Player.Instance.InitFlagInventory = false;

            AddWeapon(new InventoryItem(100, Enums.ItemType.Weapon, 50));
            AddWeapon(new InventoryItem(101, Enums.ItemType.Weapon, 50));
            AddWeapon(new InventoryItem(102, Enums.ItemType.Weapon, 50));
            AddWeapon(new InventoryItem(103, Enums.ItemType.Weapon, 50));
            AddItem(DataManager.Instance.GetItem(0), 12);
            AddItem(DataManager.Instance.GetItem(1), 4);
            AddItem(DataManager.Instance.GetItem(6), 5);
            AddItem(DataManager.Instance.GetItem(8), 5);
            AddItem(DataManager.Instance.GetItem(7), 1);
        }
    }
}
