using System;

namespace Steamworks.Data
{
	// Token: 0x020001F1 RID: 497
	public struct DepotId
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x00019A00 File Offset: 0x00017C00
		public static implicit operator DepotId(uint value)
		{
			return new DepotId
			{
				Value = value
			};
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00019A24 File Offset: 0x00017C24
		public static implicit operator DepotId(int value)
		{
			return new DepotId
			{
				Value = (uint)value
			};
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00019A48 File Offset: 0x00017C48
		public static implicit operator uint(DepotId value)
		{
			return value.Value;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00019A60 File Offset: 0x00017C60
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x04000BEC RID: 3052
		public uint Value;
	}
}
