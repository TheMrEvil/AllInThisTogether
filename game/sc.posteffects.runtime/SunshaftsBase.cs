using System;
using UnityEngine;

namespace SCPE
{
	// Token: 0x02000047 RID: 71
	public class SunshaftsBase
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00007A58 File Offset: 0x00005C58
		public static void AddShaftCaster()
		{
			GameObject gameObject = null;
			if (GameObject.Find("Directional Light"))
			{
				gameObject = GameObject.Find("Directional Light");
			}
			if (!gameObject && GameObject.Find("Directional light"))
			{
				gameObject = GameObject.Find("Directional light");
			}
			if (!gameObject)
			{
				Debug.LogError("<b>Sunshafts:</b> No object with the name 'Directional Light' or 'Directional light' could be found");
				return;
			}
			SunshaftCaster sunshaftCaster = gameObject.GetComponent<SunshaftCaster>();
			if (!sunshaftCaster)
			{
				sunshaftCaster = gameObject.AddComponent<SunshaftCaster>();
				Debug.Log("\"SunshaftCaster\" component was added to the <b>" + sunshaftCaster.gameObject.name + "</b> GameObject", sunshaftCaster.gameObject);
			}
			if (!sunshaftCaster.enabled)
			{
				sunshaftCaster.enabled = true;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007B04 File Offset: 0x00005D04
		public SunshaftsBase()
		{
		}

		// Token: 0x02000080 RID: 128
		public enum BlendMode
		{
			// Token: 0x040001D2 RID: 466
			Additive,
			// Token: 0x040001D3 RID: 467
			Screen
		}

		// Token: 0x02000081 RID: 129
		public enum SunShaftsResolution
		{
			// Token: 0x040001D5 RID: 469
			High = 1,
			// Token: 0x040001D6 RID: 470
			Normal,
			// Token: 0x040001D7 RID: 471
			Low
		}

		// Token: 0x02000082 RID: 130
		public enum Pass
		{
			// Token: 0x040001D9 RID: 473
			SkySource,
			// Token: 0x040001DA RID: 474
			RadialBlur,
			// Token: 0x040001DB RID: 475
			Blend
		}
	}
}
