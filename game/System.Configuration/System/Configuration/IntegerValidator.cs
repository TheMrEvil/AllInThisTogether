using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an <see cref="T:System.Int32" /> value.</summary>
	// Token: 0x02000047 RID: 71
	public class IntegerValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum length of the integer value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum length of the integer value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int32" /> object that specifies a value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is less than <see langword="0" />.  
		/// -or-
		///  <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x06000253 RID: 595 RVA: 0x00007B42 File Offset: 0x00005D42
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			if (minValue != 0)
			{
				this.minValue = minValue;
			}
			if (maxValue != 0)
			{
				this.maxValue = maxValue;
			}
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		/// <param name="rangeIsExclusive">
		///   <see langword="true" /> to specify that the validation range is exclusive. Inclusive means the value to be validated must be within the specified range; exclusive means that it must be below the minimum or above the maximum.</param>
		// Token: 0x06000254 RID: 596 RVA: 0x00007B78 File Offset: 0x00005D78
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		// Token: 0x06000255 RID: 597 RVA: 0x00007B84 File Offset: 0x00005D84
		public IntegerValidator(int minValue, int maxValue) : this(minValue, maxValue, false, 0)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <param name="type">The type of the object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="type" /> parameter matches an <see cref="T:System.Int32" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000256 RID: 598 RVA: 0x00007B90 File Offset: 0x00005D90
		public override bool CanValidate(Type type)
		{
			return type == typeof(int);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value to be validated.</param>
		// Token: 0x06000257 RID: 599 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public override void Validate(object value)
		{
			int num = (int)value;
			if (!this.rangeIsExclusive)
			{
				if (num < this.minValue || num > this.maxValue)
				{
					throw new ArgumentException("The value must be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
				}
			}
			else if (num >= this.minValue && num <= this.maxValue)
			{
				throw new ArgumentException("The value must not be in the range " + this.minValue.ToString() + " - " + this.maxValue.ToString());
			}
			if (this.resolution != 0 && num % this.resolution != 0)
			{
				throw new ArgumentException("The value must have a resolution of " + this.resolution.ToString());
			}
		}

		// Token: 0x040000ED RID: 237
		private bool rangeIsExclusive;

		// Token: 0x040000EE RID: 238
		private int minValue;

		// Token: 0x040000EF RID: 239
		private int maxValue = int.MaxValue;

		// Token: 0x040000F0 RID: 240
		private int resolution;
	}
}
