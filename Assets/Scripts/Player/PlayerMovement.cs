using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    private float coyoteCounter;
    private int jumpCounter;

    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x , body.velocity.y/2);

        body.gravityScale = 7;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (isGrounded()){
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }else
            coyoteCounter -= Time.deltaTime;
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && jumpCounter <= 0) return;
        
        SoundManager.instance.PlaySound(jumpSound);

        if (isGrounded())
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        else
            if (coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
                if (jumpCounter > 0){
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
        coyoteCounter = 0;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    public void LoadData(GameData data){
        // this.transform.position = data.playerPosition;

    }

    public void SaveData(ref GameData data){
        // data.playerPosition = this.transform.position;
    }
}