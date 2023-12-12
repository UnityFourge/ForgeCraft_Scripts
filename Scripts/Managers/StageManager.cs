using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance 
    { 
        get { return instance == null ? null : instance; } 
    }

    public GameObject ResultWindow;
    public GameObject[] EnemyPrefabs;

    public Expedition[] expeditions;
    public EnemyBehavior enemy;

    public Spawner _spawner;
    public Transform EnemySpawnPoint;

    public UI_HPGauge _hp;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float[] spriteY;
    [SerializeField] private SpriteRenderer background;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Init();
    }

    private void Init()
    {
        int stage = Player.Instance.SelectStage - 1;
        _spawner.Spawn();
        Spawn(EnemySpawnPoint, stage);
        CharacterManager.Instance.Init();

        expeditions = CharacterManager.Instance.Expeditions;
        enemy = CharacterManager.Instance.Enemy;

        SkillManager.Instance.Init(expeditions, enemy);
        _hp.Init();

        background.sprite = sprites[stage];
        background.transform.position = new Vector2(0, spriteY[stage]);
    }

    public void GameOver()
    {
        UI_GameResult gameResult = ResultWindow.GetComponent<UI_GameResult>();
        bool allCharacterDead = true;

        if (enemy.EnemyHealth <= 0)
        {
            ResultWindow.SetActive(true);
            gameResult.ResultInit();
        }
        else
        {
            foreach (Expedition exp in expeditions)
            {
                if (exp.curHP > 0)
                {
                    allCharacterDead = false;
                    break;
                }
            }
            if (allCharacterDead)
            {
                ResultWindow.SetActive(true);
                gameResult.ResultInit();
            }
        }
    }

    public void Spawn(Transform spawnPoint, int type)
    {
        enemy = Instantiate(EnemyPrefabs[type], spawnPoint.position, Quaternion.identity, CharacterManager.Instance.transform).GetComponent<EnemyBehavior>();
    }
}
