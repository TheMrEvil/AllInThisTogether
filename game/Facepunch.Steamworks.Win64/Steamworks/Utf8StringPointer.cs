using System;
using System.Text;

namespace Steamworks
{
	// Token: 0x020000C2 RID: 194
	internal struct Utf8StringPointer
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001280C File Offset: 0x00010A0C
		public unsafe static implicit operator string(Utf8StringPointer p)
		{
			bool flag = p.ptr == IntPtr.Zero;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte* ptr = (byte*)((void*)p.ptr);
				int i;
				for (i = 0; i < 67108864; i++)
				{
					bool flag2 = ptr[i] == 0;
					if (flag2)
					{
						break;
					}
				}
				result = Encoding.UTF8.GetString(ptr, i);
			}
			return result;
		}

		// Token: 0x04000783 RID: 1923
		internal IntPtr ptr;
	}
}
