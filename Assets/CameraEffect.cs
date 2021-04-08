using UnityEngine;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour
{

    public Material material;
    public Material material2;
    public RenderTexture text;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        text.Release();
        text.width = source.width;
        print(text.width);
        text.height = source.height;
        print(text.height);
        text.Create();
        Graphics.Blit(source, text, material);
        Graphics.Blit(text, destination, material2);
    }
}