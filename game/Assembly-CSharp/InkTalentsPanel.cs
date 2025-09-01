using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E3 RID: 483
public class InkTalentsPanel : MonoBehaviour
{
	// Token: 0x0600143E RID: 5182 RVA: 0x0007E520 File Offset: 0x0007C720
	private void Awake()
	{
		InkTalentsPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredUI));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.GoToSignatureInk));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.GoToLevels));
		component.OnPageNext = (Action)Delegate.Combine(component.OnPageNext, new Action(this.NextColor));
		component.OnPagePrev = (Action)Delegate.Combine(component.OnPagePrev, new Action(this.PrevColor));
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x0007E5E4 File Offset: 0x0007C7E4
	public void OpenUI(MagicColor color)
	{
		PanelManager.instance.PushOrGoTo(PanelType.InkTalents);
		this.LevelText.text = Progression.InkLevel.ToString();
		this.SetupTalentDisplays(color);
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x0007E61C File Offset: 0x0007C81C
	private void OnEnteredUI()
	{
		UITutorial.TryTutorial(UITutorial.Tutorial.Inscription);
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x0007E628 File Offset: 0x0007C828
	private void NextColor()
	{
		int num = this.GetCurrentColorIndex();
		num++;
		if (num >= this.SelectorButtons.Count)
		{
			num = 0;
		}
		this.SetupTalentDisplays(this.SelectorButtons[num].Color);
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x0007E668 File Offset: 0x0007C868
	private void PrevColor()
	{
		int num = this.GetCurrentColorIndex();
		num--;
		if (num < 0)
		{
			num = this.SelectorButtons.Count - 1;
		}
		this.SetupTalentDisplays(this.SelectorButtons[num].Color);
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x0007E6AC File Offset: 0x0007C8AC
	private int GetCurrentColorIndex()
	{
		for (int i = 0; i < this.SelectorButtons.Count; i++)
		{
			if (this.SelectorButtons[i].Color == this.selectedColor)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x0007E6EC File Offset: 0x0007C8EC
	public void SetupTalentDisplays(MagicColor color)
	{
		this.selectedColor = color;
		foreach (InkTalentColorSelector inkTalentColorSelector in this.SelectorButtons)
		{
			inkTalentColorSelector.SetSelected(inkTalentColorSelector.Color == this.selectedColor, inkTalentColorSelector.Color == PlayerControl.myInstance.actions.core.Root.magicColor);
		}
		foreach (InkTalentsPanel.TextValues textValues in this.TextVals)
		{
			if (textValues.InkColor == color)
			{
				this.TypeText.text = textValues.Text;
				VertexGradient colorGradient = this.TypeText.colorGradient;
				colorGradient.bottomLeft = textValues.GradientBottom;
				colorGradient.bottomRight = textValues.GradientBottom;
				this.TypeText.colorGradient = colorGradient;
				break;
			}
		}
		this.ClearRows();
		PlayerDB.TalentTree talentTree = PlayerDB.GetTalentTree(color);
		if (talentTree == null)
		{
			return;
		}
		bool flag = true;
		if (color != MagicColor.Neutral)
		{
			PlayerDB.CoreDisplay core = PlayerDB.GetCore(color);
			if (core != null)
			{
				flag = UnlockManager.IsCoreUnlocked(core.core);
			}
		}
		this.UnavailableDisplay.SetActive(!flag);
		if (flag)
		{
			for (int i = 0; i < talentTree.Rows.Count; i++)
			{
				this.CreateTalentRow(talentTree.Rows[i], i);
			}
		}
		this.SetupVerticalNav();
		if (this.rowItems.Count > 0 && this.rowItems[0].items.Count > 0)
		{
			UISelector.SelectSelectable(this.rowItems[0].items[0].GetComponent<Button>());
		}
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x0007E8C4 File Offset: 0x0007CAC4
	private void ClearRows()
	{
		foreach (InkTalentRowUI inkTalentRowUI in this.rowItems)
		{
			UnityEngine.Object.Destroy(inkTalentRowUI.gameObject);
		}
		this.rowItems.Clear();
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x0007E924 File Offset: 0x0007CB24
	private void CreateTalentRow(PlayerDB.TalentRow row, int rowID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TalentRowRef, this.TalentRowRef.transform.parent);
		gameObject.SetActive(true);
		InkTalentRowUI component = gameObject.GetComponent<InkTalentRowUI>();
		component.Setup(row, rowID);
		this.rowItems.Add(component);
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0007E970 File Offset: 0x0007CB70
	private void SetupVerticalNav()
	{
		for (int i = 0; i < this.rowItems.Count; i++)
		{
			InkTalentRowUI inkTalentRowUI = this.rowItems[i];
			if (i < this.rowItems.Count - 1)
			{
				InkTalentRowUI inkTalentRowUI2 = this.rowItems[i + 1];
				for (int j = 0; j < inkTalentRowUI.items.Count; j++)
				{
					Button component = inkTalentRowUI.items[j].GetComponent<Button>();
					Navigation navigation = component.navigation;
					Button component2 = inkTalentRowUI2.items[Mathf.Clamp(j, 0, inkTalentRowUI2.items.Count - 1)].GetComponent<Button>();
					navigation.selectOnDown = component2;
					component.navigation = navigation;
				}
			}
			if (i > 0)
			{
				InkTalentRowUI inkTalentRowUI3 = this.rowItems[i - 1];
				for (int k = 0; k < inkTalentRowUI.items.Count; k++)
				{
					Button component3 = inkTalentRowUI.items[k].GetComponent<Button>();
					Navigation navigation2 = component3.navigation;
					Button component4 = inkTalentRowUI3.items[Mathf.Clamp(k, 0, inkTalentRowUI3.items.Count - 1)].GetComponent<Button>();
					navigation2.selectOnUp = component4;
					component3.navigation = navigation2;
				}
			}
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0007EAAC File Offset: 0x0007CCAC
	public void UpdateEquippedTalent(int row, int index)
	{
		Progression.EquippedTalents talentBuild = Progression.TalentBuild;
		if (!talentBuild.SelectedTalents.ContainsKey(this.selectedColor))
		{
			talentBuild.SelectedTalents.Add(this.selectedColor, new List<int>());
		}
		for (int i = 0; i <= row; i++)
		{
			if (talentBuild.SelectedTalents[this.selectedColor].Count <= i)
			{
				talentBuild.SelectedTalents[this.selectedColor].Add(-1);
			}
			if (i == row)
			{
				talentBuild.SelectedTalents[this.selectedColor][i] = index;
			}
		}
		Settings.SaveTalentBuild();
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance == null)
		{
			return;
		}
		myInstance.UpdateTalents();
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0007EB54 File Offset: 0x0007CD54
	public static bool IsTalentEquipped(int row, int index)
	{
		MagicColor key = InkTalentsPanel.instance.selectedColor;
		return Progression.TalentBuild.SelectedTalents.ContainsKey(key) && Progression.TalentBuild.SelectedTalents[key].Count > row && Progression.TalentBuild.SelectedTalents[key][row] == index;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0007EBB3 File Offset: 0x0007CDB3
	public void GoToSignatureInk()
	{
		if (this.selectedColor == MagicColor.Neutral)
		{
			InkCoresPanel.instance.OpenUI(PlayerControl.myInstance.actions.core.Root.magicColor);
			return;
		}
		InkCoresPanel.instance.OpenUI(this.selectedColor);
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0007EBF2 File Offset: 0x0007CDF2
	public void GoToLevels()
	{
		PanelManager.instance.PushOrGoTo(PanelType.SignatureInk);
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0007EC00 File Offset: 0x0007CE00
	public InkTalentsPanel()
	{
	}

	// Token: 0x0400137B RID: 4987
	public static InkTalentsPanel instance;

	// Token: 0x0400137C RID: 4988
	public TextMeshProUGUI LevelText;

	// Token: 0x0400137D RID: 4989
	public List<InkTalentColorSelector> SelectorButtons;

	// Token: 0x0400137E RID: 4990
	public GameObject UnavailableDisplay;

	// Token: 0x0400137F RID: 4991
	public TextMeshProUGUI TypeText;

	// Token: 0x04001380 RID: 4992
	public GameObject TalentRowRef;

	// Token: 0x04001381 RID: 4993
	private List<InkTalentRowUI> rowItems = new List<InkTalentRowUI>();

	// Token: 0x04001382 RID: 4994
	public List<InkTalentsPanel.TextValues> TextVals;

	// Token: 0x04001383 RID: 4995
	private MagicColor selectedColor;

	// Token: 0x020005B0 RID: 1456
	[Serializable]
	public class TextValues
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x000D2264 File Offset: 0x000D0464
		public TextValues()
		{
		}

		// Token: 0x0400283A RID: 10298
		public MagicColor InkColor;

		// Token: 0x0400283B RID: 10299
		public string Text;

		// Token: 0x0400283C RID: 10300
		public Color GradientBottom;
	}
}
