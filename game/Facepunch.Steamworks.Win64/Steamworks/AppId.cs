using System;

namespace Steamworks
{
	// Token: 0x020000A9 RID: 169
	public struct AppId
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00010BB9 File Offset: 0x0000EDB9
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public static implicit operator AppId(uint value)
		{
			return new AppId
			{
				Value = value
			};
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public static implicit operator AppId(int value)
		{
			return new AppId
			{
				Value = (uint)value
			};
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00010C10 File Offset: 0x0000EE10
		public static implicit operator uint(AppId value)
		{
			return value.Value;
		}

		// Token: 0x04000742 RID: 1858
		public uint Value;
	}
}
