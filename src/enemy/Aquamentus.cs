
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Enemy;
namespace ZeldaGame.Enemy;

public class Aquamentus : IEnemy {
	enum State { Idle, Walking, Attacking, Dead };
	private int frameID = 0;
	private Texture2D texture;
	private Vector2 position;
	private State state;
	private Vector2 scale;
	private static int[,] character_sprites = new int[,] { { 1, 11, 24, 32 }, { 26, 11, 24, 32 }, { 51, 11, 24, 32 }, { 76, 11, 24, 32 } }; // x, y, width, height
	private int currentFrame = 0;
	private int speed = 2;
    private int projectile_speed = 4;
	private int health = 10;
    private EnemyProjectileFactory enemyProjectileFactory;
	public Aquamentus(Texture2D texture, Vector2 position, EnemyProjectileFactory enemyProjectileFactory, Vector2 scale) {
		this.texture = texture;
		this.position = position;
		state = State.Walking;
        this.enemyProjectileFactory = enemyProjectileFactory;
		this.scale = scale;
	}

	public void Draw(SpriteBatch spriteBatch) {
		Rectangle sourceRectangle = new Rectangle(character_sprites[currentFrame, 0], character_sprites[currentFrame, 1], character_sprites[currentFrame, 2], character_sprites[currentFrame, 3]);
		Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[currentFrame, 2] * scale.X), (int)(character_sprites[currentFrame, 3] * scale.Y));
		spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
	}

	public void Update() {
        if (frameID % 17 == 0)
            currentFrame = (currentFrame + 1) % 4;
        if (frameID % 37 == 0)
            Attack();
		if (state == State.Walking)
			Walk();
		frameID++;
	}
    private void Attack() {
        Vector2[] directions = new Vector2[] { new Vector2(-0.894f, 0.447f), new Vector2(-0.894f, -0.447f), new Vector2(-1, 0) };
        foreach (Vector2 direction in directions) {
            enemyProjectileFactory.CreateProjectile(EnemyProjectileFactory.ProjectileType.Fireball, position, direction * projectile_speed);
        }
    }

	public void Walk() {
		if (frameID / 30 % 2 == 0)
            position.X += speed;
        else
            position.X -= speed;
	}

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

	public Rectangle GetRectangle()
	{
		return new Rectangle((int)position.X, (int)position.Y, (int)(character_sprites[currentFrame, 2] * scale.X), (int)(character_sprites[currentFrame, 3] * scale.Y));
	}

	public void Collide(Rectangle rectangle)
	{
		// Empty
	}
}
