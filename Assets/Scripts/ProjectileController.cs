using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damagePoints = 5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile OnTriggerEnter");
        if (!other.CompareTag("Zombie"))
        {
            return;
        }

        Health zombieHealth = other.GetComponent<Health>();
        zombieHealth.MakeDamage(damagePoints);

        Destroy(gameObject);
    }
}
