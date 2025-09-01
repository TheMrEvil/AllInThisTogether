using System;

namespace System
{
	// Token: 0x0200016A RID: 362
	internal class UncNameHelper
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0000219B File Offset: 0x0000039B
		private UncNameHelper()
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002B3BE File Offset: 0x000295BE
		internal static string ParseCanonicalName(string str, int start, int end, ref bool loopback)
		{
			return DomainNameHelper.ParseCanonicalName(str, start, end, ref loopback);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002B3CC File Offset: 0x000295CC
		internal unsafe static bool IsValid(char* name, ushort start, ref int returnedEnd, bool notImplicitFile)
		{
			ushort num = (ushort)returnedEnd;
			if (start == num)
			{
				return false;
			}
			bool flag = false;
			ushort num2;
			for (num2 = start; num2 < num; num2 += 1)
			{
				if (name[num2] == '/' || name[num2] == '\\' || (notImplicitFile && (name[num2] == ':' || name[num2] == '?' || name[num2] == '#')))
				{
					num = num2;
					break;
				}
				if (name[num2] == '.')
				{
					num2 += 1;
					break;
				}
				if (char.IsLetter(name[num2]) || name[num2] == '-' || name[num2] == '_')
				{
					flag = true;
				}
				else if (name[num2] < '0' || name[num2] > '9')
				{
					return false;
				}
			}
			if (!flag)
			{
				return false;
			}
			while (num2 < num)
			{
				if (name[num2] == '/' || name[num2] == '\\' || (notImplicitFile && (name[num2] == ':' || name[num2] == '?' || name[num2] == '#')))
				{
					num = num2;
					break;
				}
				if (name[num2] == '.')
				{
					if (!flag || (num2 - 1 >= start && name[num2 - 1] == '.'))
					{
						return false;
					}
					flag = false;
				}
				else if (name[num2] == '-' || name[num2] == '_')
				{
					if (!flag)
					{
						return false;
					}
				}
				else
				{
					if (!char.IsLetter(name[num2]) && (name[num2] < '0' || name[num2] > '9'))
					{
						return false;
					}
					if (!flag)
					{
						flag = true;
					}
				}
				num2 += 1;
			}
			if (num2 - 1 >= start && name[num2 - 1] == '.')
			{
				flag = true;
			}
			if (!flag)
			{
				return false;
			}
			returnedEnd = (int)num;
			return true;
		}

		// Token: 0x04000681 RID: 1665
		internal const int MaximumInternetNameLength = 256;
	}
}
