using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1000;
    new Rigidbody2D rigidbody2D;
   
   

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rigidbody2D.AddForce(new Vector2(x, 0) * moveSpeed * Time.deltaTime);
    }

    internal void ResetPosition()
    {
        transform.position = Vector2.zero;
        rigidbody2D.velocity = Vector2.zero;
    }

   
}
