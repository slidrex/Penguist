using UnityEngine;
using Pathfinding;

public class Enemy : UnfrozenObject
{
    private Transform target;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float speed;
    [SerializeField] private float nextWaypointDistance;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Rigidbody2D rb;
    private bool blocked;

    private void Start()
    {
        blocked = true;
        target = FindObjectOfType<PlayerController>().transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        StartCoroutine(Delay());
    }
    private System.Collections.IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        blocked = false;
    }
    private void FixedUpdate()
    {
        if(blocked == false)
        Path();
    }
    private void Path()
    {
        if(path == null) return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        float distance = Vector2.SqrMagnitude(path.vectorPath[currentWaypoint] - transform.position);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        Movement();
    }
    private void Movement()
    {
        if(path.vectorPath.Count <= currentWaypoint) return;
        Vector2 direction = path.vectorPath[currentWaypoint] - transform.position;
        rb.velocity = direction.normalized * speed;
        Turn(direction);
    }
    private void Turn(Vector2 direction)
    {
        if(direction.x < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else
            transform.eulerAngles = new Vector2(0, 0);
    }
    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, target.position, OnPathComplete);
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
        GameObject obj = Instantiate(dieEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(obj, 2);
        FindObjectOfType<EntityStatistics>().AddStat(QuestNPC.QuestHook.KillBear, 1);
        Destroy(gameObject, 2);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, nextWaypointDistance);
    }
}
