using Hazel;
using Reactor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.WorldItems;
using InnerNet;
using UnityEngine;

//Get random item positions for Mira and Skeld
//Make the snitch float around and bounce off walls
//Finish making game-settings work (set up Order of Imps)
//Finishing importing textures (mainly curses and item buttons)
//Comms and deluminator break the task list text for Impostors
//Add durations, cooldowns, and the player's role in the task list
//Change colors and introstrings for Hermione, Harry, and Ron
//Various small visual changes

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

        public List<Tuple<ShipStatus.MapType, Vector2>> PossibleItemPositions { get; } = new List<Tuple<ShipStatus.MapType, Vector2>>
        {
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(18.58625f, -21.96028f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(17.26129f, -19.21864f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(17.12244f, -17.1841f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(20.71172f, -21.69597f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(22.10037f, -25.14912f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(24.01747f, -24.70225f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(27.69598f, -20.75172f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(36.57115f, -21.61535f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(39.53685f, -10.09214f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(34.10915f, -10.16738f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(40.37588f, -7.95609f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(30.84753f, -7.736305f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(25.44218f, -7.583571f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(22.11417f, -8.61549f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(24.15556f, -3.469771f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(25.03353f, -12.18799f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(30.12325f, -16.94627f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(24.99184f, -17.18198f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(19.82783f, -14.6528f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(13.25453f, -12.43384f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(20.17053f, -11.87447f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(9.549327f, -9.725276f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(7.533298f, -12.32464f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(5.438123f, -11.85916f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(2.918584f, -11.96187f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(5.385927f, -16.55273f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(1.579458f, -17.25811f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(2.281264f, -24.06277f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(1.496747f, -20.5509f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(6.260661f, -24.13944f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(10.77005f, -20.56692f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(12.74145f, -23.43479f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(11.00322f, -17.61416f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(16.18938f, -24.71703f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(4.66598f, -4.429569f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(11.46261f, -7.265319f)),
            new Tuple<ShipStatus.MapType, Vector2>(ShipStatus.MapType.Pb, new Vector2(16.66492f, -2.420291f)),
        };

        public List<Vector2> GetAllApplicableItemPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (Tuple<ShipStatus.MapType, Vector2> position in PossibleItemPositions)
                if (ShipStatus.Instance != null && position.Item1 == ShipStatus.Instance.Type)
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
            while (true)
            {
                if (player.AmOwner)
                    GetLocalModdedPlayer().Role?.ResetCooldowns();
                
                if (MeetingHud.Instance ||
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.HourglassTimer) < DateTime.UtcNow)
                {
                    if (ModdedPlayerById(player.PlayerId).KilledByCurse)
                        yield break;

                    if (player.Data.IsDead)
                        RpcRevivePlayer(player);

                    if (MeetingHud.Instance)
                        yield break;

                    RpcTeleportPlayer(player, startPosition);
                    yield break;
                }

                yield return null;
            }
        }
        
        public void SpawnItem(int id, Vector2 pos)
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
                    TheGoldenSnitchWorld snitch = new TheGoldenSnitchWorld(pos);
                    AllItems.Add(snitch);
                    break;
            }
        }

        public void SetNameColor(PlayerControl player, Color color)
        {
            player.nameText.Color = color;
            if (HudManager.Instance && HudManager.Instance.Chat)
                foreach (PoolableBehavior bubble in HudManager.Instance.Chat.chatBubPool.activeChildren)
                    if (bubble.Cast<ChatBubble>().NameText.text == player.nameText.Text)
                        bubble.Cast<ChatBubble>().NameText.color = color;
            if (MeetingHud.Instance && MeetingHud.Instance.playerStates != null)
                foreach (PlayerVoteArea voteArea in MeetingHud.Instance.playerStates)
                    if (voteArea.TargetPlayerId == player.PlayerId)
                        voteArea.NameText.Color = color;
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
            SpawnItem(id, pos);
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.SpawnItem, SendOption.Reliable);
            writer.Write(id);
            writer.Write(pos.x);
            writer.Write(pos.y);
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
            ModdedPlayerById(player.PlayerId).Immortal = true;
            player.MyPhysics.body.velocity = new Vector2(0, 0);
            player.moveable = false;
            while (true)
            {
                if (player.AmOwner)
                    GetLocalModdedPlayer().Role?.ResetCooldowns();
                
                if (MeetingHud.Instance ||
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.DefensiveDuelistDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(player.PlayerId).ControllerOverride != null)
                {
                    ModdedPlayerById(player.PlayerId).Immortal = false;
                    player.moveable = true;
                    
                    yield break;
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
            while (true)
            {
                if (target.AmOwner)
                    GetLocalModdedPlayer().Role?.ResetCooldowns();

                target.Visible = false;
                
                if (MeetingHud.Instance || 
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.InvisCloakDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(target.PlayerId).ControllerOverride != null)
                {
                    target.Visible = true;
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
            while (true)
            {
                if (target == PlayerControl.LocalPlayer)
                {
                    target.myLight.LightRadius = Mathf.Lerp(ShipStatus.Instance.MinLightRadius, ShipStatus.Instance.MaxLightRadius, num) * PlayerControl.GameOptions.CrewLightMod;
                    target.moveable = false;
                }
                if (MeetingHud.Instance || 
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.CrucioDuration) < DateTime.UtcNow ||
                    ModdedPlayerById(target.PlayerId).ControllerOverride != null)
                {
                    target.moveable = true;
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

            if (controller.AmOwner)
                Camera.main.GetComponent<FollowerCamera>().Target = target;

            target.moveable = true;
            controller.moveable = true;
            
            if (target.AmOwner || controller.AmOwner)
                PlayerControl.LocalPlayer.MyPhysics.body.velocity = new Vector2(0, 0);

            while (true)
            {
                if (target.Data.IsDead || 
                    MeetingHud.Instance || 
                    AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started ||
                    now.AddSeconds(Config.CrucioDuration) < DateTime.UtcNow)
                {
                    if (target.AmOwner)
                        target.moveable = true;
                    else if (controller.AmOwner)
                    {
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

            if (target.PlayerId == PlayerControl.LocalPlayer.PlayerId || 
                killer.PlayerId == PlayerControl.LocalPlayer.PlayerId || 
                ModdedPlayerById(killer.PlayerId).ControllerOverride?._Object.PlayerId == PlayerControl.LocalPlayer.PlayerId)
            {  
                System.Console.WriteLine("Trying to play the kill sound!");
                SoundManager.Instance.StopSound(PlayerControl.LocalPlayer.KillSfx);
                SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.8f);
            }

            if (target == PlayerControl.LocalPlayer)
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
                ModdedPlayerById(target.PlayerId).KilledByCurse = true;
            
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

        public System.Collections.IEnumerator CoCastCrucio(Vector3 direction, ModdedPlayerClass Owner)
        {
            DateTime now = DateTime.UtcNow;
            
            CrucioObject?.Destroy();
            CrucioObject = new GameObject();
            SpriteRenderer crucioRender = CrucioObject.AddComponent<SpriteRenderer>();
            Rigidbody2D crucioRigid = CrucioObject.AddComponent<Rigidbody2D>();
            CrucioObject.SetActive(true);
            BoxCollider2D crucioCollider = CrucioObject.AddComponent<BoxCollider2D>();
            crucioRender.enabled = true;
            crucioRender.sprite = Assets.CurseSprite;
            crucioRender.color = Color.red;
            crucioRigid.transform.position = new Vector3(Owner._Object.myRend.bounds.center.x, Owner._Object.myRend.bounds.center.y, -50f);
            crucioRender.transform.localScale = new Vector2(0.5f, 0.5f);

            crucioCollider.autoTiling = false;
            crucioCollider.edgeRadius = 0;
            crucioCollider.size = Owner._Object.Collider.bounds.size * 2;
            crucioRigid.velocity = new Vector2(direction.x, direction.y);
            CrucioObject.layer = 8;

            while (true)
            {
                if (CrucioObject == null)
                    yield break;
                
                crucioRigid.fixedAngle = true;
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

                    Vector2 screenMin = Camera.main.WorldToScreenPoint(player.myRend.bounds.min);
                    Vector2 screenMax = Camera.main.WorldToScreenPoint(player.myRend.bounds.max);
                    Vector2 rigidScreen = Camera.main.WorldToScreenPoint(crucioRigid.transform.position);

                    if (rigidScreen.x <= screenMin.x || rigidScreen.x >= screenMax.x)
                        continue;
                
                    if (rigidScreen.y <= screenMin.y || rigidScreen.y >= screenMax.y)
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
        
        public void CreateCrucio(Vector2 d, ModdedPlayerClass Owner)
        {
            Reactor.Coroutines.Start(CoCastCrucio(d, Owner));
        }
        
        public void RpcCreateCrucio(Vector2 d, ModdedPlayerClass Owner)
        {
            CreateCrucio(d, Owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCrucio, SendOption.Reliable);
            writer.Write(Owner._Object.PlayerId);
            writer.Write(d.x);
            writer.Write(d.y);
            writer.EndMessage();
        }
        
        public System.Collections.IEnumerator CoCastCurse(Vector3 direction, ModdedPlayerClass Owner)
        {
            DateTime now = DateTime.UtcNow;
            
            CurseObject?.Destroy();
            CurseObject = new GameObject();
            SpriteRenderer curseRender = CurseObject.AddComponent<SpriteRenderer>();
            Rigidbody2D curseRigid = CurseObject.AddComponent<Rigidbody2D>();
            CurseObject.SetActive(true);
            BoxCollider2D curseCollider = CurseObject.AddComponent<BoxCollider2D>();
            curseRender.enabled = true;
            curseRender.sprite = Assets.CurseSprite;
            curseRender.color = Color.green;
            curseRigid.transform.position = new Vector3(Owner._Object.myRend.bounds.center.x, Owner._Object.myRend.bounds.center.y, -50f);
            curseRender.transform.localScale = new Vector2(0.5f, 0.5f);

            curseCollider.autoTiling = false;
            curseCollider.edgeRadius = 0;
            curseCollider.size = Owner._Object.Collider.bounds.size * 2;
            curseRigid.velocity = new Vector2(direction.x, direction.y);
            CurseObject.layer = 8;

            while (true)
            {
                if (CurseObject == null)
                    yield break;
                
                curseRigid.fixedAngle = true;
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

                    Vector2 screenMin = Camera.main.WorldToScreenPoint(player.myRend.bounds.min);
                    Vector2 screenMax = Camera.main.WorldToScreenPoint(player.myRend.bounds.max);
                    Vector2 rigidScreen = Camera.main.WorldToScreenPoint(curseRigid.transform.position);

                    if (rigidScreen.x <= screenMin.x || rigidScreen.x >= screenMax.x)
                        continue;
                
                    if (rigidScreen.y <= screenMin.y || rigidScreen.y >= screenMax.y)
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
        
        public void CreateCurse(Vector2 d, ModdedPlayerClass Owner)
        {
            Reactor.Coroutines.Start(CoCastCurse(d, Owner));
            Owner._Object.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
        }
        
        public void RpcCreateCurse(Vector2 d, ModdedPlayerClass Owner)
        {
            CreateCurse(d, Owner);

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.CreateCurse, SendOption.Reliable);
            writer.Write(Owner._Object.PlayerId);
            writer.Write(d.x);
            writer.Write(d.y);
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