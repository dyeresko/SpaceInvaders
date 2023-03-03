using UnityEngine;

public class Invader : MonoBehaviour
{

    public System.Action killed;
    public Sprite[] animationSprites;

    private SpriteRenderer spriteRenderer;
    public float animationTime;
    public int animationFrame;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
