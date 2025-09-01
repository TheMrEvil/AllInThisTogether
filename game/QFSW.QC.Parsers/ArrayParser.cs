using System;
using System.Collections;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000002 RID: 2
	public class ArrayParser : IQcParser
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public int Priority
		{
			get
			{
				return -100;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002054 File Offset: 0x00000254
		public bool CanParse(Type type)
		{
			return type.IsArray;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			Type elementType = type.GetElementType();
			string[] array = value.ReduceScope('[', ']').SplitScoped(',');
			IList list = Array.CreateInstance(elementType, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list[i] = recursiveParser(array[i], elementType);
			}
			return list;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AC File Offset: 0x000002AC
		public ArrayParser()
		{
		}
	}
}
