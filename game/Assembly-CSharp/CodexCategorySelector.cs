using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000141 RID: 321
public class CodexCategorySelector : MonoBehaviour
{
	// Token: 0x06000EA6 RID: 3750 RVA: 0x0005CD36 File Offset: 0x0005AF36
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0005CD54 File Offset: 0x0005AF54
	public void UpdateDisplay()
	{
		ValueTuple<int, int> counts = CodexPanel.GetCounts(this.Category);
		int item = counts.Item1;
		int item2 = counts.Item2;
		this.ProgressText.text = item.ToString() + "/" + item2.ToString();
		this.ProgressFill.fillAmount = (float)item / Mathf.Max(1f, (float)item2);
		if (this.AugmentDetails != null)
		{
			this.SetupRandomDetails();
		}
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0005CDCC File Offset: 0x0005AFCC
	private void SetupRandomDetails()
	{
		if (this.AugmentDetails == null)
		{
			return;
		}
		ModType augmentCategory = CodexPanel.GetAugmentCategory(this.Category);
		List<AugmentTree> list = CodexPanel.GetAllCodexAugments(augmentCategory);
		list = CodexPanel.RemoveUnseen(list, augmentCategory);
		if (list.Count == 0)
		{
			return;
		}
		AugmentTree augmentTree = list[UnityEngine.Random.Range(0, list.Count)];
		if (augmentTree != null)
		{
			this.AugmentDetails.SetupAugment(augmentTree, this.AugmentDetails.transform.position);
		}
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0005CE44 File Offset: 0x0005B044
	private void Click()
	{
		CodexPanel.instance.GoToCategory(this.Category);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0005CE56 File Offset: 0x0005B056
	public CodexCategorySelector()
	{
	}

	// Token: 0x04000C36 RID: 3126
	public CodexPanel.CodexCategory Category;

	// Token: 0x04000C37 RID: 3127
	public GameObject NewDisplay;

	// Token: 0x04000C38 RID: 3128
	public TextMeshProUGUI ProgressText;

	// Token: 0x04000C39 RID: 3129
	public Image ProgressFill;

	// Token: 0x04000C3A RID: 3130
	public AugmentDetailBox AugmentDetails;
}
