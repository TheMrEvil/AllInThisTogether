using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200022D RID: 557
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x00026F00 File Offset: 0x00025100
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x04000835 RID: 2101
		internal float m_Seconds;
	}
}
