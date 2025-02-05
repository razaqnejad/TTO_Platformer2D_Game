using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine.UI;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header ("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [Header ("SFX")]
    [SerializeField] private AudioClip impactSound;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private void OnEnable() {
        Stop();
    }

    private void Update() {
        Vector3 direction = destination.normalized;
        
        if (attacking){
            transform.Translate(direction * Time.deltaTime * speed);
        }
        else{
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void Stop(){
        destination = transform.position;
        attacking = false;
    }

    private void CheckForPlayer(){
        CalculateDirections();
        for (int i = 0; i < directions.Length ; i++){
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && !attacking){
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }           
        }
    }

    private void CalculateDirections(){
        directions[0] = transform.right * range;    //right direction
        directions[1] = -transform.right * range;   //left direction
        directions[2] = transform.up * range;       //up direction
        directions[3] = -transform.up * range;      //down direction
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
