using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Policy;
using Unity;

namespace System.Security
{
	/// <summary>Allows the control and customization of security behavior for application domains.</summary>
	// Token: 0x020003DF RID: 991
	[ComVisible(true)]
	[Serializable]
	public class HostSecurityManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.HostSecurityManager" /> class.</summary>
		// Token: 0x060028AB RID: 10411 RVA: 0x0000259F File Offset: 0x0000079F
		public HostSecurityManager()
		{
		}

		/// <summary>When overridden in a derived class, gets the security policy for the current application domain.</summary>
		/// <returns>The security policy for the current application domain. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x0000AF5E File Offset: 0x0000915E
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the flag representing the security policy components of concern to the host.</summary>
		/// <returns>One of the enumeration values that specifies security policy components. The default is <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000932EB File Offset: 0x000914EB
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		/// <summary>Determines whether an application should be executed.</summary>
		/// <param name="applicationEvidence">The evidence for the application to be activated.</param>
		/// <param name="activatorEvidence">Optionally, the evidence for the activating application domain.</param>
		/// <param name="context">The trust context.</param>
		/// <returns>An object that contains trust information about the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationEvidence" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object could not be found in the application evidence.  
		///  -or-  
		///  The <see cref="P:System.Runtime.Hosting.ActivationArguments.ActivationContext" /> property in the activation arguments is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Security.Policy.ApplicationTrust" /> grant set does not contain the minimum request set specified by the <see cref="T:System.ActivationContext" />.</exception>
		// Token: 0x060028AE RID: 10414 RVA: 0x000932F0 File Offset: 0x000914F0
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			ActivationArguments activationArguments = null;
			foreach (object obj in applicationEvidence)
			{
				activationArguments = (obj as ActivationArguments);
				if (activationArguments != null)
				{
					break;
				}
			}
			if (activationArguments == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("No {0} found in {1}."), "ActivationArguments", "Evidence"), "applicationEvidence");
			}
			if (activationArguments.ActivationContext == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("No {0} found in {1}."), "ActivationContext", "ActivationArguments"), "applicationEvidence");
			}
			if (!ApplicationSecurityManager.DetermineApplicationTrust(activationArguments.ActivationContext, context))
			{
				return null;
			}
			if (activationArguments.ApplicationIdentity == null)
			{
				return new ApplicationTrust();
			}
			return new ApplicationTrust(activationArguments.ApplicationIdentity);
		}

		/// <summary>Provides the application domain evidence for an assembly being loaded.</summary>
		/// <param name="inputEvidence">Additional evidence to add to the <see cref="T:System.AppDomain" /> evidence.</param>
		/// <returns>The evidence to be used for the <see cref="T:System.AppDomain" />.</returns>
		// Token: 0x060028AF RID: 10415 RVA: 0x00002731 File Offset: 0x00000931
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Provides the assembly evidence for an assembly being loaded.</summary>
		/// <param name="loadedAssembly">The loaded assembly.</param>
		/// <param name="inputEvidence">Additional evidence to add to the assembly evidence.</param>
		/// <returns>The evidence to be used for the assembly.</returns>
		// Token: 0x060028B0 RID: 10416 RVA: 0x00088677 File Offset: 0x00086877
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		/// <returns>The permission set that can be granted by the security system.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is <see langword="null" />.</exception>
		// Token: 0x060028B1 RID: 10417 RVA: 0x000933D0 File Offset: 0x000915D0
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new NullReferenceException("evidence");
			}
			return SecurityManager.ResolvePolicy(evidence);
		}

		/// <summary>Requests a specific evidence type for the application domain.</summary>
		/// <param name="evidenceType">The evidence type.</param>
		/// <returns>The requested application domain evidence.</returns>
		// Token: 0x060028B2 RID: 10418 RVA: 0x00052955 File Offset: 0x00050B55
		public virtual EvidenceBase GenerateAppDomainEvidence(Type evidenceType)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Requests a specific evidence type for the assembly.</summary>
		/// <param name="evidenceType">The evidence type.</param>
		/// <param name="assembly">The target assembly.</param>
		/// <returns>The requested assembly evidence.</returns>
		// Token: 0x060028B3 RID: 10419 RVA: 0x00052955 File Offset: 0x00050B55
		public virtual EvidenceBase GenerateAssemblyEvidence(Type evidenceType, Assembly assembly)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Determines which evidence types the host can supply for the application domain, if requested.</summary>
		/// <returns>An array of evidence types.</returns>
		// Token: 0x060028B4 RID: 10420 RVA: 0x00052955 File Offset: 0x00050B55
		public virtual Type[] GetHostSuppliedAppDomainEvidenceTypes()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Determines which evidence types the host can supply for the assembly, if requested.</summary>
		/// <param name="assembly">The target assembly.</param>
		/// <returns>An array of evidence types.</returns>
		// Token: 0x060028B5 RID: 10421 RVA: 0x00052955 File Offset: 0x00050B55
		public virtual Type[] GetHostSuppliedAssemblyEvidenceTypes(Assembly assembly)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
