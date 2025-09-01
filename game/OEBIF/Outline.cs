using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace cakeslice
{
	// Token: 0x02000003 RID: 3
	[RequireComponent(typeof(Renderer))]
	public class Outline : MonoBehaviour
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002160 File Offset: 0x00000360
		public Renderer Renderer
		{
			[CompilerGenerated]
			get
			{
				return this.<Renderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Renderer>k__BackingField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002169 File Offset: 0x00000369
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002171 File Offset: 0x00000371
		public SpriteRenderer SpriteRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<SpriteRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SpriteRenderer>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000217A File Offset: 0x0000037A
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002182 File Offset: 0x00000382
		public SkinnedMeshRenderer SkinnedMeshRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<SkinnedMeshRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SkinnedMeshRenderer>k__BackingField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000218B File Offset: 0x0000038B
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002193 File Offset: 0x00000393
		public MeshFilter MeshFilter
		{
			[CompilerGenerated]
			get
			{
				return this.<MeshFilter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MeshFilter>k__BackingField = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000219C File Offset: 0x0000039C
		private void Awake()
		{
			this.Renderer = base.GetComponent<Renderer>();
			this.SkinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
			this.SpriteRenderer = base.GetComponent<SpriteRenderer>();
			this.MeshFilter = base.GetComponent<MeshFilter>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021CE File Offset: 0x000003CE
		public void Activate()
		{
			if (this.activated)
			{
				return;
			}
			this.activated = true;
			OutlineEffect instance = OutlineEffect.Instance;
			if (instance == null)
			{
				return;
			}
			instance.AddOutline(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021F0 File Offset: 0x000003F0
		public void Deactivate()
		{
			if (!this.activated)
			{
				return;
			}
			this.activated = false;
			OutlineEffect instance = OutlineEffect.Instance;
			if (instance == null)
			{
				return;
			}
			instance.RemoveOutline(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002212 File Offset: 0x00000412
		private void OnDestroy()
		{
			this.Deactivate();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000221A File Offset: 0x0000041A
		public Material[] SharedMaterials
		{
			get
			{
				if (this._SharedMaterials == null)
				{
					Renderer renderer = this.Renderer;
					this._SharedMaterials = (((renderer != null) ? renderer.sharedMaterials : null) ?? Array.Empty<Material>());
				}
				return this._SharedMaterials;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000224B File Offset: 0x0000044B
		public Outline()
		{
		}

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		private Renderer <Renderer>k__BackingField;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		private SpriteRenderer <SpriteRenderer>k__BackingField;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		private SkinnedMeshRenderer <SkinnedMeshRenderer>k__BackingField;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private MeshFilter <MeshFilter>k__BackingField;

		// Token: 0x04000007 RID: 7
		public int color;

		// Token: 0x04000008 RID: 8
		public bool eraseRenderer;

		// Token: 0x04000009 RID: 9
		public bool activated;

		// Token: 0x0400000A RID: 10
		private Material[] _SharedMaterials;
	}
}
