using UnityEngine;

[ExecuteInEditMode]
public class SwitchMeshInverter : MonoBehaviour
{
    public bool InvertFaces = true;
    public bool InvertNormals = true;
    public bool NowInvert = false;
    private MeshFilter mf;
    private SkinnedMeshRenderer smr;

    void Update()
    {
        if (NowInvert)
        {
            NowInvert = false;
            if (mf == null)
            {
                mf = GetComponent<MeshFilter>();
            }
            else
            {
                var m = Instantiate(mf.sharedMesh);
                MeshInverter.Process(m,InvertFaces,InvertNormals);
                mf.sharedMesh = m;
                if (smr == null)
                {
                    smr = GetComponent<SkinnedMeshRenderer>();
                }
                else
                {
                    var m2 = Instantiate(smr.sharedMesh);
                    MeshInverter.Process(m2, InvertFaces, InvertNormals);
                    smr.sharedMesh = m2;
                }
            }
        }
    }
}