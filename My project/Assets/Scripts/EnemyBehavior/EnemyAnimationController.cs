using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        stopCurrentAnimation();
        setChildrenRigidBodyKinematicToFalse();
    }

    private void stopCurrentAnimation()
    {
        animator.enabled = false;
    }

    private void setChildrenRigidBodyKinematicToFalse()
    {
        foreach (Rigidbody rigidbody in this.gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = false;
        }
    }


}
