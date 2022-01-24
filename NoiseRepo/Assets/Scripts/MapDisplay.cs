using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public Renderer textureRendererDeriv;
    public void DrawNoiseMap(float[,] noiseMap, bool isNoise = true)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                colorMap[x + y * width] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        if(isNoise)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.SetPixels(colorMap);
            texture.Apply();

            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(width, 1, height);
        }
        else
        {
            Texture2D textureDeriv = new Texture2D(width, height);
            textureDeriv.SetPixels(colorMap);
            textureDeriv.Apply();

            textureRendererDeriv.sharedMaterial.mainTexture = textureDeriv;
            textureRendererDeriv.transform.localScale = new Vector3(width, 1, height);
        }
    }
}
