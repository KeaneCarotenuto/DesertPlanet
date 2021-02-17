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
        CreateGroundTiles();

        SmoothCliffs();

        CreateObstacles();
    }

    private void CreateObstacles()
    {
        //Create Obstacles if sand below
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

    private void SmoothCliffs()
    {
        //How many times to "smooth" the cliffs
        for (int i = 0; i < cliffSmoothItterations; i++)
        {
            //An array for cliffs to be delted (dont want to delete while doing the check...)
            bool[,] toDelete;
            toDelete = new bool[width, height];

            //For each cliff, find how many cliffs are around, and maybe mark to be deleted
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //If currrent tile is cliff
                    if (groundTilemap.GetTile(new Vector3Int(x, y, 0)) == cliffTile)
                    {
                        int count = 0;
                        //search for all cliffs around
                        for (int vert = y - cliffSearchRadius; vert <= y + cliffSearchRadius; vert++)
                        {
                            for (int horiz = x - cliffSearchRadius; horiz <= x + cliffSearchRadius; horiz++)
                            {
                                //If cliff is found, add to count
                                if (groundTilemap.GetTile(new Vector3Int(horiz, vert, 0)) != null && groundTilemap.GetTile(new Vector3Int(horiz, vert, 0)) == cliffTile)
                                {
                                    count++;
                                }
                            }
                        }

                        //If cliff doesnt have enough cliffs around it, mark to be deleted
                        if (count < cliffMinOtherCliffs)
                        {
                            toDelete[x, y] = true;
                        }
                    }
                }
            }

            //Foreach tile, if marked to be deleted, make it a random sand tile
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
    }

    private void CreateGroundTiles()
    {
        //set seed for randomness
        Random.InitState((int)System.DateTime.Now.Second + (int)System.DateTime.Now.Hour);
        int offset = Random.Range(1000, 10000);

        //Loop through all tiles and calculate the height from noise map, then apply tile type
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Find noise value (0-1)
                float val = Mathf.PerlinNoise(x * noiseScale + offset, y * noiseScale + offset);

                //Caluclate some useful info from the noise and settings
                float levelRange = maxTopoHeight / topoLevels;
                float realWaterHeight = maxTopoHeight * waterHeight;
                float realTileHeight = maxTopoHeight * val;

                //If not at water level, and close enough to a different height, make it a cliff
                if ((realTileHeight) >= realWaterHeight && Mathf.Abs(realTileHeight - (Mathf.Round(realTileHeight / levelRange) * levelRange)) < cliffWidth)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), cliffTile);
                }
                // if at water level make it water
                else if (realTileHeight < realWaterHeight)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                }
                //if at the top of the height, make it stone
                else if (realTileHeight > (maxTopoHeight - levelRange))
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), stoneTile);
                }
                //Otherwise random sand tile
                else
                {
                    Tile randTile = ((int)Random.Range(0, 1 / groundEmptyChance) != 0 ? groundTiles[9] : groundTiles[Random.Range(0, groundTiles.Count)]);
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
                }

            }
        }
    }
}