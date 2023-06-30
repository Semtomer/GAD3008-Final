using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class Master : MonoBehaviourPunCallbacks
{
    public PhotonView PhotonView;

    AudioSource GameAudioSource;
    string RoomName;
    public Text Dger;
    int playerCount;
    bool JoinRoomPlayer = false, OyunBekliyor = true;
    public Image Background;

    public List<Sprite> LodingGame = new();

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

        PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
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
        
        if (PlayerPrefs.HasKey("LodingGame"))
        {
            if (PlayerPrefs.GetInt("LodingGame") >= LodingGame.Count)
            {
                PlayerPrefs.SetInt("LodingGame", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("LodingGame", 0);
        }

        Background.GetComponent<Image>().sprite = LodingGame[PlayerPrefs.GetInt("LodingGame")];
        PlayerPrefs.SetInt("LodingGame", PlayerPrefs.GetInt("LodingGame") + 1);

        PhotonNetwork.NickName = StartP.PubNickName;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        JoinRoomPlayer = true;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        RoomName = "Room #" + Random.Range(0, 9999);
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true}, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        StartCoroutine(WaitAndPrint());
    }

    private void Update()
    {
        try
        {
            if (JoinRoomPlayer == true)
            {
                playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            }
        }
        catch (System.Exception)
        {

        }
       
        if (playerCount == 2 || Input.GetKeyDown(KeyCode.P))
        {
            OyunBekliyor = false;
        }
    }

    IEnumerator WaitAndPrint()
    {
        while (true)
        {
            if (OyunBekliyor == true)
            {
                float YieldTime = 0.3f;
                yield return new WaitForSeconds(YieldTime);
                Dger.text = "     We Are Waiting Other Players ";
                yield return new WaitForSeconds(YieldTime);
                Dger.text = "     We Are Waiting Other Players .";
                yield return new WaitForSeconds(YieldTime);
                Dger.text = "     We Are Waiting Other Players ..";
                yield return new WaitForSeconds(YieldTime);
                Dger.text = "     We Are Waiting Other Players ...";
            }
            else
            {
                yield return new WaitForSeconds(1);
                Dger.text = "                 The Game Begins ";

                yield return new WaitForSeconds(1);
                Dger.text = "                 The Game Begins .";

                yield return new WaitForSeconds(1);
                Dger.text = "                 The Game Begins ..";

                yield return new WaitForSeconds(0.1f);
                Dger.text = "                 The Game Begins ...";

                yield return new WaitForSeconds(1);
               // PhotonView.RPC("LoadScene", RpcTarget.All, sceneName);
                SceneManager.LoadScene("GameSection");
                break;
            }
        }
    }

    //[PunRPC]
    //void LoadScene(string sceneName)
    //{
    //    SceneManager.LoadScene(sceneName);
    //}
}
