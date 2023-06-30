using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HUDController3 : MonoBehaviour
{
    public Text firstPlayer, secondPlayer, score1, score2;
   
    void Start()
    {
        firstPlayer.text = PhotonNetwork.PlayerList[0].NickName;
        secondPlayer.text = PhotonNetwork.PlayerList[1].NickName;

        score1.text = PlayerPrefs.GetInt("Master").ToString();
        score2.text = PlayerPrefs.GetInt("NotMaster").ToString();
    }
}
