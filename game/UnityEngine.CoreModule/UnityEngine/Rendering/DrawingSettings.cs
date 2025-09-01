using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FC RID: 1020
	public struct DrawingSettings : IEquatable<DrawingSettings>
	{
		// Token: 0x060022AF RID: 8879 RVA: 0x0003A3BC File Offset: 0x000385BC
		public unsafe DrawingSettings(ShaderTagId shaderPassName, SortingSettings sortingSettings)
		{
			this.m_SortingSettings = sortingSettings;
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_OverrideMaterialInstanceId = 0;
			this.m_OverrideMaterialPassIndex = 0;
			this.m_fallbackMaterialInstanceId = 0;
			this.m_MainLightIndex = -1;
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				*ptr2 = shaderPassName.id;
				for (int i = 1; i < DrawingSettings.maxShaderPasses; i++)
				{
					ptr2[i] = -1;
				}
			}
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_UseSrpBatcher = 0;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0003A44C File Offset: 0x0003864C
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0003A464 File Offset: 0x00038664
		public SortingSettings sortingSettings
		{
			get
			{
				return this.m_SortingSettings;
			}
			set
			{
				this.m_SortingSettings = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0003A470 File Offset: 0x00038670
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x0003A488 File Offset: 0x00038688
		public PerObjectData perObjectData
		{
			get
			{
				return this.m_PerObjectData;
			}
			set
			{
				this.m_PerObjectData = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0003A494 File Offset: 0x00038694
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x0003A4B4 File Offset: 0x000386B4
		public bool enableDynamicBatching
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableDynamicBatching) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableDynamicBatching;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableDynamicBatching;
				}
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0003A4E8 File Offset: 0x000386E8
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x0003A508 File Offset: 0x00038708
		public bool enableInstancing
		{
			get
			{
				return (this.m_Flags & DrawRendererFlags.EnableInstancing) > DrawRendererFlags.None;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableInstancing;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableInstancing;
				}
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0003A53C File Offset: 0x0003873C
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x0003A569 File Offset: 0x00038769
		public Material overrideMaterial
		{
			get
			{
				return (this.m_OverrideMaterialInstanceId != 0) ? (Object.FindObjectFromInstanceID(this.m_OverrideMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_OverrideMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0003A580 File Offset: 0x00038780
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x0003A598 File Offset: 0x00038798
		public int overrideMaterialPassIndex
		{
			get
			{
				return this.m_OverrideMaterialPassIndex;
			}
			set
			{
				this.m_OverrideMaterialPassIndex = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0003A5A4 File Offset: 0x000387A4
		// (set) Token: 0x060022BD RID: 8893 RVA: 0x0003A5D1 File Offset: 0x000387D1
		public Material fallbackMaterial
		{
			get
			{
				return (this.m_fallbackMaterialInstanceId != 0) ? (Object.FindObjectFromInstanceID(this.m_fallbackMaterialInstanceId) as Material) : null;
			}
			set
			{
				this.m_fallbackMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0003A5E8 File Offset: 0x000387E8
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0003A600 File Offset: 0x00038800
		public int mainLightIndex
		{
			get
			{
				return this.m_MainLightIndex;
			}
			set
			{
				this.m_MainLightIndex = value;
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0003A60C File Offset: 0x0003880C
		public unsafe ShaderTagId GetShaderPassName(int index)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				return new ShaderTagId
				{
					id = ptr2[index]
				};
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0003A680 File Offset: 0x00038880
		public unsafe void SetShaderPassName(int index, ShaderTagId shaderPassName)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* ptr2 = ptr;
				ptr2[index] = shaderPassName.id;
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0003A6E8 File Offset: 0x000388E8
		public bool Equals(DrawingSettings other)
		{
			for (int i = 0; i < DrawingSettings.maxShaderPasses; i++)
			{
				bool flag = !this.GetShaderPassName(i).Equals(other.GetShaderPassName(i));
				if (flag)
				{
					return false;
				}
			}
			return this.m_SortingSettings.Equals(other.m_SortingSettings) && this.m_PerObjectData == other.m_PerObjectData && this.m_Flags == other.m_Flags && this.m_OverrideMaterialInstanceId == other.m_OverrideMaterialInstanceId && this.m_OverrideMaterialPassIndex == other.m_OverrideMaterialPassIndex && this.m_fallbackMaterialInstanceId == other.m_fallbackMaterialInstanceId && this.m_UseSrpBatcher == other.m_UseSrpBatcher;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0003A7A4 File Offset: 0x000389A4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DrawingSettings && this.Equals((DrawingSettings)obj);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0003A7DC File Offset: 0x000389DC
		public override int GetHashCode()
		{
			int num = this.m_SortingSettings.GetHashCode();
			num = (num * 397 ^ (int)this.m_PerObjectData);
			num = (num * 397 ^ (int)this.m_Flags);
			num = (num * 397 ^ this.m_OverrideMaterialInstanceId);
			num = (num * 397 ^ this.m_OverrideMaterialPassIndex);
			num = (num * 397 ^ this.m_fallbackMaterialInstanceId);
			return num * 397 ^ this.m_UseSrpBatcher;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0003A85C File Offset: 0x00038A5C
		public static bool operator ==(DrawingSettings left, DrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0003A878 File Offset: 0x00038A78
		public static bool operator !=(DrawingSettings left, DrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0003A895 File Offset: 0x00038A95
		// Note: this type is marked as 'beforefieldinit'.
		static DrawingSettings()
		{
		}

		// Token: 0x04000CD4 RID: 3284
		private const int kMaxShaderPasses = 16;

		// Token: 0x04000CD5 RID: 3285
		public static readonly int maxShaderPasses = 16;

		// Token: 0x04000CD6 RID: 3286
		private SortingSettings m_SortingSettings;

		// Token: 0x04000CD7 RID: 3287
		[FixedBuffer(typeof(int), 16)]
		internal DrawingSettings.<shaderPassNames>e__FixedBuffer shaderPassNames;

		// Token: 0x04000CD8 RID: 3288
		private PerObjectData m_PerObjectData;

		// Token: 0x04000CD9 RID: 3289
		private DrawRendererFlags m_Flags;

		// Token: 0x04000CDA RID: 3290
		private int m_OverrideMaterialInstanceId;

		// Token: 0x04000CDB RID: 3291
		private int m_OverrideMaterialPassIndex;

		// Token: 0x04000CDC RID: 3292
		private int m_fallbackMaterialInstanceId;

		// Token: 0x04000CDD RID: 3293
		private int m_MainLightIndex;

		// Token: 0x04000CDE RID: 3294
		private int m_UseSrpBatcher;

		// Token: 0x020003FD RID: 1021
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <shaderPassNames>e__FixedBuffer
		{
			// Token: 0x04000CDF RID: 3295
			public int FixedElementField;
		}
	}
}
