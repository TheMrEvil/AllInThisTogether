using System;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000009 RID: 9
	public class GameObjectParser : BasicQcParser<GameObject>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026A0 File Offset: 0x000008A0
		public override GameObject Parse(string value)
		{
			GameObject gameObject = GameObjectExtensions.Find(base.ParseRecursive<string>(value), true);
			if (!gameObject)
			{
				throw new ParserInputException("Could not find GameObject of name " + value + ".");
			}
			return gameObject;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026CD File Offset: 0x000008CD
		public GameObjectParser()
		{
		}
	}
}
