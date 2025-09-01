using System;
using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents a complex number.</summary>
	// Token: 0x02000014 RID: 20
	[Serializable]
	public struct Complex : IEquatable<Complex>, IFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Numerics.Complex" /> structure using the specified real and imaginary values.</summary>
		/// <param name="real">The real part of the complex number.</param>
		/// <param name="imaginary">The imaginary part of the complex number.</param>
		// Token: 0x0600021D RID: 541 RVA: 0x0000FDCE File Offset: 0x0000DFCE
		public Complex(double real, double imaginary)
		{
			this.m_real = real;
			this.m_imaginary = imaginary;
		}

		/// <summary>Gets the real component of the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>The real component of a complex number.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000FDDE File Offset: 0x0000DFDE
		public double Real
		{
			get
			{
				return this.m_real;
			}
		}

		/// <summary>Gets the imaginary component of the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>The imaginary component of a complex number.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000FDE6 File Offset: 0x0000DFE6
		public double Imaginary
		{
			get
			{
				return this.m_imaginary;
			}
		}

		/// <summary>Gets the magnitude (or absolute value) of a complex number.</summary>
		/// <returns>The magnitude of the current instance.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000FDEE File Offset: 0x0000DFEE
		public double Magnitude
		{
			get
			{
				return Complex.Abs(this);
			}
		}

		/// <summary>Gets the phase of a complex number.</summary>
		/// <returns>The phase of a complex number, in radians.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000FDFB File Offset: 0x0000DFFB
		public double Phase
		{
			get
			{
				return Math.Atan2(this.m_imaginary, this.m_real);
			}
		}

		/// <summary>Creates a complex number from a point's polar coordinates.</summary>
		/// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
		/// <param name="phase">The phase, which is the angle from the line to the horizontal axis, measured in radians.</param>
		/// <returns>A complex number.</returns>
		// Token: 0x06000222 RID: 546 RVA: 0x0000FE0E File Offset: 0x0000E00E
		public static Complex FromPolarCoordinates(double magnitude, double phase)
		{
			return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
		}

		/// <summary>Returns the additive inverse of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The result of the <see cref="P:System.Numerics.Complex.Real" /> and <see cref="P:System.Numerics.Complex.Imaginary" /> components of the <paramref name="value" /> parameter multiplied by -1.</returns>
		// Token: 0x06000223 RID: 547 RVA: 0x0000FE25 File Offset: 0x0000E025
		public static Complex Negate(Complex value)
		{
			return -value;
		}

		/// <summary>Adds two complex numbers and returns the result.</summary>
		/// <param name="left">The first complex number to add.</param>
		/// <param name="right">The second complex number to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		// Token: 0x06000224 RID: 548 RVA: 0x0000FE2D File Offset: 0x0000E02D
		public static Complex Add(Complex left, Complex right)
		{
			return left + right;
		}

		/// <summary>Subtracts one complex number from another and returns the result.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		// Token: 0x06000225 RID: 549 RVA: 0x0000FE36 File Offset: 0x0000E036
		public static Complex Subtract(Complex left, Complex right)
		{
			return left - right;
		}

		/// <summary>Returns the product of two complex numbers.</summary>
		/// <param name="left">The first complex number to multiply.</param>
		/// <param name="right">The second complex number to multiply.</param>
		/// <returns>The product of the <paramref name="left" /> and <paramref name="right" /> parameters.</returns>
		// Token: 0x06000226 RID: 550 RVA: 0x0000FE3F File Offset: 0x0000E03F
		public static Complex Multiply(Complex left, Complex right)
		{
			return left * right;
		}

		/// <summary>Divides one complex number by another and returns the result.</summary>
		/// <param name="dividend">The complex number to be divided.</param>
		/// <param name="divisor">The complex number to divide by.</param>
		/// <returns>The quotient of the division.</returns>
		// Token: 0x06000227 RID: 551 RVA: 0x0000FE48 File Offset: 0x0000E048
		public static Complex Divide(Complex dividend, Complex divisor)
		{
			return dividend / divisor;
		}

		/// <summary>Returns the additive inverse of a specified complex number.</summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the <see cref="P:System.Numerics.Complex.Real" /> and <see cref="P:System.Numerics.Complex.Imaginary" /> components of the <paramref name="value" /> parameter multiplied by -1.</returns>
		// Token: 0x06000228 RID: 552 RVA: 0x0000FE51 File Offset: 0x0000E051
		public static Complex operator -(Complex value)
		{
			return new Complex(-value.m_real, -value.m_imaginary);
		}

		/// <summary>Adds two complex numbers.</summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of <paramref name="left" /> and <paramref name="right" />.</returns>
		// Token: 0x06000229 RID: 553 RVA: 0x0000FE66 File Offset: 0x0000E066
		public static Complex operator +(Complex left, Complex right)
		{
			return new Complex(left.m_real + right.m_real, left.m_imaginary + right.m_imaginary);
		}

		/// <summary>Subtracts a complex number from another complex number.</summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
		// Token: 0x0600022A RID: 554 RVA: 0x0000FE87 File Offset: 0x0000E087
		public static Complex operator -(Complex left, Complex right)
		{
			return new Complex(left.m_real - right.m_real, left.m_imaginary - right.m_imaginary);
		}

		/// <summary>Multiplies two specified complex numbers.</summary>
		/// <param name="left">The first value to multiply.</param>
		/// <param name="right">The second value to multiply.</param>
		/// <returns>The product of <paramref name="left" /> and <paramref name="right" />.</returns>
		// Token: 0x0600022B RID: 555 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public static Complex operator *(Complex left, Complex right)
		{
			double real = left.m_real * right.m_real - left.m_imaginary * right.m_imaginary;
			double imaginary = left.m_imaginary * right.m_real + left.m_real * right.m_imaginary;
			return new Complex(real, imaginary);
		}

		/// <summary>Divides a specified complex number by another specified complex number.</summary>
		/// <param name="left">The value to be divided.</param>
		/// <param name="right">The value to divide by.</param>
		/// <returns>The result of dividing <paramref name="left" /> by <paramref name="right" />.</returns>
		// Token: 0x0600022C RID: 556 RVA: 0x0000FEF4 File Offset: 0x0000E0F4
		public static Complex operator /(Complex left, Complex right)
		{
			double real = left.m_real;
			double imaginary = left.m_imaginary;
			double real2 = right.m_real;
			double imaginary2 = right.m_imaginary;
			if (Math.Abs(imaginary2) < Math.Abs(real2))
			{
				double num = imaginary2 / real2;
				return new Complex((real + imaginary * num) / (real2 + imaginary2 * num), (imaginary - real * num) / (real2 + imaginary2 * num));
			}
			double num2 = real2 / imaginary2;
			return new Complex((imaginary + real * num2) / (imaginary2 + real2 * num2), (-real + imaginary * num2) / (imaginary2 + real2 * num2));
		}

		/// <summary>Gets the absolute value (or magnitude) of a complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The absolute value of <paramref name="value" />.</returns>
		// Token: 0x0600022D RID: 557 RVA: 0x0000FF75 File Offset: 0x0000E175
		public static double Abs(Complex value)
		{
			return Complex.Hypot(value.m_real, value.m_imaginary);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000FF88 File Offset: 0x0000E188
		private static double Hypot(double a, double b)
		{
			a = Math.Abs(a);
			b = Math.Abs(b);
			double num;
			double num2;
			if (a < b)
			{
				num = a;
				num2 = b;
			}
			else
			{
				num = b;
				num2 = a;
			}
			if (num == 0.0)
			{
				return num2;
			}
			if (double.IsPositiveInfinity(num2) && !double.IsNaN(num))
			{
				return double.PositiveInfinity;
			}
			double num3 = num / num2;
			return num2 * Math.Sqrt(1.0 + num3 * num3);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		private static double Log1P(double x)
		{
			double num = 1.0 + x;
			if (num == 1.0)
			{
				return x;
			}
			if (x < 0.75)
			{
				return x * Math.Log(num) / (num - 1.0);
			}
			return Math.Log(num);
		}

		/// <summary>Computes the conjugate of a complex number and returns the result.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The conjugate of <paramref name="value" />.</returns>
		// Token: 0x06000230 RID: 560 RVA: 0x00010042 File Offset: 0x0000E242
		public static Complex Conjugate(Complex value)
		{
			return new Complex(value.m_real, -value.m_imaginary);
		}

		/// <summary>Returns the multiplicative inverse of a complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The reciprocal of <paramref name="value" />.</returns>
		// Token: 0x06000231 RID: 561 RVA: 0x00010056 File Offset: 0x0000E256
		public static Complex Reciprocal(Complex value)
		{
			if (value.m_real == 0.0 && value.m_imaginary == 0.0)
			{
				return Complex.Zero;
			}
			return Complex.One / value;
		}

		/// <summary>Returns a value that indicates whether two complex numbers are equal.</summary>
		/// <param name="left">The first complex number to compare.</param>
		/// <param name="right">The second complex number to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000232 RID: 562 RVA: 0x0001008B File Offset: 0x0000E28B
		public static bool operator ==(Complex left, Complex right)
		{
			return left.m_real == right.m_real && left.m_imaginary == right.m_imaginary;
		}

		/// <summary>Returns a value that indicates whether two complex numbers are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000233 RID: 563 RVA: 0x000100AB File Offset: 0x0000E2AB
		public static bool operator !=(Complex left, Complex right)
		{
			return left.m_real != right.m_real || left.m_imaginary != right.m_imaginary;
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified object have the same value.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Numerics.Complex" /> object or a type capable of implicit conversion to a <see cref="T:System.Numerics.Complex" /> object, and its value is equal to the current <see cref="T:System.Numerics.Complex" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000234 RID: 564 RVA: 0x000100CE File Offset: 0x0000E2CE
		public override bool Equals(object obj)
		{
			return obj is Complex && this.Equals((Complex)obj);
		}

		/// <summary>Returns a value that indicates whether the current instance and a specified complex number have the same value.</summary>
		/// <param name="value">The complex number to compare.</param>
		/// <returns>
		///   <see langword="true" /> if this complex number and <paramref name="value" /> have the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000235 RID: 565 RVA: 0x000100E6 File Offset: 0x0000E2E6
		public bool Equals(Complex value)
		{
			return this.m_real.Equals(value.m_real) && this.m_imaginary.Equals(value.m_imaginary);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Numerics.Complex" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000236 RID: 566 RVA: 0x00010110 File Offset: 0x0000E310
		public override int GetHashCode()
		{
			int num = 99999997;
			int num2 = this.m_real.GetHashCode() % num;
			int hashCode = this.m_imaginary.GetHashCode();
			return num2 ^ hashCode;
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form.</summary>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		// Token: 0x06000237 RID: 567 RVA: 0x0001013E File Offset: 0x0000E33E
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real, this.m_imaginary);
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format for its real and imaginary parts.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance in Cartesian form.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		// Token: 0x06000238 RID: 568 RVA: 0x00010165 File Offset: 0x0000E365
		public string ToString(string format)
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real.ToString(format, CultureInfo.CurrentCulture), this.m_imaginary.ToString(format, CultureInfo.CurrentCulture));
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified by <paramref name="provider" />.</returns>
		// Token: 0x06000239 RID: 569 RVA: 0x00010198 File Offset: 0x0000E398
		public string ToString(IFormatProvider provider)
		{
			return string.Format(provider, "({0}, {1})", this.m_real, this.m_imaginary);
		}

		/// <summary>Converts the value of the current complex number to its equivalent string representation in Cartesian form by using the specified format and culture-specific format information for its real and imaginary parts.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance in Cartesian form, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid format string.</exception>
		// Token: 0x0600023A RID: 570 RVA: 0x000101BB File Offset: 0x0000E3BB
		public string ToString(string format, IFormatProvider provider)
		{
			return string.Format(provider, "({0}, {1})", this.m_real.ToString(format, provider), this.m_imaginary.ToString(format, provider));
		}

		/// <summary>Returns the sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The sine of <paramref name="value" />.</returns>
		// Token: 0x0600023B RID: 571 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public static Complex Sin(Complex value)
		{
			double num = Math.Exp(value.m_imaginary);
			double num2 = 1.0 / num;
			double num3 = (num - num2) * 0.5;
			double num4 = (num + num2) * 0.5;
			return new Complex(Math.Sin(value.m_real) * num4, Math.Cos(value.m_real) * num3);
		}

		/// <summary>Returns the hyperbolic sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic sine of <paramref name="value" />.</returns>
		// Token: 0x0600023C RID: 572 RVA: 0x00010244 File Offset: 0x0000E444
		public static Complex Sinh(Complex value)
		{
			Complex complex = Complex.Sin(new Complex(-value.m_imaginary, value.m_real));
			return new Complex(complex.m_imaginary, -complex.m_real);
		}

		/// <summary>Returns the angle that is the arc sine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The angle which is the arc sine of <paramref name="value" />.</returns>
		// Token: 0x0600023D RID: 573 RVA: 0x0001027C File Offset: 0x0000E47C
		public static Complex Asin(Complex value)
		{
			double d;
			double num;
			double num2;
			Complex.Asin_Internal(Math.Abs(value.Real), Math.Abs(value.Imaginary), out d, out num, out num2);
			double num3;
			if (num < 0.0)
			{
				num3 = Math.Asin(d);
			}
			else
			{
				num3 = Math.Atan(num);
			}
			if (value.Real < 0.0)
			{
				num3 = -num3;
			}
			if (value.Imaginary < 0.0)
			{
				num2 = -num2;
			}
			return new Complex(num3, num2);
		}

		/// <summary>Returns the cosine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The cosine of <paramref name="value" />.</returns>
		// Token: 0x0600023E RID: 574 RVA: 0x000102FC File Offset: 0x0000E4FC
		public static Complex Cos(Complex value)
		{
			double num = Math.Exp(value.m_imaginary);
			double num2 = 1.0 / num;
			double num3 = (num - num2) * 0.5;
			double num4 = (num + num2) * 0.5;
			return new Complex(Math.Cos(value.m_real) * num4, -Math.Sin(value.m_real) * num3);
		}

		/// <summary>Returns the hyperbolic cosine of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic cosine of <paramref name="value" />.</returns>
		// Token: 0x0600023F RID: 575 RVA: 0x0001035D File Offset: 0x0000E55D
		public static Complex Cosh(Complex value)
		{
			return Complex.Cos(new Complex(-value.m_imaginary, value.m_real));
		}

		/// <summary>Returns the angle that is the arc cosine of the specified complex number.</summary>
		/// <param name="value">A complex number that represents a cosine.</param>
		/// <returns>The angle, measured in radians, which is the arc cosine of <paramref name="value" />.</returns>
		// Token: 0x06000240 RID: 576 RVA: 0x00010378 File Offset: 0x0000E578
		public static Complex Acos(Complex value)
		{
			double d;
			double num;
			double num2;
			Complex.Asin_Internal(Math.Abs(value.Real), Math.Abs(value.Imaginary), out d, out num, out num2);
			double num3;
			if (num < 0.0)
			{
				num3 = Math.Acos(d);
			}
			else
			{
				num3 = Math.Atan(1.0 / num);
			}
			if (value.Real < 0.0)
			{
				num3 = 3.141592653589793 - num3;
			}
			if (value.Imaginary > 0.0)
			{
				num2 = -num2;
			}
			return new Complex(num3, num2);
		}

		/// <summary>Returns the tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The tangent of <paramref name="value" />.</returns>
		// Token: 0x06000241 RID: 577 RVA: 0x00010408 File Offset: 0x0000E608
		public static Complex Tan(Complex value)
		{
			double num = 2.0 * value.m_real;
			double num2 = 2.0 * value.m_imaginary;
			double num3 = Math.Exp(num2);
			double num4 = 1.0 / num3;
			double num5 = (num3 + num4) * 0.5;
			if (Math.Abs(value.m_imaginary) <= 4.0)
			{
				double num6 = (num3 - num4) * 0.5;
				double num7 = Math.Cos(num) + num5;
				return new Complex(Math.Sin(num) / num7, num6 / num7);
			}
			double num8 = 1.0 + Math.Cos(num) / num5;
			return new Complex(Math.Sin(num) / num5 / num8, Math.Tanh(num2) / num8);
		}

		/// <summary>Returns the hyperbolic tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The hyperbolic tangent of <paramref name="value" />.</returns>
		// Token: 0x06000242 RID: 578 RVA: 0x000104CC File Offset: 0x0000E6CC
		public static Complex Tanh(Complex value)
		{
			Complex complex = Complex.Tan(new Complex(-value.m_imaginary, value.m_real));
			return new Complex(complex.m_imaginary, -complex.m_real);
		}

		/// <summary>Returns the angle that is the arc tangent of the specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The angle that is the arc tangent of <paramref name="value" />.</returns>
		// Token: 0x06000243 RID: 579 RVA: 0x00010504 File Offset: 0x0000E704
		public static Complex Atan(Complex value)
		{
			Complex right = new Complex(2.0, 0.0);
			return Complex.ImaginaryOne / right * (Complex.Log(Complex.One - Complex.ImaginaryOne * value) - Complex.Log(Complex.One + Complex.ImaginaryOne * value));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00010574 File Offset: 0x0000E774
		private static void Asin_Internal(double x, double y, out double b, out double bPrime, out double v)
		{
			if (x > Complex.s_asinOverflowThreshold || y > Complex.s_asinOverflowThreshold)
			{
				b = -1.0;
				bPrime = x / y;
				double num;
				double num2;
				if (x < y)
				{
					num = x;
					num2 = y;
				}
				else
				{
					num = y;
					num2 = x;
				}
				double num3 = num / num2;
				v = Complex.s_log2 + Math.Log(num2) + 0.5 * Complex.Log1P(num3 * num3);
				return;
			}
			double num4 = Complex.Hypot(x + 1.0, y);
			double num5 = Complex.Hypot(x - 1.0, y);
			double num6 = (num4 + num5) * 0.5;
			b = x / num6;
			if (b > 0.75)
			{
				if (x <= 1.0)
				{
					double num7 = (y * y / (num4 + (x + 1.0)) + (num5 + (1.0 - x))) * 0.5;
					bPrime = x / Math.Sqrt((num6 + x) * num7);
				}
				else
				{
					double num8 = (1.0 / (num4 + (x + 1.0)) + 1.0 / (num5 + (x - 1.0))) * 0.5;
					bPrime = x / y / Math.Sqrt((num6 + x) * num8);
				}
			}
			else
			{
				bPrime = -1.0;
			}
			if (num6 >= 1.5)
			{
				v = Math.Log(num6 + Math.Sqrt((num6 - 1.0) * (num6 + 1.0)));
				return;
			}
			if (x < 1.0)
			{
				double num9 = (1.0 / (num4 + (x + 1.0)) + 1.0 / (num5 + (1.0 - x))) * 0.5;
				double num10 = y * y * num9;
				v = Complex.Log1P(num10 + y * Math.Sqrt(num9 * (num6 + 1.0)));
				return;
			}
			double num11 = (y * y / (num4 + (x + 1.0)) + (num5 + (x - 1.0))) * 0.5;
			v = Complex.Log1P(num11 + Math.Sqrt(num11 * (num6 + 1.0)));
		}

		/// <summary>Returns the natural (base <see langword="e" />) logarithm of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The natural (base <see langword="e" />) logarithm of <paramref name="value" />.</returns>
		// Token: 0x06000245 RID: 581 RVA: 0x000107BD File Offset: 0x0000E9BD
		public static Complex Log(Complex value)
		{
			return new Complex(Math.Log(Complex.Abs(value)), Math.Atan2(value.m_imaginary, value.m_real));
		}

		/// <summary>Returns the logarithm of a specified complex number in a specified base.</summary>
		/// <param name="value">A complex number.</param>
		/// <param name="baseValue">The base of the logarithm.</param>
		/// <returns>The logarithm of <paramref name="value" /> in base <paramref name="baseValue" />.</returns>
		// Token: 0x06000246 RID: 582 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public static Complex Log(Complex value, double baseValue)
		{
			return Complex.Log(value) / Complex.Log(baseValue);
		}

		/// <summary>Returns the base-10 logarithm of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The base-10 logarithm of <paramref name="value" />.</returns>
		// Token: 0x06000247 RID: 583 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public static Complex Log10(Complex value)
		{
			return Complex.Scale(Complex.Log(value), 0.43429448190325);
		}

		/// <summary>Returns <see langword="e" /> raised to the power specified by a complex number.</summary>
		/// <param name="value">A complex number that specifies a power.</param>
		/// <returns>The number <see langword="e" /> raised to the power <paramref name="value" />.</returns>
		// Token: 0x06000248 RID: 584 RVA: 0x00010810 File Offset: 0x0000EA10
		public static Complex Exp(Complex value)
		{
			double num = Math.Exp(value.m_real);
			double real = num * Math.Cos(value.m_imaginary);
			double imaginary = num * Math.Sin(value.m_imaginary);
			return new Complex(real, imaginary);
		}

		/// <summary>Returns the square root of a specified complex number.</summary>
		/// <param name="value">A complex number.</param>
		/// <returns>The square root of <paramref name="value" />.</returns>
		// Token: 0x06000249 RID: 585 RVA: 0x0001084C File Offset: 0x0000EA4C
		public static Complex Sqrt(Complex value)
		{
			if (value.m_imaginary != 0.0)
			{
				bool flag = false;
				if (Math.Abs(value.m_real) >= Complex.s_sqrtRescaleThreshold || Math.Abs(value.m_imaginary) >= Complex.s_sqrtRescaleThreshold)
				{
					if (double.IsInfinity(value.m_imaginary) && !double.IsNaN(value.m_real))
					{
						return new Complex(double.PositiveInfinity, value.m_imaginary);
					}
					value.m_real *= 0.25;
					value.m_imaginary *= 0.25;
					flag = true;
				}
				double num;
				double num2;
				if (value.m_real >= 0.0)
				{
					num = Math.Sqrt((Complex.Hypot(value.m_real, value.m_imaginary) + value.m_real) * 0.5);
					num2 = value.m_imaginary / (2.0 * num);
				}
				else
				{
					num2 = Math.Sqrt((Complex.Hypot(value.m_real, value.m_imaginary) - value.m_real) * 0.5);
					if (value.m_imaginary < 0.0)
					{
						num2 = -num2;
					}
					num = value.m_imaginary / (2.0 * num2);
				}
				if (flag)
				{
					num *= 2.0;
					num2 *= 2.0;
				}
				return new Complex(num, num2);
			}
			if (value.m_real < 0.0)
			{
				return new Complex(0.0, Math.Sqrt(-value.m_real));
			}
			return new Complex(Math.Sqrt(value.m_real), 0.0);
		}

		/// <summary>Returns a specified complex number raised to a power specified by a complex number.</summary>
		/// <param name="value">A complex number to be raised to a power.</param>
		/// <param name="power">A complex number that specifies a power.</param>
		/// <returns>The complex number <paramref name="value" /> raised to the power <paramref name="power" />.</returns>
		// Token: 0x0600024A RID: 586 RVA: 0x000109F0 File Offset: 0x0000EBF0
		public static Complex Pow(Complex value, Complex power)
		{
			if (power == Complex.Zero)
			{
				return Complex.One;
			}
			if (value == Complex.Zero)
			{
				return Complex.Zero;
			}
			double real = value.m_real;
			double imaginary = value.m_imaginary;
			double real2 = power.m_real;
			double imaginary2 = power.m_imaginary;
			double num = Complex.Abs(value);
			double num2 = Math.Atan2(imaginary, real);
			double num3 = real2 * num2 + imaginary2 * Math.Log(num);
			double num4 = Math.Pow(num, real2) * Math.Pow(2.718281828459045, -imaginary2 * num2);
			return new Complex(num4 * Math.Cos(num3), num4 * Math.Sin(num3));
		}

		/// <summary>Returns a specified complex number raised to a power specified by a double-precision floating-point number.</summary>
		/// <param name="value">A complex number to be raised to a power.</param>
		/// <param name="power">A double-precision floating-point number that specifies a power.</param>
		/// <returns>The complex number <paramref name="value" /> raised to the power <paramref name="power" />.</returns>
		// Token: 0x0600024B RID: 587 RVA: 0x00010A92 File Offset: 0x0000EC92
		public static Complex Pow(Complex value, double power)
		{
			return Complex.Pow(value, new Complex(power, 0.0));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010AAC File Offset: 0x0000ECAC
		private static Complex Scale(Complex value, double factor)
		{
			double real = factor * value.m_real;
			double imaginary = factor * value.m_imaginary;
			return new Complex(real, imaginary);
		}

		/// <summary>Defines an implicit conversion of a 16-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x0600024D RID: 589 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public static implicit operator Complex(short value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 32-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x0600024E RID: 590 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public static implicit operator Complex(int value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 64-bit signed integer to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x0600024F RID: 591 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public static implicit operator Complex(long value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000250 RID: 592 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		[CLSCompliant(false)]
		public static implicit operator Complex(ushort value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000251 RID: 593 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		[CLSCompliant(false)]
		public static implicit operator Complex(uint value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000252 RID: 594 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		[CLSCompliant(false)]
		public static implicit operator Complex(ulong value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a signed byte to a complex number.   
		/// This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000253 RID: 595 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		[CLSCompliant(false)]
		public static implicit operator Complex(sbyte value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of an unsigned byte to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000254 RID: 596 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public static implicit operator Complex(byte value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a single-precision floating-point number to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000255 RID: 597 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		public static implicit operator Complex(float value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an implicit conversion of a double-precision floating-point number to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>An object that contains the value of the <paramref name="value" /> parameter as its real part and zero as its imaginary part.</returns>
		// Token: 0x06000256 RID: 598 RVA: 0x00010AF5 File Offset: 0x0000ECF5
		public static implicit operator Complex(double value)
		{
			return new Complex(value, 0.0);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Numerics.BigInteger" /> value to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>A complex number that has a real component equal to <paramref name="value" /> and an imaginary component equal to zero.</returns>
		// Token: 0x06000257 RID: 599 RVA: 0x00010B06 File Offset: 0x0000ED06
		public static explicit operator Complex(BigInteger value)
		{
			return new Complex((double)value, 0.0);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> value to a complex number.</summary>
		/// <param name="value">The value to convert to a complex number.</param>
		/// <returns>A complex number that has a real component equal to <paramref name="value" /> and an imaginary component equal to zero.</returns>
		// Token: 0x06000258 RID: 600 RVA: 0x00010B1D File Offset: 0x0000ED1D
		public static explicit operator Complex(decimal value)
		{
			return new Complex((double)value, 0.0);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00010B34 File Offset: 0x0000ED34
		// Note: this type is marked as 'beforefieldinit'.
		static Complex()
		{
		}

		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to zero and an imaginary number equal to zero.</summary>
		// Token: 0x0400008C RID: 140
		public static readonly Complex Zero = new Complex(0.0, 0.0);

		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to one and an imaginary number equal to zero.</summary>
		// Token: 0x0400008D RID: 141
		public static readonly Complex One = new Complex(1.0, 0.0);

		/// <summary>Returns a new <see cref="T:System.Numerics.Complex" /> instance with a real number equal to zero and an imaginary number equal to one.</summary>
		// Token: 0x0400008E RID: 142
		public static readonly Complex ImaginaryOne = new Complex(0.0, 1.0);

		// Token: 0x0400008F RID: 143
		private const double InverseOfLog10 = 0.43429448190325;

		// Token: 0x04000090 RID: 144
		private static readonly double s_sqrtRescaleThreshold = double.MaxValue / (Math.Sqrt(2.0) + 1.0);

		// Token: 0x04000091 RID: 145
		private static readonly double s_asinOverflowThreshold = Math.Sqrt(double.MaxValue) / 2.0;

		// Token: 0x04000092 RID: 146
		private static readonly double s_log2 = Math.Log(2.0);

		// Token: 0x04000093 RID: 147
		private double m_real;

		// Token: 0x04000094 RID: 148
		private double m_imaginary;
	}
}
