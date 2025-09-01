using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents an identity and is the base class for the <see cref="T:System.Security.Principal.NTAccount" /> and <see cref="T:System.Security.Principal.SecurityIdentifier" /> classes. This class does not provide a public constructor, and therefore cannot be inherited.</summary>
	// Token: 0x020004E4 RID: 1252
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x060031F2 RID: 12786 RVA: 0x0000259F File Offset: 0x0000079F
		internal IdentityReference()
		{
		}

		/// <summary>Gets the string value of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>The string value of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060031F3 RID: 12787
		public abstract string Value { get; }

		/// <summary>Returns a value that indicates whether the specified object equals this instance of the <see cref="T:System.Security.Principal.IdentityReference" /> class.</summary>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.IdentityReference" /> instance, or a null reference.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.IdentityReference" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031F4 RID: 12788
		public abstract override bool Equals(object o);

		/// <summary>Serves as a hash function for <see cref="T:System.Security.Principal.IdentityReference" />. <see cref="M:System.Security.Principal.IdentityReference.GetHashCode" /> is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>The hash code for this <see cref="T:System.Security.Principal.IdentityReference" /> object.</returns>
		// Token: 0x060031F5 RID: 12789
		public abstract override int GetHashCode();

		/// <summary>Returns a value that indicates whether the specified type is a valid translation type for the <see cref="T:System.Security.Principal.IdentityReference" /> class.</summary>
		/// <param name="targetType">The type being queried for validity to serve as a conversion from <see cref="T:System.Security.Principal.IdentityReference" />. The following target types are valid:  
		///  <see cref="T:System.Security.Principal.NTAccount" /><see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="targetType" /> is a valid translation type for the <see cref="T:System.Security.Principal.IdentityReference" /> class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031F6 RID: 12790
		public abstract bool IsValidTargetType(Type targetType);

		/// <summary>Returns the string representation of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>The identity in string format.</returns>
		// Token: 0x060031F7 RID: 12791
		public abstract override string ToString();

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.IdentityReference" />.</param>
		/// <returns>The converted identity.</returns>
		// Token: 0x060031F8 RID: 12792
		public abstract IdentityReference Translate(Type targetType);

		/// <summary>Compares two <see cref="T:System.Security.Principal.IdentityReference" /> objects to determine whether they are equal. They are considered equal if they have the same canonical name representation as the one returned by the <see cref="P:System.Security.Principal.IdentityReference.Value" /> property or if they are both <see langword="null" />.</summary>
		/// <param name="left">The left <see cref="T:System.Security.Principal.IdentityReference" /> operand to use for the equality comparison. This parameter can be <see langword="null" />.</param>
		/// <param name="right">The right <see cref="T:System.Security.Principal.IdentityReference" /> operand to use for the equality comparison. This parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031F9 RID: 12793 RVA: 0x000B7B98 File Offset: 0x000B5D98
		public static bool operator ==(IdentityReference left, IdentityReference right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.IdentityReference" /> objects to determine whether they are not equal. They are considered not equal if they have different canonical name representations than the one returned by the <see cref="P:System.Security.Principal.IdentityReference.Value" /> property or if one of the objects is <see langword="null" /> and the other is not.</summary>
		/// <param name="left">The left <see cref="T:System.Security.Principal.IdentityReference" /> operand to use for the inequality comparison. This parameter can be <see langword="null" />.</param>
		/// <param name="right">The right <see cref="T:System.Security.Principal.IdentityReference" /> operand to use for the inequality comparison. This parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031FA RID: 12794 RVA: 0x000B7BB8 File Offset: 0x000B5DB8
		public static bool operator !=(IdentityReference left, IdentityReference right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}
	}
}
