using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000646 RID: 1606
	public static class CompatibilitySwitch
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return false;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x0000AF5E File Offset: 0x0000915E
		public static string GetValue(string compatibilitySwitchName)
		{
			return null;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return null;
		}
	}
}
