using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DamageNumbersPro.Demo
{
	// Token: 0x0200001A RID: 26
	public class DNP_PrefabSettings : MonoBehaviour
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00007B24 File Offset: 0x00005D24
		public void Apply(DamageNumber target)
		{
			if (this.texts != null && this.texts.Count > 0)
			{
				int num = UnityEngine.Random.Range(0, this.texts.Count);
				target.leftText = this.texts[num];
				if (this.fonts != null && num < this.fonts.Count)
				{
					target.SetFontMaterial(this.fonts[num]);
				}
				if (this.texts.Count > 1)
				{
					target.enableNumber = false;
				}
			}
			if (this.randomColor)
			{
				target.SetColor(Color.HSVToRGB(UnityEngine.Random.value, 0.5f, 1f));
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007BCA File Offset: 0x00005DCA
		public DNP_PrefabSettings()
		{
		}

		// Token: 0x0400015E RID: 350
		public int damage = 1;

		// Token: 0x0400015F RID: 351
		public int numberRange = 100;

		// Token: 0x04000160 RID: 352
		public List<string> texts;

		// Token: 0x04000161 RID: 353
		public List<TMP_FontAsset> fonts;

		// Token: 0x04000162 RID: 354
		public bool randomColor;
	}
}
