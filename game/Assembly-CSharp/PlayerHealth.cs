using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class PlayerHealth : EntityHealth
{
	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000785 RID: 1925 RVA: 0x0003639A File Offset: 0x0003459A
	public PlayerControl Control
	{
		get
		{
			return this.control as PlayerControl;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000786 RID: 1926 RVA: 0x000363A8 File Offset: 0x000345A8
	public int AutoReviveCount
	{
		get
		{
			if (RaidManager.IsInRaid)
			{
				return 0;
			}
			int num = (int)this.Control.GetPassiveMod(Passive.EntityValue.P_AutoRevives, 0f);
			if (GameplayManager.IsChallengeActive)
			{
				num++;
			}
			return Mathf.Max(0, num);
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000787 RID: 1927 RVA: 0x000363E7 File Offset: 0x000345E7
	public bool HasAutoRevivesAvailable
	{
		get
		{
			return this.AutoReviveCount > this.AutoRevivesUsed;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000788 RID: 1928 RVA: 0x000363F7 File Offset: 0x000345F7
	public bool CanSelfRevive
	{
		get
		{
			return PlayerControl.PlayerCount > 1;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000789 RID: 1929 RVA: 0x00036401 File Offset: 0x00034601
	private bool isLowHealth
	{
		get
		{
			return (float)this.health + this.shield < (float)base.MaxHealth * 0.5f;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600078A RID: 1930 RVA: 0x00036420 File Offset: 0x00034620
	public bool LowHealthDanger
	{
		get
		{
			return (!RaidManager.IsInRaid && this.oneshotCD > 0f) || (this.isLowHealth && this.lowHealthTime < 3.5f);
		}
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00036450 File Offset: 0x00034650
	public override void Setup()
	{
		base.Setup();
		this.soloEndT = this.SoloEndTime;
		this.OnDie = (Action<DamageInfo>)Delegate.Combine(this.OnDie, new Action<DamageInfo>(this.OnDied));
		this.OnRevive = (Action<int>)Delegate.Combine(this.OnRevive, new Action<int>(this.DisableGhost));
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000364B4 File Offset: 0x000346B4
	internal override void Update()
	{
		base.Update();
		if (this.oneshotCD > 0f)
		{
			this.oneshotCD -= GameplayManager.deltaTime;
		}
		if (this.oneshotInvulnT > 0f)
		{
			this.oneshotInvulnT -= GameplayManager.deltaTime;
		}
		if (this.isLowHealth)
		{
			this.lowHealthTime += GameplayManager.deltaTime;
		}
		else
		{
			this.lowHealthTime = 0f;
		}
		if (this.isDead)
		{
			if (GameplayManager.IsInGame)
			{
				if (!this.isAutoReviving)
				{
					if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1)
					{
						this.MultiplayerWhileDead();
						return;
					}
					if (this.HasAutoRevivesAvailable)
					{
						this.StartAutoRevive();
						return;
					}
					this.SoloWhileDead();
					return;
				}
			}
			else
			{
				this.Revive(1f);
			}
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00036580 File Offset: 0x00034780
	public override void ApplyDamageImmediate(DamageInfo dmg)
	{
		if (GameplayManager.CurState != GameState.InWave && GameplayManager.CurState != GameState.EXPLICIT_CMD && !RaidManager.IsInRaid)
		{
			return;
		}
		base.ApplyDamageLeftover(ref dmg);
		base.ApplyDamageShield(ref dmg);
		if (this.oneshotInvulnT > 0f && !dmg.CanOneShot)
		{
			return;
		}
		int num = (int)dmg.Amount;
		if (num >= this.health && this.oneshotCD <= 0f && !dmg.CanOneShot)
		{
			num = Mathf.Max(0, this.health - UnityEngine.Random.Range(1, 5));
			dmg.Amount = (float)num;
			if (RaidManager.IsInRaid)
			{
				this.oneshotInvulnT = ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard) ? this.Raid_H_OSInv : this.Raid_OSInv);
				this.oneshotCD = ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard) ? this.Raid_H_OSCD : this.Raid_OSCD);
			}
			else
			{
				this.oneshotInvulnT = this.OneShotInvulnTime;
				this.oneshotCD = this.OnshotCooldown;
			}
			this.lowHealthTime = 0f;
		}
		this.health -= num;
		this.health = Mathf.Clamp(this.health, 0, base.MaxHealth);
		this.lastDamagedTime = Time.realtimeSinceStartup;
		Action<DamageInfo> onDamageTaken = this.OnDamageTaken;
		if (onDamageTaken != null)
		{
			onDamageTaken(dmg);
		}
		if (this.health <= 0 && base.isLocalControl)
		{
			base.DieFromDamage(dmg);
		}
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x000366E4 File Offset: 0x000348E4
	public override void Reset()
	{
		base.Reset();
		this.soloEndT = this.SoloEndTime;
		this.oneshotCD = 0f;
		this.ReviveProgress = 0.3f;
		this.LastStandReviveCount = 0f;
		this.GroupRezTimer = this.GroupReviveTime;
		this.AutoRevivesUsed = 0;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00036737 File Offset: 0x00034937
	public void ReviveFromGhost()
	{
		this.Revive(1f);
		this.Control.Movement.SetPositionImmediate(this.Ghost.groundPoint, this.Control.Movement.GetForward(), true);
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00036770 File Offset: 0x00034970
	public override void Revive(float portionHeal = 1f)
	{
		if (!this.isDead)
		{
			return;
		}
		this.ReviveProgress = 0f;
		this.oneshotCD = 0f;
		this.soloEndT = this.SoloEndTime;
		this.GroupRezTimer = this.GroupReviveTime;
		base.Revive(portionHeal);
		this.Control.Movement.ResetVelocity();
		this.isAutoReviving = false;
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x000367D2 File Offset: 0x000349D2
	private void SoloWhileDead()
	{
		this.soloEndT -= GameplayManager.deltaTime;
		if (this.soloEndT <= 0f)
		{
			if (RaidManager.IsInRaid)
			{
				RaidManager.instance.EncounterFailed();
				return;
			}
			GameplayManager.instance.EndGame(false);
		}
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x00036810 File Offset: 0x00034A10
	private void MultiplayerWhileDead()
	{
		if (this.ReviveProgress >= 1f && this.Control.IsDead)
		{
			this.isAutoReviving = true;
			base.StartCoroutine("AutoReviveSequence");
			this.LastStandReviveCount += 1f;
			return;
		}
		if (this.HasAutoRevivesAvailable && (PlayerInput.myInstance.interactPressed || PlayerControl.DeadPlayersCount >= PlayerControl.AllPlayers.Count))
		{
			this.StartAutoRevive();
		}
		this.CheckGhostNearbyPlayers();
		if (this.GroupReviveTime >= 0f)
		{
			this.GroupRezTimer -= GameplayManager.deltaTime;
		}
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x000368B0 File Offset: 0x00034AB0
	private void CheckGhostNearbyPlayers()
	{
		int num = PlayerControl.PlayerCount;
		if (num <= 1)
		{
			return;
		}
		num--;
		int num2 = 0;
		Vector3 position = this.Ghost.transform.position;
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!playerControl.IsDead && !(playerControl == this.Control) && Vector3.Distance(playerControl.movement.GetPosition(), position) <= 5f)
			{
				num2++;
			}
		}
		float num3 = (float)num2 / (float)num;
		this.IncreaseReviveProgress(num3 * (Time.deltaTime / this.TeamRevTime) * this.DeathCountProgressMult.Evaluate(this.LastStandReviveCount));
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00036980 File Offset: 0x00034B80
	private void DebugRevProgress()
	{
		this.IncreaseReviveProgress(0.25f);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00036990 File Offset: 0x00034B90
	public void DamageDone(DamageInfo dmg)
	{
		float num = this.DeathCountProgressMult.Evaluate(this.LastStandReviveCount);
		float num2 = this.DamageProgressValue.Evaluate(dmg.TotalAmount);
		float num3 = this.WaveProgressMult.Evaluate((float)WaveManager.instance.WavesCompleted);
		float num4 = num2 * num * num3;
		if (PlayerControl.PlayerCount > 1)
		{
			num4 *= 0.25f;
			float num5 = 0.75f;
			if (this.ReviveProgress + num4 > num5)
			{
				num4 = num5 - this.ReviveProgress;
			}
		}
		if (this.CanSelfRevive)
		{
			this.IncreaseReviveProgress(num4);
			this.RezDeclinePause = 1.5f;
		}
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00036A20 File Offset: 0x00034C20
	public void IncreaseReviveProgress(float amount)
	{
		this.ReviveProgress += Mathf.Max(amount, 0f);
		if (this.ReviveProgress >= 1f)
		{
			this.ReviveProgress = 1f;
		}
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00036A52 File Offset: 0x00034C52
	public void ActivateGhost(Vector3 pos)
	{
		this.Ghost.Activate(pos);
		GameRecord.RecordEvent(GameRecord.EventType.Player_Died, this.Control, pos, null);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00036A6E File Offset: 0x00034C6E
	public void DisableGhost(int withHealth)
	{
		this.Ghost.gameObject.SetActive(false);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00036A81 File Offset: 0x00034C81
	public void StartAutoRevive()
	{
		if (this.isAutoReviving)
		{
			return;
		}
		this.isAutoReviving = true;
		this.AutoRevivesUsed++;
		base.StartCoroutine("AutoReviveSequence");
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00036AAD File Offset: 0x00034CAD
	private IEnumerator AutoReviveSequence()
	{
		yield return new WaitForSeconds(0.33f);
		if (this.Control.IsMine)
		{
			AudioManager.PlayLoudClipAtPoint(this.AutoReviveSFX, this.Ghost.groundPoint, 1f, 1f, 0.3f, 20f, 300f);
		}
		else
		{
			AudioManager.PlayClipAtPoint(this.AutoReviveSFX, this.Ghost.groundPoint, 1f, 1f, 0.8f, 20f, 300f);
		}
		UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.AutoReviveVFX, this.Ghost.groundPoint, Quaternion.identity), 5f);
		yield return new WaitForSeconds(1.75f);
		this.ReviveFromGhost();
		this.isAutoReviving = false;
		yield break;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x00036ABC File Offset: 0x00034CBC
	private void OnDied(DamageInfo dmg)
	{
		this.health = 0;
		this.shield = 0f;
		this.ReviveProgress = 0.3f;
		this.GroupRezTimer = this.GroupReviveTime;
		this.Control.Net.ActivateGhost(this.Control.Movement.GetPosition());
		this.DecayRate = 0.05f * Mathf.Max(this.LastStandReviveCount, 3f);
		this.RezDeclinePause = 1f;
		if (this.Control.IsMine)
		{
			DeathRecap.OnDeath(dmg);
			AIControl aicontrol = EntityControl.GetEntity(dmg.SourceID) as AIControl;
			if (aicontrol != null && !string.IsNullOrEmpty(aicontrol.StatID))
			{
				GameStats.IncrementEnemyStat(aicontrol.StatID, true);
			}
			AIControl boss = AIManager.GetBoss();
			if (boss != null && boss.StatID != null)
			{
				GameStats.IncrementEnemyStat(boss.StatID, true);
			}
			GameStats.SaveIfNeeded();
		}
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00036BA4 File Offset: 0x00034DA4
	public PlayerHealth()
	{
	}

	// Token: 0x0400063E RID: 1598
	[Range(0f, 1f)]
	public float MinProtectionHealth = 0.3f;

	// Token: 0x0400063F RID: 1599
	public float OnshotCooldown = 1f;

	// Token: 0x04000640 RID: 1600
	public float OneShotInvulnTime = 0.5f;

	// Token: 0x04000641 RID: 1601
	public float Raid_OSCD = 1f;

	// Token: 0x04000642 RID: 1602
	public float Raid_OSInv = 0.5f;

	// Token: 0x04000643 RID: 1603
	public float Raid_H_OSCD = 1f;

	// Token: 0x04000644 RID: 1604
	public float Raid_H_OSInv = 0.5f;

	// Token: 0x04000645 RID: 1605
	private float oneshotCD;

	// Token: 0x04000646 RID: 1606
	private float oneshotInvulnT;

	// Token: 0x04000647 RID: 1607
	[Tooltip("How fast does the Progress bar decay in solo games")]
	public float DecayRate = 0.05f;

	// Token: 0x04000648 RID: 1608
	private float GroupReviveTime = 2.5f;

	// Token: 0x04000649 RID: 1609
	[Tooltip("How long it takes for allies to revive a teammate")]
	[Range(1f, 15f)]
	public float TeamRevTime = 5f;

	// Token: 0x0400064A RID: 1610
	public AnimationCurve DamageProgressValue;

	// Token: 0x0400064B RID: 1611
	public AnimationCurve DeathCountProgressMult;

	// Token: 0x0400064C RID: 1612
	public AnimationCurve WaveProgressMult;

	// Token: 0x0400064D RID: 1613
	public GhostPlayerDisplay Ghost;

	// Token: 0x0400064E RID: 1614
	public float LastStandReviveCount;

	// Token: 0x0400064F RID: 1615
	public float ReviveProgress;

	// Token: 0x04000650 RID: 1616
	public float GroupRezTimer;

	// Token: 0x04000651 RID: 1617
	public float RezDeclinePause;

	// Token: 0x04000652 RID: 1618
	public float SoloEndTime = 1f;

	// Token: 0x04000653 RID: 1619
	private float soloEndT;

	// Token: 0x04000654 RID: 1620
	public int AutoRevivesUsed;

	// Token: 0x04000655 RID: 1621
	[NonSerialized]
	public bool isAutoReviving;

	// Token: 0x04000656 RID: 1622
	public GameObject AutoReviveVFX;

	// Token: 0x04000657 RID: 1623
	public AudioClip AutoReviveSFX;

	// Token: 0x04000658 RID: 1624
	private float lowHealthTime;

	// Token: 0x020004B1 RID: 1201
	[CompilerGenerated]
	private sealed class <AutoReviveSequence>d__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x000C6AA7 File Offset: 0x000C4CA7
		[DebuggerHidden]
		public <AutoReviveSequence>d__54(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000C6AB6 File Offset: 0x000C4CB6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000C6AB8 File Offset: 0x000C4CB8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerHealth playerHealth = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.33f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				if (playerHealth.Control.IsMine)
				{
					AudioManager.PlayLoudClipAtPoint(playerHealth.AutoReviveSFX, playerHealth.Ghost.groundPoint, 1f, 1f, 0.3f, 20f, 300f);
				}
				else
				{
					AudioManager.PlayClipAtPoint(playerHealth.AutoReviveSFX, playerHealth.Ghost.groundPoint, 1f, 1f, 0.8f, 20f, 300f);
				}
				UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(playerHealth.AutoReviveVFX, playerHealth.Ghost.groundPoint, Quaternion.identity), 5f);
				this.<>2__current = new WaitForSeconds(1.75f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				playerHealth.ReviveFromGhost();
				playerHealth.isAutoReviving = false;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000C6BD0 File Offset: 0x000C4DD0
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000C6BD8 File Offset: 0x000C4DD8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000C6BDF File Offset: 0x000C4DDF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400240C RID: 9228
		private int <>1__state;

		// Token: 0x0400240D RID: 9229
		private object <>2__current;

		// Token: 0x0400240E RID: 9230
		public PlayerHealth <>4__this;
	}
}
