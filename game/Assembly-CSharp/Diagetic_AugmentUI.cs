using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018D RID: 397
public class Diagetic_AugmentUI : MonoBehaviour
{
	// Token: 0x060010C1 RID: 4289 RVA: 0x0006856C File Offset: 0x0006676C
	public void Setup(AugmentTree augment)
	{
		AugmentRootNode root = augment.Root;
		this.Icon.sprite = root.Icon;
		this.Title.text = root.Name;
		this.Title.color = GameDB.Quality(root.DisplayQuality).PlayerColor;
		this.Detail.text = TextParser.AugmentDetail(root.Detail, augment, PlayerControl.myInstance, false);
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(root.Detail, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywordObjs, PlayerControl.myInstance);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0006864C File Offset: 0x0006684C
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywordObjs)
		{
			UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
		}
		this.keywordObjs.Clear();
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000686AC File Offset: 0x000668AC
	public Diagetic_AugmentUI()
	{
	}

	// Token: 0x04000F09 RID: 3849
	public Image Icon;

	// Token: 0x04000F0A RID: 3850
	public TextMeshProUGUI Title;

	// Token: 0x04000F0B RID: 3851
	public TextMeshProUGUI Detail;

	// Token: 0x04000F0C RID: 3852
	public RectTransform KeywordHolder;

	// Token: 0x04000F0D RID: 3853
	private List<KeywordBoxUI> keywordObjs = new List<KeywordBoxUI>();
}
