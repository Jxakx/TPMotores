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

        // Asignamos el ataque de la serpiente al delegate
        OnAttack = SpitVenom;
    }

    protected override void Update()
    {
        _counter += Time.deltaTime;
        base.Update(); // Llama a Update() de Entity para manejar los ataques automáticamente
    }

    private void SpitVenom()
    {
        if (_counter >= ShootTimer)
        {
            _counter = 0;
            Instantiate(VenomBallPrefab, shootingPoint.position, shootingPoint.rotation);
            print("🐍 ¡La serpiente escupe veneno!"); // El emoji xd
        }
    }
}
