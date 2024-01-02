using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    private float speed = 6f;
    public int direction;
    [SerializeField] private ParticleSystem splashParticles;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = direction > 0 ? false : true;
        transform.Translate(Vector3.right * speed * Time.deltaTime * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.Die(collision.GetContact(0).point, direction);
            }
            if (collision.gameObject != null)
            {
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.GetComponent<SecretArea>() != null)
        {
            collision.gameObject.GetComponent<SecretArea>().OpenArea();
        }

        Instantiate(splashParticles, transform.position, Quaternion.Euler(new Vector3(0, 90 * direction, 0)));
        Destroy(gameObject);

    }

}
