using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	// Token: 0x0200009B RID: 155
	[DisallowMultipleComponent]
	public abstract class UIEffectBase : BaseMeshEffect, IParameterTexture
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00028A09 File Offset: 0x00026C09
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00028A11 File Offset: 0x00026C11
		public int parameterIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<parameterIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<parameterIndex>k__BackingField = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00028A1A File Offset: 0x00026C1A
		public virtual ParameterTexture ptex
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00028A1D File Offset: 0x00026C1D
		public Graphic targetGraphic
		{
			get
			{
				return base.graphic;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00028A25 File Offset: 0x00026C25
		public Material effectMaterial
		{
			get
			{
				return this.m_EffectMaterial;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00028A2D File Offset: 0x00026C2D
		public virtual void ModifyMaterial()
		{
			this.targetGraphic.material = (base.isActiveAndEnabled ? this.m_EffectMaterial : null);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00028A4B File Offset: 0x00026C4B
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.ptex != null)
			{
				this.ptex.Register(this);
			}
			this.ModifyMaterial();
			this.SetVerticesDirty();
			this.SetDirty();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00028A79 File Offset: 0x00026C79
		protected override void OnDisable()
		{
			base.OnDisable();
			this.ModifyMaterial();
			this.SetVerticesDirty();
			if (this.ptex != null)
			{
				this.ptex.Unregister(this);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00028AA1 File Offset: 0x00026CA1
		protected virtual void SetDirty()
		{
			this.SetVerticesDirty();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00028AA9 File Offset: 0x00026CA9
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00028AB1 File Offset: 0x00026CB1
		protected UIEffectBase()
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00028ABC File Offset: 0x00026CBC
		// Note: this type is marked as 'beforefieldinit'.
		static UIEffectBase()
		{
		}

		// Token: 0x04000534 RID: 1332
		protected static readonly Vector2[] splitedCharacterPosition = new Vector2[]
		{
			Vector2.up,
			Vector2.one,
			Vector2.right,
			Vector2.zero
		};

		// Token: 0x04000535 RID: 1333
		protected static readonly List<UIVertex> tempVerts = new List<UIVertex>();

		// Token: 0x04000536 RID: 1334
		[HideInInspector]
		[SerializeField]
		private int m_Version;

		// Token: 0x04000537 RID: 1335
		[SerializeField]
		protected Material m_EffectMaterial;

		// Token: 0x04000538 RID: 1336
		[CompilerGenerated]
		private int <parameterIndex>k__BackingField;
	}
}
