using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The <see langword="PropertyToken" /> struct is an opaque representation of the <see langword="Token" /> returned by the metadata to represent a property.</summary>
	// Token: 0x02000942 RID: 2370
	[ComVisible(true)]
	[Serializable]
	public readonly struct PropertyToken : IEquatable<PropertyToken>
	{
		// Token: 0x0600524A RID: 21066 RVA: 0x001027B2 File Offset: 0x001009B2
		internal PropertyToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of <see langword="PropertyToken" /> and is equal to this instance.</summary>
		/// <param name="obj">The object to this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="PropertyToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600524B RID: 21067 RVA: 0x001027BC File Offset: 0x001009BC
		public override bool Equals(object obj)
		{
			bool flag = obj is PropertyToken;
			if (flag)
			{
				PropertyToken propertyToken = (PropertyToken)obj;
				flag = (this.tokValue == propertyToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.PropertyToken" />.</summary>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600524C RID: 21068 RVA: 0x001027ED File Offset: 0x001009ED
		public bool Equals(PropertyToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600524D RID: 21069 RVA: 0x001027FD File Offset: 0x001009FD
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.PropertyToken" /> structures are not equal.</summary>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.PropertyToken" /> to compare to <paramref name="a" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600524E RID: 21070 RVA: 0x00102810 File Offset: 0x00100A10
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this property.</summary>
		/// <returns>The hash code for this property.</returns>
		// Token: 0x0600524F RID: 21071 RVA: 0x00102826 File Offset: 0x00100A26
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this property.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this instance.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x00102826 File Offset: 0x00100A26
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x00004BF9 File Offset: 0x00002DF9
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyToken()
		{
		}

		// Token: 0x04003309 RID: 13065
		internal readonly int tokValue;

		/// <summary>The default <see langword="PropertyToken" /> with <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> value 0.</summary>
		// Token: 0x0400330A RID: 13066
		public static readonly PropertyToken Empty;
	}
}
