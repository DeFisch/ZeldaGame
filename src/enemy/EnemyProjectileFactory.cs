using System.Collections.Generic;

namespace Enemy
{
    public class EnemyProjectileFactory
    {
        private List<IEnemyProjectile> projectiles;

        public EnemyProjectileFactory()
        {
            projectiles = new List<IEnemyProjectile>();
        }

        public void CreateProjectile()
        {
            // Create a new enemy projectile and add it to the list
            // IEnemyProjectile projectile = new IEnemyProjectile();
            // projectiles.Add(projectile);
        }

        public void UpdateProjectiles()
        {
            // Update all the enemy projectiles
            foreach (IEnemyProjectile projectile in projectiles)
            {
                projectile.Update();
            }
        }

        public void RemoveProjectile(IEnemyProjectile projectile)
        {
            // Remove the specified enemy projectile from the list
            projectiles.Remove(projectile);
        }
    }
}
