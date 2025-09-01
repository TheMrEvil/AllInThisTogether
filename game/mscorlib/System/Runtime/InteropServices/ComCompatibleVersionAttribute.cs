using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates to a COM client that all classes in the current version of an assembly are compatible with classes in an earlier version of the assembly.</summary>
	// Token: 0x0200070F RID: 1807
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComCompatibleVersionAttribute" /> class with the major version, minor version, build, and revision numbers of the assembly.</summary>
		/// <param name="major">The major version number of the assembly.</param>
		/// <param name="minor">The minor version number of the assembly.</param>
		/// <param name="build">The build number of the assembly.</param>
		/// <param name="revision">The revision number of the assembly.</param>
		// Token: 0x060040BC RID: 16572 RVA: 0x000E15C3 File Offset: 0x000DF7C3
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		/// <summary>Gets the major version number of the assembly.</summary>
		/// <returns>The major version number of the assembly.</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x000E15E8 File Offset: 0x000DF7E8
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		/// <summary>Gets the minor version number of the assembly.</summary>
		/// <returns>The minor version number of the assembly.</returns>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x000E15F0 File Offset: 0x000DF7F0
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		/// <summary>Gets the build number of the assembly.</summary>
		/// <returns>The build number of the assembly.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000E15F8 File Offset: 0x000DF7F8
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		/// <summary>Gets the revision number of the assembly.</summary>
		/// <returns>The revision number of the assembly.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000E1600 File Offset: 0x000DF800
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002AE9 RID: 10985
		internal int _major;

		// Token: 0x04002AEA RID: 10986
		internal int _minor;

		// Token: 0x04002AEB RID: 10987
		internal int _build;

		// Token: 0x04002AEC RID: 10988
		internal int _revision;
	}
}
