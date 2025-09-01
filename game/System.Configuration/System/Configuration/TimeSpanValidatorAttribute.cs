using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the relative maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents less than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValue" />.</exception>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000AD38 File Offset: 0x00008F38
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000AD40 File Offset: 0x00008F40
		public string MaxValueString
		{
			get
			{
				return this.maxValueString;
			}
			set
			{
				this.maxValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the relative minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The minimum allowed <see cref="T:System.TimeSpan" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents more than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000AD50 File Offset: 0x00008F50
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000AD58 File Offset: 0x00008F58
		public string MinValueString
		{
			get
			{
				return this.minValueString;
			}
			set
			{
				this.minValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets the absolute maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000AD68 File Offset: 0x00008F68
		public TimeSpan MaxValue
		{
			get
			{
				return TimeSpan.Parse(this.maxValueString);
			}
		}

		/// <summary>Gets the absolute minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed minimum <see cref="T:System.TimeSpan" /> value.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000AD75 File Offset: 0x00008F75
		public TimeSpan MinValue
		{
			get
			{
				return TimeSpan.Parse(this.minValueString);
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range as defined by <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValueString" /> and <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValueString" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000AD82 File Offset: 0x00008F82
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000AD8A File Offset: 0x00008F8A
		public bool ExcludeRange
		{
			get
			{
				return this.excludeRange;
			}
			set
			{
				this.excludeRange = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000AD9A File Offset: 0x00008F9A
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new TimeSpanValidator(this.MinValue, this.MaxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidatorAttribute" /> class.</summary>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public TimeSpanValidatorAttribute()
		{
		}

		// Token: 0x0400015A RID: 346
		private bool excludeRange;

		// Token: 0x0400015B RID: 347
		private string maxValueString = "10675199.02:48:05.4775807";

		// Token: 0x0400015C RID: 348
		private string minValueString = "-10675199.02:48:05.4775808";

		/// <summary>Gets the absolute maximum value allowed.</summary>
		// Token: 0x0400015D RID: 349
		public const string TimeSpanMaxValue = "10675199.02:48:05.4775807";

		/// <summary>Gets the absolute minimum value allowed.</summary>
		// Token: 0x0400015E RID: 350
		public const string TimeSpanMinValue = "-10675199.02:48:05.4775808";

		// Token: 0x0400015F RID: 351
		private ConfigurationValidatorBase instance;
	}
}
