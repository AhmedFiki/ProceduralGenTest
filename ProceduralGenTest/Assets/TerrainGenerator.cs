using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 100; 
    public int length = 100; 
    public int depth = 20; //amplitude
    public float scale = 10f;
    public int octaves = 4; // number of octaves
    public float persistence = 0.5f; // (impact of each octave)
    public float lacunarity = 2f; // (frequency multiplier between octaves)
    public int smoothingIterations = 3;
    private Terrain terrain;
    private TerrainData terrainData;
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = GenerateTerrainData();
        ApplyTerrainData();
    }
    void Update()
    {
       
            terrainData = GenerateTerrainData();
            ApplyTerrainData();
        
    }
    void ApplyTerrainData()
    {
        terrain.terrainData = terrainData;
    }
    void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrainData();
    }

     TerrainData GenerateTerrainData()
     {
         TerrainData terrainData = new TerrainData();
         terrainData.heightmapResolution = width + 1;
         terrainData.size = new Vector3(width, depth, length);
         terrainData.SetHeights(0, 0, GenerateHeights());

         return terrainData;
     }
    

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;
                float totalAmplitude = 0f;

                for (int octave = 0; octave < octaves; octave++)
                {
                    float sampleX = (float)x / scale * frequency;
                    float sampleZ = (float)z / scale * frequency;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    totalAmplitude += amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                noiseHeight /= totalAmplitude; // Normalize the noise height
                noiseHeight = Mathf.Clamp01(noiseHeight); // Clamp the value between 0 and 1

                heights[x, z] = noiseHeight;
            }
        }

        // Apply smoothing
        
        for (int i = 0; i < smoothingIterations; i++)
        {
            SmoothHeights(heights);
        }

        return heights;
    }

    void SmoothHeights(float[,] heights)
    {
        int width = heights.GetLength(0);
        int length = heights.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float heightSum = heights[x, z];
                int neighbors = 1;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (x + i >= 0 && x + i < width && z + j >= 0 && z + j < length)
                        {
                            heightSum += heights[x + i, z + j];
                            neighbors++;
                        }
                    }
                }

                heights[x, z] = heightSum / neighbors;
            }
        }
    }

}

