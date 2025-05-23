using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D coll;

    private void Awake() {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile(){
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;

    }

    private void Update() {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
            gameObject.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        if (collision.tag != "Enemy")
        {
            hit = true;
            coll.enabled = false;
            if (anim != null)
                anim.SetTrigger("explode"); //when the projectile is fireball, explode
            else
                gameObject.SetActive(false); // when the object is arrow, deactivate arrow
        }
    }

    private void Deactivate(){
        gameObject.SetActive(false); // when the object is arrow, deactivate arrow
    }
}
