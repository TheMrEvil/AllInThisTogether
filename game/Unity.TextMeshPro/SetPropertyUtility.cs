using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000040 RID: 64
	internal static class SetPropertyUtility
	{
		// Token: 0x06000326 RID: 806 RVA: 0x000229E4 File Offset: 0x00020BE4
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00022A33 File Offset: 0x00020C33
		public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00022A4E File Offset: 0x00020C4E
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00022A70 File Offset: 0x00020C70
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
