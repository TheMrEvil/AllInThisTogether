using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000012 RID: 18
	public class Vector3IntParser : BasicCachedQcParser<Vector3Int>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override Vector3Int Parse(string value)
		{
			string[] array = value.Split(',', StringSplitOptions.None);
			Vector3Int vector3Int = default(Vector3Int);
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ParserInputException("Cannot parse '" + value + "' as an int vector, the format must be either x,y or x,y,z");
			}
			int i = 0;
			Vector3Int result;
			try
			{
				while (i < array.Length)
				{
					vector3Int[i] = int.Parse(array[i]);
					i++;
				}
				result = vector3Int;
			}
			catch
			{
				throw new ParserInputException("Cannot parse '" + array[i] + "' as it must be integral.");
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B88 File Offset: 0x00000D88
		public Vector3IntParser()
		{
		}
	}
}
