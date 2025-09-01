using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000014 RID: 20
	public class Vector4Parser : BasicCachedQcParser<Vector4>
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public override Vector4 Parse(string value)
		{
			string[] array = value.SplitScoped(',');
			Vector4 result = default(Vector4);
			if (array.Length < 2 || array.Length > 4)
			{
				throw new ParserInputException("Cannot parse '" + value + "' as a vector, the format must be either x,y x,y,z or x,y,z,w.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = base.ParseRecursive<float>(array[i]);
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C08 File Offset: 0x00000E08
		public Vector4Parser()
		{
		}
	}
}
