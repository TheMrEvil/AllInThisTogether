using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x02000006 RID: 6
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Glyph
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000299C File Offset: 0x00000B9C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000029B4 File Offset: 0x00000BB4
		public uint index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029C0 File Offset: 0x00000BC0
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000029D8 File Offset: 0x00000BD8
		public GlyphMetrics metrics
		{
			get
			{
				return this.m_Metrics;
			}
			set
			{
				this.m_Metrics = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029E4 File Offset: 0x00000BE4
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000029FC File Offset: 0x00000BFC
		public GlyphRect glyphRect
		{
			get
			{
				return this.m_GlyphRect;
			}
			set
			{
				this.m_GlyphRect = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002A20 File Offset: 0x00000C20
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002A44 File Offset: 0x00000C44
		public int atlasIndex
		{
			get
			{
				return this.m_AtlasIndex;
			}
			set
			{
				this.m_AtlasIndex = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002A50 File Offset: 0x00000C50
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002A68 File Offset: 0x00000C68
		public GlyphClassDefinitionType classDefinitionType
		{
			get
			{
				return this.m_ClassDefinitionType;
			}
			set
			{
				this.m_ClassDefinitionType = value;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A72 File Offset: 0x00000C72
		public Glyph()
		{
			this.m_Index = 0U;
			this.m_Metrics = default(GlyphMetrics);
			this.m_GlyphRect = default(GlyphRect);
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public Glyph(Glyph glyph)
		{
			this.m_Index = glyph.index;
			this.m_Metrics = glyph.metrics;
			this.m_GlyphRect = glyph.glyphRect;
			this.m_Scale = glyph.scale;
			this.m_AtlasIndex = glyph.atlasIndex;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B04 File Offset: 0x00000D04
		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			this.m_Index = glyphStruct.index;
			this.m_Metrics = glyphStruct.metrics;
			this.m_GlyphRect = glyphStruct.glyphRect;
			this.m_Scale = glyphStruct.scale;
			this.m_AtlasIndex = glyphStruct.atlasIndex;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B55 File Offset: 0x00000D55
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B86 File Offset: 0x00000D86
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = scale;
			this.m_AtlasIndex = atlasIndex;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public bool Compare(Glyph other)
		{
			return this.index == other.index && this.metrics == other.metrics && this.glyphRect == other.glyphRect && this.scale == other.scale && this.atlasIndex == other.atlasIndex;
		}

		// Token: 0x04000026 RID: 38
		[NativeName("index")]
		[SerializeField]
		private uint m_Index;

		// Token: 0x04000027 RID: 39
		[NativeName("metrics")]
		[SerializeField]
		private GlyphMetrics m_Metrics;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		[NativeName("glyphRect")]
		private GlyphRect m_GlyphRect;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		// Token: 0x0400002A RID: 42
		[NativeName("atlasIndex")]
		[SerializeField]
		private int m_AtlasIndex;

		// Token: 0x0400002B RID: 43
		[NativeName("type")]
		[SerializeField]
		private GlyphClassDefinitionType m_ClassDefinitionType;
	}
}
