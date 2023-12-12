using UnityEngine;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject shield;

    [SerializeField] private Button nextBtn;

    [SerializeField] private GameObject forging;

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject lever;

    private Vector3 leverPosition;
    private Quaternion targetPosition;

    private int selectedWeapon;
    void Start()
    {
        HideAll();

        seletedWeapon();

        nextBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.SfxPlay(Enums.SFX.Button);
            Next();
        });

        leverPosition = lever.transform.position;
        targetPosition = target.transform.rotation;

        //SoundManager.Instance.SfxPlay(Enums.SFX.Blacksmith_Fire);
    }

    private void OnDisable()
    {
        HideAll();

        seletedWeapon();

        lever.transform.position = leverPosition;
        target.transform.rotation = targetPosition;
    }

    void HideAll() 
    {
        sword.SetActive(false);
        arrow.SetActive(false);
        axe.SetActive(false);
        shield.SetActive(false);
    }

    void seletedWeapon() 
    {
        selectedWeapon = ForgeManager.Instance.GetSelectedWeapon();

        switch (selectedWeapon)
        {
            case 1:
                sword.SetActive(true);
                break;
            case 2:
                arrow.SetActive(true);
                break;
            case 3:
                axe.SetActive(true);
                break;
            case 4:
                shield.SetActive(true);
                break;
        }
    }

    void Next() 
    {
        gameObject.SetActive(false);
        forging.SetActive(true);        
    }
}
