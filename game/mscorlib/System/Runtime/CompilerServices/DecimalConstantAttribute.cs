using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Stores the value of a <see cref="T:System.Decimal" /> constant in metadata. This class cannot be inherited.</summary>
	// Token: 0x020007ED RID: 2029
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> class with the specified unsigned integer values.</summary>
		/// <param name="scale">The power of 10 scaling factor that indicates the number of digits to the right of the decimal point. Valid values are 0 through 28 inclusive.</param>
		/// <param name="sign">A value of 0 indicates a positive value, and a value of 1 indicates a negative value.</param>
		/// <param name="hi">The high 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="mid">The middle 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="low">The low 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> &gt; 28.</exception>
		// Token: 0x060045F5 RID: 17909 RVA: 0x000E5770 File Offset: 0x000E3970
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this._dec = new decimal((int)low, (int)mid, (int)hi, sign > 0, scale);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DecimalConstantAttribute" /> class with the specified signed integer values.</summary>
		/// <param name="scale">The power of 10 scaling factor that indicates the number of digits to the right of the decimal point. Valid values are 0 through 28 inclusive.</param>
		/// <param name="sign">A value of 0 indicates a positive value, and a value of 1 indicates a negative value.</param>
		/// <param name="hi">The high 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="mid">The middle 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		/// <param name="low">The low 32 bits of the 96-bit <see cref="P:System.Runtime.CompilerServices.DecimalConstantAttribute.Value" />.</param>
		// Token: 0x060045F6 RID: 17910 RVA: 0x000E5770 File Offset: 0x000E3970
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this._dec = new decimal(low, mid, hi, sign > 0, scale);
		}

		/// <summary>Gets the decimal constant stored in this attribute.</summary>
		/// <returns>The decimal constant stored in this attribute.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000E578D File Offset: 0x000E398D
		public decimal Value
		{
			get
			{
				return this._dec;
			}
		}

		// Token: 0x04002D37 RID: 11575
		private decimal _dec;
	}
}
