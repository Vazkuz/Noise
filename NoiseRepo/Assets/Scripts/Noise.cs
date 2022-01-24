using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
		float[,] noiseMap = new float[mapWidth,mapHeight];

		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offset.x;
			float offsetY = prng.Next (-100000, 100000) + offset.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

		if (scale <= 0) {
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
		
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				noiseMap [x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
			}
		}

		return noiseMap;
	}

    public static float[,] GenerateDerivateMap(float[,] noiseMap)
    {
        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);

        float minDeriv = float.MaxValue;
        float maxDeriv = float.MinValue;
        int[] minDerivPos = new int[2];
        int[] maxDerivPos = new int[2];

        float[,] derivateMap = new float[mapWidth, mapHeight];
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                float derivateMapX;
                float derivateMapY;

                if(x == 0)
                {
                    derivateMapX = (noiseMap[x+1, y] - noiseMap[x, y]);
                }
                else if(x == mapWidth - 1)
                {
                    derivateMapX = (noiseMap[x, y] - noiseMap[x-1, y]);
                }
                else
                {
                    derivateMapX = (noiseMap[x+1, y] - noiseMap[x-1, y]);
                }
                
                if(y == 0)
                {
                    derivateMapY = (noiseMap[x, y+1] - noiseMap[x, y]);
                }
                else if(y == mapHeight - 1)
                {
                    derivateMapY = (noiseMap[x, y] - noiseMap[x, y-1]);
                }
                else
                {
                    derivateMapY = (noiseMap[x, y+1] - noiseMap[x, y-1]);
                }
                
                derivateMap[x, y] = (derivateMapX + derivateMapY);
                
                if(derivateMap[x, y] < minDeriv)
                {
                    minDeriv = derivateMap[x, y];
                    minDerivPos[0] = x;
                    minDerivPos[1] = y;
                }
                else if(derivateMap[x, y] > maxDeriv)
                {
                    maxDeriv = derivateMap[x, y];
                    maxDerivPos[0] = x;
                    maxDerivPos[1] = y;
                }
            }
        }

        // Normalize derivates
        float[,] normalizedDMap = new float[mapWidth, mapHeight];
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                normalizedDMap[x, y] = Mathf.InverseLerp(minDeriv, maxDeriv, derivateMap[x, y]);
            }
        }
        derivateMap = normalizedDMap;

        return derivateMap;
    }
}




        // Debug.Log("minDeriv: " + Mathf.InverseLerp(minDeriv, maxDeriv, minDeriv));
        // Debug.Log("maxDeriv: " + Mathf.InverseLerp(minDeriv, maxDeriv, maxDeriv));
        // Debug.Log("0.0147106: " + Mathf.InverseLerp(minDeriv, maxDeriv, 0.0147106f));
        // Debug.Log("0: " + Mathf.InverseLerp(minDeriv, maxDeriv, 0f));
        // Debug.Log("-0.3: " + Mathf.InverseLerp(minDeriv, maxDeriv, -0.3f));
        // Debug.Log("0.3: " + Mathf.InverseLerp(minDeriv, maxDeriv, 0.3f));
        // int x_0 = minDerivPos[0];
        // int y_0 = minDerivPos[1];
        // int x_plus1 = minDerivPos[0] + 1;
        // int x_minus1 = minDerivPos[0] - 1;
        // int y_plus1 = minDerivPos[1] + 1;
        // int y_minus1 = minDerivPos[1] -1;

        // Debug.Log("Min derivate: " + minDeriv + " is at position: ( " + x_0 + ", " + y_0 + ")");
        // Debug.Log("Min derivate surrounding positions:");
        // Debug.Log("(" + x_0 + "; " + y_0 + "): " + noiseMap[x_0, y_0]);
        // Debug.Log("(" + x_minus1 + "; " + y_0 + "): " + noiseMap[x_minus1, y_0]);
        // Debug.Log("(" + x_plus1 + "; " + y_0 + "): " + noiseMap[x_plus1, y_0]);
        // Debug.Log("(" + x_0 + "; " + y_minus1 + "): " + noiseMap[x_0, y_minus1]);
        // Debug.Log("(" + x_0 + "; " + y_plus1 + "): " + noiseMap[x_0, y_plus1]);

        // Debug.Log("");
        // Debug.Log("");
        
        // x_0 = maxDerivPos[0];
        // y_0 = maxDerivPos[1];
        // x_plus1 = maxDerivPos[0] + 1;
        // x_minus1 = maxDerivPos[0] - 1;
        // y_plus1 = maxDerivPos[1] + 1;
        // y_minus1 = maxDerivPos[1] -1;

        // Debug.Log("Max derivate: " + maxDeriv + " is at position: ( " + maxDerivPos[0] + ", " + maxDerivPos[1] + ")");
        // Debug.Log("Min derivate surrounding positions:");
        // Debug.Log("(" + x_0 + "; " + y_0 + "): " + noiseMap[x_0, y_0]);
        // Debug.Log("(" + x_minus1 + "; " + y_0 + "): " + noiseMap[x_minus1, y_0]);
        // Debug.Log("(" + x_plus1 + "; " + y_0 + "): " + noiseMap[x_plus1, y_0]);
        // Debug.Log("(" + x_0 + "; " + y_minus1 + "): " + noiseMap[x_0, y_minus1]);
        // Debug.Log("(" + x_0 + "; " + y_plus1 + "): " + noiseMap[x_0, y_plus1]);