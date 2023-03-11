using UnityEngine;
using Pathfinding;

public class Bear : UnfrozenObject
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float nextWaypointDistance;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    private void FixedUpdate()
    {
        if(path == null) return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = path.vectorPath[currentWaypoint] - transform.position;
        rb.velocity = direction.normalized * speed;
        float distance = Vector2.SqrMagnitude(path.vectorPath[currentWaypoint] - transform.position);
        Turn(direction);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, nextWaypointDistance);
    }
}
