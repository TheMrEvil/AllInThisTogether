using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides properties and methods to add a license to a component and to manage a <see cref="T:System.ComponentModel.LicenseProvider" />. This class cannot be inherited.</summary>
	// Token: 0x020003C8 RID: 968
	public sealed class LicenseManager
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x0000219B File Offset: 0x0000039B
		private LicenseManager()
		{
		}

		/// <summary>Gets or sets the current <see cref="T:System.ComponentModel.LicenseContext" />, which specifies when you can use the licensed object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property is currently locked and cannot be changed.</exception>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006CF48 File Offset: 0x0006B148
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0006CFA8 File Offset: 0x0006B1A8
		public static LicenseContext CurrentContext
		{
			get
			{
				if (LicenseManager.s_context == null)
				{
					object obj = LicenseManager.s_internalSyncObject;
					lock (obj)
					{
						if (LicenseManager.s_context == null)
						{
							LicenseManager.s_context = new RuntimeLicenseContext();
						}
					}
				}
				return LicenseManager.s_context;
			}
			set
			{
				object obj = LicenseManager.s_internalSyncObject;
				lock (obj)
				{
					if (LicenseManager.s_contextLockHolder != null)
					{
						throw new InvalidOperationException("The CurrentContext property of the LicenseManager is currently locked and cannot be changed.");
					}
					LicenseManager.s_context = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.LicenseUsageMode" /> which specifies when you can use the licensed object for the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values, as specified in the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0006CFFC File Offset: 0x0006B1FC
		public static LicenseUsageMode UsageMode
		{
			get
			{
				if (LicenseManager.s_context != null)
				{
					return LicenseManager.s_context.UsageMode;
				}
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0006D018 File Offset: 0x0006B218
		private static void CacheProvider(Type type, LicenseProvider provider)
		{
			if (LicenseManager.s_providers == null)
			{
				LicenseManager.s_providers = new Hashtable();
			}
			LicenseManager.s_providers[type] = provider;
			if (provider != null)
			{
				if (LicenseManager.s_providerInstances == null)
				{
					LicenseManager.s_providerInstances = new Hashtable();
				}
				LicenseManager.s_providerInstances[provider.GetType()] = provider;
			}
		}

		/// <summary>Creates an instance of the specified type, given a context in which you can use the licensed instance.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance.</param>
		/// <returns>An instance of the specified type.</returns>
		// Token: 0x06001F53 RID: 8019 RVA: 0x0006D073 File Offset: 0x0006B273
		public static object CreateWithContext(Type type, LicenseContext creationContext)
		{
			return LicenseManager.CreateWithContext(type, creationContext, Array.Empty<object>());
		}

		/// <summary>Creates an instance of the specified type with the specified arguments, given a context in which you can use the licensed instance.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance.</param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> that represents the arguments for the type.</param>
		/// <returns>An instance of the specified type with the given array of arguments.</returns>
		// Token: 0x06001F54 RID: 8020 RVA: 0x0006D084 File Offset: 0x0006B284
		public static object CreateWithContext(Type type, LicenseContext creationContext, object[] args)
		{
			object result = null;
			object obj = LicenseManager.s_internalSyncObject;
			lock (obj)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = creationContext;
					LicenseManager.LockContext(LicenseManager.s_selfLock);
					try
					{
						result = SecurityUtils.SecureCreateInstance(type, args);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				finally
				{
					LicenseManager.UnlockContext(LicenseManager.s_selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return result;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0006D110 File Offset: 0x0006B310
		private static bool GetCachedNoLicenseProvider(Type type)
		{
			return LicenseManager.s_providers != null && LicenseManager.s_providers.ContainsKey(type);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0006D12A File Offset: 0x0006B32A
		private static LicenseProvider GetCachedProvider(Type type)
		{
			Hashtable hashtable = LicenseManager.s_providers;
			return (LicenseProvider)((hashtable != null) ? hashtable[type] : null);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0006D145 File Offset: 0x0006B345
		private static LicenseProvider GetCachedProviderInstance(Type providerType)
		{
			Hashtable hashtable = LicenseManager.s_providerInstances;
			return (LicenseProvider)((hashtable != null) ? hashtable[providerType] : null);
		}

		/// <summary>Returns whether the given type has a valid license.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to find a valid license for.</param>
		/// <returns>
		///   <see langword="true" /> if the given type is licensed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F58 RID: 8024 RVA: 0x0006D160 File Offset: 0x0006B360
		public static bool IsLicensed(Type type)
		{
			License license;
			bool result = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return result;
		}

		/// <summary>Determines whether a valid license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the <see cref="T:System.ComponentModel.License" />.</param>
		/// <returns>
		///   <see langword="true" /> if a valid license can be granted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F59 RID: 8025 RVA: 0x0006D184 File Offset: 0x0006B384
		public static bool IsValid(Type type)
		{
			License license;
			bool result = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return result;
		}

		/// <summary>Determines whether a valid license can be granted for the specified instance of the type. This method creates a valid <see cref="T:System.ComponentModel.License" />.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <param name="instance">An object of the specified type or a type derived from the specified type.</param>
		/// <param name="license">A <see cref="T:System.ComponentModel.License" /> that is a valid license, or <see langword="null" /> if a valid license cannot be granted.</param>
		/// <returns>
		///   <see langword="true" /> if a valid <see cref="T:System.ComponentModel.License" /> can be granted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F5A RID: 8026 RVA: 0x0006D1A6 File Offset: 0x0006B3A6
		public static bool IsValid(Type type, object instance, out License license)
		{
			return LicenseManager.ValidateInternal(type, instance, false, out license);
		}

		/// <summary>Prevents changes being made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to lock.</param>
		/// <exception cref="T:System.InvalidOperationException">The context is already locked.</exception>
		// Token: 0x06001F5B RID: 8027 RVA: 0x0006D1B4 File Offset: 0x0006B3B4
		public static void LockContext(object contextUser)
		{
			object obj = LicenseManager.s_internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.s_contextLockHolder != null)
				{
					throw new InvalidOperationException("The CurrentContext property of the LicenseManager is already locked by another user.");
				}
				LicenseManager.s_contextLockHolder = contextUser;
			}
		}

		/// <summary>Allows changes to be made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to unlock.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextUser" /> represents a different user than the one specified in a previous call to <see cref="M:System.ComponentModel.LicenseManager.LockContext(System.Object)" />.</exception>
		// Token: 0x06001F5C RID: 8028 RVA: 0x0006D208 File Offset: 0x0006B408
		public static void UnlockContext(object contextUser)
		{
			object obj = LicenseManager.s_internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.s_contextLockHolder != contextUser)
				{
					throw new ArgumentException("The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser.");
				}
				LicenseManager.s_contextLockHolder = null;
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0006D25C File Offset: 0x0006B45C
		private static bool ValidateInternal(Type type, object instance, bool allowExceptions, out License license)
		{
			string text;
			return LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, type, instance, allowExceptions, out license, out text);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0006D27C File Offset: 0x0006B47C
		private static bool ValidateInternalRecursive(LicenseContext context, Type type, object instance, bool allowExceptions, out License license, out string licenseKey)
		{
			LicenseProvider licenseProvider = LicenseManager.GetCachedProvider(type);
			if (licenseProvider == null && !LicenseManager.GetCachedNoLicenseProvider(type))
			{
				LicenseProviderAttribute licenseProviderAttribute = (LicenseProviderAttribute)Attribute.GetCustomAttribute(type, typeof(LicenseProviderAttribute), false);
				if (licenseProviderAttribute != null)
				{
					Type licenseProvider2 = licenseProviderAttribute.LicenseProvider;
					licenseProvider = (LicenseManager.GetCachedProviderInstance(licenseProvider2) ?? ((LicenseProvider)SecurityUtils.SecureCreateInstance(licenseProvider2)));
				}
				LicenseManager.CacheProvider(type, licenseProvider);
			}
			license = null;
			bool flag = true;
			licenseKey = null;
			if (licenseProvider != null)
			{
				license = licenseProvider.GetLicense(context, type, instance, allowExceptions);
				if (license == null)
				{
					flag = false;
				}
				else
				{
					licenseKey = license.LicenseKey;
				}
			}
			if (flag && instance == null)
			{
				Type baseType = type.BaseType;
				if (baseType != typeof(object) && baseType != null)
				{
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
					string text;
					flag = LicenseManager.ValidateInternalRecursive(context, baseType, null, allowExceptions, out license, out text);
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether a license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <exception cref="T:System.ComponentModel.LicenseException">A <see cref="T:System.ComponentModel.License" /> cannot be granted.</exception>
		// Token: 0x06001F5F RID: 8031 RVA: 0x0006D364 File Offset: 0x0006B564
		public static void Validate(Type type)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, null, true, out license))
			{
				throw new LicenseException(type);
			}
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
		}

		/// <summary>Determines whether a license can be granted for the instance of the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <param name="instance">An <see cref="T:System.Object" /> of the specified type or a type derived from the specified type.</param>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />.</returns>
		/// <exception cref="T:System.ComponentModel.LicenseException">The type is licensed, but a <see cref="T:System.ComponentModel.License" /> cannot be granted.</exception>
		// Token: 0x06001F60 RID: 8032 RVA: 0x0006D390 File Offset: 0x0006B590
		public static License Validate(Type type, object instance)
		{
			License result;
			if (!LicenseManager.ValidateInternal(type, instance, true, out result))
			{
				throw new LicenseException(type, instance);
			}
			return result;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0006D3B2 File Offset: 0x0006B5B2
		// Note: this type is marked as 'beforefieldinit'.
		static LicenseManager()
		{
		}

		// Token: 0x04000F4D RID: 3917
		private static readonly object s_selfLock = new object();

		// Token: 0x04000F4E RID: 3918
		private static volatile LicenseContext s_context;

		// Token: 0x04000F4F RID: 3919
		private static object s_contextLockHolder;

		// Token: 0x04000F50 RID: 3920
		private static volatile Hashtable s_providers;

		// Token: 0x04000F51 RID: 3921
		private static volatile Hashtable s_providerInstances;

		// Token: 0x04000F52 RID: 3922
		private static readonly object s_internalSyncObject = new object();
	}
}
