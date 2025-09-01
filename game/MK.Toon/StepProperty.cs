using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000029 RID: 41
	public class StepProperty : Property<int>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000313D File Offset: 0x0000133D
		public StepProperty(Uniform uniform, int minValue, int maxValue, string keyword, int keywordDisabled = 0) : base(uniform, new string[]
		{
			keyword
		})
		{
			this._keywordDisabled = keywordDisabled;
			this._minValue = minValue;
			this._maxValue = maxValue;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003167 File Offset: 0x00001367
		public StepProperty(Uniform uniform, int minValue, int maxValue) : base(uniform, Array.Empty<string>())
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003183 File Offset: 0x00001383
		public override int GetValue(Material material)
		{
			return material.GetInt(this._uniform.id);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003196 File Offset: 0x00001396
		public override void SetValue(Material material, int value)
		{
			value = Mathf.Clamp(value, this._minValue, this._maxValue);
			material.SetInt(this._uniform.id, value);
			base.SetKeyword(material, value != this._keywordDisabled, value);
		}

		// Token: 0x04000186 RID: 390
		private int _keywordDisabled;

		// Token: 0x04000187 RID: 391
		private int _minValue;

		// Token: 0x04000188 RID: 392
		private int _maxValue;
	}
}
