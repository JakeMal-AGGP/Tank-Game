using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw;
using DrawingExample;
using DrawingExample.Core;
using Microsoft.Xna.Framework.Audio;

namespace Tools
{
    public class Tank
    {
        //Maths
        public int trapezoid_xfactor = 15;
        public int trapezoid_xaddition = 10;
        public int trapezoid_yfactor = 10;
        public float angle = 90;
        public float cannonDistance = 30;

        //Vectors
        public Vector2 position;
        public Vector2 cannonBase;
        public Vector2 cannonTip;
        public Vector2 gravity = new Vector2(0, .98f);

        //Collisions
        public Circle hitbox;
        public Circle enemyHitbox;
        public Tank enemyTank;
        List<Projectiles> projectiles = new List<Projectiles>();
        List<Projectiles> projectileTrail = new List<Projectiles>();

        //Personal Player Settings
        public bool quickMove;
        public float power = 4;
        public float lastPower;
        public int player_num;
        public Terrain Terrain;
        public Color healthColor;
        public Color tankColor;
        public bool isSelectingTank;

        //Settings
        public bool canFire = true;
        public bool turnOver = false;
        public bool gameWin = false;
        public float fuel = 100;
        public float fuel_consumption;
        public float health;
        public int tankType;
        public int projectileType;

        //Other stuffs
        Random globalRandom;
        List<Explosion> globalExplosion;
        Delete delete = new Delete();
        List<SoundEffect> soundEffects;

        public Tank(int plr, Terrain terrain, Random globalrandom, List<Explosion> globalexplosion, List<SoundEffect> se)
        {

            globalRandom = globalrandom;
            globalExplosion = globalexplosion;
            soundEffects = se;
            player_num = plr;
            Terrain = terrain;

            if(player_num == 1)
            {
                position = terrain.terrainPoints[2];
            }

            if (player_num == 2)
            {
                position = terrain.terrainPoints[terrain.terrainPoints.Length - 2];
            }

            position += new Vector2(0, -10);

            isSelectingTank = true;

            tankType = 0;
            cycleTankType(true);

            cannonBase = position;
            cannonTip = new Vector2(cannonBase.X + cannonDistance, cannonBase.Y);

            hitbox = new Circle(position, trapezoid_xfactor + trapezoid_xaddition);
        }

        public void cycleTankType(bool direction)
        {
            if(direction)
            {
                tankType++;
            }
            else
            {
                tankType--;
            }

            if(tankType < 0) { tankType = 2; }
            if(tankType > 2) { tankType = 0; }

            switch(tankType)
            {
                case 0: // Speedy Tank
                    fuel_consumption = .25f;
                    health = 50;
                    projectileType = 1;
                    break;
                case 1: // Heafty Tank
                    fuel_consumption = .75f;
                    health = 150;
                    projectileType = 1;
                    break;
                case 2: // Powerful Tank
                    fuel_consumption = .5f;
                    health = 100;
                    projectileType = 2;
                    break;
            }

            var rnd = globalRandom;

            tankColor = new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        public void drawTank(SpriteBatch spriteBatch)
        {

            cannonBase = position;

            var displacedPosition = new Vector2(cannonDistance * (float)Math.Sin(angle), cannonDistance * (float)Math.Cos(angle));

            cannonTip = displacedPosition;

            hitbox = new Circle(position, trapezoid_xfactor + trapezoid_xaddition);

            // Visualize Hitbox
            //LinePrimatives.DrawCircle(spriteBatch, 1, Color.Red, position, trapezoid_xfactor + trapezoid_xaddition, 15);

            switch (tankType)
            {
                case 0: // Speedy Tank
                    LinePrimatives.DrawRectangle(spriteBatch, 2, tankColor, new Rectangle((int)position.X - 20, (int)position.Y - 7, 40, 15));
                    break;
                case 1: // Heafty Tank
                    LinePrimatives.DrawTrapezoid(spriteBatch, position, trapezoid_xfactor, trapezoid_xaddition, trapezoid_yfactor, tankColor);
                    break;
                case 2: // Powerful Tank
                    LinePrimatives.DrawTriangle(spriteBatch, 2, tankColor, new Vector2(position.X - 20, position.Y + 10), new Vector2(position.X + 20, position.Y + 10), new Vector2(position.X, position.Y - 10));
                    break;
            }

            
            LineDrawer.DrawLine(spriteBatch, 3, tankColor, cannonBase, displacedPosition + cannonBase);
        }

        public void fire()
        {
            if(canFire)
            {
                canFire = false;

                Projectiles projectile = new Projectiles();
                projectile.velocity = (cannonTip);
                projectile.position = cannonTip + cannonBase;
                projectile.type = projectileType;

                projectiles.Add(projectile);

                lastPower = power;

                soundEffects[1].CreateInstance().Play();
            }

            
        }

        public void updateProjectiles(SpriteBatch spriteBatch)
        {
            Projectiles proj = projectiles[0];

            proj.velocity += gravity;
            proj.position += (proj.velocity) / power;

            int sec = getSector(proj.position);

            if(sec - 1 >= 0)
            {
                float prog = (proj.position.X - Terrain.terrainPoints[sec - 1].X);
                prog = prog * 1;
                Vector2 ground = new Vector2(proj.position.X, (Terrain.terrainPoints[sec - 1].Y + (Terrain.sector_rr[sec - 1] * prog)));

                var tempTrail = new Projectiles();
                tempTrail.type = 0;
                tempTrail.position = proj.position;

                projectileTrail.Add(tempTrail);

                foreach(Projectiles trail in projectileTrail)
                {
                    trail.draw(spriteBatch);
                }

                //LineDrawer.DrawLine(spriteBatch, 3, Color.Purple, proj.position, ground);

                //Projectile collide with ground
                if (proj.position.Y > ground.Y)
                {
                    endTurn(proj);
                    Console.WriteLine("Shell Hit Ground");
                }
                else if(checkCollisionWithTank(proj)) //Projectile collide with tank
                {
                    enemyTank.takeDamage(proj);
                    endTurn(proj);
                    Console.WriteLine("Shell hit tank!");
                }
            }
            if (proj.position.X <= 0 || proj.position.X > Terrain.maxX)
            {
                endTurn(proj);
                Console.WriteLine("Shell Despawned (out of range)");
            }

        }

        public void takeDamage(Projectiles proj)
        {
            health -= proj.damage;

            if(health < 0)
            {
                enemyTank.gameWin = true;
            }
        }

        public bool checkCollisionWithTank(Projectiles proj)
        {

            var dist = Vector2.Distance(enemyHitbox.Center, proj.position);

            //Console.WriteLine(dist);

            if(dist < enemyHitbox.rad)
            {
                return true;
            }

            return false;
        }

        public void moveTank(float amt)
        {

            int sec = getSector(position);

            if(fuel > 0)
            {
                if (sec - 1 >= 0)
                {
                    if (position.X + amt > 0 && position.X + amt < Terrain.terrainPoints[Terrain.terrainPoints.Length - 1].X)
                    {
                        position += new Vector2(amt, 0);
                        float prog = (position.X - Terrain.terrainPoints[sec - 1].X);
                        Vector2 ground = new Vector2(position.X, (Terrain.terrainPoints[sec - 1].Y + (Terrain.sector_rr[sec - 1] * prog)));
                        position = ground;
                        position += new Vector2(0, -10);
                        fuel -= fuel_consumption;
                    }
                }
            }
        }

        public void endTurn(Projectiles proj)
        {
            //Projectiles proj = projectiles[0];

            globalExplosion.Add(new Explosion(proj.position, soundEffects[0]));

            delete.DeleteProjectile(proj);
            projectiles.Remove(proj);
            projectileTrail.Clear();

            turnOver = true;
        }

        public int getSector(Vector2 pos)
        {
            //Projectiles proj = projectiles[0];

            int i = 0;
            while (pos.X > Terrain.terrainPoints[i].X)
            {
                if (i + 1 > Terrain.terrainPoints.Length - 1)
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            

            return i;
        }

        public void draw(SpriteBatch spriteBatch)
        {

            drawTank(spriteBatch);

            var color = healthColor;

            if(health > 66)
            {
                color = Color.Green;
            }
            else if(health <= 66 && health >= 33)
            {
                color = Color.Yellow;
            }
            else
            {
                color = Color.Red;
            }
            
            if(health > 0)
            {
                LineDrawer.DrawLine(spriteBatch, 10, color, new Vector2(position.X - 50, position.Y + 25), (new Vector2(position.X - 50, position.Y + 25) + new Vector2((health), 0)));
            }

            if (projectiles.Count != 0)
            {
                Projectiles proj = projectiles[0];

                updateProjectiles(spriteBatch);
                proj.draw(spriteBatch);
            }


        }
    }
}
