using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private bool hasObj = false;
    private bool isSoundPanel = false;

    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private GameObject SoundPanel;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }    
    }

    public enum Sound
    {
        Bgm = 0,
        Effect,
        MaxCount = 2,
    }

    AudioSource[] _audiosources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (hasObj) return;

        GameObject root = GameObject.Find("SoundManager");
        if (root == null)
        {
            root = new GameObject { name = "SoundManager" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audiosources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audiosources[(int)Sound.Bgm].loop = true;
        }
        else
        {
            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                _audiosources[i] = root.transform.GetChild(i).GetComponent<AudioSource>();
            }
            _audiosources[0].loop = true;
        }

        Play("100100", SoundManager.Sound.Bgm);

        hasObj = true;
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audiosources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        if(type == Sound.Bgm)
        {
            AudioSource audioSource = _audiosources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audiosources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    private AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (path.Contains("Sound/") == false) path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if(type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if(_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }

    public void ControlSoundPanel()
    {
        isSoundPanel = !isSoundPanel;

        if (isSoundPanel)
        {
            SoundManager.instance.Play("Pause In", SoundManager.Sound.Effect);
            SoundPanel.SetActive(true);
        }
        else
        {
            SoundManager.instance.Play("Pause Out", SoundManager.Sound.Effect);
            SoundPanel.SetActive(false);
        }
    }

    private void SetSoundValue()
    {
        if (!isSoundPanel) return;

        _audiosources[0].volume = BGMSlider.value;
        _audiosources[1].volume = SFXSlider.value;
    }

    private void Update()
    {
        SetSoundValue();
    }
}
