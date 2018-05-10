using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class sg_Protanopia : MonoBehaviour {

    private Shader protaniopiaShader;
    private Material mat;
    [Range(0, 1)]
    public float blend = 1f;

    private void Start()
    {
        if (protaniopiaShader == null) protaniopiaShader = Shader.Find("Space Game/Protanopia");
        mat = new Material(protaniopiaShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat && protaniopiaShader)
        {
            mat.SetFloat("Blend", blend);
            Graphics.Blit(source, destination, mat, 0);
        }
    }
}
