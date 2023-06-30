using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HUDController2 : MonoBehaviourPunCallbacks
{
    public static int firstPlayerScore = 0, secondPlayerScore = 0, thirdPlayerScore = 0, fourthPlayerScore = 0, PuanStatic = 0;
    public Button[] secondScoreButtons = new Button[8];
    public Button[] thirdScoreButtons = new Button[8];
    public Button[] fourthScoreButtons = new Button[8];

    public static List<string> drawableObjectList2 = new List<string>(); 

    public List<Image> canvasImages1 = new List<Image>();
    public List<Image> canvasImages2 = new List<Image>();
    public List<Image> canvasImages3 = new List<Image>();
    public List<Image> canvasImages4 = new List<Image>();

    public static bool buradangeliyor = false;

    public static List<string> GelenStatic = new();
    public static List<string> GelenStatic2 = new();

    public PhotonView PhotonView;

    public AudioSource GameAudioSource;

    public AudioSource SlidingSound, ClickingSound;

    private void Awake()
    {
        GameAudioSource = GetComponent<AudioSource>();

        if (StartP.SoundB == true)
        {
            GameAudioSource.Play();
        }
        else
        {
            GameAudioSource.Pause();
        }


        PhotonView = GetComponent<PhotonView>();

        firstPlayerScore = HUDController.firstPlayerScore2;
        secondPlayerScore = HUDController.secondPlayerScore2;
        thirdPlayerScore = HUDController.thirdPlayerScore2;
        fourthPlayerScore = HUDController.fourthPlayerScore2;
    }

    void Start()
    {
        for (int i = 0; i < 144; i++)
        {
            canvasImages1[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(HUDController.canvasSpritesStatic[i].name);
        }

        try
        {
            if (GelenStatic[0] == "MasterDegil" && PhotonNetwork.IsMasterClient == false)
            {
                GelenStatic.RemoveAt(0);

                for (int a = 0; a < 144; a++)
                {
                    canvasImages2[a].GetComponent<Image>().sprite = Resources.Load<Sprite>(GelenStatic[a]);
                }
            }

            if (GelenStatic2[0] == "Master" && PhotonNetwork.IsMasterClient == true)
            {
                GelenStatic2.RemoveAt(0);

                for (int a = 0; a < 144; a++)
                {
                    canvasImages2[a].GetComponent<Image>().sprite = Resources.Load<Sprite>(GelenStatic2[a]);
                }
            }
        }
        catch (System.Exception)
        {
            if (GelenStatic2[0] == "Master")
            {
                GelenStatic2.RemoveAt(0);

                for (int a = 0; a < 144; a++)
                {
                    canvasImages2[a].GetComponent<Image>().sprite = Resources.Load<Sprite>(GelenStatic2[a]);
                }
            }
        }

        buradangeliyor = true;
    }

    public void AddScore(int ScoreNumber)
    {
        foreach (Button item in secondScoreButtons)
        {
            item.interactable = false;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PlayerPrefs.SetInt("NotMaster", PlayerPrefs.GetInt("NotMaster") + ScoreNumber);

            PuanStatic = PlayerPrefs.GetInt("NotMaster");
        }
        else if (PhotonNetwork.IsMasterClient == false)
        {
            PlayerPrefs.SetInt("Master", PlayerPrefs.GetInt("Master") + ScoreNumber);

            PuanStatic = PlayerPrefs.GetInt("Master");
        }
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


