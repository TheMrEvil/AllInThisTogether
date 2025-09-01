using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000181 RID: 385
public class Tooltip : MonoBehaviour
{
	// Token: 0x06001039 RID: 4153 RVA: 0x0006588C File Offset: 0x00063A8C
	private void Awake()
	{
		Tooltip.instance = this;
		this.canvasRect = base.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		this.KeywordLayout = this.KeywordRect.GetComponent<VerticalLayoutGroup>();
		this.rect = base.GetComponent<RectTransform>();
		this.cgroup = base.GetComponent<CanvasGroup>();
		this.cgroup.alpha = 0f;
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x000658EC File Offset: 0x00063AEC
	public static void Show(Vector3 pos, TextAnchor anchor, AugmentTree augment, int stackCount = 1, EntityControl owner = null)
	{
		if (Tooltip.instance == null || augment == null)
		{
			return;
		}
		AugmentRootNode root = augment.Root;
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Augment, augment.Root.Detail);
		Tooltip.instance.SetLocation(pos, anchor);
		GameDB.QualityInfo qualityInfo = GameDB.Quality(root.DisplayQuality);
		bool flag = root.modType == ModType.Player || root.modType == ModType.Fountain;
		Tooltip.instance.AugmentIcon.sprite = root.Icon;
		Tooltip.instance.AugmentTitle.text = root.Name + ((stackCount > 1) ? (" x" + stackCount.ToString()) : "");
		Tooltip.instance.AugmentTitle.color = ((root.modType == ModType.Enemy) ? qualityInfo.EnemyColor : qualityInfo.PlayerColor);
		Tooltip.instance.AugmentDetail.text = TextParser.AugmentDetail(root.Detail, augment, owner, false);
		Tooltip.instance.Aug_Border_Enemy.SetActive(root.modType == ModType.Enemy);
		Tooltip.instance.Aug_Border_Binding.SetActive(root.modType == ModType.Binding);
		Tooltip.instance.Aug_Border_Player.gameObject.SetActive(flag);
		if (flag)
		{
			Tooltip.instance.Aug_Border_Player.sprite = qualityInfo.Border;
		}
		Tooltip.instance.HeatGroup.SetActive(root.modType == ModType.Binding && root.HeatLevel > 0);
		if (root.modType == ModType.Binding)
		{
			Tooltip.instance.HeatLevel.text = root.HeatLevel.ToString();
		}
		PlayerControl.myInstance.Display.ShowRange(root.DisplayRadius);
		Tooltip.instance.ShowKeywords(root, owner);
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x00065AB0 File Offset: 0x00063CB0
	public static void Show(Vector3 pos, TextAnchor anchor, AbilityTree ability, EntityControl owner = null)
	{
		if (Tooltip.instance == null || ability == null)
		{
			return;
		}
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Ability, "");
		Tooltip.instance.SetLocation(pos, anchor);
		AbilityRootNode root = ability.Root;
		Tooltip.instance.AbilityTitle.text = TextParser.GetAbilityActionSprite(root.PlrAbilityType) + " " + root.Usage.AbilityMetadata.Name;
		Tooltip.instance.AbilityDetail.text = TextParser.AugmentDetail(root.Usage.AbilityMetadata.Detail, null, null, false);
		Tooltip.instance.AbilityIcon.sprite = root.Usage.AbilityMetadata.Icon;
		if (!(owner == null))
		{
			PlayerControl playerControl = owner as PlayerControl;
			if (playerControl != null)
			{
				EffectProperties props = new EffectProperties(owner)
				{
					AbilityType = root.PlrAbilityType
				};
				float num = playerControl.ModifyManaCost(props, root.Usage.AbilityMetadata.ManaCost);
				Tooltip.instance.ManaGroup.SetActive(num >= 1f);
				Tooltip.instance.ManaCost.text = (((int)num).ToString() ?? "");
				goto IL_184;
			}
		}
		Tooltip.instance.ManaGroup.SetActive(root.Usage.AbilityMetadata.ManaCost > 0f);
		Tooltip.instance.ManaCost.text = (root.Usage.AbilityMetadata.ManaCost.ToString() ?? "");
		IL_184:
		Tooltip.instance.ShowKeywords(ability, owner);
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00065C50 File Offset: 0x00063E50
	public static void Show(Vector3 pos, TextAnchor anchor, StatusRootNode status, EntityControl owner = null)
	{
		if (Tooltip.instance == null || status == null)
		{
			return;
		}
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Status, "");
		Tooltip.instance.SetLocation(pos, anchor);
		Tooltip.instance.StatusTitle.text = status.EffectName;
		Tooltip.instance.StatusDetail.text = TextParser.AugmentDetail(status.Description, null, null, false);
		Tooltip.instance.StatusIcon.sprite = status.EffectIcon;
		Tooltip.instance.StatusBorderPositive.SetActive(!status.IsNegative);
		Tooltip.instance.StatusBorderNegative.SetActive(status.IsNegative);
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x00065D04 File Offset: 0x00063F04
	public static void Show(Vector3 pos, TextAnchor anchor, InfoTooltip data)
	{
		if (Tooltip.instance == null || data == null)
		{
			return;
		}
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Info, "");
		Tooltip.instance.SetLocation(pos, anchor);
		Tooltip.instance.rect.sizeDelta = data.Size;
		Tooltip.instance.InfoTitle.text = data.Title;
		Tooltip.instance.InfoDetail.text = data.Detail;
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00065D84 File Offset: 0x00063F84
	public static void Show(Vector3 pos, TextAnchor anchor, Tooltip.SimpleInfoData data)
	{
		if (Tooltip.instance == null || data == null)
		{
			return;
		}
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Info, "");
		Tooltip.instance.SetLocation(pos, anchor);
		Tooltip.instance.rect.sizeDelta = data.Size;
		Tooltip.instance.InfoTitle.text = data.Title;
		Tooltip.instance.InfoDetail.text = data.Detail;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x00065E00 File Offset: 0x00064000
	public static void Show(Vector3 pos, TextAnchor anchor, GenreTree tome, bool useUnlockInfo = false)
	{
		if (Tooltip.instance == null || tome == null)
		{
			return;
		}
		GenreRootNode root = tome.Root;
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Tome, "");
		Tooltip.instance.SetLocation(pos, anchor);
		Tooltip.instance.TomeIcon.sprite = root.Icon;
		Tooltip.instance.TomeTitle.text = root.ShortName;
		Tooltip.instance.TomeDetail.text = "A new Tome to cleanse!";
		if (useUnlockInfo)
		{
			string text = "Unlock Info Unknown!";
			UnlockDB.GenreUnlock genreUnlock = UnlockDB.GetGenreUnlock(tome);
			if (genreUnlock.UnlockedBy == Unlockable.UnlockType.TomeReward || genreUnlock.UnlockedBy == Unlockable.UnlockType.Achievement)
			{
				text = genreUnlock.GetUnlockReqText();
			}
			Tooltip.instance.TomeDetail.text = text;
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x00065EC0 File Offset: 0x000640C0
	public static void Show(Vector3 pos, TextAnchor anchor, Cosmetic c)
	{
		if (Tooltip.instance == null || c == null)
		{
			return;
		}
		Tooltip.instance.SetTooltipType(Tooltip.TooltipType.Cosmetic, "");
		Tooltip.instance.SetLocation(pos, anchor);
		Tooltip.instance.CosmeticTitle.text = c.Name;
		string text = "Cosmetic Item";
		if (c.CType() == CosmeticType.Book)
		{
			text = "Book";
		}
		else if (c.CType() == CosmeticType.Skin)
		{
			text = "Regalia";
		}
		else if (c.CType() == CosmeticType.Skin)
		{
			text = "Stylus";
		}
		Tooltip.instance.CosmeticDetail.text = text;
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x00065F58 File Offset: 0x00064158
	public static Vector3 GetLocationFromWorld(Vector3 WorldPoint)
	{
		if (PlayerControl.MyCamera == null)
		{
			return Vector3.zero;
		}
		Vector3 vector = PlayerControl.MyCamera.WorldToViewportPoint(WorldPoint);
		vector = new Vector3(Mathf.Clamp(vector.x, 0f, 1f), Mathf.Clamp(vector.y, 0f, 1f), vector.z);
		Transform transform = PlayerControl.MyCamera.transform;
		float num = Vector3.Dot(transform.forward, (WorldPoint - transform.position).normalized);
		if (vector.z < 0f)
		{
			vector.x = (float)((vector.x > 0.5f) ? 0 : 1);
			vector.y = 0.3f;
		}
		else if (num < 0.5f && (vector.x == 0f || vector.x == 1f))
		{
			vector.y = Mathf.Lerp(0.3f, vector.y, (num - 0.3f) / 0.2f);
		}
		Vector2 sizeDelta = Tooltip.instance.canvasRect.sizeDelta;
		Vector2 v = new Vector2(vector.x * sizeDelta.x - sizeDelta.x * 0.5f, vector.y * sizeDelta.y - sizeDelta.y * 0.5f);
		return Tooltip.instance.canvasRect.TransformPoint(v);
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x000660C2 File Offset: 0x000642C2
	public static void Release()
	{
		if (Tooltip.instance == null)
		{
			return;
		}
		Tooltip.instance.isVisible = false;
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.myInstance.Display.ReleaseRange();
		}
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x000660F9 File Offset: 0x000642F9
	private void Update()
	{
		this.cgroup.UpdateOpacity(this.isVisible, 4f, true);
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x00066114 File Offset: 0x00064314
	private void SetLocation(Vector3 pos, TextAnchor anchor)
	{
		float num = 1f;
		if (Settings.LowRez)
		{
			num = 1.25f;
		}
		Tooltip.instance.transform.localScale = new Vector3(num, num, num);
		this.SetAnchor(anchor);
		base.transform.position = new Vector3(pos.x, pos.y, base.transform.position.z);
		this.isVisible = true;
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x00066188 File Offset: 0x00064388
	private void SetAnchor(TextAnchor a)
	{
		switch (a)
		{
		case TextAnchor.UpperLeft:
			this.rect.pivot = new Vector2(0f, 1f);
			this.KeywordRect.anchorMin = new Vector2(0f, 0f);
			this.KeywordRect.anchorMax = new Vector2(1f, 0f);
			break;
		case TextAnchor.UpperCenter:
			this.rect.pivot = new Vector2(0.5f, 1f);
			this.KeywordRect.anchorMin = new Vector2(0f, 0f);
			this.KeywordRect.anchorMax = new Vector2(1f, 0f);
			break;
		case TextAnchor.UpperRight:
			this.rect.pivot = new Vector2(1f, 1f);
			this.KeywordRect.anchorMin = new Vector2(0f, 0f);
			this.KeywordRect.anchorMax = new Vector2(1f, 0f);
			break;
		case TextAnchor.MiddleLeft:
			this.rect.pivot = new Vector2(0f, 0.5f);
			this.KeywordRect.anchorMin = new Vector2(1f, 0f);
			this.KeywordRect.anchorMax = new Vector2(1f, 1f);
			break;
		case TextAnchor.MiddleRight:
			this.rect.pivot = new Vector2(1f, 0.5f);
			this.KeywordRect.anchorMin = new Vector2(0f, 0f);
			this.KeywordRect.anchorMax = new Vector2(0f, 1f);
			break;
		case TextAnchor.LowerLeft:
			this.rect.pivot = new Vector2(0f, 0f);
			this.KeywordRect.anchorMin = new Vector2(0f, 1f);
			this.KeywordRect.anchorMax = new Vector2(1f, 1f);
			break;
		case TextAnchor.LowerCenter:
			this.rect.pivot = new Vector2(0.5f, 0f);
			this.KeywordRect.anchorMin = new Vector2(0f, 1f);
			this.KeywordRect.anchorMax = new Vector2(1f, 1f);
			break;
		case TextAnchor.LowerRight:
			this.rect.pivot = new Vector2(1f, 0f);
			this.KeywordRect.anchorMin = new Vector2(0f, 1f);
			this.KeywordRect.anchorMax = new Vector2(1f, 1f);
			break;
		}
		this.KeywordRect.pivot = this.rect.pivot;
		this.KeywordLayout.childAlignment = a;
		this.KeywordRect.anchoredPosition = Vector2.zero;
		this.KeywordRect.offsetMin = Vector2.zero;
		this.KeywordRect.offsetMax = Vector2.zero;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000664A8 File Offset: 0x000646A8
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywordObjs)
		{
			if (keywordBoxUI != null)
			{
				UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
			}
		}
		this.keywordObjs.Clear();
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x00066514 File Offset: 0x00064714
	private void ShowKeywords(AugmentRootNode mod, EntityControl owner)
	{
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(mod.Detail, owner))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordRect, ref this.keywordObjs, owner);
		}
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x00066580 File Offset: 0x00064780
	private void ShowKeywords(AbilityTree ability, EntityControl owner)
	{
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(ability.Root.Usage.AbilityMetadata.Detail, owner))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordRect, ref this.keywordObjs, owner);
		}
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000665F8 File Offset: 0x000647F8
	private void SetTooltipType(Tooltip.TooltipType ttype, string inputText = "")
	{
		this.ClearKeywords();
		this.AugmentGroup.SetActive(ttype == Tooltip.TooltipType.Augment);
		this.AbilityGroup.SetActive(ttype == Tooltip.TooltipType.Ability);
		this.InfoGroup.SetActive(ttype == Tooltip.TooltipType.Info);
		this.TomeGroup.SetActive(ttype == Tooltip.TooltipType.Tome);
		this.CosmeticGroup.SetActive(ttype == Tooltip.TooltipType.Cosmetic);
		this.StatusGroup.SetActive(ttype == Tooltip.TooltipType.Status);
		if (ttype == Tooltip.TooltipType.Info)
		{
			return;
		}
		Vector2 vector;
		switch (ttype)
		{
		case Tooltip.TooltipType.Augment:
			vector = this.AugmentSize;
			break;
		case Tooltip.TooltipType.Ability:
			vector = this.AbilitySize;
			break;
		case Tooltip.TooltipType.Tome:
			vector = this.AugmentSize;
			break;
		case Tooltip.TooltipType.Cosmetic:
			vector = this.CosmeticSize;
			break;
		case Tooltip.TooltipType.Status:
			vector = this.StatusSize;
			break;
		default:
			vector = new Vector3(100f, 100f);
			break;
		}
		Vector2 sizeDelta = vector;
		if (!string.IsNullOrEmpty(inputText))
		{
			int num = inputText.Length;
			num += inputText.CountNewLines() * 40;
			if (num > 200)
			{
				sizeDelta.y += (float)(num - 200) / 2f;
			}
		}
		this.rect.sizeDelta = sizeDelta;
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x00066713 File Offset: 0x00064913
	public Tooltip()
	{
	}

	// Token: 0x04000E51 RID: 3665
	private static Tooltip instance;

	// Token: 0x04000E52 RID: 3666
	public RectTransform KeywordRect;

	// Token: 0x04000E53 RID: 3667
	public Vector2 AugmentSize;

	// Token: 0x04000E54 RID: 3668
	public GameObject AugmentGroup;

	// Token: 0x04000E55 RID: 3669
	public Image Aug_Border_Player;

	// Token: 0x04000E56 RID: 3670
	public GameObject Aug_Border_Enemy;

	// Token: 0x04000E57 RID: 3671
	public GameObject Aug_Border_Binding;

	// Token: 0x04000E58 RID: 3672
	public TextMeshProUGUI AugmentTitle;

	// Token: 0x04000E59 RID: 3673
	public TextMeshProUGUI AugmentDetail;

	// Token: 0x04000E5A RID: 3674
	public GameObject HeatGroup;

	// Token: 0x04000E5B RID: 3675
	public TextMeshProUGUI HeatLevel;

	// Token: 0x04000E5C RID: 3676
	public Image AugmentIcon;

	// Token: 0x04000E5D RID: 3677
	public Vector2 AbilitySize;

	// Token: 0x04000E5E RID: 3678
	public GameObject AbilityGroup;

	// Token: 0x04000E5F RID: 3679
	public TextMeshProUGUI AbilityTitle;

	// Token: 0x04000E60 RID: 3680
	public TextMeshProUGUI AbilityDetail;

	// Token: 0x04000E61 RID: 3681
	public Image AbilityIcon;

	// Token: 0x04000E62 RID: 3682
	public GameObject ManaGroup;

	// Token: 0x04000E63 RID: 3683
	public TextMeshProUGUI ManaCost;

	// Token: 0x04000E64 RID: 3684
	public GameObject StatusGroup;

	// Token: 0x04000E65 RID: 3685
	public Vector2 StatusSize = new Vector2(460f, 138f);

	// Token: 0x04000E66 RID: 3686
	public TextMeshProUGUI StatusTitle;

	// Token: 0x04000E67 RID: 3687
	public TextMeshProUGUI StatusDetail;

	// Token: 0x04000E68 RID: 3688
	public Image StatusIcon;

	// Token: 0x04000E69 RID: 3689
	public GameObject StatusBorderPositive;

	// Token: 0x04000E6A RID: 3690
	public GameObject StatusBorderNegative;

	// Token: 0x04000E6B RID: 3691
	public GameObject InfoGroup;

	// Token: 0x04000E6C RID: 3692
	public TextMeshProUGUI InfoTitle;

	// Token: 0x04000E6D RID: 3693
	public TextMeshProUGUI InfoDetail;

	// Token: 0x04000E6E RID: 3694
	public GameObject TomeGroup;

	// Token: 0x04000E6F RID: 3695
	public TextMeshProUGUI TomeTitle;

	// Token: 0x04000E70 RID: 3696
	public TextMeshProUGUI TomeDetail;

	// Token: 0x04000E71 RID: 3697
	public Image TomeIcon;

	// Token: 0x04000E72 RID: 3698
	public GameObject CosmeticGroup;

	// Token: 0x04000E73 RID: 3699
	public Vector2 CosmeticSize = new Vector2(460f, 138f);

	// Token: 0x04000E74 RID: 3700
	public TextMeshProUGUI CosmeticTitle;

	// Token: 0x04000E75 RID: 3701
	public TextMeshProUGUI CosmeticDetail;

	// Token: 0x04000E76 RID: 3702
	private VerticalLayoutGroup KeywordLayout;

	// Token: 0x04000E77 RID: 3703
	private RectTransform rect;

	// Token: 0x04000E78 RID: 3704
	private CanvasGroup cgroup;

	// Token: 0x04000E79 RID: 3705
	private RectTransform canvasRect;

	// Token: 0x04000E7A RID: 3706
	private bool isVisible;

	// Token: 0x04000E7B RID: 3707
	private List<KeywordBoxUI> keywordObjs = new List<KeywordBoxUI>();

	// Token: 0x0200055B RID: 1371
	private enum TooltipType
	{
		// Token: 0x040026D7 RID: 9943
		Info,
		// Token: 0x040026D8 RID: 9944
		Augment,
		// Token: 0x040026D9 RID: 9945
		Ability,
		// Token: 0x040026DA RID: 9946
		Tome,
		// Token: 0x040026DB RID: 9947
		Cosmetic,
		// Token: 0x040026DC RID: 9948
		Status
	}

	// Token: 0x0200055C RID: 1372
	public class SimpleInfoData
	{
		// Token: 0x0600249A RID: 9370 RVA: 0x000CE8B5 File Offset: 0x000CCAB5
		public SimpleInfoData()
		{
		}

		// Token: 0x040026DD RID: 9949
		public string Title = "";

		// Token: 0x040026DE RID: 9950
		public string Detail = "";

		// Token: 0x040026DF RID: 9951
		public Vector2 Size = new Vector2(400f, 300f);
	}
}
