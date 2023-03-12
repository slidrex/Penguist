using UnityEngine;

public class Bird : UnfrozenObject
{
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private GameObject dieEffect;
    private Rigidbody2D rb;
    private bool blockMovement;
    private void Start()
    {
        blockMovement = true;
        target = FindObjectOfType<PlayerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }
    private System.Collections.IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        blockMovement = false;
    }
    private void FixedUpdate()
    {
        if(blockMovement == false)
        {
            Vector2 distance = target.position - transform.position;
            rb.velocity = distance.normalized * speed;
            if(distance.x < 0)
                transform.eulerAngles = new Vector2(0, 0);
            else
                transform.eulerAngles = new Vector2(0, 180);
        }
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.GetComponent<PlayerController>() != null)
        {
            Player player = FindObjectOfType<Player>();
            player.AddRule(Entity.Rule.DisableMovement);
            player.AddRule(Entity.Rule.DisableInteraction);
            GameObject a = Instantiate(player.LoseImage, transform.position, Quaternion.identity, FindObjectOfType<Canvas>().transform);
            a.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }    
    public override void OnDamage()
    {
        GameObject obj = Instantiate(dieEffect, transform.position, Quaternion.identity, transform);
        Destroy(obj, 2);
    }
}
