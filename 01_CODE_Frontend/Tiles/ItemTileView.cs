using System;
using CODE_GameLib;
using CODE_GameLib.Items;
using CODE_GameLib.Items.Decorators;

namespace CODE_Frontend.Tiles
{
    public class ItemTileView : TileView<IItem>
    {
        public ItemTileView(IItem item) : base(item)
        {
        }

        public override string GetIcon()
        {
            if (!Tile.Visible)
            {
                return base.GetIcon();
            }

            if (Tile.GetItem() is BoobietrapItem)
            {
                var tile = Tile;

                while (tile is BaseItemDecorator decoratedTile)
                {
                    if (decoratedTile is DisappearingItemDecorator)
                    {
                        return "@";
                    }

                    tile = decoratedTile.Decoratee;
                }
                
                return "O";
            }

            return Tile.GetItem() switch
            {
                KeyItem _ => "K",
                SankaraStoneItem _ => "S",
                BoobietrapItem _ => "O",
                PressurePlateItem _ => "T",
                _ => base.GetIcon()
            };
        }

        public override ConsoleColor GetColor()
        {
            return Tile.GetItem() switch
            {
                KeyItem keyItem => Enum.Parse<ConsoleColor>(keyItem.Color, true),
                SankaraStoneItem _ => ConsoleColor.Red,
                _ => base.GetColor()
            };
        }
    }
}