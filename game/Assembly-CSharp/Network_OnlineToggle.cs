using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class Network_OnlineToggle : MonoBehaviour
{
	// Token: 0x06000C87 RID: 3207 RVA: 0x00050893 File Offset: 0x0004EA93
	private void Awake()
	{
		Network_OnlineToggle.IsToggling = false;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0005089B File Offset: 0x0004EA9B
	public void GoOnline()
	{
		if (!PhotonNetwork.OfflineMode)
		{
			return;
		}
		Network_OnlineToggle.IsToggling = true;
		NetworkManager.instance.GoOnline();
		base.StartCoroutine("GoOnlineSequence");
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x000508C1 File Offset: 0x0004EAC1
	private IEnumerator GoOnlineSequence()
	{
		float t = 0f;
		while (PhotonNetwork.NetworkClientState != ClientState.JoinedLobby && t < 5f)
		{
			t += Time.deltaTime;
			yield return true;
		}
		if (t >= 5f)
		{
			UnityEngine.Debug.LogError("Couldn't Connect");
		}
		else
		{
			UnityEngine.Debug.Log("Connected to Network");
			MainPanel.instance.CreateGameButton();
		}
		Network_OnlineToggle.IsToggling = false;
		yield break;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000508C9 File Offset: 0x0004EAC9
	public Network_OnlineToggle()
	{
	}

	// Token: 0x04000A04 RID: 2564
	public static bool IsToggling;

	// Token: 0x02000508 RID: 1288
	[CompilerGenerated]
	private sealed class <GoOnlineSequence>d__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600239D RID: 9117 RVA: 0x000CAFF6 File Offset: 0x000C91F6
		[DebuggerHidden]
		public <GoOnlineSequence>d__3(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000CB005 File Offset: 0x000C9205
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000CB008 File Offset: 0x000C9208
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
			}
			if (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby || t >= 5f)
			{
				if (t >= 5f)
				{
					UnityEngine.Debug.LogError("Couldn't Connect");
				}
				else
				{
					UnityEngine.Debug.Log("Connected to Network");
					MainPanel.instance.CreateGameButton();
				}
				Network_OnlineToggle.IsToggling = false;
				return false;
			}
			t += Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000CB0B0 File Offset: 0x000C92B0
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000CB0B8 File Offset: 0x000C92B8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000CB0BF File Offset: 0x000C92BF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002593 RID: 9619
		private int <>1__state;

		// Token: 0x04002594 RID: 9620
		private object <>2__current;

		// Token: 0x04002595 RID: 9621
		private float <t>5__2;
	}
}
