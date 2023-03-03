using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25.0f;
    public Vector3 _direction;
    public System.Action destroyed;

    private void Update()
    {
        this.transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
