using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using GTA;
using System.Windows.Forms;
using GTA.Native;
using FusionLibrary;
using FusionLibrary.Extensions;
using GTA.Math;
using GTA.UI;
using static GTA.Native.NativeInvoker;
using static GTA.Script;
using Control = GTA.Control;
using Hash = GTA.Native.Hash;
using Screen = GTA.UI.Screen;
using Notification = GTA.UI.Notification;

namespace GTA5Test
{
	internal static class Prologue1
	{
		public enum TEXT_FONTS
		{
			FONT_STANDARD = 0,
			FONT_CURSIVE,
			FONT_ROCKSTAR_TAG,
			FONT_LEADERBOARD,
			FONT_CONDENSED,
			FONT_STYLE_FIXED_WIDTH_NUMBERS,
			FONT_CONDENSED_NOT_GAMERNAME,
			FONT_STYLE_PRICEDOWN,
			FONT_STYLE_TAXI
		}
		public static void DrawTextCrap()
		{

			/*REQUEST_ADDITIONAL_TEXT("PROLOG", 0); // separate GXT2 file.
			if (!HAS_ADDITIONAL_TEXT_LOADED(0))
				Wait(100);*/
			
			SETTIMERA(0);
			Screen.FadeOut(175);
			while (true)
			{
				SET_TEXT_SCALE(0.65f, 0.65f); // scale, size
				var fVar1 = GET_RENDERED_CHARACTER_HEIGHT(0.65f, (int)TEXT_FONTS.FONT_STANDARD);
				SET_TEXT_CENTRE(true);
				SET_SCRIPT_GFX_DRAW_ORDER(7);
				BEGIN_TEXT_COMMAND_DISPLAY_TEXT(/*"PRO_SETTING"*/ "STRING"); // draw crap on screen
				ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME($"{FusionUtils.CurrentTime.ToLongDateString()}\n{FusionUtils.CurrentTime.ToLongTimeString()}");
				END_TEXT_COMMAND_DISPLAY_TEXT(0.5f, 0.9f - fVar1 / 2, 0);
				// -1f to 1f.
				// X - Horizontal
				// Y - Vertical
				Wait(1);
				if (TIMERA() > 2500)
				{
					break;
				}
			}
			RESET_SCRIPT_GFX_ALIGN();
			HIDE_LOADING_ON_FADE_THIS_FRAME();
			Screen.FadeIn(175);
			Main.ScaleformRender = false;
			return;
		}
	}
}