using UnityEngine;

public class FollowCanvas : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Camera referencedCamera;
    [SerializeField] private Transform followObject;
    private Transform Transform;
    private void Start()
    {
        Transform = transform;
        canvas = GetComponent<Canvas>();
    }
    private void Update()
    {
        FollowToObject();
    }
    private void FollowToObject()
    {
        if(Transform.position != followObject.position) Transform.position = followObject.position;
        if(Transform.eulerAngles.y != 0.0f) Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, 0.0f, Transform.eulerAngles.z);
    }
}
