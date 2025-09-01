using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000042 RID: 66
	internal class ImmediateModeException : Exception
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000829C File Offset: 0x0000649C
		public ImmediateModeException(Exception inner) : base("", inner)
		{
		}
	}
}
