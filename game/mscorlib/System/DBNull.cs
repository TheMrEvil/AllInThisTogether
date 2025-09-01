using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a nonexistent value. This class cannot be inherited.</summary>
	// Token: 0x0200010A RID: 266
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0000259F File Offset: 0x0000079F
		private DBNull()
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000258C0 File Offset: 0x00023AC0
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException("Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed.");
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.DBNull" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing information required to serialize the <see cref="T:System.DBNull" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.DBNull" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600098F RID: 2447 RVA: 0x000258D2 File Offset: 0x00023AD2
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2);
		}

		/// <summary>Returns an empty string (<see cref="F:System.String.Empty" />).</summary>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		// Token: 0x06000990 RID: 2448 RVA: 0x000258DB File Offset: 0x00023ADB
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Returns an empty string using the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <param name="provider">The <see cref="T:System.IFormatProvider" /> to be used to format the return value.  
		///  -or-  
		///  <see langword="null" /> to obtain the format information from the current locale setting of the operating system.</param>
		/// <returns>An empty string (<see cref="F:System.String.Empty" />).</returns>
		// Token: 0x06000991 RID: 2449 RVA: 0x000258DB File Offset: 0x00023ADB
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		/// <summary>Gets the <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> value for <see cref="T:System.DBNull" />, which is <see cref="F:System.TypeCode.DBNull" />.</returns>
		// Token: 0x06000992 RID: 2450 RVA: 0x00015831 File Offset: 0x00013A31
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000993 RID: 2451 RVA: 0x000258E2 File Offset: 0x00023AE2
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000994 RID: 2452 RVA: 0x000258E2 File Offset: 0x00023AE2
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000995 RID: 2453 RVA: 0x000258E2 File Offset: 0x00023AE2
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000996 RID: 2454 RVA: 0x000258E2 File Offset: 0x00023AE2
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000997 RID: 2455 RVA: 0x000258E2 File Offset: 0x00023AE2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000998 RID: 2456 RVA: 0x000258E2 File Offset: 0x00023AE2
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x06000999 RID: 2457 RVA: 0x000258E2 File Offset: 0x00023AE2
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099A RID: 2458 RVA: 0x000258E2 File Offset: 0x00023AE2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099B RID: 2459 RVA: 0x000258E2 File Offset: 0x00023AE2
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099C RID: 2460 RVA: 0x000258E2 File Offset: 0x00023AE2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099D RID: 2461 RVA: 0x000258E2 File Offset: 0x00023AE2
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099E RID: 2462 RVA: 0x000258E2 File Offset: 0x00023AE2
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x0600099F RID: 2463 RVA: 0x000258E2 File Offset: 0x00023AE2
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>This conversion is not supported. Attempting to make this conversion throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>None. The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		// Token: 0x060009A0 RID: 2464 RVA: 0x000258E2 File Offset: 0x00023AE2
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException("Object cannot be cast from DBNull to other types.");
		}

		/// <summary>Converts the current <see cref="T:System.DBNull" /> object to the specified type.</summary>
		/// <param name="type">The type to convert the current <see cref="T:System.DBNull" /> object to.</param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface and is used to augment the conversion. If <see langword="null" /> is specified, format information is obtained from the current culture.</param>
		/// <returns>The boxed equivalent of the current <see cref="T:System.DBNull" /> object, if that conversion is supported; otherwise, an exception is thrown and no value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DBNull" /> type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001931A File Offset: 0x0001751A
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000258EE File Offset: 0x00023AEE
		// Note: this type is marked as 'beforefieldinit'.
		static DBNull()
		{
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.DBNull" /> class.</summary>
		// Token: 0x04001092 RID: 4242
		public static readonly DBNull Value = new DBNull();
	}
}
