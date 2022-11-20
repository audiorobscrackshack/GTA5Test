using GTA;
using GTA.Math;
using GTA.Native;
using static GTA.Native.NativeInvoker;
using static GTA.Script;
using Screen = GTA.UI.Screen;
using Notification = GTA.UI.Notification;

namespace GTA5Test
{
    public class CustomCheats
    {
        public static void Tick()
        {
            Ped pPed = Game.Player.Character;
            Vehicle pVeh = pPed.CurrentVehicle;
            var global32209 = GlobalVariable.Get(32209).Read<int>();


            if (Game.WasCheatStringJustEntered("deluxo"))
            {
                if (global32209 == 0)
                    GlobalVariable.Get(32209).Write((int)CheatType.Vehicle);
                Extra.ShowCheatActivatedNotificationWithSubString("Spawn Deluxo");
                Vector3 carSpawnPos = GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(PLAYER_PED_ID(), 0f, 3.5f, 1f);
                Vehicle vehicle = World.CreateVehicle(VehicleHash.Deluxo, carSpawnPos, pPed.Heading - 270f);
                SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX(vehicle, 2); //yellow/blue
                SET_VEHICLE_NUMBER_PLATE_TEXT(vehicle, "TIMEOUT"); // lore-friendly bttf plate
            }

            if (Game.WasCheatStringJustEntered("suicide"))
            {
                if (global32209 == 0)
                    GlobalVariable.Get(32209).Write((int)CheatType.Painkiller); // This is dumb. But meh, it works.
                Extra.ShowCheatActivatedNotificationWithSubString("Commit suicide.");
                Wait(1250);
                pPed.Kill();
            }
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