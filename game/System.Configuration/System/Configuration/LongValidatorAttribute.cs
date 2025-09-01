using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform long-integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LongValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidatorAttribute" /> class.</summary>
		// Token: 0x060002C5 RID: 709 RVA: 0x00007C67 File Offset: 0x00005E67
		public LongValidatorAttribute()
		{
		}

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>
		///   <see langword="true" /> if the value must be excluded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000860F File Offset: 0x0000680F
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00008617 File Offset: 0x00006817
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
		/// <returns>A long integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" />.</exception>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008627 File Offset: 0x00006827
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000862F File Offset: 0x0000682F
		public long MaxValue
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
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000863F File Offset: 0x0000683F
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00008647 File Offset: 0x00006847
		public long MinValue
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

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008657 File Offset: 0x00006857
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new LongValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x04000107 RID: 263
		private bool excludeRange;

		// Token: 0x04000108 RID: 264
		private long maxValue;

		// Token: 0x04000109 RID: 265
		private long minValue;

		// Token: 0x0400010A RID: 266
		private ConfigurationValidatorBase instance;
	}
}
