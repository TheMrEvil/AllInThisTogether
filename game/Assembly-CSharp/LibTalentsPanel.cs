using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E4 RID: 484
public class LibTalentsPanel : MonoBehaviour
{
	// Token: 0x0600144D RID: 5197 RVA: 0x0007EC13 File Offset: 0x0007CE13
	private void Awake()
	{
		LibTalentsPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredUI));
		this.TalentRowRef.SetActive(false);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0007EC4E File Offset: 0x0007CE4E
	private void OnEnteredUI()
	{
		this.SetupTalentDisplays();
		this.UpdateCurrencyInfo();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x0007EC5C File Offset: 0x0007CE5C
	public void SetupTalentDisplays()
	{
		this.ClearRows();
		bool flag = true;
		this.UnavailableDisplay.SetActive(!flag);
		if (flag)
		{
			List<PlayerDB.LibraryTalent> libraryTalents = PlayerDB.LibraryTalents;
			for (int i = 0; i < libraryTalents.Count; i++)
			{
				this.CreateTalentRow(libraryTalents[i], i);
			}
		}
		this.SetupVerticalNav();
		if (this.rowItems.Count > 0 && this.rowItems[0].items.Count > 0)
		{
			UISelector.SelectSelectable(this.rowItems[0].items[0].GetComponent<Button>());
		}
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0007ECF8 File Offset: 0x0007CEF8
	private void ClearRows()
	{
		foreach (LibTalentRow libTalentRow in this.rowItems)
		{
			UnityEngine.Object.Destroy(libTalentRow.gameObject);
		}
		this.rowItems.Clear();
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x0007ED58 File Offset: 0x0007CF58
	private void CreateTalentRow(PlayerDB.LibraryTalent row, int rowID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TalentRowRef, this.TalentRowRef.transform.parent);
		gameObject.SetActive(true);
		LibTalentRow component = gameObject.GetComponent<LibTalentRow>();
		component.Setup(row, rowID);
		this.rowItems.Add(component);
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0007EDA4 File Offset: 0x0007CFA4
	private void SetupVerticalNav()
	{
		for (int i = 0; i < this.rowItems.Count; i++)
		{
			LibTalentRow libTalentRow = this.rowItems[i];
			if (i < this.rowItems.Count - 1)
			{
				LibTalentRow libTalentRow2 = this.rowItems[i + 1];
				for (int j = 0; j < libTalentRow.items.Count; j++)
				{
					Button component = libTalentRow.items[j].GetComponent<Button>();
					Navigation navigation = component.navigation;
					Button component2 = libTalentRow2.items[Mathf.Clamp(j, 0, libTalentRow2.items.Count - 1)].GetComponent<Button>();
					navigation.selectOnDown = component2;
					component.navigation = navigation;
				}
			}
			if (i > 0)
			{
				LibTalentRow libTalentRow3 = this.rowItems[i - 1];
				for (int k = 0; k < libTalentRow.items.Count; k++)
				{
					Button component3 = libTalentRow.items[k].GetComponent<Button>();
					Navigation navigation2 = component3.navigation;
					Button component4 = libTalentRow3.items[Mathf.Clamp(k, 0, libTalentRow3.items.Count - 1)].GetComponent<Button>();
					navigation2.selectOnUp = component4;
					component3.navigation = navigation2;
				}
			}
		}
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0007EEDF File Offset: 0x0007D0DF
	public void UpdateEquippedTalent(int row, int index)
	{
		Progression.LibraryBuild.ChangeSavedTalent(row, index);
		Settings.SaveLibraryBuild();
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null)
		{
			return;
		}
		myInstance.UpdateTalents();
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x0007EF04 File Offset: 0x0007D104
	public void RowUnlocked()
	{
		this.UpdateCurrencyInfo();
		foreach (LibTalentRow libTalentRow in this.rowItems)
		{
			libTalentRow.UpdateSelected();
		}
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x0007EF5C File Offset: 0x0007D15C
	public static bool IsTalentEquipped(int row, int index)
	{
		return Progression.LibraryBuild != null && Progression.LibTalentsUnlocked > row && Progression.LibraryBuild.TalentSelections.Count > row && Progression.LibraryBuild.TalentSelections[row] == index;
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x0007EF98 File Offset: 0x0007D198
	private void UpdateCurrencyInfo()
	{
		this.CurrentGildings.text = Currency.Gildings.ToString();
		base.StartCoroutine("UpdateLayoutDelayed");
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x0007EFC9 File Offset: 0x0007D1C9
	private IEnumerator UpdateLayoutDelayed()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.CurrentGildings.transform.parent.GetComponent<RectTransform>());
		yield break;
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0007EFD8 File Offset: 0x0007D1D8
	public LibTalentsPanel()
	{
	}

	// Token: 0x04001384 RID: 4996
	public static LibTalentsPanel instance;

	// Token: 0x04001385 RID: 4997
	public TextMeshProUGUI CurrentGildings;

	// Token: 0x04001386 RID: 4998
	public GameObject UnavailableDisplay;

	// Token: 0x04001387 RID: 4999
	public GameObject TalentRowRef;

	// Token: 0x04001388 RID: 5000
	private List<LibTalentRow> rowItems = new List<LibTalentRow>();

	// Token: 0x020005B1 RID: 1457
	[CompilerGenerated]
	private sealed class <UpdateLayoutDelayed>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x000D226C File Offset: 0x000D046C
		[DebuggerHidden]
		public <UpdateLayoutDelayed>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000D227B File Offset: 0x000D047B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000D2280 File Offset: 0x000D0480
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LibTalentsPanel libTalentsPanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(libTalentsPanel.CurrentGildings.transform.parent.GetComponent<RectTransform>());
			return false;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000D22E2 File Offset: 0x000D04E2
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000D22EA File Offset: 0x000D04EA
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000D22F1 File Offset: 0x000D04F1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400283D RID: 10301
		private int <>1__state;

		// Token: 0x0400283E RID: 10302
		private object <>2__current;

		// Token: 0x0400283F RID: 10303
		public LibTalentsPanel <>4__this;
	}
}
