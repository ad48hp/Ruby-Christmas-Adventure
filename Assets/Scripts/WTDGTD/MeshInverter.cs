using UnityEngine;

public class MeshInverter : MonoBehaviour
{
    public bool InvertFaces = true;
    public bool InvertNormals = true;
    void Start()
    {
        var mf = GetComponent<MeshFilter>();
        if (mf != null)
        {
            var m = Instantiate(mf.sharedMesh);
            Process(m, InvertFaces, InvertNormals);
            mf.sharedMesh = m;
        }
        var smr = GetComponent<SkinnedMeshRenderer>();
        if (smr != null)
        {
            var m = Instantiate(smr.sharedMesh);
            Process(m, InvertFaces, InvertNormals);
            smr.sharedMesh = m;
        }
    }

    public static void Process(Mesh m, bool f1, bool f2)
    {
        int subMeshes = m.subMeshCount;
        for (int i = 0; i < subMeshes; i++)
        {
            if (f1)
            {
                var type = m.GetTopology(i);
                var indices = m.GetIndices(i);
                if (type == MeshTopology.Quads)
                {
                    for (int n = 0; n < indices.Length; n += 4)
                    {
                        int tmp = indices[n];
                        indices[n] = indices[n + 3];
                        indices[n + 3] = tmp;
                        tmp = indices[n + 1];
                        indices[n + 1] = indices[n + 2];
                        indices[n + 2] = tmp;
                    }
                }
                else if (type == MeshTopology.Triangles)
                {
                    for (int n = 0; n < indices.Length; n += 3)
                    {
                        int tmp = indices[n];
                        indices[n] = indices[n + 1];
                        indices[n + 1] = tmp;
                    }
                }
                m.SetIndices(indices, type, i);
            }
        }
        if (f2)
        {
            var normals = m.normals;
            for (int n = 0; n < normals.Length; n++)
                normals[n] = -normals[n];
            m.normals = normals;
        }
    }
}