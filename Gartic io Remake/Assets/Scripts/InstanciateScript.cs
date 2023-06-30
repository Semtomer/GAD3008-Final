using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class InstanciateScript : MonoBehaviourPunCallbacks
{
    public string[] drawableObjects = new string[27];

    public static List<string> drawableObjectList = new List<string>();

    bool equal;

    public PhotonView PhotonView;

    public AudioSource GameAudioSource;

    public AudioSource SlidingSound, ClickingSound;

    void Start()
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
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        drawableObjectList = HUDController2.drawableObjectList2;

    }

    // Update is called once per frame
    void Update()
    {
        
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
