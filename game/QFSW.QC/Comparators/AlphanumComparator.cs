using System;
using System.Collections.Generic;

namespace QFSW.QC.Comparators
{
	// Token: 0x0200006C RID: 108
	public class AlphanumComparator : IComparer<string>
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public unsafe int Compare(string x, string y)
		{
			if (x == null)
			{
				return 0;
			}
			if (y == null)
			{
				return 0;
			}
			int length = x.Length;
			int length2 = y.Length;
			if (length + length2 + 2 <= 512)
			{
				checked
				{
					char* buffer = stackalloc char[unchecked((UIntPtr)(length + 1)) * 2];
					char* buffer2 = stackalloc char[unchecked((UIntPtr)(length2 + 1)) * 2];
					return this.Compare(x, buffer, length, y, buffer2, length2);
				}
			}
			char[] array = new char[length + 1];
			char[] array2 = new char[length2 + 1];
			fixed (char[] array3 = array)
			{
				char* buffer3;
				if (array == null || array3.Length == 0)
				{
					buffer3 = null;
				}
				else
				{
					buffer3 = &array3[0];
				}
				char[] array4;
				char* buffer4;
				if ((array4 = array2) == null || array4.Length == 0)
				{
					buffer4 = null;
				}
				else
				{
					buffer4 = &array4[0];
				}
				return this.Compare(x, buffer3, length, y, buffer4, length2);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public unsafe int Compare(string x, char* buffer1, int len1, string y, char* buffer2, int len2)
		{
			int num = 0;
			int num2 = 0;
			while (num < len1 && num2 < len2)
			{
				char c = x[num];
				char c2 = y[num2];
				int num3 = 0;
				int num4 = 0;
				do
				{
					buffer1[num3++] = c;
					num++;
					if (num >= len1)
					{
						break;
					}
					c = x[num];
				}
				while (char.IsDigit(c) == char.IsDigit(*buffer1));
				do
				{
					buffer2[num4++] = c2;
					num2++;
					if (num2 >= len2)
					{
						break;
					}
					c2 = y[num2];
				}
				while (char.IsDigit(c2) == char.IsDigit(*buffer2));
				buffer1[num3] = (buffer2[num4] = '\0');
				int num7;
				if (char.IsDigit(*buffer1) && char.IsDigit(*buffer2))
				{
					int num5 = this.ParseInt(buffer1);
					int num6 = this.ParseInt(buffer2);
					num7 = num5 - num6;
				}
				else
				{
					num7 = this.CompareStrings(buffer1, buffer2);
				}
				if (num7 != 0)
				{
					return num7;
				}
			}
			return len1 - len2;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A490 File Offset: 0x00008690
		private unsafe int ParseInt(char* buffer)
		{
			int num = 0;
			while (*buffer != '\0')
			{
				num *= 10;
				num += (int)(*(buffer++) - '0');
			}
			return num;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A4B8 File Offset: 0x000086B8
		private unsafe int CompareStrings(char* buffer1, char* buffer2)
		{
			int num = 0;
			while (buffer1[num] != '\0' && buffer2[num] != '\0')
			{
				char c = buffer1[num];
				char c2 = buffer2[num++];
				if (c > c2)
				{
					return 1;
				}
				if (c < c2)
				{
					return -1;
				}
			}
			if (buffer1[num] != '\0')
			{
				return 1;
			}
			if (buffer2[num] != '\0')
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A512 File Offset: 0x00008712
		public AlphanumComparator()
		{
		}

		// Token: 0x0400014B RID: 331
		private const int MaxStackSize = 512;
	}
}
