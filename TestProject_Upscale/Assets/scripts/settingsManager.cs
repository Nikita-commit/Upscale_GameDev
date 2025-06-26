using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class settingsManager : MonoBehaviour
{
    public enum Difficulty { Easy, Middle, Hard }
    public static Difficulty currentDifficulty = Difficulty.Easy; // по умолчанию
    public Button[] difficultyButtons;

    public Toggle[] toggles;

    public AudioSource musicSource;

    [SerializeField] private TextMeshProUGUI sliderText = null;
    [SerializeField] private float maxSliderAmount = 100.0f;
    [SerializeField] private AudioMixer myAudioMixer;

    private void Start()
    {
        toggles[0].onValueChanged.AddListener(ToggleChangedNot);
        toggles[1].onValueChanged.AddListener(ToggleChangedTrn);
        toggles[2].onValueChanged.AddListener(ToggleChangedSnd);
        toggles[3].onValueChanged.AddListener(ToggleChangedEff);

        if (PlayerPrefs.HasKey("Difficulty"))
        {
            int savedDiff = PlayerPrefs.GetInt("Difficulty");
            currentDifficulty = (Difficulty)savedDiff;// преобразуем в enum
            Debug.Log("Difficulty: " + currentDifficulty);
            for (int i = 0; i < difficultyButtons.Length; i++)
            {
                difficultyButtons[i].gameObject.SetActive(i == savedDiff);
            }
        }
        if (PlayerPrefs.HasKey("toggleStateNot"))
        {
            bool savedStateNot = PlayerPrefs.GetInt("toggleStateNot") == 1;
            toggles[0].isOn = savedStateNot;
        }
        if (PlayerPrefs.HasKey("toggleStateTrn"))
        {
            bool savedStateTrn = PlayerPrefs.GetInt("toggleStateTrn") == 1;
            toggles[1].isOn = savedStateTrn;
        }
        if (PlayerPrefs.HasKey("toggleStateSnd"))
        {
            bool savedStateSnd = PlayerPrefs.GetInt("toggleStateSnd") == 1;
            toggles[2].isOn = savedStateSnd;
            musicSource.volume = savedStateSnd ? 1f : 0f;
        }
        if (PlayerPrefs.HasKey("toggleStateEff"))
        {
            bool savedStateSnd = PlayerPrefs.GetInt("toggleStateEff") == 1;
            toggles[3].isOn = savedStateSnd;
        }
    }

    public void SetEasy()
    {
        currentDifficulty = Difficulty.Easy;
        Debug.Log("Difficulty: Easy");
        PlayerPrefs.SetInt("Difficulty", 0);
    }

    public void SetMiddle()
    {
        currentDifficulty = Difficulty.Middle;
        Debug.Log("Difficulty: Middle");
        PlayerPrefs.SetInt("Difficulty", 1);
    }

    public void SetHard()
    {
        currentDifficulty = Difficulty.Hard;
        Debug.Log("Difficulty: Hard");
        PlayerPrefs.SetInt("Difficulty", 2);
    }

    private void ToggleChangedNot(bool isOn)
    {
        PlayerPrefs.SetInt("toggleStateNot", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void ToggleChangedTrn(bool isOn)
    {
        PlayerPrefs.SetInt("toggleStateTrn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void ToggleChangedSnd(bool isOn)
    {
        PlayerPrefs.SetInt("toggleStateSnd", isOn ? 1 : 0);
        musicSource.volume = isOn ? 1f : 0f;
        PlayerPrefs.Save();
    }
    private void ToggleChangedEff(bool isOn)
    {
        PlayerPrefs.SetInt("toggleStateEff", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void SliderChange(float value)
    {
        float localValue = value * maxSliderAmount;
        sliderText.text = localValue.ToString("0");
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log(value) * 20);
    }
}
