using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000418 RID: 1048
	internal class MonoTrustManager : IApplicationTrustManager, ISecurityEncodable
	{
		// Token: 0x06002AC6 RID: 10950 RVA: 0x0009A7C0 File Offset: 0x000989C0
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
		public ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return null;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0009A7D1 File Offset: 0x000989D1
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.Tag != "IApplicationTrustManager")
			{
				throw new ArgumentException("e", Locale.GetText("Invalid XML tag."));
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0009A808 File Offset: 0x00098A08
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			securityElement.AddAttribute("class", typeof(MonoTrustManager).AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0000259F File Offset: 0x0000079F
		public MonoTrustManager()
		{
		}

		// Token: 0x04001FA0 RID: 8096
		private const string tag = "IApplicationTrustManager";
	}
}
