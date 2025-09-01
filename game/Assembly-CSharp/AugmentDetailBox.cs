using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class AugmentDetailBox : MonoBehaviour
{
	// Token: 0x06000E48 RID: 3656 RVA: 0x0005A71A File Offset: 0x0005891A
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0005A728 File Offset: 0x00058928
	public void SetupAugment(AugmentTree augment, int stackCount, float ypos, EntityControl owner, bool parseConditionals = false)
	{
		AugmentRootNode root = augment.Root;
		this.Icon.sprite = root.Icon;
		this.Title.text = root.Name + ((stackCount > 1) ? (" x" + stackCount.ToString()) : "");
		this.Title.color = ((root.modType == ModType.Enemy) ? GameDB.Quality(root.DisplayQuality).EnemyColor : (this.UseDarkColors ? GameDB.Quality(root.DisplayQuality).DarkTextColor : GameDB.Quality(root.DisplayQuality).PlayerColor));
		this.Descr.text = TextParser.AugmentDetail(root.Detail, augment, owner, parseConditionals);
		base.transform.position = new Vector3(base.transform.position.x, ypos, base.transform.position.z);
		if (this.RarityBorder != null && this.RaritySprites.Count > 0)
		{
			this.RarityBorder.sprite = this.RaritySprites[Mathf.Clamp((int)augment.Root.DisplayQuality, 0, this.RaritySprites.Count - 1)];
		}
		this.modRef = root;
		if (this.KeywordHolder == null)
		{
			return;
		}
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(root.Detail, owner))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywordObjs, owner);
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0005A8E0 File Offset: 0x00058AE0
	public void SetupAugment(AugmentTree augment, Vector3 pos)
	{
		AugmentRootNode root = augment.Root;
		this.Icon.sprite = root.Icon;
		this.Title.text = root.Name;
		this.Descr.text = TextParser.AugmentDetail(root.Detail, augment, null, false);
		this.Title.color = ((root.modType == ModType.Enemy) ? GameDB.Quality(root.DisplayQuality).EnemyColor : (this.UseDarkColors ? GameDB.Quality(root.DisplayQuality).DarkTextColor : GameDB.Quality(root.DisplayQuality).PlayerColor));
		base.transform.position = pos;
		if (this.RarityBorder != null && this.RaritySprites.Count > 0)
		{
			this.RarityBorder.sprite = this.RaritySprites[Mathf.Clamp((int)augment.Root.Rarity, 0, this.RaritySprites.Count - 1)];
		}
		this.modRef = root;
		if (this.KeywordHolder == null)
		{
			return;
		}
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.modRef.Detail, null))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywordObjs, null);
		}
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0005AA4C File Offset: 0x00058C4C
	public void ShowKeywords()
	{
		if (this.KeywordHolder == null || this.modRef == null)
		{
			return;
		}
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.modRef.Detail, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywordObjs, PlayerControl.myInstance);
		}
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x0005AAE0 File Offset: 0x00058CE0
	public void ClearKeywords()
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

	// Token: 0x06000E4D RID: 3661 RVA: 0x0005AB4C File Offset: 0x00058D4C
	public void UpdateOpacity(bool shouldShow, float overrideVal = -1f)
	{
		if (overrideVal != -1f)
		{
			this.canvasGroup.alpha = overrideVal;
			return;
		}
		this.canvasGroup.UpdateOpacity(shouldShow, 5f, true);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x0005AB75 File Offset: 0x00058D75
	public AugmentDetailBox()
	{
	}

	// Token: 0x04000BB3 RID: 2995
	private CanvasGroup canvasGroup;

	// Token: 0x04000BB4 RID: 2996
	public TextMeshProUGUI Title;

	// Token: 0x04000BB5 RID: 2997
	public TextMeshProUGUI Descr;

	// Token: 0x04000BB6 RID: 2998
	public Image Icon;

	// Token: 0x04000BB7 RID: 2999
	public bool UseDarkColors;

	// Token: 0x04000BB8 RID: 3000
	public RectTransform KeywordHolder;

	// Token: 0x04000BB9 RID: 3001
	private List<KeywordBoxUI> keywordObjs = new List<KeywordBoxUI>();

	// Token: 0x04000BBA RID: 3002
	public Image RarityBorder;

	// Token: 0x04000BBB RID: 3003
	public List<Sprite> RaritySprites;

	// Token: 0x04000BBC RID: 3004
	private AugmentRootNode modRef;
}
