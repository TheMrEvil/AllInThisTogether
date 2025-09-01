using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform string validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200006B RID: 107
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the invalid characters for the property.</summary>
		/// <returns>The string that contains the set of characters that are not allowed for the property.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000A8ED File Offset: 0x00008AED
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		public string InvalidCharacters
		{
			get
			{
				return this.invalidCharacters;
			}
			set
			{
				this.invalidCharacters = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the maximum length allowed for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the maximum allowed length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.StringValidatorAttribute.MinLength" />.</exception>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000A905 File Offset: 0x00008B05
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000A90D File Offset: 0x00008B0D
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the minimum allowed value for the string to assign to the property.</summary>
		/// <returns>An integer that indicates the allowed minimum length for the string to assign to the property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.StringValidatorAttribute.MaxLength" />.</exception>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000A91D File Offset: 0x00008B1D
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000A925 File Offset: 0x00008B25
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.StringValidator" /> class.</summary>
		/// <returns>A current <see cref="T:System.Configuration.StringValidator" /> settings in a <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000A935 File Offset: 0x00008B35
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new StringValidator(this.minLength, this.maxLength, this.invalidCharacters);
				}
				return this.instance;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.StringValidatorAttribute" /> class.</summary>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A962 File Offset: 0x00008B62
		public StringValidatorAttribute()
		{
		}

		// Token: 0x0400014F RID: 335
		private string invalidCharacters;

		// Token: 0x04000150 RID: 336
		private int maxLength = int.MaxValue;

		// Token: 0x04000151 RID: 337
		private int minLength;

		// Token: 0x04000152 RID: 338
		private ConfigurationValidatorBase instance;
	}
}
