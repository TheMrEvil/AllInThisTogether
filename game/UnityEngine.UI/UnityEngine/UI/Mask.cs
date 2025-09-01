using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000029 RID: 41
	[AddComponentMenu("UI/Mask", 13)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_RectTransform) == null)
				{
					result = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000F2EA File Offset: 0x0000D4EA
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000F318 File Offset: 0x0000D518
		public Graphic graphic
		{
			get
			{
				Graphic result;
				if ((result = this.m_Graphic) == null)
				{
					result = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return result;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F33E File Offset: 0x0000D53E
		protected Mask()
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F34D File Offset: 0x0000D54D
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000F365 File Offset: 0x0000D565
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000F368 File Offset: 0x0000D568
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = true;
				}
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
				if (this.graphic is MaskableGraphic)
				{
					(this.graphic as MaskableGraphic).isMaskingGraphic = false;
				}
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000F462 File Offset: 0x0000D662
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000F47C File Offset: 0x0000D67C
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!this.MaskEnabled())
			{
				return baseMaterial;
			}
			Transform stopAfter = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, stopAfter);
			if (stencilDepth >= 8)
			{
				Debug.LogWarning("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				Material maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0));
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMaterial;
				Material unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = unmaskMaterial;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material maskMaterial2 = StencilMaterial.Add(baseMaterial, num | num - 1, StencilOp.Replace, CompareFunction.Equal, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0), num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = maskMaterial2;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material unmaskMaterial2 = StencilMaterial.Add(baseMaterial, num - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, num - 1, num | num - 1);
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = unmaskMaterial2;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}

		// Token: 0x040000F6 RID: 246
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x040000F8 RID: 248
		[NonSerialized]
		private Graphic m_Graphic;

		// Token: 0x040000F9 RID: 249
		[NonSerialized]
		private Material m_MaskMaterial;

		// Token: 0x040000FA RID: 250
		[NonSerialized]
		private Material m_UnmaskMaterial;
	}
}
