using System;

namespace Steamworks.Data
{
	// Token: 0x020001F7 RID: 503
	public struct Image
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x00019B2C File Offset: 0x00017D2C
		public Color GetPixel(int x, int y)
		{
			bool flag = x < 0 || (long)x >= (long)((ulong)this.Width);
			if (flag)
			{
				throw new Exception("x out of bounds");
			}
			bool flag2 = y < 0 || (long)y >= (long)((ulong)this.Height);
			if (flag2)
			{
				throw new Exception("y out of bounds");
			}
			Color result = default(Color);
			long num = ((long)y * (long)((ulong)this.Width) + (long)x) * 4L;
			checked
			{
				result.r = this.Data[(int)((IntPtr)num)];
				result.g = this.Data[(int)((IntPtr)(unchecked(num + 1L)))];
				result.b = this.Data[(int)((IntPtr)(unchecked(num + 2L)))];
				result.a = this.Data[(int)((IntPtr)(unchecked(num + 3L)))];
				return result;
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00019BEC File Offset: 0x00017DEC
		public override string ToString()
		{
			return string.Format("{0}x{1} ({2}bytes)", this.Width, this.Height, this.Data.Length);
		}

		// Token: 0x04000BF8 RID: 3064
		public uint Width;

		// Token: 0x04000BF9 RID: 3065
		public uint Height;

		// Token: 0x04000BFA RID: 3066
		public byte[] Data;
	}
}
