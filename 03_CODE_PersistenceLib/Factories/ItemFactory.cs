using System;
using System.Collections.Generic;
using CODE_FileSystem.Factories.Interfaces;
using CODE_GameLib.Items;
using CODE_GameLib.Items.Decorators;

namespace CODE_FileSystem.Factories
{
    public class ItemFactory : IItemFactory
    {
        //Generates an Item according to the type
        public IItem CreateItem(string type, int x, int y, IDictionary<string, string> options)
        {
            IItem roomItem;

            switch (type)
            {
                case "key":
                    roomItem = new KeyItem(x, y, options["color"]);
                    options.Add("pickupable", "true");
                    options.Add("disappearing", "true");
                    break;
                case "disappearing boobietrap":
                    roomItem = new BoobietrapItem(x, y);
                    options.Add("disappearing", "true");
                    break;
                case "boobietrap":
                    roomItem = new BoobietrapItem(x, y);
                    break;
                case "sankara stone":
                    roomItem = new SankaraStoneItem(x, y);
                    options.Add("pickupable", "true");
                    options.Add("disappearing", "true");
                    break;
                case "pressure plate":
                    roomItem = new PressurePlateItem(x, y);
                    break;
                default:
                    throw new NotImplementedException("This item has not been implemented yet");
            }

            return ApplyDecorators(roomItem, options);
        }

        public IItem ApplyDecorators(IItem item, IDictionary<string, string> options)
        {
            foreach (var option in options)
            {
                switch (option.Key)
                {
                    case "damage":
                        item = new DamageItemDecorator(item, int.Parse(option.Value));
                        break;
                    case "disappearing":
                        item = new DisappearingItemDecorator(item);
                        break;
                    case "pickupable":
                        item = new PickupableItemDecorator(item);
                        break;
                }
            }
            
            return item;
        }
    }
}