using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance;
    public float speed;
    private Vector3 startingPos;
    private int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var limitRight = startingPos.x + moveDistance;
        var limitLeft = startingPos.x - moveDistance;

        if (limitRight < transform.position.x)
        {
            direction = -1;

        } else if (limitLeft > transform.position.x)
        {
            direction = 1;
        }
        transform.Translate(speed * Time.deltaTime * (Vector3.right * direction));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * moveDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * moveDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

}
