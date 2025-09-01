using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D7 RID: 471
public class CodexPanel : MonoBehaviour
{
	// Token: 0x0600138C RID: 5004 RVA: 0x000798B0 File Offset: 0x00077AB0
	private void Awake()
	{
		CodexPanel.instance = this;
		this.InCategorySelection = true;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredPanel));
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000798E6 File Offset: 0x00077AE6
	private void OnEnteredPanel()
	{
		this.GoToCategorySelection();
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000798F0 File Offset: 0x00077AF0
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Codex)
		{
			return;
		}
		this.CategoryGroup.UpdateOpacity(this.InCategorySelection, 4f, false);
		this.GridView.TickUpdate();
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			this.GoBack();
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00079940 File Offset: 0x00077B40
	private void GoToCategorySelection()
	{
		this.GridView.StopAllCoroutines();
		foreach (CodexCategorySelector codexCategorySelector in this.Categories)
		{
			codexCategorySelector.UpdateDisplay();
		}
		this.InCategorySelection = true;
		this.CategoryGroup.UpdateOpacity(true, 1f, false);
		this.LifeQuillmarks.text = (Currency.LCoinSpent + Currency.LoadoutCoin).ToString();
		this.UpdateDefaultSelection();
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000799D8 File Offset: 0x00077BD8
	private void GoBack()
	{
		if (PanelManager.CurPanel != PanelType.Codex)
		{
			return;
		}
		if (this.InCategorySelection)
		{
			PanelManager.instance.PopPanel();
			return;
		}
		this.GoToCategorySelection();
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000799FD File Offset: 0x00077BFD
	public void GoToCategory(CodexPanel.CodexCategory category)
	{
		this.InCategorySelection = false;
		this.CurCategory = category;
		if (CodexPanel.WantGridView())
		{
			this.GridView.Setup(category);
			this.GridView.TickUpdate();
		}
		this.UpdateDefaultSelection();
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00079A31 File Offset: 0x00077C31
	public void GoToLorePanel()
	{
		PanelManager.instance.PushPanel(PanelType.Codex_Journal);
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x00079A3F File Offset: 0x00077C3F
	public void GoToEnemiesPanel()
	{
		PanelManager.instance.PushPanel(PanelType.Codex_Enemies);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00079A4D File Offset: 0x00077C4D
	public void GoToStatsPanel()
	{
		PanelManager.instance.PushPanel(PanelType.Codex_Stats);
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x00079A5B File Offset: 0x00077C5B
	private void UpdateDefaultSelection()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.InCategorySelection)
		{
			UISelector.SelectSelectable(this.Categories[0].GetComponent<Selectable>());
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x00079A84 File Offset: 0x00077C84
	public static string GetTitle(CodexPanel.CodexCategory cat)
	{
		string result;
		switch (cat)
		{
		case CodexPanel.CodexCategory.ScribePages:
			result = "Scribe Pages";
			break;
		case CodexPanel.CodexCategory.FontPages:
			result = "Font Pages";
			break;
		case CodexPanel.CodexCategory.TornPages:
			result = "Torn Pages";
			break;
		default:
			result = "UNDEFINED";
			break;
		}
		return result;
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x00079AC4 File Offset: 0x00077CC4
	public static List<AugmentTree> GetAllCodexAugments(ModType modType)
	{
		List<AugmentTree> allAugments = GraphDB.GetAllAugments(modType);
		if (allAugments == null || allAugments.Count == 0)
		{
			return allAugments;
		}
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in allAugments)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit)
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x00079B3C File Offset: 0x00077D3C
	[return: TupleElementNames(new string[]
	{
		"seen",
		"total"
	})]
	public static ValueTuple<int, int> GetCounts(CodexPanel.CodexCategory cat)
	{
		if (cat == CodexPanel.CodexCategory.TornEnemies)
		{
			return new ValueTuple<int, int>(0, 0);
		}
		ModType augmentCategory = CodexPanel.GetAugmentCategory(cat);
		List<AugmentTree> allCodexAugments = CodexPanel.GetAllCodexAugments(augmentCategory);
		if (allCodexAugments.Count == 0)
		{
			return new ValueTuple<int, int>(0, 0);
		}
		return new ValueTuple<int, int>(CodexPanel.RemoveUnseen(allCodexAugments, augmentCategory).Count, allCodexAugments.Count);
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00079B8C File Offset: 0x00077D8C
	public static List<AugmentTree> RemoveUnseen(List<AugmentTree> allAugments, ModType modType)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in allAugments)
		{
			AugmentRootNode root = augmentTree.Root;
			string id = augmentTree.ID;
			if ((modType != ModType.Player || ((!root.StartUnlocked || Progression.FoundAugments.Contains(id)) && (root.StartUnlocked || UnlockManager.IsAugmentUnlocked(augmentTree)))) && (modType != ModType.Fountain || Progression.FoundAugments.Contains(id)) && (modType != ModType.Enemy || Progression.FoundAugments.Contains(id)))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x00079C40 File Offset: 0x00077E40
	public static ModType GetAugmentCategory(CodexPanel.CodexCategory cat)
	{
		ModType result;
		switch (cat)
		{
		case CodexPanel.CodexCategory.ScribePages:
			result = ModType.Player;
			break;
		case CodexPanel.CodexCategory.FontPages:
			result = ModType.Fountain;
			break;
		case CodexPanel.CodexCategory.TornPages:
			result = ModType.Enemy;
			break;
		default:
			result = ModType.Binding;
			break;
		}
		return result;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00079C70 File Offset: 0x00077E70
	public static bool WantGridView()
	{
		return !CodexPanel.instance.InCategorySelection;
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00079C7F File Offset: 0x00077E7F
	public CodexPanel()
	{
	}

	// Token: 0x040012B1 RID: 4785
	public static CodexPanel instance;

	// Token: 0x040012B2 RID: 4786
	public CanvasGroup CategoryGroup;

	// Token: 0x040012B3 RID: 4787
	public List<CodexCategorySelector> Categories;

	// Token: 0x040012B4 RID: 4788
	[NonSerialized]
	public bool InCategorySelection;

	// Token: 0x040012B5 RID: 4789
	private CodexPanel.CodexCategory CurCategory;

	// Token: 0x040012B6 RID: 4790
	public TextMeshProUGUI LifeQuillmarks;

	// Token: 0x040012B7 RID: 4791
	public CodexGridView GridView;

	// Token: 0x020005A1 RID: 1441
	public enum CodexCategory
	{
		// Token: 0x040027FF RID: 10239
		ScribePages,
		// Token: 0x04002800 RID: 10240
		FontPages,
		// Token: 0x04002801 RID: 10241
		TornPages,
		// Token: 0x04002802 RID: 10242
		TornEnemies
	}
}
