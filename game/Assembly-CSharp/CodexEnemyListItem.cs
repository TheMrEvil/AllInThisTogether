using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000144 RID: 324
public class CodexEnemyListItem : MonoBehaviour
{
	// Token: 0x06000EAF RID: 3759 RVA: 0x0005CF84 File Offset: 0x0005B184
	public void Setup(AIData.EnemyCodexEntry entry, bool hasSeen)
	{
		this.Enemy = entry;
		this.HasSeen = (hasSeen || entry.AlwaysVisible);
		this.Name.text = (this.HasSeen ? entry.Name : "???");
		this.Name.color = (this.HasSeen ? Color.black : new Color(0f, 0f, 0f, 0.5f));
		foreach (Image image in this.Icons)
		{
			image.sprite = entry.Portrait;
		}
		this.SeenDisplay.SetActive(this.HasSeen);
		this.UnseenDisplay.SetActive(!this.HasSeen);
		if (this.HasSeen)
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0005D090 File Offset: 0x0005B290
	private void OnClick()
	{
		CodexEnemyPanel.instance.SelectEnemy(this);
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x0005D09D File Offset: 0x0005B29D
	public CodexEnemyListItem()
	{
	}

	// Token: 0x04000C42 RID: 3138
	public TextMeshProUGUI Name;

	// Token: 0x04000C43 RID: 3139
	public List<Image> Icons;

	// Token: 0x04000C44 RID: 3140
	public GameObject SelectedDisplay;

	// Token: 0x04000C45 RID: 3141
	public GameObject SeenDisplay;

	// Token: 0x04000C46 RID: 3142
	public GameObject UnseenDisplay;

	// Token: 0x04000C47 RID: 3143
	[NonSerialized]
	public AIData.EnemyCodexEntry Enemy;

	// Token: 0x04000C48 RID: 3144
	[NonSerialized]
	public bool HasSeen;
}
