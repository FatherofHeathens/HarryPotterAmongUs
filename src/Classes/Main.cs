using Hazel;
using Reactor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.WorldItems;
using InnerNet;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace HarryPotter.Classes
{
    class Main
    {
        public static Main Instance { get; set; }
        public List<ModdedPlayerClass> AllPlayers { get; set; }
        public List<WorldItem> AllItems { get; set; }
        public Config Config { get; set; }
        public CustomRpc Rpc { get; set; }
        public Asset Assets { get; set; }
        public int CurrentStage { get; set; }
        private GameObject CurseObject { get; set; }
        private GameObject CrucioObject { get; set; }
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
        
        public System.Collections.IEnumerator CoActivateHourglass(PlayerControl player)
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
        
        public void SpawnItem(int id, Vector2 pos, Vector2? vel = null)
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
                    TheGoldenSnitchWorld snitch = new TheGoldenSnitchWorld(pos, vel.Value);
                    AllItems.Add(snitch);
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
            if (id == 3)
                SpawnItem(id, pos, velocity);
            else
                SpawnItem(id, pos);
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.SpawnItem, SendOption.Reliable);
            writer.Write(id);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.Write(velocity.x);
            writer.Write(velocity.y);
            writer.EndMessage();
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
            Reactor.Coroutines.Start(CoActivateHourglass(player));
        }
        
        public System.Collections.IEnumerator CoDefensiveDuelist(PlayerControl player)
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
            Reactor.Coroutines.Start(CoDefensiveDuelist(player));
        }
        
        public void RpcDefensiveDuelist(PlayerControl player)
        {
            DefensiveDuelist(player);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DefensiveDuelist, SendOption.Reliable);
            writer.Write(player.PlayerId);
            writer.EndMessage();
        }

        public System.Collections.IEnumerator CoInvisPlayer(PlayerControl target)
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

        public System.Collections.IEnumerator CoBlindPlayer(PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            float num = 0f;
            target.MyPhysics.body.velocity = new Vector2(0, 0);
            ImportantTextTask durationText = new ImportantTextTask();
            if (target.AmOwner)
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(target)}You are blinded and frozen! {Config.CrucioDuration}s remaining</color></color>");
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
        public System.Collections.IEnumerator CoControlPlayer(PlayerControl controller, PlayerControl target)
        {
            DateTime now = DateTime.UtcNow;
            ControlKillUsed = false;
            
            Instance.ModdedPlayerById(target.PlayerId).ControllerOverride =
                Instance.ModdedPlayerById(controller.PlayerId);
            
            ((Bellatrix) Instance.ModdedPlayerById(controller.PlayerId).Role).MindControlledPlayer =
                Instance.ModdedPlayerById(target.PlayerId);

            ImportantTextTask durationText = new ImportantTextTask();
            if (controller.AmOwner)
            {
                Camera.main.GetComponent<FollowerCamera>().Target = target;
                Camera.main.GetComponent<FollowerCamera>().shakeAmount = 0;
                durationText = TaskInfoHandler.Instance.AddNewItem(1, $"{TaskInfoHandler.Instance.GetRoleHexColor(controller)}You are mind-controlling \"{target.Data.PlayerName}\"! {Config.ImperioDuration}s remaining</color></color>");
            }

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
                        target.moveable = true;
                    else if (controller.AmOwner)
                    {
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
                        if (ControlKillUsed)
                            HudManager.Instance.KillButton.SetTarget(null);
                        else
                            HudManager.Instance.KillButton.SetTarget(target.FindClosestTarget());
                    }
                }

                yield return null;
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
            Reactor.Coroutines.Start(CoControlPlayer(controller, target));
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
            Reactor.Coroutines.Start(CoInvisPlayer(target));
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
            Reactor.Coroutines.Start(CoBlindPlayer(target));
        }

        public void RpcCrucioBlind(PlayerControl target)
        {
            CrucioBlind(target);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DeactivatePlayer, SendOption.Reliable);
            writer.Write(target.PlayerId);
            writer.EndMessage();
        }

        public void RpcKillPlayer(PlayerControl killer, PlayerControl target, bool isCurseKill = true)
        {
            KillPlayer(killer, target, isCurseKill);
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.KillPlayerUnsafe, SendOption.Reliable);
            writer.Write(killer.PlayerId);
            writer.Write(target.PlayerId);
            writer.Write(isCurseKill);
            writer.EndMessage();
        }
        
        public void KillPlayer(PlayerControl killer, PlayerControl target, bool isCurseKill)
        {
            if (MeetingHud.Instance || AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
                return;

            if (ModdedPlayerById(target.PlayerId).Immortal)
                return;

            if (target.AmOwner || killer.AmOwner || ModdedPlayerById(killer.PlayerId).ControllerOverride?._Object.AmOwner == true)
            {  
                System.Console.WriteLine("Trying to play the kill sound!");
                SoundManager.Instance.StopSound(PlayerControl.LocalPlayer.KillSfx);
                SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.8f);
            }

            if (target.AmOwner)
            {
                HudManager.Instance.ShadowQuad.gameObject.SetActive(false);
                AmongUsClient.Instance.gameObject.layer = LayerMask.NameToLayer("Ghost");
            }
            
            target.Data.IsDead = true;
            target.nameText.GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
            target.gameObject.layer = LayerMask.NameToLayer("Ghost");

            if (!isCurseKill)
                killer.MyPhysics.body.transform.position = target.transform.position;
            else
            {
                ModdedPlayerById(target.PlayerId).KilledByCurse = true;
                if (target.AmOwner && killer.AmOwner)
                    HudManager.Instance.KillOverlay.ShowOne(killer.Data, killer.Data);
            }

            DeadBody deadBody = DeadBody.Instantiate(target.KillAnimations[0].bodyPrefab);
            Vector3 vector = target.transform.position + target.KillAnimations[0].BodyOffset;
            vector.z = vector.y / 1000f;
            deadBody.transform.position = vector;
            deadBody.ParentId = target.PlayerId;
            target.SetPlayerMaterialColors(deadBody.GetComponent<Renderer>());
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
            foreach (PlayerVoteArea playerVoteArea in MeetingHud.Instance.playerStates)
            {
                if (AmongUsClient.Instance.AmHost)
                {
                    playerVoteArea.didVote = false;
                    if (playerVoteArea.isDead || playerVoteArea.didVote)
                        continue;
                    
                    if (PlayerControl.LocalPlayer.PlayerId == playerVoteArea.TargetPlayerId ||
                        AmongUsClient.Instance.GameMode != GameModes.LocalGame)
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
                    snitchIcoR.sprite = Main.Instance.Assets.SmallSnitchSprite;
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

        public System.Collections.IEnumerator CoCastCrucio(Vector3 mousePosition, ModdedPlayerClass Owner)
        {
            DateTime now = DateTime.UtcNow;
            int crucioIndex = 0;
            
            CrucioObject?.Destroy();
            CrucioObject = new GameObject();
            SpriteRenderer crucioRender = CrucioObject.AddComponent<SpriteRenderer>();
            Rigidbody2D crucioRigid = CrucioObject.AddComponent<Rigidbody2D>();
            CrucioObject.SetActive(true);
            BoxCollider2D crucioCollider = CrucioObject.AddComponent<BoxCollider2D>();
            crucioRender.enabled = true;
            crucioRigid.transform.position = Owner._Object.myRend.bounds.center;
            crucioRender.transform.localScale = new Vector2(1f, 1f);

            Vector3 v = mousePosition - Owner._Object.myRend.bounds.center;
            float dist = Vector2.Distance(mousePosition, Owner._Object.myRend.bounds.center);
            Vector3 d = v * 3f * (2f / dist);
            float AngleRad = Mathf.Atan2(mousePosition.y - Owner._Object.myRend.bounds.center.y, mousePosition.x - Owner._Object.myRend.bounds.center.x);
            float AngleDeg = (180 / (float)Math.PI) * AngleRad;

            crucioCollider.autoTiling = false;
            crucioCollider.edgeRadius = 0;
            crucioCollider.size = Owner._Object.Collider.bounds.size * 2;
            crucioRigid.velocity = new Vector2(d.x, d.y);
            CrucioObject.layer = 8;

            while (true)
            {
                if (CrucioObject == null)
                    yield break;
                
                if (crucioIndex <= 5)
                    crucioRender.sprite = Assets.CrucioSprite[0];
                else
                    crucioRender.sprite = Assets.CrucioSprite[1];

                if (crucioIndex >= 10)
                    crucioIndex = 0;

                crucioIndex++;
                
                crucioRigid.rotation = AngleDeg;
                crucioRigid.drag = 0;
                crucioRigid.angularDrag = 0;
                crucioRigid.inertia = 0;
                crucioRigid.gravityScale = 0;

                Vector2 oldVelocity = crucioRigid.velocity;
                
                yield return null;
                
                if (CrucioObject == null)
                    yield break;

                if (crucioRigid.velocity != oldVelocity && Owner._Object.AmOwner)
                {
                    RpcDestroyCrucio();
                    yield break;
                }
                
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.IsDead || player.Data.Disconnected || Owner._Object == player || player.Data.IsImpostor)
                        continue;

                    if (!player.myRend.bounds.Intersects(crucioRender.bounds))
                        continue;

                    if (!Owner._Object.AmOwner)
                        yield break;
                    
                    RpcDestroyCrucio();
                    
                    if (ModdedPlayerById(player.PlayerId).Immortal)
                        yield break;
                    
                    RpcCrucioBlind(player);
                    yield break;
                }

                if (now.AddSeconds(5) >= DateTime.UtcNow && !MeetingHud.Instance && AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
                    continue;
                
                RpcDestroyCrucio();
                yield break;
            }
        }
        
        public void DestroyCrucio()
        {
            System.Console.WriteLine("Trying to destroy CrucioObject");
            CrucioObject?.SetActive(false);
            CrucioObject?.GetComponent<SpriteRenderer>().Destroy();
            CrucioObject?.Destroy();
        }

        public void RpcDestroyCrucio()
        {
            DestroyCrucio();
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DestroyCrucio, SendOption.Reliable);
            writer.EndMessage();
        }
        
        public void CreateCrucio(Vector2 pos, ModdedPlayerClass Owner)
        {
            Reactor.Coroutines.Start(CoCastCrucio(pos, Owner));
        }
        
        public void RpcCreateCrucio(Vector2 pos, ModdedPlayerClass Owner)
        {
            CreateCrucio(pos, Owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCrucio, SendOption.Reliable);
            writer.Write(Owner._Object.PlayerId);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.EndMessage();
        }
        
        public System.Collections.IEnumerator CoCastCurse(Vector3 mousePosition, ModdedPlayerClass Owner)
        {
            DateTime now = DateTime.UtcNow;
            int curseindex = 0;
            
            CurseObject?.Destroy();
            CurseObject = new GameObject();
            SpriteRenderer curseRender = CurseObject.AddComponent<SpriteRenderer>();
            Rigidbody2D curseRigid = CurseObject.AddComponent<Rigidbody2D>();
            CurseObject.SetActive(true);
            BoxCollider2D curseCollider = CurseObject.AddComponent<BoxCollider2D>();
            curseRender.enabled = true;
            curseRigid.transform.position = Owner._Object.myRend.bounds.center;
            curseRender.transform.localScale = new Vector2(1f, 1f);

            Vector3 v = mousePosition - Owner._Object.myRend.bounds.center;
            float dist = Vector2.Distance(mousePosition, Owner._Object.myRend.bounds.center);
            Vector3 d = v * 3f * (2f / dist);
            float AngleRad = Mathf.Atan2(mousePosition.y - Owner._Object.myRend.bounds.center.y, mousePosition.x - Owner._Object.myRend.bounds.center.x);
            float AngleDeg = (180 / (float)Math.PI) * AngleRad;
            
            curseCollider.autoTiling = false;
            curseCollider.edgeRadius = 0;
            curseCollider.size = Owner._Object.Collider.bounds.size * 2;
            curseRigid.velocity = new Vector2(d.x, d.y);
            CurseObject.layer = 8;

            while (true)
            {
                if (CurseObject == null)
                    yield break;

                if (curseindex <= 5)
                    curseRender.sprite = Assets.CurseSprite[0];
                else
                    curseRender.sprite = Assets.CurseSprite[1];

                if (curseindex >= 10)
                    curseindex = 0;

                curseindex++;
                
                curseRigid.rotation = AngleDeg;
                curseRigid.drag = 0;
                curseRigid.angularDrag = 0;
                curseRigid.inertia = 0;
                curseRigid.gravityScale = 0;

                Vector2 oldVelocity = curseRigid.velocity;
                
                yield return null;
                
                if (CurseObject == null)
                    yield break;

                if (curseRigid.velocity != oldVelocity && Owner._Object.AmOwner)
                {
                    RpcDestroyCurse();
                    yield break;
                }
                
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.IsDead || player.Data.Disconnected || Owner._Object == player || player.Data.IsImpostor)
                        continue;

                    if (!player.myRend.bounds.Intersects(curseRender.bounds))
                        continue;

                    if (!Owner._Object.AmOwner)
                        yield break;
                    
                    RpcDestroyCurse();

                    if (ModdedPlayerById(player.PlayerId).Immortal)
                        yield break;

                    if (GetPlayerRoleName(ModdedPlayerById(player.PlayerId)) == "Harry")
                        RpcKillPlayer(Owner._Object, Owner._Object);
                    else
                        RpcKillPlayer(Owner._Object, player);

                    yield break;
                }

                if (now.AddSeconds(5) >= DateTime.UtcNow && !MeetingHud.Instance && AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
                    continue;
                
                RpcDestroyCurse();
                yield break;
            }
        }
        
        public void DestroyCurse()
        {
            CurseObject?.SetActive(false);
            CurseObject?.GetComponent<SpriteRenderer>().Destroy();
            CurseObject?.Destroy();
        }

        public void RpcDestroyCurse()
        {
            DestroyCurse();
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DestroyCurse, SendOption.Reliable);
            writer.EndMessage();
        }
        
        public void CreateCurse(Vector2 pos, ModdedPlayerClass Owner)
        {
            Reactor.Coroutines.Start(CoCastCurse(pos, Owner));
            Owner._Object.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
        }
        
        public void RpcCreateCurse(Vector2 pos, ModdedPlayerClass Owner)
        {
            CreateCurse(pos, Owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCurse, SendOption.Reliable);
            writer.Write(Owner._Object.PlayerId);
            writer.Write(pos.x);
            writer.Write(pos.y);
            writer.EndMessage();
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
            return player?.Role?.RoleName;
        }
    }
}