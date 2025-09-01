using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F0 RID: 496
public class SignatureChallengePanel : MonoBehaviour
{
	// Token: 0x0600152A RID: 5418 RVA: 0x00084AB8 File Offset: 0x00082CB8
	private void Awake()
	{
		SignatureChallengePanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEntered));
		this.ChallengeRef.SetActive(false);
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in this.SignatureChallenges)
		{
			MagicColor color = challengeSet.Color;
			challengeSet.Button.onClick.AddListener(delegate()
			{
				this.SelectChallenges(color);
			});
		}
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x00084B74 File Offset: 0x00082D74
	private void OnEntered()
	{
		this.SelectChallenges(PlayerControl.myInstance.actions.core.Root.magicColor);
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in this.SignatureChallenges)
		{
			int num = 0;
			using (List<AchievementTree>.Enumerator enumerator2 = challengeSet.Challenges.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (AchievementManager.IsUnlocked(enumerator2.Current.Root.ID))
					{
						num++;
					}
				}
			}
			challengeSet.ProgressText.text = num.ToString() + " / " + challengeSet.Challenges.Count.ToString();
		}
		this.UpdatedUnclaimed();
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x00084C6C File Offset: 0x00082E6C
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.SignatureChallenges)
		{
			return;
		}
		if (InputManager.IsUsingController)
		{
			this.AutoScroller.TickUpdate();
		}
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x00084C8C File Offset: 0x00082E8C
	public void SelectChallenges(MagicColor color)
	{
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in this.SignatureChallenges)
		{
			challengeSet.CategoryHighlight.SetActive(challengeSet.Color == color);
		}
		this.SelectedTitle.text = color.ToString() + " Signature";
		SignatureChallengePanel.ChallengeSet challengeSet2 = this.GetChallengeSet(color);
		if (challengeSet2 == null)
		{
			return;
		}
		Button button = challengeSet2.Button;
		foreach (ChallengeItemDisplay challengeItemDisplay in this.ItemList)
		{
			UnityEngine.Object.Destroy(challengeItemDisplay.gameObject);
		}
		this.ItemList.Clear();
		foreach (AchievementTree ach in challengeSet2.Challenges)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChallengeRef, this.ChallengeRef.transform.parent);
			gameObject.gameObject.SetActive(true);
			ChallengeItemDisplay component = gameObject.GetComponent<ChallengeItemDisplay>();
			component.SetupDisplay(ach);
			this.ItemList.Add(component);
		}
		UISelector.SetupVerticalListNav<ChallengeItemDisplay>(this.ItemList, null, null, false);
		if (this.ItemList.Count > 0)
		{
			foreach (SignatureChallengePanel.ChallengeSet challengeSet3 in this.SignatureChallenges)
			{
				challengeSet3.Button.SetNavigation(this.ItemList[0].GetComponent<Button>(), UIDirection.Right, false);
			}
		}
		foreach (ChallengeItemDisplay challengeItemDisplay2 in this.ItemList)
		{
			challengeItemDisplay2.GetComponent<Button>().SetNavigation(button, UIDirection.Left, false);
		}
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x00084EB0 File Offset: 0x000830B0
	private SignatureChallengePanel.ChallengeSet GetChallengeSet(MagicColor color)
	{
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in this.SignatureChallenges)
		{
			if (challengeSet.Color == color)
			{
				return challengeSet;
			}
		}
		return null;
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x00084F0C File Offset: 0x0008310C
	public static bool HasUnclaimed()
	{
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in SignatureChallengePanel.instance.SignatureChallenges)
		{
			foreach (AchievementTree achievementTree in challengeSet.Challenges)
			{
				if (AchievementManager.IsUnlocked(achievementTree.Root.ID) && !AchievementManager.IsClaimed(achievementTree.Root.ID))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x00084FC0 File Offset: 0x000831C0
	public void UpdatedUnclaimed()
	{
		foreach (SignatureChallengePanel.ChallengeSet challengeSet in this.SignatureChallenges)
		{
			bool active = false;
			foreach (AchievementTree achievementTree in challengeSet.Challenges)
			{
				bool flag = AchievementManager.IsUnlocked(achievementTree.Root.ID);
				bool flag2 = AchievementManager.IsClaimed(achievementTree.Root.ID);
				if (flag && !flag2)
				{
					active = true;
					break;
				}
			}
			challengeSet.UnclaimedDisplay.SetActive(active);
		}
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x00085084 File Offset: 0x00083284
	public SignatureChallengePanel()
	{
	}

	// Token: 0x0400149A RID: 5274
	public static SignatureChallengePanel instance;

	// Token: 0x0400149B RID: 5275
	public List<SignatureChallengePanel.ChallengeSet> SignatureChallenges = new List<SignatureChallengePanel.ChallengeSet>();

	// Token: 0x0400149C RID: 5276
	public TextMeshProUGUI SelectedTitle;

	// Token: 0x0400149D RID: 5277
	public GameObject ChallengeRef;

	// Token: 0x0400149E RID: 5278
	private List<ChallengeItemDisplay> ItemList = new List<ChallengeItemDisplay>();

	// Token: 0x0400149F RID: 5279
	public AutoScrollRect AutoScroller;

	// Token: 0x020005C1 RID: 1473
	[Serializable]
	public class ChallengeSet
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000D2C67 File Offset: 0x000D0E67
		public ChallengeSet()
		{
		}

		// Token: 0x0400288D RID: 10381
		public MagicColor Color;

		// Token: 0x0400288E RID: 10382
		public TextMeshProUGUI ProgressText;

		// Token: 0x0400288F RID: 10383
		public Button Button;

		// Token: 0x04002890 RID: 10384
		public GameObject CategoryHighlight;

		// Token: 0x04002891 RID: 10385
		public GameObject UnclaimedDisplay;

		// Token: 0x04002892 RID: 10386
		public List<AchievementTree> Challenges = new List<AchievementTree>();
	}

	// Token: 0x020005C2 RID: 1474
	[CompilerGenerated]
	private sealed class <>c__DisplayClass6_0
	{
		// Token: 0x06002604 RID: 9732 RVA: 0x000D2C7A File Offset: 0x000D0E7A
		public <>c__DisplayClass6_0()
		{
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000D2C82 File Offset: 0x000D0E82
		internal void <Awake>b__0()
		{
			this.<>4__this.SelectChallenges(this.color);
		}

		// Token: 0x04002893 RID: 10387
		public MagicColor color;

		// Token: 0x04002894 RID: 10388
		public SignatureChallengePanel <>4__this;
	}
}
