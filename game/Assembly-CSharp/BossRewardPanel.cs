using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D6 RID: 470
public class BossRewardPanel : MonoBehaviour
{
	// Token: 0x06001379 RID: 4985 RVA: 0x0007911C File Offset: 0x0007731C
	private void Awake()
	{
		BossRewardPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnExit));
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0007914B File Offset: 0x0007734B
	public static void Open(Progression.BossRewardType rewardType, List<GraphTree> rewards, int currency, string detailOverride)
	{
		if (PanelManager.CurPanel != PanelType.BossReward)
		{
			PanelManager.instance.PushPanel(PanelType.BossReward);
		}
		BossRewardPanel.instance.Setup(rewardType, rewards, currency, detailOverride);
		BossRewardPanel.instance.ParticlesDelayed();
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0007917A File Offset: 0x0007737A
	public static void Open(Cosmetic reward, string detailOverride)
	{
		if (PanelManager.CurPanel != PanelType.BossReward)
		{
			PanelManager.instance.PushPanel(PanelType.BossReward);
		}
		BossRewardPanel.instance.SetupCosmetic(reward, detailOverride);
		BossRewardPanel.instance.ParticlesDelayed();
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000791A7 File Offset: 0x000773A7
	public static void Open(NookDB.NookObject reward, string detailOverride)
	{
		if (PanelManager.CurPanel != PanelType.BossReward)
		{
			PanelManager.instance.PushPanel(PanelType.BossReward);
		}
		BossRewardPanel.instance.SetupNookItem(reward);
		BossRewardPanel.instance.ParticlesDelayed();
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000791D3 File Offset: 0x000773D3
	private void ParticlesDelayed()
	{
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			if (PanelManager.CurPanel != PanelType.BossReward)
			{
				return;
			}
			BossRewardPanel.instance.RewardParticles.Play();
		}, 0.66f);
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x00079204 File Offset: 0x00077404
	private void Setup(Progression.BossRewardType rewardType, List<GraphTree> rewards, int currency, string detailOverride)
	{
		BossRewardPanel.RewardDisplay display = this.GetDisplay(rewardType);
		if (display == null)
		{
			return;
		}
		this.Header.text = display.Title;
		this.RewardInfo.text = display.Description;
		if (!string.IsNullOrEmpty(detailOverride))
		{
			this.RewardInfo.text = detailOverride;
		}
		foreach (BossRewardPanel.RewardDisplay rewardDisplay in this.Displays)
		{
			rewardDisplay.DisplayObj.SetActive(rewardDisplay == display);
		}
		switch (rewardType)
		{
		case Progression.BossRewardType.Tome:
			this.DisplayTome(display, rewards[0] as GenreTree);
			break;
		case Progression.BossRewardType.Binding:
			this.DisplayBinding(display, (rewards.Count > 0) ? (rewards[0] as AugmentTree) : null);
			break;
		case Progression.BossRewardType.Pages:
			this.DisplayPages(display, rewards);
			break;
		case Progression.BossRewardType.CosmCurrency:
			this.DisplayCurrency(display, currency);
			break;
		case Progression.BossRewardType.Quillmarks:
			this.DisplayQuillmarks(display, currency);
			break;
		case Progression.BossRewardType.Cosmetic:
			this.DisplayCosmetic(display);
			break;
		case Progression.BossRewardType.NookItem:
			this.DisplayCosmetic(display);
			break;
		}
		this.interactDelay = Time.realtimeSinceStartup;
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x0007933C File Offset: 0x0007753C
	private void SetupCosmetic(Cosmetic c, string detailOverride = "")
	{
		BossRewardPanel.RewardDisplay display = this.GetDisplay(Progression.BossRewardType.Cosmetic);
		if (display == null)
		{
			return;
		}
		this.Header.text = display.Title;
		this.RewardInfo.text = display.Description;
		if (!string.IsNullOrEmpty(detailOverride))
		{
			this.RewardInfo.text = detailOverride;
		}
		foreach (BossRewardPanel.RewardDisplay rewardDisplay in this.Displays)
		{
			rewardDisplay.DisplayObj.SetActive(rewardDisplay == display);
		}
		this.DisplayCosmetic(display);
		this.interactDelay = Time.realtimeSinceStartup;
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000793EC File Offset: 0x000775EC
	private void SetupNookItem(NookDB.NookObject item)
	{
		BossRewardPanel.RewardDisplay display = this.GetDisplay(Progression.BossRewardType.NookItem);
		if (display == null)
		{
			return;
		}
		this.Header.text = item.Name + " Unlocked!";
		this.RewardInfo.text = display.Description;
		this.NookItemIcon.sprite = item.Icon;
		foreach (BossRewardPanel.RewardDisplay rewardDisplay in this.Displays)
		{
			rewardDisplay.DisplayObj.SetActive(rewardDisplay == display);
		}
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x00079490 File Offset: 0x00077690
	private void DisplayCurrency(BossRewardPanel.RewardDisplay display, int amount)
	{
		this.CurrencyText.text = amount.ToString();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000794A4 File Offset: 0x000776A4
	private void DisplayQuillmarks(BossRewardPanel.RewardDisplay display, int amount)
	{
		this.QuillmarkText.text = amount.ToString();
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000794B8 File Offset: 0x000776B8
	private void DisplayCosmetic(BossRewardPanel.RewardDisplay display)
	{
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000794BC File Offset: 0x000776BC
	private void DisplayBinding(BossRewardPanel.RewardDisplay display, AugmentTree binding)
	{
		if (binding == null)
		{
			this.BindingTitle.text = "???";
			this.BindingDetail.text = "<sprite name=\"binding\"> Bindings are now available to enhance your future runs.";
			return;
		}
		this.BindingTitle.text = binding.Root.Name;
		this.BindingDetail.text = TextParser.AugmentDetail(binding.Root.Detail, binding, null, false);
		this.BindingIcon.sprite = binding.Root.Icon;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x00079540 File Offset: 0x00077740
	private void DisplayTome(BossRewardPanel.RewardDisplay display, GenreTree tome)
	{
		this.Header.text = tome.Root.Name;
		BossRewardPanel.TomeRewardPreview tomeRewardPreview = this.BookOptions[UnityEngine.Random.Range(0, this.BookOptions.Count)];
		this.BookImage.sprite = tomeRewardPreview.BookImage;
		this.BookIcon.sprite = tome.Root.Icon;
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000795A8 File Offset: 0x000777A8
	private void DisplayPages(BossRewardPanel.RewardDisplay display, List<GraphTree> trees)
	{
		foreach (GameObject obj in this.pageRefs)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.pageRefs.Clear();
		foreach (GraphTree graphTree in trees)
		{
			if (!(graphTree == null))
			{
				AugmentTree augmentTree = graphTree as AugmentTree;
				if (augmentTree != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PageRef, this.PageRef.transform.parent);
					gameObject.SetActive(true);
					gameObject.transform.GetChild(0).localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(-5f, 5f));
					gameObject.transform.GetChild(0).localPosition += new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-3, 3), 0f);
					gameObject.GetComponent<AugmentDetailBox>().SetupAugment(augmentTree, 1, 0f, null, true);
					this.pageRefs.Add(gameObject);
				}
			}
		}
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x0007970C File Offset: 0x0007790C
	private BossRewardPanel.RewardDisplay GetDisplay(Progression.BossRewardType reward)
	{
		foreach (BossRewardPanel.RewardDisplay rewardDisplay in this.Displays)
		{
			if (rewardDisplay.RewardType == reward)
			{
				return rewardDisplay;
			}
		}
		return null;
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x00079768 File Offset: 0x00077968
	public void TryExit()
	{
		if (PanelManager.CurPanel != PanelType.BossReward)
		{
			return;
		}
		if (Time.realtimeSinceStartup - this.interactDelay < 0.5f)
		{
			return;
		}
		PanelManager.instance.PopPanel();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x00079792 File Offset: 0x00077992
	private void OnExit()
	{
		this.RewardParticles.Stop();
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000797A0 File Offset: 0x000779A0
	public void Test()
	{
		if (this.TestReward == Progression.BossRewardType.Tome)
		{
			BossRewardPanel.Open(Progression.BossRewardType.Tome, new List<GraphTree>
			{
				UnlockDB.DB.DemoData.Tomes[0]
			}, 0, "");
		}
		if (this.TestReward == Progression.BossRewardType.Binding)
		{
			BossRewardPanel.Open(Progression.BossRewardType.Binding, new List<GraphTree>
			{
				UnlockDB.DB.DemoData.Bindings[0]
			}, 0, "");
		}
		if (this.TestReward == Progression.BossRewardType.Pages)
		{
			BossRewardPanel.Open(Progression.BossRewardType.Pages, new List<GraphTree>
			{
				UnlockDB.DB.DemoData.Augments[0],
				UnlockDB.DB.DemoData.Augments[1],
				UnlockDB.DB.DemoData.Augments[2]
			}, 0, "");
		}
		if (this.TestReward == Progression.BossRewardType.CosmCurrency)
		{
			BossRewardPanel.Open(Progression.BossRewardType.CosmCurrency, new List<GraphTree>(), 80, "");
		}
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x0007989D File Offset: 0x00077A9D
	public BossRewardPanel()
	{
	}

	// Token: 0x0400129F RID: 4767
	public static BossRewardPanel instance;

	// Token: 0x040012A0 RID: 4768
	public TextMeshProUGUI Header;

	// Token: 0x040012A1 RID: 4769
	public TextMeshProUGUI RewardInfo;

	// Token: 0x040012A2 RID: 4770
	public List<BossRewardPanel.RewardDisplay> Displays;

	// Token: 0x040012A3 RID: 4771
	public ParticleSystem RewardParticles;

	// Token: 0x040012A4 RID: 4772
	[Header("Tome")]
	public Image BookIcon;

	// Token: 0x040012A5 RID: 4773
	public Image BookImage;

	// Token: 0x040012A6 RID: 4774
	public List<BossRewardPanel.TomeRewardPreview> BookOptions;

	// Token: 0x040012A7 RID: 4775
	[Header("Pages")]
	public GameObject PageRef;

	// Token: 0x040012A8 RID: 4776
	private List<GameObject> pageRefs = new List<GameObject>();

	// Token: 0x040012A9 RID: 4777
	[Header("Binding")]
	public TextMeshProUGUI BindingTitle;

	// Token: 0x040012AA RID: 4778
	public TextMeshProUGUI BindingDetail;

	// Token: 0x040012AB RID: 4779
	public Image BindingIcon;

	// Token: 0x040012AC RID: 4780
	[Header("Currency")]
	public TextMeshProUGUI CurrencyText;

	// Token: 0x040012AD RID: 4781
	public TextMeshProUGUI QuillmarkText;

	// Token: 0x040012AE RID: 4782
	[Header("Nook Item")]
	public Image NookItemIcon;

	// Token: 0x040012AF RID: 4783
	public Progression.BossRewardType TestReward;

	// Token: 0x040012B0 RID: 4784
	private float interactDelay;

	// Token: 0x0200059E RID: 1438
	[Serializable]
	public class RewardDisplay
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x000D1D6E File Offset: 0x000CFF6E
		public RewardDisplay()
		{
		}

		// Token: 0x040027F2 RID: 10226
		public Progression.BossRewardType RewardType;

		// Token: 0x040027F3 RID: 10227
		public string Title;

		// Token: 0x040027F4 RID: 10228
		[TextArea(2, 4)]
		public string Description;

		// Token: 0x040027F5 RID: 10229
		[Header("Reward Display Data")]
		public GameObject DisplayObj;

		// Token: 0x040027F6 RID: 10230
		public TextMeshProUGUI TitleText;

		// Token: 0x040027F7 RID: 10231
		public TextMeshProUGUI DetailText;

		// Token: 0x040027F8 RID: 10232
		public Image Icon;

		// Token: 0x040027F9 RID: 10233
		public GameObject PageDisplayRef;
	}

	// Token: 0x0200059F RID: 1439
	[Serializable]
	public class TomeRewardPreview
	{
		// Token: 0x06002593 RID: 9619 RVA: 0x000D1D76 File Offset: 0x000CFF76
		public TomeRewardPreview()
		{
		}

		// Token: 0x040027FA RID: 10234
		public Sprite BookImage;

		// Token: 0x040027FB RID: 10235
		public Color TextColor;
	}

	// Token: 0x020005A0 RID: 1440
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002594 RID: 9620 RVA: 0x000D1D7E File Offset: 0x000CFF7E
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000D1D8A File Offset: 0x000CFF8A
		public <>c()
		{
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000D1D92 File Offset: 0x000CFF92
		internal void <ParticlesDelayed>b__22_0()
		{
			if (PanelManager.CurPanel != PanelType.BossReward)
			{
				return;
			}
			BossRewardPanel.instance.RewardParticles.Play();
		}

		// Token: 0x040027FC RID: 10236
		public static readonly BossRewardPanel.<>c <>9 = new BossRewardPanel.<>c();

		// Token: 0x040027FD RID: 10237
		public static Action <>9__22_0;
	}
}
