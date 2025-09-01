using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SubclassTypeValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidatorAttribute" /> class.</summary>
		/// <param name="baseClass">The base class type.</param>
		// Token: 0x060003BC RID: 956 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		public SubclassTypeValidatorAttribute(Type baseClass)
		{
			this.baseClass = baseClass;
		}

		/// <summary>Gets the base type of the object being validated.</summary>
		/// <returns>The base type of the object being validated.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public Type BaseClass
		{
			get
			{
				return this.baseClass;
			}
		}

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" /> instance.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new SubclassTypeValidator(this.baseClass);
				}
				return this.instance;
			}
		}

		// Token: 0x04000154 RID: 340
		private Type baseClass;

		// Token: 0x04000155 RID: 341
		private ConfigurationValidatorBase instance;
	}
}
