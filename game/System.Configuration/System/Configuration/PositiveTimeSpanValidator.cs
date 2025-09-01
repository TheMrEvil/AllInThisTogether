using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000056 RID: 86
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidator" /> class.</summary>
		// Token: 0x060002DF RID: 735 RVA: 0x00007745 File Offset: 0x00005945
		public PositiveTimeSpanValidator()
		{
		}

		/// <summary>Determines whether the object type can be validated.</summary>
		/// <param name="type">The object type.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002E0 RID: 736 RVA: 0x000087A7 File Offset: 0x000069A7
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> cannot be resolved as a positive <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x060002E1 RID: 737 RVA: 0x000087B9 File Offset: 0x000069B9
		public override void Validate(object value)
		{
			if ((TimeSpan)value <= new TimeSpan(0L))
			{
				throw new ArgumentException("The time span value must be positive");
			}
		}
	}
}
