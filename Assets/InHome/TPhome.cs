using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPhome : MonoBehaviour
{
    [SerializeField] private Transform point;
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.transform.position = point.position;
    }

}
