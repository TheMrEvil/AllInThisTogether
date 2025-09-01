using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class AchievementToast : MonoBehaviour
{
	// Token: 0x06000F09 RID: 3849 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
	private void Awake()
	{
		AchievementToast.instance = this;
		this.Fader.HideImmediate();
		AchievementManager.AchievementUnlocked = (Action<string>)Delegate.Combine(AchievementManager.AchievementUnlocked, new Action<string>(this.OnAchievement));
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0005F8E7 File Offset: 0x0005DAE7
	public void Test()
	{
		if (this.TestGraph == null)
		{
			return;
		}
		this.OnAchievement(this.TestGraph.ID);
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x0005F90C File Offset: 0x0005DB0C
	private void OnAchievement(string achievementID)
	{
		AchievementRootNode achievement = AchievementManager.GetAchievement(achievementID);
		if (achievement == null || !achievement.ShowToast)
		{
			return;
		}
		this.Show(achievement);
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0005F93C File Offset: 0x0005DB3C
	public void Show(AchievementRootNode achievement)
	{
		if (achievement == null)
		{
			return;
		}
		this.Title.text = achievement.Name;
		this.Description.text = TextParser.AugmentDetail(achievement.Detail, null, null, false);
		List<Unlockable> achivementRewards = UnlockDB.GetAchivementRewards(achievement.ID);
		Unlockable unlockable = (achivementRewards.Count > 0) ? achivementRewards[0] : null;
		this.RewardDisplay.gameObject.SetActive(unlockable != null);
		if (unlockable != null)
		{
			this.RewardDisplay.Setup(0, unlockable);
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.DisplayRoutine());
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0005F9D4 File Offset: 0x0005DBD4
	public void Show(MetaDB.DailyQuest Quest)
	{
		if (Quest == null)
		{
			return;
		}
		AugmentTree graph = Quest.Graph;
		if (graph == null)
		{
			return;
		}
		this.Title.text = "Quest Complete!";
		this.Description.text = TextParser.AugmentDetail(graph.Root.Detail, null, null, false);
		this.RewardDisplay.gameObject.SetActive(false);
		base.StopAllCoroutines();
		base.StartCoroutine(this.DisplayRoutine());
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0005FA48 File Offset: 0x0005DC48
	private IEnumerator DisplayRoutine()
	{
		this.Fader.HideImmediate();
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 0.5f;
			this.Fader.UpdateOpacity(true, 1.5f, true);
			yield return true;
		}
		yield return new WaitForSeconds(2.25f);
		while (t > 0f)
		{
			t -= Time.deltaTime * 1f;
			this.Fader.UpdateOpacity(false, 4f, true);
			yield return true;
		}
		yield break;
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0005FA57 File Offset: 0x0005DC57
	public AchievementToast()
	{
	}

	// Token: 0x04000CBF RID: 3263
	public static AchievementToast instance;

	// Token: 0x04000CC0 RID: 3264
	public CanvasGroup Fader;

	// Token: 0x04000CC1 RID: 3265
	public Scriptorium_PrestigeRewardItem RewardDisplay;

	// Token: 0x04000CC2 RID: 3266
	public TextMeshProUGUI Title;

	// Token: 0x04000CC3 RID: 3267
	public TextMeshProUGUI Description;

	// Token: 0x04000CC4 RID: 3268
	public AchievementTree TestGraph;

	// Token: 0x0200054A RID: 1354
	[CompilerGenerated]
	private sealed class <DisplayRoutine>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600244B RID: 9291 RVA: 0x000CDAD7 File Offset: 0x000CBCD7
		[DebuggerHidden]
		public <DisplayRoutine>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000CDAE6 File Offset: 0x000CBCE6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000CDAE8 File Offset: 0x000CBCE8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AchievementToast achievementToast = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				achievementToast.Fader.HideImmediate();
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_100;
			case 3:
				this.<>1__state = -1;
				goto IL_100;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(2.25f);
				this.<>1__state = 2;
				return true;
			}
			t += Time.deltaTime * 0.5f;
			achievementToast.Fader.UpdateOpacity(true, 1.5f, true);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_100:
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime * 1f;
			achievementToast.Fader.UpdateOpacity(false, 4f, true);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000CDC03 File Offset: 0x000CBE03
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000CDC0B File Offset: 0x000CBE0B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000CDC12 File Offset: 0x000CBE12
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400269C RID: 9884
		private int <>1__state;

		// Token: 0x0400269D RID: 9885
		private object <>2__current;

		// Token: 0x0400269E RID: 9886
		public AchievementToast <>4__this;

		// Token: 0x0400269F RID: 9887
		private float <t>5__2;
	}
}
