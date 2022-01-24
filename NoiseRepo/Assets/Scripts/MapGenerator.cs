using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)] public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        float[,] derivateMap = Noise.GenerateDerivateMap(noiseMap);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(derivateMap, false);
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate() 
    {
        if(mapWidth < 1)    
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 1)
        {
            octaves = 1;
        }
        if(noiseScale < 0.001f)
        {
            noiseScale = 0.001f;
        }
    }
}
