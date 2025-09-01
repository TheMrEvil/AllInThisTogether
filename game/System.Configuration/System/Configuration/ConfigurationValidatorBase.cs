using System;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving a validation class so that a value of an object can be verified.</summary>
	// Token: 0x02000037 RID: 55
	public abstract class ConfigurationValidatorBase
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ConfigurationValidatorBase" /> class.</summary>
		// Token: 0x060001F0 RID: 496 RVA: 0x00002050 File Offset: 0x00000250
		protected ConfigurationValidatorBase()
		{
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter value matches the expected <see langword="type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001F1 RID: 497 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual bool CanValidate(Type type)
		{
			return false;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		// Token: 0x060001F2 RID: 498
		public abstract void Validate(object value);
	}
}
