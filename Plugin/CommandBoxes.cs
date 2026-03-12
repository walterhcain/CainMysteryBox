using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace walterhcain.MysteryBox
{
    public class CommandBoxes : IRocketCommand
    {
        public List<string> Aliases
        {
            get
            {
                return new List<string>() { };
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Help
        {
            get
            {
                return "Lists the donator mystery boxes you have available";
            }
        }

        public string Name
        {
            get
            {
                return "Boxes";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "MysteryBoxPlugin.boxes" };
            }
        }

        public string Syntax
        {
            get
            {
                return "<boxes>";
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer) {
                UnturnedPlayer player = (UnturnedPlayer)caller;

                List<string> bxs = new List<string>();
                foreach (RocketPermissionsGroup b in R.Permissions.GetGroups(player, true))
                {
                    foreach (MysteryBoxConfig.Group g in MysteryBoxPlugin.Instance.Configuration.Instance.Groups)
                    {
                        if (b.Id.ToLower() == g.Name.ToLower())
                        {
                            bxs.Add(g.Name);
                        }
                    }
                }
                if (bxs.Count > 0)
                {
                    UnturnedChat.Say(player, "You have the following donator Mystery Boxes: " + String.Join(", ", bxs.ToArray()));
                }
                else
                {
                    UnturnedChat.Say(player, "You have not purchased any donator Mystery Boxes, type /donate to purchase one");
                }
            }
            else
            {
                Rocket.Core.Logging.Logger.Log("Can only be called by a player");
            }
        }
            
    }
}
