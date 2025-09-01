using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents an operating system platform.</summary>
	// Token: 0x020006CC RID: 1740
	public readonly struct OSPlatform : IEquatable<OSPlatform>
	{
		/// <summary>Gets an object that represents the Linux operating system.</summary>
		/// <returns>An object that represents the Linux operating system.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000E04CA File Offset: 0x000DE6CA
		public static OSPlatform Linux
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Linux>k__BackingField;
			}
		} = new OSPlatform("LINUX");

		/// <summary>Gets an object that represents the OSX operating system.</summary>
		/// <returns>An object that represents the OSX operating system.</returns>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x000E04D1 File Offset: 0x000DE6D1
		public static OSPlatform OSX
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<OSX>k__BackingField;
			}
		} = new OSPlatform("OSX");

		/// <summary>Gets an object that represents the Windows operating system.</summary>
		/// <returns>An object that represents the Windows operating system.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000E04D8 File Offset: 0x000DE6D8
		public static OSPlatform Windows
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Windows>k__BackingField;
			}
		} = new OSPlatform("WINDOWS");

		// Token: 0x06004005 RID: 16389 RVA: 0x000E04DF File Offset: 0x000DE6DF
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException("Value cannot be empty.", "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		/// <summary>Creates a new <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</summary>
		/// <param name="osPlatform">The name of the platform that this instance represents.</param>
		/// <returns>An object that represents the <paramref name="osPlatform" /> operating system.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="osPlatform" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="osPlatform" /> is <see langword="null" />.</exception>
		// Token: 0x06004006 RID: 16390 RVA: 0x000E050E File Offset: 0x000DE70E
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		/// <summary>Determines whether the current instance and the specified <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance are equal.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance and <paramref name="other" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004007 RID: 16391 RVA: 0x000E0516 File Offset: 0x000DE716
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000E0524 File Offset: 0x000DE724
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		/// <summary>Determines whether the current <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance is equal to the specified object.</summary>
		/// <param name="obj">
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance and its name is the same as the current object; otherwise, false.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance and its name is the same as the current object.</returns>
		// Token: 0x06004009 RID: 16393 RVA: 0x000E0533 File Offset: 0x000DE733
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x0600400A RID: 16394 RVA: 0x000E054B File Offset: 0x000DE74B
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instance.</returns>
		// Token: 0x0600400B RID: 16395 RVA: 0x000E0562 File Offset: 0x000DE762
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.InteropServices.OSPlatform" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600400C RID: 16396 RVA: 0x000E0573 File Offset: 0x000DE773
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.InteropServices.OSPlatform" /> instances are unequal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600400D RID: 16397 RVA: 0x000E057D File Offset: 0x000DE77D
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000E0589 File Offset: 0x000DE789
		// Note: this type is marked as 'beforefieldinit'.
		static OSPlatform()
		{
		}

		// Token: 0x04002A04 RID: 10756
		private readonly string _osPlatform;

		// Token: 0x04002A05 RID: 10757
		[CompilerGenerated]
		private static readonly OSPlatform <Linux>k__BackingField;

		// Token: 0x04002A06 RID: 10758
		[CompilerGenerated]
		private static readonly OSPlatform <OSX>k__BackingField;

		// Token: 0x04002A07 RID: 10759
		[CompilerGenerated]
		private static readonly OSPlatform <Windows>k__BackingField;
	}
}
