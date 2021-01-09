using UnityEngine;
using System.Collections;

public class PlantController : MonoBehaviour
{
    float timeSinceLastProjectile = 100f;
    const float attackThresshold = 1f;
    [SerializeField] State currentState;
    [SerializeField] GameObject projectilePrefab;
    Health attackingHealth;
    public enum State
    {
        Idle,
        Attack
    }

    // Use this for initialization
    void Start()
    {
        currentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.Idle)
        {
            //try to find an enemy in range
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach(GameObject zombie in zombies)
            {
                // the zombies go an a straight line,
                // so if the X axis match with some approximation it means we can shoot
                int zombiePosX = (int)Mathf.Round(zombie.transform.position.x);
                int plantPosX = (int)Mathf.Round(transform.position.x);
                if(plantPosX == zombiePosX)
                {
                    currentState = State.Attack;
                    attackingHealth = gameObject.GetComponent<Health>();
                }
            }

        }

        if(currentState == State.Attack)
        {
            if (timeSinceLastProjectile > attackThresshold && attackingHealth != null)
            {
                //through projectile
                Debug.Log("Plant attack");
                timeSinceLastProjectile = 0f;
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }

            if(attackingHealth == null)
            {
                Debug.Log("Health is null");
                //killed the enemy
                //TODO maybe I could store a list of all the enemies in the row.
                //To avoid recalling and recacluting if there is any enemy in my row.
                currentState = State.Idle;
            }
        }

        timeSinceLastProjectile += Time.deltaTime;
    }
}
