using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000069 RID: 105
	public sealed class PropertySheet
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00010C28 File Offset: 0x0000EE28
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00010C30 File Offset: 0x0000EE30
		public MaterialPropertyBlock properties
		{
			[CompilerGenerated]
			get
			{
				return this.<properties>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<properties>k__BackingField = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00010C39 File Offset: 0x0000EE39
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00010C41 File Offset: 0x0000EE41
		internal Material material
		{
			[CompilerGenerated]
			get
			{
				return this.<material>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<material>k__BackingField = value;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00010C4A File Offset: 0x0000EE4A
		internal PropertySheet(Material material)
		{
			this.material = material;
			this.properties = new MaterialPropertyBlock();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00010C64 File Offset: 0x0000EE64
		public void ClearKeywords()
		{
			this.material.shaderKeywords = null;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010C72 File Offset: 0x0000EE72
		public void EnableKeyword(string keyword)
		{
			this.material.EnableKeyword(keyword);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010C80 File Offset: 0x0000EE80
		public void DisableKeyword(string keyword)
		{
			this.material.DisableKeyword(keyword);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00010C8E File Offset: 0x0000EE8E
		internal void Release()
		{
			RuntimeUtilities.Destroy(this.material);
			this.material = null;
		}

		// Token: 0x0400021F RID: 543
		[CompilerGenerated]
		private MaterialPropertyBlock <properties>k__BackingField;

		// Token: 0x04000220 RID: 544
		[CompilerGenerated]
		private Material <material>k__BackingField;
	}
}
