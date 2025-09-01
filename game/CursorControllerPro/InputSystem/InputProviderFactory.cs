using System;

namespace SlimUI.CursorControllerPro.InputSystem
{
	// Token: 0x02000012 RID: 18
	internal class InputProviderFactory
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003C06 File Offset: 0x00001E06
		public static IInputProvider GetInputProvider()
		{
			return new DefaultInputProvider();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003C0D File Offset: 0x00001E0D
		public InputProviderFactory()
		{
		}
	}
}
