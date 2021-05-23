using Hazel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Helpers;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.UI;
using HarryPotter.Classes.WorldItems;
using HarryPotter.Patches;
using hunterlib.Classes;
using InnerNet;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace HarryPotter.Classes
{
    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            Item1 = first;
            Item2 = second;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }
    
    class Main
    {
        public static Main Instance { get; set; }
        public List<ModdedPlayerClass> AllPlayers { get; set; }
        public List<WorldItem> AllItems { get; set; }
        public Config Config { get; set; }
        public CustomRpc Rpc { get; set; }
        public Asset Assets { get; set; }
        public int CurrentStage { get; set; }
        public List<Pair<PlayerControl, string>> PlayersWithRequestedRoles { get; set; }
        public List<Tuple<byte, Vector2>> PossibleItemPositions { get; set; }
        public List<Tuple<byte, Vector2>> DefaultItemPositons { get; } = new List<Tuple<byte, Vector2>>
        {
            new Tuple<byte, Vector2>(2, new Vector2(18.58625f, -21.96028f)),
            new Tuple<byte, Vector2>(2, new Vector2(17.26129f, -19.21864f)),
            new Tuple<byte, Vector2>(2, new Vector2(17.12244f, -17.1841f)),
            new Tuple<byte, Vector2>(2, new Vector2(20.71172f, -21.69597f)),
            new Tuple<byte, Vector2>(2, new Vector2(22.10037f, -25.14912f)),
            new Tuple<byte, Vector2>(2, new Vector2(24.01747f, -24.70225f)),
            new Tuple<byte, Vector2>(2, new Vector2(27.69598f, -20.75172f)),
            new Tuple<byte, Vector2>(2, new Vector2(36.57115f, -21.61535f)),
            new Tuple<byte, Vector2>(2, new Vector2(39.53685f, -10.09214f)),
            new Tuple<byte, Vector2>(2, new Vector2(34.10915f, -10.16738f)),
            new Tuple<byte, Vector2>(2, new Vector2(40.37588f, -7.95609f)),
            new Tuple<byte, Vector2>(2, new Vector2(30.84753f, -7.736305f)),
            new Tuple<byte, Vector2>(2, new Vector2(25.44218f, -7.583571f)),
            new Tuple<byte, Vector2>(2, new Vector2(22.11417f, -8.61549f)),
            new Tuple<byte, Vector2>(2, new Vector2(24.15556f, -3.469771f)),
            new Tuple<byte, Vector2>(2, new Vector2(25.03353f, -12.18799f)),
            new Tuple<byte, Vector2>(2, new Vector2(30.12325f, -16.94627f)),
            new Tuple<byte, Vector2>(2, new Vector2(24.99184f, -17.18198f)),
            new Tuple<byte, Vector2>(2, new Vector2(19.82783f, -14.6528f)),
            new Tuple<byte, Vector2>(2, new Vector2(13.25453f, -12.43384f)),
            new Tuple<byte, Vector2>(2, new Vector2(20.17053f, -11.87447f)),
            new Tuple<byte, Vector2>(2, new Vector2(9.549327f, -9.725276f)),
            new Tuple<byte, Vector2>(2, new Vector2(7.533298f, -12.32464f)),
            new Tuple<byte, Vector2>(2, new Vector2(5.438123f, -11.85916f)),
            new Tuple<byte, Vector2>(2, new Vector2(2.918584f, -11.96187f)),
            new Tuple<byte, Vector2>(2, new Vector2(5.385927f, -16.55273f)),
            new Tuple<byte, Vector2>(2, new Vector2(1.579458f, -17.25811f)),
            new Tuple<byte, Vector2>(2, new Vector2(2.281264f, -24.06277f)),
            new Tuple<byte, Vector2>(2, new Vector2(1.496747f, -20.5509f)),
            new Tuple<byte, Vector2>(2, new Vector2(6.260661f, -24.13944f)),
            new Tuple<byte, Vector2>(2, new Vector2(10.77005f, -20.56692f)),
            new Tuple<byte, Vector2>(2, new Vector2(12.74145f, -23.43479f)),
            new Tuple<byte, Vector2>(2, new Vector2(11.00322f, -17.61416f)),
            new Tuple<byte, Vector2>(2, new Vector2(16.18938f, -24.71703f)),
            new Tuple<byte, Vector2>(2, new Vector2(4.66598f, -4.429569f)),
            new Tuple<byte, Vector2>(2, new Vector2(11.46261f, -7.265319f)),
            new Tuple<byte, Vector2>(2, new Vector2(16.66492f, -2.420291f)),
            new Tuple<byte, Vector2>(0, new Vector2(-4.182233f, 1.07599f)),
            new Tuple<byte, Vector2>(0, new Vector2(-0.7304592f, -2.823687f)),
            new Tuple<byte, Vector2>(0, new Vector2(2.807524f, 0.9642968f)),
            new Tuple<byte, Vector2>(0, new Vector2(-0.930865f, 4.924871f)),
            new Tuple<byte, Vector2>(0, new Vector2(9.511244f, -0.3346088f)),
            new Tuple<byte, Vector2>(0, new Vector2(6.474998f, -3.704194f)),
            new Tuple<byte, Vector2>(0, new Vector2(16.74196f, -4.494635f)),
            new Tuple<byte, Vector2>(0, new Vector2(11.9279f, -6.496367f)),
            new Tuple<byte, Vector2>(0, new Vector2(9.137466f, -12.28679f)),
            new Tuple<byte, Vector2>(0, new Vector2(7.774953f, -14.2921f)),
            new Tuple<byte, Vector2>(0, new Vector2(4.006791f, -15.55052f)),
            new Tuple<byte, Vector2>(0, new Vector2(-0.5833896f, -15.77747f)),
            new Tuple<byte, Vector2>(0, new Vector2(-3.692864f, -14.83466f)),
            new Tuple<byte, Vector2>(0, new Vector2(-9.448769f, -14.59895f)),
            new Tuple<byte, Vector2>(0, new Vector2(-8.734068f, -11.36326f)),
            new Tuple<byte, Vector2>(0, new Vector2(-9.266067f, -8.41872f)),
            new Tuple<byte, Vector2>(0, new Vector2(-12.16772f, -11.86919f)),
            new Tuple<byte, Vector2>(0, new Vector2(-17.45776f, -13.38308f)),
            new Tuple<byte, Vector2>(0, new Vector2(-13.52165f, -5.388005f)),
            new Tuple<byte, Vector2>(0, new Vector2(-20.25068f, -5.388005f)),
            new Tuple<byte, Vector2>(0, new Vector2(-21.6893f, -7.326626f)),
            new Tuple<byte, Vector2>(0, new Vector2(-21.79704f, -3.05468f)),
            new Tuple<byte, Vector2>(0, new Vector2(-16.86277f, -1.031248f)),
            new Tuple<byte, Vector2>(0, new Vector2(-17.02352f, 2.37704f)),
            new Tuple<byte, Vector2>(0, new Vector2(-9.174051f, 0.764464f)),
            new Tuple<byte, Vector2>(0, new Vector2(-9.0562f, -2.353384f)),
            new Tuple<byte, Vector2>(0, new Vector2(-9.0562f, -4.770045f)),
            new Tuple<byte, Vector2>(0, new Vector2(6.228173f, -7.598089f)),
            new Tuple<byte, Vector2>(0, new Vector2(2.062296f, -7.244535f)),
            new Tuple<byte, Vector2>(1, new Vector2(25.60198f, -1.924499f)),
            new Tuple<byte, Vector2>(1, new Vector2(22.01862f, -1.924499f)),
            new Tuple<byte, Vector2>(1, new Vector2(19.5472f, 0.2135701f)),
            new Tuple<byte, Vector2>(1, new Vector2(19.26237f, 4.432643f)),
            new Tuple<byte, Vector2>(1, new Vector2(22.10624f, 2.365375f)),
            new Tuple<byte, Vector2>(1, new Vector2(25.37229f, 4.71474f)),
            new Tuple<byte, Vector2>(1, new Vector2(23.33011f, 6.756915f)),
            new Tuple<byte, Vector2>(1, new Vector2(17.85176f, 11.23526f)),
            new Tuple<byte, Vector2>(1, new Vector2(12.27656f, 6.639064f)),
            new Tuple<byte, Vector2>(1, new Vector2(6.114027f, 0.9975319f)),
            new Tuple<byte, Vector2>(1, new Vector2(9.414569f, 1.145713f)),
            new Tuple<byte, Vector2>(1, new Vector2(6.087941f, 6.03428f)),
            new Tuple<byte, Vector2>(1, new Vector2(2.498136f, 10.72081f)),
            new Tuple<byte, Vector2>(1, new Vector2(6.086607f, 12.19221f)),
            new Tuple<byte, Vector2>(1, new Vector2(8.707928f, 12.92791f)),
            new Tuple<byte, Vector2>(1, new Vector2(15.20495f, 4.131007f)),
            new Tuple<byte, Vector2>(1, new Vector2(14.62f, 0.3439525f)),
            new Tuple<byte, Vector2>(1, new Vector2(12.28571f, -1.344667f)),
            new Tuple<byte, Vector2>(1, new Vector2(8.014729f, -1.344667f)),
            new Tuple<byte, Vector2>(1, new Vector2(-4.464246f, -1.344667f)),
            new Tuple<byte, Vector2>(1, new Vector2(-4.464246f, 2.071996f)),
            new Tuple<byte, Vector2>(1, new Vector2(17.84423f, 17.16991f)),
            new Tuple<byte, Vector2>(1, new Vector2(14.76334f, 20.16747f)),
            new Tuple<byte, Vector2>(1, new Vector2(19.75764f, 20.30775f)),
            new Tuple<byte, Vector2>(1, new Vector2(17.82509f, 22.0234f)),
            new Tuple<byte, Vector2>(1, new Vector2(16.16338f, 24.20611f)),
            new Tuple<byte, Vector2>(4, new Vector2(-0.7782411f, 0.4685913f)),
            new Tuple<byte, Vector2>(4, new Vector2(-7.420381f, 0.4966883f)),
            new Tuple<byte, Vector2>(4, new Vector2(-10.912f, -1.175899f)),
            new Tuple<byte, Vector2>(4, new Vector2(-13.3935f, 1.22228f)),
            new Tuple<byte, Vector2>(4, new Vector2(-17.48869f, -0.9646047f)),
            new Tuple<byte, Vector2>(4, new Vector2(-23.44337f, -1.358598f)),
            new Tuple<byte, Vector2>(4, new Vector2(-14.16418f, -4.727928f)),
            new Tuple<byte, Vector2>(4, new Vector2(-10.07188f, -5.741868f)),
            new Tuple<byte, Vector2>(4, new Vector2(-10.07188f, -7.658527f)),
            new Tuple<byte, Vector2>(4, new Vector2(-14.55895f, -8.608552f)),
            new Tuple<byte, Vector2>(4, new Vector2(-7.496612f, -7.233172f)),
            new Tuple<byte, Vector2>(4, new Vector2(-6.758393f, -11.28371f)),
            new Tuple<byte, Vector2>(4, new Vector2(-2.138819f, -12.1676f)),
            new Tuple<byte, Vector2>(4, new Vector2(1.444511f, -12.33426f)),
            new Tuple<byte, Vector2>(4, new Vector2(7.065756f, -12.64319f)),
            new Tuple<byte, Vector2>(4, new Vector2(-13.58657f, -12.36934f)),
            new Tuple<byte, Vector2>(4, new Vector2(-15.81556f, -12.36934f)),
            new Tuple<byte, Vector2>(4, new Vector2(-13.67749f, -14.22881f)),
            new Tuple<byte, Vector2>(4, new Vector2(10.26638f, -15.4287f)),
            new Tuple<byte, Vector2>(4, new Vector2(10.49246f, -8.517835f)),
            new Tuple<byte, Vector2>(4, new Vector2(10.34679f, -6.517842f)),
            new Tuple<byte, Vector2>(4, new Vector2(13.29307f, -8.638988f)),
            new Tuple<byte, Vector2>(4, new Vector2(16.3192f, -11.1449f)),
            new Tuple<byte, Vector2>(4, new Vector2(16.3192f, -8.728246f)),
            new Tuple<byte, Vector2>(4, new Vector2(16.3192f, -6.394921f)),
            new Tuple<byte, Vector2>(4, new Vector2(12.55694f, -2.757262f)),
            new Tuple<byte, Vector2>(4, new Vector2(19.35949f, -4.450295f)),
            new Tuple<byte, Vector2>(4, new Vector2(22.77165f, -8.549025f)),
            new Tuple<byte, Vector2>(4, new Vector2(24.76921f, -5.884814f)),
            new Tuple<byte, Vector2>(4, new Vector2(29.19261f, -5.933629f)),
            new Tuple<byte, Vector2>(4, new Vector2(32.40412f, -5.814812f)),
            new Tuple<byte, Vector2>(4, new Vector2(32.42162f, -3.183638f)),
            new Tuple<byte, Vector2>(4, new Vector2(33.88465f, -0.9706092f)),
            new Tuple<byte, Vector2>(4, new Vector2(33.85013f, 3.216274f)),
            new Tuple<byte, Vector2>(4, new Vector2(38.45294f, -0.2198919f)),
            new Tuple<byte, Vector2>(4, new Vector2(37.77024f, -3.351321f)),
            new Tuple<byte, Vector2>(4, new Vector2(28.91639f, -1.531849f)),
            new Tuple<byte, Vector2>(4, new Vector2(25.86155f, 0.5424373f)),
            new Tuple<byte, Vector2>(4, new Vector2(24.07702f, 0.09125323f)),
            new Tuple<byte, Vector2>(4, new Vector2(24.05602f, 2.257918f)),
            new Tuple<byte, Vector2>(4, new Vector2(18.72092f, 0.006185418f)),
            new Tuple<byte, Vector2>(4, new Vector2(19.91638f, 6.678555f)),
            new Tuple<byte, Vector2>(4, new Vector2(22.81039f, 8.989212f)),
            new Tuple<byte, Vector2>(4, new Vector2(19.80523f, 11.26535f)),
            new Tuple<byte, Vector2>(4, new Vector2(17.69994f, 9.076731f)),
            new Tuple<byte, Vector2>(4, new Vector2(15.384f, 1.640165f)),
            new Tuple<byte, Vector2>(4, new Vector2(12.27794f, 1.865757f)),
            new Tuple<byte, Vector2>(4, new Vector2(12.31664f, -0.1788689f)),
            new Tuple<byte, Vector2>(4, new Vector2(9.262692f, 1.896087f)),
            new Tuple<byte, Vector2>(4, new Vector2(6.444838f, -2.560592f)),
            new Tuple<byte, Vector2>(4, new Vector2(6.163728f, 2.282847f)),
            new Tuple<byte, Vector2>(4, new Vector2(24.74887f, 7.754529f)),
            new Tuple<byte, Vector2>(4, new Vector2(30.77678f, 7.037311f)),
            new Tuple<byte, Vector2>(4, new Vector2(30.63032f, 5.350429f)),
            new Tuple<byte, Vector2>(4, new Vector2(11.98442f, 8.979587f)),
            new Tuple<byte, Vector2>(4, new Vector2(11.79658f, 6.068962f)),
            new Tuple<byte, Vector2>(4, new Vector2(-0.9279394f, 4.762999f)),
            new Tuple<byte, Vector2>(4, new Vector2(-0.9279395f, 8.512984f)),
            new Tuple<byte, Vector2>(4, new Vector2(-5.042377f, 8.689761f)),
            new Tuple<byte, Vector2>(4, new Vector2(-8.985039f, 12.50144f)),
            new Tuple<byte, Vector2>(4, new Vector2(-12.64849f, 8.643408f)),
            new Tuple<byte, Vector2>(4, new Vector2(-8.8501f, 5.144454f)),
            new Tuple<byte, Vector2>(4, new Vector2(4.531908f, 15.29855f)),
            new Tuple<byte, Vector2>(4, new Vector2(16.36781f, 15.21838f)),
        };

        public Main()
        {
            Config = new Config();
            Rpc = new CustomRpc();
            Assets = new Asset();
            AllPlayers = new List<ModdedPlayerClass>();
            AllItems = new List<WorldItem>();
            PlayersWithRequestedRoles = new List<Pair<PlayerControl, string>>();
        }
        
        public void RpcRequestRole(string roleName)
        {
            if (AmongUsClient.Instance.AmHost)
            {
                if (PlayersWithRequestedRoles.All(x => x.Item1 != PlayerControl.LocalPlayer))
                    PlayersWithRequestedRoles.Add(new Pair<PlayerControl, string>(PlayerControl.LocalPlayer, roleName));
            }
            else
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.RequestRole, SendOption.Reliable);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(roleName);
                writer.EndMessage();
            }
        }
        
        public List<Vector2> GetAllApplicableItemPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (Tuple<byte, Vector2> position in PossibleItemPositions)
                if (ShipStatus.Instance != null && position.Item1 == PlayerControl.GameOptions.MapId)
                    positions.Add(position.Item2);
            return positions;
        }
        
        public ModdedPlayerClass ModdedPlayerById(byte id)
        {
            List<ModdedPlayerClass> matches = AllPlayers.FindAll(player => player._Object.PlayerId == id);
            return matches.FirstOrDefault();
        }

        public IEnumerator CoStunPlayer(PlayerControl player)
        {
            ImportantTextTask durationText = TaskInfoHandler.Instance.AddNewItem(1, "");;
            
            while (true)
            {
                bool isMeeting = MeetingHud.Instance;
                bool isGameEnded = !AmongUsClient.Instance.IsGameStarted;

                if (player.AmOwner)
                {
                    string roleColorHex = TaskInfoHandler.Instance.GetRoleHexColor(player);
                    durationText.Text = $"{roleColorHex}You are stunned until the next meeting.</color></color>";
                    player.myLight.LightRadius = Mathf.Lerp(ShipStatus.Instance.MinLightRadius, ShipStatus.Instance.MaxLightRadius, 0) * PlayerControl.GameOptions.CrewLightMod;
                    player.moveable = false;
                    player.MyPhysics.body.velocity = Vector2.zero;
                }
                
                if (isMeeting || isGameEnded)
                {
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    player.moveable = true;
                    yield break;
                }

                yield return null;
            }
        }

        public IEnumerator CoActivateButterBeer(PlayerControl player)
        {
            DateTime now = DateTime.UtcNow;
            ImportantTextTask durationText = TaskInfoHandler.Instance.AddNewItem(1, "");;
            SetSpeedMultiplier(player.PlayerId, 2f);
            ModdedPlayerById(player.PlayerId).ReverseDirectionalControls = true;
            
            while (true)
            {
                bool isMeeting = MeetingHud.Instance;
                bool isGameEnded = !AmongUsClient.Instance.IsGameStarted;
                bool hasTimeExpired = now.AddSeconds(Config.BeerDuration) < DateTime.UtcNow;
                
                if (player.AmOwner)
                {
                    double remainingTime = Math.Ceiling(Config.HourglassTimer - (float) (DateTime.UtcNow - now).TotalSeconds);
                    string roleColorHex = TaskInfoHandler.Instance.GetRoleHexColor(player);
                    durationText.Text = $"{roleColorHex}You are drunk on Butter Beer! {remainingTime}s remaining</color></color>";
                }

                if (isMeeting || isGameEnded || hasTimeExpired)
                {
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    SetSpeedMultiplier(player.PlayerId, 1f);
                    ModdedPlayerById(player.PlayerId).ReverseDirectionalControls = false;
                    yield break;
                }

                yield return null;
            }
        }

        public IEnumerator CoActivateHourglass(PlayerControl player)
        {
            DateTime now = DateTime.UtcNow;
            Vector2 startPosition = player.transform.position;
            ImportantTextTask durationText = new ImportantTextTask();
            ModdedPlayerById(player.PlayerId).CanSeeAllRolesOveridden = true;
            if (player.AmOwner)
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(player)}Time Turner will activate in {Config.HourglassTimer}s</color></color>");
            while (true)
            {
                if (player.AmOwner)
                {
                    durationText.Text = $"{TaskInfoHandler.Instance.GetRoleHexColor(player)}Time Turner will activate in {Math.Ceiling(Config.HourglassTimer - (float) (DateTime.UtcNow - now).TotalSeconds)}s</color></color>";
                    GetLocalModdedPlayer().Role?.ResetCooldowns();
                }
                
                if (MeetingHud.Instance ||
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.HourglassTimer) < DateTime.UtcNow)
                {
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    
                    if (ModdedPlayerById(player.PlayerId).KilledByCurse)
                        yield break;

                    if (player.Data.IsDead)
                        RpcRevivePlayer(player);

                    if (MeetingHud.Instance)
                        yield break;

                    RpcTeleportPlayer(player, startPosition);
                    ModdedPlayerById(player.PlayerId).CanSeeAllRolesOveridden = false;
                    yield break;
                }

                yield return null;
            }
        }
        
        public void SpawnItem(int id, Vector2 pos, Vector2 vel)
        {
            switch (id)
            {
                case 0:
                    DeluminatorWorld deluminator = new DeluminatorWorld(pos);
                    AllItems.Add(deluminator);
                    break;
                case 1:
                    MaraudersMapWorld map = new MaraudersMapWorld(pos);
                    AllItems.Add(map);
                    break;
                case 2:
                    PortKeyWorld key = new PortKeyWorld(pos);
                    AllItems.Add(key);
                    break;
                case 3:
                    TheGoldenSnitchWorld snitch = new TheGoldenSnitchWorld(pos, vel);
                    AllItems.Add(snitch);
                    break;
                case 4:
                    GhostStoneWorld ghostStone = new GhostStoneWorld(pos);
                    AllItems.Add(ghostStone);
                    break;
                case 5:
                    ButterBeerWorld butterBeer = new ButterBeerWorld(pos);
                    AllItems.Add(butterBeer);
                    break;
                case 6:
                    ElderWandWorld elderWand = new ElderWandWorld(pos);
                    AllItems.Add(elderWand);
                    break;
                case 7:
                    BasWorldItem basItem = new BasWorldItem(pos);
                    AllItems.Add(basItem);
                    break;
                case 8:
                    SortingHatWorld sortingHat = new SortingHatWorld(pos);
                    AllItems.Add(sortingHat);
                    break;
                case 9:
                    PhiloStoneWorld philoStone = new PhiloStoneWorld(pos);
                    AllItems.Add(philoStone);
                    break;
            }
        }

        public void SetNameColor(PlayerControl player, Color color)
        {
            player.nameText.color = color;
            if (HudManager.Instance && HudManager.Instance.Chat)
                foreach (PoolableBehavior bubble in HudManager.Instance.Chat.chatBubPool.activeChildren)
                    if (bubble.Cast<ChatBubble>().NameText.text == player.nameText.text)
                        bubble.Cast<ChatBubble>().NameText.color = color;
            if (MeetingHud.Instance && MeetingHud.Instance.playerStates != null)
                foreach (PlayerVoteArea voteArea in MeetingHud.Instance.playerStates)
                    if (voteArea.TargetPlayerId == player.PlayerId)
                        voteArea.NameText.color = color;
        }

        public void RpcFakeKill(PlayerControl target)
        {
            Coroutines.Start(CoFakeKill(target));
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.FakeKill, SendOption.Reliable);
            writer.Write(target.PlayerId);
            writer.EndMessage();
        }

        public IEnumerator CoFakeKill(PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            GameObject bodyObject = GameObject.Find("body_" + target.PlayerId);

            while (true)
            {
                bool isMeeting = MeetingHud.Instance;
                bool isGameEnded = !AmongUsClient.Instance.IsGameStarted;
                bool hasTimeExpired = now.AddSeconds(4) < DateTime.UtcNow;
                
                target.MyPhysics.body.velocity = Vector2.zero;
                target.moveable = false;
                target.Visible = false;
                target.Collider.enabled = false;

                if (isMeeting || isGameEnded || hasTimeExpired)
                {
                    if (target.AmOwner)
                    {
                        GetLocalModdedPlayer().Inventory.Find(x => x.Id == 9).Delete();
                        PopupTMPHandler.Instance.CreatePopup("You were revived by The Philosopher's Stone!", Color.white, Color.black);
                    }
                    target.moveable = true;
                    target.Visible = true;
                    target.Collider.enabled = true;
                    
                    bodyObject.Destroy();
                    
                    yield break;
                }

                yield return null;
            }
        }
        
        public void GiveGrabbedItem(int id)
        {
            if (!GetLocalModdedPlayer().HasItem(id))
                GetLocalModdedPlayer().GiveItem(id);
            List<WorldItem> allMatches = AllItems.FindAll(x => x.Id == id);
            foreach (WorldItem item in allMatches)
                item.Delete();
        }
        
        public void RpcSpawnItem(int id, Vector2 pos)
        {
            float x = UnityEngine.Random.Range(-1.2f, 1.2f);
            float y = UnityEngine.Random.Range(-1.2f, 1.2f);
            Vector2 velocity = new Vector2(x, y);
            SpawnItem(id, pos, velocity);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.SpawnItem, SendOption.Reliable);
            writer.Write(id);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(velocity.x);
            writer.Write(velocity.y);
            writer.EndMessage();
        }

        public void RpcRevealRole(byte playerId)
        {
            if (!MeetingHud.Instance) return;

            foreach (PlayerVoteArea voteArea in MeetingHud.Instance.playerStates)
            {
                Transform child = voteArea.Buttons.transform.GetChild(voteArea.Buttons.transform.childCount - 1);
                if (child.gameObject.name != "SortButton") child.gameObject.Destroy();
            }
            GetLocalModdedPlayer().Inventory.Find(x => x.Id == 8).Delete();
            RevealRole(playerId);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.RevealRole, SendOption.Reliable);
            writer.Write(playerId);
            writer.EndMessage();
        }
        
        public void RevealRole(byte playerId)
        {
            if (!MeetingHud.Instance) return;

            foreach (PlayerVoteArea voteArea in MeetingHud.Instance.playerStates)
            {
                if (voteArea.TargetPlayerId != playerId) continue;
                ModdedPlayerClass targetPlayer = ModdedPlayerById(playerId);
                
                PopupTMPHandler.Instance.CreatePopup(targetPlayer._Object.Data.PlayerName + " has had their role revealed!", Color.white, Color.black);
                
                string roleHex = TaskInfoHandler.Instance.GetRoleHexColor(targetPlayer._Object);
                string playerName = targetPlayer._Object.Data.PlayerName;
                string roleName = GetPlayerRoleName(targetPlayer);
                voteArea.NameText.text = $"{roleHex}{playerName}\n{roleName}";
                voteArea.NameText.fontSize = 1.8f;
                voteArea.NameText.enableAutoSizing = false;
            }
        }

        public void RpcRevivePlayer(PlayerControl player)
        {
            player.Revive();
            foreach (DeadBody body in UnityEngine.Object.FindObjectsOfType<DeadBody>())
                if (body.ParentId == player.PlayerId)
                    UnityEngine.Object.Destroy(body.gameObject);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId,
                (byte) Packets.RevivePlayer, SendOption.Reliable);
            writer.Write(player.PlayerId);
            writer.EndMessage();
        }

        public void RpcTeleportPlayer(PlayerControl player, Vector2 position)
        {
            player.NetTransform.SnapTo(position);

            MessageWriter posWriter = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId,
                (byte) Packets.TeleportPlayer, SendOption.Reliable);
            posWriter.Write(player.PlayerId);
            posWriter.Write(position.x);
            posWriter.Write(position.y);
            posWriter.EndMessage();
        }
        
        public void UseHourglass(PlayerControl player)
        {
            Coroutines.Start(CoActivateHourglass(player));
        }
        
        public IEnumerator CoDefensiveDuelist(PlayerControl player)
        {
            DateTime now = DateTime.UtcNow;
            Vector3 startingPosition = player.transform.position;
            ModdedPlayerById(player.PlayerId).Immortal = true;
            ImportantTextTask durationText = new ImportantTextTask();
            if (player.AmOwner)
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(player)}Defensive Duelist: {Config.DefensiveDuelistDuration}s remaining</color></color>");
            while (true)
            {
                if (MeetingHud.Instance ||
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.DefensiveDuelistDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(player.PlayerId).ControllerOverride != null)
                {
                    ModdedPlayerById(player.PlayerId).Immortal = false;
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    yield break;
                }
                
                if (player.AmOwner)
                {
                    player.transform.position = startingPosition;
                    player.MyPhysics.body.velocity = new Vector2(0, 0);
                    durationText.Text = $"{TaskInfoHandler.Instance.GetRoleHexColor(player)}Defensive Duelist: {Math.Ceiling(Config.DefensiveDuelistDuration - (float) (DateTime.UtcNow - now).TotalSeconds)}s remaining</color></color>";
                    GetLocalModdedPlayer().Role?.ResetCooldowns();
                }

                yield return null;
            }
        }

        public void DefensiveDuelist(PlayerControl player)
        {
            Coroutines.Start(CoDefensiveDuelist(player));
        }
        
        public void RpcDefensiveDuelist(PlayerControl player)
        {
            DefensiveDuelist(player);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DefensiveDuelist, SendOption.Reliable);
            writer.Write(player.PlayerId);
            writer.EndMessage();
        }

        public IEnumerator CoInvisPlayer(PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            ImportantTextTask durationText = new ImportantTextTask();
            if (target.AmOwner)
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(target)}Invisibility Cloak: {Config.InvisCloakDuration}s remaining</color></color>");
            
            while (true)
            {
                if (target.AmOwner)
                {
                    durationText.Text = $"{TaskInfoHandler.Instance.GetRoleHexColor(target)}Invisibility Cloak: {Math.Ceiling(Config.InvisCloakDuration - (float) (DateTime.UtcNow - now).TotalSeconds)}s remaining</color></color>";
                    GetLocalModdedPlayer().Role?.ResetCooldowns();

                    target.Visible = true;

                    target.myRend.color = new Color(1f, 1f, 1f, 100f / 255f);
                    target.HatRenderer.color = new Color(1f, 1f, 1f, 100f / 255f);
                    target.MyPhysics.Skin.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 100f / 255f);
                    target.CurrentPet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 100f / 255f);
                }
                else
                {
                    target.Visible = false;

                    if (target.CurrentPet)
                        target.CurrentPet.Visible = false;
                }
                
                if (MeetingHud.Instance || 
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.InvisCloakDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(target.PlayerId).ControllerOverride != null)
                {
                    target.myRend.color = Color.white;
                    target.HatRenderer.color = Color.white;
                    target.MyPhysics.Skin.GetComponent<SpriteRenderer>().color = Color.white;

                    if (target.CurrentPet)
                    {
                        target.CurrentPet.GetComponent<SpriteRenderer>().color = Color.white;
                        target.CurrentPet.Visible = true;
                    }

                    target.Visible = true;
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    yield break;
                }

                yield return null;
            }
        }

        public IEnumerator CoBlindPlayer(PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            float num = 0f;
            target.MyPhysics.body.velocity = new Vector2(0, 0);
            ImportantTextTask durationText = new ImportantTextTask();
            if (target.AmOwner)
            {
                PopupTMPHandler.Instance.CreatePopup("You were blinded and frozen by a spell!", Color.white, Color.black);
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(target)}You are blinded and frozen! {Config.CrucioDuration}s remaining</color></color>");
            }
            while (true)
            {
                if (target.AmOwner)
                {
                    durationText.Text = $"{TaskInfoHandler.Instance.GetRoleHexColor(target)}You are blinded and frozen! {Math.Ceiling(Config.CrucioDuration - (float) (DateTime.UtcNow - now).TotalSeconds)}s remaining</color></color>";
                    target.myLight.LightRadius = Mathf.Lerp(ShipStatus.Instance.MinLightRadius, ShipStatus.Instance.MaxLightRadius, num) * PlayerControl.GameOptions.CrewLightMod;
                    target.moveable = false;
                }
                if (MeetingHud.Instance || 
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.CrucioDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(target.PlayerId).ControllerOverride != null)
                {
                    target.moveable = true;
                    TaskInfoHandler.Instance.RemoveItem(durationText);
                    yield break;
                }

                yield return null;
            }
        }

        public bool ControlKillUsed;

        public IEnumerator CoControlPlayer(PlayerControl controller, PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            ControlKillUsed = false;

            Instance.ModdedPlayerById(target.PlayerId).ControllerOverride =
                Instance.ModdedPlayerById(controller.PlayerId);

            ((Bellatrix) Instance.ModdedPlayerById(controller.PlayerId).Role).MindControlledPlayer =
                Instance.ModdedPlayerById(target.PlayerId);

            ImportantTextTask durationText = null;

            if (controller.AmOwner)
            {
                target.MyPhysics.body.interpolation = RigidbodyInterpolation2D.Interpolate;
                Camera.main.GetComponent<FollowerCamera>().Target = target;
                Camera.main.GetComponent<FollowerCamera>().shakeAmount = 0;
                durationText = TaskInfoHandler.Instance.AddNewItem(1,
                    $"{TaskInfoHandler.Instance.GetRoleHexColor(controller)}You are mind-controlling \"{target.Data.PlayerName}\"! {Config.ImperioDuration}s remaining</color></color>");
            }

            if (target.AmOwner)
                PopupTMPHandler.Instance.CreatePopup("You are being mind-controlled!", Color.white, Color.black);

            target.moveable = true;
            controller.moveable = true;

            if (target.AmOwner || controller.AmOwner)
                PlayerControl.LocalPlayer.MyPhysics.body.velocity = new Vector2(0, 0);

            while (true)
            {
                if (target.Data.IsDead ||
                    MeetingHud.Instance ||
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.ImperioDuration) < DateTime.UtcNow)
                {
                    if (target.AmOwner)
                    {
                        target.moveable = true;
                    }
                    else if (controller.AmOwner)
                    {
                        target.MyPhysics.body.interpolation = RigidbodyInterpolation2D.None;
                        TaskInfoHandler.Instance.RemoveItem(durationText);
                        controller.moveable = true;
                        Camera.main.GetComponent<FollowerCamera>().Target = controller;
                        controller.myLight.transform.position = controller.transform.position;
                        PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
                    }

                    Instance.ModdedPlayerById(target.PlayerId).ControllerOverride = null;
                    ((Bellatrix) Instance.ModdedPlayerById(controller.PlayerId).Role).MindControlledPlayer = null;
                    yield break;
                }

                if (controller.AmOwner || target.AmOwner)
                {
                    if (Minigame.Instance)
                        Minigame.Instance.Close();
                    PlayerControl.LocalPlayer.moveable = false;
                    if (controller.AmOwner)
                    {
                        durationText.Text =
                            $"{TaskInfoHandler.Instance.GetRoleHexColor(controller)}You are mind-controlling \"{target.Data.PlayerName}\"! {Math.Ceiling(Config.ImperioDuration - (float) (DateTime.UtcNow - now).TotalSeconds)}s remaining</color></color>";
                        controller.myLight.transform.position = target.transform.position;
                        HudManager.Instance.KillButton.SetCoolDown(0f, PlayerControl.GameOptions.KillCooldown);

                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            if (target.FindClosestTarget() != null && !ControlKillUsed)
                            {
                                ControlKillUsed = true;
                                RpcKillPlayer(target, target.FindClosestTarget(), true);
                            }
                        }

                        if (ControlKillUsed)
                            HudManager.Instance.KillButton.SetTarget(null);
                        else
                            HudManager.Instance.KillButton.SetTarget(target.FindClosestTarget());
                    }
                }

                yield return null;
            }
        }

        public void SetSpeedMultiplier(byte playerId, float newSpeed)
        {
            ModdedPlayerById(playerId).SpeedMultiplier = newSpeed;
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.UpdateSpeedMultiplier, SendOption.Reliable);
            writer.Write(playerId);
            writer.Write(newSpeed);
            writer.EndMessage();
        }
        
        public List<TextMeshPro> CustomOptions { get; set; }
                
        public string GetOptionTextByName(string name)
        {
            foreach (CustomNumberOption numberOption in CustomNumberOption.AllNumberOptions)
                if (numberOption.Name == name) return $"{numberOption.Name}: {numberOption.Value}";

            foreach (CustomToggleOption toggleOption in CustomToggleOption.AllToggleOptions)
                if (toggleOption.Name == name) return $"{toggleOption.Name}: {(toggleOption.Value ? "On" : "Off")}";

            return "no option text found (ERR)";
        }

        public void ResetCustomOptions()
        {
            if (CustomOptions != null)
            {
                foreach (TextMeshPro meshPro in CustomOptions)
                {
                    meshPro.gameObject.Destroy();
                }
            }
            
            CustomOptions = new List<TextMeshPro>();
            
            foreach (CustomNumberOption numberOption in CustomNumberOption.AllNumberOptions)
            {
                string optionString = $"{numberOption.Name}: {numberOption.Value}";
                
                GameObject lobbyTextObj = new GameObject(numberOption.Name).DontDestroy();
                lobbyTextObj.layer = 5;
                
                Tooltip lobbyTextTooltip = lobbyTextObj.AddComponent<Tooltip>();
                lobbyTextTooltip.TooltipText = GetTooltipByOptionName(numberOption.Name);

                TextMeshPro lobbyTextMesh = lobbyTextObj.AddComponent<TextMeshPro>();
                lobbyTextMesh.fontSize = 1.6f;
                lobbyTextMesh.alignment = TextAlignmentOptions.BottomLeft;
                lobbyTextMesh.overflowMode = TextOverflowModes.Overflow;
                lobbyTextMesh.maskable = false;
                lobbyTextMesh.fontMaterial = Assets.GenericOutlineMat;
                lobbyTextMesh.fontMaterial.SetFloat("_UnderlayDilate", 0.75f);
                
                RectTransform lobbyTextTrans = lobbyTextObj.GetComponent<RectTransform>();
                lobbyTextTrans.sizeDelta = lobbyTextMesh.GetPreferredValues(optionString);

                BoxCollider2D lobbyTextCollider = lobbyTextObj.AddComponent<BoxCollider2D>();
                lobbyTextCollider.size = lobbyTextTrans.sizeDelta;
                
                lobbyTextMesh.text = optionString;
                
                CustomOptions.Add(lobbyTextMesh);
            }
            
            foreach (CustomToggleOption toggleOption in CustomToggleOption.AllToggleOptions)
            {
                string optionString = $"{toggleOption.Name}: {(toggleOption.Value ? "On" : "Off")}";
                
                GameObject lobbyTextObj = new GameObject(toggleOption.Name).DontDestroy();
                lobbyTextObj.layer = 5;

                Tooltip lobbyTextTooltip = lobbyTextObj.AddComponent<Tooltip>();
                lobbyTextTooltip.TooltipText = GetTooltipByOptionName(toggleOption.Name);
                
                TextMeshPro lobbyTextMesh = lobbyTextObj.AddComponent<TextMeshPro>();
                lobbyTextMesh.fontSize = 1.6f;
                lobbyTextMesh.alignment = TextAlignmentOptions.BottomLeft;
                lobbyTextMesh.overflowMode = TextOverflowModes.Overflow;
                lobbyTextMesh.maskable = false;
                lobbyTextMesh.fontMaterial = Assets.GenericOutlineMat;
                lobbyTextMesh.fontMaterial.SetFloat("_UnderlayDilate", 0.75f);

                RectTransform lobbyTextTrans = lobbyTextObj.GetComponent<RectTransform>();
                lobbyTextTrans.sizeDelta = lobbyTextMesh.GetPreferredValues(optionString);

                BoxCollider2D lobbyTextCollider = lobbyTextObj.AddComponent<BoxCollider2D>();
                lobbyTextCollider.size = lobbyTextTrans.sizeDelta;

                lobbyTextMesh.text = optionString;
                
                CustomOptions.Add(lobbyTextMesh);
            }
        }
        
        public void ControlPlayer(PlayerControl controller, PlayerControl target)
        {
            target.MyPhysics.body.velocity = Vector2.zero;
            controller.MyPhysics.body.velocity = Vector2.zero;
            KillAnimation.SetMovement(controller, false);
            KillAnimation.SetMovement(controller, true);
            KillAnimation.SetMovement(target, false);
            KillAnimation.SetMovement(target, true);
            Coroutines.Start(CoControlPlayer(controller, target));
        }
        
        public void RpcControlPlayer(PlayerControl controller, PlayerControl target)
        {
            ControlPlayer(controller, target);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.StartControlling, SendOption.Reliable);
            writer.Write(controller.PlayerId);
            writer.Write(target.PlayerId);
            writer.EndMessage();
        }
        
        public void InvisPlayer(PlayerControl target)
        {
            Coroutines.Start(CoInvisPlayer(target));
        }
        
        public void RpcInvisPlayer(PlayerControl target)
        {
            InvisPlayer(target);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.InvisPlayer, SendOption.Reliable);
            writer.Write(target.PlayerId);
            writer.EndMessage();
        }
        
        public void CrucioBlind(PlayerControl target)
        {
            System.Console.WriteLine("Blinding " + target.Data.PlayerName);
            hunterlib.Classes.Coroutines.Start(CoBlindPlayer(target));
        }

        public void RpcCrucioBlind(PlayerControl target)
        {
            CrucioBlind(target);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DeactivatePlayer, SendOption.Reliable);
            writer.Write(target.PlayerId);
            writer.EndMessage();
        }
        
        public void RpcKillPlayer(PlayerControl killer, PlayerControl target, bool isCurseKill = true, bool forceShowAnim = false)
        {
            KillPlayer(killer, target, isCurseKill, forceShowAnim);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.KillPlayerUnsafe, SendOption.Reliable);
            writer.Write(killer.PlayerId);
            writer.Write(target.PlayerId);
            writer.Write(isCurseKill);
            writer.Write(forceShowAnim);
            writer.EndMessage();
        }
        
        public void KillPlayer(PlayerControl killer, PlayerControl target, bool isCurseKill, bool forceShowAnim)
        {
            if (MeetingHud.Instance || AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
                return;

            if (!target.Collider.enabled)
                return;
            
            if (ModdedPlayerById(target.PlayerId).Immortal)
            {
                if (killer.AmOwner)
                {
                    PopupTMPHandler.Instance.CreatePopup("When using his ability, Ron cannot be killed.\nYour cooldowns were reset.", Color.white, Color.black);
                    killer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
                    ModdedPlayerById(killer.PlayerId).Role?.ResetCooldowns();
                }
                
                return;
            }

            if (target.AmOwner || killer.AmOwner || ModdedPlayerById(killer.PlayerId).ControllerOverride?._Object.AmOwner == true)
            {  
                System.Console.WriteLine("Trying to play the kill sound!");
                SoundManager.Instance.StopSound(PlayerControl.LocalPlayer.KillSfx);
                SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.8f);
            }

            if (!isCurseKill)
                killer.MyPhysics.body.transform.position = target.transform.position;
            else
            {
                ModdedPlayerById(target.PlayerId).KilledByCurse = true;
                if (target.AmOwner && killer.AmOwner)
                {
                    PopupTMPHandler.Instance.CreatePopup("You tried to kill Harry with the Killing Curse!\nBecause of Harry's passive ability, you are dead.", Color.white, Color.black, 3f);
                    HudManager.Instance.KillOverlay.ShowOne(killer.Data, killer.Data);
                }
                else if (target.AmOwner)
                {
                    PopupTMPHandler.Instance.CreatePopup("You were hit by a spell!", Color.white, Color.black);
                }
            }
            
            DeadBody deadBody = DeadBody.Instantiate(target.KillAnimations[0].bodyPrefab);
            deadBody.gameObject.name = "body_" + target.PlayerId;
            Vector3 vector = target.transform.position + target.KillAnimations[0].BodyOffset;
            vector.z = vector.y / 1000f;
            deadBody.transform.position = vector;
            deadBody.ParentId = target.PlayerId;
            target.SetPlayerMaterialColors(deadBody.GetComponent<Renderer>());

            if (target.AmOwner)
            {
                if (forceShowAnim)
                    HudManager.Instance.KillOverlay.ShowOne(killer.Data, target.Data);
                
                if (ModdedPlayerById(target.PlayerId).ShouldRevive)
                    RpcFakeKill(target);
                else
                {
                    PlayerDie(target);
                    
                    MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.FinallyDie, SendOption.Reliable);
                    writer.Write(target.PlayerId);
                    writer.EndMessage();
                }
            }
        }

        public void PlayerDie(PlayerControl target)
        {
            if (target.AmOwner)
            {
                HudManager.Instance.Chat.SetVisible(true);
                HudManager.Instance.ShadowQuad.gameObject.SetActive(false);
                AmongUsClient.Instance.gameObject.layer = LayerMask.NameToLayer("Ghost");
            }
            
            if (target.CurrentPet)
                target.CurrentPet.SetMourning();

            target.Data.IsDead = true;
            target.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
            target.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }

        public void RpcForceAllVotes(sbyte playerId)
        {
            GetLocalModdedPlayer().Inventory.Find(x => x.Id == 3).Delete();
            ForceAllVotes(playerId);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.ForceAllVotes, SendOption.Reliable);
            writer.Write(playerId);
            writer.EndMessage();
        }

        public void ForceAllVotes(sbyte playerId)
        {
            PopupTMPHandler.Instance.CreatePopup($"All votes were forced onto a specific player!", Color.white, Color.black);
            
            foreach (PlayerVoteArea playerVoteArea in MeetingHud.Instance.playerStates)
            {
                if (AmongUsClient.Instance.AmHost)
                {
                    SoundManager.Instance.PlaySound(MeetingHud.Instance.VoteLockinSound, false, 1f);
                    
                    playerVoteArea.didVote = true;
                    playerVoteArea.votedFor = playerId;
                    playerVoteArea.Flag.enabled = true;

                    MeetingHud.Instance.SetDirtyBit(
                        1U << MeetingHud.Instance.playerStates.IndexOf(playerVoteArea));
                    MeetingHud.Instance.CheckForEndVoting();
                }

                if (playerVoteArea.TargetPlayerId == playerId)
                {
                    PassiveButton confirmButton = playerVoteArea.Buttons.GetComponentsInChildren<PassiveButton>()
                        .FirstOrDefault();
                    GameObject snitchIco = new GameObject();
                    SpriteRenderer snitchIcoR = snitchIco.AddComponent<SpriteRenderer>();
                    snitchIco.layer = 5;
                    snitchIcoR.sprite = Assets.SmallSnitchSprite;
                    snitchIco.transform.SetParent(playerVoteArea.transform);
                    snitchIco.transform.localPosition = new Vector3(2.8f, 0.01f);
                    snitchIco.transform.localScale = confirmButton.transform.localScale;
                }
                
                playerVoteArea.ClearButtons();
                playerVoteArea.voteComplete = true;
            }
            MeetingHud.Instance.SkipVoteButton.ClearButtons();
            MeetingHud.Instance.SkipVoteButton.voteComplete = true;
            MeetingHud.Instance.SkipVoteButton.gameObject.SetActive(false);
        }

        public bool IsLightsSabotaged()
        {
            foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                if (task.TaskType == TaskTypes.FixLights)
                    return true;
            return false;
        }

        public void CreateCrucio(Vector2 pos, ModdedPlayerClass owner)
        {
            GameObject crucioObject = new GameObject();
            crucioObject.name = "_crucio";
            
            Spell crucio = crucioObject.AddComponent<Spell>();
            crucio.Owner = owner;
            crucio.MousePostition = pos;
            crucio.SpellSprites = Assets.CrucioSprite.ToArray();
            crucio.OnHit += (spell, player) =>
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DestroyCrucio, SendOption.Reliable);
                writer.EndMessage();
                
                crucioObject.Destroy();
                
                if (player == null) return;
                if (( (Bellatrix) owner.Role ).MarkedPlayers.All(x => x.PlayerId != player.PlayerId)) return;
                ModdedPlayerClass moddedPlayer = ModdedPlayerById(player.PlayerId);
                if (moddedPlayer.Immortal) return;
                RpcCrucioBlind(player);
            };
        }
        
        public void RpcCreateCrucio(Vector2 pos, ModdedPlayerClass owner)
        {
            CreateCrucio(pos, owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCrucio, SendOption.Reliable);
            writer.Write(owner._Object.PlayerId);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.EndMessage();
        }

        public void CreateCurse(Vector2 pos, ModdedPlayerClass owner)
        {
            GameObject curseObject = new GameObject();
            curseObject.name = "_curse";
            
            Spell curse = curseObject.AddComponent<Spell>();
            curse.Owner = owner;
            curse.MousePostition = pos;
            curse.SpellSprites = Assets.CurseSprite.ToArray();
            curse.OnHit += (spell, player) =>
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DestroyCurse, SendOption.Reliable);
                writer.EndMessage();
                
                curseObject.Destroy();
                
                if (player == null) return;
                if (ModdedPlayerById(player.PlayerId).Immortal) return;

                if (GetPlayerRoleName(ModdedPlayerById(player.PlayerId)) == "Harry")
                {
                    RpcKillPlayer(owner._Object, owner._Object);
                    return;
                }
                
                RpcKillPlayer(owner._Object, player);
            };
        }
        
        public void RpcCreateCurse(Vector2 pos, ModdedPlayerClass owner)
        {
            CreateCurse(pos, owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCurse, SendOption.Reliable);
            writer.Write(owner._Object.PlayerId);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.EndMessage();
        }

        public void DestroySpell(string name)
        {
            GameObject.Find(name)?.Destroy();
        }

        public void RpcAssignRole(ModdedPlayerClass player, Role role)
        {
            AssignRole(player, role);
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.AssignRole, SendOption.Reliable);
            writer.Write(player._Object.PlayerId);
            writer.Write(role.RoleName);
            writer.EndMessage();
        }

        public void AssignRole(ModdedPlayerClass player, Role role)
        {
            player.Role = role;
        }
        
        public ModdedPlayerClass GetLocalModdedPlayer()
        {
            List<ModdedPlayerClass> matches = AllPlayers.FindAll(player => player._Object == PlayerControl.LocalPlayer);
            return matches.FirstOrDefault();
        }
        
        public ModdedPlayerClass FindPlayerOfRole(string roleName)
        {
            List<ModdedPlayerClass> matches = AllPlayers.FindAll(player => player.Role != null && player.Role.RoleName == roleName);
            return matches.FirstOrDefault();
        }

        public bool IsPlayerRole(ModdedPlayerClass player, string roleName)
        {
            if (player?.Role?.RoleName == roleName)
                return true;
            return false;
        }

        public string GetPlayerRoleName(ModdedPlayerClass player)
        {
            if (player == null) return "Null";
            if (player.Role == null) return player._Object.Data.IsImpostor ? "Impostor" : "Muggle";
            return player.Role.RoleName;
        }

        public PlayerControl GetClosestTarget(PlayerControl player, bool excludeImp, PlayerControl[] exclusions = null)
        {
            PlayerControl result = null;
            float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
            if (!ShipStatus.Instance)
            {
                return null;
            }
            Vector2 truePosition = player.GetTruePosition();
            List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers.ToArray().ToList();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                GameData.PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.Disconnected && playerInfo.PlayerId != player.PlayerId && !playerInfo.IsDead && (!playerInfo.IsImpostor || !excludeImp) && (exclusions == null || !exclusions.Any(x => x.PlayerId == playerInfo.PlayerId)))
                {
                    PlayerControl @object = playerInfo.Object;
                    if (@object && @object.Collider.enabled)
                    {
                        Vector2 vector = @object.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                        {
                            result = @object;
                            num = magnitude;
                        }
                    }
                }
            }
            return result;
        }

        public string GetTooltipByOptionName(string name)
        {
            switch (name)
            {
                case "Order of the Impostors":
                    return "If 'On', when Harry, Ron, and Hermione are dead, Impostors will win";
                case "Can Spells be Used In Vents":
                    return "When 'On', spells can be casted from inside vents";
                case "Show Info Popups/Tooltips":
                    return "When 'On', informational popups/tooltips will be shown";
                case "Defensive Duelist Cooldown":
                    return "The cooldown for Ron's main ability";
                case "Invisibility Cloak Cooldown":
                    return "The cooldown for Harry's main ability";
                case "Time Turner Cooldown":
                    return "The cooldown for Hermione's main ability";
                case "Crucio Cooldown":
                    return "The cooldown for Bellatrix's secondary ability";
                case "Shared Voldemort Cooldowns":
                    return "When 'On', the Kill button and the Curse button will share a cooldown";
            }
            return "no tooltip supplied. (error code: the dev is brain-dead)";
        }
    }
}