using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002B RID: 43
	public class RangeProperty : Property<float>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003236 File Offset: 0x00001436
		public RangeProperty(Uniform uniform, string keyword, float minValue, float maxValue, float keywordDisabled = 0f) : base(uniform, new string[]
		{
			keyword
		})
		{
			this._keywordDisabled = keywordDisabled;
			this._minValue = minValue;
			this._maxValue = maxValue;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003260 File Offset: 0x00001460
		public RangeProperty(Uniform uniform, string keyword, float minValue, float keywordDisabled = 0f) : base(uniform, new string[]
		{
			keyword
		})
		{
			this._keywordDisabled = keywordDisabled;
			this._minValue = minValue;
			this._maxValue = float.PositiveInfinity;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000328D File Offset: 0x0000148D
		public RangeProperty(Uniform uniform, float minValue, float maxValue) : base(uniform, Array.Empty<string>())
		{
			this._minValue = minValue;
			this._maxValue = maxValue;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000032A9 File Offset: 0x000014A9
		public RangeProperty(Uniform uniform, float minValue) : base(uniform, Array.Empty<string>())
		{
			this._minValue = minValue;
			this._maxValue = float.PositiveInfinity;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000032C9 File Offset: 0x000014C9
		public override float GetValue(Material material)
		{
			return material.GetFloat(this._uniform.id);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000032DC File Offset: 0x000014DC
		public override void SetValue(Material material, float value)
		{
			value = Mathf.Clamp(value, this._minValue, this._maxValue);
			material.SetFloat(this._uniform.id, value);
			base.SetKeyword(material, value != this._keywordDisabled, (int)value);
		}

		// Token: 0x0400018A RID: 394
		private float _keywordDisabled;

		// Token: 0x0400018B RID: 395
		private float _minValue;

		// Token: 0x0400018C RID: 396
		private float _maxValue;
	}
}
