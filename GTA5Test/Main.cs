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
using Control = GTA.Control;
using Hash = GTA.Native.Hash;
using Screen = GTA.UI.Screen;
using Notification = GTA.UI.Notification;


namespace GTA5Test // Yeah, not the most creative name in the world.
{
    public class Main : Script
    {
        private static bool _displayStr;
        private static bool _bonePose;
        private static bool _fakeInt;
        public static bool ScaleformRender;
        private static bool _controllerTest;
        public Main()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            var workingTitle = "GTA5Test C# Script";

            // Ω (Omega) means this is a DEBUG build
            #if DEBUG
            File.AppendAllText($"./ScriptHookVDotNet.log", $"{workingTitle} - {version}Ω ({buildDate})" + Environment.NewLine);
            #endif

            Tick += OnTick;
			KeyDown += OnKeyDown;
            Aborted += OnAbort;
        }
        private void OnTick(object sender, EventArgs e)
        {
            Ped pPed = Game.Player.Character;
            Vehicle pVeh = pPed.CurrentVehicle;

            CustomCheats.Tick();

            if (_displayStr)
            {
                Screen.ShowSubtitle($"Global_32209: {GlobalVariable.Get(32209).Read<int>()}\n Global_32210: {GlobalVariable.Get(32210).Read<int>()}\nGlobal_32211 {GlobalVariable.Get(32211).Read<int>()}");
            }

            if (_bonePose)
            {
                var boneName = "handlebars";

                if (pVeh == null) return;

                var s = pVeh.Bones[boneName];
                s.Pose = new Vector3(-0.3f, 0f, 0f);
            }

            if (_controllerTest)
            {
                var movieClip = new Scaleform("controller_test");
                if (!movieClip.IsLoaded)
                    Wait(100);

                #region Disable Controls
                Game.DisableControlThisFrame(Control.FrontendAccept);
                Game.DisableControlThisFrame(Control.FrontendCancel);
                Game.DisableControlThisFrame(Control.FrontendX);
                Game.DisableControlThisFrame(Control.FrontendY);
                
                Game.DisableControlThisFrame(Control.FrontendUp);
                Game.DisableControlThisFrame(Control.FrontendDown);
                Game.DisableControlThisFrame(Control.FrontendLeft); 
                Game.DisableControlThisFrame(Control.FrontendRight);
                
                Game.DisableControlThisFrame(Control.FrontendLb);
                Game.DisableControlThisFrame(Control.FrontendRb);
                
                Game.DisableControlThisFrame(Control.FrontendLt); 
                Game.DisableControlThisFrame(Control.FrontendRt);
                
                Game.DisableControlThisFrame(Control.FrontendLs); 
                Game.DisableControlThisFrame(Control.FrontendRs);
                
                Game.DisableControlThisFrame(Control.FrontendPause); 
                Game.DisableControlThisFrame(Control.FrontendSelect);
                
                Game.DisableControlThisFrame(Control.FrontendAxisX);
                Game.DisableControlThisFrame(Control.FrontendAxisY);
                Game.DisableControlThisFrame(Control.FrontendRightAxisX); 
                Game.DisableControlThisFrame(Control.FrontendRightAxisY);
                #endregion
                
                #region  D-Pad Buttons
                if (Game.IsControlPressed(Control.FrontendDown))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 184);
                }
                if (Game.IsControlPressed(Control.FrontendUp))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 185);
                }
                if (Game.IsControlPressed(Control.FrontendLeft))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 186);
                }
                if (Game.IsControlPressed(Control.FrontendRight))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 187);
                }
                #endregion
                
                #region Face buttons (Cross, Circle, Square, Triangle)
                if (Game.IsControlPressed(Control.FrontendAccept))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 197);
                }
                if (Game.IsControlPressed(Control.FrontendCancel))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 198);
                }
                if (Game.IsControlPressed(Control.FrontendX))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 199);
                }
                if (Game.IsControlPressed(Control.FrontendY))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 200);
                }
                #endregion

                #region Start/Select
                if (Game.IsControlPressed(Control.FrontendPause))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 196);
                }
                if (Game.IsControlPressed(Control.FrontendSelect))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 207);
                }
                #endregion

                #region L1, L2, L3, R1, R2, R3
                
                // L1/R1
                if (Game.IsControlPressed(Control.FrontendLb))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 201);
                }
                if (Game.IsControlPressed(Control.FrontendRb))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 202);
                }
                
                // L2/R2
                if (Game.IsControlPressed(Control.FrontendLt))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 203);
                }
                if (Game.IsControlPressed(Control.FrontendRt))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 204);
                }
                
                // L3/R3
                if (Game.IsControlPressed(Control.FrontendLs))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 205);
                }
                if (Game.IsControlPressed(Control.FrontendRs))
                {
                    movieClip.CallFunction("SET_INPUT_EVENT", 206);
                }
                
                
                #endregion

                #region Analog Sticks
                // Left Analog
                var lAxisX = Game.GetDisabledControlValueNormalized(Control.FrontendAxisX) * 128;
                var lAxisY = Game.GetDisabledControlValueNormalized(Control.FrontendAxisY) * 128;
                
                movieClip.CallFunction("SET_ANALOG_STICK_INPUT", true, Convert.ToInt32(lAxisX), Convert.ToInt32(lAxisY));
                
                // Right Analog
                var rAxisX = Game.GetDisabledControlValueNormalized(Control.FrontendRightAxisX) * 128;
                var rAxisY = Game.GetDisabledControlValueNormalized(Control.FrontendRightAxisY) * 128;
                
                movieClip.CallFunction("SET_ANALOG_STICK_INPUT", false, Convert.ToInt32(rAxisX), Convert.ToInt32(rAxisY));
                #endregion
                
                movieClip.Render2DScreenSpace(new PointF(1000, 200), new PointF(256, 512));
            }
            
            /*if (ScaleformRender)
            {
                var dashMov = new Scaleform("dashboard");
                if (!dashMov.IsLoaded)
                    Wait(100);
                
                var rand = new Random();
                var test = rand.NextDouble();
                var test2 = rand.Next(0, 180);
                var test3 = rand.Next(0, 10);
                var test4 = Convert.ToInt32(test);
                var test4Vool = Convert.ToBoolean(test4);
                
                dashMov.CallFunction("SET_VEHICLE_TYPE", (int)Extra.DashboardType.VDT_FEROCI);
                //RPM, speed, fuel, temp, vacuum, boost, oilTemperature, oilPressure, waterTemp, curGear
                dashMov.CallFunction("SET_DASHBOARD_DIALS", (float)test, (float)test2, (float)test, (float)test, (float)test, (float)test, (float)test2, (float)test2, (float)test2, (float)test3);
                dashMov.CallFunction("SET_DASHBOARD_LIGHTS", test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool, test4Vool);
                dashMov.CallFunction("SET_RADIO", "88:0", "radioName", "artistName", "songName");
                
                //Wait(250);
                DRAW_RECT(1, 1, 2, 2, 200, 200, 200, 255, false); // fake fade
                //TextElement text = new TextElement("Victor", new PointF(0.5f, 0.5f), 1.0f, Color.Black);
                //text.Draw();
                dashMov.Render2D();
            }*/

            /*if (ScaleformRender)
            {
                // possible names "dials_lazer", "dials_lazer_vintage", "aircraft_dials"
                var lazerMov = new Scaleform("dials_lazer");
                if (!lazerMov.IsLoaded)
                    Wait(100);
                    
                var rand = new Random();
                var test = rand.NextDouble();
                // all values between 0 and 1.
                // fuel, temp, oilPressure, battery, fuelPSI, airSpeed, verticleAirSpeed, compass, roll, pitch, alt_small, alt_large
                // compass = 0N, 0.25E, 0.5S, 0.75W
                lazerMov.CallFunction("SET_DASHBOARD_DIALS", (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test, (float)test);
                // gearUp, gearDown, breach (doesn't work)
                lazerMov.CallFunction("SET_DASHBOARD_LIGHTS", true, true, true);
                lazerMov.CallFunction("SET_AIRCRAFT_HUD", "000", "111", "222", "333");

                DRAW_RECT(1, 1, 2, 2, 100, 110, 100, 255, false); // fake fade
                lazerMov.Render2D();
            }*/

            if (_fakeInt)
            {
                var powerStationLocation = new Vector3(2795f, 1600f, 0);
                Screen.ShowSubtitle($"{powerStationLocation.DistanceTo2D(pPed.Position)}");
                if (pVeh == null) // iLocal_1581 was probably some other check in the mission, but this works for my use case.
                {
                    if (powerStationLocation.DistanceTo2D(pPed.Position) < 300f)
                    {
                        var height = pPed.Position.Z;
                        var zoom = height switch // requires C# 9.0, annoyingly.
                        {
                            > 66f => 8,
                            > 50f => 7,
                            > 47f => 6,
                            > 44.5f => 5,
                            > 41.5f => 4,
                            > 39f => 3,
                            > 33f => 2,
                            > 25f => 1,
                            _ => 0
                        };

                        SET_RADAR_AS_INTERIOR_THIS_FRAME(GET_HASH_KEY("V_FakePowerStationCS503"), 2795f, 1600f,
                            0,
                            zoom);
                        SET_INSIDE_VERY_LARGE_INTERIOR(true);
                    }
                }
            }

            /*if (pVeh != null)
            {
                Screen.ShowSubtitle($"{World.Raycast(pVeh.Position, pVeh.Position.GetSingleOffset(FusionEnums.Coordinate.Z, -1f), IntersectFlags.Map, pVeh).MaterialHash.ToString()}");
            }*/

            if (ScaleformRender)
            {
                Prologue1.DrawTextCrap();
                //CLEAR_ADDITIONAL_TEXT(0, false);
                return;
            }
        }
        private static void OnAbort(object sender, EventArgs e)
        {
            _displayStr = false;
            _bonePose = false;
            _fakeInt = false;
            ScaleformRender = false;
            _controllerTest = false;
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Ped pPed = Game.Player.Character;
            Vehicle pVeh = pPed.CurrentVehicle;
            
            if (e.KeyCode == Keys.F1)
            {
                _fakeInt = !_fakeInt;
                Screen.ShowHelpText($"FakeInt is now {_fakeInt.ToString().ToLower()}");
            }
            
            /*if (e.KeyCode == Keys.F2)
            {
                if (pVeh != null)
                {
                    Screen.ShowSubtitle("You can't be inside a vehicle!");
                    return;
                }
                Screen.ShowSubtitle("trying to create metro");
                Function.Call(Hash.REQUEST_MODEL, "metrotrain");
                Wait(100);
                Function.Call(Hash.CREATE_MISSION_TRAIN, 25, pPed.Position.X, pPed.Position.Y, pPed.Position.Z, true);
                Screen.ShowSubtitle("Train Created. Probably?");
            }*/
            
            if (e.KeyCode == Keys.F2)
            {
                ScaleformRender = !ScaleformRender;
                Screen.ShowHelpText($"ScaleformRender is now {ScaleformRender.ToString().ToLower()}");
            }
            
            if (e.KeyCode == Keys.F3)
            {
                _controllerTest = !_controllerTest;
                Screen.ShowHelpText($"ControllerTest is now {_controllerTest.ToString().ToLower()}");
            }
            
        }
    }    
}
