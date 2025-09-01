using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class EntityNetworked : MonoBehaviour, IPunObservable
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000575 RID: 1397 RVA: 0x000279F0 File Offset: 0x00025BF0
	private static float STATUS_SEND_RATE
	{
		get
		{
			int num = Mathf.Clamp(AIManager.AliveEnemies, 0, 20);
			return Mathf.Lerp(0.2f, 0.75f, (float)num / 20f);
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x00027A24 File Offset: 0x00025C24
	private static float DAMAGE_SEND_RATE
	{
		get
		{
			int num = Mathf.Clamp(AIManager.AliveEnemies, 0, 20);
			return Mathf.Lerp(0.1f, 0.3f, (float)num / 20f);
		}
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00027A56 File Offset: 0x00025C56
	public virtual void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		this.control = base.GetComponent<EntityControl>();
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00027A70 File Offset: 0x00025C70
	public virtual void Setup()
	{
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00027A74 File Offset: 0x00025C74
	private void Update()
	{
		if (this.control.IsDead)
		{
			return;
		}
		if (this.damageSendT > 0f)
		{
			this.damageSendT -= Time.unscaledDeltaTime;
		}
		else
		{
			this.PropagateDamageDelayed();
		}
		if (this.statusSendT > 0f)
		{
			this.statusSendT -= Time.unscaledDeltaTime;
			return;
		}
		this.SendBatchedStatuses();
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00027ADC File Offset: 0x00025CDC
	public void PropagateDamage(DamageInfo info)
	{
		if (this.view.IsMine)
		{
			this.control.health.ApplyDamageImmediate(info);
			return;
		}
		this.toPropagate.Enqueue(info);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00027B0C File Offset: 0x00025D0C
	private void PropagateDamageDelayed()
	{
		if (this.toPropagate.Count <= 0)
		{
			return;
		}
		this.damageSendT = EntityNetworked.DAMAGE_SEND_RATE;
		if (this.view.IsMine)
		{
			while (this.toPropagate.Count > 0)
			{
				this.control.health.ApplyDamageImmediate(this.toPropagate.Dequeue());
			}
			return;
		}
		DamageInfo damageInfo = this.toPropagate.Dequeue();
		while (this.toPropagate.Count > 0)
		{
			damageInfo.Add(this.toPropagate.Dequeue());
		}
		this.view.RPC("PropagateDamageNetwork", this.view.Owner, new object[]
		{
			damageInfo
		});
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00027BBE File Offset: 0x00025DBE
	[PunRPC]
	public void PropagateDamageNetwork(DamageInfo info)
	{
		this.control.health.ApplyDamageImmediate(info);
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00027BD1 File Offset: 0x00025DD1
	public void ExecuteActionTree(string ActionGUID, EffectProperties props)
	{
		this.control.RunSnippetLocal(ActionGUID, props);
		if (PlayerControl.PlayerCount > 1)
		{
			this.view.RPC("ExecuteActionNetworked", RpcTarget.Others, new object[]
			{
				ActionGUID,
				props.ToString()
			});
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00027C0C File Offset: 0x00025E0C
	[PunRPC]
	public void ExecuteActionNetworked(string ActionGUID, string props)
	{
		EffectProperties props2 = new EffectProperties(props);
		this.control.RunSnippetLocal(ActionGUID, props2);
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00027C30 File Offset: 0x00025E30
	public void ApplyStatusBatched(int StatusID, int sourceID, float duration, int stacks, bool delay, int depth)
	{
		if (!this.control.AddStatusEffect(StatusID, sourceID, duration, stacks, delay, depth))
		{
			return;
		}
		if (PlayerControl.PlayerCount > 1)
		{
			if (this.toPropStatuses.ContainsKey(StatusID))
			{
				this.toPropStatuses[StatusID].Add(duration, stacks);
				return;
			}
			this.toPropStatuses.Add(StatusID, new EntityNetworked.StatusPropInfo(sourceID, duration, stacks, delay, depth));
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00027C9C File Offset: 0x00025E9C
	public void ApplyStatus(int StatusID, int sourceID, float duration, int stacks, bool delay, int depth)
	{
		if (!this.control.AddStatusEffect(StatusID, sourceID, duration, stacks, delay, depth))
		{
			return;
		}
		if (PlayerControl.PlayerCount > 1)
		{
			this.view.RPC("ApplyStatusNetwork", RpcTarget.Others, new object[]
			{
				StatusID,
				sourceID,
				duration,
				stacks,
				delay,
				depth
			});
		}
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00027D18 File Offset: 0x00025F18
	[PunRPC]
	public void ApplyStatusNetwork(int StatusID, int sourceID, float duration, int stacks, bool delay, int depth)
	{
		this.control.AddStatusEffect(StatusID, sourceID, duration, stacks, delay, depth);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00027D30 File Offset: 0x00025F30
	public void RemoveStatusBatched(int StatusID, int sourceID, int stacks, bool delay, bool byAll)
	{
		if (!this.control.HasStatusEffectHash(StatusID) && !this.toPropStatuses.ContainsKey(StatusID))
		{
			return;
		}
		if (this.toPropStatuses.ContainsKey(StatusID))
		{
			if (stacks <= 0)
			{
				this.toPropStatuses.Remove(StatusID);
			}
			else if (stacks <= this.toPropStatuses[StatusID].stacks)
			{
				this.toPropStatuses[StatusID].RemoveStacks(stacks);
			}
			else
			{
				int num = stacks - this.toPropStatuses[StatusID].stacks;
				this.toPropStatuses.Remove(StatusID);
				this.view.RPC("RemoveStatusNetwork", RpcTarget.Others, new object[]
				{
					StatusID,
					sourceID,
					num,
					delay,
					byAll
				});
			}
			this.control.RemoveStatusEffect(StatusID, sourceID, stacks, delay, byAll);
			return;
		}
		this.view.RPC("RemoveStatusNetwork", RpcTarget.All, new object[]
		{
			StatusID,
			sourceID,
			stacks,
			delay,
			byAll
		});
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00027E6C File Offset: 0x0002606C
	public void RemoveStatus(int StatusID, int sourceID, int stacks, bool delay, bool byAll)
	{
		if (!this.control.HasStatusEffectHash(StatusID))
		{
			return;
		}
		this.view.RPC("RemoveStatusNetwork", RpcTarget.All, new object[]
		{
			StatusID,
			sourceID,
			stacks,
			delay,
			byAll
		});
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00027ECE File Offset: 0x000260CE
	[PunRPC]
	public void RemoveStatusNetwork(int StatusID, int sourceID, int stacks, bool delay, bool byAll)
	{
		this.control.RemoveStatusEffect(StatusID, sourceID, stacks, delay, byAll);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00027EE4 File Offset: 0x000260E4
	private void SendBatchedStatuses()
	{
		if (this.toPropStatuses.Count <= 0)
		{
			return;
		}
		this.statusSendT = EntityNetworked.STATUS_SEND_RATE;
		foreach (KeyValuePair<int, EntityNetworked.StatusPropInfo> keyValuePair in this.toPropStatuses)
		{
			int num;
			EntityNetworked.StatusPropInfo statusPropInfo;
			keyValuePair.Deconstruct(out num, out statusPropInfo);
			int num2 = num;
			EntityNetworked.StatusPropInfo statusPropInfo2 = statusPropInfo;
			this.view.RPC("ApplyStatusNetwork", RpcTarget.Others, new object[]
			{
				num2,
				statusPropInfo2.sourceID,
				statusPropInfo2.duration,
				statusPropInfo2.stacks,
				statusPropInfo2.delay,
				statusPropInfo2.depth
			});
		}
		this.toPropStatuses.Clear();
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00027FD0 File Offset: 0x000261D0
	public void ClearStatuses(bool fromDeath)
	{
		this.view.RPC("ClearStatusesNetwork", RpcTarget.Others, new object[]
		{
			fromDeath
		});
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00027FF2 File Offset: 0x000261F2
	[PunRPC]
	public void ClearStatusesNetwork(bool fromDeath)
	{
		this.control.RemoveAllStatuses(fromDeath);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00028000 File Offset: 0x00026200
	public void Kill(DamageInfo dmg)
	{
		if (!this.view.IsMine)
		{
			this.view.RPC("KillNetwork", this.view.Owner, new object[]
			{
				dmg
			});
			return;
		}
		this.KillNetwork(dmg);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0002803C File Offset: 0x0002623C
	[PunRPC]
	public void KillNetwork(DamageInfo dmg)
	{
		this.control.health.Die(dmg);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00028050 File Offset: 0x00026250
	public virtual void SendAugments(Player player = null)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		if (player == null)
		{
			this.view.RPC("SendAugmentsNetwork", RpcTarget.Others, new object[]
			{
				this.control.Augment.ToString()
			});
			return;
		}
		this.view.RPC("SendAugmentsNetwork", player, new object[]
		{
			this.control.Augment.ToString()
		});
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x000280C4 File Offset: 0x000262C4
	[PunRPC]
	public void SendAugmentsNetwork(string AugmentStr)
	{
		if (this.view.IsMine)
		{
			return;
		}
		this.control.Augment = new Augments(AugmentStr);
		PlayerControl playerControl = this.control as PlayerControl;
		if (playerControl != null)
		{
			playerControl.AugmentsChanged();
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00028105 File Offset: 0x00026305
	public void AddAugment(string AugmentID)
	{
		if (this.view.IsMine || PhotonNetwork.IsMasterClient)
		{
			this.view.RPC("AddAugmentNetwork", RpcTarget.All, new object[]
			{
				AugmentID
			});
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00028136 File Offset: 0x00026336
	[PunRPC]
	public void AddAugmentNetwork(string AugmentID)
	{
		this.control.AddAugment(GraphDB.GetAugment(AugmentID), 1);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0002814A File Offset: 0x0002634A
	public void ToggleTargetable(bool isTargetable, bool isAffectable)
	{
		if (this.view.IsMine || PhotonNetwork.IsMasterClient)
		{
			this.view.RPC("ToggleTargetableNetwork", RpcTarget.All, new object[]
			{
				isTargetable,
				isAffectable
			});
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0002818C File Offset: 0x0002638C
	[PunRPC]
	public void ToggleTargetableNetwork(bool isTargetable, bool isAffectable)
	{
		this.control.Targetable = isTargetable;
		this.control.Affectable = isAffectable;
		if (!this.control.Targetable && PlayerControl.myInstance != null && PlayerControl.myInstance.currentTarget == this.control)
		{
			PlayerControl.myInstance.ForceClearTarget();
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x000281EC File Offset: 0x000263EC
	public void OnPlayerEnteredRoom(Player Player)
	{
		if (!this.view.IsMine)
		{
			return;
		}
		foreach (EntityControl.AppliedStatus appliedStatus in this.control.Statuses)
		{
			this.view.RPC("ApplyStatusNetwork", Player, new object[]
			{
				appliedStatus.rootNode.guid,
				appliedStatus.sourceID,
				appliedStatus.Duration,
				appliedStatus.Stacks
			});
		}
		this.PlayerConnected(Player);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000282A4 File Offset: 0x000264A4
	internal virtual void PlayerConnected(Player Player)
	{
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x000282A8 File Offset: 0x000264A8
	public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(this.control.TeamID);
		}
		else
		{
			int newTeam = (int)stream.ReceiveNext();
			this.control.ChangeTeam(newTeam);
		}
		if (this.control.health != null)
		{
			EntityHealth component = base.GetComponent<EntityHealth>();
			if (stream.IsWriting)
			{
				stream.SendNext(this.control.LastDamageTaken);
				stream.SendNext(this.control.LastDamageTakenPoint);
				stream.SendNext(component.health);
				stream.SendNext(component.shield);
				stream.SendNext(component.shieldDelay);
				stream.SendNext(component.isDead);
			}
			else
			{
				this.control.LastDamageTaken = (float)stream.ReceiveNext();
				this.control.LastDamageTakenPoint = (Vector3)stream.ReceiveNext();
				int hp = (int)stream.ReceiveNext();
				float shield = (float)stream.ReceiveNext();
				float shieldDelay = (float)stream.ReceiveNext();
				bool dead = (bool)stream.ReceiveNext();
				component.UpdateHealthFromNetwork(hp, shield, shieldDelay, dead);
			}
		}
		if (this.control.movement != null)
		{
			EntityMovement component2 = base.GetComponent<EntityMovement>();
			if (stream.IsWriting)
			{
				stream.SendNext(component2.IsMoverEnabled());
				stream.SendNext(component2.GetPosition());
				stream.SendNext(component2.GetRotation());
				stream.SendNext(component2.GetVelocity());
				stream.SendNext(component2.GetForward());
				return;
			}
			bool flag = (bool)stream.ReceiveNext();
			Vector3 pos = (Vector3)stream.ReceiveNext();
			Quaternion rot = (Quaternion)stream.ReceiveNext();
			Vector3 vel = (Vector3)stream.ReceiveNext();
			Vector3 wantForward = (Vector3)stream.ReceiveNext();
			if (flag)
			{
				component2.UpdateFromNetwork(pos, rot, vel);
				component2.wantForward = wantForward;
			}
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x000284C6 File Offset: 0x000266C6
	public void Destroy()
	{
		if (!PhotonNetwork.InRoom)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (!this.view.IsMine)
		{
			return;
		}
		PhotonNetwork.Destroy(this.view);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000284F4 File Offset: 0x000266F4
	public EntityNetworked()
	{
	}

	// Token: 0x04000444 RID: 1092
	[NonSerialized]
	public PhotonView view;

	// Token: 0x04000445 RID: 1093
	[NonSerialized]
	public EntityControl control;

	// Token: 0x04000446 RID: 1094
	private Dictionary<int, EntityNetworked.StatusPropInfo> toPropStatuses = new Dictionary<int, EntityNetworked.StatusPropInfo>();

	// Token: 0x04000447 RID: 1095
	private float statusSendT;

	// Token: 0x04000448 RID: 1096
	private Queue<DamageInfo> toPropagate = new Queue<DamageInfo>();

	// Token: 0x04000449 RID: 1097
	private float damageSendT;

	// Token: 0x0200049B RID: 1179
	private struct StatusPropInfo
	{
		// Token: 0x0600221D RID: 8733 RVA: 0x000C59E7 File Offset: 0x000C3BE7
		public StatusPropInfo(int sourceID, float duration, int stacks, bool delay, int depth)
		{
			this.sourceID = sourceID;
			this.duration = duration;
			this.stacks = stacks;
			this.delay = delay;
			this.depth = depth;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000C5A0E File Offset: 0x000C3C0E
		public void Add(float d, int s)
		{
			if (d <= 0f)
			{
				this.duration = d;
			}
			else if (this.duration > 0f)
			{
				this.duration = Mathf.Max(this.duration, d);
			}
			this.stacks += s;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000C5A4E File Offset: 0x000C3C4E
		public void RemoveStacks(int value)
		{
			this.stacks -= value;
		}

		// Token: 0x04002366 RID: 9062
		public int sourceID;

		// Token: 0x04002367 RID: 9063
		public float duration;

		// Token: 0x04002368 RID: 9064
		public int stacks;

		// Token: 0x04002369 RID: 9065
		public bool delay;

		// Token: 0x0400236A RID: 9066
		public int depth;
	}
}
