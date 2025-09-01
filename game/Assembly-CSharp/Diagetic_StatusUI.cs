using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018F RID: 399
public class Diagetic_StatusUI : MonoBehaviour
{
	// Token: 0x060010C6 RID: 4294 RVA: 0x00068708 File Offset: 0x00066908
	public void Setup(StatusTree status, int stacks = 1)
	{
		StatusRootNode root = status.Root;
		this.Icon.sprite = root.EffectIcon;
		this.Title.text = root.EffectName;
		this.Detail.text = TextParser.AugmentDetail(root.Description, null, PlayerControl.myInstance, false);
		this.PositiveRing.SetActive(!root.IsNegative);
		this.NegativeRing.SetActive(root.IsNegative);
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(root.Description, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordHolder, ref this.keywordObjs, PlayerControl.myInstance);
		}
		this.StackDisplay.SetActive(status.Root.CanStack);
		this.StackText.text = stacks.ToString() + "<size=22> x</size>";
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00068824 File Offset: 0x00066A24
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywordObjs)
		{
			UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
		}
		this.keywordObjs.Clear();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00068884 File Offset: 0x00066A84
	public Diagetic_StatusUI()
	{
	}

	// Token: 0x04000F0F RID: 3855
	public Image Icon;

	// Token: 0x04000F10 RID: 3856
	public TextMeshProUGUI Title;

	// Token: 0x04000F11 RID: 3857
	public TextMeshProUGUI Detail;

	// Token: 0x04000F12 RID: 3858
	public RectTransform KeywordHolder;

	// Token: 0x04000F13 RID: 3859
	private List<KeywordBoxUI> keywordObjs = new List<KeywordBoxUI>();

	// Token: 0x04000F14 RID: 3860
	public GameObject PositiveRing;

	// Token: 0x04000F15 RID: 3861
	public GameObject NegativeRing;

	// Token: 0x04000F16 RID: 3862
	public GameObject StackDisplay;

	// Token: 0x04000F17 RID: 3863
	public TextMeshProUGUI StackText;
}
