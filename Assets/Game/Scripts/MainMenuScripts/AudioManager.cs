using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource m_AudioSource;

    public List<GameObject> gameObjectsToScale;
    public AudioSource audioSource;
    public int spectrumIndex = 0; // Indice dello spettro da analizzare (frequenza specifica)
    public float scaleMultiplier = 1.0f; // Quanto amplificare la scala
    public float bounceSpeed = 5.0f;

    private float[] spectrumData = new float[256];
    private Dictionary<GameObject, Vector3> originalScales;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalScales = new Dictionary<GameObject, Vector3>();
        foreach (var obj in gameObjectsToScale)
        {
            if (obj != null)
                originalScales[obj] = obj.transform.localScale;
        }
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        if (spectrumIndex < 0 || spectrumIndex >= spectrumData.Length)
        {
            Debug.LogWarning("Indice dello spettro non valido, correggo a 0.");
            spectrumIndex = 0;
        }

        float spectrumValue = spectrumData[spectrumIndex] * scaleMultiplier;

        foreach (var obj in gameObjectsToScale)
        {
            if (obj != null && originalScales.ContainsKey(obj))
            {
                Vector3 targetScale = originalScales[obj] + Vector3.one * spectrumValue;
                obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, targetScale, Time.deltaTime * bounceSpeed);
            }
        }
    }

    public void PlaySound(AudioClip clip)

    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip passato è null.");
            return;
        }

        GameObject tempAudioSourceObject = new GameObject("TempAudioSource");
        AudioSource tempAudioSource = tempAudioSourceObject.AddComponent<AudioSource>();

        tempAudioSource.clip = clip;
        tempAudioSource.playOnAwake = false;
        tempAudioSource.volume = 1f; // Regola il volume da qui

        tempAudioSource.Play();

        Destroy(tempAudioSourceObject, clip.length);
    }
}
