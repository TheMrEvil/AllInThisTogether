using System;
using TMPro;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class Library_Dice_Reward : ActionPrefab
{
	// Token: 0x06000D57 RID: 3415 RVA: 0x00055486 File Offset: 0x00053686
	public override void Setup(EffectProperties properties)
	{
		this.Value = (int)properties.GetExtra(EProp.DynamicInput, 0f);
		this.ValueText.text = this.Value.ToString();
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x000554B4 File Offset: 0x000536B4
	public void Collect()
	{
		this.ValueText.gameObject.SetActive(false);
		this.LoopFX.Stop();
		this.CollectVFX.Play();
		AudioManager.PlayClipAtPoint(this.CollectSFX, this.LoopFX.transform.position, 1f, 1f, 1f, 10f, 250f);
		UnityEngine.Object.Destroy(base.gameObject, 1.25f);
		Currency.Add(this.Value, true);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00055539 File Offset: 0x00053739
	public Library_Dice_Reward()
	{
	}

	// Token: 0x04000AE9 RID: 2793
	public TextMeshProUGUI ValueText;

	// Token: 0x04000AEA RID: 2794
	private int Value;

	// Token: 0x04000AEB RID: 2795
	public ParticleSystem LoopFX;

	// Token: 0x04000AEC RID: 2796
	public ParticleSystem CollectVFX;

	// Token: 0x04000AED RID: 2797
	public AudioClip CollectSFX;
}
