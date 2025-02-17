using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    public GameObject[] platformsGroupA; // Primer grupo de plataformas
    public GameObject[] platformsGroupB; // Segundo grupo de plataformas

    private bool isGroupAActive = true; // Controla qué grupo está activo

    private void OnEnable()
    {
        Player.OnJumpEvent += TogglePlatforms;
    }

    private void OnDisable()
    {
        Player.OnJumpEvent -= TogglePlatforms;
    }

    private void Start()
    {
        SetPlatformState(isGroupAActive); // Asegurar que el juego empieza con un grupo activo
    }

    private void TogglePlatforms()
    {
        isGroupAActive = !isGroupAActive; // Cambiar de estado
        SetPlatformState(isGroupAActive);
    }

    private void SetPlatformState(bool activateGroupA)
    {
        foreach (GameObject platform in platformsGroupA)
        {
            if (platform != null) platform.SetActive(activateGroupA);
        }
        foreach (GameObject platform in platformsGroupB)
        {
            if (platform != null) platform.SetActive(!activateGroupA);
        }
    }
}
