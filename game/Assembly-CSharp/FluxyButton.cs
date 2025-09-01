using System;
using Fluxy;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Token: 0x02000196 RID: 406
public class FluxyButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x0600110F RID: 4367 RVA: 0x00069F64 File Offset: 0x00068164
	public float Opacity
	{
		get
		{
			if (this.canvasGrps == null || this.canvasGrps.Length == 0)
			{
				return 1f;
			}
			float num = 1f;
			CanvasGroup[] array = this.canvasGrps;
			for (int i = 0; i < array.Length; i++)
			{
				num = Mathf.Min(array[i].alpha, num);
			}
			return num;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06001110 RID: 4368 RVA: 0x00069FB3 File Offset: 0x000681B3
	public bool IsInteractable
	{
		get
		{
			if (this.button != null)
			{
				return this.button.interactable;
			}
			return !(this.inputField != null) || this.inputField.interactable;
		}
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00069FEC File Offset: 0x000681EC
	private void Start()
	{
		FluxyCanvasContainer container = UIFluidManager.GetContainer(this.FluidType);
		if (container == null)
		{
			return;
		}
		this.inputField = base.GetComponent<TMP_InputField>();
		this.button = base.GetComponent<Button>();
		if (this.button != null)
		{
			this.button.onClick.AddListener(new UnityAction(this.Click));
		}
		this.canvasGrps = base.GetComponentsInParent<CanvasGroup>();
		this.rect = base.GetComponent<RectTransform>();
		this.target = base.GetComponent<FluxyTarget>();
		if (this.target == null)
		{
			this.AddTarget();
		}
		container.AddTarget(this.target);
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0006A098 File Offset: 0x00068298
	private void AddTarget()
	{
		this.target = base.gameObject.AddComponent<FluxyTarget>();
		this.target.densityWeight = 0f;
		this.wantWeight = 0f;
		this.target.scaleWithDistance = false;
		this.target.scaleWithTransform = false;
		this.target.scale = new Vector2(this.rect.sizeDelta.x * 0.0005f, this.rect.sizeDelta.y * 0.0005f);
		this.target.splatMaterial = UIFluidManager.instance.SplatMat;
		this.target.densityTexture = ((this.TexOverride == null) ? UIFluidManager.instance.SplatTex : this.TexOverride);
		this.target.velocityTexture = ((this.VelocityOverride == null) ? UIFluidManager.instance.VelTex : this.VelocityOverride);
		this.target.noiseTexture = UIFluidManager.instance.NoiseTex;
		this.target.srcBlend = BlendMode.One;
		this.target.dstBlend = BlendMode.SrcAlphaSaturate;
		this.target.color = this.color;
		this.target.velocityNoise = 2f;
		this.target.velocityNoiseOffset = 0.5f;
		this.target.velocityNoiseTiling = 2f;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x0006A200 File Offset: 0x00068400
	public void DoUpdate()
	{
		if (this.target == null)
		{
			return;
		}
		float opacity = this.Opacity;
		if (opacity <= 0f)
		{
			if (this.target.enabled)
			{
				this.target.enabled = false;
			}
			return;
		}
		if (!this.target.enabled)
		{
			this.target.enabled = true;
		}
		if (this.button != null || this.inputField != null || this.MouseRequired)
		{
			this.wantWeight = (this.mouseOver ? (0.2f * this.DefaultWeight) : 0f);
		}
		else
		{
			this.wantWeight = this.DefaultWeight;
		}
		this.wantWeight = Mathf.Min(this.wantWeight, opacity);
		this.target.densityWeight = this.wantWeight;
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0006A2D4 File Offset: 0x000684D4
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (InputManager.IsUsingController)
		{
			return;
		}
		if (this.button != null && !this.IsInteractable)
		{
			return;
		}
		this.mouseOver = true;
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0006A2FC File Offset: 0x000684FC
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0006A305 File Offset: 0x00068505
	public void OnSelect(BaseEventData ev)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.button != null && !this.IsInteractable)
		{
			return;
		}
		this.mouseOver = true;
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0006A32D File Offset: 0x0006852D
	public void OnDeselect(BaseEventData ev)
	{
		this.mouseOver = false;
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0006A336 File Offset: 0x00068536
	private void Click()
	{
		this.target.densityWeight = 1f;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x0006A348 File Offset: 0x00068548
	private void OnEnable()
	{
		UIFluidManager.Buttons.Add(this);
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0006A355 File Offset: 0x00068555
	private void OnDisable()
	{
		UIFluidManager.Buttons.Remove(this);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0006A363 File Offset: 0x00068563
	public FluxyButton()
	{
	}

	// Token: 0x04000F6E RID: 3950
	public UIFluidType FluidType;

	// Token: 0x04000F6F RID: 3951
	private RectTransform rect;

	// Token: 0x04000F70 RID: 3952
	private FluxyTarget target;

	// Token: 0x04000F71 RID: 3953
	private Button button;

	// Token: 0x04000F72 RID: 3954
	private TMP_InputField inputField;

	// Token: 0x04000F73 RID: 3955
	public Color color = Color.white;

	// Token: 0x04000F74 RID: 3956
	public Texture2D TexOverride;

	// Token: 0x04000F75 RID: 3957
	public Texture2D VelocityOverride;

	// Token: 0x04000F76 RID: 3958
	public bool MouseRequired;

	// Token: 0x04000F77 RID: 3959
	public float DefaultWeight = 1f;

	// Token: 0x04000F78 RID: 3960
	private CanvasGroup[] canvasGrps;

	// Token: 0x04000F79 RID: 3961
	private float wantWeight;

	// Token: 0x04000F7A RID: 3962
	private bool mouseOver;
}
