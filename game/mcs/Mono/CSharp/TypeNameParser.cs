using System;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x020002CF RID: 719
	internal struct TypeNameParser
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x000A83A4 File Offset: 0x000A65A4
		public static string Escape(string name)
		{
			if (name == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int i = 0;
			while (i < name.Length)
			{
				char value = name[i];
				switch (value)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					goto IL_4F;
				case '\'':
				case '(':
				case ')':
					goto IL_77;
				default:
					switch (value)
					{
					case '[':
					case '\\':
					case ']':
						goto IL_4F;
					default:
						goto IL_77;
					}
					break;
				}
				IL_82:
				i++;
				continue;
				IL_4F:
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(name, 0, i, name.Length + 3);
				}
				stringBuilder.Append("\\").Append(value);
				goto IL_82;
				IL_77:
				if (stringBuilder != null)
				{
					stringBuilder.Append(value);
					goto IL_82;
				}
				goto IL_82;
			}
			if (stringBuilder == null)
			{
				return name;
			}
			return stringBuilder.ToString();
		}
	}
}
