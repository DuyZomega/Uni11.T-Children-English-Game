using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider2 : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sfxvolumeSlider;
    void start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("sfxmusicVolume"))
        {
            PlayerPrefs.SetFloat("sfxmusicVolume", 1);
            SFXLoad();
        }

        else
        {
            SFXLoad();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SFXSave();
    }

    public void ChangeSFXVolume()
    {
        AudioListener.volume = sfxvolumeSlider.value;
        SFXSave();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void SFXLoad()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    private void SFXSave()
    {
        PlayerPrefs.SetFloat("sfxmusicVolume", sfxvolumeSlider.value);
    }
}
