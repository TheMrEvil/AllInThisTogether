using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Provides a permission structure that allows methods or classes to access configuration files.</summary>
	// Token: 0x02000027 RID: 39
	[Serializable]
	public sealed class ConfigurationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermission" /> class.</summary>
		/// <param name="state">The permission level to grant.</param>
		/// <exception cref="T:System.ArgumentException">The value of <paramref name="state" /> is neither <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> nor <see cref="F:System.Security.Permissions.PermissionState.None" />.</exception>
		// Token: 0x0600015A RID: 346 RVA: 0x00006114 File Offset: 0x00004314
		public ConfigurationPermission(PermissionState state)
		{
			this.unrestricted = (state == PermissionState.Unrestricted);
		}

		/// <summary>Returns a new <see cref="T:System.Configuration.ConfigurationPermission" /> object with the same permission level.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationPermission" /> with the same permission level.</returns>
		// Token: 0x0600015B RID: 347 RVA: 0x00006126 File Offset: 0x00004326
		public override IPermission Copy()
		{
			return new ConfigurationPermission(this.unrestricted ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Reads the value of the permission state from XML.</summary>
		/// <param name="securityElement">The configuration element from which the permission state is read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see langword="unrestricted" /> attribute for the given <paramref name="securityElement" /> is neither <see langword="true" /> nor <see langword="false" />.
		/// -or-
		/// The <see cref="P:System.Security.SecurityElement.Tag" /> for the given <paramref name="securityElement" /> does not equal "IPermission".
		/// -or-
		/// The <see langword="class" /> attribute of the given <paramref name="securityElement " /> is <see langword="null" /> or is not the type name for <see cref="T:System.Configuration.ConfigurationPermission" />.
		/// -or-
		/// The <see langword="version" /> attribute for the given <paramref name="securityElement" /> does not equal 1.</exception>
		// Token: 0x0600015C RID: 348 RVA: 0x0000613C File Offset: 0x0000433C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.unrestricted = (string.Compare(text, "true", StringComparison.InvariantCultureIgnoreCase) == 0);
			}
		}

		/// <summary>Returns the logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> object and a given object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object containing the permissions to perform the intersection with.</param>
		/// <returns>The logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> and a given object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x0600015D RID: 349 RVA: 0x00006198 File Offset: 0x00004398
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((this.unrestricted && configurationPermission.IsUnrestricted()) ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Returns the logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> object and an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object to perform the union with.</param>
		/// <returns>The logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> and an object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x0600015E RID: 350 RVA: 0x000061D8 File Offset: 0x000043D8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((this.unrestricted || configurationPermission.IsUnrestricted()) ? PermissionState.Unrestricted : PermissionState.None);
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationPermission" /> object with an object implementing the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <param name="target">The object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if the permission state is equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x0600015F RID: 351 RVA: 0x00006220 File Offset: 0x00004420
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.unrestricted;
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return !this.unrestricted || configurationPermission.IsUnrestricted();
		}

		/// <summary>Indicates whether the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> object is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000160 RID: 352 RVA: 0x0000625F File Offset: 0x0000445F
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Returns a <see cref="T:System.Security.SecurityElement" /> object with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" />.</returns>
		// Token: 0x06000161 RID: 353 RVA: 0x00006268 File Offset: 0x00004468
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x0400009D RID: 157
		private bool unrestricted;
	}
}
