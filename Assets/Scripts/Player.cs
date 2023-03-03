
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Projectile laserPrefab;
    public float speed = 5.0f;
    public bool laserActive = false;


    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, transform.position, transform.rotation);
            laserActive = true;
            projectile.destroyed += LaserDestroyed;
        }


    }

    void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Missle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
