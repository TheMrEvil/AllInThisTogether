using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an object. This class cannot be inherited.</summary>
	// Token: 0x0200003D RID: 61
	public sealed class DefaultValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DefaultValidator" /> class.</summary>
		// Token: 0x0600021F RID: 543 RVA: 0x00007745 File Offset: 0x00005945
		public DefaultValidator()
		{
		}

		/// <summary>Determines whether an object can be validated, based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> for all types being validated.</returns>
		// Token: 0x06000220 RID: 544 RVA: 0x00004919 File Offset: 0x00002B19
		public override bool CanValidate(Type type)
		{
			return true;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		// Token: 0x06000221 RID: 545 RVA: 0x000023B9 File Offset: 0x000005B9
		public override void Validate(object value)
		{
		}
	}
}
