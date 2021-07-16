using UnityEngine;

public class JumpTrig : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().DoubleJump();
        }
    }
}
