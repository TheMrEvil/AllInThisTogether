using System;

namespace System.Configuration
{
	/// <summary>Serves as the base class for the <see cref="N:System.Configuration" /> validator attribute types.</summary>
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Property)]
	public class ConfigurationValidatorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class.</summary>
		// Token: 0x060001EC RID: 492 RVA: 0x000020A8 File Offset: 0x000002A8
		protected ConfigurationValidatorAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" /> class using the specified validator type.</summary>
		/// <param name="validator">The validator type to use when creating an instance of <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="validator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="validator" /> is not derived from <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</exception>
		// Token: 0x060001ED RID: 493 RVA: 0x00007378 File Offset: 0x00005578
		public ConfigurationValidatorAttribute(Type validator)
		{
			this.validatorType = validator;
		}

		/// <summary>Gets the validator attribute instance.</summary>
		/// <returns>The current <see cref="T:System.Configuration.ConfigurationValidatorBase" />.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007387 File Offset: 0x00005587
		public virtual ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = (ConfigurationValidatorBase)Activator.CreateInstance(this.validatorType);
				}
				return this.instance;
			}
		}

		/// <summary>Gets the type of the validator attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the current validator attribute instance.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000073AD File Offset: 0x000055AD
		public Type ValidatorType
		{
			get
			{
				return this.validatorType;
			}
		}

		// Token: 0x040000D5 RID: 213
		private Type validatorType;

		// Token: 0x040000D6 RID: 214
		private ConfigurationValidatorBase instance;
	}
}
