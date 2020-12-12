﻿using System;
using System.Drawing;

namespace CODE_GameLib.Items
{
    public interface IItem : ICoordinates
    {
        int Damage { get; set; }

        bool Visible { get; set; }

        /// <summary>
        /// Trigger when the player use the item
        /// </summary>
        /// <param name="player"></param>
        void OnUse(Player player);

        /// <summary>
        /// Returns item
        /// </summary>
        /// <returns></returns>
        IItem GetItem()
        {
            return this;
        }
    }
}