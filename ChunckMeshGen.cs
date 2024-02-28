using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChunckMeshGen : MonoBehaviour
{
    public Vector3 ChunkPosition;
    public Vector3 dimesions;

    public List<Vector3> voxels = new List<Vector3>();

    public float filter = 0.5f;
    public float noise_scale = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        ChunkPosition = transform.position;
        GenerateVoxelMap(ChunkPosition, filter);
        GenerateMesh();

    }

    private void GenerateVoxelMap(Vector3 position, float filter)
    {
        for(float x = 0; x < dimesions.x; x++)
        {
            for(float y = 0; y < dimesions.y; y++)
            {
                for(float z = 0; z < dimesions.z; z++)
                {
                    if (Perlin3D(position.x + x * noise_scale, position.y + y * noise_scale, position.z + z * noise_scale) >= filter)
                    {
                        voxels.Add(new Vector3(x,y,z));
                    }
                }
            }
        }
    }
    void ReVoxel(Vector3 Velosity)
    {
        print("remeshed!");
        voxels.Clear();
        ChunkPosition += Velosity * 16;
        GenerateVoxelMap(ChunkPosition, filter);
        GenerateMesh();

    }
    private float Perlin3D(float x, float y, float z)
    {
        float AB = Mathf.PerlinNoise(x, y);
        float BC = Mathf.PerlinNoise(y, z);
        float AC = Mathf.PerlinNoise(x, z);


        float BA = Mathf.PerlinNoise(y, x);
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, x);

        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f;
    }

    private void GenerateMesh()//Mesh mesh)
    {
        Mesh example = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < voxels.Count; i++)
        {
            GenerateFront(voxels[i], vertices, triangles);
            GenerateBack(voxels[i], vertices, triangles);
            GenerateRight(voxels[i], vertices, triangles);
            GenerateLeft(voxels[i], vertices, triangles);
            GenerateUp(voxels[i], vertices, triangles);
            GenerateDown(voxels[i], vertices, triangles);
        }

        example.vertices = vertices.ToArray();
        example.triangles = triangles.ToArray();
        example.SetUVs(0, uvs.ToArray());
        example.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = example;
    }

    void GenerateFront(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(0, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(0, 0, 1));
        vortex_list.Add(voxel_pos + new Vector3(0, 1, 0));
        vortex_list.Add(voxel_pos + new Vector3(0, 1, 1));

        GenerateReverseTriangles(last, trig_list);
    }
    void GenerateBack(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(1, 0, 1));
        vortex_list.Add(voxel_pos + new Vector3(1, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 1));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 0));

        GenerateReverseTriangles(last, trig_list);
    }
    void GenerateRight(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(0, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(1, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(0, 1, 0));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 0));

        GenerateTriangles(last, trig_list);
    }
    void GenerateLeft(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(0, 0, 1));
        vortex_list.Add(voxel_pos + new Vector3(0, 1, 1));
        vortex_list.Add(voxel_pos + new Vector3(1, 0, 1));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 1));

        GenerateTriangles(last, trig_list);
    }
    void GenerateUp(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(0, 1, 0));
        vortex_list.Add(voxel_pos + new Vector3(0, 1, 1));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 0));
        vortex_list.Add(voxel_pos + new Vector3(1, 1, 1));

        GenerateReverseTriangles(last, trig_list);
    }
    void GenerateDown(Vector3 voxel_pos, List<Vector3> vortex_list, List<int> trig_list)
    {
        int last = vortex_list.Count;

        vortex_list.Add(voxel_pos + new Vector3(1, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(1, 0, 1));
        vortex_list.Add(voxel_pos + new Vector3(0, 0, 0));
        vortex_list.Add(voxel_pos + new Vector3(0, 0, 1));

        GenerateReverseTriangles(last, trig_list);
    }

    void GenerateTriangles(int last, List<int> trig_list)
    {
        trig_list.Add(last);
        trig_list.Add(last + 2);
        trig_list.Add(last + 1);

        trig_list.Add(last + 1);
        trig_list.Add(last + 2);
        trig_list.Add(last + 3);
    }

    void GenerateReverseTriangles(int last, List<int> trig_list)
    {
        trig_list.Add(last + 1);
        trig_list.Add(last + 2);
        trig_list.Add(last);

        trig_list.Add(last + 3);
        trig_list.Add(last + 2);
        trig_list.Add(last + 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                noise_scale += 0.01f;
                print(noise_scale);
            }
            else
            {
                filter += 0.1f;
                print(filter);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                noise_scale -= 0.01f;
                print(noise_scale);
            }
            else
            {
                filter -= 0.1f;
                print(filter);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                ReVoxel(Vector3.back);
            }
            else
            {
                ReVoxel(Vector3.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                ReVoxel(Vector3.forward);
            }
            else
            {
                ReVoxel(Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ReVoxel(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ReVoxel(Vector3.right);
        }
    }
}
