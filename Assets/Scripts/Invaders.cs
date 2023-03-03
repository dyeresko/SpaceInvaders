using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Invaders : MonoBehaviour
{

    public Projectile misslePrefab;
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;

    public AnimationCurve speed;



    private Vector3 _direction = Vector3.right;

    public int amountKilled { get; private set; }

    public int totalInvaders => this.rows * this.columns;

    public float missleAttackRate = 1.0f;

    public int amountAlive => totalInvaders - amountKilled;

    public float percentKilled => (float)amountKilled / totalInvaders;


    private void Start()
    {
        InvokeRepeating(nameof(MissleAttack), missleAttackRate, missleAttackRate);
    }

    private void Awake()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = (columns - 1) * 2.0f;
            float height = (rows - 1) * 2.0f;
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * 2.0f, 0.0f);
            for (int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }
    private void Update()
    {
        this.transform.position += speed.Evaluate(percentKilled) * _direction * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1))
            {
                AdvanceRow();
            }
        }
    }

    public void AdvanceRow()
    {
        _direction *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }


    void MissleAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < 1.0 / (float)this.amountAlive)
            {
                Instantiate(this.misslePrefab, invader.transform.position, Quaternion.identity);
                break;
            }
        }
    }

    void InvaderKilled()
    {
        amountKilled++;
        if (amountKilled >= totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

