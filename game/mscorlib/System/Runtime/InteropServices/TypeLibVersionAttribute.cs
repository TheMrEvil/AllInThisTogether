using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the version number of an exported type library.</summary>
	// Token: 0x0200070E RID: 1806
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibVersionAttribute" /> class with the major and minor version numbers of the type library.</summary>
		/// <param name="major">The major version number of the type library.</param>
		/// <param name="minor">The minor version number of the type library.</param>
		// Token: 0x060040B9 RID: 16569 RVA: 0x000E159D File Offset: 0x000DF79D
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		/// <summary>Gets the major version number of the type library.</summary>
		/// <returns>The major version number of the type library.</returns>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000E15B3 File Offset: 0x000DF7B3
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the type library.</summary>
		/// <returns>The minor version number of the type library.</returns>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000E15BB File Offset: 0x000DF7BB
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002AE7 RID: 10983
		internal int _major;

		// Token: 0x04002AE8 RID: 10984
		internal int _minor;
	}
}
