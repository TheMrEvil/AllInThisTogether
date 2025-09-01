using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017F RID: 383
public class SettingsTab : MonoBehaviour
{
	// Token: 0x0600102C RID: 4140 RVA: 0x0006554C File Offset: 0x0006374C
	public void Setup(SettingsPanel.SettingTab tabInfo)
	{
		foreach (TextMeshProUGUI textMeshProUGUI in this.Titles)
		{
			textMeshProUGUI.text = tabInfo.Title;
		}
		this.tab = tabInfo.Setting;
		this.button.onClick.AddListener(delegate()
		{
			SettingsPanel.instance.SetTab(tabInfo.Setting, false);
		});
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x000655E4 File Offset: 0x000637E4
	public void UpdateSelected(SettingsPanel.TabType t)
	{
		this.SelectedDisplay.SetActive(t == this.tab);
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000655FA File Offset: 0x000637FA
	public SettingsTab()
	{
	}

	// Token: 0x04000E43 RID: 3651
	public GameObject SelectedDisplay;

	// Token: 0x04000E44 RID: 3652
	public List<TextMeshProUGUI> Titles;

	// Token: 0x04000E45 RID: 3653
	public Button button;

	// Token: 0x04000E46 RID: 3654
	private SettingsPanel.TabType tab;

	// Token: 0x0200055A RID: 1370
	[CompilerGenerated]
	private sealed class <>c__DisplayClass4_0
	{
		// Token: 0x06002498 RID: 9368 RVA: 0x000CE895 File Offset: 0x000CCA95
		public <>c__DisplayClass4_0()
		{
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000CE89D File Offset: 0x000CCA9D
		internal void <Setup>b__0()
		{
			SettingsPanel.instance.SetTab(this.tabInfo.Setting, false);
		}

		// Token: 0x040026D5 RID: 9941
		public SettingsPanel.SettingTab tabInfo;
	}
}
