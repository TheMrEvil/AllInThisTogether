using System;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	public sealed class ExitGUIException : Exception
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CA39 File Offset: 0x0000AC39
		internal ExitGUIException(string message) : base(message)
		{
			GUIUtility.guiIsExiting = true;
		}
	}
}
