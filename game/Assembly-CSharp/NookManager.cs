using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class NookManager : MonoBehaviour
{
	// Token: 0x06000DB6 RID: 3510 RVA: 0x00057CA1 File Offset: 0x00055EA1
	private void Awake()
	{
		NookManager.instance = this;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00057CAC File Offset: 0x00055EAC
	private void Start()
	{
		LibraryManager libraryManager = LibraryManager.instance;
		libraryManager.OnLibraryEntered = (Action)Delegate.Combine(libraryManager.OnLibraryEntered, new Action(this.OnLibraryEntered));
		NetworkManager.LeftRoom = (Action)Delegate.Combine(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x00057CFF File Offset: 0x00055EFF
	private void OnLibraryEntered()
	{
		base.StartCoroutine("ReuqestNookDelayed");
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00057D0D File Offset: 0x00055F0D
	private IEnumerator ReuqestNookDelayed()
	{
		yield return new WaitForSeconds(0.25f);
		MapManager.instance.RequestNook();
		yield break;
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x00057D18 File Offset: 0x00055F18
	public int GetAvailableNookID()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.Nooks.Count; i++)
		{
			if (this.Nooks[i].Owner == null)
			{
				list.Add(i);
			}
		}
		if (list.Count <= 0)
		{
			UnityEngine.Debug.LogError("No Available Nooks to assign");
			return -1;
		}
		list.Shuffle(null);
		return list[0];
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00057D84 File Offset: 0x00055F84
	public void AssignNook(PlayerControl player, int nookID)
	{
		this.Nooks[nookID].SetOwner(player);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00057D98 File Offset: 0x00055F98
	public void SendNookLayout(PlayerNook nook, string layout)
	{
		if (nook != PlayerNook.MyNook)
		{
			return;
		}
		int nookID = this.Nooks.IndexOf(nook);
		MapManager.instance.SendNookLayout(nookID, layout);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00057DCC File Offset: 0x00055FCC
	public static void PlayerJoinedRoom(Player plr)
	{
		if (PlayerNook.MyNook != null && NookManager.instance != null)
		{
			MapManager.instance.SendNookToPlayer(plr, PlayerControl.myInstance.view.OwnerActorNr, NookManager.instance.Nooks.IndexOf(PlayerNook.MyNook), PlayerNook.MyNook.GetLayoutJSON());
		}
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00057E2C File Offset: 0x0005602C
	public static void PlayerLeftRoom(Player plr)
	{
		if (PlayerNook.MyNook != null && NookManager.instance != null)
		{
			foreach (PlayerNook playerNook in NookManager.instance.Nooks)
			{
				if (playerNook.Owner != null && (playerNook.Owner == null || playerNook.Owner.view.Owner == plr))
				{
					playerNook.SetOwner(null);
				}
			}
		}
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00057ECC File Offset: 0x000560CC
	public void LoadNookLayout(int nookID, string layout)
	{
		this.Nooks[nookID].LoadLayout(layout);
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00057EE0 File Offset: 0x000560E0
	private void OnLeftRoom()
	{
		base.StopAllCoroutines();
		foreach (PlayerNook playerNook in this.Nooks)
		{
			playerNook.SetOwner(null);
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00057F38 File Offset: 0x00056138
	private void OnDestroy()
	{
		NetworkManager.LeftRoom = (Action)Delegate.Remove(NetworkManager.LeftRoom, new Action(this.OnLeftRoom));
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00057F5A File Offset: 0x0005615A
	public NookManager()
	{
	}

	// Token: 0x04000B4A RID: 2890
	public static NookManager instance;

	// Token: 0x04000B4B RID: 2891
	public List<PlayerNook> Nooks;

	// Token: 0x02000531 RID: 1329
	[CompilerGenerated]
	private sealed class <ReuqestNookDelayed>d__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002403 RID: 9219 RVA: 0x000CCA9B File Offset: 0x000CAC9B
		[DebuggerHidden]
		public <ReuqestNookDelayed>d__5(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000CCAAA File Offset: 0x000CACAA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000CCAAC File Offset: 0x000CACAC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			MapManager.instance.RequestNook();
			return false;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x000CCAFB File Offset: 0x000CACFB
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000CCB03 File Offset: 0x000CAD03
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000CCB0A File Offset: 0x000CAD0A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002641 RID: 9793
		private int <>1__state;

		// Token: 0x04002642 RID: 9794
		private object <>2__current;
	}
}
