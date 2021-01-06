using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    public static MapSetup i;
    void Awake()
    {
        Debug.Log("Spawn guns");
        //if (i == null)
        //{
        //    i = this;           
        //}
        //else Destroy(this);
    }
    void Start()
    {
        Debug.Log("Spawn guns");
        SpawnGuns();
    }

    void SpawnGuns()
    {
        Debug.Log("Spawn guns");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Guns", "AUG"), Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Guns", "Pistol"), new Vector3 (3,0,0), Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Guns", "AUG"), new Vector3 (4,4,0), Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Guns", "Pistol"), new Vector3 (7,4,0), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
