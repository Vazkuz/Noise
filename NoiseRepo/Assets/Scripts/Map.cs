using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public RawImage debugImage;
    public Renderer floor;

    [Header("Dimensions")]
    public int width;
    public int height;
    public float scale;
    public Vector2 offset;


    [Header("Height Map")]
    public float[,] heightMap;
    public Color firstColor;
    public Color lastColor;


    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        heightMap = NoiseGenerator.Generate(width, height, scale, offset);

        Color[] pixels = new Color[width * height];
        int i = 0;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                pixels[i] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                i++;
            }
        }

        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels(pixels);
        tex.filterMode = FilterMode.Point;
        tex.Apply();

        debugImage.texture = tex;
        floor.material.mainTexture = tex;
    }
}
