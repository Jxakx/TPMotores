using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.GraphicsBuffer;


//TP2 Santiago Rodriguez Barba
public class miniGolem : Entity

{

    public Transform player;
    public float rangoVision;
    public float speed;
    public Transform[] positions;
    public int index;
    public GameObject explosionEffect;
    public float explosionRadius = 3f;
    public int explosionDamage = 1;

    private bool isChasingPlayer = false;

    public Transform punto1;
    public Transform punto2;
    public Transform punto3;
    public Transform punto4;
    public GameObject ball;

    public bool shaseNow;

    protected override void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < rangoVision)
        {
            shaseNow = true;
        }

        if (shaseNow == true)
        {
            StartCoroutine(ExplodeAfterDelay(3.5f));

            Vector3 playerDirection = (player.position - transform.position).normalized;
            playerDirection.y = 0;

            if (Vector3.Distance(transform.position, player.position) > 0.5f)
            {
                transform.position += playerDirection * speed * Time.deltaTime;
            }

            Vector3 targetDirection = player.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        else
        {
            Vector3 positionDirection = (positions[index].position - transform.position).normalized;
            positionDirection.y = 0;

            transform.position = Vector3.MoveTowards(transform.position, positions[index].position, speed * Time.deltaTime);


            Vector3 targetDirection = positions[index].position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, positions[index].position) < 1f)
            {
               
                index++;
                if (index >= positions.Length)
                {
                    index = 0;
                }
                
            }
            else
            {
                Debug.Log(Vector3.Distance(transform.position, positions[index].position));
            }
        }
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Attack();
        yield break;
    }
    protected override void Attack()
    {
        
        GameObject bullet1 = Instantiate(ball);
        bullet1.GetComponent<MoveSphere>().speed = 3;
        bullet1.transform.position = punto1.position;
        bullet1.GetComponent<MoveSphere>().direction = punto1.position - transform.position;

        GameObject bullet2 = Instantiate(ball);
        bullet2.GetComponent<MoveSphere>().speed = 3;
        bullet2.transform.position = punto2.position;
        bullet2.GetComponent<MoveSphere>().direction = punto2.position - transform.position;

        GameObject bullet3 = Instantiate(ball);
        bullet3.GetComponent<MoveSphere>().speed = 3;
        bullet3.transform.position = punto3.position;
        bullet3.GetComponent<MoveSphere>().direction = punto3.position - transform.position;

        GameObject bullet4 = Instantiate(ball);
        bullet4.GetComponent<MoveSphere>().speed = 3;
        bullet4.transform.position = punto4.position;
        bullet4.GetComponent<MoveSphere>().direction = punto4.position - transform.position;

        Destroy(gameObject);
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
        // Dibujar el radio de la explosión
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(1);
            Attack();
        }

    }
}
