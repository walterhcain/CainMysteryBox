using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace walterhcain.MysteryBox
{
    public class MysteryBoxConfig : IRocketPluginConfiguration
    {

        [XmlArrayItem(ElementName = "Groups")]
        public List<Group> Groups = new List<Group>();

        [XmlArrayItem(ElementName = "MysteryBox")]
        public List<Box> MysteryBoxes = new List<Box>();

        

        

     

        public void LoadDefaults()
        {

            Groups = new List<Group>()
            {
                new Group() { Name = "Sniper", GroupBox = "Sniper Box" },
                new Group() { Name = "Survivor", GroupBox = "Survivor Box" },
                new Group() { Name = "Heavy", GroupBox = "Heavy Box" }
            };

            MysteryBoxes = new List<Box>() {
                new Box() { Name = "Sniper Box", Items = new List<BoxItem>() { new BoxItem(18, 1, 1), new BoxItem(20, 2, 5), new BoxItem(263, 3, 3) }},
                new Box() { Name = "Survivor Box", Items = new List<BoxItem>() { new BoxItem(16, 1, 4), new BoxItem(81, 2, 2), new BoxItem(200, 1, 4) }},
                new Box() { Name = "Heavy Box", Items = new List<BoxItem>() { new BoxItem(1364, 1, 1), new BoxItem(1365, 2, 2), new BoxItem(263, 1, 5) }}
            };

    }
    
    public class Group
        {
            public Group() { }

            public string Name;

            [XmlArrayItem(ElementName = "Boxes")]
            public string GroupBox;
        }

    public class Box{
        public Box() { }

        public string Name; 

        [XmlArrayItem(ElementName = "Item")]
        public List<BoxItem> Items;

    }


    public class BoxItem
    {
        public BoxItem() { }

        public BoxItem(ushort itemId, byte amount, int chance)
        {
            ItemId = itemId;
            Amount = amount;
            Chance = chance;
        }

        [XmlAttribute("id")]
        public ushort ItemId;

        [XmlAttribute("amount")]
        public byte Amount;

        [XmlAttribute("chance")]
        public int Chance;

        }
}




}
