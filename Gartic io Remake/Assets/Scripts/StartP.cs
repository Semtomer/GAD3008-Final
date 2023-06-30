using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class StartP : MonoBehaviourPunCallbacks
{
    AudioSource GameAudioSource;
    public GameObject Sound1, Sound2;
    public AudioSource SlidingSound, ClickingSound;
    public static bool SoundB;
    public InputField NickName;
    public Button StartButton;
    public static string PubNickName;

    void Start()
    {
        if (PlayerPrefs.HasKey("NickName"))
        {
            NickName.text = PlayerPrefs.GetString("NickName");
        }

        SoundStartFunc();
    }

    void SoundStartFunc()
    {
        GameAudioSource = GetComponent<AudioSource>();
        Sound1.SetActive(true);
        Sound2.SetActive(false);
        GameAudioSource.Play();
        SoundB = true;
    }

    private void Update()
    {
        if (NickName.text == null || NickName.text == "" || NickName.text == " "|| NickName.text.Length <= 3)
        {
            StartButton.interactable = false;
        }
        else
        {
            StartButton.interactable = true;
            PubNickName = NickName.text;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void StartFunc()
    {
        PlayerPrefs.SetString("NickName", NickName.text);

        PlayerPrefs.SetInt("Master", 0);
        PlayerPrefs.SetInt("NotMaster", 0);

        if (PlayerPrefs.HasKey("WhichList"))
        {
            if (PlayerPrefs.GetInt("WhichList") >= 5)
            {
                PlayerPrefs.SetInt("WhichList", 1);
            }
            else
            {
                PlayerPrefs.SetInt("WhichList", PlayerPrefs.GetInt("WhichList") + 1);
            }
                //print("deger var");
        }
        else
        {
            //print("deger yok");
            PlayerPrefs.SetInt("WhichList", 1);
        }

        SceneManager.LoadScene("MasterScene");
    }

    public void QuitFunc()
    {
        Application.Quit();
    }

    public void SoundOnFunc()
    {
        Sound1.SetActive(true);
        Sound2.SetActive(false);
        GameAudioSource.Play();
        SoundB = true;
    }

    public void SoundOffFunc()
    {
        Sound1.SetActive(false);
        Sound2.SetActive(true);
        GameAudioSource.Pause();
        SoundB = false;
    }

    public void Sliding()
    {
        SlidingSound.Play();
    }
    public void clicking()
    {
        ClickingSound.Play();
    }
}
