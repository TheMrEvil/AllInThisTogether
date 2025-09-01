using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the <see langword="Token" /> returned by the metadata to represent a type.</summary>
	// Token: 0x02000949 RID: 2377
	[ComVisible(true)]
	[Serializable]
	public readonly struct TypeToken : IEquatable<TypeToken>
	{
		// Token: 0x06005353 RID: 21331 RVA: 0x00105C04 File Offset: 0x00103E04
		internal TypeToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of <see langword="TypeToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this TypeToken.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="TypeToken" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005354 RID: 21332 RVA: 0x00105C10 File Offset: 0x00103E10
		public override bool Equals(object obj)
		{
			bool flag = obj is TypeToken;
			if (flag)
			{
				TypeToken typeToken = (TypeToken)obj;
				flag = (this.tokValue == typeToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005355 RID: 21333 RVA: 0x00105C41 File Offset: 0x00103E41
		public bool Equals(TypeToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005356 RID: 21334 RVA: 0x00105C51 File Offset: 0x00103E51
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.TypeToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005357 RID: 21335 RVA: 0x00105C64 File Offset: 0x00103E64
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this type.</summary>
		/// <returns>The hash code for this type.</returns>
		// Token: 0x06005358 RID: 21336 RVA: 0x00105C7A File Offset: 0x00103E7A
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this class.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this type.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x00105C7A File Offset: 0x00103E7A
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x00004BF9 File Offset: 0x00002DF9
		// Note: this type is marked as 'beforefieldinit'.
		static TypeToken()
		{
		}

		// Token: 0x0400333D RID: 13117
		internal readonly int tokValue;

		/// <summary>The default <see langword="TypeToken" /> with <see cref="P:System.Reflection.Emit.TypeToken.Token" /> value 0.</summary>
		// Token: 0x0400333E RID: 13118
		public static readonly TypeToken Empty;
	}
}
