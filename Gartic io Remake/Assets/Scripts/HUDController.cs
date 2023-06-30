using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Photon.Pun;

public class HUDController : MonoBehaviourPunCallbacks
{
    public Sprite[] Palette = new Sprite[19];

    public Button[] canvasButtons = new Button[144];
    public Sprite spriteType;

    public float timeValue;
    public Text timeText;
    bool AnimText = true, GamePlayable, IsNotFinish = true, OneShoot = true, OneShoot2 = true;

    public static int lapCount = 4;
    Text lapCountText;

    public Text firstPlayer, secondPlayer, thirdPlayer, fourthPlayer;
    public Text score1, score2, score3, score4;
    public Text scoreN;

    List<Sprite> canvasSprites = new List<Sprite>();
    public static List<Sprite> canvasSpritesStatic = new List<Sprite>();

    public List<string> GelenMaster = new();
    public List<string> GelenMasterDegil = new();

    public static int firstPlayerScore2 = 0, secondPlayerScore2 = 0, thirdPlayerScore2 = 0, fourthPlayerScore2 = 0;

    public PhotonView PhotonView;

    int[] scoreList = new int[4];

    public GameObject Panell;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();

        Panell = GameObject.Find("Panel");
        score2 = GameObject.Find("Score2").GetComponent<Text>();
        scoreN = GameObject.Find("ScoreN").GetComponent<Text>();

        timeText = GameObject.Find("Time").GetComponent<Text>();
        firstPlayer = GameObject.Find("PlayerName1").GetComponent<Text>();
        secondPlayer = GameObject.Find("PlayerName2").GetComponent<Text>();
    }

    void Start()
    {
        for (int i = 0; i < 144; i++)
        {
            string buttonName = "Button";
            canvasButtons[i] = GameObject.Find(buttonName + (i+1)).GetComponent<Button>();
        }

        firstPlayer.text = PhotonNetwork.PlayerList[0].NickName;
        secondPlayer.text = PhotonNetwork.PlayerList[1].NickName;

        lapCountText = GameObject.Find("LapCount").GetComponent<Text>();
        lapCountText.text = lapCount.ToString();
        GelenMaster.Insert(0, "MasterDegil");
        GelenMasterDegil.Insert(0, "Master");
        ScoreTab();
        print("Start Calısıyor");
    }

    void Update()
    {
        if (AnimText)
        {
            AnimText = false;
            StartCoroutine(waitText());
        }
        else if(GamePlayable)
        {
            if (timeValue <= 0)
            {
                timeValue = 0;
                Panell.SetActive(true);

                foreach (Button item in canvasButtons)
                {
                    canvasSprites.Add(item.GetComponent<Image>().sprite);
                }

                canvasSpritesStatic = canvasSprites;

                if (OneShoot)
                {
                    OneShoot = false;
                    CanvasSpritesNameIsCooll();
                }

                StartCoroutine(Waittttt());
            }
            else if (timeValue > 0)
            {
                TimeAction();
            }
        }

        /*if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("VoteSection");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("StartP");
        }*/
    }

    void TimeAction()
    {
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeValue -= Time.deltaTime;
    }

    IEnumerator waitText()
    {
        //Burada Animasyon çalışıyor.
        yield return new WaitForSeconds(6f);
        GamePlayable = true;
    }

    public void SetSpriteType (Button button) 
    {
        foreach (Sprite item in Palette)
        {
            if (button.name == item.name)
            {
                spriteType = item;
            }
        }
    }

    public void DrawCanvasButton(Button button)
    {
        button.GetComponent<Image>().sprite = spriteType;
    }

    void CanvasSpritesNameIsCooll()
    {
       
        if (PhotonNetwork.IsMasterClient)
        {
            //print("Master kısmı");
            for (int i = 0; i < 144; i++)
            {
                PhotonView.RPC("SetStringRPC", RpcTarget.All, canvasSpritesStatic[i].name);
            }
            StartCoroutine(DataTransfer());
           
        }
        else  if (PhotonNetwork.IsMasterClient == false)
        {
            //print("Master Degil kısmı");
            StartCoroutine(DataTransfer());
            for (int i = 0; i < 144; i++)
            {
                PhotonView.RPC("SetStringRPCc", RpcTarget.All, canvasSpritesStatic[i].name);
            }
        }
    }

    [PunRPC]
    public void SetStringRPC(string gelennMaster)
    {
        GelenMaster.Add(gelennMaster);
        HUDController2.GelenStatic = GelenMaster;
        //print("gelennMaster " + HUDController2.GelenStatic[0]);
    }

    [PunRPC]
    public void SetStringRPCc(string gelennMasterDegil)
    {

        GelenMasterDegil.Add(gelennMasterDegil);
        HUDController2.GelenStatic2 = GelenMasterDegil;
        //print("gelennMasterDegil " + HUDController2.GelenStatic2[0]);
    }

    IEnumerator Waittttt()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("VoteSection");
    }

    IEnumerator DataTransfer()
    {
        yield return new WaitForSeconds(3);
        print("Data gönderiliyor");
    }

    void ScoreTab()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView.RPC("ScoreTabEvery", RpcTarget.All,  HUDController2.PuanStatic);
            StartCoroutine(DataTransfer());
        }
        else if (PhotonNetwork.IsMasterClient == false)
        {
            StartCoroutine(DataTransfer());
            PhotonView.RPC("ScoreTabEveryM", RpcTarget.All,  HUDController2.PuanStatic);
        }
    }

    [PunRPC]
    public void ScoreTabEveryM(int _PuanStaticC)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            print("Burası calısıyor");
            PlayerPrefs.SetInt("Master", _PuanStaticC);
            print("Master" + PlayerPrefs.GetInt("Master"));
            //score1.text = PlayerPrefs.GetInt("Master").ToString();
            scoreN.text = PlayerPrefs.GetInt("Master").ToString();

        }

        if (PhotonNetwork.IsMasterClient == false)
        {
            PlayerPrefs.SetInt("Master", _PuanStaticC);
            scoreN.text = PlayerPrefs.GetInt("Master").ToString();
            print("Master" + PlayerPrefs.GetInt("Master"));
        }
    }

    [PunRPC]
    public void ScoreTabEvery(int _PuanStatic)
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            PlayerPrefs.SetInt("NotMaster", _PuanStatic);
            print("NotMaster" + PlayerPrefs.GetInt("NotMaster"));
            score2.text = PlayerPrefs.GetInt("NotMaster").ToString();
            
        }
        else if (PhotonNetwork.IsMasterClient && OneShoot2)
        {
            OneShoot2 = false;
            print("Burası 2 kez calısıyor");
            PlayerPrefs.SetInt("NotMaster", _PuanStatic);
            score2.text = PlayerPrefs.GetInt("NotMaster").ToString();
            print("NotMaster" + PlayerPrefs.GetInt("NotMaster"));
        }
    }
}