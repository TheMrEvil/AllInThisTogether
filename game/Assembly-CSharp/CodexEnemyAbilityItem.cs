using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000142 RID: 322
public class CodexEnemyAbilityItem : MonoBehaviour
{
	// Token: 0x06000EAB RID: 3755 RVA: 0x0005CE60 File Offset: 0x0005B060
	public void Setup(AIData.EnemyCodexAbility ability, bool isRaid)
	{
		this.Name.text = ability.AbilityName;
		this.Description.text = ability.GetDetailText(isRaid);
		foreach (CodexEnemyAbilityItem.CodexEnemyAbilityIcon codexEnemyAbilityIcon in this.Icons)
		{
			if (codexEnemyAbilityIcon.Category == ability.AbilityType)
			{
				this.Icon.sprite = codexEnemyAbilityIcon.Icon;
				break;
			}
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0005CEF0 File Offset: 0x0005B0F0
	public CodexEnemyAbilityItem()
	{
	}

	// Token: 0x04000C3B RID: 3131
	public TextMeshProUGUI Name;

	// Token: 0x04000C3C RID: 3132
	public Image Icon;

	// Token: 0x04000C3D RID: 3133
	public TextMeshProUGUI Description;

	// Token: 0x04000C3E RID: 3134
	public List<CodexEnemyAbilityItem.CodexEnemyAbilityIcon> Icons;

	// Token: 0x0200053D RID: 1341
	[Serializable]
	public class CodexEnemyAbilityIcon
	{
		// Token: 0x06002426 RID: 9254 RVA: 0x000CD180 File Offset: 0x000CB380
		public CodexEnemyAbilityIcon()
		{
		}

		// Token: 0x0400266D RID: 9837
		public AIData.EnemyCodexAbility.Category Category;

		// Token: 0x0400266E RID: 9838
		public Sprite Icon;
	}
}
