using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    [Range(6f, 10f)] [SerializeField] private float bulletSpeed = 7f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
    }
}
