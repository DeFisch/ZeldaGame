using System;
using Microsoft.Xna.Framework;

namespace ZeldaGame{
    public static class Globals {
        public static AudioLoader audioLoader;
        public static GameStateScreenHandler gameStateScreenHandler;
        public static Vector2 scale;

        public enum Direction { Up, Left, Down, Right }
        public enum PlayerProjectiles { WoodenArrow, BlueArrow, Boomerang, BlueBoomerang, Bomb, Fireball }
        public enum Swords { None, Wood, White, Magic}
    }
}