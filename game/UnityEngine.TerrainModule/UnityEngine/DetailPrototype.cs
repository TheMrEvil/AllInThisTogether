using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DetailPrototype
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002620 File Offset: 0x00000820
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002638 File Offset: 0x00000838
		public GameObject prototype
		{
			get
			{
				return this.m_Prototype;
			}
			set
			{
				this.m_Prototype = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002644 File Offset: 0x00000844
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000265C File Offset: 0x0000085C
		public Texture2D prototypeTexture
		{
			get
			{
				return this.m_PrototypeTexture;
			}
			set
			{
				this.m_PrototypeTexture = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002668 File Offset: 0x00000868
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002680 File Offset: 0x00000880
		public float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				this.m_MinWidth = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000268C File Offset: 0x0000088C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000026A4 File Offset: 0x000008A4
		public float maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				this.m_MaxWidth = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000026C8 File Offset: 0x000008C8
		public float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				this.m_MinHeight = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000026D4 File Offset: 0x000008D4
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000026EC File Offset: 0x000008EC
		public float maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				this.m_MaxHeight = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000026F8 File Offset: 0x000008F8
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002710 File Offset: 0x00000910
		public int noiseSeed
		{
			get
			{
				return this.m_NoiseSeed;
			}
			set
			{
				this.m_NoiseSeed = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000271C File Offset: 0x0000091C
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002734 File Offset: 0x00000934
		public float noiseSpread
		{
			get
			{
				return this.m_NoiseSpread;
			}
			set
			{
				this.m_NoiseSpread = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002237 File Offset: 0x00000437
		[Obsolete("bendFactor has no effect and is deprecated.", false)]
		public float bendFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002758 File Offset: 0x00000958
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002770 File Offset: 0x00000970
		public float holeEdgePadding
		{
			get
			{
				return this.m_HoleEdgePadding;
			}
			set
			{
				this.m_HoleEdgePadding = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002794 File Offset: 0x00000994
		public Color healthyColor
		{
			get
			{
				return this.m_HealthyColor;
			}
			set
			{
				this.m_HealthyColor = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000027A0 File Offset: 0x000009A0
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000027B8 File Offset: 0x000009B8
		public Color dryColor
		{
			get
			{
				return this.m_DryColor;
			}
			set
			{
				this.m_DryColor = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000027DC File Offset: 0x000009DC
		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)this.m_RenderMode;
			}
			set
			{
				this.m_RenderMode = (int)value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000027E8 File Offset: 0x000009E8
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002803 File Offset: 0x00000A03
		public bool usePrototypeMesh
		{
			get
			{
				return this.m_UsePrototypeMesh != 0;
			}
			set
			{
				this.m_UsePrototypeMesh = (value ? 1 : 0);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002814 File Offset: 0x00000A14
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000282F File Offset: 0x00000A2F
		public bool useInstancing
		{
			get
			{
				return this.m_UseInstancing != 0;
			}
			set
			{
				this.m_UseInstancing = (value ? 1 : 0);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002840 File Offset: 0x00000A40
		public DetailPrototype()
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000028D8 File Offset: 0x00000AD8
		public DetailPrototype(DetailPrototype other)
		{
			this.m_Prototype = other.m_Prototype;
			this.m_PrototypeTexture = other.m_PrototypeTexture;
			this.m_HealthyColor = other.m_HealthyColor;
			this.m_DryColor = other.m_DryColor;
			this.m_MinWidth = other.m_MinWidth;
			this.m_MaxWidth = other.m_MaxWidth;
			this.m_MinHeight = other.m_MinHeight;
			this.m_MaxHeight = other.m_MaxHeight;
			this.m_NoiseSeed = other.m_NoiseSeed;
			this.m_NoiseSpread = other.m_NoiseSpread;
			this.m_HoleEdgePadding = other.m_HoleEdgePadding;
			this.m_RenderMode = other.m_RenderMode;
			this.m_UsePrototypeMesh = other.m_UsePrototypeMesh;
			this.m_UseInstancing = other.m_UseInstancing;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002A18 File Offset: 0x00000C18
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DetailPrototype);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002A38 File Offset: 0x00000C38
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002A50 File Offset: 0x00000C50
		private bool Equals(DetailPrototype other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = other == this;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = base.GetType() != other.GetType();
					result = (!flag3 && (this.m_Prototype == other.m_Prototype && this.m_PrototypeTexture == other.m_PrototypeTexture && this.m_HealthyColor == other.m_HealthyColor && this.m_DryColor == other.m_DryColor && this.m_MinWidth == other.m_MinWidth && this.m_MaxWidth == other.m_MaxWidth && this.m_MinHeight == other.m_MinHeight && this.m_MaxHeight == other.m_MaxHeight && this.m_NoiseSeed == other.m_NoiseSeed && this.m_NoiseSpread == other.m_NoiseSpread && this.m_HoleEdgePadding == other.m_HoleEdgePadding && this.m_RenderMode == other.m_RenderMode && this.m_UsePrototypeMesh == other.m_UsePrototypeMesh) && this.m_UseInstancing == other.m_UseInstancing);
				}
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002B88 File Offset: 0x00000D88
		public bool Validate()
		{
			string text;
			return DetailPrototype.ValidateDetailPrototype(this, out text);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002B9D File Offset: 0x00000D9D
		public bool Validate(out string errorMessage)
		{
			return DetailPrototype.ValidateDetailPrototype(this, out errorMessage);
		}

		// Token: 0x060000BF RID: 191
		[FreeFunction("TerrainDataScriptingInterface::ValidateDetailPrototype")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ValidateDetailPrototype([NotNull("ArgumentNullException")] DetailPrototype prototype, out string errorMessage);

		// Token: 0x060000C0 RID: 192 RVA: 0x00002BA8 File Offset: 0x00000DA8
		internal static bool IsModeSupportedByRenderPipeline(DetailRenderMode renderMode, bool useInstancing, out string errorMessage)
		{
			bool flag = GraphicsSettings.currentRenderPipeline != null;
			if (flag)
			{
				bool flag2 = renderMode == DetailRenderMode.GrassBillboard && GraphicsSettings.currentRenderPipeline.terrainDetailGrassBillboardShader == null;
				if (flag2)
				{
					errorMessage = "The current render pipeline does not support Billboard details. Details will not be rendered.";
					return false;
				}
				bool flag3 = renderMode == DetailRenderMode.VertexLit && !useInstancing && GraphicsSettings.currentRenderPipeline.terrainDetailLitShader == null;
				if (flag3)
				{
					errorMessage = "The current render pipeline does not support VertexLit details. Details will be rendered using the default shader.";
					return false;
				}
				bool flag4 = renderMode == DetailRenderMode.Grass && GraphicsSettings.currentRenderPipeline.terrainDetailGrassShader == null;
				if (flag4)
				{
					errorMessage = "The current render pipeline does not support Grass details. Details will be rendered using the default shader without alpha test and animation.";
					return false;
				}
			}
			errorMessage = string.Empty;
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002C4C File Offset: 0x00000E4C
		// Note: this type is marked as 'beforefieldinit'.
		static DetailPrototype()
		{
		}

		// Token: 0x04000022 RID: 34
		internal static readonly Color DefaultHealthColor = new Color(0.2627451f, 0.9764706f, 0.16470589f, 1f);

		// Token: 0x04000023 RID: 35
		internal static readonly Color DefaultDryColor = new Color(0.8039216f, 0.7372549f, 0.101960786f, 1f);

		// Token: 0x04000024 RID: 36
		internal GameObject m_Prototype = null;

		// Token: 0x04000025 RID: 37
		internal Texture2D m_PrototypeTexture = null;

		// Token: 0x04000026 RID: 38
		internal Color m_HealthyColor = DetailPrototype.DefaultHealthColor;

		// Token: 0x04000027 RID: 39
		internal Color m_DryColor = DetailPrototype.DefaultDryColor;

		// Token: 0x04000028 RID: 40
		internal float m_MinWidth = 1f;

		// Token: 0x04000029 RID: 41
		internal float m_MaxWidth = 2f;

		// Token: 0x0400002A RID: 42
		internal float m_MinHeight = 1f;

		// Token: 0x0400002B RID: 43
		internal float m_MaxHeight = 2f;

		// Token: 0x0400002C RID: 44
		internal int m_NoiseSeed = 0;

		// Token: 0x0400002D RID: 45
		internal float m_NoiseSpread = 0.1f;

		// Token: 0x0400002E RID: 46
		internal float m_HoleEdgePadding = 0f;

		// Token: 0x0400002F RID: 47
		internal int m_RenderMode = 2;

		// Token: 0x04000030 RID: 48
		internal int m_UsePrototypeMesh = 0;

		// Token: 0x04000031 RID: 49
		internal int m_UseInstancing = 0;
	}
}
