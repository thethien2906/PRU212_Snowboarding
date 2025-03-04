using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;

    [Header("SFX Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParameter;

    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParameter;

    void Start()
    {
        float currentBGM;
        if (audioMixer.GetFloat(bgmParameter, out currentBGM))
        {
            bgmSlider.value = Mathf.Pow(10, currentBGM / 20); // Convert dB back to linear
        }

        float currentSFX;
        if (audioMixer.GetFloat(sfxParameter, out currentSFX))
        {
            sfxSlider.value = Mathf.Pow(10, currentSFX / 20);
        }
    }

    public void SFXSliderValue(float value)
    {
        float mixerValue = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat(sfxParameter, mixerValue);

        float currentValue;
        audioMixer.GetFloat(sfxParameter, out currentValue);
        Debug.Log("SFX Mixer Set to: " + currentValue);
    }

    public void BGMSliderValue(float value)
    {
        float mixerValue = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat(bgmParameter, mixerValue);

        float currentValue;
        audioMixer.GetFloat(bgmParameter, out currentValue);
        Debug.Log("BGM Mixer Set to: " + currentValue);
    }


}
