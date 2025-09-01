using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="ParameterToken" /> struct is an opaque representation of the token returned by the metadata to represent a parameter.</summary>
	// Token: 0x0200093F RID: 2367
	[ComVisible(true)]
	[Serializable]
	public readonly struct ParameterToken : IEquatable<ParameterToken>
	{
		// Token: 0x0600520F RID: 21007 RVA: 0x001023E2 File Offset: 0x001005E2
		internal ParameterToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of <see langword="ParameterToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to compare to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="ParameterToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005210 RID: 21008 RVA: 0x001023EC File Offset: 0x001005EC
		public override bool Equals(object obj)
		{
			bool flag = obj is ParameterToken;
			if (flag)
			{
				ParameterToken parameterToken = (ParameterToken)obj;
				flag = (this.tokValue == parameterToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.ParameterToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005211 RID: 21009 RVA: 0x0010241D File Offset: 0x0010061D
		public bool Equals(ParameterToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005212 RID: 21010 RVA: 0x0010242D File Offset: 0x0010062D
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.ParameterToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.ParameterToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005213 RID: 21011 RVA: 0x00102440 File Offset: 0x00100640
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this parameter.</summary>
		/// <returns>The hash code for this parameter.</returns>
		// Token: 0x06005214 RID: 21012 RVA: 0x00102456 File Offset: 0x00100656
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this parameter.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this parameter.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x00102456 File Offset: 0x00100656
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00004BF9 File Offset: 0x00002DF9
		// Note: this type is marked as 'beforefieldinit'.
		static ParameterToken()
		{
		}

		// Token: 0x040032F6 RID: 13046
		internal readonly int tokValue;

		/// <summary>The default <see langword="ParameterToken" /> with <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> value 0.</summary>
		// Token: 0x040032F7 RID: 13047
		public static readonly ParameterToken Empty;
	}
}
