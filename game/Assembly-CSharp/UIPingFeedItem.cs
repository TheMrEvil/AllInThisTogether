using System;
using TMPro;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class UIPingFeedItem : MonoBehaviour
{
	// Token: 0x06001061 RID: 4193 RVA: 0x00066AD1 File Offset: 0x00064CD1
	public void Setup(PlayerControl plr)
	{
		this.plrRef = plr;
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x00066ADC File Offset: 0x00064CDC
	public void TickUpdate()
	{
		if (this.showTime > 0f)
		{
			this.showTime -= Time.deltaTime;
		}
		bool flag = this.showTime > 0f;
		this.Fader.UpdateOpacity(flag, flag ? 8f : 2f, true);
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x00066B32 File Offset: 0x00064D32
	public void ShowDisplay(string textVal, float timeOverride = 2f)
	{
		this.Fader.HideImmediate();
		this.showTime = timeOverride;
		this.Label.text = textVal;
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x00066B52 File Offset: 0x00064D52
	public UIPingFeedItem()
	{
	}

	// Token: 0x04000E89 RID: 3721
	[NonSerialized]
	public PlayerControl plrRef;

	// Token: 0x04000E8A RID: 3722
	public TextMeshProUGUI Label;

	// Token: 0x04000E8B RID: 3723
	public CanvasGroup Fader;

	// Token: 0x04000E8C RID: 3724
	private float showTime;
}
