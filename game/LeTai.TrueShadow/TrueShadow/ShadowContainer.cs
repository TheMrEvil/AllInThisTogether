using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x02000015 RID: 21
	internal class ShadowContainer
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004E29 File Offset: 0x00003029
		public RenderTexture Texture
		{
			[CompilerGenerated]
			get
			{
				return this.<Texture>k__BackingField;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004E31 File Offset: 0x00003031
		public ShadowSettingSnapshot Snapshot
		{
			[CompilerGenerated]
			get
			{
				return this.<Snapshot>k__BackingField;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004E39 File Offset: 0x00003039
		public int Padding
		{
			[CompilerGenerated]
			get
			{
				return this.<Padding>k__BackingField;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004E41 File Offset: 0x00003041
		public Vector2Int ImprintSize
		{
			[CompilerGenerated]
			get
			{
				return this.<ImprintSize>k__BackingField;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004E49 File Offset: 0x00003049
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004E51 File Offset: 0x00003051
		public int RefCount
		{
			[CompilerGenerated]
			get
			{
				return this.<RefCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<RefCount>k__BackingField = value;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004E5A File Offset: 0x0000305A
		internal ShadowContainer(RenderTexture texture, ShadowSettingSnapshot snapshot, int padding, Vector2Int imprintSize)
		{
			this.Texture = texture;
			this.Snapshot = snapshot;
			this.Padding = padding;
			this.ImprintSize = imprintSize;
			this.RefCount = 1;
			this.requestHash = snapshot.GetHashCode();
		}

		// Token: 0x04000078 RID: 120
		[CompilerGenerated]
		private readonly RenderTexture <Texture>k__BackingField;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		private readonly ShadowSettingSnapshot <Snapshot>k__BackingField;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		private readonly int <Padding>k__BackingField;

		// Token: 0x0400007B RID: 123
		[CompilerGenerated]
		private readonly Vector2Int <ImprintSize>k__BackingField;

		// Token: 0x0400007C RID: 124
		[CompilerGenerated]
		private int <RefCount>k__BackingField;

		// Token: 0x0400007D RID: 125
		public readonly int requestHash;
	}
}
