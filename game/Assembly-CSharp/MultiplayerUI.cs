using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BC RID: 444
public class MultiplayerUI : MonoBehaviourPunCallbacks
{
	// Token: 0x0600124B RID: 4683 RVA: 0x000714B7 File Offset: 0x0006F6B7
	private void Awake()
	{
		MultiplayerUI.instance = this;
		this.AFKGroup.HideImmediate();
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x000714CC File Offset: 0x0006F6CC
	private void Update()
	{
		if (!PhotonNetwork.InRoom || PhotonNetwork.OfflineMode)
		{
			this.OfflineUpdate();
			return;
		}
		bool isInAFKDanger = StateManager.IsInAFKDanger;
		this.AFKGroup.UpdateOpacity(isInAFKDanger, 3f, true);
		if (isInAFKDanger)
		{
			this.AFKTimer.fillAmount = StateManager.TimeToAFKKick / 30f;
		}
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x0007151F File Offset: 0x0006F71F
	public void OnPlayerJoined(PlayerControl player)
	{
		if (player.IsMine)
		{
			return;
		}
		this.PlayerActionDisplay(player, "joined the game");
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00071536 File Offset: 0x0006F736
	public void OnPlayerLeft(PlayerControl player)
	{
		if (player.IsMine)
		{
			return;
		}
		this.PlayerActionDisplay(player, "left the game");
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0007154D File Offset: 0x0006F74D
	private void PlayerActionDisplay(PlayerControl plr, string action)
	{
		if (this.ActionRoutine != null)
		{
			base.StopCoroutine(this.ActionRoutine);
		}
		this.ActionRoutine = base.StartCoroutine(this.PlayerActionRoutine(plr, action));
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x00071577 File Offset: 0x0006F777
	private IEnumerator PlayerActionRoutine(PlayerControl player, string action)
	{
		this.UserActionGroup.HideImmediate();
		this.ActionUsername.text = player.GetUsernameText();
		this.ActionText.text = action;
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.ActionNameHolder);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.UserActionGroup.UpdateOpacity(true, 4f, true);
			yield return true;
		}
		yield return new WaitForSeconds(1.5f);
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.UserActionGroup.UpdateOpacity(false, 4f, true);
			yield return true;
		}
		yield break;
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x00071594 File Offset: 0x0006F794
	private void OfflineUpdate()
	{
		if (this.AFKGroup.alpha > 0f)
		{
			this.AFKGroup.alpha = 0f;
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000715B8 File Offset: 0x0006F7B8
	public MultiplayerUI()
	{
	}

	// Token: 0x0400113B RID: 4411
	public static MultiplayerUI instance;

	// Token: 0x0400113C RID: 4412
	public CanvasGroup AFKGroup;

	// Token: 0x0400113D RID: 4413
	public Image AFKTimer;

	// Token: 0x0400113E RID: 4414
	public CanvasGroup UserActionGroup;

	// Token: 0x0400113F RID: 4415
	public TextMeshProUGUI ActionUsername;

	// Token: 0x04001140 RID: 4416
	public RectTransform ActionNameHolder;

	// Token: 0x04001141 RID: 4417
	public TextMeshProUGUI ActionText;

	// Token: 0x04001142 RID: 4418
	private Coroutine ActionRoutine;

	// Token: 0x02000580 RID: 1408
	[CompilerGenerated]
	private sealed class <PlayerActionRoutine>d__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002521 RID: 9505 RVA: 0x000D093C File Offset: 0x000CEB3C
		[DebuggerHidden]
		public <PlayerActionRoutine>d__13(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000D094B File Offset: 0x000CEB4B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000D0950 File Offset: 0x000CEB50
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			MultiplayerUI multiplayerUI = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				multiplayerUI.UserActionGroup.HideImmediate();
				multiplayerUI.ActionUsername.text = player.GetUsernameText();
				multiplayerUI.ActionText.text = action;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(multiplayerUI.ActionNameHolder);
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				t = 0f;
				goto IL_151;
			case 4:
				this.<>1__state = -1;
				goto IL_151;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(1.5f);
				this.<>1__state = 3;
				return true;
			}
			t += Time.deltaTime;
			multiplayerUI.UserActionGroup.UpdateOpacity(true, 4f, true);
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
			IL_151:
			if (t >= 1f)
			{
				return false;
			}
			t += Time.deltaTime;
			multiplayerUI.UserActionGroup.UpdateOpacity(false, 4f, true);
			this.<>2__current = true;
			this.<>1__state = 4;
			return true;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000D0ABC File Offset: 0x000CECBC
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000D0AC4 File Offset: 0x000CECC4
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000D0ACB File Offset: 0x000CECCB
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400276E RID: 10094
		private int <>1__state;

		// Token: 0x0400276F RID: 10095
		private object <>2__current;

		// Token: 0x04002770 RID: 10096
		public MultiplayerUI <>4__this;

		// Token: 0x04002771 RID: 10097
		public PlayerControl player;

		// Token: 0x04002772 RID: 10098
		public string action;

		// Token: 0x04002773 RID: 10099
		private float <t>5__2;
	}
}
