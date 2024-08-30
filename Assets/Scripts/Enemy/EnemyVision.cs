using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] public UnityEvent PlayerDetected;

    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float FOVAngle = 90;
    [SerializeField] private float FOVDistance = 5;
    [SerializeField] private int NumberOfTraces = 20;

    private Mesh VisionMesh;
    private Vector3[] VisionMeshVertices;
    private int[] VisionMeshTriangles;

    private void Awake()
    {
        VisionMesh = GetComponent<MeshFilter>().mesh;
        VisionMesh.MarkDynamic();
    }
    void Start()
    {
        MakeMeshData();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Quaternion.AngleAxis(FOVAngle / 2, Vector3.up) * gameObject.transform.forward;
        float angle_step = -FOVAngle / (NumberOfTraces - 1);
        Vector3 trace_start = gameObject.transform.position;

        Vector3 trace_end;
        RaycastHit hit_info;

        for (int i = 0; i < NumberOfTraces; i++)
        {
            trace_end = trace_start + direction * FOVDistance;

            if (Physics.Linecast(trace_start, trace_end, out hit_info))
            {
                if (IsPlayer(hit_info.collider.gameObject))
                {
                    PlayerDetected.Invoke();

                    // TODO: delete this
                    Debug.Log("Hit");
                }

                VisionMeshVertices[i] = gameObject.transform.InverseTransformPoint(hit_info.point);
            }
            else
            {
                VisionMeshVertices[i] = gameObject.transform.InverseTransformPoint(trace_end);
            }

            direction = Quaternion.AngleAxis(angle_step, Vector3.up) * direction;
        }

        VisionMeshVertices[NumberOfTraces] = gameObject.transform.InverseTransformPoint(trace_start);
        UpdateMesh();
    }

    private bool IsPlayer(GameObject gameObject)
    {
        return (PlayerLayer & (1 << gameObject.layer)) != 0;
    }

    private void MakeMeshData()
    {
        VisionMeshVertices = new Vector3[NumberOfTraces + 1];
        VisionMeshTriangles = new int[NumberOfTraces * 3];

        int last_vertex_index = VisionMeshVertices.Length - 1;
        for (int i = 0; i < NumberOfTraces - 1; i++)
        {
            VisionMeshTriangles[i * 3] = i;
            VisionMeshTriangles[i * 3 + 1] = last_vertex_index;
            VisionMeshTriangles[i * 3 + 2] = i + 1;
        }
    }

    private void UpdateMesh()
    {
        VisionMesh.Clear();
        VisionMesh.vertices = VisionMeshVertices;
        VisionMesh.triangles = VisionMeshTriangles;
    }
}
