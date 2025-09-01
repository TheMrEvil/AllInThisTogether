using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000146 RID: 326
public class CodexGridView : MonoBehaviour
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0005D562 File Offset: 0x0005B762
	private bool WantVisible
	{
		get
		{
			return CodexPanel.WantGridView();
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0005D569 File Offset: 0x0005B769
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.GridItemRef.gameObject.SetActive(false);
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0005D588 File Offset: 0x0005B788
	public void Setup(CodexPanel.CodexCategory category)
	{
		this.Category = category;
		this.TitleText.text = CodexPanel.GetTitle(this.Category);
		ValueTuple<int, int> counts = CodexPanel.GetCounts(this.Category);
		int item = counts.Item1;
		int item2 = counts.Item2;
		this.ProgressText.text = item.ToString() + "/" + item2.ToString();
		this.ProgressFill.fillAmount = (float)item / Mathf.Max(1f, (float)item2);
		this.ClearGrid();
		base.StartCoroutine(this.SetupGridView());
		if (this.gridItems.Count > 0)
		{
			UISelector.SelectSelectable(this.gridItems[0].GetComponent<Selectable>());
		}
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0005D640 File Offset: 0x0005B840
	public void FilterObjects(string searchTerm)
	{
		List<string> list = searchTerm.ToLower().Split(' ', StringSplitOptions.None).ToList<string>();
		bool active = true;
		bool flag = list.Count > 0 && !string.IsNullOrEmpty(list[0]);
		foreach (CodexGridItem codexGridItem in this.gridItems)
		{
			if (flag)
			{
				active = codexGridItem.MatchesSearch(list);
			}
			codexGridItem.gameObject.SetActive(active);
		}
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
	public void TickUpdate()
	{
		this.canvasGroup.UpdateOpacity(this.WantVisible, 4f, false);
		this.AutoScroll.TickUpdate();
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0005D6FC File Offset: 0x0005B8FC
	private IEnumerator SetupGridView()
	{
		float num = 367f;
		int num2 = Mathf.FloorToInt(this.GridHolderRect.rect.width) - this.GridLayout.padding.left - this.GridLayout.padding.right;
		UnityEngine.Debug.Log("Usable Width: " + num2.ToString());
		int num3 = Mathf.FloorToInt((float)num2 / num);
		float num4 = (float)num2 - (float)num3 * num - (float)(num3 - 1) * this.GridLayout.spacing.x;
		float num5 = num + num4 / (float)num3;
		this.GridLayout.cellSize = new Vector2(num5, this.GridLayout.cellSize.y);
		int perRow = (int)((float)num2 / num5);
		ModType augmentCategory = CodexPanel.GetAugmentCategory(this.Category);
		List<AugmentTree> allCodexAugments = CodexPanel.GetAllCodexAugments(augmentCategory);
		List<AugmentTree> seenAugments = CodexPanel.RemoveUnseen(allCodexAugments, augmentCategory);
		allCodexAugments.Sort((AugmentTree x, AugmentTree y) => x.Root.Name.CompareTo(y.Root.Name));
		int num6 = 0;
		bool didFirstSelect = false;
		foreach (AugmentTree item in allCodexAugments)
		{
			CodexGridItem codexGridItem = null;
			if (seenAugments.Contains(item))
			{
				codexGridItem = this.AddGridItem(item);
			}
			else
			{
				this.AddGridItem(null);
			}
			if (codexGridItem != null && !didFirstSelect)
			{
				codexGridItem.OnClick();
				didFirstSelect = true;
			}
			num6++;
			if (num6 > 10)
			{
				yield return true;
				num6 = 0;
			}
		}
		List<AugmentTree>.Enumerator enumerator = default(List<AugmentTree>.Enumerator);
		UISelector.SetupGridListNav<CodexGridItem>(this.gridItems, perRow, null, null, true);
		yield break;
		yield break;
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x0005D70C File Offset: 0x0005B90C
	private CodexGridItem AddGridItem(AugmentTree item)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GridItemRef, this.GridItemRef.transform.parent);
		gameObject.SetActive(true);
		CodexGridItem component = gameObject.GetComponent<CodexGridItem>();
		component.Setup(item);
		this.gridItems.Add(component);
		return component;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0005D755 File Offset: 0x0005B955
	public void SelectItem(AugmentTree augment)
	{
		if (augment == null)
		{
			return;
		}
		this.UpdateDisplay(augment);
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0005D768 File Offset: 0x0005B968
	private void ClearGrid()
	{
		foreach (CodexGridItem codexGridItem in this.gridItems)
		{
			UnityEngine.Object.Destroy(codexGridItem.gameObject);
		}
		this.gridItems.Clear();
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0005D7C8 File Offset: 0x0005B9C8
	public void UpdateDisplay(AugmentTree augment)
	{
		this.DetailIcon.sprite = augment.Root.Icon;
		this.DetailTitle.text = augment.Root.Name;
		this.DetailInfo.text = TextParser.AugmentDetail(augment.Root.Detail, augment, null, false);
		bool flag = augment.Root.modType == ModType.Player && augment.Root.Raid;
		this.StickerHolder.gameObject.SetActive(flag);
		if (flag)
		{
			this.StickerHolder.ShowStickers(augment.ID);
		}
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0005D860 File Offset: 0x0005BA60
	public CodexGridView()
	{
	}

	// Token: 0x04000C51 RID: 3153
	public TextMeshProUGUI TitleText;

	// Token: 0x04000C52 RID: 3154
	public TextMeshProUGUI ProgressText;

	// Token: 0x04000C53 RID: 3155
	public Image ProgressFill;

	// Token: 0x04000C54 RID: 3156
	public GameObject GridItemRef;

	// Token: 0x04000C55 RID: 3157
	public GridLayoutGroup GridLayout;

	// Token: 0x04000C56 RID: 3158
	public RectTransform GridHolderRect;

	// Token: 0x04000C57 RID: 3159
	public AutoScrollRect AutoScroll;

	// Token: 0x04000C58 RID: 3160
	private List<CodexGridItem> gridItems = new List<CodexGridItem>();

	// Token: 0x04000C59 RID: 3161
	public Image DetailIcon;

	// Token: 0x04000C5A RID: 3162
	public TextMeshProUGUI DetailTitle;

	// Token: 0x04000C5B RID: 3163
	public TextMeshProUGUI DetailInfo;

	// Token: 0x04000C5C RID: 3164
	public TextMeshProUGUI DetailFlavor;

	// Token: 0x04000C5D RID: 3165
	public RaidStickers StickerHolder;

	// Token: 0x04000C5E RID: 3166
	private CanvasGroup canvasGroup;

	// Token: 0x04000C5F RID: 3167
	private CodexPanel.CodexCategory Category;

	// Token: 0x0200053F RID: 1343
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x000CD190 File Offset: 0x000CB390
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000CD19C File Offset: 0x000CB39C
		public <>c()
		{
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000CD1A4 File Offset: 0x000CB3A4
		internal int <SetupGridView>b__21_0(AugmentTree x, AugmentTree y)
		{
			return x.Root.Name.CompareTo(y.Root.Name);
		}

		// Token: 0x04002672 RID: 9842
		public static readonly CodexGridView.<>c <>9 = new CodexGridView.<>c();

		// Token: 0x04002673 RID: 9843
		public static Comparison<AugmentTree> <>9__21_0;
	}

	// Token: 0x02000540 RID: 1344
	[CompilerGenerated]
	private sealed class <SetupGridView>d__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600242B RID: 9259 RVA: 0x000CD1C1 File Offset: 0x000CB3C1
		[DebuggerHidden]
		public <SetupGridView>d__21(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000CD1D0 File Offset: 0x000CB3D0
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000CD208 File Offset: 0x000CB408
		bool IEnumerator.MoveNext()
		{
			bool result;
			try
			{
				int num = this.<>1__state;
				CodexGridView codexGridView = this;
				int num2;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					num2 = 0;
				}
				else
				{
					this.<>1__state = -1;
					float num3 = 367f;
					int num4 = Mathf.FloorToInt(codexGridView.GridHolderRect.rect.width) - codexGridView.GridLayout.padding.left - codexGridView.GridLayout.padding.right;
					UnityEngine.Debug.Log("Usable Width: " + num4.ToString());
					int num5 = Mathf.FloorToInt((float)num4 / num3);
					float num6 = (float)num4 - (float)num5 * num3 - (float)(num5 - 1) * codexGridView.GridLayout.spacing.x;
					float num7 = num3 + num6 / (float)num5;
					codexGridView.GridLayout.cellSize = new Vector2(num7, codexGridView.GridLayout.cellSize.y);
					perRow = (int)((float)num4 / num7);
					ModType augmentCategory = CodexPanel.GetAugmentCategory(codexGridView.Category);
					List<AugmentTree> allCodexAugments = CodexPanel.GetAllCodexAugments(augmentCategory);
					seenAugments = CodexPanel.RemoveUnseen(allCodexAugments, augmentCategory);
					allCodexAugments.Sort(new Comparison<AugmentTree>(CodexGridView.<>c.<>9.<SetupGridView>b__21_0));
					num2 = 0;
					didFirstSelect = false;
					enumerator = allCodexAugments.GetEnumerator();
					this.<>1__state = -3;
				}
				while (enumerator.MoveNext())
				{
					AugmentTree item = enumerator.Current;
					CodexGridItem codexGridItem = null;
					if (seenAugments.Contains(item))
					{
						codexGridItem = codexGridView.AddGridItem(item);
					}
					else
					{
						codexGridView.AddGridItem(null);
					}
					if (codexGridItem != null && !didFirstSelect)
					{
						codexGridItem.OnClick();
						didFirstSelect = true;
					}
					num2++;
					if (num2 > 10)
					{
						this.<>2__current = true;
						this.<>1__state = 1;
						return true;
					}
				}
				this.<>m__Finally1();
				enumerator = default(List<AugmentTree>.Enumerator);
				UISelector.SetupGridListNav<CodexGridItem>(codexGridView.gridItems, perRow, null, null, true);
				result = false;
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000CD44C File Offset: 0x000CB64C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator).Dispose();
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x000CD466 File Offset: 0x000CB666
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000CD46E File Offset: 0x000CB66E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x000CD475 File Offset: 0x000CB675
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002674 RID: 9844
		private int <>1__state;

		// Token: 0x04002675 RID: 9845
		private object <>2__current;

		// Token: 0x04002676 RID: 9846
		public CodexGridView <>4__this;

		// Token: 0x04002677 RID: 9847
		private int <perRow>5__2;

		// Token: 0x04002678 RID: 9848
		private List<AugmentTree> <seenAugments>5__3;

		// Token: 0x04002679 RID: 9849
		private bool <didFirstSelect>5__4;

		// Token: 0x0400267A RID: 9850
		private List<AugmentTree>.Enumerator <>7__wrap4;
	}
}
