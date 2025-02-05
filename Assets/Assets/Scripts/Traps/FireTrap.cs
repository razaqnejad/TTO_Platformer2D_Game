using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    
    [SerializeField] private float damage;
    [Header ("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header ("SFX")]
    [SerializeField] private AudioClip fireSound;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //when trap is triggered
    private bool active; //when trap is active and can hurt

    private Health playerHealth;

    private void Awake() {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        
    }

    private void Update() {
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
                StartCoroutine(ActivateFireTrap());
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player")
            playerHealth = null;
    }

    private IEnumerator ActivateFireTrap(){
        triggered = true;
        spriteRend.color = new Color(0.7f, 0.5f, 0.5f, 1);;
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
