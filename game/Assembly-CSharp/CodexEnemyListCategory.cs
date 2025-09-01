using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000143 RID: 323
public class CodexEnemyListCategory : MonoBehaviour
{
	// Token: 0x06000EAD RID: 3757 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
	public void Setup(EnemyType eType, string nameOverride = "")
	{
		foreach (CodexEnemyListCategory.CategoryInfo categoryInfo in this.InfoOptions)
		{
			if (categoryInfo.EnemyType == eType)
			{
				this.IconDisplay.sprite = categoryInfo.Icon;
				this.Label.text = (string.IsNullOrEmpty(nameOverride) ? categoryInfo.Name : nameOverride);
				break;
			}
		}
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0005CF7C File Offset: 0x0005B17C
	public CodexEnemyListCategory()
	{
	}

	// Token: 0x04000C3F RID: 3135
	public List<CodexEnemyListCategory.CategoryInfo> InfoOptions;

	// Token: 0x04000C40 RID: 3136
	public TextMeshProUGUI Label;

	// Token: 0x04000C41 RID: 3137
	public Image IconDisplay;

	// Token: 0x0200053E RID: 1342
	[Serializable]
	public class CategoryInfo
	{
		// Token: 0x06002427 RID: 9255 RVA: 0x000CD188 File Offset: 0x000CB388
		public CategoryInfo()
		{
		}

		// Token: 0x0400266F RID: 9839
		public EnemyType EnemyType;

		// Token: 0x04002670 RID: 9840
		public Sprite Icon;

		// Token: 0x04002671 RID: 9841
		public string Name;
	}
}
