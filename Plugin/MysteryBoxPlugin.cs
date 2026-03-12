using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using System;

namespace walterhcain.MysteryBox
{
    public class MysteryBoxPlugin : RocketPlugin<MysteryBoxConfig>
    {
        public static MysteryBoxPlugin Instance;
        public string version = "Version 1.0.0";


        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    new TranslationListEntry("box_invalid_params", "Invalid Parameters"),
                    new TranslationListEntry("box_no_perms", "You do not have permission to call this command"),
                    new TranslationListEntry("box_no_console", "This command can only be called by a player"),
                    new TranslationListEntry("box_failed_giving_item", "Failed to give player items"),
                    new TranslationListEntry("box_given", "You were given a Mystery Box!")
                };
            }
        }


        protected override void Load()
        {
            Instance = this;
            Logger.Log("Cain's Mystery Boxes has been successfully loaded!", ConsoleColor.Yellow);
            Logger.Log("--------------------------------------------------", ConsoleColor.Yellow);
            Logger.Log(version, ConsoleColor.Yellow);
            Logger.Log("--------------------------------------------------", ConsoleColor.Yellow);
        }


        protected override void Unload()
        {
            Logger.Log("Cain's Mystery Boxes has been successfully unloaded!", ConsoleColor.Yellow);
            Logger.Log("--------------------------------------------------", ConsoleColor.Yellow);
        }

    }
}
