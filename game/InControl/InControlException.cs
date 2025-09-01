using System;

namespace InControl
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class InControlException : Exception
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000068CB File Offset: 0x00004ACB
		public InControlException()
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000068D3 File Offset: 0x00004AD3
		public InControlException(string message) : base(message)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000068DC File Offset: 0x00004ADC
		public InControlException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
