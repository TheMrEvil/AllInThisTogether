using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DB RID: 475
public class DailyQuestPanel : MonoBehaviour
{
	// Token: 0x060013CC RID: 5068 RVA: 0x0007B128 File Offset: 0x00079328
	private void Awake()
	{
		DailyQuestPanel.instance = this;
		this.QuestObjRef.SetActive(false);
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnterPanel));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnLeftPanel));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.OnSecondary));
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x0007B1B2 File Offset: 0x000793B2
	private void OnEnterPanel()
	{
		this.RefreshQuests(true);
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x0007B1BC File Offset: 0x000793BC
	private void OnLeftPanel()
	{
		foreach (DailyQuestUIBox dailyQuestUIBox in this.QuestItems)
		{
			UnityEngine.Object.Destroy(dailyQuestUIBox.gameObject);
		}
		this.QuestItems.Clear();
		foreach (DailyQuestPanel.QuestSlot questSlot in this.DailySlots)
		{
			questSlot.CurSel = null;
		}
		this.WeeklySlot.CurSel = null;
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x0007B268 File Offset: 0x00079468
	private void OnSecondary()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		Selectable currentSelection = UISelector.instance.CurrentSelection;
		if (currentSelection == null)
		{
			return;
		}
		DailyQuestUIBox component = currentSelection.GetComponent<DailyQuestUIBox>();
		if (component == null)
		{
			return;
		}
		component.TryReroll();
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x0007B2A9 File Offset: 0x000794A9
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.DailyQuests)
		{
			return;
		}
		this.SecondUpdate();
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x0007B2BB File Offset: 0x000794BB
	private void SecondUpdate()
	{
		this.t -= Time.deltaTime;
		if (this.t > 0f)
		{
			return;
		}
		this.t = 1f;
		this.UpdateTimers();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x0007B2F0 File Offset: 0x000794F0
	private void UpdateTimers()
	{
		TimeSpan timeSpan = MetaDB.QuestProgress.NextDailyQuestTime() - DateTime.Now;
		if (timeSpan.TotalSeconds <= 1.0)
		{
			base.Invoke("RefreshDelayed", 1f);
		}
		string text = string.Concat(new string[]
		{
			timeSpan.Hours.ToString("D2"),
			":",
			timeSpan.Minutes.ToString("D2"),
			":",
			timeSpan.Seconds.ToString("D2")
		});
		foreach (DailyQuestPanel.QuestSlot questSlot in this.DailySlots)
		{
			questSlot.TimerText.text = text;
		}
		TimeSpan timeSpan2 = MetaDB.QuestProgress.NextWeekyQuestTime() - DateTime.Now;
		string text2 = string.Concat(new string[]
		{
			timeSpan2.Days.ToString("D2"),
			":",
			timeSpan2.Hours.ToString("D2"),
			":",
			timeSpan2.Minutes.ToString("D2"),
			":",
			timeSpan2.Seconds.ToString("D2")
		});
		this.WeeklySlot.TimerText.text = text2;
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x0007B484 File Offset: 0x00079684
	public void RefreshDelayed()
	{
		base.Invoke("RefreshQ", 0.5f);
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x0007B496 File Offset: 0x00079696
	private void RefreshQ()
	{
		this.RefreshQuests(true);
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x0007B4A0 File Offset: 0x000796A0
	public void RefreshQuests(bool createBoxes)
	{
		List<string> list = new List<string>();
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (MetaDB.GetQuest(questProgress.ID) == null)
			{
				Debug.Log("Expiring " + questProgress.ID + " - Invalid Quest");
				list.Add(questProgress.ID);
			}
			else if (questProgress.IsExpired && questProgress.IsCollected)
			{
				Debug.Log("Expiring " + questProgress.ID + " - Collected");
				list.Add(questProgress.ID);
			}
		}
		foreach (string id in list)
		{
			Progression.RemoveQuest(id);
		}
		this.SetupWeeklyQuest(createBoxes);
		this.SetupDailyQuests(createBoxes);
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x0007B5A4 File Offset: 0x000797A4
	private void SetupWeeklyQuest(bool createBoxes)
	{
		int num = 0;
		using (List<MetaDB.QuestProgress>.Enumerator enumerator = Progression.Quests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Timescale == MetaDB.DailyQuest.Timescale.Weekly)
				{
					num++;
				}
			}
		}
		if (num == 0)
		{
			MetaDB.DailyQuest newQuest = MetaDB.GetNewQuest(MetaDB.DailyQuest.Timescale.Weekly);
			if (newQuest == null)
			{
				Debug.LogError("Couldn't find a new Weekly quest");
				return;
			}
			Progression.AddQuest(newQuest);
		}
		if (!createBoxes)
		{
			return;
		}
		string text = null;
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.Timescale == MetaDB.DailyQuest.Timescale.Weekly && !questProgress.IsCollected)
			{
				text = questProgress.ID;
			}
		}
		if (this.WeeklySlot.CurSel == null || this.WeeklySlot.CurSel.QuestID != text)
		{
			if (this.WeeklySlot.CurSel != null)
			{
				this.WeeklySlot.CurSel.DestroySequence();
			}
			if (text != null)
			{
				MetaDB.DailyQuest quest = MetaDB.GetQuest(text);
				this.AddQuestDisplay(quest, this.WeeklySlot);
			}
		}
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x0007B6E0 File Offset: 0x000798E0
	private void SetupDailyQuests(bool createBoxes)
	{
		int num = 0;
		using (List<MetaDB.QuestProgress>.Enumerator enumerator = Progression.Quests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Timescale == MetaDB.DailyQuest.Timescale.Daily)
				{
					num++;
				}
			}
		}
		for (int i = num; i < 3; i++)
		{
			MetaDB.DailyQuest newQuest = MetaDB.GetNewQuest(MetaDB.DailyQuest.Timescale.Daily);
			if (newQuest == null)
			{
				Debug.LogError("Couldn't find a new Daily quest");
				break;
			}
			Progression.AddQuest(newQuest);
		}
		if (!createBoxes)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.Timescale == MetaDB.DailyQuest.Timescale.Daily && !questProgress.IsCollected)
			{
				list.Add(questProgress.ID);
			}
		}
		foreach (DailyQuestPanel.QuestSlot questSlot in this.DailySlots)
		{
			if (questSlot.CurSel != null && !list.Contains(questSlot.CurSel.QuestID))
			{
				MetaDB.DailyQuest quest = MetaDB.GetQuest(questSlot.CurSel.QuestID);
				if (quest != null)
				{
					Debug.Log("Removing old quest " + quest.Graph.Root.Name);
				}
				questSlot.CurSel.DestroySequence();
			}
		}
		foreach (DailyQuestPanel.QuestSlot questSlot2 in this.DailySlots)
		{
			if (questSlot2.CurSel != null && list.Contains(questSlot2.CurSel.QuestID))
			{
				list.Remove(questSlot2.CurSel.QuestID);
			}
		}
		foreach (DailyQuestPanel.QuestSlot questSlot3 in this.DailySlots)
		{
			if (!(questSlot3.CurSel != null) && list.Count != 0)
			{
				string id = list[0];
				list.RemoveAt(0);
				MetaDB.DailyQuest quest2 = MetaDB.GetQuest(id);
				this.AddQuestDisplay(quest2, questSlot3);
			}
		}
		this.UpdateNav();
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x0007B95C File Offset: 0x00079B5C
	private void AddQuestDisplay(MetaDB.DailyQuest quest, DailyQuestPanel.QuestSlot slot)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestObjRef, slot.Holder);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.SetActive(true);
		DailyQuestUIBox component = gameObject.GetComponent<DailyQuestUIBox>();
		component.Setup(quest);
		slot.CurSel = component;
		this.QuestItems.Add(component);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x0007B9C4 File Offset: 0x00079BC4
	private void UpdateNav()
	{
		DailyQuestUIBox curSel = this.DailySlots[0].CurSel;
		Button button = (curSel != null) ? curSel.Button : null;
		bool flag = button != null;
		DailyQuestUIBox curSel2 = this.DailySlots[2].CurSel;
		Button button2 = (curSel2 != null) ? curSel2.Button : null;
		bool flag2 = button2 != null;
		DailyQuestUIBox curSel3 = this.DailySlots[1].CurSel;
		Button button3 = (curSel3 != null) ? curSel3.Button : null;
		bool flag3 = button3 != null;
		DailyQuestUIBox curSel4 = this.WeeklySlot.CurSel;
		Button button4 = (curSel4 != null) ? curSel4.Button : null;
		bool flag4 = button4 != null;
		if (flag)
		{
			if (flag2)
			{
				button.SetNavigation(button2, UIDirection.Right, false);
				button2.SetNavigation(button, UIDirection.Left, false);
			}
			if (flag3)
			{
				button.SetNavigation(button3, UIDirection.Down, false);
				button3.SetNavigation(button, UIDirection.Up, false);
			}
			else if (flag4)
			{
				button.SetNavigation(button4, UIDirection.Down, false);
				button4.SetNavigation(button, UIDirection.Up, false);
			}
		}
		if (flag2)
		{
			if (flag4)
			{
				button2.SetNavigation(button4, UIDirection.Down, false);
				button4.SetNavigation(button2, UIDirection.Up, false);
			}
			else if (flag3)
			{
				button2.SetNavigation(button3, UIDirection.Down, false);
				if (!flag)
				{
					button3.SetNavigation(button2, UIDirection.Up, false);
				}
			}
		}
		if (flag4 && flag3)
		{
			button3.SetNavigation(button4, UIDirection.Right, false);
			button4.SetNavigation(button3, UIDirection.Left, false);
		}
		if (flag)
		{
			UISelector.SelectSelectable(button);
			return;
		}
		if (flag2)
		{
			UISelector.SelectSelectable(button2);
			return;
		}
		if (flag3)
		{
			UISelector.SelectSelectable(button3);
			return;
		}
		if (flag4)
		{
			UISelector.SelectSelectable(button4);
		}
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x0007BB34 File Offset: 0x00079D34
	public void QuestCompleted(MetaDB.DailyQuest quest)
	{
		int rewardValue = MetaDB.GetRewardValue(quest.RewardValue);
		if (quest.Reward == MetaDB.DailyQuest.RewardType.Quillmarks)
		{
			Currency.AddLoadoutCoin(rewardValue, true);
			LibraryInfoWidget.QuillmarksGained(rewardValue);
		}
		else if (quest.Reward == MetaDB.DailyQuest.RewardType.Gildings)
		{
			Currency.Add(rewardValue, true);
			LibraryInfoWidget.GildingsGained(rewardValue);
		}
		AudioManager.PlayInterfaceSFX(this.QuestCompleteSFX, 1f, 0f);
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x0007BB90 File Offset: 0x00079D90
	public void RemoveBox(DailyQuestUIBox box)
	{
		this.t = 5f;
		this.QuestItems.Remove(box);
		foreach (DailyQuestPanel.QuestSlot questSlot in this.DailySlots)
		{
			if (questSlot.CurSel == box)
			{
				questSlot.CurSel = null;
			}
		}
		if (this.WeeklySlot.CurSel == box)
		{
			this.WeeklySlot.CurSel = null;
		}
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x0007BC28 File Offset: 0x00079E28
	public static bool HasUnclaimed()
	{
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.IsComplete && !questProgress.IsCollected)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x0007BC8C File Offset: 0x00079E8C
	public DailyQuestPanel()
	{
	}

	// Token: 0x040012F3 RID: 4851
	public static DailyQuestPanel instance;

	// Token: 0x040012F4 RID: 4852
	public List<DailyQuestPanel.QuestSlot> DailySlots;

	// Token: 0x040012F5 RID: 4853
	public DailyQuestPanel.QuestSlot WeeklySlot;

	// Token: 0x040012F6 RID: 4854
	public GameObject QuestObjRef;

	// Token: 0x040012F7 RID: 4855
	private List<DailyQuestUIBox> QuestItems = new List<DailyQuestUIBox>();

	// Token: 0x040012F8 RID: 4856
	public AudioClip QuestCompleteSFX;

	// Token: 0x040012F9 RID: 4857
	private float t;

	// Token: 0x020005A6 RID: 1446
	[Serializable]
	public class QuestSlot
	{
		// Token: 0x060025A3 RID: 9635 RVA: 0x000D1EAF File Offset: 0x000D00AF
		public QuestSlot()
		{
		}

		// Token: 0x0400280D RID: 10253
		public Transform Holder;

		// Token: 0x0400280E RID: 10254
		public TextMeshProUGUI TimerText;

		// Token: 0x0400280F RID: 10255
		[NonSerialized]
		public DailyQuestUIBox CurSel;
	}
}
