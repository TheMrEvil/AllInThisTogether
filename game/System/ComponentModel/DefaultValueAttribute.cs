using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x0200035E RID: 862
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to.</param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture.</param>
		// Token: 0x06001C94 RID: 7316 RVA: 0x00067718 File Offset: 0x00065918
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				object value2;
				if (DefaultValueAttribute.<.ctor>g__TryConvertFromInvariantString|2_0(type, value, out value2))
				{
					this._value = value2;
				}
				else if (type.IsSubclassOf(typeof(Enum)))
				{
					this._value = Enum.Parse(type, value, true);
				}
				else if (type == typeof(TimeSpan))
				{
					this._value = TimeSpan.Parse(value);
				}
				else
				{
					this._value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value.</param>
		// Token: 0x06001C95 RID: 7317 RVA: 0x000677B0 File Offset: 0x000659B0
		public DefaultValueAttribute(char value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value.</param>
		// Token: 0x06001C96 RID: 7318 RVA: 0x000677C4 File Offset: 0x000659C4
		public DefaultValueAttribute(byte value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value.</param>
		// Token: 0x06001C97 RID: 7319 RVA: 0x000677D8 File Offset: 0x000659D8
		public DefaultValueAttribute(short value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value.</param>
		// Token: 0x06001C98 RID: 7320 RVA: 0x000677EC File Offset: 0x000659EC
		public DefaultValueAttribute(int value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value.</param>
		// Token: 0x06001C99 RID: 7321 RVA: 0x00067800 File Offset: 0x00065A00
		public DefaultValueAttribute(long value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value.</param>
		// Token: 0x06001C9A RID: 7322 RVA: 0x00067814 File Offset: 0x00065A14
		public DefaultValueAttribute(float value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value.</param>
		// Token: 0x06001C9B RID: 7323 RVA: 0x00067828 File Offset: 0x00065A28
		public DefaultValueAttribute(double value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value.</param>
		// Token: 0x06001C9C RID: 7324 RVA: 0x0006783C File Offset: 0x00065A3C
		public DefaultValueAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value.</param>
		// Token: 0x06001C9D RID: 7325 RVA: 0x00067850 File Offset: 0x00065A50
		public DefaultValueAttribute(string value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value.</param>
		// Token: 0x06001C9E RID: 7326 RVA: 0x00067850 File Offset: 0x00065A50
		public DefaultValueAttribute(object value)
		{
			this._value = value;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006785F File Offset: 0x00065A5F
		[CLSCompliant(false)]
		public DefaultValueAttribute(sbyte value)
		{
			this._value = value;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00067873 File Offset: 0x00065A73
		[CLSCompliant(false)]
		public DefaultValueAttribute(ushort value)
		{
			this._value = value;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00067887 File Offset: 0x00065A87
		[CLSCompliant(false)]
		public DefaultValueAttribute(uint value)
		{
			this._value = value;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006789B File Offset: 0x00065A9B
		[CLSCompliant(false)]
		public DefaultValueAttribute(ulong value)
		{
			this._value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x000678AF File Offset: 0x00065AAF
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000678B8 File Offset: 0x00065AB8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00067902 File Offset: 0x00065B02
		protected void SetValue(object value)
		{
			this._value = value;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006790C File Offset: 0x00065B0C
		[CompilerGenerated]
		internal static bool <.ctor>g__TryConvertFromInvariantString|2_0(Type typeToConvert, string stringValue, out object conversionResult)
		{
			conversionResult = null;
			if (DefaultValueAttribute.s_convertFromInvariantString == null)
			{
				Type type = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter", false);
				Volatile.Write<object>(ref DefaultValueAttribute.s_convertFromInvariantString, (type == null) ? new object() : Delegate.CreateDelegate(typeof(Func<Type, string, object>), type, "ConvertFromInvariantString", false));
			}
			Func<Type, string, object> func = DefaultValueAttribute.s_convertFromInvariantString as Func<Type, string, object>;
			if (func == null)
			{
				return false;
			}
			conversionResult = func(typeToConvert, stringValue);
			return true;
		}

		// Token: 0x04000E8A RID: 3722
		private object _value;

		// Token: 0x04000E8B RID: 3723
		private static object s_convertFromInvariantString;
	}
}
