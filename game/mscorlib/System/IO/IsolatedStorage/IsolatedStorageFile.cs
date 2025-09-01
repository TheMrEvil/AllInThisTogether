using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono.Security.Cryptography;
using Unity;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents an isolated storage area containing files and directories.</summary>
	// Token: 0x02000B73 RID: 2931
	[ComVisible(true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	public sealed class IsolatedStorageFile : IsolatedStorage, IDisposable
	{
		/// <summary>Gets the enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within an isolated storage scope.</summary>
		/// <param name="scope">Represents the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> for which to return isolated stores. <see langword="User" /> and <see langword="User|Roaming" /> are the only <see langword="IsolatedStorageScope" /> combinations supported.</param>
		/// <returns>Enumerator for the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> stores within the specified isolated storage scope.</returns>
		// Token: 0x06006A8D RID: 27277 RVA: 0x0016C98E File Offset: 0x0016AB8E
		public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.Demand(scope);
			if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
			{
				throw new ArgumentException(Locale.GetText("Invalid scope, only User, User|Roaming and Machine are valid"));
			}
			return new IsolatedStorageFileEnumerator(scope, IsolatedStorageFile.GetIsolatedStorageRoot(scope));
		}

		/// <summary>Obtains isolated storage corresponding to the given application domain and the assembly evidence objects and types.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainEvidence">An object that contains the application domain identity.</param>
		/// <param name="domainEvidenceType">The identity type to choose from the application domain evidence.</param>
		/// <param name="assemblyEvidence">An object that contains the code assembly identity.</param>
		/// <param name="assemblyEvidenceType">The identity type to choose from the application code assembly evidence.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="domainEvidence" /> or <paramref name="assemblyEvidence" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A8E RID: 27278 RVA: 0x0016C9C0 File Offset: 0x0016ABC0
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			bool flag = (scope & IsolatedStorageScope.Domain) > IsolatedStorageScope.None;
			if (flag && domainEvidence == null)
			{
				throw new ArgumentNullException("domainEvidence");
			}
			bool flag2 = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag2 && assemblyEvidence == null)
			{
				throw new ArgumentNullException("assemblyEvidence");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				if (domainEvidenceType == null)
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(domainEvidence);
				}
				else
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(domainEvidence, domainEvidenceType);
				}
				if (isolatedStorageFile._domainIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find domain identity."));
				}
			}
			if (flag2)
			{
				if (assemblyEvidenceType == null)
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(assemblyEvidence);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(assemblyEvidence, assemblyEvidenceType);
				}
				if (isolatedStorageFile._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find assembly identity."));
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains the isolated storage corresponding to the given application domain and assembly evidence objects.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainIdentity">An object that contains evidence for the application domain identity.</param>
		/// <param name="assemblyIdentity">An object that contains evidence for the code assembly identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">Neither <paramref name="domainIdentity" /> nor <paramref name="assemblyIdentity" /> has been passed in. This verifies that the correct constructor is being used.  
		///  -or-  
		///  Either <paramref name="domainIdentity" /> or <paramref name="assemblyIdentity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A8F RID: 27279 RVA: 0x0016CA90 File Offset: 0x0016AC90
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None && domainIdentity == null)
			{
				throw new ArgumentNullException("domainIdentity");
			}
			bool flag = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag && assemblyIdentity == null)
			{
				throw new ArgumentNullException("assemblyIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			}
			isolatedStorageFile._domainIdentity = domainIdentity;
			isolatedStorageFile._assemblyIdentity = assemblyIdentity;
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the isolated storage scope given the application domain and assembly evidence types.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="domainEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <param name="assemblyEvidenceType">The type of the <see cref="T:System.Security.Policy.Evidence" /> that you can chose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The evidence type provided is missing in the assembly evidence list.  
		///  -or-  
		///  An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A90 RID: 27280 RVA: 0x0016CAFC File Offset: 0x0016ACFC
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
			{
				if (domainEvidenceType == null)
				{
					domainEvidenceType = typeof(Url);
				}
				isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(AppDomain.CurrentDomain.Evidence, domainEvidenceType);
			}
			if ((scope & IsolatedStorageScope.Assembly) != IsolatedStorageScope.None)
			{
				Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
				isolatedStorageFile._fullEvidences = evidence;
				if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
				{
					if (assemblyEvidenceType == null)
					{
						assemblyEvidenceType = typeof(Url);
					}
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(evidence, assemblyEvidenceType);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the given application identity.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="applicationIdentity">An object that contains evidence for the application identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The  <paramref name="applicationIdentity" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined,because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A91 RID: 27281 RVA: 0x0016CB99 File Offset: 0x0016AD99
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile._applicationIdentity = applicationIdentity;
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains isolated storage corresponding to the isolation scope and the application identity object.</summary>
		/// <param name="scope">A bitwise combination of the enumeration values.</param>
		/// <param name="applicationEvidenceType">An object that contains the application identity.</param>
		/// <returns>An object that represents the parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The   <paramref name="applicationEvidence" /> identity has not been passed in.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="scope" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  <paramref name="scope" /> contains the enumeration value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />, but the application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> for  the current application domain returned <see langword="null" />.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />, but the permissions for the application domain cannot be determined.  
		///  -or-  
		///  <paramref name="scope" /> contains the value <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />, but the permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A92 RID: 27282 RVA: 0x0016CBD2 File Offset: 0x0016ADD2
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile.InitStore(scope, applicationEvidenceType);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's application identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The application identity of the caller could not be determined.  
		///  -or-  
		///  The granted permission set for the application domain could not be determined.  
		///  -or-  
		///  An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		// Token: 0x06006A93 RID: 27283 RVA: 0x0016CC00 File Offset: 0x0016AE00
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForApplication()
		{
			IsolatedStorageScope scope = IsolatedStorageScope.Machine | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile.InitStore(scope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.</exception>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		// Token: 0x06006A94 RID: 27284 RVA: 0x0016CC34 File Offset: 0x0016AE34
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains machine-scoped isolated storage corresponding to the application domain identity and the assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.  
		///  -or-  
		///  The assembly specified has insufficient permissions to create isolated stores.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.  
		///  -or-  
		///  An isolated storage location cannot be initialized.</exception>
		// Token: 0x06006A95 RID: 27285 RVA: 0x0016CC6C File Offset: 0x0016AE6C
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's application identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  The application identity of the caller cannot be determined, because the <see cref="P:System.AppDomain.ActivationContext" /> property returned <see langword="null" />.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.</exception>
		// Token: 0x06006A96 RID: 27286 RVA: 0x0016CCBC File Offset: 0x0016AEBC
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForApplication()
		{
			IsolatedStorageScope scope = IsolatedStorageScope.User | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile.InitStore(scope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the calling code's assembly identity.</summary>
		/// <returns>An object corresponding to the isolated storage scope based on the calling code's assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage location cannot be initialized.  
		///  -or-  
		///  The permissions for the calling assembly cannot be determined.</exception>
		// Token: 0x06006A97 RID: 27287 RVA: 0x0016CCF0 File Offset: 0x0016AEF0
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Assembly);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains user-scoped isolated storage corresponding to the application domain identity and assembly identity.</summary>
		/// <returns>An object corresponding to the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />, based on a combination of the application domain identity and the assembly identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">Sufficient isolated storage permissions have not been granted.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The store failed to open.  
		///  -or-  
		///  The assembly specified has insufficient permissions to create isolated stores.  
		///  -or-  
		///  An isolated storage location cannot be initialized.  
		///  -or-  
		///  The permissions for the application domain cannot be determined.</exception>
		// Token: 0x06006A98 RID: 27288 RVA: 0x0016CD28 File Offset: 0x0016AF28
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		/// <summary>Obtains a user-scoped isolated store for use by applications in a virtual host domain.</summary>
		/// <returns>The isolated storage file that corresponds to the isolated storage scope based on the calling code's application identity.</returns>
		// Token: 0x06006A99 RID: 27289 RVA: 0x000472C8 File Offset: 0x000454C8
		[ComVisible(false)]
		public static IsolatedStorageFile GetUserStoreForSite()
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the specified isolated storage scope for all identities.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be removed.</exception>
		// Token: 0x06006A9A RID: 27290 RVA: 0x0016CD74 File Offset: 0x0016AF74
		public static void Remove(IsolatedStorageScope scope)
		{
			string isolatedStorageRoot = IsolatedStorageFile.GetIsolatedStorageRoot(scope);
			if (!Directory.Exists(isolatedStorageRoot))
			{
				return;
			}
			try
			{
				Directory.Delete(isolatedStorageRoot, true);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x0016CDB8 File Offset: 0x0016AFB8
		internal static string GetIsolatedStorageRoot(IsolatedStorageScope scope)
		{
			string text = null;
			if ((scope & IsolatedStorageScope.User) != IsolatedStorageScope.None)
			{
				if ((scope & IsolatedStorageScope.Roaming) != IsolatedStorageScope.None)
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
				}
				else
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
				}
			}
			else if ((scope & IsolatedStorageScope.Machine) != IsolatedStorageScope.None)
			{
				text = Environment.UnixGetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create);
			}
			if (text == null)
			{
				throw new IsolatedStorageException(string.Format(Locale.GetText("Couldn't access storage location for '{0}'."), scope));
			}
			return Path.Combine(text, ".isolated-storage");
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0016CE2B File Offset: 0x0016B02B
		private static void Demand(IsolatedStorageScope scope)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new IsolatedStorageFilePermission(PermissionState.None)
				{
					UsageAllowed = IsolatedStorageFile.ScopeToContainment(scope)
				}.Demand();
			}
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0016CE4C File Offset: 0x0016B04C
		private static IsolatedStorageContainment ScopeToContainment(IsolatedStorageScope scope)
		{
			if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
			{
				if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.AssemblyIsolationByUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.DomainIsolationByUser;
					}
				}
				else
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.AssemblyIsolationByRoamingUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.DomainIsolationByRoamingUser;
					}
				}
			}
			else if (scope <= (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
			{
				if (scope == (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.AssemblyIsolationByMachine;
				}
				if (scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.DomainIsolationByMachine;
				}
			}
			else
			{
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByUser;
				}
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByRoamingUser;
				}
				if (scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByMachine;
				}
			}
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0016CEBC File Offset: 0x0016B0BC
		internal static ulong GetDirectorySize(DirectoryInfo di)
		{
			ulong num = 0UL;
			foreach (FileInfo fileInfo in di.GetFiles())
			{
				num += (ulong)fileInfo.Length;
			}
			foreach (DirectoryInfo di2 in di.GetDirectories())
			{
				num += IsolatedStorageFile.GetDirectorySize(di2);
			}
			return num;
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0016CF16 File Offset: 0x0016B116
		private IsolatedStorageFile(IsolatedStorageScope scope)
		{
			this.storage_scope = scope;
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x0016CF25 File Offset: 0x0016B125
		internal IsolatedStorageFile(IsolatedStorageScope scope, string location)
		{
			this.storage_scope = scope;
			this.directory = new DirectoryInfo(location);
			if (!this.directory.Exists)
			{
				throw new IsolatedStorageException(Locale.GetText("Invalid storage."));
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06006AA1 RID: 27297 RVA: 0x0016CF60 File Offset: 0x0016B160
		~IsolatedStorageFile()
		{
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x0016CF88 File Offset: 0x0016B188
		private void PostInit()
		{
			string text = IsolatedStorageFile.GetIsolatedStorageRoot(base.Scope);
			string path;
			if (this._applicationIdentity != null)
			{
				path = string.Format("a{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._applicationIdentity));
			}
			else if (this._domainIdentity != null)
			{
				path = string.Format("d{0}{1}{0}{2}", this.SeparatorInternal, this.GetNameFromIdentity(this._domainIdentity), this.GetNameFromIdentity(this._assemblyIdentity));
			}
			else
			{
				if (this._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("No code identity available."));
				}
				path = string.Format("d{0}none{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._assemblyIdentity));
			}
			text = Path.Combine(text, path);
			this.directory = new DirectoryInfo(text);
			if (!this.directory.Exists)
			{
				try
				{
					this.directory.Create();
					this.SaveIdentities(text);
				}
				catch (IOException)
				{
				}
			}
		}

		/// <summary>Gets the current size of the isolated storage.</summary>
		/// <returns>The total number of bytes of storage currently in use within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. The current store has a roaming scope or is not open.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object size is undefined.</exception>
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06006AA3 RID: 27299 RVA: 0x0016D08C File Offset: 0x0016B28C
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong CurrentSize
		{
			get
			{
				return IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage within the limits established by the quota.</summary>
		/// <returns>The limit of isolated storage space in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is unavailable. <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFile.MaximumSize" /> cannot be determined without evidence from the assembly's creation. The evidence could not be determined when the object was created.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">An isolated storage error occurred.</exception>
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x0016D09C File Offset: 0x0016B29C
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong MaximumSize
		{
			get
			{
				if (!SecurityManager.SecurityEnabled)
				{
					return 9223372036854775807UL;
				}
				if (this._resolved)
				{
					return this._maxSize;
				}
				Evidence evidence;
				if (this._fullEvidences != null)
				{
					evidence = this._fullEvidences;
				}
				else
				{
					evidence = new Evidence();
					if (this._assemblyIdentity != null)
					{
						evidence.AddHost(this._assemblyIdentity);
					}
				}
				if (evidence.Count < 1)
				{
					throw new InvalidOperationException(Locale.GetText("Couldn't get the quota from the available evidences."));
				}
				PermissionSet permissionSet = null;
				PermissionSet permissionSet2 = SecurityManager.ResolvePolicy(evidence, null, null, null, out permissionSet);
				IsolatedStoragePermission permission = this.GetPermission(permissionSet2);
				if (permission == null)
				{
					if (!permissionSet2.IsUnrestricted())
					{
						throw new InvalidOperationException(Locale.GetText("No quota from the available evidences."));
					}
					this._maxSize = 9223372036854775807UL;
				}
				else
				{
					this._maxSize = (ulong)permission.UserQuota;
				}
				this._resolved = true;
				return this._maxSize;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06006AA5 RID: 27301 RVA: 0x0016D16A File Offset: 0x0016B36A
		internal string Root
		{
			get
			{
				return this.directory.FullName;
			}
		}

		/// <summary>Gets a value that represents the amount of free space available for isolated storage.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x0016D177 File Offset: 0x0016B377
		[ComVisible(false)]
		public override long AvailableFreeSpace
		{
			get
			{
				this.CheckOpen();
				return long.MaxValue;
			}
		}

		/// <summary>Gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06006AA7 RID: 27303 RVA: 0x0016D188 File Offset: 0x0016B388
		[ComVisible(false)]
		public override long Quota
		{
			get
			{
				this.CheckOpen();
				return (long)this.MaximumSize;
			}
		}

		/// <summary>Gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x0016D196 File Offset: 0x0016B396
		[ComVisible(false)]
		public override long UsedSize
		{
			get
			{
				this.CheckOpen();
				return (long)IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		/// <summary>Gets a value that indicates whether isolated storage is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06006AA9 RID: 27305 RVA: 0x000040F7 File Offset: 0x000022F7
		[ComVisible(false)]
		public static bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06006AAA RID: 27306 RVA: 0x0016D1A9 File Offset: 0x0016B3A9
		internal bool IsClosed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06006AAB RID: 27307 RVA: 0x0016D1B1 File Offset: 0x0016B3B1
		internal bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		/// <summary>Closes a store previously opened with <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope,System.Type,System.Type)" />, <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly" />, or <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain" />.</summary>
		// Token: 0x06006AAC RID: 27308 RVA: 0x0016D1B9 File Offset: 0x0016B3B9
		public void Close()
		{
			this.closed = true;
		}

		/// <summary>Creates a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to create within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The current code has insufficient permissions to create isolated storage directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path is <see langword="null" />.</exception>
		// Token: 0x06006AAD RID: 27309 RVA: 0x0016D1C4 File Offset: 0x0016B3C4
		public void CreateDirectory(string dir)
		{
			if (dir == null)
			{
				throw new ArgumentNullException("dir");
			}
			if (dir.IndexOfAny(Path.PathSeparatorChars) >= 0)
			{
				string[] array = dir.Split(Path.PathSeparatorChars, StringSplitOptions.RemoveEmptyEntries);
				DirectoryInfo directoryInfo = this.directory;
				for (int i = 0; i < array.Length; i++)
				{
					if (directoryInfo.GetFiles(array[i]).Length != 0)
					{
						throw new IsolatedStorageException("Unable to create directory.");
					}
					directoryInfo = directoryInfo.CreateSubdirectory(array[i]);
				}
				return;
			}
			if (this.directory.GetFiles(dir).Length != 0)
			{
				throw new IsolatedStorageException("Unable to create directory.");
			}
			this.directory.CreateSubdirectory(dir);
		}

		/// <summary>Copies an existing file to a new file.</summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  <paramref name="destinationFileName" /> exists.  
		///  -or-  
		///  An I/O error has occurred.</exception>
		// Token: 0x06006AAE RID: 27310 RVA: 0x0016D258 File Offset: 0x0016B458
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			this.CopyFile(sourceFileName, destinationFileName, false);
		}

		/// <summary>Copies an existing file to a new file, and optionally overwrites an existing file.</summary>
		/// <param name="sourceFileName">The name of the file to copy.</param>
		/// <param name="destinationFileName">The name of the destination file. This cannot be a directory.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> if the destination file can be overwritten; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  An I/O error has occurred.</exception>
		// Token: 0x06006AAF RID: 27311 RVA: 0x0016D264 File Offset: 0x0016B464
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (File.Exists(text2) && !overwrite)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Copy(text, text2, overwrite);
			}
			catch (IOException inner)
			{
				throw new IsolatedStorageException("Operation not allowed.", inner);
			}
			catch (UnauthorizedAccessException inner2)
			{
				throw new IsolatedStorageException("Operation not allowed.", inner2);
			}
		}

		/// <summary>Creates a file in the isolated store.</summary>
		/// <param name="path">The relative path of the file to create.</param>
		/// <returns>A new isolated storage file.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06006AB0 RID: 27312 RVA: 0x0016D3A8 File Offset: 0x0016B5A8
		[ComVisible(false)]
		public IsolatedStorageFileStream CreateFile(string path)
		{
			return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
		}

		/// <summary>Deletes a directory in the isolated storage scope.</summary>
		/// <param name="dir">The relative path of the directory to delete within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The directory could not be deleted.</exception>
		/// <exception cref="T:System.ArgumentNullException">The directory path was <see langword="null" />.</exception>
		// Token: 0x06006AB1 RID: 27313 RVA: 0x0016D3B4 File Offset: 0x0016B5B4
		public void DeleteDirectory(string dir)
		{
			try
			{
				if (Path.IsPathRooted(dir))
				{
					dir = dir.Substring(1);
				}
				this.directory.CreateSubdirectory(dir).Delete();
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete directory '{0}'", new object[]
				{
					dir
				}));
			}
		}

		/// <summary>Deletes a file in the isolated storage scope.</summary>
		/// <param name="file">The relative path of the file to delete within the isolated storage scope.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The target file is open or the path is incorrect.</exception>
		/// <exception cref="T:System.ArgumentNullException">The file path is <see langword="null" />.</exception>
		// Token: 0x06006AB2 RID: 27314 RVA: 0x0016D410 File Offset: 0x0016B610
		public void DeleteFile(string file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (!File.Exists(Path.Combine(this.directory.FullName, file)))
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[]
				{
					file
				}));
			}
			try
			{
				File.Delete(Path.Combine(this.directory.FullName, file));
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[]
				{
					file
				}));
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" />.</summary>
		// Token: 0x06006AB3 RID: 27315 RVA: 0x0016D4A4 File Offset: 0x0016B6A4
		public void Dispose()
		{
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Determines whether the specified path refers to an existing directory in the isolated store.</summary>
		/// <param name="path">The path to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> refers to an existing directory in the isolated store and is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006AB4 RID: 27316 RVA: 0x0016D4B4 File Offset: 0x0016B6B4
		[ComVisible(false)]
		public bool DirectoryExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string path2 = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(path2) && Directory.Exists(path2);
		}

		/// <summary>Determines whether the specified path refers to an existing file in the isolated store.</summary>
		/// <param name="path">The path and file name to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> refers to an existing file in the isolated store and is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		// Token: 0x06006AB5 RID: 27317 RVA: 0x0016D4F8 File Offset: 0x0016B6F8
		[ComVisible(false)]
		public bool FileExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string path2 = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(path2) && File.Exists(path2);
		}

		/// <summary>Returns the creation date and time of a specified file or directory.</summary>
		/// <param name="path">The path to the file or directory for which to obtain creation date and time information.</param>
		/// <returns>The creation date and time for the specified file or directory. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006AB6 RID: 27318 RVA: 0x0016D53C File Offset: 0x0016B73C
		[ComVisible(false)]
		public DateTimeOffset GetCreationTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string path2 = Path.Combine(this.directory.FullName, path);
			if (File.Exists(path2))
			{
				return File.GetCreationTime(path2);
			}
			return Directory.GetCreationTime(path2);
		}

		/// <summary>Returns the date and time a specified file or directory was last accessed.</summary>
		/// <param name="path">The path to the file or directory for which to obtain last access date and time information.</param>
		/// <returns>The date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006AB7 RID: 27319 RVA: 0x0016D5A8 File Offset: 0x0016B7A8
		[ComVisible(false)]
		public DateTimeOffset GetLastAccessTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string path2 = Path.Combine(this.directory.FullName, path);
			if (File.Exists(path2))
			{
				return File.GetLastAccessTime(path2);
			}
			return Directory.GetLastAccessTime(path2);
		}

		/// <summary>Returns the date and time a specified file or directory was last written to.</summary>
		/// <param name="path">The path to the file or directory for which to obtain last write date and time information.</param>
		/// <returns>The date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006AB8 RID: 27320 RVA: 0x0016D614 File Offset: 0x0016B814
		[ComVisible(false)]
		public DateTimeOffset GetLastWriteTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string path2 = Path.Combine(this.directory.FullName, path);
			if (File.Exists(path2))
			{
				return File.GetLastWriteTime(path2);
			}
			return Directory.GetLastWriteTime(path2);
		}

		/// <summary>Enumerates the directories in an isolated storage scope that match a given search pattern.</summary>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported.</param>
		/// <returns>An array of the relative paths of directories in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no directories that match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories resolved from <paramref name="searchPattern" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory or directories specified by <paramref name="searchPattern" /> are not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		// Token: 0x06006AB9 RID: 27321 RVA: 0x0016D680 File Offset: 0x0016B880
		public string[] GetDirectoryNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			DirectoryInfo[] array = null;
			if (directoryName == null || directoryName.Length == 0)
			{
				array = this.directory.GetDirectories(searchPattern);
			}
			else
			{
				DirectoryInfo directoryInfo = this.directory.GetDirectories(directoryName)[0];
				if (directoryInfo.FullName.IndexOf(this.directory.FullName) >= 0)
				{
					array = directoryInfo.GetDirectories(fileName);
					string[] array2 = directoryName.Split(new char[]
					{
						Path.DirectorySeparatorChar
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int i = array2.Length - 1; i >= 0; i--)
					{
						if (directoryInfo.Name != array2[i])
						{
							array = null;
							break;
						}
						directoryInfo = directoryInfo.Parent;
					}
				}
			}
			if (array == null)
			{
				throw new SecurityException();
			}
			FileSystemInfo[] afsi = array;
			return this.GetNames(afsi);
		}

		/// <summary>Enumerates the directories at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of directories at the root of the isolated store. A zero-length array specifies that there are no directories at the root.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store is closed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Caller does not have permission to enumerate directories.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">One or more directories are not found.</exception>
		// Token: 0x06006ABA RID: 27322 RVA: 0x0016D76F File Offset: 0x0016B96F
		[ComVisible(false)]
		public string[] GetDirectoryNames()
		{
			return this.GetDirectoryNames("*");
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0016D77C File Offset: 0x0016B97C
		private string[] GetNames(FileSystemInfo[] afsi)
		{
			string[] array = new string[afsi.Length];
			for (int num = 0; num != afsi.Length; num++)
			{
				array[num] = afsi[num].Name;
			}
			return array;
		}

		/// <summary>Gets the file names that match a search pattern.</summary>
		/// <param name="searchPattern">A search pattern. Both single-character ("?") and multi-character ("*") wildcards are supported.</param>
		/// <returns>An array of relative paths of files in the isolated storage scope that match <paramref name="searchPattern" />. A zero-length array specifies that there are no files that match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The file path specified by <paramref name="searchPattern" /> cannot be found.</exception>
		// Token: 0x06006ABC RID: 27324 RVA: 0x0016D7AC File Offset: 0x0016B9AC
		public string[] GetFileNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			FileInfo[] files;
			if (directoryName == null || directoryName.Length == 0)
			{
				files = this.directory.GetFiles(searchPattern);
			}
			else
			{
				DirectoryInfo[] directories = this.directory.GetDirectories(directoryName);
				if (directories.Length != 1)
				{
					throw new SecurityException();
				}
				if (!directories[0].FullName.StartsWith(this.directory.FullName))
				{
					throw new SecurityException();
				}
				if (directories[0].FullName.Substring(this.directory.FullName.Length + 1) != directoryName)
				{
					throw new SecurityException();
				}
				files = directories[0].GetFiles(fileName);
			}
			FileSystemInfo[] afsi = files;
			return this.GetNames(afsi);
		}

		/// <summary>Enumerates the file names at the root of an isolated store.</summary>
		/// <returns>An array of relative paths of files at the root of the isolated store.  A zero-length array specifies that there are no files at the root.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">File paths from the isolated store root cannot be determined.</exception>
		// Token: 0x06006ABD RID: 27325 RVA: 0x0016D885 File Offset: 0x0016BA85
		[ComVisible(false)]
		public string[] GetFileNames()
		{
			return this.GetFileNames("*");
		}

		/// <summary>Enables an application to explicitly request a larger quota size, in bytes.</summary>
		/// <param name="newQuotaSize">The requested size, in bytes.</param>
		/// <returns>
		///   <see langword="true" /> if the new quota is accepted; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="newQuotaSize" /> is less than current quota size.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="newQuotaSize" /> is less than zero, or less than or equal to the current quota size.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current scope is not for an application user.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006ABE RID: 27326 RVA: 0x0016D892 File Offset: 0x0016BA92
		[ComVisible(false)]
		public override bool IncreaseQuotaTo(long newQuotaSize)
		{
			if (newQuotaSize < this.Quota)
			{
				throw new ArgumentException();
			}
			this.CheckOpen();
			return false;
		}

		/// <summary>Moves a specified directory and its contents to a new location.</summary>
		/// <param name="sourceDirectoryName">The name of the directory to move.</param>
		/// <param name="destinationDirectoryName">The path to the new location for <paramref name="sourceDirectoryName" />. This cannot be the path to an existing directory.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="sourceDirectoryName" /> does not exist.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.  
		///  -or-  
		///  <paramref name="destinationDirectoryName" /> already exists.  
		///  -or-  
		///  <paramref name="sourceDirectoryName" /> and <paramref name="destinationDirectoryName" /> refer to the same directory.</exception>
		// Token: 0x06006ABF RID: 27327 RVA: 0x0016D8AC File Offset: 0x0016BAAC
		[ComVisible(false)]
		public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
		{
			if (sourceDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (sourceDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "sourceDirectoryName");
			}
			if (destinationDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "destinationDirectoryName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceDirectoryName);
			string text2 = Path.Combine(this.directory.FullName, destinationDirectoryName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(text))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceDirectoryName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + destinationDirectoryName + "'.");
			}
			try
			{
				Directory.Move(text, text2);
			}
			catch (IOException inner)
			{
				throw new IsolatedStorageException("Operation not allowed.", inner);
			}
			catch (UnauthorizedAccessException inner2)
			{
				throw new IsolatedStorageException("Operation not allowed.", inner2);
			}
		}

		/// <summary>Moves a specified file to a new location, and optionally lets you specify a new file name.</summary>
		/// <param name="sourceFileName">The name of the file to move.</param>
		/// <param name="destinationFileName">The path to the new location for the file. If a file name is included, the moved file will have that name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The isolated store has been closed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		// Token: 0x06006AC0 RID: 27328 RVA: 0x0016D9D8 File Offset: 0x0016BBD8
		[ComVisible(false)]
		public void MoveFile(string sourceFileName, string destinationFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Move(text, text2);
			}
			catch (UnauthorizedAccessException inner)
			{
				throw new IsolatedStorageException("Operation not allowed.", inner);
			}
		}

		/// <summary>Opens a file in the specified mode.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file.</param>
		/// <returns>A file that is opened in the specified mode, with read/write access, and is unshared.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06006AC1 RID: 27329 RVA: 0x0016DAE0 File Offset: 0x0016BCE0
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
		{
			return new IsolatedStorageFileStream(path, mode, this);
		}

		/// <summary>Opens a file in the specified mode with the specified read/write access.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access.</param>
		/// <returns>A file that is opened in the specified mode and access, and is unshared.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="F:System.IO.FileMode.Open" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06006AC2 RID: 27330 RVA: 0x0016DAEA File Offset: 0x0016BCEA
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
		{
			return new IsolatedStorageFileStream(path, mode, access, this);
		}

		/// <summary>Opens a file in the specified mode, with the specified read/write access and sharing permission.</summary>
		/// <param name="path">The relative path of the file within the isolated store.</param>
		/// <param name="mode">One of the enumeration values that specifies how to open or create the file.</param>
		/// <param name="access">One of the enumeration values that specifies whether the file will be opened with read, write, or read/write access</param>
		/// <param name="share">A bitwise combination of enumeration values that specify the type of access other <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> objects have to this file.</param>
		/// <returns>A file that is opened in the specified mode and access, and with the specified sharing options.</returns>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store has been removed.  
		///  -or-  
		///  Isolated storage is disabled.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory in <paramref name="path" /> does not exist.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">No file was found and the <paramref name="mode" /> is set to <see cref="M:System.IO.FileInfo.Open(System.IO.FileMode)" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The isolated store has been disposed.</exception>
		// Token: 0x06006AC3 RID: 27331 RVA: 0x0016DAF5 File Offset: 0x0016BCF5
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new IsolatedStorageFileStream(path, mode, access, share, this);
		}

		/// <summary>Removes the isolated storage scope and all its contents.</summary>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The isolated store cannot be deleted.</exception>
		// Token: 0x06006AC4 RID: 27332 RVA: 0x0016DB04 File Offset: 0x0016BD04
		public override void Remove()
		{
			this.CheckOpen(false);
			try
			{
				this.directory.Delete(true);
			}
			catch
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
			this.Close();
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0016DB48 File Offset: 0x0016BD48
		protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			if (ps == null)
			{
				return null;
			}
			return (IsolatedStoragePermission)ps.GetPermission(typeof(IsolatedStorageFilePermission));
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0016DB64 File Offset: 0x0016BD64
		private void CheckOpen()
		{
			this.CheckOpen(true);
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0016DB70 File Offset: 0x0016BD70
		private void CheckOpen(bool checkDirExists)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("IsolatedStorageFile");
			}
			if (this.closed)
			{
				throw new InvalidOperationException("Storage needs to be open for this operation.");
			}
			if (checkDirExists && !Directory.Exists(this.directory.FullName))
			{
				throw new IsolatedStorageException("Isolated storage has been removed or disabled.");
			}
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0016DBC3 File Offset: 0x0016BDC3
		private bool IsPathInStorage(string path)
		{
			return Path.GetFullPath(path).StartsWith(this.directory.FullName);
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x0016DBDC File Offset: 0x0016BDDC
		private string GetNameFromIdentity(object identity)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(identity.ToString());
			Array src = SHA1.Create().ComputeHash(bytes, 0, bytes.Length);
			byte[] array = new byte[10];
			Buffer.BlockCopy(src, 0, array, 0, array.Length);
			return CryptoConvert.ToHex(array);
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x0016DC24 File Offset: 0x0016BE24
		private static object GetTypeFromEvidence(Evidence e, Type t)
		{
			foreach (object obj in e)
			{
				if (obj.GetType() == t)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0016DC84 File Offset: 0x0016BE84
		internal static object GetAssemblyIdentityFromEvidence(Evidence e)
		{
			object typeFromEvidence = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Publisher));
			if (typeFromEvidence != null)
			{
				return typeFromEvidence;
			}
			typeFromEvidence = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(StrongName));
			if (typeFromEvidence != null)
			{
				return typeFromEvidence;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x0016DCD0 File Offset: 0x0016BED0
		internal static object GetDomainIdentityFromEvidence(Evidence e)
		{
			object typeFromEvidence = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(ApplicationDirectory));
			if (typeFromEvidence != null)
			{
				return typeFromEvidence;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x0016DD04 File Offset: 0x0016BF04
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void SaveIdentities(string root)
		{
			IsolatedStorageFile.Identities identities = new IsolatedStorageFile.Identities(this._applicationIdentity, this._assemblyIdentity, this._domainIdentity);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			IsolatedStorageFile.mutex.WaitOne();
			try
			{
				using (FileStream fileStream = File.Create(root + ".storage"))
				{
					binaryFormatter.Serialize(fileStream, identities);
				}
			}
			finally
			{
				IsolatedStorageFile.mutex.ReleaseMutex();
			}
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0016DD8C File Offset: 0x0016BF8C
		// Note: this type is marked as 'beforefieldinit'.
		static IsolatedStorageFile()
		{
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x000173AD File Offset: 0x000155AD
		internal IsolatedStorageFile()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04003D99 RID: 15769
		private bool _resolved;

		// Token: 0x04003D9A RID: 15770
		private ulong _maxSize;

		// Token: 0x04003D9B RID: 15771
		private Evidence _fullEvidences;

		// Token: 0x04003D9C RID: 15772
		private static readonly Mutex mutex = new Mutex();

		// Token: 0x04003D9D RID: 15773
		private bool closed;

		// Token: 0x04003D9E RID: 15774
		private bool disposed;

		// Token: 0x04003D9F RID: 15775
		private DirectoryInfo directory;

		// Token: 0x02000B74 RID: 2932
		[Serializable]
		private struct Identities
		{
			// Token: 0x06006AD0 RID: 27344 RVA: 0x0016DD98 File Offset: 0x0016BF98
			public Identities(object application, object assembly, object domain)
			{
				this.Application = application;
				this.Assembly = assembly;
				this.Domain = domain;
			}

			// Token: 0x04003DA0 RID: 15776
			public object Application;

			// Token: 0x04003DA1 RID: 15777
			public object Assembly;

			// Token: 0x04003DA2 RID: 15778
			public object Domain;
		}
	}
}
