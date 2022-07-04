using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : BaseScren
{
    [SerializeField] AudioMixer mixer;
    public Slider master, sfx, music;
    public string masterP, sfxP, musicP;

    public void Back()
    {
        GameData.menuManager.Hide<Settings>();
        GameData.menuManager.Show<MainMenu>();
    }
    private void Awake()
    {
        master.onValueChanged.AddListener(MainAudio);
        sfx.onValueChanged.AddListener(SFXAudio);
        music.onValueChanged.AddListener(MusicAudio);
    }
    public override void Show()
    {
        float a, b, c;
        mixer.GetFloat("master", out a);
        mixer.GetFloat("sfx", out b);
        mixer.GetFloat("music", out c);
        master.value = a+1;
        sfx.value = b+1;
        music.value = c+1;
        base.Show();
    }
    public void MainAudio(float value)
    {
        mixer.SetFloat("master", Mathf.Log10(value) * 20);
    }
    public void SFXAudio(float value)
    {
        mixer.SetFloat("sfx", Mathf.Log10(value) * 20);
    }
    public void MusicAudio(float value)
    {
        mixer.SetFloat("music", Mathf.Log10(value) * 20);
    }
}

