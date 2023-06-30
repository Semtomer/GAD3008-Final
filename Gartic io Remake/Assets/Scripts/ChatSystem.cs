using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviourPunCallbacks
{
    public PhotonView PhotonView;
    public GameObject ChatBar;
    public GameObject ChatFeed;
    public InputField InputField;
    public GameObject Panell;
    public Animator animator;

    bool isProper = true, IsFirstSelect = true;
    public Text ObjeAdı;
    public Text AnimationObjeAdı;

    private void Awake()
    {
        Panell.SetActive(true);
        PhotonView = GetComponent<PhotonView>();
        InputField = GameObject.Find("InputField").GetComponent<InputField>();
        ChatBar = GameObject.Find("ChatBar");
        ObjeAdı = GameObject.Find("Objectt").GetComponent<Text>();
        AnimationObjeAdı = GameObject.Find("AnimationObject").GetComponent<Text>();
    }

    void Start()
    {
        if (HUDController2.buradangeliyor == false)
        {
            PlayerPrefs.SetInt("WhichTur", 0);
        }
        PlayerPrefs.SetInt("WhichTur", PlayerPrefs.GetInt("WhichTur") + 1);
        //print("oyun tur sayısı attırıldı");

        if (PhotonNetwork.IsMasterClient == true && HUDController2.buradangeliyor == false)
        {
            //print("Sen mastersın");
            WhichListMaster();
            PhotonView.RPC("SendInputMessage", RpcTarget.All, InputField.text);
            InputField.text = "";
        }

        StartCoroutine(sendStartTime());
    }

    void Update()
    {

        if (InputField)
        {
            if (InputField.text != "" && InputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Tab) && isProper)
            {
                isProper = false;
                PhotonView.RPC("SendInputMessage", RpcTarget.All, StartP.PubNickName + ": " + InputField.text);
                InputField.text = "";
                StartCoroutine(sendTime());
            }
        }
    }

    [PunRPC]
    public void SendInputMessage(string message)
    {
        GameObject lastMessage = Instantiate(ChatFeed, Vector3.zero, Quaternion.identity);
        lastMessage.transform.SetParent(ChatBar.transform, false);
        lastMessage.GetComponent<Text>().text = message;
        lastMessage.GetComponent<Text>().color = Color.black;

        if (lastMessage.GetComponent<Text>().text == "elmacı" || lastMessage.GetComponent<Text>().text == "1" ||
            lastMessage.GetComponent<Text>().text == "2" || lastMessage.GetComponent<Text>().text == "3" ||
            lastMessage.GetComponent<Text>().text == "4" || lastMessage.GetComponent<Text>().text == "5")
        {
            IsFirstSelect = false;
            //print("liste sayısı belirlendi");
            PlayerPrefs.SetInt("WhichList", int.Parse(lastMessage.GetComponent<Text>().text));
            Destroy(lastMessage);
        }
        else if (IsFirstSelect == true)
        {
            print("Normal Mesaj");
        }
       
        Destroy(lastMessage, 5f);
    }

    public void WhichListMaster()
    {
        //print("WhichListMaster " + PlayerPrefs.GetInt("WhichList"));

        if (PlayerPrefs.GetInt("WhichList") == 1)
        {
            //print("liste 1 olacak");
            InputField.text = "1";
        }
        else if (PlayerPrefs.GetInt("WhichList") == 2)
        {
            //print("liste 2 olacak");
            InputField.text = "2";
        }
        else if (PlayerPrefs.GetInt("WhichList") == 3)
        {
           // print("liste 3 olacak");
            InputField.text = "3";
        }
        else if (PlayerPrefs.GetInt("WhichList") == 4)
        {
            //print("liste 4 olacak");
            InputField.text = "4";
        }
        else if (PlayerPrefs.GetInt("WhichList") == 5)
        {
            //print("liste 5 olacak");
            InputField.text = "5";
        }
    }

    public void WhichList()
    {
        //print("WhichList "+PlayerPrefs.GetInt("WhichList"));

        if (PlayerPrefs.GetInt("WhichList") == 1)
        {
           // print("liste 1 olacak");
            InputField.text = "1";
            WhichTurList1();
        }
        else if (PlayerPrefs.GetInt("WhichList") == 2)
        {
            //print("liste 2 olacak");
            InputField.text = "2";
            WhichTurList2();
        }
        else if (PlayerPrefs.GetInt("WhichList") == 3)
        {
            //print("liste 3 olacak");
            InputField.text = "3";
            WhichTurList3();
        }
        else if (PlayerPrefs.GetInt("WhichList") == 4)
        {
            //print("liste 4 olacak");
            InputField.text = "4";
            WhichTurList4();
        }
        else if (PlayerPrefs.GetInt("WhichList") == 5)
        {
            //print("liste 5 olacak");
            InputField.text = "5";
            WhichTurList5();
        }
    }

    public void WhichTurList1()
    {
       
        if (PlayerPrefs.GetInt("WhichTur") == 1)
        {
            ObjeAdı.text = "Tetris";
            AnimationObjeAdı.text = "Tetris";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 2)
        {
            ObjeAdı.text = "Arrow";
            AnimationObjeAdı.text = "Arrow";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 3)
        {
            ObjeAdı.text = "Bow";
            AnimationObjeAdı.text = "Bow";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 4)
        {
            ObjeAdı.text = "Bread";
            AnimationObjeAdı.text = "Bread";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 5)
        {
            ObjeAdı.text = "Bamboo";
            AnimationObjeAdı.text = "Bamboo";
        }

        //print("Nesne " +PlayerPrefs.GetInt("WhichTur"));
        
        InputField.text = "";
    }

    public void WhichTurList2()
    {

        if (PlayerPrefs.GetInt("WhichTur") == 1)
        {
            ObjeAdı.text = "Banana";
            AnimationObjeAdı.text = "Banana";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 2)
        {
            ObjeAdı.text = "Border";
            AnimationObjeAdı.text = "Border";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 3)
        {
            ObjeAdı.text = "Bottle";
            AnimationObjeAdı.text = "Bottle";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 4)
        {
            ObjeAdı.text = "Tree";
            AnimationObjeAdı.text = "Tree";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 5)
        {
            ObjeAdı.text = "House";
            AnimationObjeAdı.text = "House";
        }

        //print("Nesne " + PlayerPrefs.GetInt("WhichTur"));

        InputField.text = "";
    }

    public void WhichTurList3()
    {

        if (PlayerPrefs.GetInt("WhichTur") == 1)
        {
            ObjeAdı.text = "Bridge";
            AnimationObjeAdı.text = "Bridge";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 2)
        {
            ObjeAdı.text = "Bubble";
            AnimationObjeAdı.text = "Bubble";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 3)
        {
            ObjeAdı.text = "Cactus";
            AnimationObjeAdı.text = "Cactus";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 4)
        {
            ObjeAdı.text = "Camera";
            AnimationObjeAdı.text = "Camera";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 5)
        {
            ObjeAdı.text = "Candle";
            AnimationObjeAdı.text = "Candle";
        }

        //print("Nesne " + PlayerPrefs.GetInt("WhichTur"));

        InputField.text = "";
    }

    public void WhichTurList4()
    {
        if (PlayerPrefs.GetInt("WhichTur") == 1)
        {
            ObjeAdı.text = "Cannon";
            AnimationObjeAdı.text = "Cannon";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 2)
        {
            ObjeAdı.text = "Carpet";
            AnimationObjeAdı.text = "Carpet";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 3)
        {
            ObjeAdı.text = "Carrot";
            AnimationObjeAdı.text = "Carrot";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 4)
        {
            ObjeAdı.text = "Cheese";
            AnimationObjeAdı.text = "Cheese";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 5)
        {
            ObjeAdı.text = "Coffin";
            AnimationObjeAdı.text = "Coffin";
        }

        //print("Nesne " + PlayerPrefs.GetInt("WhichTur"));

        InputField.text = "";
    }

    public void WhichTurList5()
    {

        if (PlayerPrefs.GetInt("WhichTur") == 1)
        {
            ObjeAdı.text = "Dagger";
            AnimationObjeAdı.text = "Dagger";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 2)
        {
            ObjeAdı.text = "Car";
            AnimationObjeAdı.text = "Car";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 3)
        {
            ObjeAdı.text = "Fridge";
            AnimationObjeAdı.text = "Fridge";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 4)
        {
            ObjeAdı.text = "Laptop";
            AnimationObjeAdı.text = "Laptop";
        }
        else if (PlayerPrefs.GetInt("WhichTur") == 5)
        {
            ObjeAdı.text = "Magnet";
            AnimationObjeAdı.text = "Magnet";
        }

        //print("Nesne " + PlayerPrefs.GetInt("WhichTur"));

        InputField.text = "";
    }

    IEnumerator sendTime()
    {
        yield return new WaitForSeconds(0.1f);
        isProper = true;
    }

    IEnumerator sendStartTime()
    {
        yield return new WaitForSeconds(3);
        WhichList();
        Panell.SetActive(false);
        animator.SetBool("Start", true);
    }
}
