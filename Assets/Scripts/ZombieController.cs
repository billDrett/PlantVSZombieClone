using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damagePoints;
    const float attackFrequency = 1f;
    float timeSinceLastAttack = 100f;

    [SerializeField] State currentState = State.Moving;

    [SerializeField]  Health attackingHealth;

    public enum State
    {
        Moving,
        Attack
    } 


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.Moving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (currentState == State.Attack)
        {
            if(timeSinceLastAttack > attackFrequency && attackingHealth != null)
            {
                attackingHealth.MakeDamage(damagePoints);
                timeSinceLastAttack = 0f;
            }

            if (attackingHealth == null)
            {
                Debug.Log("KIlled him");
                //probably we killed the enemy
                currentState = State.Moving;
            }
        }

        timeSinceLastAttack += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        Debug.Log("Hit something");
        if (!other.CompareTag("Plant")){
            //only attacking plants
            return;
        }

        
        currentState = State.Attack;
        attackingHealth = other.gameObject.GetComponent<Health>();
    }
}
