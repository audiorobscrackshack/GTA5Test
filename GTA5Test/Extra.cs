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
        /// <summary>
        /// Shows a notification on TheFeed, specifically "Cheat activated:\n". The substring is what comes next. So you can have, for example: "Cheat activated:\nSpawn Comet.".<br/>
        /// </summary>
        /// <param name="subString">Plain-text string.</param>
        public static void ShowCheatActivatedNotificationWithSubString(string subString)
        {
            BEGIN_TEXT_COMMAND_THEFEED_POST("CHEAT_ACTIVATED");
            ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(subString);
            END_TEXT_COMMAND_THEFEED_POST_TICKER(false, true);
        }

        public enum DashboardType
        {
            VDT_BANSHEE = 0,
            VDT_BOBCAT = 1,
            VDT_CAVALCADE = 2,
            VDT_DUKES = 4,
            VDT_FELTZER = 6,
            VDT_FEROCI = 7,
            VDT_FUTO = 8,
            VDT_GENTAXI = 9,
            VDT_PEYOTE = 11,
            VDT_SPEEDO = 13,
            VDT_SULTAN = 14,
            VDT_SUPERGT = 15,
            VDT_TAILGATER = 16,
            VDT_TRUCK = 17,
            VDT_TRUCKDIGI = 18,
            VDT_ZTYPE = 20,
            VDT_SPORTBK = 22,
            VDT_RACE = 23
        }
    }
}
