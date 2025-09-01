using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed assembly is a primary interop assembly.</summary>
	// Token: 0x0200070B RID: 1803
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> class with the major and minor version numbers of the type library for which this assembly is the primary interop assembly.</summary>
		/// <param name="major">The major version of the type library for which this assembly is the primary interop assembly.</param>
		/// <param name="minor">The minor version of the type library for which this assembly is the primary interop assembly.</param>
		// Token: 0x060040B1 RID: 16561 RVA: 0x000E153A File Offset: 0x000DF73A
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The major version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000E1550 File Offset: 0x000DF750
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library for which this assembly is the primary interop assembly.</summary>
		/// <returns>The minor version number of the type library for which this assembly is the primary interop assembly.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000E1558 File Offset: 0x000DF758
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002AE2 RID: 10978
		internal int _major;

		// Token: 0x04002AE3 RID: 10979
		internal int _minor;
	}
}
