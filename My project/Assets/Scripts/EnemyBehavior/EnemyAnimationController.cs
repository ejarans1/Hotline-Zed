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
        /*stopCurrentAnimation();
        setChildrenRigidBodyKinematicToFalse();
        stopMovement();*/
    }

    public void stopEnemyAnimationCommand()
    {
        stopCurrentAnimation();
        setChildrenRigidBodyKinematicToFalse();
        stopMovement();
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

    private void stopMovement()
    {
        EnemyAI enemnAiHandler = this.gameObject.GetComponent<EnemyAI>();
        enemnAiHandler.setAlive(false);
    }


}
