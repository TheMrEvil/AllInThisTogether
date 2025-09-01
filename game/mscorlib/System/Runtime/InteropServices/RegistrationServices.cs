using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services for registering and unregistering managed assemblies for use from COM.</summary>
	// Token: 0x0200074E RID: 1870
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("475e398f-8afa-43a7-a3be-f4ef8d6787c9")]
	[ComVisible(true)]
	public class RegistrationServices : IRegistrationServices
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.RegistrationServices" /> class.</summary>
		// Token: 0x06004238 RID: 16952 RVA: 0x0000259F File Offset: 0x0000079F
		public RegistrationServices()
		{
		}

		/// <summary>Returns the GUID of the COM category that contains the managed classes.</summary>
		/// <returns>The GUID of the COM category that contains the managed classes.</returns>
		// Token: 0x06004239 RID: 16953 RVA: 0x000E3A8E File Offset: 0x000E1C8E
		public virtual Guid GetManagedCategoryGuid()
		{
			return RegistrationServices.guidManagedCategory;
		}

		/// <summary>Retrieves the COM ProgID for the specified type.</summary>
		/// <param name="type">The type corresponding to the ProgID that is being requested.</param>
		/// <returns>The ProgID for the specified type.</returns>
		// Token: 0x0600423A RID: 16954 RVA: 0x000E3A95 File Offset: 0x000E1C95
		[SecurityCritical]
		public virtual string GetProgIdForType(Type type)
		{
			return Marshal.GenerateProgIdForType(type);
		}

		/// <summary>Retrieves a list of classes in an assembly that would be registered by a call to <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.</summary>
		/// <param name="assembly">The assembly to search for classes.</param>
		/// <returns>A <see cref="T:System.Type" /> array containing a list of classes in <paramref name="assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600423B RID: 16955 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("implement")]
		[SecurityCritical]
		public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the classes in a managed assembly to enable creation from COM.</summary>
		/// <param name="assembly">The assembly to be registered.</param>
		/// <param name="flags">An <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> value indicating any special settings used when registering <paramref name="assembly" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="assembly" /> contains types that were successfully registered; otherwise <see langword="false" /> if the assembly contains no eligible types.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is <see langword="null" />.  
		///  -or-  
		///  A method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not valid.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom registration function (marked with the <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> attribute) throws an exception.</exception>
		// Token: 0x0600423C RID: 16956 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityCritical]
		[MonoTODO("implement")]
		public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the specified type with COM using the specified GUID.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to be registered for use from COM.</param>
		/// <param name="g">The <see cref="T:System.Guid" /> used to register the specified type.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		// Token: 0x0600423D RID: 16957 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityCritical]
		[MonoTODO("implement")]
		public virtual void RegisterTypeForComClients(Type type, ref Guid g)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent.</summary>
		/// <param name="type">The type to check for being a COM type.</param>
		/// <returns>
		///   <see langword="true" /> if a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent; otherwise <see langword="false" />.</returns>
		// Token: 0x0600423E RID: 16958 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("implement")]
		[SecuritySafeCritical]
		public virtual bool TypeRepresentsComType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified type requires registration.</summary>
		/// <param name="type">The type to check for COM registration requirements.</param>
		/// <returns>
		///   <see langword="true" /> if the type must be registered for use from COM; otherwise <see langword="false" />.</returns>
		// Token: 0x0600423F RID: 16959 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("implement")]
		[SecurityCritical]
		public virtual bool TypeRequiresRegistration(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unregisters the classes in a managed assembly.</summary>
		/// <param name="assembly">The assembly to be unregistered.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="assembly" /> contains types that were successfully unregistered; otherwise <see langword="false" /> if the assembly contains no eligible types.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is <see langword="null" />.  
		///  -or-  
		///  A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not <see langword="static" />.  
		///  -or-  
		///  There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.  
		///  -or-  
		///  The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom unregistration function (marked with the <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> attribute) throws an exception.</exception>
		// Token: 0x06004240 RID: 16960 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("implement")]
		[SecurityCritical]
		public virtual bool UnregisterAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the specified type with COM using the specified execution context and connection type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> object to register for use from COM.</param>
		/// <param name="classContext">One of the <see cref="T:System.Runtime.InteropServices.RegistrationClassContext" /> values that indicates the context in which the executable code will be run.</param>
		/// <param name="flags">One of the <see cref="T:System.Runtime.InteropServices.RegistrationConnectionType" /> values that specifies how connections are made to the class object.</param>
		/// <returns>An integer that represents a cookie value.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		// Token: 0x06004241 RID: 16961 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ComVisible(false)]
		[MonoTODO("implement")]
		public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes references to a type registered with the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method.</summary>
		/// <param name="cookie">The cookie value returned by a previous call to the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method overload.</param>
		// Token: 0x06004242 RID: 16962 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ComVisible(false)]
		[MonoTODO("implement")]
		public virtual void UnregisterTypeForComClients(int cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000E3A9D File Offset: 0x000E1C9D
		// Note: this type is marked as 'beforefieldinit'.
		static RegistrationServices()
		{
		}

		// Token: 0x04002BF1 RID: 11249
		private static Guid guidManagedCategory = new Guid("{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}");
	}
}
