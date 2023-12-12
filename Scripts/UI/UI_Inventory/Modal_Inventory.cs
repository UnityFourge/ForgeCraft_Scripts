using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Modal_Inventory : MonoBehaviour
{

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemRank;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI[] itemStatus;

    private string[] strStatus = new string[5] { "체력", "공격력", "공격 딜레이", "방어력", "사거리" };

    public void SetModal(Sprite image, int rank, string name, string desc, float[] status)
    {
        itemIcon.sprite = image;
        SetTextRank(rank);
        itemName.text = name;
        itemDesc.text = desc;
        SetTextStatus(status);
    }

    private void SetTextRank(int rank)
    {
        switch (rank)
        {
            case 0:
                itemRank.text = "하급";
                itemRank.color = Color.gray;
                break;
            case 1:
                itemRank.text = "중급";
                itemRank.color = Color.cyan;
                break;
            case 2:
                itemRank.text = "상급";
                itemRank.color = Color.yellow;
                break;
            case 3:
                itemRank.text = "최상급";
                itemRank.color = Color.red;
                break;
            default:
                itemRank.text = "";
                break;
        }
    }

    private void SetTextStatus(float[] status)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == 0)
            {
                itemStatus[i].gameObject.SetActive(false);
                continue;
            }

            if (status[i] > 0)
            {
                sb.Append(strStatus[i]).Append(" +").Append(status[i]);
            }
            else
            {
                sb.Append(strStatus[i]).Append(" -").Append(status[i]);

            }

            itemStatus[i].gameObject.SetActive(true);
            itemStatus[i].text = sb.ToString();
            sb.Clear();
        }
    }
}
