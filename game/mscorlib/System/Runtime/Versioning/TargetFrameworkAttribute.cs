using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the version of the .NET Framework that a particular assembly was compiled against.</summary>
	// Token: 0x0200063B RID: 1595
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetFrameworkAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> class by specifying the .NET Framework version against which an assembly was built.</summary>
		/// <param name="frameworkName">The version of the .NET Framework against which the assembly was built.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is <see langword="null" />.</exception>
		// Token: 0x06003C22 RID: 15394 RVA: 0x000D108C File Offset: 0x000CF28C
		public TargetFrameworkAttribute(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			this._frameworkName = frameworkName;
		}

		/// <summary>Gets the name of the .NET Framework version against which a particular assembly was compiled.</summary>
		/// <returns>The name of the .NET Framework version with which the assembly was compiled.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x000D10A9 File Offset: 0x000CF2A9
		public string FrameworkName
		{
			get
			{
				return this._frameworkName;
			}
		}

		/// <summary>Gets the display name of the .NET Framework version against which an assembly was built.</summary>
		/// <returns>The display name of the .NET Framework version.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000D10B1 File Offset: 0x000CF2B1
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x000D10B9 File Offset: 0x000CF2B9
		public string FrameworkDisplayName
		{
			get
			{
				return this._frameworkDisplayName;
			}
			set
			{
				this._frameworkDisplayName = value;
			}
		}

		// Token: 0x040026EE RID: 9966
		private string _frameworkName;

		// Token: 0x040026EF RID: 9967
		private string _frameworkDisplayName;
	}
}
