using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter.</param>
		/// <param name="value">The value for this attribute.</param>
		// Token: 0x06001D47 RID: 7495 RVA: 0x00068790 File Offset: 0x00066990
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.Value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D48 RID: 7496 RVA: 0x000687CC File Offset: 0x000669CC
		public AmbientValueAttribute(char value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D49 RID: 7497 RVA: 0x000687E0 File Offset: 0x000669E0
		public AmbientValueAttribute(byte value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4A RID: 7498 RVA: 0x000687F4 File Offset: 0x000669F4
		public AmbientValueAttribute(short value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4B RID: 7499 RVA: 0x00068808 File Offset: 0x00066A08
		public AmbientValueAttribute(int value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4C RID: 7500 RVA: 0x0006881C File Offset: 0x00066A1C
		public AmbientValueAttribute(long value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4D RID: 7501 RVA: 0x00068830 File Offset: 0x00066A30
		public AmbientValueAttribute(float value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4E RID: 7502 RVA: 0x00068844 File Offset: 0x00066A44
		public AmbientValueAttribute(double value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D4F RID: 7503 RVA: 0x00068858 File Offset: 0x00066A58
		public AmbientValueAttribute(bool value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D50 RID: 7504 RVA: 0x0006886C File Offset: 0x00066A6C
		public AmbientValueAttribute(string value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute.</param>
		// Token: 0x06001D51 RID: 7505 RVA: 0x0006886C File Offset: 0x00066A6C
		public AmbientValueAttribute(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0006887B File Offset: 0x00066A7B
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D53 RID: 7507 RVA: 0x00068884 File Offset: 0x00066A84
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.Value == null)
			{
				return ambientValueAttribute.Value == null;
			}
			return this.Value.Equals(ambientValueAttribute.Value);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x06001D54 RID: 7508 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000ECF RID: 3791
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}
