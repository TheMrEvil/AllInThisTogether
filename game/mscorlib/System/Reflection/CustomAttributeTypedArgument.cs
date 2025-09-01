using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Represents an argument of a custom attribute in the reflection-only context, or an element of an array argument.</summary>
	// Token: 0x020008DE RID: 2270
	public struct CustomAttributeTypedArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified value.</summary>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06004B9B RID: 19355 RVA: 0x000F0D04 File Offset: 0x000EEF04
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.ArgumentType = value.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified type and value.</summary>
		/// <param name="argumentType">The type of the custom attribute argument.</param>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argumentType" /> is <see langword="null" />.</exception>
		// Token: 0x06004B9C RID: 19356 RVA: 0x000F0D2C File Offset: 0x000EEF2C
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.Value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.ArgumentType = argumentType;
			Array array = value as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[array.GetLength(0)];
				for (int i = 0; i < array2.Length; i++)
				{
					object value2 = array.GetValue(i);
					Type argumentType2 = (elementType == typeof(object) && value2 != null) ? value2.GetType() : elementType;
					array2[i] = new CustomAttributeTypedArgument(argumentType2, value2);
				}
				this.Value = new ReadOnlyCollection<CustomAttributeTypedArgument>(array2);
			}
		}

		/// <summary>Gets the type of the argument or of the array argument element.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the argument or of the array element.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x000F0DDE File Offset: 0x000EEFDE
		public readonly Type ArgumentType
		{
			[CompilerGenerated]
			get
			{
				return this.<ArgumentType>k__BackingField;
			}
		}

		/// <summary>Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.</summary>
		/// <returns>An object that represents the value of the argument or element, or a generic <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> objects that represent the values of an array-type argument.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x000F0DE6 File Offset: 0x000EEFE6
		public readonly object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B9F RID: 19359 RVA: 0x000F0DEE File Offset: 0x000EEFEE
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06004BA0 RID: 19360 RVA: 0x000F0DFE File Offset: 0x000EEFFE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004BA1 RID: 19361 RVA: 0x000F0E10 File Offset: 0x000EF010
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different.</summary>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004BA2 RID: 19362 RVA: 0x000F0E25 File Offset: 0x000EF025
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a string consisting of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string consisting of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06004BA3 RID: 19363 RVA: 0x000F0E3D File Offset: 0x000EF03D
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x000F0E48 File Offset: 0x000EF048
		internal string ToString(bool typed)
		{
			if (this.ArgumentType == null)
			{
				return base.ToString();
			}
			string result;
			try
			{
				if (this.ArgumentType.IsEnum)
				{
					result = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullNameOrDefault);
				}
				else if (this.Value == null)
				{
					result = string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.NameOrDefault);
				}
				else if (this.ArgumentType == typeof(string))
				{
					result = string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
				}
				else if (this.ArgumentType == typeof(char))
				{
					result = string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
				}
				else if (this.ArgumentType == typeof(Type))
				{
					result = string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullNameOrDefault);
				}
				else if (this.ArgumentType.IsArray)
				{
					IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
					Type elementType = this.ArgumentType.GetElementType();
					string str = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullNameOrDefault : elementType.NameOrDefault, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						str += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
					}
					result = str + " }";
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.NameOrDefault);
				}
			}
			catch (MissingMetadataException)
			{
				result = base.ToString();
			}
			return result;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x000F10A4 File Offset: 0x000EF2A4
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x04002F6C RID: 12140
		[CompilerGenerated]
		private readonly Type <ArgumentType>k__BackingField;

		// Token: 0x04002F6D RID: 12141
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}
