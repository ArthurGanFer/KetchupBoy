using UnityEngine;

public class EnemyController : CharacterBehaviour
{
    [SerializeField] private float platformCheckDistance = 0.5f;
    [SerializeField] private ParticleSystem splashParticles;
    private Vector3 leftTestStartOffset;
    private Vector3 leftTestEndOffset;
    private Vector3 rightTestStartOffset;
    private Vector3 rightTestEndOffset;

    void Start()
    {
        leftTestStartOffset = Vector3.left * platformCheckDistance;
        leftTestEndOffset = leftTestStartOffset + Vector3.down * groundCheckDistance;
        rightTestStartOffset = Vector3.right * platformCheckDistance;
        rightTestEndOffset = rightTestStartOffset + Vector3.down * groundCheckDistance;
    }

    void Update()
    {
        isGrounded = Physics2D.Linecast(
            transform.position + leftTestStartOffset,
            transform.position + leftTestEndOffset,
            groundLayers);

        isGrounded = isGrounded && Physics2D.Linecast(
            transform.position + rightTestStartOffset,
            transform.position + rightTestEndOffset,
            groundLayers);

        if (!isGrounded)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;

            Debug.DrawLine(
            transform.position + leftTestStartOffset,
            transform.position + leftTestEndOffset,
            Color.red);

            Debug.DrawLine(
            transform.position + rightTestStartOffset,
            transform.position + rightTestEndOffset,
            Color.red);
        }
        else
        {
            Debug.DrawLine(
            transform.position + leftTestStartOffset,
            transform.position + leftTestEndOffset,
            Color.green);

            Debug.DrawLine(
            transform.position + rightTestStartOffset,
            transform.position + rightTestEndOffset,
            Color.green);
        }

        if (spriteRenderer.flipX)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
        }
        else
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    //direction:1 for right -1 for left
    public void Die(Vector2 contactPoint, int direction)
    {
        Instantiate(splashParticles, contactPoint, Quaternion.Euler(new Vector3(0, 90 * direction, 0)));
        Destroy(gameObject);
    }

}
