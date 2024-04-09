using System;
using System.Numerics;

namespace ZeldaGame{
    public static class Globals {
        public static AudioLoader audioLoader;
        public static GameStateScreenHandler gameStateScreenHandler;

        public enum Direction { Up, Left, Down, Right }
        public enum PlayerProjectiles { WoodenArrow, BlueArrow, Boomerang, BlueBoomerang, Bomb, Fireball }
        public enum Swords {  Wood, Magic, White, None }
    }
}