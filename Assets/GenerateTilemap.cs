using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{

    [Header("Tile shit")]
    public Tilemap groundTilemap;
    public Tilemap obstaclesTilemap;
    public List<Tile> groundTiles;
    public List<Tile> obstTiles;
    public Tile cliffTile;
    public Tile waterTile;
    public Tile stoneTile; 

    [Header("Size of map")]
    public int width;
    public int height;

    [Header("Settings")]
    [Range(0.0f, 1.0f)] public float noiseScale; //0.012f
    [Range(0.0f, 1.0f)] public float groundEmptyChance; 
    [Range(0.0f, 1.0f)] public float obstacleChance;
    [Range(0.0f, 1.0f)] public float waterHeight;
    [Range(0.0f, 50.0f)] public float maxTopoHeight;
    [Range(0.0f, 20.0f)] public float topoLevels;
    [Range(0.0f, 20.0f)] public float cliffWidth;
    [Range(0, 10)] public int cliffSearchRadius;
    [Range(0, 100)] public int cliffMinOtherCliffs;
    [Range(0, 100)] public int cliffSmoothItterations;


    // Start is called before the first frame update
    void Start()
    {
        CreateWorld();
    }

    void Update()
    {

    }

    void CreateWorld()
    {
        Random.InitState((int)System.DateTime.Now.Second + (int)System.DateTime.Now.Hour);
        int offset = Random.Range(1000, 10000);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float val = Mathf.PerlinNoise(x * noiseScale + offset, y * noiseScale + offset);

                float levelRange = maxTopoHeight / topoLevels;
                float realWaterHeight = maxTopoHeight * waterHeight;
                float realTileHeight = maxTopoHeight * val;

                Debug.Log(realTileHeight + ",  dif:" + Mathf.Abs(realTileHeight - (Mathf.Round(realTileHeight / levelRange) * levelRange)));

                if ((realTileHeight) >= realWaterHeight && Mathf.Abs(realTileHeight - (Mathf.Round(realTileHeight / levelRange) * levelRange)) < cliffWidth)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), cliffTile);
                }
                else if (realTileHeight < realWaterHeight)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                }
                else if (realTileHeight > (maxTopoHeight - levelRange))
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), stoneTile);
                }
                else
                {
                    Tile randTile = ((int)Random.Range(0, 1 / groundEmptyChance) != 0 ? groundTiles[9] : groundTiles[Random.Range(0, groundTiles.Count)]);
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
                }

            }
        }


        for (int i = 0; i < cliffSmoothItterations; i++)
        {
            bool[,] toDelete;
            toDelete = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (groundTilemap.GetTile(new Vector3Int(x, y, 0)) == cliffTile)
                    {
                        int count = 0;
                        for (int vert = y - cliffSearchRadius; vert <= y + cliffSearchRadius; vert++)
                        {
                            for (int horiz = x - cliffSearchRadius; horiz <= x + cliffSearchRadius; horiz++)
                            {
                                if (groundTilemap.GetTile(new Vector3Int(horiz, vert, 0)) != null && groundTilemap.GetTile(new Vector3Int(horiz, vert, 0)) == cliffTile)
                                {
                                    count++;
                                }
                            }
                        }

                        Debug.Log(count);

                        if (count < cliffMinOtherCliffs)
                        {
                            toDelete[x, y] = true;
                        }
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (toDelete[x, y] == true)
                    {
                        Tile randTile = ((int)Random.Range(0, 1 / groundEmptyChance) != 0 ? groundTiles[9] : groundTiles[Random.Range(0, groundTiles.Count)]);
                        groundTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
                    }
                }
            }
        }


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile groundTile = (Tile)groundTilemap.GetTile(new Vector3Int(x, y, 0));
                if (groundTile != cliffTile && groundTile != waterTile && groundTile != stoneTile)
                {
                    Tile randTile = ((int)Random.Range(0, 1 / obstacleChance) != 0 ? null : obstTiles[Random.Range(0, obstTiles.Count)]);

                    if (randTile != null)
                    {
                        obstaclesTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
                    }
                }
            }
        }
    }
}