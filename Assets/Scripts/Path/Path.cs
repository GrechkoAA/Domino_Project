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
    
    private bool _currenShowHandles = true;
    private int _numberHandles = 4;
    public bool Spawn;

    private void Start()
    {
        if (Application.isPlaying == true)
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
        {
            AddSegment();
        }
        else if (transform.childCount > GetNumberHandles())
        {
            RemoveSegment();
        }

        if (_currenShowHandles != _showHandles)
        {
            ShowHandles(_showHandles);
        }

        for (int i = 0; i < _handles.Count; i++)
        {
            Vector3 newPosition = _handles[i].position;

            if (_handles[i].position != newPosition)
            {

            }
        }
    }

    public void MovePoint(int i, Vector3 handle)
    {


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
        {
            handle.GetComponent<MeshRenderer>().enabled = enable;
        }
    }

    private int GetNumberHandles()
    {
        return 4 + _numberSegments * 3;
    }

    private void OnDrawGizmos()
    {
        ShowPath();

        if (_showHandles == true)
        {
            ShowHandleConnection();
        }
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
        int sigmentsNumber = 20;
        Gizmos.color = Color.green;

        Vector3[] preveousePoints = GetPreviousePoints();

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;

            for (int x = 0; x < _numberHandles - 1; x += 3)
            {
                Vector3 point = Bezier.GetPoint(_handles[x].position, _handles[x + 1].position, _handles[x + 2].position, _handles[x + 3].position, paremeter);
                Gizmos.DrawLine(preveousePoints[x / 3], point);

                preveousePoints[x / 3] = point;
            }
        }
    }

    private Vector3[] GetPreviousePoints()
    {
        Vector3[] preveousePoints = new Vector3[_handles.Count / 3];

        for (int i = 0; i < preveousePoints.Length; i++)
        {
            preveousePoints[i] = _handles[i * 3].position;
        }

        return preveousePoints;
    }

    private void SpawnFigures()
    {
        var spacing = 0.2f;
        int sigmentsNumber = 20;
        Gizmos.color = Color.green;

        Vector3[] preveousePoints = GetPreviousePoints();

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;

            for (int x = 0; x < _numberHandles - 1; x += 3)
            {
                Vector3 point = Bezier.GetPoint(_handles[x].position, _handles[x + 1].position, _handles[x + 2].position, _handles[x + 3].position, paremeter);

                var firstPoint = preveousePoints[x / 3];
                var secondPoint = point;
                
                var distance = Vector3.Distance(firstPoint, secondPoint);
                var direction = (secondPoint - firstPoint).normalized;
                var figuresCount = (int) (distance / spacing);
                
                for (int j = 0; j < figuresCount; j++)
                {
                    var newSpacing = spacing * (j + 1);
                    _spawner.Spawn(firstPoint + (direction * newSpacing));
                }
                
                preveousePoints[x / 3] = point;
            }
        }
    }
}