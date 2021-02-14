using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemap : MonoBehaviour
{

    public Tilemap groundTilemap;
    public Tilemap obstaclesTilemap;
    public List<Tile> groundTiles;
    public List<Tile> obstTiles;
    public int width;
    public int height;

    [Range(0.0f, 1.0f)] public float groundEmptyChance;
    [Range(0.0f, 1.0f)] public float obstacleChance;

    // Start is called before the first frame update
    void Start()
    {

        for (int y = -(height / 2); y < (height/2); y++)
        {
            for (int x = -(width / 2); x < (width / 2); x++)
            {
                Tile randTile = ((int)Random.Range(0, 1/groundEmptyChance) != 0 ? groundTiles[9] : groundTiles[Random.Range(0, groundTiles.Count)]);

                groundTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
            }
        }

        for (int y = -(height / 2); y < (height / 2); y++)
        {
            for (int x = -(width / 2); x < (width / 2); x++)
            {
                Tile randTile = ((int)Random.Range(0, 1/obstacleChance) != 0 ? null : obstTiles[Random.Range(0, obstTiles.Count)]);

                if (randTile != null)
                {
                    obstaclesTilemap.SetTile(new Vector3Int(x, y, 0), randTile);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
