using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : CharacterBehaviour
{
    private const string INTERACTABLE_TAG = "Interactable";

    private readonly int speedParamHash = Animator.StringToHash("Speed");
    private readonly int groundedParamHash = Animator.StringToHash("Grounded");
    private readonly int verticalVelocityParamHash = Animator.StringToHash("VerticalVelocity");
    public ProjectileBehaviour projectilePrefab;
    private float RateOfFire = 1f;
    private float timeSinceLastShot = 0;
    private Vector3 projectileOffset = new Vector3(0.5f, 0, 0);

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        isGrounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down * groundCheckDistance, groundLayers);

        if (isGrounded)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance, Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance, Color.red);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);  
        }

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (timeSinceLastShot > RateOfFire && Input.GetButtonDown("Fire1"))
        {
            animator.Play("Attack");
            projectilePrefab.direction = spriteRenderer.flipX ? -1 : 1;
            Instantiate(projectilePrefab, transform.position + (projectileOffset * projectilePrefab.direction), transform.rotation);
            timeSinceLastShot = 0;
        }

        animator.SetFloat(speedParamHash, Mathf.Abs(horizontalInput));
        animator.SetBool(groundedParamHash, isGrounded);
        animator.SetFloat(verticalVelocityParamHash, body.velocity.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(INTERACTABLE_TAG))
        {
            collision.GetComponent<IPlayerInteractable>().OnPlayerContact(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Gizmos.DrawWireCube(transform.position, new Vector2(2, 1));
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

}
