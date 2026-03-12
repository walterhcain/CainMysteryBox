using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace walterhcain.MysteryBox
{
    public class CommandUnwrap : IRocketCommand
    {
        public List<string> Aliases
        {
            get
            {
                return new List<string>();
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
                return "Unwraps your mystery box";
            }
        }

        public string Name
        {
            get
            {
                return "unwrap";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "MysteryBox.unwrap" }; 
            }
        }

        public string Syntax
        {
            get
            {
                return "<unwrap> <Box Name>";
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if(caller is UnturnedPlayer)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                
                if (player.HasPermission("unwrap")) { 
                    if(command.Length == 1)
                    {
                        MysteryBoxConfig.Group g = null;
                        bool boxExists = false;
                        bool hasBox = false;
                        foreach(MysteryBoxConfig.Group gr in MysteryBoxPlugin.Instance.Configuration.Instance.Groups)
                        {
                            if(gr.Name.ToLower() == command[0].ToLower())
                            {
                                boxExists = true;
                                g = gr;
                                break;
                            }
                        }
                        foreach(RocketPermissionsGroup rock in R.Permissions.GetGroups(player, true))
                        {
                            
                            if(rock.Id.ToLower() == command[0].ToLower())
                            {
                                hasBox = true;
                                break;
                            }
                        }
                        if(boxExists && hasBox)
                        {
                            giveItem(player, getBox(g));
                            UnturnedChat.Say(player, MysteryBoxPlugin.Instance.Translate("box_given"));
                            Logger.Log(player.CharacterName + " was given a Mystery Box!");
                            try
                            {
                                R.Permissions.RemovePlayerFromGroup(command[0], player);
                                Logger.Log(player.CharacterName + " was removed from the Mystery Box Donator group!");
                            }
                            catch (Exception e)
                            {
                                Logger.Log(e);
                            }
                        }
                        else
                        {
                            UnturnedChat.Say(player, "You do not have a box of this name purchased, check /boxes to find the name of any boxes you own");
                        }
                    }
                    else
                    {
                        UnturnedChat.Say(player, MysteryBoxPlugin.Instance.Translate("box_invalid_params"), UnityEngine.Color.red);
                    }
                }
                else
                {
                    UnturnedChat.Say(player, MysteryBoxPlugin.Instance.Translate("box_no_perms"), UnityEngine.Color.red);
                }
            }
            else
            {
                Logger.Log(MysteryBoxPlugin.Instance.Translate("box_no_console"));
            }
        }


        private void giveItem(UnturnedPlayer player, MysteryBoxConfig.Box b)
        {
            List<int> il = new List<int>();
            int chance = 0;
            foreach (MysteryBoxConfig.BoxItem item in b.Items)
            {
                chance = item.Chance;
                if (chance > 30)
                {
                    chance = 30;
                }
                for (int i = 0; i < chance; i++)
                {
                    il.Add(MysteryBoxPlugin.Instance.Configuration.Instance.MysteryBoxes.IndexOf(b));
                }
            }
            int rand = getRandom(il.Count);
            player.GiveItem(b.Items[il[rand]].ItemId, b.Items[il[rand]].Amount);
        }
        

        private MysteryBoxConfig.Box getBox(MysteryBoxConfig.Group g)
        {
            foreach(MysteryBoxConfig.Box b in MysteryBoxPlugin.Instance.Configuration.Instance.MysteryBoxes)
            {
                if(b.Name.ToLower() == g.GroupBox.ToLower())
                {
                    return b;
                }
            }
            return null;
        }
        



        private int getRandom(int max)
        {
            Random r = new Random();
            int rInt = r.Next(0, max);
            return rInt;
        }
    }
}
