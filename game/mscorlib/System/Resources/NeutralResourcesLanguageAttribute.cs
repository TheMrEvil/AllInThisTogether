using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	/// <summary>Informs the resource manager of an app's default culture. This class cannot be inherited.</summary>
	// Token: 0x0200085C RID: 2140
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cultureName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004740 RID: 18240 RVA: 0x000E80EC File Offset: 0x000E62EC
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this.CultureName = cultureName;
			this.Location = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> class with the specified ultimate resource fallback location.</summary>
		/// <param name="cultureName">The name of the culture that the current assembly's neutral resources were written in.</param>
		/// <param name="location">One of the enumeration values that indicates the location from which to retrieve neutral fallback resources.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cultureName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="location" /> is not a member of <see cref="T:System.Resources.UltimateResourceFallbackLocation" />.</exception>
		// Token: 0x06004741 RID: 18241 RVA: 0x000E8110 File Offset: 0x000E6310
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(SR.Format("The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".", location));
			}
			this.CultureName = cultureName;
			this.Location = location;
		}

		/// <summary>Gets the culture name.</summary>
		/// <returns>The name of the default culture for the main assembly.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x000E816C File Offset: 0x000E636C
		public string CultureName
		{
			[CompilerGenerated]
			get
			{
				return this.<CultureName>k__BackingField;
			}
		}

		/// <summary>Gets the location for the <see cref="T:System.Resources.ResourceManager" /> class to use to retrieve neutral resources by using the resource fallback process.</summary>
		/// <returns>One of the enumeration values that indicates the location (main assembly or satellite) from which to retrieve neutral resources.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000E8174 File Offset: 0x000E6374
		public UltimateResourceFallbackLocation Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
		}

		// Token: 0x04002DA2 RID: 11682
		[CompilerGenerated]
		private readonly string <CultureName>k__BackingField;

		// Token: 0x04002DA3 RID: 11683
		[CompilerGenerated]
		private readonly UltimateResourceFallbackLocation <Location>k__BackingField;
	}
}
