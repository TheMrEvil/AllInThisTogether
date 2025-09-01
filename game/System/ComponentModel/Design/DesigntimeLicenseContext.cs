using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x0200044D RID: 1101
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0000390E File Offset: 0x00001B0E
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		/// <summary>Gets a saved license key.</summary>
		/// <param name="type">The type of the license key.</param>
		/// <param name="resourceAssembly">The assembly to get the key from.</param>
		/// <returns>The saved license key that matches the specified type.</returns>
		// Token: 0x060023D9 RID: 9177 RVA: 0x00002F6A File Offset: 0x0000116A
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key.</param>
		/// <param name="key">The license key.</param>
		// Token: 0x060023DA RID: 9178 RVA: 0x00081450 File Offset: 0x0007F650
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.savedLicenseKeys[type.AssemblyQualifiedName] = key;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> class.</summary>
		// Token: 0x060023DB RID: 9179 RVA: 0x00081464 File Offset: 0x0007F664
		public DesigntimeLicenseContext()
		{
		}

		// Token: 0x040010C0 RID: 4288
		internal Hashtable savedLicenseKeys = new Hashtable();
	}
}
