using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Game
    {
        private bool _quit;

        private readonly IDictionary<Cheat, bool> _cheats;

        public Game(string level, List<RoomBase> rooms, Player player)
        {
            Level = level;

            Rooms = rooms;
            Player = player;

            //Set all cheats to disabled
            _cheats = Enum.GetValues(typeof(Cheat)).Cast<Cheat>().ToDictionary(cheat => cheat, cheat => false);
        }

        public string Level { get; }

        public List<RoomBase> Rooms { get; }

        public Player Player { get; }

        public bool Quit
        {
            get => _quit;
            set
            {
                _quit = value;
                Updated?.Invoke(this, this);
            }
        }

        public int StonesNeeded { get; private set; } = Config.StoneNeededToWin;

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
                if (Player.Stones >= StonesNeeded) Quit = true;

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

        public void ToggleCheat(Cheat cheat)
        {
            var enabled = !_cheats[cheat];

            switch (cheat)
            {
                case Cheat.NextStoneWin:
                    StonesNeeded = enabled ? Player.Stones + 1 : Config.StoneNeededToWin;
                    break;
                case Cheat.ClosingGatePortal:
                    var gateDoors = Rooms
                        .SelectMany(room => room.Connections)
                        .Select(pair => pair.Value.Door)
                        .Where(door => door is ClosingGateDoor)
                        .Cast<ClosingGateDoor>()
                        .Distinct();

                    foreach (var closingDoor in gateDoors)
                    {
                        closingDoor.PortalMode = enabled;
                    }

                    break;
            }

            _cheats[cheat] = enabled;

            Updated?.Invoke(this, this);
        }
    }
}