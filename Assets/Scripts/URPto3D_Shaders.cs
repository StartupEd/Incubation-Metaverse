using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URPto3D_Shaders : MonoBehaviour
{
    // Call this function to convert all URP shaders in the scene to the Standard shader.
    public void ConvertURPShadersToStandard()
    {
        // Loop through all renderers in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Loop through all materials in the renderer
            foreach (Material material in renderer.sharedMaterials)
            {
                if (material != null && material.shader != null)
                {
                    // Check if the material is using a URP shader
                    string shaderName = material.shader.name;
                    if (shaderName.Contains("Universal Render Pipeline"))
                    {
                        // Convert the URP shader to the Standard shader
                        material.shader = Shader.Find("Standard");

                        // Transfer main properties (e.g., color, textures)
                        if (material.HasProperty("_BaseColor"))
                            material.SetColor("_Color", material.GetColor("_BaseColor"));

                        if (material.HasProperty("_BaseMap"))
                            material.SetTexture("_MainTex", material.GetTexture("_BaseMap"));

                        Debug.Log($"Converted {shaderName} to Standard shader for material {material.name}");
                    }
                }
            }
        }
    }
}
