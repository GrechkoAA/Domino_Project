using System;
using Core;
using Figure;
using UnityEngine;

[ExecuteAlways]
public class Path : MonoBehaviour
{
    [SerializeField] private Transform _handleTemplate;
    [SerializeField, Min(0)] private int _numberSegments;
    [SerializeField] private bool _showHandles;
    [SerializeField] private System.Collections.Generic.List<Transform> _handles;
    [SerializeField] private Spawner _spawner;

    [Header("FiguresConfig")]
    [SerializeField] private float _spacing = 0.2f;
   
    private bool _currenShowHandles = true;
    private int _numberHandles = 4;
    private int _defaulSegmentNumbers = 20;

    private void Start()
    {
        if (Application.isPlaying)
        {
            SpawnFigures();
            Destroy(gameObject);
        }
    }

    private void CreateSegment(int count)
    {
        int segmentBlock = 0;

        for (int i = 0; i < count; i++)
        {
            Transform newSegment = Instantiate(_handleTemplate);
            _handles.Add(newSegment);

            newSegment.localScale = GetSize(i);
            newSegment.SetParent(transform);

            if (segmentBlock % 2 == 0 && segmentBlock != 0)
            {
                segmentBlock = -1;
                SetPosition();
            }

            segmentBlock++;
        }

        ShowHandles(_showHandles);
    }
 
    private void SetPosition()
    {
        float offsetHandles = 1;
        Vector3 lastPositon = new Vector3(_handles[_handles.Count - 4].position.x + 1, 0, 0);

        int handleId = _handles.Count - 3;
        int inversionSign = _handles.Count % 2 == 0 ? -1 : 1;

        _handles[handleId++].position = lastPositon + (Vector3.left + -Vector3.forward * inversionSign) * offsetHandles;
        _handles[handleId++].position = lastPositon + (Vector3.right + -Vector3.forward * inversionSign) * offsetHandles;
        _handles[handleId++].position = lastPositon + Vector3.right;
    }

    private Vector3 GetSize(int index)
    {
        return Vector3.one * (index % 2 == 0 && index != 0 ? 0.3f : 0.15f);
    }

    private void Update()
    {
        if (transform.childCount < GetNumberHandles())
            AddSegment();
        else if (transform.childCount > GetNumberHandles())
            RemoveSegment();
        
        if (_currenShowHandles != _showHandles)
            ShowHandles(_showHandles);
    }

    private void RemoveSegment()
    {
        _numberHandles = GetNumberHandles();

        for (int i = transform.childCount - 1; i >= _numberHandles; i--)
        {
            _handles.Remove(transform.GetChild(i));
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void AddSegment()
    {
        _numberHandles = GetNumberHandles();
        int countNewSegments = _numberHandles - transform.childCount;

        CreateSegment(countNewSegments);
    }

    private void ShowHandles(bool enable)
    {
        _currenShowHandles = _showHandles;

        foreach (var handle in _handles)
            handle.GetComponent<MeshRenderer>().enabled = enable;
    }

    private int GetNumberHandles()
    {
        return 4 + _numberSegments * 3;
    }

    private void OnDrawGizmos()
    {
        ShowPath();

        if (_showHandles)
            ShowHandleConnection();
    }

    private void ShowHandleConnection()
    {
        Gizmos.color = Color.black;

        for (int i = 0; i < _numberHandles - 1; i += 3)
        {
            Gizmos.DrawLine(_handles[i].position, _handles[i + 1].position);
            Gizmos.DrawLine(_handles[i + 2].position, _handles[i + 3].position);
        }
    }

    private void ShowPath()
    {
        Gizmos.color = Color.green;

        Vector3[] previousePoints = GetPreviousePoints();

        for (int i = 0; i < _defaulSegmentNumbers + 1; i++)
        {
            for (int x = 0; x < _numberHandles - 1; x += 3)
            {
                Vector3 point = Bezier.GetPoint(_handles[x].position, _handles[x + 1].position, _handles[x + 2].position, _handles[x + 3].position, GetParameter(i));
                Gizmos.DrawLine(previousePoints[x / 3], point);

                previousePoints[x / 3] = point;
            }
        }
    }

    private void SpawnFigures()
    {
        Vector3[] previousePoints = GetPreviousePoints();

        for (int i = 0; i < _defaulSegmentNumbers + 1; i++)
        {
            for (int x = 0; x < _numberHandles - 1; x += 3)
            {
                Vector3 firstPoint = previousePoints[x / 3];
                Vector3 secondPoint = Bezier.GetPoint(_handles[x].position, _handles[x + 1].position, _handles[x + 2].position, _handles[x + 3].position, GetParameter(i));
                Vector3 direction = (secondPoint - firstPoint).normalized;

                int figuresCount = CalculateNumberOfFiguresOverDistance(firstPoint, secondPoint);
                
                for (int j = 0; j < figuresCount; j++)
                    _spawner.Spawn(firstPoint + (direction * GetNewSpacing(j)));

                previousePoints[x / 3] = secondPoint;
            }
        }
    }
    
    private float GetParameter(int currentSegment)
    {
        return (float)currentSegment / _defaulSegmentNumbers;
    }

    private int CalculateNumberOfFiguresOverDistance(Vector3 firstPoint, Vector3 secondPoint)
    {
        var distance = Vector3.Distance(firstPoint, secondPoint);
        return (int)(distance / _spacing);
    }

    private float GetNewSpacing(int figureNumber)
    {
        return _spacing * (figureNumber + 1);
    }

    private Vector3[] GetPreviousePoints()
    {
        Vector3[] previousePoints = new Vector3[_handles.Count / 3];

        for (int i = 0; i < previousePoints.Length; i++)
            previousePoints[i] = _handles[i * 3].position;

        return previousePoints;
    }
}