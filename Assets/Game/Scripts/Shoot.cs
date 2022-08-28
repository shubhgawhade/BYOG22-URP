using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioClip shot;
    
    private Animator anim;
    private bool cd;

    private Camera cam;
    private AudioSource audSource;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        audSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cd)
        {
            muzzleFlash.SetActive(true);
            audSource.PlayOneShot(shot);
            anim.SetBool("Shoot", true);
            cd = true;

            ShootBullet();

            StartCoroutine(A());
        }
    }

    private void ShootBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            //Instantiate bullet holes if not enemy
            // if enemy instantiate bool
            
            //Instantiate(muzzleFlash, hit.point, Quaternion.Euler(hit.normal));
            
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<ThirdPersonCharacter>().ded = true;
            }
            else
            {
            }
        }
    }

    IEnumerator A()
    {
        yield return new WaitForSeconds(0.8f);
            
        anim.SetBool("Shoot", false);
        cd = false;
    }
}
