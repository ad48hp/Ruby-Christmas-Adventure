using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bend : MonoBehaviour
{
    public bool FromOrigMesh = false;
    public bool ExecEdit = false;
    public bool ReturnBendThen = false;
    public bool NowBend = false;
    //Works only in FromOrigMesh
    public bool LerpRot = false;
    public enum BendAxis { X, Y, Z };
    public float rotateme = 0;
    public float fromRot = -8;
    public float toRot = 8;
    public float stepRot = 1.69F;
    public bool wayRot = true;
    public float maxStep = 0.05F;
    public float fromPosition = 0.5F;
    public BendAxis axis = BendAxis.X;
    public int maxi = 3;
    public int i = 1;
    MeshFilter meshF;
    public Mesh origMesh;
    Vector3[] vertices;
    Mesh mesh;
    float rotate;

    void Update()
    {
        i += 1;
        if (i > maxi)
        {
            i = 1;
            if (NowBend && (Application.isPlaying || ExecEdit) && (1/Time.deltaTime)>25)
            {
                if (ReturnBendThen)
                {
                    NowBend = false;
                }
                else
                {
                    if (wayRot)
                    {
                        if (!LerpRot)
                        {
                            rotateme = Mathf.MoveTowards(rotateme, toRot, stepRot * Time.deltaTime);
                        }
                        else
                        {
                            rotateme = Mathf.Lerp(rotateme, toRot, stepRot * Time.deltaTime);
                        }
                       if (Mathf.Abs(rotateme - toRot) < maxStep)
                        {
                            wayRot = !wayRot;
                        }
                    }
                    else
                    {
                        if (!LerpRot)
                        {
                            rotateme = Mathf.MoveTowards(rotateme, fromRot, stepRot * Time.deltaTime);
                        }
                        else
                        {
                            rotateme = Mathf.Lerp(rotateme, fromRot, stepRot * Time.deltaTime);
                        }
                        if (Mathf.Abs(rotateme - fromRot) < maxStep)
                        {
                            wayRot = !wayRot;
                        }
                    }
                }
                if (meshF == null)
                {
                    meshF = GetComponent<MeshFilter>();
                }
                if (origMesh == null)
                {
                    origMesh = Instantiate(meshF.sharedMesh);
                }
                if (FromOrigMesh)
                {
                    mesh = Instantiate(origMesh);
                    rotate = rotateme;
                }
                else
                {
                    mesh = meshF.sharedMesh;
                    if (wayRot)
                    {
                        rotate = stepRot * Time.deltaTime;
                    }
                    else
                    {
                        rotate = -stepRot * Time.deltaTime;
                    }
                }
                vertices = mesh.vertices;
                if (axis == BendAxis.X)
                {
                    float meshWidth = mesh.bounds.size.z;
                    for (var i = 0; i < vertices.Length; i++)
                    {
                        float formPos = Mathf.Lerp(meshWidth / 2, -meshWidth / 2, fromPosition);
                        float zeroPos = vertices[i].z + formPos;
                        float rotateValue = (-rotate / 2) * (zeroPos / meshWidth);

                        zeroPos -= 2 * vertices[i].x * Mathf.Cos((90 - rotateValue) * Mathf.Deg2Rad);

                        vertices[i].x += zeroPos * Mathf.Sin(rotateValue * Mathf.Deg2Rad);
                        vertices[i].z = zeroPos * Mathf.Cos(rotateValue * Mathf.Deg2Rad) - formPos;
                    }
                }
                else if (axis == BendAxis.Y)
                {
                    float meshWidth = mesh.bounds.size.z;
                    for (var i = 0; i < vertices.Length; i++)
                    {
                        float formPos = Mathf.Lerp(meshWidth / 2, -meshWidth / 2, fromPosition);
                        float zeroPos = vertices[i].z + formPos;
                        float rotateValue = (-rotate / 2) * (zeroPos / meshWidth);

                        zeroPos -= 2 * vertices[i].y * Mathf.Cos((90 - rotateValue) * Mathf.Deg2Rad);

                        vertices[i].y += zeroPos * Mathf.Sin(rotateValue * Mathf.Deg2Rad);
                        vertices[i].z = zeroPos * Mathf.Cos(rotateValue * Mathf.Deg2Rad) - formPos;
                    }
                }
                else if (axis == BendAxis.Z)
                {
                    float meshWidth = mesh.bounds.size.x;
                    for (var i = 0; i < vertices.Length; i++)
                    {
                        float formPos = Mathf.Lerp(meshWidth / 2, -meshWidth / 2, fromPosition);
                        float zeroPos = vertices[i].x + formPos;
                        float rotateValue = (-rotate / 2) * (zeroPos / meshWidth);

                        zeroPos -= 2 * vertices[i].y * Mathf.Cos((90 - rotateValue) * Mathf.Deg2Rad);

                        vertices[i].y += zeroPos * Mathf.Sin(rotateValue * Mathf.Deg2Rad);
                        vertices[i].x = zeroPos * Mathf.Cos(rotateValue * Mathf.Deg2Rad) - formPos;
                    }
                }

                mesh.vertices = vertices;
                mesh.RecalculateBounds();
                meshF.mesh = mesh;
            }
        }
    }
}