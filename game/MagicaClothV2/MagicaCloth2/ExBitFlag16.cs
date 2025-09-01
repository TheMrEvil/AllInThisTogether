using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public struct ExBitFlag16
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00020848 File Offset: 0x0001EA48
		public ExBitFlag16(ushort initialValue = 0)
		{
			this.Value = initialValue;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00020851 File Offset: 0x0001EA51
		public void Clear()
		{
			this.Value = 0;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0002085A File Offset: 0x0001EA5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(ushort flag, bool sw)
		{
			if (sw)
			{
				this.Value |= flag;
				return;
			}
			this.Value &= ~flag;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0002087F File Offset: 0x0001EA7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(ushort flag)
		{
			return (this.Value & flag) > 0;
		}

		// Token: 0x04000636 RID: 1590
		public ushort Value;
	}
}
