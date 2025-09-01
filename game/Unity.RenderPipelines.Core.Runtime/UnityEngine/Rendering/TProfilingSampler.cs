using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000070 RID: 112
	internal class TProfilingSampler<TEnum> : ProfilingSampler where TEnum : Enum
	{
		// Token: 0x0600039A RID: 922 RVA: 0x000112B0 File Offset: 0x0000F4B0
		static TProfilingSampler()
		{
			string[] names = Enum.GetNames(typeof(TEnum));
			Array values = Enum.GetValues(typeof(TEnum));
			for (int i = 0; i < names.Length; i++)
			{
				TProfilingSampler<TEnum> value = new TProfilingSampler<TEnum>(names[i]);
				TProfilingSampler<TEnum>.samples.Add((TEnum)((object)values.GetValue(i)), value);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011315 File Offset: 0x0000F515
		public TProfilingSampler(string name) : base(name)
		{
		}

		// Token: 0x0400024C RID: 588
		internal static Dictionary<TEnum, TProfilingSampler<TEnum>> samples = new Dictionary<TEnum, TProfilingSampler<TEnum>>();
	}
}
