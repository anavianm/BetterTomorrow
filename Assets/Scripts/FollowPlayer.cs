using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform followTarget;

    public float maxDistance;
    public float moveSpeed;

    void Start()
    {
        GameObject followTargetTemp = GameObject.FindWithTag("Player");
        followTarget = followTargetTemp.transform;
    }

    void Update()
    {
        float dist = (followTarget.position - transform.position).sqrMagnitude;

        if (dist > (maxDistance * maxDistance)) {
            transform.position = Vector2.Lerp(transform.position, followTarget.position, moveSpeed * Time.deltaTime);
        }
    }


}
