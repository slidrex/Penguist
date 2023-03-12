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
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(blockMovement == false && collider.GetComponent<PlayerController>() != null)
        {
            int die;
        }
    }   
    public override void OnDamage()
    {
        GameObject obj = Instantiate(dieEffect, transform.position, Quaternion.identity, transform);
        Destroy(obj, 2);
    }
}
