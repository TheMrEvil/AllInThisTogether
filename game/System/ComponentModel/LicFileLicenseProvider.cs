using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides an implementation of a <see cref="T:System.ComponentModel.LicenseProvider" />. The provider works in a similar fashion to the Microsoft .NET Framework standard licensing model.</summary>
	// Token: 0x020003C4 RID: 964
	public class LicFileLicenseProvider : LicenseProvider
	{
		/// <summary>Determines whether the key that the <see cref="M:System.ComponentModel.LicFileLicenseProvider.GetLicense(System.ComponentModel.LicenseContext,System.Type,System.Object,System.Boolean)" /> method retrieves is valid for the specified type.</summary>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to check.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />.</param>
		/// <returns>
		///   <see langword="true" /> if the key is a valid <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F3F RID: 7999 RVA: 0x0006CDFE File Offset: 0x0006AFFE
		protected virtual bool IsKeyValid(string key, Type type)
		{
			return key != null && key.StartsWith(this.GetKey(type));
		}

		/// <summary>Returns a key for the specified type.</summary>
		/// <param name="type">The object type to return the key.</param>
		/// <returns>A confirmation that the <paramref name="type" /> parameter is licensed.</returns>
		// Token: 0x06001F40 RID: 8000 RVA: 0x0006CE12 File Offset: 0x0006B012
		protected virtual string GetKey(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} is a licensed component.", type.FullName);
		}

		/// <summary>Returns a license for the instance of the component, if one is available.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies where you can use the licensed object.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />.</param>
		/// <param name="instance">An object that requests the <see cref="T:System.ComponentModel.License" />.</param>
		/// <param name="allowExceptions">
		///   <see langword="true" /> if a <see cref="T:System.ComponentModel.LicenseException" /> should be thrown when a component cannot be granted a license; otherwise, <see langword="false" />.</param>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />. If this method cannot find a valid <see cref="T:System.ComponentModel.License" /> or a valid <paramref name="context" /> parameter, it returns <see langword="null" />.</returns>
		// Token: 0x06001F41 RID: 8001 RVA: 0x0006CE2C File Offset: 0x0006B02C
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			LicFileLicenseProvider.LicFileLicense licFileLicense = null;
			if (context != null)
			{
				if (context.UsageMode == LicenseUsageMode.Runtime)
				{
					string savedLicenseKey = context.GetSavedLicenseKey(type, null);
					if (savedLicenseKey != null && this.IsKeyValid(savedLicenseKey, type))
					{
						licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, savedLicenseKey);
					}
				}
				if (licFileLicense == null)
				{
					string text = null;
					if (context != null)
					{
						ITypeResolutionService typeResolutionService = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
						if (typeResolutionService != null)
						{
							text = typeResolutionService.GetPathOfAssembly(type.Assembly.GetName());
						}
					}
					if (text == null)
					{
						text = type.Module.FullyQualifiedName;
					}
					string path = Path.GetDirectoryName(text) + "\\" + type.FullName + ".lic";
					if (File.Exists(path))
					{
						StreamReader streamReader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
						string key = streamReader.ReadLine();
						streamReader.Close();
						if (this.IsKeyValid(key, type))
						{
							licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, this.GetKey(type));
						}
					}
					if (licFileLicense != null)
					{
						context.SetSavedLicenseKey(type, licFileLicense.LicenseKey);
					}
				}
			}
			return licFileLicense;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicFileLicenseProvider" /> class.</summary>
		// Token: 0x06001F42 RID: 8002 RVA: 0x0006CF18 File Offset: 0x0006B118
		public LicFileLicenseProvider()
		{
		}

		// Token: 0x020003C5 RID: 965
		private class LicFileLicense : License
		{
			// Token: 0x06001F43 RID: 8003 RVA: 0x0006CF20 File Offset: 0x0006B120
			public LicFileLicense(LicFileLicenseProvider owner, string key)
			{
				this._owner = owner;
				this.LicenseKey = key;
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0006CF36 File Offset: 0x0006B136
			public override string LicenseKey
			{
				[CompilerGenerated]
				get
				{
					return this.<LicenseKey>k__BackingField;
				}
			}

			// Token: 0x06001F45 RID: 8005 RVA: 0x0006CF3E File Offset: 0x0006B13E
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000F4B RID: 3915
			private LicFileLicenseProvider _owner;

			// Token: 0x04000F4C RID: 3916
			[CompilerGenerated]
			private readonly string <LicenseKey>k__BackingField;
		}
	}
}
