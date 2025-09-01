using System;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public struct DigitSettings
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027C0 File Offset: 0x000009C0
		public DigitSettings(float customDefault)
		{
			this.decimals = 0;
			this.decimalChar = ".";
			this.hideZeros = false;
			this.dotSeparation = false;
			this.dotDistance = 3;
			this.dotChar = ".";
			this.suffixShorten = false;
			this.suffixes = new List<string>
			{
				"K",
				"M",
				"B"
			};
			this.suffixDigits = new List<int>
			{
				3,
				3,
				3
			};
			this.maxDigits = 4;
		}

		// Token: 0x04000023 RID: 35
		[Header("Decimals:")]
		[Range(0f, 3f)]
		[Tooltip("Amount of digits visible after the dot.")]
		public int decimals;

		// Token: 0x04000024 RID: 36
		[Tooltip("The character used for the dot.")]
		public string decimalChar;

		// Token: 0x04000025 RID: 37
		[Tooltip("If true zeros at the end of the number will be hidden.")]
		public bool hideZeros;

		// Token: 0x04000026 RID: 38
		[Header("Dots:")]
		[Tooltip("Separates the number with dots.")]
		public bool dotSeparation;

		// Token: 0x04000027 RID: 39
		[Tooltip("Amount of digits between each dot.")]
		public int dotDistance;

		// Token: 0x04000028 RID: 40
		[Tooltip("The character used for the dot.")]
		public string dotChar;

		// Token: 0x04000029 RID: 41
		[Header("Suffix Shorten:")]
		[Tooltip("Shortens a number like 10000 to 10K.")]
		public bool suffixShorten;

		// Token: 0x0400002A RID: 42
		[Tooltip("List of suffixes.")]
		public List<string> suffixes;

		// Token: 0x0400002B RID: 43
		[Tooltip("Corresponding list of how many digits a suffix shortens.  Keep both lists at the same size.")]
		public List<int> suffixDigits;

		// Token: 0x0400002C RID: 44
		[Tooltip("Maximum of visible digits.  If number has more digits than this it will be shortened.")]
		public int maxDigits;
	}
}
