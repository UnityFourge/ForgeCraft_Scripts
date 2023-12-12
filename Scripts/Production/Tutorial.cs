using UnityEngine;
using UnityEngine.UI;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button tutorialBtn;
    [SerializeField]private GameObject[] tutorials;
    [SerializeField] private GameObject panel;
    public string tutorialID;

    private int currentTutorial = 0;
    public static bool IsTutorialActive { get; private set; }

    void Start()
    {
        HIdeAll();
        IsTutorialActive = false;

        tutorialBtn.onClick.AddListener(OnPanel);
        foreach (var tutorial in tutorials)
        {
            Button nextButton = tutorial.GetComponentInChildren<Button>(true);
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(Next);
            }
        }
        int tutorialIndex = int.Parse(tutorialID);
        if (!GameManager.Instance.HasTutorialBeenShown(tutorialIndex))
        {
            OnPanel();
            GameManager.Instance.SetTutorialShown(tutorialIndex);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && panel.activeSelf)
        {
            Next();
        }
    }

    void OnPanel()
    {
        IsTutorialActive = true;

        SoundManager.Instance.SfxPlay(Enums.SFX.Button);

        if (currentTutorial < tutorials.Length)
        {
            panel.SetActive(true);
            tutorials[currentTutorial].SetActive(true);
        }
    }

    void Next()
    {
        SoundManager.Instance.SfxPlay(Enums.SFX.Button);
        tutorials[currentTutorial].SetActive(false);
        currentTutorial++;

        if (currentTutorial < tutorials.Length)
        {
            tutorials[currentTutorial].SetActive(true);
        }
        else 
        {
            currentTutorial = 0; 
            panel.SetActive(false);
            IsTutorialActive = false;
        }
    }

    void HIdeAll() 
    {
        foreach (var tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }
    }
}
