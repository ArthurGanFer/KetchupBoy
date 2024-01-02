using UnityEngine;

[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer)),
 RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] protected LayerMask groundLayers;
    [SerializeField] protected float groundCheckDistance = 0.6f;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 15f;

    protected bool isGrounded = false;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D body;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }
}
