using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public partial class BoxRing : MonoBehaviour
{
    [SerializeField] private int _gridSize = 16;
    [SerializeField] private int _tielSize = 8;
    [Space]
    [SerializeField] private float _ringRadius = 40;
    [SerializeField] private float _ringHeight = -6f;
    [SerializeField] private float _ringsScatter = 2f;
    [Space]
    [SerializeField] private float _maxRadius = 60;
    [SerializeField] private Vector2 _outHeight = new Vector2(2, 8);
    [SerializeField] private float _outScatter = 2f;
    [SerializeField] private AnimationCurve _outHeightCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [Space]
    [SerializeField] private float _perlinNoiseScale = 1;
    [HideInInspector]
    [SerializeField] private GameObject _collider;
    [Space]
    [SerializeField] private bool _gizmos = true;

    private float ColliderRadius => _tielSize + _ringRadius;

    private void OnDrawGizmosSelected ()
    {
        if (_gizmos == false)
            return;

        Color mainColor = new Color(0.5f, 1, 0.5f);

        Color gridColor = mainColor;
        gridColor.a = 0.2f;
        Gizmos.color = gridColor;
        GizmosUtility.DrawGridXZ(Vector2.zero, new Vector2Int(_gridSize, _gridSize), new Vector2(_tielSize, _tielSize));

        Gizmos.color = mainColor;
        GizmosUtility.DrawRingXZ(Vector2.zero, _ringRadius);
        GizmosUtility.DrawRingXZ(Vector2.zero, _maxRadius);

        Gizmos.color = new Color(1, 0.5f, 0.5f);
        GizmosUtility.DrawRingXZ(Vector2.zero, ColliderRadius);
    }

    [ContextMenu("Generate")]
    public void Generate ()
    {
        Vector2 totalSize = new Vector2(_gridSize * _tielSize, _gridSize * _tielSize);
        // center of bottom-left tile
        Vector3 ancorePoint = new Vector3(-totalSize.x + _tielSize, 0, -totalSize.y + _tielSize) * 0.5f;

        float[,] heightMap = new float[_gridSize, _gridSize];
        for (int x = 0; x < _gridSize; x++)
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 position = ancorePoint + new Vector3(x * _tielSize, 0, y * _tielSize);
                float distance = position.magnitude;
                float scatter = Mathf.PerlinNoise(x * _perlinNoiseScale, y * _perlinNoiseScale);
                if (distance < _ringRadius)
                {
                    heightMap[x, y] = _ringHeight + scatter *_ringsScatter;
                } 
                else
                {
                    float rate = Mathf.InverseLerp(_ringRadius, _maxRadius, distance);
                    float height = Mathf.Lerp(_outHeight.x, _outHeight.y, _outHeightCurve.Evaluate(rate));
                    heightMap[x, y] = height + scatter * _outScatter;
                }
            }

        GenerateMesh(heightMap, ancorePoint);
        SetColliders(heightMap, ancorePoint);
    }

    private void GenerateMesh (float[,] heightMap, Vector3 ancorePoint)
    {
        CombineInstance[] combine = new CombineInstance[3];
        combine[0].mesh = GenerateFloor(heightMap, ancorePoint);
        combine[1].mesh = GenerateHorizontalWalls(heightMap, ancorePoint);
        combine[2].mesh = GenerateVertivalWalls(heightMap, ancorePoint);
        for (int i = 0; i < combine.Length; i++)
            combine[i].transform = transform.localToWorldMatrix;

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine, true);
        mesh.name = "BoxRing";

        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter != null)
            filter.mesh = mesh;
    }

    private Mesh GenerateFloor (float[,] heightMap, Vector3 ancorePoint)
    {
        int totalTile = _gridSize * _gridSize;
        Vector3[] vertices = new Vector3[totalTile * 4];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[totalTile * 6];

        for (int x = 0; x < _gridSize; x++)
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 position = ancorePoint + new Vector3(x * _tielSize, heightMap[x, y], y * _tielSize);
                int index = y * _gridSize + x;
                int verticesIndex = index * 4;
                int trianglesIndex = index * 6;
                float halfTileSize = _tielSize * 0.5f;

                vertices[verticesIndex + 0] = position + new Vector3(-halfTileSize, 0, -halfTileSize);
                vertices[verticesIndex + 1] = position + new Vector3(halfTileSize, 0, -halfTileSize);
                vertices[verticesIndex + 2] = position + new Vector3(-halfTileSize, 0, halfTileSize);
                vertices[verticesIndex + 3] = position + new Vector3(halfTileSize, 0, halfTileSize);

                SetNormals(normals, verticesIndex, Vector3.up);
                SetUV(uv, verticesIndex, 0);
                SetTrianglesForRect(triangles, trianglesIndex, verticesIndex, verticesIndex + 1, verticesIndex + 2, verticesIndex + 3);
            }

        return GetMesh("Floor", vertices, normals, uv, triangles);
    }

    private Mesh GenerateHorizontalWalls (float[,] heightMap, Vector3 ancorePoint)
    {
        int totalWalls = (_gridSize -1) * _gridSize;
        Vector3[] vertices = new Vector3[totalWalls * 4];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[totalWalls * 6];

        for (int x = 0; x < _gridSize - 1; x++)
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 position = ancorePoint + new Vector3(x * _tielSize, 0, y * _tielSize);
                int index = y * (_gridSize - 1) + x;
                int verticesIndex = index * 4;
                int trianglesIndex = index * 6;
                float halfTileSize = _tielSize * 0.5f;

                float height = heightMap[x, y];
                float heightLinked = heightMap[x + 1, y];
                bool linkedLower = height > heightLinked;

                Vector3 normal;
                float minHeight, maxHeight;
                if (linkedLower)
                {
                    minHeight = heightLinked;
                    maxHeight = height;
                    normal = Vector3.right;
                } 
                else
                {
                    minHeight = height;
                    maxHeight = heightLinked;
                    normal = Vector3.left;
                }

                Vector3 positionRight = position + new Vector3(halfTileSize, 0, 0);
                vertices[verticesIndex + 0] = positionRight + new Vector3(0, minHeight, -halfTileSize);
                vertices[verticesIndex + 1] = positionRight + new Vector3(0, minHeight, halfTileSize);
                vertices[verticesIndex + 2] = positionRight + new Vector3(0, maxHeight, -halfTileSize);
                vertices[verticesIndex + 3] = positionRight + new Vector3(0, maxHeight, halfTileSize);

                float v = 1 - (maxHeight - minHeight) / _tielSize;
                SetNormals(normals, verticesIndex, normal);
                SetUV(uv, verticesIndex, v);
                SetTrianglesForRect(triangles, trianglesIndex, verticesIndex, verticesIndex + 1, verticesIndex + 2, verticesIndex + 3, linkedLower);
            }

        return GetMesh("HorizontalWalls", vertices, normals, uv, triangles);
    }

    private Mesh GenerateVertivalWalls (float[,] heightMap, Vector3 ancorePoint)
    {
        int totalWalls = _gridSize * (_gridSize - 1);
        Vector3[] vertices = new Vector3[totalWalls * 4];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[totalWalls * 6];

        for (int x = 0; x < _gridSize; x++)
            for (int y = 0; y < _gridSize - 1; y++)
            {
                Vector3 position = ancorePoint + new Vector3(x * _tielSize, 0, y * _tielSize);
                int index = y * _gridSize + x;
                int verticesIndex = index * 4;
                int trianglesIndex = index * 6;
                float halfTileSize = _tielSize * 0.5f;

                float height = heightMap[x, y];
                float heightLinked = heightMap[x, y + 1];
                bool linkedLower = height > heightLinked;

                Vector3 normal;
                float minHeight, maxHeight;
                if (linkedLower)
                {
                    minHeight = heightLinked;
                    maxHeight = height;
                    normal = Vector3.forward;
                }
                else
                {
                    minHeight = height;
                    maxHeight = heightLinked;
                    normal = Vector3.back;
                }
                Vector3 positionUp = position + new Vector3(0, 0, halfTileSize);
                vertices[verticesIndex + 0] = positionUp + new Vector3(halfTileSize, minHeight, 0);
                vertices[verticesIndex + 1] = positionUp + new Vector3(-halfTileSize, minHeight, 0);
                vertices[verticesIndex + 2] = positionUp + new Vector3(halfTileSize, maxHeight, 0);
                vertices[verticesIndex + 3] = positionUp + new Vector3(-halfTileSize, maxHeight, 0);

                float v = 1 - (maxHeight - minHeight) / _tielSize;
                SetNormals(normals, verticesIndex, normal);
                SetUV(uv, verticesIndex, v);
                SetTrianglesForRect(triangles, trianglesIndex, verticesIndex, verticesIndex + 1, verticesIndex + 2, verticesIndex + 3, linkedLower);
            }

        return GetMesh("VertivalWalls", vertices, normals, uv, triangles);
    }

    private void SetNormals (Vector3[] normals, int index, Vector3 normal)
    {
        for (int i = 0; i < 4; i++)
            normals[index + i] = normal;
    }

    private void SetUV (Vector2[] uv, int index, float v)
    {
        uv[index + 0] = new Vector2(0, v);
        uv[index + 1] = new Vector2(1, v);
        uv[index + 2] = Vector2.up;
        uv[index + 3] = Vector2.one;
    }

    private void SetTrianglesForRect (int[] triangles, int index, int botLeft, int botRight, int topLeft, int topRight, bool front = true)
    {
        triangles[index + 0] = botLeft;
        triangles[index + 3] = botLeft;
        if (front)
        {
            triangles[index + 1] = topLeft;
            triangles[index + 2] = topRight;
            triangles[index + 4] = topRight;
            triangles[index + 5] = botRight;
        } 
        else
        {
            triangles[index + 1] = topRight;
            triangles[index + 2] = topLeft;
            triangles[index + 4] = botRight;
            triangles[index + 5] = topRight;
        }
    }

    private Mesh GetMesh (string name, Vector3[] vertices, Vector3[] normals, Vector2[] uv, int[] triangles)
    {
        Mesh mesh = new Mesh();
        mesh.name = name;
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }

    private void SetColliders (float[,] heightMap, Vector3 ancorePoint)
    {
        if (_collider != null)
            DestroyImmediate(_collider);

        _collider = new GameObject("Collider");
        _collider.layer = gameObject.layer;
        Transform colliderTransform = _collider.transform;
        colliderTransform.parent = transform;
        colliderTransform.localPosition = Vector3.zero;
        colliderTransform.localEulerAngles = Vector3.zero;
        colliderTransform.localScale = Vector3.one;

        for (int x = 0; x < _gridSize; x++)
            for (int y = 0; y < _gridSize; y++)
            {
                Vector3 position = ancorePoint + new Vector3(x * _tielSize, 0, y * _tielSize);
                float distance = position.magnitude;
                if (distance <= ColliderRadius) {

                    BoxCollider box = _collider.AddComponent<BoxCollider>();
                    float height = heightMap[x, y];

                    Vector3 size = new Vector3(_tielSize, _tielSize, _tielSize);
                    size.y = height - _ringHeight;
                    box.size = size;

                    position.y = _ringHeight + size.y * 0.5f;
                    box.center = position;
                }
            }
    }
}
