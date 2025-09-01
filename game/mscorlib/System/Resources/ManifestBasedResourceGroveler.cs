using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000867 RID: 2151
	internal class ManifestBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06004773 RID: 18291 RVA: 0x000E8B44 File Offset: 0x000E6D44
		public ManifestBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000E8B54 File Offset: 0x000E6D54
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			Stream stream = null;
			RuntimeAssembly runtimeAssembly = null;
			CultureInfo cultureInfo = this.UltimateFallbackFixup(culture);
			if (cultureInfo.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				runtimeAssembly = this._mediator.MainAssembly;
			}
			else
			{
				runtimeAssembly = this.GetSatelliteAssembly(cultureInfo, ref stackMark);
				if (runtimeAssembly == null && (culture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite))
				{
					this.HandleSatelliteMissing();
				}
			}
			string resourceFileName = this._mediator.GetResourceFileName(cultureInfo);
			if (runtimeAssembly != null)
			{
				lock (localResourceSets)
				{
					localResourceSets.TryGetValue(culture.Name, out resourceSet);
				}
				stream = this.GetManifestResourceStream(runtimeAssembly, resourceFileName, ref stackMark);
			}
			if (createIfNotExists && stream != null && resourceSet == null)
			{
				resourceSet = this.CreateResourceSet(stream, runtimeAssembly);
			}
			else if (stream == null && tryParents && culture.HasInvariantCultureName)
			{
				this.HandleResourceStreamMissing(resourceFileName);
			}
			return resourceSet;
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000E8C54 File Offset: 0x000E6E54
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string value = defaultResName;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				value = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString() + defaultResName;
			}
			string[] manifestResourceNames = this._mediator.MainAssembly.GetManifestResourceNames();
			for (int i = 0; i < manifestResourceNames.Length; i++)
			{
				if (manifestResourceNames[i].Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000E8CD8 File Offset: 0x000E6ED8
		private CultureInfo UltimateFallbackFixup(CultureInfo lookForCulture)
		{
			CultureInfo result = lookForCulture;
			if (lookForCulture.Name == this._mediator.NeutralResourcesCulture.Name && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				result = CultureInfo.InvariantCulture;
			}
			else if (lookForCulture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite)
			{
				result = this._mediator.NeutralResourcesCulture;
			}
			return result;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000E8D3C File Offset: 0x000E6F3C
		[SecurityCritical]
		internal static CultureInfo GetNeutralResourcesLanguage(Assembly a, ref UltimateResourceFallbackLocation fallbackLocation)
		{
			string text = null;
			short num = 0;
			if (!ManifestBasedResourceGroveler.GetNeutralResourcesLanguageAttribute(a, ref text, ref num))
			{
				fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
				return CultureInfo.InvariantCulture;
			}
			if (num < 0 || num > 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".", new object[]
				{
					num
				}));
			}
			fallbackLocation = (UltimateResourceFallbackLocation)num;
			CultureInfo result;
			try
			{
				result = CultureInfo.GetCultureInfo(text);
			}
			catch (ArgumentException innerException)
			{
				if (!(a == typeof(object).Assembly))
				{
					throw new ArgumentException(Environment.GetResourceString("The NeutralResourcesLanguageAttribute on the assembly \"{0}\" specifies an invalid culture name: \"{1}\".", new object[]
					{
						a.ToString(),
						text
					}), innerException);
				}
				result = CultureInfo.InvariantCulture;
			}
			return result;
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000E8DF0 File Offset: 0x000E6FF0
		[SecurityCritical]
		internal ResourceSet CreateResourceSet(Stream store, Assembly assembly)
		{
			if (store.CanSeek && store.Length > 4L)
			{
				long position = store.Position;
				BinaryReader binaryReader = new BinaryReader(store);
				if (binaryReader.ReadInt32() == ResourceManager.MagicNumber)
				{
					int num = binaryReader.ReadInt32();
					string text;
					string text2;
					if (num == ResourceManager.HeaderVersionNumber)
					{
						binaryReader.ReadInt32();
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
					}
					else
					{
						if (num <= ResourceManager.HeaderVersionNumber)
						{
							throw new NotSupportedException(Environment.GetResourceString("Found an obsolete .resources file in assembly '{0}'. Rebuild that .resources file then rebuild that assembly.", new object[]
							{
								this._mediator.MainAssembly.GetSimpleName()
							}));
						}
						int num2 = binaryReader.ReadInt32();
						long offset = binaryReader.BaseStream.Position + (long)num2;
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
						binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
					}
					store.Position = position;
					if (this.CanUseDefaultResourceClasses(text, text2))
					{
						return new RuntimeResourceSet(store);
					}
					IResourceReader resourceReader = (IResourceReader)Activator.CreateInstance(Type.GetType(text, true), new object[]
					{
						store
					});
					object[] args = new object[]
					{
						resourceReader
					};
					Type type;
					if (this._mediator.UserResourceSet == null)
					{
						type = Type.GetType(text2, true, false);
					}
					else
					{
						type = this._mediator.UserResourceSet;
					}
					return (ResourceSet)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, args, null, null);
				}
				else
				{
					store.Position = position;
				}
			}
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(store);
			}
			object[] args2 = new object[]
			{
				store,
				assembly
			};
			ResourceSet result;
			try
			{
				try
				{
					return (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args2);
				}
				catch (MissingMethodException)
				{
				}
				args2 = new object[]
				{
					store
				};
				result = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args2);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.", new object[]
				{
					this._mediator.UserResourceSet.AssemblyQualifiedName
				}), innerException);
			}
			return result;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000E901C File Offset: 0x000E721C
		[SecurityCritical]
		private Stream GetManifestResourceStream(RuntimeAssembly satellite, string fileName, ref StackCrawlMark stackMark)
		{
			bool skipSecurityCheck = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			Stream stream = satellite.GetManifestResourceStream(this._mediator.LocationInfo, fileName, skipSecurityCheck, ref stackMark);
			if (stream == null)
			{
				stream = this.CaseInsensitiveManifestResourceStreamLookup(satellite, fileName);
			}
			return stream;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000E9080 File Offset: 0x000E7280
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Stream CaseInsensitiveManifestResourceStreamLookup(RuntimeAssembly satellite, string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._mediator.LocationInfo != null)
			{
				string @namespace = this._mediator.LocationInfo.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			stringBuilder.Append(name);
			string text = stringBuilder.ToString();
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			string text2 = null;
			foreach (string text3 in satellite.GetManifestResourceNames())
			{
				if (compareInfo.Compare(text3, text, CompareOptions.IgnoreCase) == 0)
				{
					if (text2 != null)
					{
						throw new MissingManifestResourceException(Environment.GetResourceString("A case-insensitive lookup for resource file \"{0}\" in assembly \"{1}\" found multiple entries. Remove the duplicates or specify the exact case.", new object[]
						{
							text,
							satellite.ToString()
						}));
					}
					text2 = text3;
				}
			}
			if (text2 == null)
			{
				return null;
			}
			bool skipSecurityCheck = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return satellite.GetManifestResourceStream(text2, ref stackCrawlMark, skipSecurityCheck);
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000E9190 File Offset: 0x000E7390
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeAssembly GetSatelliteAssembly(CultureInfo lookForCulture, ref StackCrawlMark stackMark)
		{
			if (!this._mediator.LookedForSatelliteContractVersion)
			{
				this._mediator.SatelliteContractVersion = this._mediator.ObtainSatelliteContractVersion(this._mediator.MainAssembly);
				this._mediator.LookedForSatelliteContractVersion = true;
			}
			RuntimeAssembly result = null;
			string satelliteAssemblyName = this.GetSatelliteAssemblyName();
			try
			{
				result = this._mediator.MainAssembly.InternalGetSatelliteAssembly(satelliteAssemblyName, lookForCulture, this._mediator.SatelliteContractVersion, false, ref stackMark);
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return result;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000E9228 File Offset: 0x000E7428
		private bool CanUseDefaultResourceClasses(string readerTypeName, string resSetTypeName)
		{
			if (this._mediator.UserResourceSet != null)
			{
				return false;
			}
			AssemblyName asmName = new AssemblyName(ResourceManager.MscorlibName);
			return (readerTypeName == null || ResourceManager.CompareNames(readerTypeName, ResourceManager.ResReaderTypeName, asmName)) && (resSetTypeName == null || ResourceManager.CompareNames(resSetTypeName, ResourceManager.ResSetTypeName, asmName));
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000E927C File Offset: 0x000E747C
		[SecurityCritical]
		private string GetSatelliteAssemblyName()
		{
			return this._mediator.MainAssembly.GetSimpleName() + ".resources";
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000E9298 File Offset: 0x000E7498
		[SecurityCritical]
		private void HandleSatelliteMissing()
		{
			string text = this._mediator.MainAssembly.GetSimpleName() + ".resources.dll";
			if (this._mediator.SatelliteContractVersion != null)
			{
				text = text + ", Version=" + this._mediator.SatelliteContractVersion.ToString();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this._mediator.MainAssembly.GetPublicKey());
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			int num = publicKeyToken.Length;
			StringBuilder stringBuilder = new StringBuilder(num * 2);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(publicKeyToken[i].ToString("x", CultureInfo.InvariantCulture));
			}
			string str = text;
			string str2 = ", PublicKeyToken=";
			StringBuilder stringBuilder2 = stringBuilder;
			text = str + str2 + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
			string text2 = this._mediator.NeutralResourcesCulture.Name;
			if (text2.Length == 0)
			{
				text2 = "<invariant>";
			}
			throw new MissingSatelliteAssemblyException(Environment.GetResourceString("The satellite assembly named \"{1}\" for fallback culture \"{0}\" either could not be found or could not be loaded. This is generally a setup problem. Please consider reinstalling or repairing the application.", new object[]
			{
				this._mediator.NeutralResourcesCulture,
				text
			}), text2);
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000E93B0 File Offset: 0x000E75B0
		[SecurityCritical]
		private void HandleResourceStreamMissing(string fileName)
		{
			if (this._mediator.MainAssembly == typeof(object).Assembly && this._mediator.BaseName.Equals("mscorlib"))
			{
				Environment.FailFast("mscorlib.resources couldn't be found!  Large parts of the BCL won't work!");
			}
			string text = string.Empty;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString();
			}
			text += fileName;
			throw new MissingManifestResourceException(Environment.GetResourceString("Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure \"{0}\" was correctly embedded or linked into assembly \"{1}\" at compile time, or that all the satellite assemblies required are loadable and fully signed.", new object[]
			{
				text,
				this._mediator.MainAssembly.GetSimpleName()
			}));
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000E9480 File Offset: 0x000E7680
		private static bool GetNeutralResourcesLanguageAttribute(Assembly assembly, ref string cultureName, ref short fallbackLocation)
		{
			NeutralResourcesLanguageAttribute customAttribute = assembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>();
			if (customAttribute == null)
			{
				return false;
			}
			cultureName = customAttribute.CultureName;
			fallbackLocation = (short)customAttribute.Location;
			return true;
		}

		// Token: 0x04002DCD RID: 11725
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
