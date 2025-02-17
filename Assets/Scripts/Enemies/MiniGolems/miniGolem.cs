using UnityEngine;

public class MiniGolem : Entity
{
    public GameObject explosionEffect;
    public float explosionRadius = 3f;
    public int explosionDamage = 1;
    public float speed = 3f;
    public bool isExploding = false;

    public Transform[] patrolPoints; // Puntos de patrulla
    private int currentPatrolIndex = 0;
    private bool isChasing = false; // Indica si está persiguiendo al jugador

    protected override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        OnAttack = Explode;
    }

    protected override void Update()
    {
        base.Update();

        if (isExploding) return; // Si ya explotó, no hacer nada más

        if (Vector3.Distance(transform.position, Player.position) < visionRange)
        {
            // 🔹 Si el jugador entra en la visión, lo persigue
            isChasing = true;
        }
        else
        {
            // 🔹 Si el jugador sale del rango, vuelve a patrullar
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        if (Player == null) return;

        Vector3 direction = (Player.position - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void Explode()
    {
        if (!isExploding)
        {
            isExploding = true;
            print("💥 Mini Golem explota y suelta piedritas 💥");

            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider hit in hitColliders)
            {
                if (hit.CompareTag("Player"))
                {
                    hit.GetComponent<Player>().TakeDamage(explosionDamage);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttack.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}


