using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Mono.Btls;
using Mono.Unity;

namespace Mono.Net.Security
{
	// Token: 0x020000A5 RID: 165
	internal static class MonoTlsProviderFactory
	{
		// Token: 0x0600032C RID: 812 RVA: 0x000093E4 File Offset: 0x000075E4
		internal static MobileTlsProvider GetProviderInternal()
		{
			object obj = MonoTlsProviderFactory.locker;
			MobileTlsProvider result;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeInternal();
				result = MonoTlsProviderFactory.defaultProvider;
			}
			return result;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000942C File Offset: 0x0000762C
		internal static void InitializeInternal()
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (!MonoTlsProviderFactory.initialized)
				{
					SystemDependencyProvider.Initialize();
					MonoTlsProviderFactory.InitializeProviderRegistration();
					MobileTlsProvider mobileTlsProvider;
					try
					{
						mobileTlsProvider = MonoTlsProviderFactory.CreateDefaultProviderImpl();
					}
					catch (Exception innerException)
					{
						throw new NotSupportedException("TLS Support not available.", innerException);
					}
					if (mobileTlsProvider == null)
					{
						throw new NotSupportedException("TLS Support not available.");
					}
					if (!MonoTlsProviderFactory.providerCache.ContainsKey(mobileTlsProvider.ID))
					{
						MonoTlsProviderFactory.providerCache.Add(mobileTlsProvider.ID, mobileTlsProvider);
					}
					MonoTlsProviderFactory.defaultProvider = mobileTlsProvider;
					MonoTlsProviderFactory.initialized = true;
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000094D8 File Offset: 0x000076D8
		internal static void InitializeInternal(string provider)
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (MonoTlsProviderFactory.initialized)
				{
					throw new NotSupportedException("TLS Subsystem already initialized.");
				}
				SystemDependencyProvider.Initialize();
				MonoTlsProviderFactory.defaultProvider = MonoTlsProviderFactory.LookupProvider(provider, true);
				MonoTlsProviderFactory.initialized = true;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000953C File Offset: 0x0000773C
		private static Type LookupProviderType(string name, bool throwOnError)
		{
			object obj = MonoTlsProviderFactory.locker;
			Type result;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				Tuple<Guid, string> tuple;
				if (!MonoTlsProviderFactory.providerRegistration.TryGetValue(name, out tuple))
				{
					if (throwOnError)
					{
						throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
					}
					result = null;
				}
				else
				{
					Type type = Type.GetType(tuple.Item2, false);
					if (type == null && throwOnError)
					{
						throw new NotSupportedException(string.Format("Could not find TLS Provider: `{0}'.", tuple.Item2));
					}
					result = type;
				}
			}
			return result;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000095D0 File Offset: 0x000077D0
		private static MobileTlsProvider LookupProvider(string name, bool throwOnError)
		{
			object obj = MonoTlsProviderFactory.locker;
			MobileTlsProvider result;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				Tuple<Guid, string> tuple;
				MobileTlsProvider mobileTlsProvider;
				if (!MonoTlsProviderFactory.providerRegistration.TryGetValue(name, out tuple))
				{
					if (throwOnError)
					{
						throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
					}
					result = null;
				}
				else if (MonoTlsProviderFactory.providerCache.TryGetValue(tuple.Item1, out mobileTlsProvider))
				{
					result = mobileTlsProvider;
				}
				else
				{
					Type type = Type.GetType(tuple.Item2, false);
					if (type == null && throwOnError)
					{
						throw new NotSupportedException(string.Format("Could not find TLS Provider: `{0}'.", tuple.Item2));
					}
					try
					{
						mobileTlsProvider = (MobileTlsProvider)Activator.CreateInstance(type, true);
					}
					catch (Exception innerException)
					{
						throw new NotSupportedException(string.Format("Unable to instantiate TLS Provider `{0}'.", type), innerException);
					}
					if (mobileTlsProvider == null)
					{
						if (throwOnError)
						{
							throw new NotSupportedException(string.Format("No such TLS Provider: `{0}'.", name));
						}
						result = null;
					}
					else
					{
						MonoTlsProviderFactory.providerCache.Add(tuple.Item1, mobileTlsProvider);
						result = mobileTlsProvider;
					}
				}
			}
			return result;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000096EC File Offset: 0x000078EC
		[Conditional("MONO_TLS_DEBUG")]
		private static void InitializeDebug()
		{
			if (Environment.GetEnvironmentVariable("MONO_TLS_DEBUG") != null)
			{
				MonoTlsProviderFactory.enableDebug = true;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009700 File Offset: 0x00007900
		[Conditional("MONO_TLS_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
			if (MonoTlsProviderFactory.enableDebug)
			{
				Console.Error.WriteLine(message, args);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009718 File Offset: 0x00007918
		private static void InitializeProviderRegistration()
		{
			object obj = MonoTlsProviderFactory.locker;
			lock (obj)
			{
				if (MonoTlsProviderFactory.providerRegistration == null)
				{
					MonoTlsProviderFactory.providerRegistration = new Dictionary<string, Tuple<Guid, string>>();
					MonoTlsProviderFactory.providerCache = new Dictionary<Guid, MobileTlsProvider>();
					if (UnityTls.IsSupported)
					{
						MonoTlsProviderFactory.PopulateUnityProviders();
					}
					else
					{
						MonoTlsProviderFactory.PopulateProviders();
					}
				}
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009784 File Offset: 0x00007984
		private static void PopulateUnityProviders()
		{
			Tuple<Guid, string> value = new Tuple<Guid, string>(MonoTlsProviderFactory.UnityTlsId, "Mono.Unity.UnityTlsProvider");
			MonoTlsProviderFactory.providerRegistration.Add("default", value);
			MonoTlsProviderFactory.providerRegistration.Add("unitytls", value);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000097C4 File Offset: 0x000079C4
		private static void PopulateProviders()
		{
			Tuple<Guid, string> tuple = null;
			Tuple<Guid, string> tuple2 = null;
			if (MonoTlsProviderFactory.IsBtlsSupported())
			{
				tuple2 = new Tuple<Guid, string>(MonoTlsProviderFactory.BtlsId, typeof(MonoBtlsProvider).FullName);
				MonoTlsProviderFactory.providerRegistration.Add("btls", tuple2);
			}
			Tuple<Guid, string> tuple3 = tuple ?? tuple2;
			if (tuple3 != null)
			{
				MonoTlsProviderFactory.providerRegistration.Add("default", tuple3);
				MonoTlsProviderFactory.providerRegistration.Add("legacy", tuple3);
			}
		}

		// Token: 0x06000336 RID: 822
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsBtlsSupported();

		// Token: 0x06000337 RID: 823 RVA: 0x00009830 File Offset: 0x00007A30
		private static MobileTlsProvider CreateDefaultProviderImpl()
		{
			string text = Environment.GetEnvironmentVariable("MONO_TLS_PROVIDER");
			if (string.IsNullOrEmpty(text))
			{
				text = "default";
			}
			if (!(text == "default") && !(text == "legacy"))
			{
				if (!(text == "btls"))
				{
					if (!(text == "unitytls"))
					{
						return MonoTlsProviderFactory.LookupProvider(text, true);
					}
					goto IL_6E;
				}
			}
			else
			{
				if (UnityTls.IsSupported)
				{
					goto IL_6E;
				}
				if (!MonoTlsProviderFactory.IsBtlsSupported())
				{
					throw new NotSupportedException("TLS Support not available.");
				}
			}
			return new MonoBtlsProvider();
			IL_6E:
			return new UnityTlsProvider();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000098B8 File Offset: 0x00007AB8
		internal static MobileTlsProvider GetProvider()
		{
			return MonoTlsProviderFactory.GetProviderInternal();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000098C0 File Offset: 0x00007AC0
		internal static bool IsProviderSupported(string name)
		{
			object obj = MonoTlsProviderFactory.locker;
			bool result;
			lock (obj)
			{
				MonoTlsProviderFactory.InitializeProviderRegistration();
				result = MonoTlsProviderFactory.providerRegistration.ContainsKey(name);
			}
			return result;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000990C File Offset: 0x00007B0C
		internal static MobileTlsProvider GetProvider(string name)
		{
			return MonoTlsProviderFactory.LookupProvider(name, false);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009918 File Offset: 0x00007B18
		internal static bool IsInitialized
		{
			get
			{
				object obj = MonoTlsProviderFactory.locker;
				bool result;
				lock (obj)
				{
					result = MonoTlsProviderFactory.initialized;
				}
				return result;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009958 File Offset: 0x00007B58
		internal static void Initialize()
		{
			MonoTlsProviderFactory.InitializeInternal();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000995F File Offset: 0x00007B5F
		internal static void Initialize(string provider)
		{
			MonoTlsProviderFactory.InitializeInternal(provider);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009967 File Offset: 0x00007B67
		// Note: this type is marked as 'beforefieldinit'.
		static MonoTlsProviderFactory()
		{
		}

		// Token: 0x04000277 RID: 631
		private static object locker = new object();

		// Token: 0x04000278 RID: 632
		private static bool initialized;

		// Token: 0x04000279 RID: 633
		private static MobileTlsProvider defaultProvider;

		// Token: 0x0400027A RID: 634
		private static Dictionary<string, Tuple<Guid, string>> providerRegistration;

		// Token: 0x0400027B RID: 635
		private static Dictionary<Guid, MobileTlsProvider> providerCache;

		// Token: 0x0400027C RID: 636
		private static bool enableDebug;

		// Token: 0x0400027D RID: 637
		internal static readonly Guid UnityTlsId = new Guid("06414A97-74F6-488F-877B-A6CA9BBEB82E");

		// Token: 0x0400027E RID: 638
		internal static readonly Guid AppleTlsId = new Guid("981af8af-a3a3-419a-9f01-a518e3a17c1c");

		// Token: 0x0400027F RID: 639
		internal static readonly Guid BtlsId = new Guid("432d18c9-9348-4b90-bfbf-9f2a10e1f15b");
	}
}
