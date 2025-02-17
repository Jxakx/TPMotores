using UnityEngine;

public class MiniGolem : Entity
{
    public GameObject rockProjectile; // Piedras que se generan al explotar
    public float explosionRadius = 3f;
    public int explosionDamage = 1;
    public int numRocks = 6; // Cantidad de piedras que salen volando
    public float rockSpeed = 5f;
    public float speed = 3f;
    public bool isExploding = false;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    public float stopChasingDistance = 10f; // Distancia en la que deja de perseguir
    public float chaseSpeedMultiplier = 1.5f; // Velocidad extra cuando persigue

    protected override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        OnAttack = Explode; // Se asigna el delegate, pero no se ejecuta automáticamente (solo cuando hay contacto)
    }

    protected override void Update()
    {
        base.Update();

        if (isExploding) return;

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        Debug.Log($"Distancia al jugador: {distanceToPlayer}");

        if (distanceToPlayer < visionRange)
        {
            isChasing = true;
            Debug.Log("🚨 MiniGolem detectó al jugador y lo persigue!");
        }
        else if (distanceToPlayer > stopChasingDistance)
        {
            isChasing = false;
            Debug.Log("❌ MiniGolem dejó de perseguir y vuelve a patrullar.");
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
        direction.y = 0; // Mantener en plano

        transform.position += direction * (speed * chaseSpeedMultiplier) * Time.deltaTime;
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
            print("💥 Mini Golem explota y lanza piedras 💥");


            for (int i = 0; i < numRocks; i++)
            {
                GameObject rock = Instantiate(rockProjectile, transform.position, Quaternion.identity);
                Rigidbody rb = rock.GetComponent<Rigidbody>();
                Vector3 randomDirection = Random.insideUnitSphere.normalized;
                randomDirection.y = Mathf.Abs(randomDirection.y); // Asegura que las piedras se lancen hacia arriba también
                rb.velocity = randomDirection * rockSpeed;
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider hit in hitColliders)
            {
                if (hit.CompareTag("Player"))
                {
                    print("🔥 ¡El jugador recibió daño!");
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
            OnAttack?.Invoke(); // Se usa el delegate, pero solo al tocar al jugador
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
