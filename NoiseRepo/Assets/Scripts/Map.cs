using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Dimensions")]
    public int width;
    public int height;
    public float scale;
    public Vector2 offset;


    [Header("Height Map")]
    public float[,] heightMap;


    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        heightMap = NoiseGenerator.Generate(width, height, scale, offset);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Debug.Log("Noise at (" + x +", " + y + "): " + heightMap[x, y]);
            }
        }
    }
}
