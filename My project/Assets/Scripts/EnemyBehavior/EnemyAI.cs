using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    public float MoveSpeed = 4;
    public float MaxDist = 10;
    public float MinDist = 5;
    public float interactRange;
    public LayerMask hitMarkerLayers;
    public EnemyAudioHandler enemyAudioHandler;
    public float attackRate;
    public float nextAttack;
    public bool recentlyAttacked;
    public bool alive;

     public void Start ()
     {
         attackRate = 10;
         nextAttack = 0;
         recentlyAttacked = false;
         alive = true;
     }
 
     public void Update ()
     {
         if (alive)
         {
             performMovementStep();
             performAttackStep();
        }
         
     }

     private void performMovementStep()
     {
         transform.LookAt(Player);

         if (Vector3.Distance(transform.position, Player.position) >= MinDist &&
             Vector3.Distance(transform.position, Player.position) <= MaxDist)
             enemyAudioHandler.playEnemyGrowlAudioSource();
         {
             transform.position += transform.forward * MoveSpeed * Time.deltaTime;
             if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
             {
             }
         }
     }

     private void performAttackStep()
     {
         bool attackHit = detectEnemyAttackToPlayer();

     }

     private bool detectEnemyAttackToPlayer()
     {
         determineRecentlyAttackReset();
         if (!recentlyAttacked)
         {
             Collider[] enemySphereColliders = Physics.OverlapSphere(this.gameObject.transform.position,
                 interactRange,
                 hitMarkerLayers);
             foreach (Collider interactCollider in enemySphereColliders)
             {
                 enemyAudioHandler.playEnemyGrowlAudioSource();
                 CharacterStatService characterStatService = interactCollider.gameObject.GetComponent<CharacterStatService>();
                 characterStatService.decrementHealthByAmount(10f);
                 recentlyAttacked = true;
                 return true;
             }
         }

         return false;
     }

     private void determineRecentlyAttackReset()
     {
         if (Time.time > nextAttack)
         {
             nextAttack = Time.time + attackRate;
             recentlyAttacked = false;
             Debug.Log("Reset Attack Timer");
         }
     }

     public void setAlive(bool aliveValue)
     {
         alive = aliveValue;
     }
}
