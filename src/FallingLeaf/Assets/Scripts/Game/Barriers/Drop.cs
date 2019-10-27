using UnityEngine;

public class Drop : Barrier
{
    public ParticleSystem splash;

    public float speed = 0.1f;

    private bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (destroy && !splash.isPlaying)
            Destroy(gameObject);

        upForceValue = -speed;
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Branch(Clone)" || collision.gameObject.name == "Rock(Clone)")
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            splash.Play();
            destroy = true;
        }
        else
        {
            base.OnTriggerEnter2D(collision);
        }       
    }
}
