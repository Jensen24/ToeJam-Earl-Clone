using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class TileManager
{
    public Dictionary<Point, Tile> CollisionTiles { get; set; }
    public Dictionary<Point, Tile> SolidTiles = new();
    public Dictionary<Point, Tile> EdgeTiles = new();
    public Dictionary<Point, Tile> WaterTiles = new();
    public Dictionary<Point, Tile> TransitionTiles = new();
    private Dictionary<Vector2, int> collisions1;
    private Dictionary<Vector2, int> dl1;
    private Dictionary<Vector2, int> gl1;
    private Dictionary<Vector2, int> wl1;
    private Dictionary<Vector2, int> sl1;
    private Texture2D WaterTexture;
    private Texture2D RoadTexture;
    private Texture2D AssetTexture;
    private Texture2D SkyboxTexture;
    private Texture2D DecosTexture;
    private Texture2D CollisionTexture;
    int display_tile_size = 64;
    int pixel_tile_size = 16;
    public void LoadContent(ContentManager content)
    {
        WaterTexture = content.Load<Texture2D>("Water");
        RoadTexture = content.Load<Texture2D>("Roads");
        AssetTexture = content.Load<Texture2D>("Assets");
        SkyboxTexture = content.Load<Texture2D>("Skybox");
        DecosTexture = content.Load<Texture2D>("IslandDecos");
        CollisionTexture = content.Load<Texture2D>("CollisionBoxes");
    }
    public void LoadLevel(string csv)
    {
        var tileData = LoadMap(csv);

        foreach (var bung in tileData)
        {
            var pos = new Point((int)bung.Key.X, (int)bung.Key.Y);
            int index = bung.Value;

            TileType type = TileTypeManager.GetCollisionTypeFromIndex(index);
            Tile tile = new Tile(new Vector2(pos.X * display_tile_size, pos.Y * display_tile_size), index, type);

            switch (type)
            {
                case TileType.Solid:
                    SolidTiles[pos] = tile;
                    break;
                case TileType.EdgeWobble:
                    EdgeTiles[pos] = tile;
                    break;
                case TileType.Water:
                    WaterTiles[pos] = tile;
                    break;
                case TileType.LevelTransition:
                    TransitionTiles[pos] = tile;
                    break;
            }
        }
    }
    public void LoadTileLayer(string collisions1Path, string dl1Path, string gl1Path, string wl1Path, string sl1Path)
    {
        collisions1 = LoadMap(collisions1Path);
        dl1 = LoadMap(dl1Path);
        gl1 = LoadMap(gl1Path);
        wl1 = LoadMap(wl1Path);
        sl1 = LoadMap(sl1Path);

        LoadLevel(collisions1Path);
    }
    private Dictionary<Vector2, int> LoadMap(string filepath)
    {
        Dictionary<Vector2, int> result = new();

        StreamReader reader = new(filepath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > -1)
                    {
                        result[new Vector2(x, y)] = value;
                    }
                }
            }
            y++;
        }
        return result;
    }
    public IEnumerable<Tile> AllCollisionTiles => 
        SolidTiles.Values
        .Concat(EdgeTiles.Values)
        .Concat(WaterTiles.Values)
        .Concat(TransitionTiles.Values);
    public void Draw(SpriteBatch spriteBatch)
    {
        int num_tiles_per_row;
        num_tiles_per_row = SkyboxTexture.Width / pixel_tile_size;
        foreach (var item in sl1)
        {
            var drect = new Rectangle(
                (int)item.Key.X * display_tile_size,
                (int)item.Key.Y * display_tile_size,
                display_tile_size,
                display_tile_size
                );
            int index = item.Value;
            int x = index % num_tiles_per_row;
            int y = index / num_tiles_per_row;

            var src = new Rectangle(
                x * pixel_tile_size,
                y * pixel_tile_size,
                pixel_tile_size,
                pixel_tile_size
                );
            spriteBatch.Draw(SkyboxTexture, drect, src, Color.White);
        }

        num_tiles_per_row = WaterTexture.Width / pixel_tile_size;
        foreach (var item in wl1)
        {
            var drect = new Rectangle(
                (int)item.Key.X * display_tile_size,
                (int)item.Key.Y * display_tile_size,
                display_tile_size,
                display_tile_size
                );
            int index = item.Value;
            int x = index % num_tiles_per_row;
            int y = index / num_tiles_per_row;

            var src = new Rectangle(
                x * pixel_tile_size,
                y * pixel_tile_size,
                pixel_tile_size,
                pixel_tile_size
                );

            spriteBatch.Draw(WaterTexture, drect, src, Color.White);
        }

        num_tiles_per_row = AssetTexture.Width / pixel_tile_size;
        foreach (var item in gl1)
        {
            var drect = new Rectangle(
                (int)item.Key.X * display_tile_size,
                (int)item.Key.Y * display_tile_size,
                display_tile_size,
                display_tile_size
                );
            int index = item.Value;
            int x = index % num_tiles_per_row;
            int y = index / num_tiles_per_row;

            var src = new Rectangle(
                x * pixel_tile_size,
                y * pixel_tile_size,
                pixel_tile_size,
                pixel_tile_size
                );

            spriteBatch.Draw(AssetTexture, drect, src, Color.White);
        }

        num_tiles_per_row = DecosTexture.Width / pixel_tile_size;
        foreach (var item in dl1)
        {
            var drect = new Rectangle(
                (int)item.Key.X * display_tile_size,
                (int)item.Key.Y * display_tile_size,
                display_tile_size,
                display_tile_size
                );
            int index = item.Value;
            int x = index % num_tiles_per_row;
            int y = index / num_tiles_per_row;

            var src = new Rectangle(
                x * pixel_tile_size,
                y * pixel_tile_size,
                pixel_tile_size,
                pixel_tile_size
                );
            spriteBatch.Draw(DecosTexture, drect, src, Color.White);
        }

        num_tiles_per_row = CollisionTexture.Width / pixel_tile_size;
        foreach (var item in collisions1)
        {
            var drect = new Rectangle(
                (int)item.Key.X * display_tile_size,
                (int)item.Key.Y * display_tile_size,
                display_tile_size,
                display_tile_size
                );
            int tileIndex = item.Value;
            int x = tileIndex % num_tiles_per_row;
            int y = tileIndex / num_tiles_per_row;

            var src = new Rectangle(
                x * pixel_tile_size,
                y * pixel_tile_size,
                pixel_tile_size,
                pixel_tile_size
                );
            spriteBatch.Draw(CollisionTexture, drect, src, Color.White * 0.2f);
        }
    }

}
