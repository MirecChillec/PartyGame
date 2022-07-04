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
    public static float a= 1, b=1, c=1;

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
        master.value = a;
        sfx.value = b;
        music.value = c;
        base.Show();
    }
    public void MainAudio(float value)
    {
        mixer.SetFloat("master", Mathf.Log10(value) * 20);
        a = value;
    }
    public void SFXAudio(float value)
    {
        mixer.SetFloat("sfx", Mathf.Log10(value) * 20);
        b = value;

    }
    public void MusicAudio(float value)
    {
        mixer.SetFloat("music", Mathf.Log10(value) * 20);
        c = value;
    }
}

