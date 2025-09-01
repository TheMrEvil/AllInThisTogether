using System;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the connection between an instance of <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the formatter-provided class best suited to parse the data inside the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
	// Token: 0x02000648 RID: 1608
	[CLSCompliant(false)]
	public interface IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C3C RID: 15420
		object Convert(object value, Type type);

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C3D RID: 15421
		object Convert(object value, TypeCode typeCode);

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C3E RID: 15422
		bool ToBoolean(object value);

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C3F RID: 15423
		char ToChar(object value);

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C40 RID: 15424
		sbyte ToSByte(object value);

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C41 RID: 15425
		byte ToByte(object value);

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C42 RID: 15426
		short ToInt16(object value);

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C43 RID: 15427
		ushort ToUInt16(object value);

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C44 RID: 15428
		int ToInt32(object value);

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C45 RID: 15429
		uint ToUInt32(object value);

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C46 RID: 15430
		long ToInt64(object value);

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C47 RID: 15431
		ulong ToUInt64(object value);

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C48 RID: 15432
		float ToSingle(object value);

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C49 RID: 15433
		double ToDouble(object value);

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C4A RID: 15434
		decimal ToDecimal(object value);

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C4B RID: 15435
		DateTime ToDateTime(object value);

		/// <summary>Converts a value to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The object to be converted.</param>
		/// <returns>The converted <paramref name="value" />.</returns>
		// Token: 0x06003C4C RID: 15436
		string ToString(object value);
	}
}
