using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class Questboard : MonoBehaviour
{
	// Token: 0x06000DF5 RID: 3573 RVA: 0x0005964D File Offset: 0x0005784D
	private void Start()
	{
		LibraryManager instance = LibraryManager.instance;
		instance.OnLibraryEntered = (Action)Delegate.Combine(instance.OnLibraryEntered, new Action(this.OnEnteredLibrary));
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00059678 File Offset: 0x00057878
	private void OnEnteredLibrary()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		DailyQuestPanel.instance.RefreshQuests(false);
		bool flag = QuestboardPanel.HasUnclaimedRewards();
		this.UncollectedDisplay.SetActive(flag);
		if (!flag && this.NewChallengesDisplay != null)
		{
			this.NewChallengesDisplay.SetActive(!BookClubPanel.HasSeenCurrentChallenge() && QuestboardPanel.IsBookClubUnlocked);
		}
		if (QuestboardPanel.AreIncentivesUnlocked)
		{
			base.StartCoroutine("SetupIncentiveDelayed");
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x000596EE File Offset: 0x000578EE
	private IEnumerator SetupIncentiveDelayed()
	{
		yield return true;
		yield return true;
		GoalManager.SetupNewIncentives();
		yield break;
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000596F6 File Offset: 0x000578F6
	public Questboard()
	{
	}

	// Token: 0x04000B6D RID: 2925
	public GameObject UncollectedDisplay;

	// Token: 0x04000B6E RID: 2926
	public GameObject NewChallengesDisplay;

	// Token: 0x02000535 RID: 1333
	[CompilerGenerated]
	private sealed class <SetupIncentiveDelayed>d__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002412 RID: 9234 RVA: 0x000CCEFF File Offset: 0x000CB0FF
		[DebuggerHidden]
		public <SetupIncentiveDelayed>d__4(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000CCF0E File Offset: 0x000CB10E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000CCF10 File Offset: 0x000CB110
		bool IEnumerator.MoveNext()
		{
			switch (this.<>1__state)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				GoalManager.SetupNewIncentives();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x000CCF7D File Offset: 0x000CB17D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000CCF85 File Offset: 0x000CB185
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000CCF8C File Offset: 0x000CB18C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400264B RID: 9803
		private int <>1__state;

		// Token: 0x0400264C RID: 9804
		private object <>2__current;
	}
}
