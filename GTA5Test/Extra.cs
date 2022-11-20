using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Windows.Forms;
using GTA;
using GTA.Native; // SHVDN Native thing.
using static GTA.Native.NativeInvoker;

namespace GTA5Test
{
    internal static class Extra
    {
        public static void SCRIPT_INIT(string scriptName, int stackSize) //Position - 0x269D2
        {
            if (!DOES_SCRIPT_EXIST(scriptName)) return;

            REQUEST_SCRIPT(scriptName);
            if (HAS_SCRIPT_LOADED(scriptName))
            {
                START_NEW_SCRIPT(scriptName, stackSize);
                SET_SCRIPT_AS_NO_LONGER_NEEDED(scriptName);
            }
        }
        public static void _SHOW_CHEAT_ACTIVATED_NOTIFICATION_WITH_SUBSTRING_THEFEED(string subString)
        // Shows a notification on TheFeed, specifically "Cheat activated:\n". The substring is what comes next. So you can have, for example: "Cheat activated:\n Spawn Comet.". \n was added by me, the game uses a new line for this instead.
        {
            BEGIN_TEXT_COMMAND_THEFEED_POST("CHEAT_ACTIVATED" /*Cheat activated:~n~~a~*/);
            ADD_TEXT_COMPONENT_SUBSTRING_TEXT_LABEL(subString);
            END_TEXT_COMMAND_THEFEED_POST_TICKER(false, true);
        }

        public enum DashboardType
        {
            //VDT_DEFAULT = -1, //unused.
            VDT_BANSHEE = 0,
            VDT_BOBCAT = 1,
            VDT_CAVALCADE = 2,
            //VDT_COMET = 3, //unused. Same as DUKES
            VDT_DUKES = 4,
            //VDT_FACTION = 5, //unused. Same as SULTAN
            VDT_FELTZER = 6,
            VDT_FEROCI = 7,
            VDT_FUTO = 8,
            VDT_GENTAXI = 9,
            //VDT_MAVERICK = 10, //in a separate file
            VDT_PEYOTE = 11,
            //VDT_RUINER = 12, //unused.
            VDT_SPEEDO = 13,
            VDT_SULTAN = 14,
            VDT_SUPERGT = 15,
            VDT_TAILGATER = 16,
            VDT_TRUCK = 17,
            VDT_TRUCKDIGI = 18,
            //VDT_INFERNUS = 19, //unused.
            VDT_ZTYPE = 20,
            //VDT_LAZER = 21, //in a separate file
            VDT_SPORTBK = 22,
            VDT_RACE = 23,
            //VDT_LAZER_VINTAGE = 24, //in a separate file.
            //VDT_PBUS2 = 25 //unused.
        }

        public enum CheatType
        // It adds them together! Not overwrite the value. Used as some sort of cheats ran in this session.
        // It's almost like binary numbers that get flipped.
        // For example, if you've used the vehicle cheats twice, it will only have that value ONCE. Not twice.
        // It most likely is binary numbers that get flipped, I'll check the MemoryAddress's values tomorrow.
        {
            Painkiller = 32768,
            Vehicle = 1048576,
            Skyfall = 131072,
        }
    }
}
