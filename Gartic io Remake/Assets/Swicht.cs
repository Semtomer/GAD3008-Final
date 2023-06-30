using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swicht : MonoBehaviour
{
    public GameObject Hudd, Finn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HUDController.lapCount == -1)
        {
            Finn.SetActive(true);
            Hudd.SetActive(false);
        }

    }
}
