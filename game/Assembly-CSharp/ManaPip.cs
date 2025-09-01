using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BB RID: 443
public class ManaPip : MonoBehaviour
{
	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600123B RID: 4667 RVA: 0x00070F83 File Offset: 0x0006F183
	// (set) Token: 0x0600123C RID: 4668 RVA: 0x00070F8B File Offset: 0x0006F18B
	public RectTransform rect
	{
		[CompilerGenerated]
		get
		{
			return this.<rect>k__BackingField;
		}
		[CompilerGenerated]
		internal set
		{
			this.<rect>k__BackingField = value;
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00070F94 File Offset: 0x0006F194
	private void Awake()
	{
		this.fade = base.GetComponent<CanvasGroup>();
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x00070FB0 File Offset: 0x0006F1B0
	public void Setup(Mana m, Vector3 targetPoint, ManaGroup g)
	{
		this.ClearCallbacks();
		this.group = g;
		this.TempDisplay.SetActive(m.IsTemp);
		this.InkBack.transform.localEulerAngles = new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
		this.InkBack.sprite = this.InkBacks[UnityEngine.Random.Range(0, this.InkBacks.Count)];
		if (this.localTarget != targetPoint)
		{
			base.transform.localPosition = targetPoint;
		}
		this.mana = m;
		Mana mana = this.mana;
		mana.OnGained = (Action<Mana, bool>)Delegate.Combine(mana.OnGained, new Action<Mana, bool>(this.ManaGained));
		Mana mana2 = this.mana;
		mana2.OnRegen = (Action<Mana, bool>)Delegate.Combine(mana2.OnRegen, new Action<Mana, bool>(this.ManaRegen));
		Mana mana3 = this.mana;
		mana3.OnUsed = (Action<Mana>)Delegate.Combine(mana3.OnUsed, new Action<Mana>(this.ManaUsed));
		Mana mana4 = this.mana;
		mana4.OnTransform = (Action<Mana, bool>)Delegate.Combine(mana4.OnTransform, new Action<Mana, bool>(this.ManaTransform));
		Mana mana5 = this.mana;
		mana5.OnRechargedPartial = (Action<Mana>)Delegate.Combine(mana5.OnRechargedPartial, new Action<Mana>(this.ManaChargedPartial));
		this.UpdateElement();
		this.TickUpdate(targetPoint);
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x00071120 File Offset: 0x0006F320
	public void TickUpdate(Vector3 location)
	{
		this.localTarget = location;
		bool flag;
		if (this.mana.IsAvailable)
		{
			if (this.mana.magicColor == MagicColor.Neutral)
			{
				PlayerControl myInstance = PlayerControl.myInstance;
				flag = (myInstance != null && myInstance.SignatureColor == MagicColor.Neutral);
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = false;
		}
		bool flag2 = flag;
		if (!flag2)
		{
			this.BackFill.fillAmount = this.mana.RechargeProp;
			this.FillSlider.value = this.mana.RechargeProp;
		}
		else
		{
			this.BackFill.fillAmount = 0f;
		}
		this.ReadyGroup.alpha = (float)(this.mana.IsAvailable ? 1 : 0);
		this.FillSliderGroup.UpdateOpacity(!flag2 && this.mana.RechargeProp > 0f, 8f, true);
		this.UpdateElement();
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.localTarget, Time.deltaTime * 6f);
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x00071223 File Offset: 0x0006F423
	public void PlayAnim(string animName)
	{
		this.anim.Play(animName, -1, 0f);
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x00071238 File Offset: 0x0006F438
	private void UpdateElement()
	{
		ManaPip.ManaColorDisplay display = this.GetDisplay(this.mana.magicColor);
		this.BackFill.sprite = display.BackgroundHex;
		this.Outline.sprite = display.Outline;
		this.FillHandle.sprite = display.FillLine;
		this.ReadyDisplay.sprite = display.ReadyIcon;
		if (this.mana.magicColor != this.mana.wantedColor && this.mana.IsAvailable)
		{
			display = this.GetDisplay(this.mana.wantedColor);
			this.BackFill.sprite = display.BackgroundHex;
			this.Outline.sprite = display.Outline;
			this.FillHandle.sprite = display.FillLine;
			this._magicColor = this.mana.magicColor;
		}
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x00071316 File Offset: 0x0006F516
	private void ManaRegen(Mana m, bool ignoreFX = false)
	{
		ManaDisplay.instance.ManaRegen(this, ignoreFX);
		if (!ignoreFX)
		{
			PlayerControl.myInstance.Audio.PlayManaRegen(m.magicColor);
		}
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x0007133C File Offset: 0x0006F53C
	private void ManaTransform(Mana m, bool ignoreFX = false)
	{
		ManaDisplay.instance.ManaTransform(this, m.magicColor, ignoreFX);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x00071350 File Offset: 0x0006F550
	private void ManaChargedPartial(Mana m)
	{
		ManaDisplay.instance.ManaRechargePartial(this);
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x0007135D File Offset: 0x0006F55D
	private void ManaUsed(Mana m)
	{
		ManaDisplay.instance.ManaUsed(this);
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x0007136A File Offset: 0x0006F56A
	private void ManaGained(Mana m, bool ignoreFX)
	{
		ManaDisplay.instance.ManaRegen(this, ignoreFX);
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x00071378 File Offset: 0x0006F578
	private ManaPip.ManaColorDisplay GetDisplay(MagicColor color)
	{
		foreach (ManaPip.ManaColorDisplay manaColorDisplay in this.Colors)
		{
			if (manaColorDisplay.Color == color)
			{
				return manaColorDisplay;
			}
		}
		return this.Neutral;
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000713DC File Offset: 0x0006F5DC
	private void ClearCallbacks()
	{
		if (this.mana != null)
		{
			Mana mana = this.mana;
			mana.OnGained = (Action<Mana, bool>)Delegate.Remove(mana.OnGained, new Action<Mana, bool>(this.ManaGained));
			Mana mana2 = this.mana;
			mana2.OnRegen = (Action<Mana, bool>)Delegate.Remove(mana2.OnRegen, new Action<Mana, bool>(this.ManaRegen));
			Mana mana3 = this.mana;
			mana3.OnUsed = (Action<Mana>)Delegate.Remove(mana3.OnUsed, new Action<Mana>(this.ManaUsed));
			Mana mana4 = this.mana;
			mana4.OnTransform = (Action<Mana, bool>)Delegate.Remove(mana4.OnTransform, new Action<Mana, bool>(this.ManaTransform));
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00071490 File Offset: 0x0006F690
	private void OnDestroy()
	{
		this.ClearCallbacks();
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00071498 File Offset: 0x0006F698
	public ManaPip()
	{
	}

	// Token: 0x04001125 RID: 4389
	public Image InkBack;

	// Token: 0x04001126 RID: 4390
	public Image BackFill;

	// Token: 0x04001127 RID: 4391
	public Image Outline;

	// Token: 0x04001128 RID: 4392
	public CanvasGroup FillSliderGroup;

	// Token: 0x04001129 RID: 4393
	public Slider FillSlider;

	// Token: 0x0400112A RID: 4394
	public Image FillHandle;

	// Token: 0x0400112B RID: 4395
	public Image ReadyDisplay;

	// Token: 0x0400112C RID: 4396
	public CanvasGroup ReadyGroup;

	// Token: 0x0400112D RID: 4397
	public GameObject TempDisplay;

	// Token: 0x0400112E RID: 4398
	public Animator anim;

	// Token: 0x0400112F RID: 4399
	public ManaPip.ManaColorDisplay Neutral;

	// Token: 0x04001130 RID: 4400
	public List<ManaPip.ManaColorDisplay> Colors;

	// Token: 0x04001131 RID: 4401
	public List<Sprite> InkBacks;

	// Token: 0x04001132 RID: 4402
	[CompilerGenerated]
	private RectTransform <rect>k__BackingField;

	// Token: 0x04001133 RID: 4403
	private CanvasGroup fade;

	// Token: 0x04001134 RID: 4404
	private bool tempMana;

	// Token: 0x04001135 RID: 4405
	public Mana mana;

	// Token: 0x04001136 RID: 4406
	private bool wasAvailable;

	// Token: 0x04001137 RID: 4407
	private float colorLerp;

	// Token: 0x04001138 RID: 4408
	private ManaGroup group;

	// Token: 0x04001139 RID: 4409
	private MagicColor _magicColor = MagicColor.Neutral;

	// Token: 0x0400113A RID: 4410
	private Vector3 localTarget = Vector3.one.INVALID();

	// Token: 0x0200057F RID: 1407
	[Serializable]
	public class ManaColorDisplay
	{
		// Token: 0x06002520 RID: 9504 RVA: 0x000D0934 File Offset: 0x000CEB34
		public ManaColorDisplay()
		{
		}

		// Token: 0x04002769 RID: 10089
		public MagicColor Color;

		// Token: 0x0400276A RID: 10090
		public Sprite BackgroundHex;

		// Token: 0x0400276B RID: 10091
		public Sprite Outline;

		// Token: 0x0400276C RID: 10092
		public Sprite FillLine;

		// Token: 0x0400276D RID: 10093
		public Sprite ReadyIcon;
	}
}
