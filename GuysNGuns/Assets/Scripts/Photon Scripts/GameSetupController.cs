﻿using Photon.Pun;
using System;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player"),Vector3.zero,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
