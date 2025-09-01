using System;

namespace System.Configuration
{
	/// <summary>Validates that an object is a derived class of a specified type.</summary>
	// Token: 0x0200006C RID: 108
	public sealed class SubclassTypeValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidator" /> class.</summary>
		/// <param name="baseClass">The base class to validate against.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseClass" /> is <see langword="null" />.</exception>
		// Token: 0x060003B9 RID: 953 RVA: 0x0000A975 File Offset: 0x00008B75
		public SubclassTypeValidator(Type baseClass)
		{
			this.baseClass = baseClass;
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a type that can be validated; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003BA RID: 954 RVA: 0x0000A984 File Offset: 0x00008B84
		public override bool CanValidate(Type type)
		{
			return type == typeof(Type);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The object value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of a <see cref="T:System.Type" /> that can be derived from <paramref name="baseClass" /> as defined in the constructor.</exception>
		// Token: 0x060003BB RID: 955 RVA: 0x0000A998 File Offset: 0x00008B98
		public override void Validate(object value)
		{
			Type c = (Type)value;
			if (!this.baseClass.IsAssignableFrom(c))
			{
				throw new ArgumentException("The value must be a subclass");
			}
		}

		// Token: 0x04000153 RID: 339
		private Type baseClass;
	}
}
