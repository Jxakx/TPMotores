using UnityEngine;

public class Snake : Entity
{
    [SerializeField] GameObject VenomBallPrefab;
    [SerializeField] Transform shootingPoint;
    public float ShootTimer;
    private float _counter;

    protected override void Start()
    {
        Player = FindObjectOfType<Player>().transform;
        OnAttack = SpitPoison; // Asigna su ataque al Delegate
    }

    protected override void Update()
    {
        base.Update();

        _counter += Time.deltaTime;

        if (Vector3.Distance(transform.position, Player.position) < visionRange)
        {
            LookPlayer();
            if (_counter >= ShootTimer) 
            {
                OnAttack?.Invoke();
                _counter = 0; //Resetear el contador para que vuelva a disparar
            }
        }
    }

    private void SpitPoison()
    {
        Instantiate(VenomBallPrefab, shootingPoint.position, shootingPoint.rotation);
        Debug.Log("🐍 La serpiente disparó veneno.");
    }
}

