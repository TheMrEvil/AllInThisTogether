using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class PlayerNetwork : EntityNetworked
{
	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00032A5D File Offset: 0x00030C5D
	public PlayerControl Control
	{
		get
		{
			return this.control as PlayerControl;
		}
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00032A6A File Offset: 0x00030C6A
	public override void Awake()
	{
		base.Awake();
		this.paudio = base.GetComponent<PlayerAudio>();
		base.InvokeRepeating("UpdateCache", 5f, UnityEngine.Random.Range(4.3f, 5.1f));
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00032A9D File Offset: 0x00030C9D
	public override void Setup()
	{
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00032A9F File Offset: 0x00030C9F
	public void SyncUserID()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("SendUserIDNetwork", RpcTarget.All, new object[]
		{
			PlatformSetup.UserID
		});
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00032ACE File Offset: 0x00030CCE
	[PunRPC]
	private void SendUserIDNetwork(string userID)
	{
		this.Control.SetUserID(userID);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00032ADC File Offset: 0x00030CDC
	public void Ping(PlayerDB.PingType ping, Vector3 pos, EntityControl entity, bool needOffset)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("PingNetwork", RpcTarget.All, new object[]
		{
			(int)ping,
			pos,
			(entity == null) ? -1 : entity.ViewID,
			needOffset
		});
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00032B44 File Offset: 0x00030D44
	[PunRPC]
	public void PingNetwork(int pingType, Vector3 pos, int followEntity, bool needOffset)
	{
		this.Control.actions.PingController.SetPing((PlayerDB.PingType)pingType, pos, followEntity, needOffset);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00032B60 File Offset: 0x00030D60
	public void SendUIPing(string ping_id, UIPing.UIPingType pingType, string context)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("SendUIPingNetwork", RpcTarget.All, new object[]
		{
			ping_id,
			(int)pingType,
			context
		});
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00032B98 File Offset: 0x00030D98
	[PunRPC]
	public void SendUIPingNetwork(string pid, int pingType, string context)
	{
		UIPing.instance.GotPing(this.Control, pid, (UIPing.UIPingType)pingType, context);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00032BAD File Offset: 0x00030DAD
	public void ClearPing()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("ClearPingNetwork", RpcTarget.Others, Array.Empty<object>());
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00032BD3 File Offset: 0x00030DD3
	[PunRPC]
	public void ClearPingNetwork()
	{
		this.Control.actions.PingController.Clear(true);
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00032BEB File Offset: 0x00030DEB
	public void Emote(string emote)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("EmoteNetwork", RpcTarget.All, new object[]
		{
			emote
		});
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00032C16 File Offset: 0x00030E16
	[PunRPC]
	private void EmoteNetwork(string emote)
	{
		this.Control.Display.Emote(emote);
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00032C29 File Offset: 0x00030E29
	public void StopEmote()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("StopEmoteNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00032C4F File Offset: 0x00030E4F
	[PunRPC]
	private void StopEmoteNetwork()
	{
		this.Control.Display.StopCurrentEmote();
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00032C61 File Offset: 0x00030E61
	public void SetSpectator(bool isSpectator)
	{
		this.view.RPC("SetSpectatorNetwork", RpcTarget.All, new object[]
		{
			isSpectator
		});
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00032C83 File Offset: 0x00030E83
	[PunRPC]
	private void SetSpectatorNetwork(bool isSpectator)
	{
		this.Control.SetSpectatorLocal(isSpectator);
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00032C94 File Offset: 0x00030E94
	public void TeamRevived(int Reviver, int Rev2 = -1, int Rev3 = -1)
	{
		this.view.RPC("ReviveConfirmedNetwork", RpcTarget.All, new object[]
		{
			Reviver,
			Rev2,
			Rev3,
			this.Control.GhostPlayerDisplay.transform.position
		});
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00032CF0 File Offset: 0x00030EF0
	[PunRPC]
	private void ReviveConfirmedNetwork(int Rev1, int Rev2, int Rev3, Vector3 pos)
	{
		PlayerControl player = PlayerControl.GetPlayer(Rev1);
		GameRecord.RecordEvent(GameRecord.EventType.Player_Revived, this.Control, pos, player);
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00032D13 File Offset: 0x00030F13
	public void Jumped()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("PlayerJumpedNetwork", RpcTarget.Others, Array.Empty<object>());
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00032D39 File Offset: 0x00030F39
	[PunRPC]
	public void PlayerJumpedNetwork()
	{
		this.Control.Audio.PlayEventSound(0);
		this.Control.Display.OnJumped();
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00032D5C File Offset: 0x00030F5C
	public void Landed(Vector3 momentum)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("PlayerLandedNetwork", RpcTarget.Others, new object[]
		{
			momentum
		});
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x00032D8C File Offset: 0x00030F8C
	[PunRPC]
	public void PlayerLandedNetwork(Vector3 momentum)
	{
		this.Control.Movement.GetPlanarVelocity();
		this.Control.Audio.OnLanded(momentum, this.Control.Movement.GetPosition(), Vector3.up);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00032DC8 File Offset: 0x00030FC8
	public void PlayerSound(EntityAudio.NetAudioEvent EventID)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("PlayerSoundNetwork", RpcTarget.All, new object[]
		{
			(int)EventID
		});
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00032E05 File Offset: 0x00031005
	[PunRPC]
	public void PlayerSoundNetwork(int EventID)
	{
		this.paudio.PlayEventSound(EventID);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00032E13 File Offset: 0x00031013
	public void ActivateGhost(Vector3 pos)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("ActivateGhostNetwork", RpcTarget.All, new object[]
		{
			pos
		});
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00032E43 File Offset: 0x00031043
	[PunRPC]
	public void ActivateGhostNetwork(Vector3 pos)
	{
		this.Control.Health.ActivateGhost(pos);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x00032E56 File Offset: 0x00031056
	public void SendStats()
	{
		this.view.RPC("SendStatsNetwork", RpcTarget.All, new object[]
		{
			this.Control.PStats.ToString()
		});
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00032E84 File Offset: 0x00031084
	[PunRPC]
	public void SendStatsNetwork(string statString)
	{
		PlayerGameStats playerGameStats = new PlayerGameStats(statString);
		this.Control.PStats = playerGameStats;
		playerGameStats.PlayerRef = this.Control;
		PostGamePanel.instance.AddPlayerData(playerGameStats);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00032EBC File Offset: 0x000310BC
	public void SendCosmetics(CosmeticSet cosmetic, Player player = null)
	{
		if (player == null)
		{
			this.view.RPC("SendCosmeticsNetwork", RpcTarget.Others, new object[]
			{
				cosmetic.ToString()
			});
			return;
		}
		this.view.RPC("SendCosmeticsNetwork", player, new object[]
		{
			cosmetic.ToString()
		});
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00032F0D File Offset: 0x0003110D
	[PunRPC]
	public void SendCosmeticsNetwork(string cosmeticVal)
	{
		this.Control.Display.LoadCosmetics(cosmeticVal);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00032F20 File Offset: 0x00031120
	public void AugmentChosen(AugmentTree augment, List<Choice> options)
	{
		string id = augment.ID;
		string text = (options.Count > 0) ? options[0].Augment.ID : "";
		string text2 = (options.Count > 1) ? options[1].Augment.ID : "";
		string text3 = (options.Count > 2) ? options[2].Augment.ID : "";
		string text4 = (options.Count > 3) ? options[3].Augment.ID : "";
		string text5 = (options.Count > 4) ? options[4].Augment.ID : "";
		this.view.RPC("AugmentChosenNetwork", RpcTarget.All, new object[]
		{
			id,
			text,
			text2,
			text3,
			text4,
			text5
		});
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00033014 File Offset: 0x00031214
	public void AugmentRerolled(List<AugmentTree> options)
	{
		string text = (options.Count > 0) ? options[0].ID : "";
		string text2 = (options.Count > 1) ? options[1].ID : "";
		string text3 = (options.Count > 2) ? options[2].ID : "";
		string text4 = (options.Count > 3) ? options[3].ID : "";
		string text5 = (options.Count > 4) ? options[4].ID : "";
		this.view.RPC("AugmentRerolledNetwork", RpcTarget.All, new object[]
		{
			text,
			text2,
			text3,
			text4,
			text5
		});
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x000330E0 File Offset: 0x000312E0
	public override void SendAugments(Player player = null)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		base.SendAugments(player);
		if (player == null)
		{
			this.view.RPC("SendTalentsNetwork", RpcTarget.Others, new object[]
			{
				Progression.InkLevel,
				Progression.PrestigeCount,
				this.Control.Talents.ToString()
			});
			return;
		}
		this.view.RPC("SendTalentsNetwork", player, new object[]
		{
			Progression.InkLevel,
			Progression.PrestigeCount,
			this.Control.Talents.ToString()
		});
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00033190 File Offset: 0x00031390
	[PunRPC]
	private void AugmentChosenNetwork(string chosen, string opt1, string opt2, string opt3, string opt4, string opt5)
	{
		AugmentTree augment = GraphDB.GetAugment(chosen);
		List<AugmentTree> list = new List<AugmentTree>();
		if (opt1.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt1));
		}
		if (opt2.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt2));
		}
		if (opt3.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt3));
		}
		if (opt4.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt4));
		}
		if (opt5.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt5));
		}
		GameRecord.PlayerUpgradeChosen(this.Control.ViewID, augment, list);
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0003322C File Offset: 0x0003142C
	[PunRPC]
	private void AugmentRerolledNetwork(string opt1, string opt2, string opt3, string opt4, string opt5)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		if (opt1.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt1));
		}
		if (opt2.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt2));
		}
		if (opt3.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt3));
		}
		if (opt4.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt4));
		}
		if (opt5.Length > 1)
		{
			list.Add(GraphDB.GetAugment(opt5));
		}
		GameRecord.PlayerUpgradeRerolled(this.Control.ViewID, list);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000332BD File Offset: 0x000314BD
	public void AugmentAddedExternal(AugmentTree mod)
	{
		this.view.RPC("AugmentExternalNetwork", RpcTarget.All, new object[]
		{
			mod.ID
		});
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x000332E0 File Offset: 0x000314E0
	[PunRPC]
	private void AugmentExternalNetwork(string augmentID)
	{
		AugmentTree augment = GraphDB.GetAugment(augmentID);
		GameRecord.RecordEvent(GameRecord.EventType.Player_AugmentAdd, this.Control, augment.Root.Name);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0003330B File Offset: 0x0003150B
	[PunRPC]
	public void SendTalentsNetwork(int level, int prestige, string AugmentStr)
	{
		if (this.view.IsMine)
		{
			return;
		}
		this.Control.InkLevel = level;
		this.Control.PrestigeLevel = prestige;
		this.Control.Talents = new Augments(AugmentStr);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00033344 File Offset: 0x00031544
	public void SendAbilities()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		string id = this.Control.actions.core.ID;
		string id2 = this.Control.actions.primary.ID;
		string id3 = this.Control.actions.secondary.ID;
		string id4 = this.Control.actions.movement.ID;
		string id5 = this.Control.actions.utility.ID;
		string id6 = this.Control.actions.ghost.ID;
		this.view.RPC("SendAbilitiesNetwork", RpcTarget.Others, new object[]
		{
			id,
			id2,
			id3,
			id4,
			id5,
			id6
		});
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00033418 File Offset: 0x00031618
	private void SendAbilities(Player Player)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		string id = this.Control.actions.core.ID;
		string id2 = this.Control.actions.primary.ID;
		string id3 = this.Control.actions.secondary.ID;
		string id4 = this.Control.actions.movement.ID;
		string id5 = this.Control.actions.utility.ID;
		string id6 = this.Control.actions.ghost.ID;
		this.view.RPC("SendAbilitiesNetwork", Player, new object[]
		{
			id,
			id2,
			id3,
			id4,
			id5,
			id6
		});
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000334EC File Offset: 0x000316EC
	[PunRPC]
	public void SendAbilitiesNetwork(string core, string primary, string secondary, string movement, string utility, string ghost)
	{
		this.Control.actions.LoadCore(core);
		this.Control.actions.LoadAbility(PlayerAbilityType.Primary, primary, false);
		this.Control.actions.LoadAbility(PlayerAbilityType.Secondary, secondary, false);
		this.Control.actions.LoadAbility(PlayerAbilityType.Movement, movement, false);
		this.Control.actions.LoadAbility(PlayerAbilityType.Utility, utility, false);
		this.Control.actions.LoadAbility(PlayerAbilityType.Ghost, ghost, false);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0003356C File Offset: 0x0003176C
	public void AbilityActivated(int actionID, Vector3 startPoint, Vector3 dir, EffectProperties props)
	{
		if (PlayerControl.PlayerCount > 1)
		{
			this.view.RPC("AbilityActivatedNetwork", RpcTarget.Others, new object[]
			{
				actionID,
				startPoint,
				dir,
				props.ToString()
			});
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x000335BD File Offset: 0x000317BD
	[PunRPC]
	private void AbilityActivatedNetwork(int actionID, Vector3 startPoint, Vector3 dir, string props)
	{
		(this.control as PlayerControl).ActivateAbilityLocal(actionID, startPoint, dir, new EffectProperties(props));
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x000335DC File Offset: 0x000317DC
	public void AbilityReleased(int actionID, Vector3 startPoint, Vector3 dir, EffectProperties props)
	{
		if (PlayerControl.PlayerCount > 1)
		{
			this.view.RPC("AbilityReleasedNetwork", RpcTarget.Others, new object[]
			{
				actionID,
				startPoint,
				dir,
				props.ToString()
			});
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0003362D File Offset: 0x0003182D
	[PunRPC]
	private void AbilityReleasedNetwork(int actionID, Vector3 startPoint, Vector3 dir, string props)
	{
		(this.control as PlayerControl).ReleaseAbilityLocal(actionID, startPoint, dir, new EffectProperties(props));
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0003364C File Offset: 0x0003184C
	public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(this.Control.Movement.input.cameraForward);
			stream.SendNext(this.Control.Movement.input.cameraRight);
			stream.SendNext(this.Control.Movement.headLook.baseRot);
			stream.SendNext(this.Control.Movement.headLook.fullRot);
			stream.SendNext(this.Control.AimPoint);
			stream.SendNext(this.Control.CameraAimPoint);
			stream.SendNext(this.Control.Input.WorldInputAxis);
			stream.SendNext(this.Control.Health.ReviveProgress);
			stream.SendNext(this.Control.Health.GroupRezTimer);
			stream.SendNext(this.Control.Health.AutoRevivesUsed);
			stream.SendNext((int)this.Control.CurMenu);
			stream.SendNext((this.Control.currentTarget != null) ? this.Control.currentTarget.ViewID : -1);
			stream.SendNext((this.Control.allyTarget != null) ? this.Control.allyTarget.ViewID : -1);
			stream.SendNext(this.Control.Display.ShowSpectatorCam && this.Control.IsSpectator);
			if (this.Control.Display.ShowSpectatorCam)
			{
				stream.SendNext(this.Control.Display.SpectatorCam.transform.position);
				stream.SendNext(this.Control.Display.SpectatorCam.transform.rotation);
			}
			stream.SendNext(this.CurrentDPS);
		}
		else
		{
			this.Control.Movement.input.cameraForward = (Vector3)stream.ReceiveNext();
			this.Control.Movement.input.cameraRight = (Vector3)stream.ReceiveNext();
			this.Control.Movement.headLook.baseRot = (Quaternion)stream.ReceiveNext();
			this.Control.Movement.headLook.fullRot = (Quaternion)stream.ReceiveNext();
			this.Control.AimPoint = (Vector3)stream.ReceiveNext();
			this.Control.CameraAimPoint = (Vector3)stream.ReceiveNext();
			this.Control.Input.WorldInputAxis = (Vector3)stream.ReceiveNext();
			this.Control.Health.ReviveProgress = (float)stream.ReceiveNext();
			this.Control.Health.GroupRezTimer = (float)stream.ReceiveNext();
			this.Control.Health.AutoRevivesUsed = (int)stream.ReceiveNext();
			this.Control.CurMenu = (PanelType)((int)stream.ReceiveNext());
			int num = (int)stream.ReceiveNext();
			if (num == -1)
			{
				this.Control.currentTarget = null;
			}
			else if (this.Control.currentTarget == null || this.Control.currentTarget.ViewID != num)
			{
				this.Control.currentTarget = EntityControl.GetEntity(num);
			}
			int num2 = (int)stream.ReceiveNext();
			if (num2 == -1)
			{
				this.Control.allyTarget = null;
			}
			else if (this.Control.allyTarget == null || this.Control.allyTarget.ViewID != num2)
			{
				this.Control.allyTarget = EntityControl.GetEntity(num);
			}
			bool flag = (bool)stream.ReceiveNext();
			this.Control.Display.ShowSpectatorCam = flag;
			if (flag)
			{
				this.Control.Display.SpectatorPos = (Vector3)stream.ReceiveNext();
				this.Control.Display.SpectatorRot = (Quaternion)stream.ReceiveNext();
			}
			this.CurrentDPS = (float)stream.ReceiveNext();
		}
		base.OnPhotonSerializeView(stream, info);
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00033AD8 File Offset: 0x00031CD8
	public void SendExtraData(Player Player = null)
	{
		this.CurrentAttunement = Progression.BindingAttunement;
		if (Player == null)
		{
			this.view.RPC("SendExtraDataNetworked", RpcTarget.Others, new object[]
			{
				Progression.BindingAttunement
			});
			return;
		}
		this.view.RPC("SendExtraDataNetworked", Player, new object[]
		{
			Progression.BindingAttunement
		});
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00033B3C File Offset: 0x00031D3C
	[PunRPC]
	private void SendExtraDataNetworked(int attunement)
	{
		this.CurrentAttunement = attunement;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00033B48 File Offset: 0x00031D48
	internal override void PlayerConnected(Player Player)
	{
		this.SendAugments(Player);
		this.SendAbilities(Player);
		this.SendCosmetics(this.Control.Display.CurSet, Player);
		this.SendExtraData(Player);
		this.view.RPC("SendUserIDNetwork", Player, new object[]
		{
			PlatformSetup.UserID
		});
		if (this.control.IsDead)
		{
			this.view.RPC("ActivateGhostNetwork", Player, new object[]
			{
				this.Control.GhostPlayerDisplay.transform.position
			});
		}
		if (PhotonNetwork.IsMasterClient && PlayerNetwork.PlayerCache.ContainsKey(Player.NickName))
		{
			this.view.RPC("SendCacheNetwork", Player, new object[]
			{
				PlayerNetwork.PlayerCache[Player.NickName].ToJSON()
			});
		}
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00033C29 File Offset: 0x00031E29
	public void UpdateCache()
	{
		if (!this.Control.IsMine)
		{
			return;
		}
		this.view.RPC("UpdateCacheNetwork", RpcTarget.Others, new object[]
		{
			new PlayerNetwork.PlayerDataCache(this.Control).ToJSON()
		});
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00033C64 File Offset: 0x00031E64
	[PunRPC]
	public void UpdateCacheNetwork(string data)
	{
		PlayerNetwork.PlayerDataCache playerDataCache = new PlayerNetwork.PlayerDataCache(data);
		if (PlayerNetwork.PlayerCache.ContainsKey(playerDataCache.userID))
		{
			PlayerNetwork.PlayerCache[playerDataCache.userID] = playerDataCache;
			return;
		}
		PlayerNetwork.PlayerCache.Add(playerDataCache.userID, playerDataCache);
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00033CB0 File Offset: 0x00031EB0
	[PunRPC]
	public void SendCacheNetwork(string data)
	{
		Debug.Log("Got Cache data from host - " + data);
		PlayerNetwork.PlayerDataCache dataCache = new PlayerNetwork.PlayerDataCache(data);
		GameplayManager.InvokeDelayed(0.5f, delegate()
		{
			dataCache.Apply(PlayerControl.myInstance);
		});
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00033CF5 File Offset: 0x00031EF5
	private void OnEnable()
	{
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00033CF7 File Offset: 0x00031EF7
	private void OnDisable()
	{
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00033CF9 File Offset: 0x00031EF9
	public PlayerNetwork()
	{
	}

	// Token: 0x040005B4 RID: 1460
	private PlayerAudio paudio;

	// Token: 0x040005B5 RID: 1461
	public static Dictionary<string, PlayerNetwork.PlayerDataCache> PlayerCache;

	// Token: 0x040005B6 RID: 1462
	[NonSerialized]
	public float CurrentDPS;

	// Token: 0x040005B7 RID: 1463
	public int CurrentAttunement;

	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class PlayerDataCache
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x000C65BC File Offset: 0x000C47BC
		public PlayerDataCache(PlayerControl control)
		{
			this.userID = control.Username;
			this.augments = control.Augment;
			this.loadout = new Progression.Loadout(control);
			this.statuses = new Dictionary<int, int>();
			this.rerollsUsed = PlayerChoicePanel.RerollsUsed;
			this.pageSeed = control.PageSeed;
			this.seedCounter = control.PageSeedCount;
			foreach (EntityControl.AppliedStatus appliedStatus in control.Statuses)
			{
				if (!appliedStatus.expires && appliedStatus.sourceID == control.ViewID)
				{
					this.statuses.Add(appliedStatus.HashCode, appliedStatus.Stacks);
				}
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000C6690 File Offset: 0x000C4890
		public PlayerDataCache(string input)
		{
			JSONNode jsonnode = JSON.Parse(input);
			this.userID = jsonnode.GetValueOrDefault("id", "");
			this.augments = new Augments(jsonnode.GetValueOrDefault("augments", ""));
			this.loadout = new Progression.Loadout(jsonnode.GetValueOrDefault("loadout", ""));
			this.rerollsUsed = jsonnode.GetValueOrDefault("reroll", 0);
			this.pageSeed = jsonnode.GetValueOrDefault("pageSeed", 0);
			this.seedCounter = jsonnode.GetValueOrDefault("seedCounter", 0);
			this.statuses = new Dictionary<int, int>();
			if (jsonnode.HasKey("status"))
			{
				string[] array = jsonnode.GetValueOrDefault("status", "").ToString().Replace("\"", "").Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split('|', StringSplitOptions.None);
					if (array2.Length == 2)
					{
						int key;
						int.TryParse(array2[0], out key);
						int value;
						int.TryParse(array2[1], out value);
						this.statuses.TryAdd(key, value);
					}
				}
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000C67F8 File Offset: 0x000C49F8
		public string ToJSON()
		{
			JSONNode jsonnode = new JSONObject();
			jsonnode.Add("id", this.userID);
			jsonnode.Add("augments", this.augments.ToString());
			jsonnode.Add("loadout", this.loadout.ToString());
			jsonnode.Add("reroll", this.rerollsUsed);
			jsonnode.Add("pageSeed", this.pageSeed);
			jsonnode.Add("seedCounter", this.seedCounter);
			if (this.statuses != null && this.statuses.Count > 0)
			{
				string text = "";
				foreach (KeyValuePair<int, int> keyValuePair in this.statuses)
				{
					text = string.Concat(new string[]
					{
						text,
						keyValuePair.Key.ToString(),
						"|",
						keyValuePair.Value.ToString(),
						","
					});
				}
				jsonnode.Add("status", text.Substring(0, text.Length));
			}
			return jsonnode.ToString();
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000C6964 File Offset: 0x000C4B64
		public void Apply(PlayerControl control)
		{
			control.Augment = this.augments;
			control.actions.ApplyLoadout(this.loadout, false);
			foreach (KeyValuePair<int, int> keyValuePair in this.statuses)
			{
				control.Net.ApplyStatus(keyValuePair.Key.GetHashCode(), control.ViewID, 0f, keyValuePair.Value, true, 0);
			}
			control.Net.SendAugments(null);
			PlayerChoicePanel.RerollsUsed = this.rerollsUsed;
			control.SetupRandom(this.pageSeed, this.seedCounter);
		}

		// Token: 0x040023F0 RID: 9200
		public string userID;

		// Token: 0x040023F1 RID: 9201
		public int rerollsUsed;

		// Token: 0x040023F2 RID: 9202
		public int pageSeed;

		// Token: 0x040023F3 RID: 9203
		public int seedCounter;

		// Token: 0x040023F4 RID: 9204
		public Augments augments;

		// Token: 0x040023F5 RID: 9205
		public Progression.Loadout loadout;

		// Token: 0x040023F6 RID: 9206
		public Dictionary<int, int> statuses;
	}

	// Token: 0x020004AB RID: 1195
	[CompilerGenerated]
	private sealed class <>c__DisplayClass57_0
	{
		// Token: 0x0600225A RID: 8794 RVA: 0x000C6A28 File Offset: 0x000C4C28
		public <>c__DisplayClass57_0()
		{
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000C6A30 File Offset: 0x000C4C30
		internal void <SendCacheNetwork>b__0()
		{
			this.dataCache.Apply(PlayerControl.myInstance);
		}

		// Token: 0x040023F7 RID: 9207
		public PlayerNetwork.PlayerDataCache dataCache;
	}
}
