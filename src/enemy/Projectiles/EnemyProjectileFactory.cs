using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ZeldaGame.Enemy.Projectiles;

public class EnemyProjectileFactory
{
    public enum ProjectileType
    {
        Fireball,
        Boomerang
    }
    private List<IEnemyProjectile> projectiles;
    private Texture2D[] texture;

    public EnemyProjectileFactory(Texture2D[] texture)
    {
        this.texture = texture;
        projectiles = new List<IEnemyProjectile>();
    }

    public void CreateProjectile(ProjectileType projectile, Vector2 location, Vector2 direction)
    {
        switch (projectile)
        {
            case ProjectileType.Fireball:
                projectiles.Add(new FireBall(texture[1], location, direction));
                break;
            case ProjectileType.Boomerang:
                projectiles.Add(new Boomerang(texture[0], location, direction));
                break;
        }
    }

    public void UpdateProjectiles()
    {
        // Update all the enemy projectiles
        foreach (IEnemyProjectile projectile in projectiles)
        {
            projectile.Update();
        }
        ClearCompletedProjetiles();
    }

    public void ClearCompletedProjetiles()
    {
        // Remove all the completed projectiles
        projectiles.RemoveAll(p => p.Completed());
    }

    public void DrawProjectiles(SpriteBatch spriteBatch)
    {
        // Draw all the enemy projectiles
        foreach (IEnemyProjectile projectile in projectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }

    public List<IEnemyProjectile> GetProjectiles()
    {
        return projectiles;
    }

    public void ClearAllProjectiles()
    {
        projectiles.Clear();
    }

    public void ClearProjectile(IEnemyProjectile projectile) {
        projectiles.Remove(projectile);
    }
}
