using System;
using Boxophobic.StyledGUI;
using UnityEngine;

namespace Boxophobic.Utils
{
	// Token: 0x02000013 RID: 19
	[CreateAssetMenu(fileName = "Data", menuName = "BOXOPHOBIC/Settings Data")]
	public class SettingsData : StyledScriptableObject
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000252B File Offset: 0x0000072B
		public SettingsData()
		{
		}

		// Token: 0x04000028 RID: 40
		[StyledBanner(0.65f, 0.65f, 0.65f, "Settings Data")]
		public bool styledBanner;

		// Token: 0x04000029 RID: 41
		[Space]
		public string data = "";
	}
}
