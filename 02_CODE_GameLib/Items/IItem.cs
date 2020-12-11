using System;
using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IItem : ICoordinates
    {
        int Damage { get; set; }

        bool Visible { get; set; }
        
        void OnUse(Player player);

        IItem GetItem()
        {
            return this;
        }
    }
}