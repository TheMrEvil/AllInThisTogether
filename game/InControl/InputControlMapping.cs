using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class InputControlMapping
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000577C File Offset: 0x0000397C
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000057B1 File Offset: 0x000039B1
		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(this.name))
				{
					return this.name;
				}
				return this.Target.ToString();
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000057BA File Offset: 0x000039BA
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000057C2 File Offset: 0x000039C2
		public bool Invert
		{
			get
			{
				return this.invert;
			}
			set
			{
				this.invert = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000057CB File Offset: 0x000039CB
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000057D3 File Offset: 0x000039D3
		public float Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000057DC File Offset: 0x000039DC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000057E4 File Offset: 0x000039E4
		public bool Raw
		{
			get
			{
				return this.raw;
			}
			set
			{
				this.raw = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000057ED File Offset: 0x000039ED
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000057F5 File Offset: 0x000039F5
		public bool Passive
		{
			get
			{
				return this.passive;
			}
			set
			{
				this.passive = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000057FE File Offset: 0x000039FE
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005806 File Offset: 0x00003A06
		public bool IgnoreInitialZeroValue
		{
			get
			{
				return this.ignoreInitialZeroValue;
			}
			set
			{
				this.ignoreInitialZeroValue = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000580F File Offset: 0x00003A0F
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005817 File Offset: 0x00003A17
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005825 File Offset: 0x00003A25
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000582D File Offset: 0x00003A2D
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000583B File Offset: 0x00003A3B
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005843 File Offset: 0x00003A43
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005851 File Offset: 0x00003A51
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005859 File Offset: 0x00003A59
		public InputControlSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005862 File Offset: 0x00003A62
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000586A File Offset: 0x00003A6A
		public InputControlType Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005873 File Offset: 0x00003A73
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000587B File Offset: 0x00003A7B
		public InputRangeType SourceRange
		{
			get
			{
				return this.sourceRange;
			}
			set
			{
				this.sourceRange = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005884 File Offset: 0x00003A84
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000588C File Offset: 0x00003A8C
		public InputRangeType TargetRange
		{
			get
			{
				return this.targetRange;
			}
			set
			{
				this.targetRange = value;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005898 File Offset: 0x00003A98
		public float ApplyToValue(float value)
		{
			if (this.Raw)
			{
				value *= this.Scale;
				value = (InputRange.Excludes(this.sourceRange, value) ? 0f : value);
			}
			else
			{
				value = Mathf.Clamp(value * this.Scale, -1f, 1f);
				value = InputRange.Remap(value, this.sourceRange, this.targetRange);
			}
			if (this.Invert)
			{
				value = -value;
			}
			return value;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000590C File Offset: 0x00003B0C
		public InputControlMapping()
		{
		}

		// Token: 0x04000145 RID: 325
		[SerializeField]
		private string name = "";

		// Token: 0x04000146 RID: 326
		[SerializeField]
		private bool invert;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		private float scale = 1f;

		// Token: 0x04000148 RID: 328
		[SerializeField]
		private bool raw;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		private bool passive;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		private bool ignoreInitialZeroValue;

		// Token: 0x0400014B RID: 331
		[SerializeField]
		private float sensitivity = 1f;

		// Token: 0x0400014C RID: 332
		[SerializeField]
		private float lowerDeadZone;

		// Token: 0x0400014D RID: 333
		[SerializeField]
		private float upperDeadZone = 1f;

		// Token: 0x0400014E RID: 334
		[SerializeField]
		private InputControlSource source;

		// Token: 0x0400014F RID: 335
		[SerializeField]
		private InputControlType target;

		// Token: 0x04000150 RID: 336
		[SerializeField]
		private InputRangeType sourceRange = InputRangeType.MinusOneToOne;

		// Token: 0x04000151 RID: 337
		[SerializeField]
		private InputRangeType targetRange = InputRangeType.MinusOneToOne;
	}
}
