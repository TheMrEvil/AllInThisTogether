using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000028 RID: 40
	internal static class DropdownUtility
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000054E0 File Offset: 0x000036E0
		internal static IGenericMenu CreateDropdown()
		{
			IGenericMenu result;
			if (DropdownUtility.MakeDropdownFunc == null)
			{
				IGenericMenu genericMenu = new GenericDropdownMenu();
				result = genericMenu;
			}
			else
			{
				result = DropdownUtility.MakeDropdownFunc();
			}
			return result;
		}

		// Token: 0x0400006B RID: 107
		internal static Func<IGenericMenu> MakeDropdownFunc;
	}
}
