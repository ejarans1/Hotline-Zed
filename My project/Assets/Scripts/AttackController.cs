using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;



    public void attack(){
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider enemy in hitEnemies){
                print("We hit" + enemy.name);
                Destroy(enemy.gameObject);
            }
    }

    
    void onDrawGizmoSelected(){
        if(attackPoint == null) {
            return;
        }
        Gizmos.DrawSphere(attackPoint.position, attackRange);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = attackPoint.position;
    }
}
