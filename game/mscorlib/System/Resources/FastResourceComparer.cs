using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000864 RID: 2148
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06004760 RID: 18272 RVA: 0x000E87C5 File Offset: 0x000E69C5
		public int GetHashCode(object key)
		{
			return FastResourceComparer.HashFunction((string)key);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000E87D2 File Offset: 0x000E69D2
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000E87DC File Offset: 0x000E69DC
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num ^ (uint)key[i]);
			}
			return (int)num;
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000E8810 File Offset: 0x000E6A10
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string strA = (string)a;
			string strB = (string)b;
			return string.CompareOrdinal(strA, strB);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x0004196D File Offset: 0x0003FB6D
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x00041976 File Offset: 0x0003FB76
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000E8838 File Offset: 0x000E6A38
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string a2 = (string)a;
			string b2 = (string)b;
			return string.Equals(a2, b2);
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000E8860 File Offset: 0x000E6A60
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | (int)ptr2[1] << 8;
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000E88E6 File Offset: 0x000E6AE6
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000E88F4 File Offset: 0x000E6AF4
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				num = (int)((char)((int)(*(a++)) | (int)(*(a++)) << 8) - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0000259F File Offset: 0x0000079F
		public FastResourceComparer()
		{
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000E8950 File Offset: 0x000E6B50
		// Note: this type is marked as 'beforefieldinit'.
		static FastResourceComparer()
		{
		}

		// Token: 0x04002DCB RID: 11723
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
