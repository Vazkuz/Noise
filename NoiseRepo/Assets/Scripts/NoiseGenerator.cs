using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public static float[,] Generate(int width, int height, float scale, Vector2 offset)
    {
        int newNoise = Random.Range(0,10000);
        float[,] noiseMap = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;
                noiseMap[x, y] = Mathf.PerlinNoise(samplePosX + newNoise, samplePosY + newNoise);
            }
        }

        return noiseMap;
    }
}
