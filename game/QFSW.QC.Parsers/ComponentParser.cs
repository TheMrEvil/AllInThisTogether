using System;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000006 RID: 6
	public class ComponentParser : PolymorphicQcParser<Component>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000254C File Offset: 0x0000074C
		public override Component Parse(string value, Type type)
		{
			Component component = base.ParseRecursive<GameObject>(value).GetComponent(type);
			if (!component)
			{
				throw new ParserInputException(string.Concat(new string[]
				{
					"No component on the object '",
					value,
					"' of type ",
					type.GetDisplayName(false),
					" existed."
				}));
			}
			return component;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025A7 File Offset: 0x000007A7
		public ComponentParser()
		{
		}
	}
}
