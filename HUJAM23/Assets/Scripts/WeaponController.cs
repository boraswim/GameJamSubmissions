using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Animator swordAnimator;
    [SerializeField] Animator pistolAnimator;
    public GameObject playerSword;
    public GameObject playerGun;
    public GameObject playerBullet;
    Collider2D playerSwordCollider;
    public AudioSource swordStab, swordSwing, pistolFire;
    bool weaponSelection = false;
    bool changeable = true;
    [SerializeField] float bulletSpeed = 5.0f;

    private void Awake()
    {
        playerSwordCollider = playerSword.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && changeable)
        { 
            weaponSelection = false;
            playerSword.SetActive(!weaponSelection);
            playerGun.SetActive(weaponSelection);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && changeable)
        {
            weaponSelection = true;
            playerSword.SetActive(!weaponSelection);
            playerGun.SetActive(weaponSelection);
            
        }
    }

    private void OnEnable()
    {
        StartCoroutine("attackCoroutine");
    }

    private IEnumerator attackCoroutine()
    {
        // playerSword.SetActive(!weaponSelection);
        // playerGun.SetActive(weaponSelection);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        changeable = false;
        Debug.Log("atak");
        if(!weaponSelection)
        {

        playerSwordCollider.enabled = true;
        int random = Random.Range(0,2);
        if(random == 0)
        {
            swordStab.Play();
            swordAnimator.Play("stabSword");
            yield return new WaitForSeconds(5f/6f);
        }
        else
        {
            swordAnimator.Play("swingSword");
            swordSwing.Play();
            yield return new WaitForSeconds(2.5f/6f);
        }
        playerSwordCollider.enabled = false;
        }
        else
        {
            int random = Random.Range(0,2);
            if(random==0)
                pistolAnimator.Play("firePistol");
            else
                pistolAnimator.Play("specialFirePistol"); 
            pistolFire.Play();       
            var bullet = Instantiate(playerBullet, gameObject.transform);
            playerGun.transform.GetChild(0).gameObject.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerController.facingRight * bulletSpeed,0);
            yield return new WaitForSeconds(3.5f/6f);
            playerGun.transform.GetChild(0).gameObject.SetActive(false);
        }
        changeable = true;
        StartCoroutine("attackCoroutine");
    }
}
