using System;
using TMPro;
using UnityEngine;

// Token: 0x02000208 RID: 520
public class FPSCounter : MonoBehaviour
{
	// Token: 0x06001623 RID: 5667 RVA: 0x0008C020 File Offset: 0x0008A220
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x0008C030 File Offset: 0x0008A230
	private void Update()
	{
		this.canvasGroup.UpdateOpacity(FPSCounter.ShowFPS, 2f, false);
		if (this.canvasGroup.alpha <= 0f)
		{
			return;
		}
		float smoothDeltaTime = Time.smoothDeltaTime;
		this.timer = ((this.timer <= 0f) ? this.refresh : (this.timer -= smoothDeltaTime));
		if (this.timer <= 0f)
		{
			this.avgFramerate = (float)((int)(1f / smoothDeltaTime));
		}
		this.m_Text.text = string.Format(this.display, this.avgFramerate.ToString());
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x0008C0D5 File Offset: 0x0008A2D5
	public FPSCounter()
	{
	}

	// Token: 0x040015C7 RID: 5575
	private CanvasGroup canvasGroup;

	// Token: 0x040015C8 RID: 5576
	public float timer;

	// Token: 0x040015C9 RID: 5577
	public float refresh;

	// Token: 0x040015CA RID: 5578
	public float avgFramerate;

	// Token: 0x040015CB RID: 5579
	private string display = "{0} FPS";

	// Token: 0x040015CC RID: 5580
	public TextMeshProUGUI m_Text;

	// Token: 0x040015CD RID: 5581
	public static bool ShowFPS;
}
