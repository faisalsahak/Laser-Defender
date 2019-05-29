﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float padding = 1f;
    [SerializeField] float health = 2000f;

    [Header("Projectile")]
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float projectileSpeed = 20f;
	[SerializeField] float projectileFiringPeriod = 0.1f;

	Coroutine firingCoroutine;

	float xMin;
	float xMax;
	float yMin;
	float yMax;

    // Start is called before the first frame update
    void Start(){
    	Camera gameCamera = Camera.main;
    	xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
    	xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;

    	yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
    	yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
        
    }

    // Update is called once per frame
    void Update(){
    	Move();
    	Fire();
    }


    private void Fire(){
    	if(Input.GetButtonDown("Fire1")){
    		firingCoroutine = StartCoroutine(FireContinuously());
    	}
    	if(Input.GetButtonUp("Fire1")){
    		StopCoroutine(firingCoroutine);
    	}
    }

    IEnumerator FireContinuously(){
    	while(true){
	  		GameObject laser=Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
	  		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

	  		yield return new WaitForSeconds(projectileFiringPeriod);
	  	}
    }

    private void Move(){
    	var deltaX = Input.GetAxis("Horizontal") *Time.deltaTime * moveSpeed;
    	var deltaY= Input.GetAxis("Vertical") *Time.deltaTime * moveSpeed;

    	var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    	var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
    	transform.position = new Vector2(newXpos, newYpos);
    	// transform.position = new Vector2(transform.position.y, deltaY);
    }

    private void OnTriggerEnter2D(Collider2D other){
        Damagedealer damageDealer = other.gameObject.GetComponent<Damagedealer>();
        if(!damageDealer)return;
        DestroyPlayer(damageDealer);
    }

    private void DestroyPlayer(Damagedealer damageDealer){
        health-= damageDealer.GetDamage();
        damageDealer.Hit();
      if(health <=0)
        Destroy(gameObject);
    }

}









