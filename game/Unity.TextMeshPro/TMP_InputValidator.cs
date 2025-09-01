using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public abstract class TMP_InputValidator : ScriptableObject
	{
		// Token: 0x0600032A RID: 810
		public abstract char Validate(ref string text, ref int pos, char ch);

		// Token: 0x0600032B RID: 811 RVA: 0x00022ABD File Offset: 0x00020CBD
		protected TMP_InputValidator()
		{
		}
	}
}
