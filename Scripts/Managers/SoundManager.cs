using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance == null ? null : instance; }
    }

    [SerializeField] private AudioSource sfxPrefab;
    private Transform sfxParent;

    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [SerializeField] private Enums.BGM[] bgmClipsKeys;
    [SerializeField] private AudioClip[] bgmClipsValues;
    private Dictionary<Enums.BGM, AudioClip> bgmClips;

    [SerializeField] private Enums.SFX[] sfxClipsKeys;
    [SerializeField] private AudioClip[] sfxClipsValues;
    private Dictionary<Enums.SFX, AudioClip> sfxClips;

    private List<AudioSource> pool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        sfxParent = this.transform;
        pool = new List<AudioSource>();

        Init();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("masterVolume", bgmVolume);
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }


    private void Init()
    {
        bgmClips = new Dictionary<Enums.BGM, AudioClip>();
        sfxClips = new Dictionary<Enums.SFX, AudioClip>();

        for (int i = 0; i < bgmClipsKeys.Length; i++)
        {
            bgmClips.Add(bgmClipsKeys[i], bgmClipsValues[i]);
        }

        for (int i = 0; i < sfxClipsKeys.Length; i++)
        {
            sfxClips.Add(sfxClipsKeys[i], sfxClipsValues[i]);
        }

        masterVolume = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);

        bgmSource.volume = masterVolume * bgmVolume;
    }

    private AudioSource Get()
    {
        AudioSource select = null;

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
            select = Instantiate(sfxPrefab, sfxParent);
            pool.Add(select);
        }

        select.volume = masterVolume * sfxVolume;

        return select;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Enums.BGM name = (Enums.BGM)System.Enum.Parse(typeof(Enums.BGM), scene.name);

        if (name == Enums.BGM.Battle)
        {
            int index = Player.Instance.SelectStage + 3;

            BgmPlay((Enums.BGM)index);
        }
        else
        {
            BgmPlay(name);
        }
    }

    private void BgmPlay(Enums.BGM name)
    {
        if (bgmClipsKeys.Length == 0) return;

        bgmSource.clip = bgmClips[name];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SfxPlay(Enums.SFX name)
    {
        if (Tutorial.IsTutorialActive && name != Enums.SFX.Button)
        {
            return;
        }

        if (sfxClipsKeys.Length == 0) return;

        AudioSource audioSource = Get();

        //if (audioSource.isPlaying) return;

        audioSource.clip = sfxClips[name];
        
        audioSource.Play();

        StartCoroutine(CheckPlaying(audioSource));
    }

    private IEnumerator CheckPlaying(AudioSource audioSource)
    {
        while (true)
        {
            yield return CoroutineHelper.WaitForSeconds(1.0f);

            if (!audioSource.isPlaying)
            {
                audioSource.gameObject.SetActive(false);
            }
        }
    }
    public void RandomSfxPlay(Enums.SFX[] clips)
    {
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        Enums.SFX selectedClip = clips[randomIndex];
        SfxPlay(selectedClip);
    }


}