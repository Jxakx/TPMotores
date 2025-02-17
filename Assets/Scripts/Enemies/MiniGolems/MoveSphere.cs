using UnityEngine;

// --- MOVESPHERE (Ataque de proyectil) ---
public class MoveSphere : Entity
{
    public int speed = 5;
    public Vector3 direction;
    public int damage = 1;
    private bool onlyOne = true;

    protected override void Start()
    {
        base.Start();
        Player = GameObject.FindGameObjectWithTag("Player").transform; // Busca el Player en la escena
        OnAttack = MoveForward;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void MoveForward()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(new Vector3(1, 1, 1) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && onlyOne)
        {
            player.TakeDamage(damage);
            onlyOne = false;
        }
    }
}

