using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Specifies when you can use a licensed object and provides a way of obtaining additional services needed to support licenses running within its domain.</summary>
	// Token: 0x020003C7 RID: 967
	public class LicenseContext : IServiceProvider
	{
		/// <summary>When overridden in a derived class, gets a value that specifies when you can use a license.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values that specifies when you can use a license. The default is <see cref="F:System.ComponentModel.LicenseUsageMode.Runtime" />.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00003062 File Offset: 0x00001262
		public virtual LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Runtime;
			}
		}

		/// <summary>When overridden in a derived class, returns a saved license key for the specified type, from the specified resource assembly.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component.</param>
		/// <param name="resourceAssembly">An <see cref="T:System.Reflection.Assembly" /> with the license key.</param>
		/// <returns>The <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type. This method returns <see langword="null" /> unless you override it.</returns>
		// Token: 0x06001F4A RID: 8010 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <param name="type">The type of service to retrieve.</param>
		/// <returns>An instance of the service, or <see langword="null" /> if the service cannot be found.</returns>
		// Token: 0x06001F4B RID: 8011 RVA: 0x00002F6A File Offset: 0x0000116A
		public virtual object GetService(Type type)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, sets a license key for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component associated with the license key.</param>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to save for the type of component.</param>
		// Token: 0x06001F4C RID: 8012 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void SetSavedLicenseKey(Type type, string key)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseContext" /> class.</summary>
		// Token: 0x06001F4D RID: 8013 RVA: 0x0000219B File Offset: 0x0000039B
		public LicenseContext()
		{
		}
	}
}
