using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200006B RID: 107
public class AINetworked : EntityNetworked, IOnPhotonViewControllerChange, IPhotonViewCallback
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000403 RID: 1027 RVA: 0x000200C5 File Offset: 0x0001E2C5
	internal AIControl Control
	{
		get
		{
			return this.control as AIControl;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000404 RID: 1028 RVA: 0x000200D2 File Offset: 0x0001E2D2
	private static int SendRate
	{
		get
		{
			if (AIManager.AliveEnemies > 20)
			{
				return 3;
			}
			if (AIManager.AliveEnemies > 12)
			{
				return 2;
			}
			return 1;
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x000200EC File Offset: 0x0001E2EC
	public void AbilityActivated(AbilityTree ability, Vector3 targetPt, int seed)
	{
		if (PhotonNetwork.InRoom)
		{
			this.view.RPC("AbilityActivatedNetwork", RpcTarget.All, new object[]
			{
				ability.RootNode.guid,
				targetPt,
				seed
			});
			return;
		}
		this.AbilityActivatedNetwork(ability.RootNode.guid, targetPt, seed);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0002014B File Offset: 0x0001E34B
	[PunRPC]
	public void AbilityActivatedNetwork(string AbilityGUID, Vector3 targetPt, int seed)
	{
		(this.control as AIControl).ActivateAbilityLocal(AbilityGUID, targetPt, seed);
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00020160 File Offset: 0x0001E360
	public void AbilityReleased(AbilityRootNode ability)
	{
		if (PhotonNetwork.InRoom)
		{
			this.view.RPC("AbilityReleasedNetwork", RpcTarget.All, new object[]
			{
				ability.guid
			});
			return;
		}
		this.AbilityReleasedNetwork(ability.guid);
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00020196 File Offset: 0x0001E396
	[PunRPC]
	public void AbilityReleasedNetwork(string AbilityGUID)
	{
		(this.control as AIControl).ReleaseAbility(AbilityGUID);
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000201A9 File Offset: 0x0001E3A9
	public void SendTags()
	{
		if (!this.view.IsMine)
		{
			return;
		}
		this.view.RPC("SendTagsNetwork", RpcTarget.Others, new object[]
		{
			this.Control.AllTags()
		});
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000201DE File Offset: 0x0001E3DE
	[PunRPC]
	public void SendTagsNetwork(string TagStr)
	{
		this.Control.SetTags(TagStr);
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x000201EC File Offset: 0x0001E3EC
	public void ActivateEvent(string EventID)
	{
		foreach (AINetworked.NetEvent netEvent in this.Events)
		{
			if (!(netEvent.EventID != EventID) && netEvent.CanTrigger)
			{
				this.view.RPC("ActivateEventNetwork", RpcTarget.All, new object[]
				{
					EventID
				});
				break;
			}
		}
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0002026C File Offset: 0x0001E46C
	[PunRPC]
	internal void ActivateEventNetwork(string EventID)
	{
		foreach (AINetworked.NetEvent netEvent in this.Events)
		{
			if (!(netEvent.EventID != EventID) && netEvent.CanTrigger)
			{
				netEvent.Trigger();
				break;
			}
		}
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000202D8 File Offset: 0x0001E4D8
	public void SetPetOwnership(EntityControl owner)
	{
		int num = -1;
		if (owner != null)
		{
			num = owner.ViewID;
		}
		this.view.RPC("SetPetOwnershipNetwork", RpcTarget.All, new object[]
		{
			num
		});
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00020317 File Offset: 0x0001E517
	[PunRPC]
	internal void SetPetOwnershipNetwork(int PetOwnerID)
	{
		this.Control.SetPetOwner(PetOwnerID);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00020328 File Offset: 0x0001E528
	public void SetOwnership(EntityControl owner, string nodeID)
	{
		int num = -1;
		if (owner != null)
		{
			num = owner.ViewID;
		}
		this.view.RPC("SetOwnershipNetwork", RpcTarget.All, new object[]
		{
			num,
			nodeID
		});
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0002036B File Offset: 0x0001E56B
	[PunRPC]
	public void SetOwnershipNetwork(int ownerID, string guid)
	{
		this.OwnerID = ownerID;
		this.GraphSource = guid;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0002037C File Offset: 0x0001E57C
	public void ChangeBrain(AITree tree)
	{
		if (tree == null || (this.Control.behaviourTree != null && tree.ID == this.Control.behaviourTree.ID))
		{
			return;
		}
		if (!this.Control.IsMine)
		{
			return;
		}
		this.view.RPC("ChangeBrainNetwork", RpcTarget.All, new object[]
		{
			tree.ID
		});
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x000203F4 File Offset: 0x0001E5F4
	[PunRPC]
	public void ChangeBrainNetwork(string ID)
	{
		AITree aitree = GraphDB.GetAITree(ID);
		if (aitree == null)
		{
			return;
		}
		this.Control.ChangeBrain(aitree);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0002041E File Offset: 0x0001E61E
	public void TriggerEffect(int index)
	{
		this.view.RPC("TriggerEffectNetwork", RpcTarget.All, new object[]
		{
			index,
			PlayerControl.myInstance.ViewID
		});
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00020452 File Offset: 0x0001E652
	[PunRPC]
	private void TriggerEffectNetwork(int index, int playerID)
	{
		this.Control.TriggerAIEffect(index, playerID);
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00020461 File Offset: 0x0001E661
	public void TransformInto(AIData.AIDetails data)
	{
		this.view.RPC("TransformIntoNetwork", RpcTarget.All, new object[]
		{
			data.ControlRef.StatID
		});
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00020488 File Offset: 0x0001E688
	[PunRPC]
	private void TransformIntoNetwork(string EntityID)
	{
		AIData.AIDetails enemyByID = AIManager.instance.DB.GetEnemyByID(EntityID);
		if (enemyByID == null)
		{
			return;
		}
		this.Control.TransformInto(enemyByID.Reference);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x000204BB File Offset: 0x0001E6BB
	internal override void PlayerConnected(Player Player)
	{
		this.SendTags();
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000204C4 File Offset: 0x0001E6C4
	public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		this.SendIndex++;
		if (stream.IsWriting)
		{
			bool flag = this.SendIndex % AINetworked.SendRate == 0;
			stream.SendNext(flag);
			if (!flag)
			{
				return;
			}
			stream.SendNext(this.Control.Movement.GetTargetPoint());
			stream.SendNext((this.Control.currentTarget == null) ? -1 : this.Control.currentTarget.net.view.ViewID);
			stream.SendNext((this.Control.allyTarget == null) ? -1 : this.Control.allyTarget.net.view.ViewID);
		}
		else
		{
			if (!(bool)stream.ReceiveNext())
			{
				return;
			}
			this.Control.Movement.SetTargetPoint((Vector3)stream.ReceiveNext());
			this.Control.SetTargetByViewID((int)stream.ReceiveNext());
			this.Control.SetAllyTargetByViewID((int)stream.ReceiveNext());
		}
		base.OnPhotonSerializeView(stream, info);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000205FC File Offset: 0x0001E7FC
	public void OnControllerChange(Player newOwner, Player prevOwner)
	{
		if (prevOwner == null)
		{
			return;
		}
		PlayerControl player = PlayerControl.GetPlayer(newOwner.ActorNumber);
		if (player == null)
		{
			return;
		}
		this.Control.SetTarget(player);
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0002062F File Offset: 0x0001E82F
	public AINetworked()
	{
	}

	// Token: 0x040003A0 RID: 928
	public List<AINetworked.NetEvent> Events;

	// Token: 0x040003A1 RID: 929
	[NonSerialized]
	public int OwnerID = -1;

	// Token: 0x040003A2 RID: 930
	[NonSerialized]
	public string GraphSource = "";

	// Token: 0x040003A3 RID: 931
	internal int SendIndex;

	// Token: 0x0200048D RID: 1165
	[Serializable]
	public class NetEvent
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000C4483 File Offset: 0x000C2683
		public bool CanTrigger
		{
			get
			{
				return this.CanRepeat || !this.WasTriggered;
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000C4498 File Offset: 0x000C2698
		public void Trigger()
		{
			if (!this.CanRepeat && this.WasTriggered)
			{
				return;
			}
			this.WasTriggered = true;
			UnityEvent onEvent = this.OnEvent;
			if (onEvent == null)
			{
				return;
			}
			onEvent.Invoke();
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000C44C2 File Offset: 0x000C26C2
		public NetEvent()
		{
		}

		// Token: 0x0400231E RID: 8990
		public string EventID;

		// Token: 0x0400231F RID: 8991
		public bool CanRepeat;

		// Token: 0x04002320 RID: 8992
		private bool WasTriggered;

		// Token: 0x04002321 RID: 8993
		public UnityEvent OnEvent;
	}
}
