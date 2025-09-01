using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public struct ExBitFlag8
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0002088C File Offset: 0x0001EA8C
		public ExBitFlag8(byte initialValue = 0)
		{
			this.Value = initialValue;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00020895 File Offset: 0x0001EA95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			this.Value = 0;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0002089E File Offset: 0x0001EA9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(byte flag, bool sw)
		{
			if (sw)
			{
				this.Value |= flag;
				return;
			}
			this.Value &= ~flag;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000208C3 File Offset: 0x0001EAC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(byte flag)
		{
			return (this.Value & flag) > 0;
		}

		// Token: 0x04000637 RID: 1591
		public byte Value;
	}
}
