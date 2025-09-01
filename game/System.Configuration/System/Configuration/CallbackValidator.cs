using System;

namespace System.Configuration
{
	/// <summary>Provides dynamic validation of an object.</summary>
	// Token: 0x0200000C RID: 12
	public sealed class CallbackValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CallbackValidator" /> class.</summary>
		/// <param name="type">The type of object that will be validated.</param>
		/// <param name="callback">The <see cref="T:System.Configuration.ValidatorCallback" /> used as the delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x0600001A RID: 26 RVA: 0x000022C6 File Offset: 0x000004C6
		public CallbackValidator(Type type, ValidatorCallback callback)
		{
			this.type = type;
			this.callback = callback;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see langword="type" /> parameter matches the type used as the first parameter when creating an instance of <see cref="T:System.Configuration.CallbackValidator" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600001B RID: 27 RVA: 0x000022DC File Offset: 0x000004DC
		public override bool CanValidate(Type type)
		{
			return type == this.type;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x0600001C RID: 28 RVA: 0x000022EA File Offset: 0x000004EA
		public override void Validate(object value)
		{
			this.callback(value);
		}

		// Token: 0x0400002F RID: 47
		private Type type;

		// Token: 0x04000030 RID: 48
		private ValidatorCallback callback;
	}
}
