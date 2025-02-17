using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TP2 Santiago Rodriguez Barba
public class Score : MonoBehaviour
{
    public delegate void ScoreChanged(float newScore);
    public static event ScoreChanged OnScoreChanged;

    [SerializeField] private float points;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        points = 0;
    }

    private void OnEnable()
    {
        OnScoreChanged += UpdateScoreUI;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateScoreUI;
    }

    public void AddPoints(float entryPoints)
    {
        points += entryPoints;
        OnScoreChanged?.Invoke(points); // 🔹 El evento se ejecuta aquí dentro de la clase
    }

    private void UpdateScoreUI(float newScore)
    {
        textMesh.text = newScore.ToString("0");
    }
}

