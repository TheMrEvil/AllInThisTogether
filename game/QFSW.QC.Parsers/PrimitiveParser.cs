using System;
using System.Collections.Generic;
using System.Globalization;
using QFSW.QC.Utilities;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000B RID: 11
	public class PrimitiveParser : IQcParser
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002719 File Offset: 0x00000919
		public int Priority
		{
			get
			{
				return -1000;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002720 File Offset: 0x00000920
		public bool CanParse(Type type)
		{
			return this._primitiveTypes.Contains(type);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002730 File Offset: 0x00000930
		public object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			object result;
			try
			{
				result = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
			}
			catch (FormatException innerException)
			{
				throw new ParserInputException(string.Concat(new string[]
				{
					"Cannot parse '",
					value,
					"' to the type '",
					type.GetDisplayName(false),
					"'."
				}), innerException);
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002798 File Offset: 0x00000998
		public PrimitiveParser()
		{
		}

		// Token: 0x04000004 RID: 4
		private readonly HashSet<Type> _primitiveTypes = new HashSet<Type>
		{
			typeof(int),
			typeof(float),
			typeof(decimal),
			typeof(double),
			typeof(bool),
			typeof(byte),
			typeof(sbyte),
			typeof(uint),
			typeof(short),
			typeof(ushort),
			typeof(long),
			typeof(ulong),
			typeof(char)
		};
	}
}
