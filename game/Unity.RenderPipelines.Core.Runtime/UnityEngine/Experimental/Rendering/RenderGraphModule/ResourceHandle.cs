using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000036 RID: 54
	internal struct ResourceHandle
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000BB65 File Offset: 0x00009D65
		public int index
		{
			get
			{
				return (int)(this.m_Value & 65535U);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000BB73 File Offset: 0x00009D73
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000BB7B File Offset: 0x00009D7B
		public RenderGraphResourceType type
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<type>k__BackingField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000BB84 File Offset: 0x00009D84
		public int iType
		{
			get
			{
				return (int)this.type;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000BB8C File Offset: 0x00009D8C
		internal ResourceHandle(int value, RenderGraphResourceType type, bool shared)
		{
			this.m_Value = (uint)((value & 65535) | (int)(shared ? ResourceHandle.s_SharedResourceValidBit : ResourceHandle.s_CurrentValidBit));
			this.type = type;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BBB2 File Offset: 0x00009DB2
		public static implicit operator int(ResourceHandle handle)
		{
			return handle.index;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public bool IsValid()
		{
			uint num = this.m_Value & 4294901760U;
			return num != 0U && (num == ResourceHandle.s_CurrentValidBit || num == ResourceHandle.s_SharedResourceValidBit);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		public static void NewFrame(int executionIndex)
		{
			uint num = ResourceHandle.s_CurrentValidBit;
			ResourceHandle.s_CurrentValidBit = (uint)((uint)(executionIndex >> 16 ^ (executionIndex & 65535) * 58546883) << 16);
			if (ResourceHandle.s_CurrentValidBit == 0U || ResourceHandle.s_CurrentValidBit == ResourceHandle.s_SharedResourceValidBit)
			{
				uint num2 = 1U;
				while (num == num2 << 16)
				{
					num2 += 1U;
				}
				ResourceHandle.s_CurrentValidBit = num2 << 16;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000BC48 File Offset: 0x00009E48
		// Note: this type is marked as 'beforefieldinit'.
		static ResourceHandle()
		{
		}

		// Token: 0x0400014D RID: 333
		private const uint kValidityMask = 4294901760U;

		// Token: 0x0400014E RID: 334
		private const uint kIndexMask = 65535U;

		// Token: 0x0400014F RID: 335
		private uint m_Value;

		// Token: 0x04000150 RID: 336
		private static uint s_CurrentValidBit = 65536U;

		// Token: 0x04000151 RID: 337
		private static uint s_SharedResourceValidBit = 2147418112U;

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		private RenderGraphResourceType <type>k__BackingField;
	}
}
