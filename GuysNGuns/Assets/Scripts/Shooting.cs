using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviourPun
{

    protected Joystick AimJoystick;
    protected BarrageSystem barrage;

    GameObject pickable;

    public Button pickUp;
    public Button swap;
    public int maxBarrageSize;
    public int bullets;

    Transform firePoint;
    public GameObject bulletPrefab;

    public float shootSpeed = 0.1f;

    public float bulletForce = 20f;

    float lastShotTime;

    GameObject activeGun;
    public int activeGunNumber = 0;
    GameObject firstGun;
    GameObject secondGun;


    private void Start()
    {
        pickUp = GameObject.Find("PickUp").GetComponent<Button>();
        pickUp.onClick.AddListener(PickUpGun);
        swap = GameObject.Find("Swap").GetComponent<Button>();
        swap.onClick.AddListener(Swap);
        lastShotTime = 0;
        AimJoystick = GameObject.Find("Aim Joystick").GetComponent<Joystick>();
        barrage = transform.parent.GetComponent<BarrageSystem>();
    }

    [PunRPC]
    public void PickUpGun()
    {
        if (pickable != null && transform.childCount < 2)
        {


            switch (firstGun == null)
            {
                case true:
                    firstGun = pickable;
                    activeGunNumber = 1;
                    activeGun = firstGun;
                    if (secondGun != null)
                    {
                        secondGun.SetActive(false);
                    }
                    break;
                case false:
                    secondGun = pickable;
                    activeGunNumber = 2;
                    activeGun = secondGun;
                    if (firstGun != null)
                    {
                        firstGun.SetActive(false);
                    }
                    break;
            }
            SetParams();
            pickable = null;
            activeGun.transform.parent = transform;
            activeGun.transform.localPosition = new Vector2(0.275f, 0.041f);
            activeGun.transform.rotation = transform.rotation;
            //firePoint = GameObject.Find("Thrower").transform;
            firePoint = activeGun.transform.GetChild(0);
        }
        else if (pickable != null)
        {
            switch (activeGunNumber)
            {
                case 1:
                    firstGun.transform.parent = null;
                    firstGun = pickable;
                    activeGun = firstGun;
                    break;
                case 2:
                    secondGun.transform.parent = null;
                    secondGun = pickable;
                    activeGun = secondGun;
                    break;
            }
            SetParams();
            pickable = null;
            activeGun.transform.parent = transform;
            activeGun.transform.localPosition = new Vector2(0.275f, 0.041f);
            activeGun.transform.rotation = transform.rotation;
            //firePoint = GameObject.Find("Thrower").transform;
            firePoint = activeGun.transform.GetChild(0);
        }
    }

    void SetParams()
    {
        barrage.bullets = activeGun.GetComponent<Gun>().bullets;
        barrage.numOfBullets = activeGun.GetComponent<Gun>().maxNumOfBullets;
        shootSpeed = activeGun.GetComponent<Gun>().shootSpeed;
        //shootSpeed = activeGun.GetComponent<Gun>().Damage;
    }

    public void Swap()
    {
        Debug.Log("Swap");
        if (transform.childCount == 2)
        {
            switch (activeGunNumber)
            {
                case 1:
                    activeGun = secondGun;
                    activeGunNumber = 2;
                    secondGun.SetActive(true);
                    firstGun.SetActive(false);
                    break;
                case 2:
                    activeGun = firstGun;
                    activeGunNumber = 1;
                    secondGun.SetActive(false);
                    firstGun.SetActive(true);
                    break;
                default:

                    break;
            }
            SetParams();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpGun();
        }

        if (AimJoystick.canFire)
        {
            
            if (Time.time > (lastShotTime + shootSpeed) && firePoint != null && activeGun.GetComponent<Gun>().bullets > 0)
            {
                
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }
    private void FixedUpdate()
    {
        swap.interactable = transform.childCount == 2 ? true : false;
    }

    public void Shoot()
    {
        
        photonView.RPC("RpcShoot", RpcTarget.All, firePoint.position, firePoint.rotation, activeGun.GetComponent<Gun>().Damage, transform.parent.GetComponent<PlayerMovement>().name, firePoint.right);
        activeGun.GetComponent<Gun>().bullets--;
        barrage.bullets--;
    }
    [PunRPC]
    private void RpcShoot(Vector3 position, Quaternion rotation, int damage, string name, Vector3 direction)
    {
        Debug.Log("Shoot");
        GameObject bullet = Instantiate((GameObject)Resources.Load(Path.Combine("PhotonPrefabs", "Bullet")), position, rotation);
        //GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), firePoint.position, firePoint.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        bullet.GetComponent<Bullet>().owner = name;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.AddForce(direction * 30f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {

            pickable = collision.gameObject;
            pickUp.interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            pickable = null;
            pickUp.interactable = false;
        }
    }

}
