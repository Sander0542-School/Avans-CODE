using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Game
    {
        private bool _quit;

        public Game(string level, List<RoomBase> rooms, Player player)
        {
            Level = level;

            Rooms = rooms;
            Player = player;
        }

        public Player Player { get; }
        public List<RoomBase> Rooms { get; }

        public bool Quit
        {
            get => _quit;
            set
            {
                _quit = value;
                Updated?.Invoke(this, this);
            }
        }

        public string Level { get; set; }

        public event EventHandler<Game> Updated;

        /// <summary>
        ///     Move the player to the new direction
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Direction direction)
        {
            if (Player.CanMove(direction, out var room, out var nextX, out var nextY))
            {
                Player.Move(room, nextX, nextY);

                room.MoveEnemies();

                if (Player.Room.Enemies.Any(enemy => enemy.CurrentXLocation == Player.X && enemy.CurrentYLocation == Player.Y))
                {
                    Player.Damage(1);
                }

                //When all the SankaraStones are picked up, the game ends.
                if (Player.Stones >= Config.StoneNeededToWin) Quit = true;

                //When the Lives are over the game ends
                if (Player.Lives <= 0) Quit = true;

                Updated?.Invoke(this, this);
            }
        }

        public void Shoot()
        {
            if (Player.Room.ShootEnemies(Player.X, Player.Y) > 0)
            {
                Updated?.Invoke(this, this);
            }
        }
    }
}