using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000035 RID: 53
	internal static class SetPropertyUtility
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013D03 File Offset: 0x00011F03
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013D24 File Offset: 0x00011F24
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
