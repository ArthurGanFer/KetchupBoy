using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float enemySpeed;
    public Hazard enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemy.transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, enemy.transform.position) > maxDistance)
        {
            ResetPosition();
        }

    }

    private void ResetPosition()
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = transform.position;
    }

}
