using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform[] _positions;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.positionCount = _positions.Length;
        _lineRenderer.SetPosition(0, _positions[0].position);
        _lineRenderer.SetPosition(1, _positions[1].position);
        
    }
}
