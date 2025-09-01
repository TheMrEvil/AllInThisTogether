using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A6 RID: 422
public class EndGameAnim : MonoBehaviour
{
	// Token: 0x0600118D RID: 4493 RVA: 0x0006C9C0 File Offset: 0x0006ABC0
	private void Awake()
	{
		EndGameAnim.instance = this;
		this.fader = base.gameObject.GetComponent<CanvasGroup>();
		this.fader.alpha = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0006C9F5 File Offset: 0x0006ABF5
	public static void Display(bool didWin)
	{
		if (EndGameAnim.instance == null)
		{
			return;
		}
		EndGameAnim.instance.gameObject.SetActive(true);
		EndGameAnim.instance.Setup(didWin);
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0006CA20 File Offset: 0x0006AC20
	private void Setup(bool didWin)
	{
		this.fader.alpha = 1f;
		this.Label.text = (didWin ? "Victory" : "Defeat");
		this.Anim.Play(didWin ? "Victory" : "Defeat");
		AudioManager.PlayLoudSFX2D(didWin ? this.WinSFX : this.LoseSFX, 1f, 0.1f);
		base.Invoke("Disable", 4.5f);
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0006CAA1 File Offset: 0x0006ACA1
	private void Disable()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x0006CAAF File Offset: 0x0006ACAF
	public EndGameAnim()
	{
	}

	// Token: 0x04001028 RID: 4136
	private static EndGameAnim instance;

	// Token: 0x04001029 RID: 4137
	private CanvasGroup fader;

	// Token: 0x0400102A RID: 4138
	public TextMeshProUGUI Label;

	// Token: 0x0400102B RID: 4139
	public Animator Anim;

	// Token: 0x0400102C RID: 4140
	public Image TomeIcon;

	// Token: 0x0400102D RID: 4141
	public AudioClip WinSFX;

	// Token: 0x0400102E RID: 4142
	public AudioClip LoseSFX;
}
