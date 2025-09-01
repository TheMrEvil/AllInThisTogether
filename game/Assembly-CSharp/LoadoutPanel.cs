using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E5 RID: 485
public class LoadoutPanel : MonoBehaviour
{
	// Token: 0x06001459 RID: 5209 RVA: 0x0007EFEC File Offset: 0x0007D1EC
	private void Awake()
	{
		LoadoutPanel.instance = this;
		this.Talent_Inactive.SetActive(false);
		this.Talent_Active.SetActive(false);
		this.Talent_Locked.SetActive(false);
		this.LoadoutItemRef.SetActive(false);
		this.EmptyItemRef.SetActive(false);
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnSelected));
		component.OnTertiaryAction = (Action)Delegate.Combine(component.OnTertiaryAction, new Action(this.RequestDeleteLoadout));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.SecondaryAction));
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x0007F0A6 File Offset: 0x0007D2A6
	private void OnSelected()
	{
		this.SetupLoadouts();
		this.SelectLoadout(Settings.CurFullLoadout);
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x0007F0B9 File Offset: 0x0007D2B9
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Loadouts)
		{
			return;
		}
		if (InputManager.IsUsingController)
		{
			this.autoScroller.TickUpdate();
		}
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x0007F0D8 File Offset: 0x0007D2D8
	private void SetupLoadouts()
	{
		foreach (GameObject gameObject in this.gridObjects)
		{
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
		this.gridObjects.Clear();
		this.selectables.Clear();
		this.loadoutEntries.Clear();
		this.selectables.Add(this.NewLoadoutButton);
		List<Progression.FullLoadout> fullLoadouts = Settings.GetFullLoadouts();
		for (int i = 0; i < fullLoadouts.Count; i++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.LoadoutItemRef, this.LoadoutGrid);
			gameObject2.gameObject.SetActive(true);
			LoadoutEntry component = gameObject2.GetComponent<LoadoutEntry>();
			component.Setup(fullLoadouts[i], i);
			this.loadoutEntries.Add(component);
			this.selectables.Add(gameObject2);
			this.gridObjects.Add(gameObject2);
		}
		UISelector.SetupGridListNav(this.selectables, 3, null, null, false);
		for (int j = this.gridObjects.Count; j < 11; j++)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.EmptyItemRef, this.LoadoutGrid);
			gameObject3.gameObject.SetActive(true);
			this.gridObjects.Add(gameObject3);
		}
		int num = (this.gridObjects.Count + 1) % 3;
		for (int k = 0; k < 3 - num; k++)
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.EmptyItemRef, this.LoadoutGrid);
			gameObject4.gameObject.SetActive(true);
			this.gridObjects.Add(gameObject4);
		}
		foreach (LoadoutEntry loadoutEntry in this.loadoutEntries)
		{
			loadoutEntry.UpdateIsEquipped(Settings.CurFullLoadout);
		}
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x0007F2C0 File Offset: 0x0007D4C0
	public void TryCreateNewLoadout()
	{
		Progression.FullLoadout fullLoadout = new Progression.FullLoadout(PlayerControl.myInstance);
		List<Progression.FullLoadout> fullLoadouts = Settings.GetFullLoadouts();
		for (int i = 0; i < fullLoadouts.Count; i++)
		{
			if (fullLoadouts[i].Matches(fullLoadout))
			{
				this.SelectLoadout(i);
				this.EquipSelectedLoadout();
				return;
			}
		}
		Settings.SaveNewFullLoadout(fullLoadout);
		this.SetupLoadouts();
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x0007F318 File Offset: 0x0007D518
	public void SelectLoadout(int index)
	{
		if (index >= this.loadoutEntries.Count)
		{
			index = -1;
		}
		this.curSelectedIndex = index;
		if (index < 0)
		{
			Selectable component = this.NewLoadoutButton.GetComponent<Selectable>();
			if (UISelector.instance.CurrentSelection != component)
			{
				UISelector.SelectSelectable(component);
			}
			Progression.FullLoadout loadout = new Progression.FullLoadout(PlayerControl.myInstance);
			this.UpdateSideInfo(loadout, index);
			return;
		}
		LoadoutEntry loadoutEntry = this.loadoutEntries[index];
		Selectable component2 = loadoutEntry.GetComponent<Selectable>();
		if (UISelector.instance.CurrentSelection != component2)
		{
			UISelector.SelectSelectable(component2);
		}
		this.UpdateSideInfo(loadoutEntry.Loadout, index);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0007F3B2 File Offset: 0x0007D5B2
	private void SecondaryAction()
	{
		if (this.curSelectedIndex < 0 || Settings.FullLoadouts.Count <= this.curSelectedIndex)
		{
			return;
		}
		if (this.UpdatePrompt.activeSelf)
		{
			this.UpdateCurrentLoadout();
			return;
		}
		this.RenameSelectedLoadout();
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0007F3EC File Offset: 0x0007D5EC
	private void RenameSelectedLoadout()
	{
		Progression.FullLoadout fullLoadout = new Progression.FullLoadout(Settings.FullLoadouts[this.curSelectedIndex]);
		fullLoadout.Name = fullLoadout.GenerateName();
		Settings.ModifyFullLoadout(fullLoadout, this.curSelectedIndex, true);
		this.loadoutEntries[this.curSelectedIndex].Setup(fullLoadout, this.curSelectedIndex);
		this.UpdateSideInfo(fullLoadout, this.curSelectedIndex);
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0007F454 File Offset: 0x0007D654
	private void UpdateCurrentLoadout()
	{
		if (this.curSelectedIndex != Settings.CurFullLoadout)
		{
			return;
		}
		Progression.FullLoadout fullLoadout = new Progression.FullLoadout(Settings.FullLoadouts[this.curSelectedIndex]);
		Progression.FullLoadout fullLoadout2 = new Progression.FullLoadout(PlayerControl.myInstance);
		if (fullLoadout.Matches(fullLoadout2))
		{
			return;
		}
		Progression.FullLoadout fullLoadout3 = Settings.ModifyFullLoadout(fullLoadout2, this.curSelectedIndex, false);
		if (fullLoadout3 == null)
		{
			fullLoadout3 = fullLoadout2;
		}
		this.loadoutEntries[this.curSelectedIndex].Loadout = fullLoadout3;
		this.loadoutEntries[this.curSelectedIndex].UpdateDisplayInfo();
		this.UpdateSideInfo(fullLoadout3, this.curSelectedIndex);
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0007F4E8 File Offset: 0x0007D6E8
	private void RequestDeleteLoadout()
	{
		if (this.curSelectedIndex < 0 || Settings.FullLoadouts.Count <= this.curSelectedIndex)
		{
			return;
		}
		Progression.FullLoadout fullLoadout = new Progression.FullLoadout(Settings.FullLoadouts[this.curSelectedIndex]);
		ConfirmationPrompt.OpenPrompt(this.DeleteConfirmation.Replace("%name%", fullLoadout.Name), "Yes", "No", new Action<bool>(this.ConfirmDeleteCurrentLoadout), 0f);
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0007F55D File Offset: 0x0007D75D
	private void ConfirmDeleteCurrentLoadout(bool didConfirm)
	{
		if (!didConfirm)
		{
			return;
		}
		if (this.curSelectedIndex < 0 || Settings.FullLoadouts.Count <= this.curSelectedIndex)
		{
			return;
		}
		Settings.DeleteLoadout(this.curSelectedIndex);
		this.SetupLoadouts();
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0007F590 File Offset: 0x0007D790
	public void EquipSelectedLoadout()
	{
		if (this.curSelectedIndex == Settings.CurFullLoadout)
		{
			return;
		}
		Settings.UpdateEquippedFullLoadout(this.curSelectedIndex);
		foreach (LoadoutEntry loadoutEntry in this.loadoutEntries)
		{
			loadoutEntry.UpdateIsEquipped(this.curSelectedIndex);
		}
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x0007F600 File Offset: 0x0007D800
	public void UpdateSideInfo(Progression.FullLoadout loadout, int index)
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		bool flag = this.curSelectedIndex == Settings.CurFullLoadout;
		Progression.FullLoadout l = new Progression.FullLoadout(PlayerControl.myInstance);
		if (index < 0)
		{
			this.LoadoutTitle.text = "Current Loadout";
		}
		else
		{
			this.LoadoutTitle.text = loadout.Name;
		}
		this.DeletePrompt.SetActive(index >= 0);
		this.UpdatePrompt.SetActive(flag && !loadout.Matches(l));
		this.UnsavedDisplay.SetActive(this.UpdatePrompt.activeSelf);
		this.RenamePrompt.SetActive(index >= 0 && !this.UpdatePrompt.activeSelf);
		MagicColor magicColor = loadout.Abilities.Core.Root.magicColor;
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(magicColor);
		if (core != null)
		{
			this.SignatureIcon.sprite = core.MajorIcon;
		}
		this.SignatureLock.SetActive(!UnlockManager.IsCoreUnlocked(loadout.Abilities.Core));
		this.Primary.Setup(loadout.Abilities.Generator, myInstance, 0);
		this.PrimaryLock.SetActive(!UnlockManager.IsAbilityUnlocked(loadout.Abilities.Generator));
		this.Secondary.Setup(loadout.Abilities.Spender, myInstance, 0);
		this.SecondaryLock.SetActive(!UnlockManager.IsAbilityUnlocked(loadout.Abilities.Spender));
		this.Movement.Setup(loadout.Abilities.Movement, myInstance, 0);
		this.MovementLock.SetActive(!UnlockManager.IsAbilityUnlocked(loadout.Abilities.Movement));
		this.LoadTalentDisplay(magicColor, loadout.Talents);
		this.HeadTitle.text = "- " + loadout.Cosmetics.Head.Name;
		this.RobeTitle.text = "- " + loadout.Cosmetics.Skin.Name;
		this.BookTitle.text = "- " + loadout.Cosmetics.Book.Name;
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x0007F828 File Offset: 0x0007DA28
	private void LoadTalentDisplay(MagicColor color, Progression.EquippedTalents talents)
	{
		foreach (GameObject gameObject in this.talentObjects)
		{
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
		List<int> list = new List<int>();
		List<int> list2;
		if (talents.SelectedTalents.TryGetValue(MagicColor.Neutral, out list2))
		{
			list = list2;
		}
		int talentRowsAvailable = PlayerDB.GetTalentRowsAvailable(MagicColor.Neutral, Progression.InkLevel);
		for (int i = 0; i < this.NeutralRows; i++)
		{
			int selected = -1;
			if (list.Count > i)
			{
				selected = list[i];
			}
			for (int j = 0; j < 3; j++)
			{
				this.CreateTalentBox(i, j, selected, talentRowsAvailable, this.NeutralTalentGrid);
			}
		}
		List<int> list3 = new List<int>();
		List<int> list4;
		if (talents.SelectedTalents.TryGetValue(color, out list4))
		{
			list3 = list4;
		}
		int talentRowsAvailable2 = PlayerDB.GetTalentRowsAvailable(color, Progression.InkLevel);
		for (int k = 0; k < this.SignatureRows; k++)
		{
			int selected2 = -1;
			if (list3.Count > k)
			{
				selected2 = list3[k];
			}
			for (int l = 0; l < 3; l++)
			{
				this.CreateTalentBox(k, l, selected2, talentRowsAvailable2, this.SignatureTalentGrid);
			}
		}
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x0007F968 File Offset: 0x0007DB68
	private void CreateTalentBox(int row, int col, int selected, int rowsUnl, Transform holder)
	{
		GameObject original = this.Talent_Inactive;
		if (selected == col)
		{
			original = this.Talent_Active;
		}
		if (rowsUnl <= row)
		{
			original = this.Talent_Locked;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, holder);
		gameObject.SetActive(true);
		this.talentObjects.Add(gameObject);
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0007F9B0 File Offset: 0x0007DBB0
	public LoadoutPanel()
	{
	}

	// Token: 0x04001389 RID: 5001
	public static LoadoutPanel instance;

	// Token: 0x0400138A RID: 5002
	public GameObject UnsavedDisplay;

	// Token: 0x0400138B RID: 5003
	public Transform LoadoutGrid;

	// Token: 0x0400138C RID: 5004
	public GameObject NewLoadoutButton;

	// Token: 0x0400138D RID: 5005
	public GameObject LoadoutItemRef;

	// Token: 0x0400138E RID: 5006
	public GameObject EmptyItemRef;

	// Token: 0x0400138F RID: 5007
	public AutoScrollRect autoScroller;

	// Token: 0x04001390 RID: 5008
	private List<GameObject> selectables = new List<GameObject>();

	// Token: 0x04001391 RID: 5009
	private List<GameObject> gridObjects = new List<GameObject>();

	// Token: 0x04001392 RID: 5010
	private List<LoadoutEntry> loadoutEntries = new List<LoadoutEntry>();

	// Token: 0x04001393 RID: 5011
	public TextMeshProUGUI LoadoutTitle;

	// Token: 0x04001394 RID: 5012
	public Image SignatureIcon;

	// Token: 0x04001395 RID: 5013
	public GameObject SignatureLock;

	// Token: 0x04001396 RID: 5014
	public PlayerStatAbilityUIGroup Primary;

	// Token: 0x04001397 RID: 5015
	public GameObject PrimaryLock;

	// Token: 0x04001398 RID: 5016
	public PlayerStatAbilityUIGroup Secondary;

	// Token: 0x04001399 RID: 5017
	public GameObject SecondaryLock;

	// Token: 0x0400139A RID: 5018
	public PlayerStatAbilityUIGroup Movement;

	// Token: 0x0400139B RID: 5019
	public GameObject MovementLock;

	// Token: 0x0400139C RID: 5020
	[Header("Cosmetics")]
	public TextMeshProUGUI HeadTitle;

	// Token: 0x0400139D RID: 5021
	public TextMeshProUGUI RobeTitle;

	// Token: 0x0400139E RID: 5022
	public TextMeshProUGUI BookTitle;

	// Token: 0x0400139F RID: 5023
	[Header("Inscriptions")]
	public GameObject Talent_Locked;

	// Token: 0x040013A0 RID: 5024
	public GameObject Talent_Active;

	// Token: 0x040013A1 RID: 5025
	public GameObject Talent_Inactive;

	// Token: 0x040013A2 RID: 5026
	public int NeutralRows = 6;

	// Token: 0x040013A3 RID: 5027
	public Transform NeutralTalentGrid;

	// Token: 0x040013A4 RID: 5028
	public int SignatureRows = 3;

	// Token: 0x040013A5 RID: 5029
	public Transform SignatureTalentGrid;

	// Token: 0x040013A6 RID: 5030
	private List<GameObject> talentObjects = new List<GameObject>();

	// Token: 0x040013A7 RID: 5031
	[Header("Prompts")]
	[TextArea(4, 4)]
	public string DeleteConfirmation;

	// Token: 0x040013A8 RID: 5032
	public GameObject DeletePrompt;

	// Token: 0x040013A9 RID: 5033
	public GameObject UpdatePrompt;

	// Token: 0x040013AA RID: 5034
	public GameObject RenamePrompt;

	// Token: 0x040013AB RID: 5035
	private int curSelectedIndex;
}
