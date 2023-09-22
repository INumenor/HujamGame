using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrianGen : MonoBehaviour
{
    public int dirtyLayerHeight = 5;
    public int mineLayerHeight = 1;

    public Sprite stone;
    public Sprite mine;
    public Sprite dirt;
    public Sprite grass;

    public Sprite Microstone;
    public Sprite Micromine;
    public Sprite Microdirt;
    public Sprite Micrograss;

    public GameObject paneldirt;
    public GameObject panelstone;
    public GameObject panelmine;

    public List<GameObject> worldTileObject = new List<GameObject>();
    public List<Vector2> worldTiles = new List<Vector2>();

    public GameObject tildDrop;
    public GameObject tildMicroDrop;

    public bool generateCaves = true;
    public float surfaceValue = 0.25f;
    public int worldSize = 100;
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.05f;
    public float heightMultiplier = 4f;
    public int heightAddition = 25;
    
    public float seed;
    public Texture2D noiseTexture;
    public EnergyBarMan Energy;

    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;
            for (int y = 0; y < height; y++)
            {
                Sprite tileSprite;
                
                if( 3 < y && y < height - dirtyLayerHeight)
                {
                    tileSprite = stone;
                }
                else if (y < 20)
                {
                    tileSprite = mine;
                }
                else if (y < height - 1)
                {
                    tileSprite = dirt;
                }
                else
                {
                    tileSprite = grass;
                }
                if (generateCaves)
                {
                    if (noiseTexture.GetPixel(x, y).r > surfaceValue)
                    {
                        PlaceTile(tileSprite,x,y);
                    }
                }
                else
                {
                    PlaceTile(tileSprite, x, y);
                }
            }
        }
        for(int z = 0; z < worldSize; z++)
        {
            GameObject newTile = new GameObject("leftframe");
            newTile.transform.parent = this.transform;
            newTile.AddComponent<SpriteRenderer>();
            newTile.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
            newTile.GetComponent<SpriteRenderer>().sprite = stone;
            newTile.transform.position = new Vector2(0.5f,z-0.5f);

            worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
        }
        for (int z = 0; z < worldSize; z++)
        {
            GameObject newTile = new GameObject("rightframe");
            newTile.transform.parent = this.transform;
            newTile.AddComponent<SpriteRenderer>();
            newTile.AddComponent<BoxCollider2D>();
            newTile.AddComponent<BoxCollider2D>().size = new Vector2(1,1);
            newTile.GetComponent<SpriteRenderer>().sprite = stone;
            newTile.transform.position = new Vector2(worldSize + 0.5f,z-0.5f);

            worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
        }
    } 

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

            for(int x = 0; x < noiseTexture.width; x++)
            {
                for(int y = 0;y<noiseTexture.height; y++)
                {
                    float v = Mathf.PerlinNoise((x+seed)*caveFreq, (y+seed)*caveFreq);
                    noiseTexture.SetPixel(x, y, new Color(v, v, v));
                }
            }
        noiseTexture.Apply();
    }

    public void RemoveTile(int x, int y)
    {
        if(worldTileObject[worldTiles.IndexOf(new Vector2(x, y))])
        {
            Sprite tileDropSprite = null;
            Sprite tileMicroDropSprite = null;
            
            if(worldTileObject[worldTiles.IndexOf(new Vector2(x, y))].name == "stone" && panelstone.active == false)
            {
                if(6 < Random.Range(0, 10))
                {
                    tileMicroDropSprite = Microstone;
                }
                 tileDropSprite = stone;
            }
            else if (worldTileObject[worldTiles.IndexOf(new Vector2(x, y))].name == "mine"  && panelmine.active == false)
            {
                if (8 < Random.Range(0, 10))
                {
                    tileMicroDropSprite = Micromine;
                }
                tileDropSprite = mine;
            }
            else if (worldTileObject[worldTiles.IndexOf(new Vector2(x, y))].name == "dirt" && paneldirt.active == false)
            {
                if (4 < Random.Range(0, 10))
                {
                    tileMicroDropSprite = Microdirt;
                }
                tileDropSprite = dirt;
            }
            else if(worldTileObject[worldTiles.IndexOf(new Vector2(x, y))].name == "dirt_grass")
            {
                 tileDropSprite = grass;
            }

            if (tileDropSprite != null)
            {
                Destroy(worldTileObject[worldTiles.IndexOf(new Vector2(x, y))]);
                Energy.UseEnergy();
                GameObject newtileDrop = Instantiate(tildDrop, new Vector2(x, y), Quaternion.identity);
                newtileDrop.GetComponent<SpriteRenderer>().sprite = tileDropSprite;
                newtileDrop.name = tileDropSprite.name;
                if(tileMicroDropSprite != null)
                {
                    GameObject newMicrotileDrop = Instantiate(tildDrop, new Vector2(x, y), Quaternion.identity);
                    newMicrotileDrop.GetComponent<SpriteRenderer>().sprite = tileMicroDropSprite;
                    newMicrotileDrop.name = tileMicroDropSprite.name;
                }
            }
            
        }
        
    }

    public void PlaceTile(Sprite tileSprite , float x, float y)
    {
        GameObject newTile = new GameObject();
        newTile.transform.parent = this.transform;
        newTile.AddComponent<SpriteRenderer>();
        //newTile.tag = "Ground";
        newTile.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        newTile.name = tileSprite.name;
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

        worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
        worldTileObject.Add(newTile);
    }
}
