using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.IntegerValidatorAttribute" /> class.</summary>
		// Token: 0x06000258 RID: 600 RVA: 0x00007C67 File Offset: 0x00005E67
		public IntegerValidatorAttribute()
		{
		}

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007C6F File Offset: 0x00005E6F
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00007C77 File Offset: 0x00005E77
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

		/// <summary>Gets or sets the maximum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" />.</exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00007C87 File Offset: 0x00005E87
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00007C8F File Offset: 0x00005E8F
		public int MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007C9F File Offset: 0x00005E9F
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00007CA7 File Offset: 0x00005EA7
		public int MinValue
		{
			get
			{
				return this.minValue;
			}
			set
			{
				this.minValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00007CB7 File Offset: 0x00005EB7
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new IntegerValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x040000F1 RID: 241
		private bool excludeRange;

		// Token: 0x040000F2 RID: 242
		private int maxValue;

		// Token: 0x040000F3 RID: 243
		private int minValue;

		// Token: 0x040000F4 RID: 244
		private ConfigurationValidatorBase instance;
	}
}
