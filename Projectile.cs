using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector2 target;
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().gameObject.transform.position;
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position,  new Vector2 (target.x, -4f /*(target.y + 1f)*/ ), 1 * speed * Time.deltaTime);
    }
}
