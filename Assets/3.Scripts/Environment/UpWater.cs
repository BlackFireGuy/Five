using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpWater : MonoBehaviour
{

    public float swimspeed = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, speed);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, collision.GetComponent<PlayerController>().swimSpeed),ForceMode2D.Impulse);

            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, Mathf.Min(15, collision.GetComponent<Rigidbody2D>().velocity.y));
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, Mathf.Max(-15, collision.GetComponent<Rigidbody2D>().velocity.y));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerController>().swimSpeed > 0)
            {
                collision.GetComponent<PlayerController>().swimSpeed = -collision.GetComponent<PlayerController>().swimSpeed;
            }
        }
    }
}
