using MCI_Common.Behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimulationRestaurant.Interfaces;
using System;
using TiledSharp;

namespace SimulationRestaurant
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;           
        public const int WINDOW_WIDTH = 480;
        public const int WINDOW_HEIGHT = 320;       
        TmxMap map;  
        public Texture2D tileset;
        public int tileWidth; 
        public int tileHeight; 
        public int tilesetTilesWide;
        public int tilesetTilesHigh;
        public int[,] Tiles; 
        int nbLayers = 5;
        // Affectation des textures2D 
        Texture2D textureRankChief;
        Texture2D textureServer;
        Texture2D textureHost;
        Texture2D[] textureClient;
        Texture2D textureTable;
        Texture2D textureChair;

        public IModel Model { get; set; }

        public bool Aschanged = true;

        public Game1(IModel model)
        {
            this.Model = model;
            Aschanged = false;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            Model.Change += Update;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new TmxMap("C:\\Users\\GENERAL STORES\\RiderProjects\\projetSystem\\SimulationRestaurant\\Content\\MapRestaurantNew.tmx");
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());                  
            // Récupérations des tailles des Tiles
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            // Récupérations des tailles HD des Tiles
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            // Ajout des charactere en temps que texture 2D
            textureRankChief = Content.Load<Texture2D>("RankChief");
            textureServer = Content.Load<Texture2D>("Server");
            textureHost = Content.Load<Texture2D>("Host");
            textureClient = new Texture2D[10];
            for (int i = 0; i < 10; i++)
            {
                textureClient[i] = Content.Load<Texture2D>("Client"+(i+1));
            }
            
            textureTable = Content.Load<Texture2D>("tableTwoPeople");
            textureChair = Content.Load<Texture2D>("Chair");

            
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private Vector2 PositionToVector(Position position)
        {
            return new Vector2(position.posX, position.posY);
        }
        
        protected override void Update(GameTime gametime)
        {
            // TODO: Add your update logic here

            base.Update(gametime);
        }

        private void Update(object sender, EventArgs e)
        {
            Aschanged = true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            for (int numLayer = 0; numLayer < nbLayers; numLayer++)
            {
                for (var i = 0; i < map.Layers[numLayer].Tiles.Count; i++)
                {
                    int gid = map.Layers[numLayer].Tiles[i].Gid; 

                    if (gid == 0)
                    {
                       
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (i % map.Width) * map.TileWidth; // On calcule les coordonnées de positionnement de la tile
                        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight; // On calcule les coordonnées de positionnement de la tile
                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight); // On récupere la Tile d'origine

                        spriteBatch.Begin();

                        spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White); // On dessine la Tile
                                                                                                                                  // on Draw les sprites des chara
                        spriteBatch.Draw(textureHost, PositionToVector(Model.Master.Position), Color.White);

                        foreach (var table in Model.ListTables)
                        {
                            spriteBatch.Draw(textureChair, PositionToVector(table.TableLocation), Color.White);
                            spriteBatch.Draw(textureTable, new Vector2(table.TableLocation.posX, table.TableLocation.posY + 16), Color.White);
                        }
                        foreach (var server in Model.Servers)
                        {
                            spriteBatch.Draw(textureServer, PositionToVector(server.Position), Color.White);
                        }
                        foreach (var chief in Model.Rankchiefs)
                        {
                            spriteBatch.Draw(textureRankChief, PositionToVector(chief.Position), Color.White);

                        }
                        foreach (var client in Model.Clients)
                        {
                            spriteBatch.Draw(textureClient[client.ClientList.Count-1], PositionToVector(client.Position), Color.White);
                        }
                        spriteBatch.End();                    
                    }
                }
            }





            base.Draw(gameTime);
        }
    }
}
