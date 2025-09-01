using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D4 RID: 468
public class BindingsPanel : MonoBehaviour
{
	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06001334 RID: 4916 RVA: 0x000764C8 File Offset: 0x000746C8
	public int CurrentBindingLevel
	{
		get
		{
			int num = 0;
			foreach (AugmentTree augmentTree in this.selectedBindings)
			{
				num += augmentTree.Root.HeatLevel;
			}
			return num;
		}
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x00076528 File Offset: 0x00074728
	private void Awake()
	{
		BindingsPanel.instance = this;
		this.BindingBarRef.SetActive(false);
		foreach (BindingsPanel.TomeBinding binding in this.GetBindingList())
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BindingBarRef, this.BindingBarRef.transform.parent);
			BindingBarUI component = gameObject.GetComponent<BindingBarUI>();
			component.Setup(binding);
			gameObject.SetActive(true);
			this.Bars.Add(component);
		}
		this.SetupBindingNavigation();
		VoteManager.OnVotesChanged = (Action<ChoiceType>)Delegate.Combine(VoteManager.OnVotesChanged, new Action<ChoiceType>(this.OnVotesChanged));
		UIPanel component2 = base.GetComponent<UIPanel>();
		component2.OnEnteredPanel = (Action)Delegate.Combine(component2.OnEnteredPanel, new Action(this.OnEnteredPanel));
		component2.OnTertiaryAction = (Action)Delegate.Combine(component2.OnTertiaryAction, new Action(this.Overview.SubmitInteraction));
		component2.OnRightStickAction = (Action)Delegate.Combine(component2.OnRightStickAction, new Action(this.TryRandomBindings));
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x00076658 File Offset: 0x00074858
	public void Setup(GenreTree Genre)
	{
		this.Reset();
		foreach (TextMeshProUGUI textMeshProUGUI in this.TomeTitles)
		{
			textMeshProUGUI.text = Genre.Root.Name;
		}
		AugmentTree tomePowerAugment = Genre.Root.TomePowerAugment;
		this.TomePowerArea.SetActive(tomePowerAugment != null);
		if (tomePowerAugment != null)
		{
			this.TomePowerBorder_Positive.SetActive(!Genre.Root.IsNegativePower);
			this.TomePowerBorder_Negative.SetActive(Genre.Root.IsNegativePower);
			this.TomePowerIcon.sprite = tomePowerAugment.Root.Icon;
			this.TomePowerTitle.text = tomePowerAugment.Root.Name;
			this.TomePowerDetail.text = TextParser.AugmentDetail(tomePowerAugment.Root.Detail, tomePowerAugment, null, false);
		}
		this.Overview.Setup(Genre);
		if (PanelManager.CurPanel == PanelType.GameInvisible || PanelManager.CurPanel == PanelType.Augments)
		{
			PanelManager.instance.PushPanel(PanelType.Bindings);
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x00076784 File Offset: 0x00074984
	public void ToggleUI()
	{
		if (PanelManager.CurPanel == PanelType.Bindings)
		{
			PanelManager.instance.PopPanel();
			return;
		}
		PanelManager.instance.PushPanel(PanelType.Bindings);
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000767A8 File Offset: 0x000749A8
	private void OnEnteredPanel()
	{
		bool flag = GameplayManager.CurState == GameState.Hub_Bindings;
		this.Overview.gameObject.SetActive(flag);
		this.InfoDisplay.SetActive(!flag);
		this.InteractPrompt.SetActive(flag);
		this.SetupLoadoutButton(flag);
		if (flag)
		{
			this.Overview.BindingsUpdated();
		}
		else
		{
			this.Reset();
		}
		this.UpdateBindingDisplays();
		UISelector.SelectSelectable(this.Bars[0].MainRank.GetComponent<Selectable>());
		if (!UITutorial.TryTutorial(UITutorial.Tutorial.Bindings) && Progression.OverbindAllowed > 0)
		{
			UITutorial.TryTutorial(UITutorial.Tutorial.Ascend_Bindings);
		}
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x00076840 File Offset: 0x00074A40
	public void Reset()
	{
		foreach (BindingBarUI bindingBarUI in this.Bars)
		{
			bindingBarUI.ResetState();
		}
		this.selected = null;
		this.selectedBindings.Clear();
		this.bindingIDSelections.Clear();
		this.OnVotesChanged(ChoiceType.Bindings);
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000768B4 File Offset: 0x00074AB4
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Bindings)
		{
			return;
		}
		if (!UITutorial.InTutorial && InputManager.UIAct.UIBack.WasPressed && GameplayManager.CurState != GameState.Hub_Bindings)
		{
			this.TryLeavePanel();
		}
		foreach (BindingBarUI bindingBarUI in this.Bars)
		{
			bindingBarUI.TickUpdate();
		}
		if (this.Overview.gameObject.activeSelf)
		{
			this.Overview.TickUpdate();
		}
		if (InputManager.IsUsingController)
		{
			this.CheckControllerInputs();
		}
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x00076960 File Offset: 0x00074B60
	public void TryLeavePanel()
	{
		if (PanelManager.CurPanel == PanelType.Bindings)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00076975 File Offset: 0x00074B75
	private void CheckControllerInputs()
	{
		this.Scroller.TickUpdate();
		if (this.PrevBindingsButton.gameObject.activeSelf && InputManager.UIAct.UILeftStickButton.WasPressed)
		{
			this.LoadBindingLoadout();
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000769AC File Offset: 0x00074BAC
	private void SetupBindingNavigation()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (BindingBarUI bindingBarUI in this.Bars)
		{
			list.Add(bindingBarUI.MainRank.gameObject);
		}
		UISelector.SetupVerticalListNav(list, null, null, true);
		for (int i = 0; i < this.Bars.Count; i++)
		{
			BindingBarUI bindingBarUI2 = this.Bars[i];
			Button target = (i < this.Bars.Count - 1) ? this.Bars[i + 1].MainRank.GetComponent<Button>() : this.Bars[0].MainRank.GetComponent<Button>();
			Button component;
			if (i <= 0)
			{
				List<BindingBarUI> bars = this.Bars;
				int index = bars.Count - 1;
				component = bars[index].MainRank.GetComponent<Button>();
			}
			else
			{
				component = this.Bars[i - 1].MainRank.GetComponent<Button>();
			}
			Button target2 = component;
			Button component2 = this.Bars[i].MainRank.GetComponent<Button>();
			UISelector.SetupHorizontalListNav<BindingRankUI>(bindingBarUI2.Ranks, component2, component2, false);
			if (bindingBarUI2.Ranks.Count > 0)
			{
				component2.SetNavigation(bindingBarUI2.Ranks[0].GetComponent<Button>(), UIDirection.Right, false);
			}
			foreach (BindingRankUI bindingRankUI in bindingBarUI2.Ranks)
			{
				bindingRankUI.GetComponent<Button>().SetNavigation(target, UIDirection.Down, false);
				bindingRankUI.GetComponent<Button>().SetNavigation(target2, UIDirection.Up, false);
			}
		}
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x00076B74 File Offset: 0x00074D74
	public void TryAddBinding(AugmentTree binding)
	{
		InkManager.instance.TryAddBinding(binding.ID);
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x00076B86 File Offset: 0x00074D86
	public void TryRemoveBinding(AugmentTree binding)
	{
		InkManager.instance.TryRemoveBinding(binding.ID);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00076B98 File Offset: 0x00074D98
	public void TryRandomBindings()
	{
		InkManager.instance.TryRandomBindings(Mathf.Clamp(Progression.AttunementTarget, 1, 20));
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x00076BB1 File Offset: 0x00074DB1
	public void SetRandomBindings(int level)
	{
		if (this.isRandomizing)
		{
			return;
		}
		this.bindingIDSelections.Clear();
		this.selectedBindings.Clear();
		base.StopAllCoroutines();
		base.StartCoroutine(this.RandomizeBindings(level));
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x00076BE6 File Offset: 0x00074DE6
	private IEnumerator RandomizeBindings(int level)
	{
		this.isRandomizing = true;
		IEnumerable<BindingsPanel.TomeBinding> bindings = from v in this.GetBindingList()
		where UnlockManager.IsBindingUnlocked(v.Augment) && v.Augment.Root.HeatLevel > 0
		select v;
		List<BindingsPanel.TomeBinding> usedRoots = new List<BindingsPanel.TomeBinding>();
		List<BindingsPanel.TomeBinding.BindingRank> usedRanks = new List<BindingsPanel.TomeBinding.BindingRank>();
		int heat = 0;
		int iteration = 0;
		while (heat < level && iteration < 100)
		{
			int num = iteration;
			iteration = num + 1;
			int wantLevel = level;
			if (iteration > 90)
			{
				wantLevel += 2;
			}
			if (iteration > 50)
			{
				wantLevel++;
			}
			BindingsPanel.TomeBinding randomElement = (from v in bindings
			where !usedRoots.Contains(v) && v.Augment.Root.HeatLevel + heat <= wantLevel
			select v).ToList<BindingsPanel.TomeBinding>().GetRandomElement(null);
			string key = (randomElement != null) ? randomElement.Augment.ID : "";
			if ((usedRoots.Count <= 0 || UnityEngine.Random.Range(0, 100) > 33) && randomElement != null && !this.bindingIDSelections.ContainsKey(key))
			{
				heat += randomElement.Augment.Root.HeatLevel;
				usedRoots.Add(randomElement);
				this.bindingIDSelections.Add(key, PlayerControl.myInstance.ViewID);
				this.selectedBindings.Add(randomElement.Augment);
				InkManager.instance.SyncBindings();
				yield return new WaitForSeconds(0.15f);
			}
			else
			{
				BindingsPanel.TomeBinding randomElement2 = usedRoots.GetRandomElement(null);
				if (randomElement2 != null)
				{
					BindingsPanel.TomeBinding.BindingRank randomElement3 = (from v in randomElement2.Ranks
					where !usedRanks.Contains(v) && v.Augment.Root.HeatLevel + heat <= wantLevel && v.Augment.Root.HeatLevel > 0 && UnlockManager.IsBindingUnlocked(v.Augment)
					select v).ToList<BindingsPanel.TomeBinding.BindingRank>().GetRandomElement(null);
					string key2 = (randomElement3 != null) ? randomElement3.Augment.ID : "";
					if (randomElement3 != null && !this.bindingIDSelections.ContainsKey(key2))
					{
						heat += randomElement3.Augment.Root.HeatLevel;
						usedRanks.Add(randomElement3);
						this.bindingIDSelections.Add(randomElement3.Augment.ID, PlayerControl.myInstance.ViewID);
						this.selectedBindings.Add(randomElement3.Augment);
						InkManager.instance.SyncBindings();
						yield return new WaitForSeconds(0.15f);
					}
				}
			}
		}
		this.isRandomizing = false;
		yield break;
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x00076BFC File Offset: 0x00074DFC
	public bool TryAddBindingMaster(int playerID, string bindingID)
	{
		if (this.bindingIDSelections.ContainsKey(bindingID) || this.isRandomizing)
		{
			return false;
		}
		AugmentTree augment = GraphDB.GetAugment(bindingID);
		if (augment == null)
		{
			return false;
		}
		AugmentTree rootBinding = this.GetRootBinding(augment);
		if (rootBinding != augment && rootBinding != null && !this.IsBindingActive(rootBinding))
		{
			return false;
		}
		this.bindingIDSelections.Add(bindingID, playerID);
		this.selectedBindings.Add(augment);
		return true;
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x00076C74 File Offset: 0x00074E74
	public bool TryRemoveBindingMaster(int playerID, string bindingID)
	{
		if (!this.bindingIDSelections.ContainsKey(bindingID) || this.isRandomizing)
		{
			return false;
		}
		AugmentTree augment = GraphDB.GetAugment(bindingID);
		this.bindingIDSelections.Remove(bindingID);
		this.selectedBindings.Remove(augment);
		return true;
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x00076CBC File Offset: 0x00074EBC
	public string GetBindingValues()
	{
		string text = "";
		foreach (KeyValuePair<string, int> keyValuePair in this.bindingIDSelections)
		{
			string text2;
			int num;
			keyValuePair.Deconstruct(out text2, out num);
			string arg = text2;
			int num2 = num;
			text += string.Format("{0},{1}|", arg, num2);
		}
		if (text.Length > 1)
		{
			string text3 = text;
			int num = text3.Length - 1 - 0;
			text = text3.Substring(0, num);
		}
		return text;
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x00076D58 File Offset: 0x00074F58
	public void SetBindingValues(string inputString)
	{
		string[] array = inputString.Split('|', StringSplitOptions.None);
		this.selectedBindings.Clear();
		this.bindingIDSelections.Clear();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string[] array3 = array2[i].Split(',', StringSplitOptions.None);
			if (array3.Length == 2)
			{
				AugmentTree augment = GraphDB.GetAugment(array3[0]);
				int value;
				int.TryParse(array3[1], out value);
				this.selectedBindings.Add(augment);
				this.bindingIDSelections.Add(array3[0], value);
			}
		}
		this.UpdateBindingDisplays();
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x00076DDB File Offset: 0x00074FDB
	private void OnVotesChanged(ChoiceType ct)
	{
		if (ct != ChoiceType.Bindings)
		{
			return;
		}
		this.Overview.OnVotesChanged();
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x00076DF0 File Offset: 0x00074FF0
	public void ApplyBindings()
	{
		if (PanelManager.CurPanel == PanelType.Bindings)
		{
			PanelManager.instance.PopPanel();
		}
		Settings.SaveBindingLoadout(this.GetBindingLoadout());
		string text = "";
		foreach (AugmentTree augmentTree in this.selectedBindings)
		{
			text = text + augmentTree.Root.Name + " | ";
		}
		UnityEngine.Debug.Log("Bindings Applied: " + text);
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GameplayManager.instance.GenreBindings = new Augments();
		GameplayManager.instance.SetBindings(this.selectedBindings, true);
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x00076EB0 File Offset: 0x000750B0
	private string GetBindingLoadout()
	{
		string text = "";
		foreach (AugmentTree augmentTree in this.selectedBindings)
		{
			text = text + augmentTree.ID + "|";
		}
		if (text.Length == 0)
		{
			return text;
		}
		return text.Substring(0, text.Length - 1);
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x00076F30 File Offset: 0x00075130
	private List<AugmentTree> GetPrevBindings(string str)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		string[] array = str.Split('|', StringSplitOptions.None);
		if (array.Length == 0)
		{
			return list;
		}
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			AugmentTree augment = GraphDB.GetAugment(array2[i]);
			if (!(augment == null))
			{
				list.Add(augment);
			}
		}
		return list;
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x00076F80 File Offset: 0x00075180
	private int GetBindingLevel(List<AugmentTree> augments)
	{
		int num = 0;
		foreach (AugmentTree augmentTree in augments)
		{
			num += augmentTree.Root.HeatLevel;
		}
		return num;
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x00076FD8 File Offset: 0x000751D8
	public void LoadBindingLoadout()
	{
		string prevBindings = Settings.PrevBindings;
		if (string.IsNullOrEmpty(prevBindings))
		{
			return;
		}
		InkManager.instance.TryLoadBindings(prevBindings);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00077000 File Offset: 0x00075200
	public bool TryLoadBindings(int playerID, string input)
	{
		List<AugmentTree> prevBindings = this.GetPrevBindings(input);
		if (prevBindings == null || prevBindings.Count == 0)
		{
			return false;
		}
		bool result = false;
		foreach (AugmentTree augmentTree in prevBindings)
		{
			if (!this.bindingIDSelections.ContainsKey(augmentTree.ID))
			{
				this.bindingIDSelections.Add(augmentTree.ID, playerID);
				this.selectedBindings.Add(augmentTree);
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x00077094 File Offset: 0x00075294
	private void SetupLoadoutButton(bool isInBindingSelection)
	{
		bool flag = isInBindingSelection && !string.IsNullOrEmpty(Settings.PrevBindings);
		this.PrevBindingsButton.gameObject.SetActive(flag);
		if (!flag)
		{
			return;
		}
		List<AugmentTree> prevBindings = this.GetPrevBindings(Settings.PrevBindings);
		int bindingLevel = this.GetBindingLevel(prevBindings);
		this.PrevBindingValueText.text = bindingLevel.ToString();
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x000770F0 File Offset: 0x000752F0
	private AugmentTree GetRootBinding(AugmentTree binding)
	{
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (bindingUnlock.Binding == binding)
			{
				return bindingUnlock.Parent;
			}
		}
		return binding;
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x0007715C File Offset: 0x0007535C
	private void UpdateBindingDisplays()
	{
		foreach (BindingBarUI bindingBarUI in this.Bars)
		{
			bindingBarUI.MainRank.SetSelected(this.selectedBindings.Contains(bindingBarUI.MainRank.binding));
			foreach (BindingRankUI bindingRankUI in bindingBarUI.Ranks)
			{
				bindingRankUI.SetSelected(this.selectedBindings.Contains(bindingRankUI.binding));
			}
		}
		this.Overview.BindingsUpdated();
		AudioManager.PlayInterfaceSFX(this.BindingLevelChangedSFX, 1f, UnityEngine.Random.Range(0.9f, 1.1f));
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00077248 File Offset: 0x00075448
	public bool IsBindingActive(AugmentTree binding)
	{
		return this.selectedBindings.Contains(binding);
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x00077256 File Offset: 0x00075456
	public void SelectBinding(AugmentTree mod)
	{
		if (GameplayManager.CurState != GameState.Hub_Bindings)
		{
			return;
		}
		if (!UnlockManager.IsBindingUnlocked(mod))
		{
			return;
		}
		if (!this.selectedBindings.Contains(mod))
		{
			this.TryAddBinding(mod);
		}
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x00077280 File Offset: 0x00075480
	public List<BindingsPanel.TomeBinding> GetBindingList()
	{
		List<BindingsPanel.TomeBinding> list = new List<BindingsPanel.TomeBinding>();
		Dictionary<AugmentTree, BindingsPanel.TomeBinding> dictionary = new Dictionary<AugmentTree, BindingsPanel.TomeBinding>();
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (bindingUnlock.Parent == null)
			{
				dictionary.Add(bindingUnlock.Binding, new BindingsPanel.TomeBinding(bindingUnlock.Binding));
			}
		}
		foreach (UnlockDB.BindingUnlock bindingUnlock2 in UnlockDB.DB.Bindings)
		{
			if (!(bindingUnlock2.Parent == null) && dictionary.ContainsKey(bindingUnlock2.Parent))
			{
				dictionary[bindingUnlock2.Parent].AddSubBinding(bindingUnlock2.Binding);
			}
		}
		foreach (KeyValuePair<AugmentTree, BindingsPanel.TomeBinding> keyValuePair in dictionary)
		{
			keyValuePair.Value.Ranks.Sort((BindingsPanel.TomeBinding.BindingRank x, BindingsPanel.TomeBinding.BindingRank y) => x.Value.CompareTo(y.Value));
			list.Add(keyValuePair.Value);
		}
		return list;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000773F0 File Offset: 0x000755F0
	public BindingsPanel()
	{
	}

	// Token: 0x0400124D RID: 4685
	public static BindingsPanel instance;

	// Token: 0x0400124E RID: 4686
	public BindingProgressAreaUI Overview;

	// Token: 0x0400124F RID: 4687
	public GameObject InfoDisplay;

	// Token: 0x04001250 RID: 4688
	public List<TextMeshProUGUI> TomeTitles;

	// Token: 0x04001251 RID: 4689
	public GameObject TomePowerArea;

	// Token: 0x04001252 RID: 4690
	public GameObject TomePowerBorder_Positive;

	// Token: 0x04001253 RID: 4691
	public GameObject TomePowerBorder_Negative;

	// Token: 0x04001254 RID: 4692
	public TextMeshProUGUI TomePowerDetail;

	// Token: 0x04001255 RID: 4693
	public TextMeshProUGUI TomePowerTitle;

	// Token: 0x04001256 RID: 4694
	public Image TomePowerIcon;

	// Token: 0x04001257 RID: 4695
	public GameObject InteractPrompt;

	// Token: 0x04001258 RID: 4696
	public AudioClip BindingLevelChangedSFX;

	// Token: 0x04001259 RID: 4697
	public GameObject BindingBarRef;

	// Token: 0x0400125A RID: 4698
	public AutoScrollRect Scroller;

	// Token: 0x0400125B RID: 4699
	public Button PrevBindingsButton;

	// Token: 0x0400125C RID: 4700
	public TextMeshProUGUI PrevBindingValueText;

	// Token: 0x0400125D RID: 4701
	public List<BindingBarUI> Bars = new List<BindingBarUI>();

	// Token: 0x0400125E RID: 4702
	private Dictionary<string, int> bindingIDSelections = new Dictionary<string, int>();

	// Token: 0x0400125F RID: 4703
	[NonSerialized]
	public List<AugmentTree> selectedBindings = new List<AugmentTree>();

	// Token: 0x04001260 RID: 4704
	private bool isRandomizing;

	// Token: 0x04001261 RID: 4705
	private AugmentTree selected;

	// Token: 0x02000596 RID: 1430
	[Serializable]
	public class TomeBinding
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000D176F File Offset: 0x000CF96F
		public int Value
		{
			get
			{
				return this.Augment.Root.HeatLevel;
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000D1781 File Offset: 0x000CF981
		public TomeBinding(AugmentTree root)
		{
			this.Augment = root;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000D179C File Offset: 0x000CF99C
		public void AddSubBinding(AugmentTree sub)
		{
			BindingsPanel.TomeBinding.BindingRank bindingRank = new BindingsPanel.TomeBinding.BindingRank();
			bindingRank.Augment = sub;
			this.Ranks.Add(bindingRank);
		}

		// Token: 0x040027D5 RID: 10197
		public AugmentTree Augment;

		// Token: 0x040027D6 RID: 10198
		public List<BindingsPanel.TomeBinding.BindingRank> Ranks = new List<BindingsPanel.TomeBinding.BindingRank>();

		// Token: 0x020006C3 RID: 1731
		[Serializable]
		public class BindingRank
		{
			// Token: 0x170003DD RID: 989
			// (get) Token: 0x0600286B RID: 10347 RVA: 0x000D8ACE File Offset: 0x000D6CCE
			public int Value
			{
				get
				{
					return this.Augment.Root.HeatLevel;
				}
			}

			// Token: 0x0600286C RID: 10348 RVA: 0x000D8AE0 File Offset: 0x000D6CE0
			public BindingRank()
			{
			}

			// Token: 0x04002CE5 RID: 11493
			public AugmentTree Augment;
		}
	}

	// Token: 0x02000597 RID: 1431
	[CompilerGenerated]
	private sealed class <>c__DisplayClass35_0
	{
		// Token: 0x0600257C RID: 9596 RVA: 0x000D17C2 File Offset: 0x000CF9C2
		public <>c__DisplayClass35_0()
		{
		}

		// Token: 0x040027D7 RID: 10199
		public List<BindingsPanel.TomeBinding> usedRoots;

		// Token: 0x040027D8 RID: 10200
		public int heat;

		// Token: 0x040027D9 RID: 10201
		public List<BindingsPanel.TomeBinding.BindingRank> usedRanks;
	}

	// Token: 0x02000598 RID: 1432
	[CompilerGenerated]
	private sealed class <>c__DisplayClass35_1
	{
		// Token: 0x0600257D RID: 9597 RVA: 0x000D17CA File Offset: 0x000CF9CA
		public <>c__DisplayClass35_1()
		{
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000D17D2 File Offset: 0x000CF9D2
		internal bool <RandomizeBindings>b__1(BindingsPanel.TomeBinding v)
		{
			return !this.CS$<>8__locals1.usedRoots.Contains(v) && v.Augment.Root.HeatLevel + this.CS$<>8__locals1.heat <= this.wantLevel;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000D1810 File Offset: 0x000CFA10
		internal bool <RandomizeBindings>b__2(BindingsPanel.TomeBinding.BindingRank v)
		{
			return !this.CS$<>8__locals1.usedRanks.Contains(v) && v.Augment.Root.HeatLevel + this.CS$<>8__locals1.heat <= this.wantLevel && v.Augment.Root.HeatLevel > 0 && UnlockManager.IsBindingUnlocked(v.Augment);
		}

		// Token: 0x040027DA RID: 10202
		public int wantLevel;

		// Token: 0x040027DB RID: 10203
		public BindingsPanel.<>c__DisplayClass35_0 CS$<>8__locals1;
	}

	// Token: 0x02000599 RID: 1433
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x000D1874 File Offset: 0x000CFA74
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000D1880 File Offset: 0x000CFA80
		public <>c()
		{
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000D1888 File Offset: 0x000CFA88
		internal bool <RandomizeBindings>b__35_0(BindingsPanel.TomeBinding v)
		{
			return UnlockManager.IsBindingUnlocked(v.Augment) && v.Augment.Root.HeatLevel > 0;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000D18AC File Offset: 0x000CFAAC
		internal int <GetBindingList>b__53_0(BindingsPanel.TomeBinding.BindingRank x, BindingsPanel.TomeBinding.BindingRank y)
		{
			return x.Value.CompareTo(y.Value);
		}

		// Token: 0x040027DC RID: 10204
		public static readonly BindingsPanel.<>c <>9 = new BindingsPanel.<>c();

		// Token: 0x040027DD RID: 10205
		public static Func<BindingsPanel.TomeBinding, bool> <>9__35_0;

		// Token: 0x040027DE RID: 10206
		public static Comparison<BindingsPanel.TomeBinding.BindingRank> <>9__53_0;
	}

	// Token: 0x0200059A RID: 1434
	[CompilerGenerated]
	private sealed class <RandomizeBindings>d__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x000D18CD File Offset: 0x000CFACD
		[DebuggerHidden]
		public <RandomizeBindings>d__35(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000D18DC File Offset: 0x000CFADC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000D18E0 File Offset: 0x000CFAE0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			BindingsPanel bindingsPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				CS$<>8__locals1 = new BindingsPanel.<>c__DisplayClass35_0();
				bindingsPanel.isRandomizing = true;
				bindings = bindingsPanel.GetBindingList().Where(new Func<BindingsPanel.TomeBinding, bool>(BindingsPanel.<>c.<>9.<RandomizeBindings>b__35_0));
				CS$<>8__locals1.usedRoots = new List<BindingsPanel.TomeBinding>();
				CS$<>8__locals1.usedRanks = new List<BindingsPanel.TomeBinding.BindingRank>();
				CS$<>8__locals1.heat = 0;
				iteration = 0;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			while (CS$<>8__locals1.heat < level && iteration < 100)
			{
				BindingsPanel.<>c__DisplayClass35_1 CS$<>8__locals2 = new BindingsPanel.<>c__DisplayClass35_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				int num2 = iteration;
				iteration = num2 + 1;
				CS$<>8__locals2.wantLevel = level;
				if (iteration > 90)
				{
					CS$<>8__locals2.wantLevel += 2;
				}
				if (iteration > 50)
				{
					CS$<>8__locals2.wantLevel++;
				}
				BindingsPanel.TomeBinding randomElement = bindings.Where(new Func<BindingsPanel.TomeBinding, bool>(CS$<>8__locals2.<RandomizeBindings>b__1)).ToList<BindingsPanel.TomeBinding>().GetRandomElement(null);
				string key = (randomElement != null) ? randomElement.Augment.ID : "";
				if ((CS$<>8__locals2.CS$<>8__locals1.usedRoots.Count <= 0 || UnityEngine.Random.Range(0, 100) > 33) && randomElement != null && !bindingsPanel.bindingIDSelections.ContainsKey(key))
				{
					CS$<>8__locals2.CS$<>8__locals1.heat = CS$<>8__locals2.CS$<>8__locals1.heat + randomElement.Augment.Root.HeatLevel;
					CS$<>8__locals2.CS$<>8__locals1.usedRoots.Add(randomElement);
					bindingsPanel.bindingIDSelections.Add(key, PlayerControl.myInstance.ViewID);
					bindingsPanel.selectedBindings.Add(randomElement.Augment);
					InkManager.instance.SyncBindings();
					this.<>2__current = new WaitForSeconds(0.15f);
					this.<>1__state = 1;
					return true;
				}
				BindingsPanel.TomeBinding randomElement2 = CS$<>8__locals2.CS$<>8__locals1.usedRoots.GetRandomElement(null);
				if (randomElement2 != null)
				{
					BindingsPanel.TomeBinding.BindingRank randomElement3 = randomElement2.Ranks.Where(new Func<BindingsPanel.TomeBinding.BindingRank, bool>(CS$<>8__locals2.<RandomizeBindings>b__2)).ToList<BindingsPanel.TomeBinding.BindingRank>().GetRandomElement(null);
					string key2 = (randomElement3 != null) ? randomElement3.Augment.ID : "";
					if (randomElement3 != null && !bindingsPanel.bindingIDSelections.ContainsKey(key2))
					{
						CS$<>8__locals2.CS$<>8__locals1.heat = CS$<>8__locals2.CS$<>8__locals1.heat + randomElement3.Augment.Root.HeatLevel;
						CS$<>8__locals2.CS$<>8__locals1.usedRanks.Add(randomElement3);
						bindingsPanel.bindingIDSelections.Add(randomElement3.Augment.ID, PlayerControl.myInstance.ViewID);
						bindingsPanel.selectedBindings.Add(randomElement3.Augment);
						InkManager.instance.SyncBindings();
						this.<>2__current = new WaitForSeconds(0.15f);
						this.<>1__state = 2;
						return true;
					}
				}
			}
			bindingsPanel.isRandomizing = false;
			return false;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x000D1C23 File Offset: 0x000CFE23
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000D1C2B File Offset: 0x000CFE2B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000D1C32 File Offset: 0x000CFE32
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027DF RID: 10207
		private int <>1__state;

		// Token: 0x040027E0 RID: 10208
		private object <>2__current;

		// Token: 0x040027E1 RID: 10209
		public BindingsPanel <>4__this;

		// Token: 0x040027E2 RID: 10210
		private BindingsPanel.<>c__DisplayClass35_0 <>8__1;

		// Token: 0x040027E3 RID: 10211
		public int level;

		// Token: 0x040027E4 RID: 10212
		private IEnumerable<BindingsPanel.TomeBinding> <bindings>5__2;

		// Token: 0x040027E5 RID: 10213
		private int <iteration>5__3;
	}
}
