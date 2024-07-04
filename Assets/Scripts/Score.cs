using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    
    [SerializeField]private float points;
    [SerializeField]private TextMeshProUGUI textMesh;

    private void Start()
    {
        
        textMesh = GetComponent<TextMeshProUGUI>();
        points = 0;
    }

    private void Update()
    {
        textMesh.text = points.ToString("0");
    }

    public void addPoints(float entryPoints)
    {
        points += entryPoints;
    }
}
