using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000069 RID: 105
	internal interface ITransform
	{
		// Token: 0x06000158 RID: 344
		void GetUsedTransform(HashSet<Transform> transformSet);

		// Token: 0x06000159 RID: 345
		void ReplaceTransform(Dictionary<int, Transform> replaceDict);
	}
}
