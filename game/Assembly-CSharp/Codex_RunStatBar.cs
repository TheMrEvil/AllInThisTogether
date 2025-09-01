using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014A RID: 330
public class Codex_RunStatBar : MonoBehaviour
{
	// Token: 0x06000EDB RID: 3803 RVA: 0x0005EA68 File Offset: 0x0005CC68
	public void Setup(string label, float proportion, bool isSelf)
	{
		this.ValueText.text = label;
		this.SelfDisplay.gameObject.SetActive(isSelf);
		this.SelfBar.gameObject.SetActive(isSelf);
		this.FillBar.fillAmount = proportion;
		this.SelfBar.fillAmount = proportion;
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0005EABB File Offset: 0x0005CCBB
	public Codex_RunStatBar()
	{
	}

	// Token: 0x04000C87 RID: 3207
	public TextMeshProUGUI ValueText;

	// Token: 0x04000C88 RID: 3208
	public Image FillBar;

	// Token: 0x04000C89 RID: 3209
	public Image SelfBar;

	// Token: 0x04000C8A RID: 3210
	public GameObject SelfDisplay;
}
