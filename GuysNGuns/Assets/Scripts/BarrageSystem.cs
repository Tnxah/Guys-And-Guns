using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarrageSystem : MonoBehaviour
{
    public int bullets;
    public int numOfBullets;

    public Image[] bulletses;
    public Sprite fullBullet;
    public Sprite emptyBullet;

    private void Update()
    {
        if(bullets > numOfBullets)
        {
            bullets = numOfBullets;
        }
        UpdateBarrage();
    }

    private void Start()
    {
        GameObject barr = GameObject.Find("Barrage");
        for (int i = 0; i < barr.transform.childCount; i++)
        {
            bulletses[i] = barr.transform.GetChild(i).GetComponent<Image>();
        }
        UpdateBarrage();       
    }

    private void UpdateBarrage()
    {
        for (int i = 0; i < bulletses.Length; i++)
        {
            if(i < bullets)
            {
                bulletses[i].sprite = fullBullet;
            }
            else
            {
                bulletses[i].sprite = emptyBullet;
            }

            if (i < numOfBullets)
            {
                bulletses[i].enabled = true;
            }
            else
            {
                bulletses[i].enabled = false;
            }
        }
    }
}
