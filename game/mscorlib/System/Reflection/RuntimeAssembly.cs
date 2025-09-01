using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020008F1 RID: 2289
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeAssembly : Assembly
	{
		// Token: 0x06004CB8 RID: 19640 RVA: 0x000F31C3 File Offset: 0x000F13C3
		protected RuntimeAssembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x000F31D6 File Offset: 0x000F13D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x000472C8 File Offset: 0x000454C8
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x000F31F4 File Offset: 0x000F13F4
		[SecurityCritical]
		internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0 || assemblyString[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("String cannot have zero length."));
			}
			if (forIntrospection)
			{
				AppDomain.CheckReflectionOnlyLoadSupported();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = assemblyString;
			assemblyFromResolveEvent = null;
			return assemblyName;
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x000F3247 File Offset: 0x000F1447
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.CodeBase != null)
			{
				AppDomain.CheckLoadFromSupported();
			}
			assemblyRef = (AssemblyName)assemblyRef.Clone();
			if (assemblySecurity != null)
			{
			}
			return (RuntimeAssembly)Assembly.Load(assemblyRef);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x000F3282 File Offset: 0x000F1482
		internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return (RuntimeAssembly)Assembly.LoadWithPartialName(partialName, securityEvidence);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x000F3290 File Offset: 0x000F1490
		internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			return RuntimeAssembly.LoadWithPartialNameInternal(an.ToString(), securityEvidence, ref stackMark);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x000F329F File Offset: 0x000F149F
		public override AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				string codeBase = this.CodeBase;
			}
			return AssemblyName.Create(this, true);
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x000F32B6 File Offset: 0x000F14B6
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			return base.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x000F32E4 File Offset: 0x000F14E4
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			foreach (Module module in this.GetModules(true))
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x000F333D File Offset: 0x000F153D
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x000F3348 File Offset: 0x000F1548
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x000F3396 File Offset: 0x000F1596
		[MonoTODO("Always returns the same as GetModules")]
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x000F33A0 File Offset: 0x000F15A0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x000F33BC File Offset: 0x000F15BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x000F33D6 File Offset: 0x000F15D6
		[ComVisible(false)]
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x000F33DE File Offset: 0x000F15DE
		public override bool GlobalAssemblyCache
		{
			get
			{
				return this.get_global_assembly_cache();
			}
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x000F33E6 File Offset: 0x000F15E6
		public override Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x000F33F0 File Offset: 0x000F15F0
		internal static byte[] GetAotId()
		{
			byte[] array = new byte[16];
			if (RuntimeAssembly.GetAotIdInternal(array))
			{
				return array;
			}
			return null;
		}

		// Token: 0x06004CCB RID: 19659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_code_base(Assembly a, bool escaped);

		// Token: 0x06004CCC RID: 19660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_location();

		// Token: 0x06004CCD RID: 19661
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_fullname(Assembly a);

		// Token: 0x06004CCE RID: 19662
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetAotIdInternal(byte[] aotid);

		// Token: 0x06004CCF RID: 19663
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalImageRuntimeVersion(Assembly a);

		// Token: 0x06004CD0 RID: 19664
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool get_global_assembly_cache();

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004CD1 RID: 19665
		public override extern MethodInfo EntryPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004CD2 RID: 19666
		[ComVisible(false)]
		public override extern bool ReflectionOnly { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06004CD3 RID: 19667 RVA: 0x000F3410 File Offset: 0x000F1610
		internal static string GetCodeBase(Assembly a, bool escaped)
		{
			string text = RuntimeAssembly.get_code_base(a, escaped);
			if (SecurityManager.SecurityEnabled && string.Compare("FILE://", 0, text, 0, 7, true, CultureInfo.InvariantCulture) == 0)
			{
				string path = text.Substring(7);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
			}
			return text;
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x000F3457 File Offset: 0x000F1657
		public override string CodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, false);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x000F3460 File Offset: 0x000F1660
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x000F3469 File Offset: 0x000F1669
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x000F3471 File Offset: 0x000F1671
		[ComVisible(false)]
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x000F3479 File Offset: 0x000F1679
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x000F3481 File Offset: 0x000F1681
		internal override bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x000F348C File Offset: 0x000F168C
		public override string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				string location = this.get_location();
				if (location != string.Empty && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, location).Demand();
				}
				return location;
			}
		}

		// Token: 0x06004CDB RID: 19675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		// Token: 0x06004CDC RID: 19676 RVA: 0x000F34D0 File Offset: 0x000F16D0
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo(null, null, (ResourceLocation)0);
			if (this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo))
			{
				return manifestResourceInfo;
			}
			return null;
		}

		// Token: 0x06004CDD RID: 19677
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern string[] GetManifestResourceNames();

		// Token: 0x06004CDE RID: 19678
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		// Token: 0x06004CDF RID: 19679 RVA: 0x000F3514 File Offset: 0x000F1714
		public unsafe override Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				Assembly assembly = AppDomain.CurrentDomain.DoResourceResolve(name, this);
				if (assembly != null && assembly != this)
				{
					return assembly.GetManifestResourceStream(name);
				}
				return null;
			}
			else
			{
				if (manifestResourceInfo.ReferencedAssembly != null)
				{
					return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
				}
				if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
				{
					if (this.fromByteArray)
					{
						throw new FileNotFoundException(manifestResourceInfo.FileName);
					}
					return new FileStream(Path.Combine(Path.GetDirectoryName(this.Location), manifestResourceInfo.FileName), FileMode.Open, FileAccess.Read);
				}
				else
				{
					int num;
					Module module;
					IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
					if (manifestResourceInternal == (IntPtr)0)
					{
						return null;
					}
					return new RuntimeAssembly.UnmanagedMemoryStreamForModule((byte*)((void*)manifestResourceInternal), (long)num, module);
				}
			}
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x000F3608 File Offset: 0x000F1808
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetManifestResourceStream(type, name, false, ref stackCrawlMark);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x000F3622 File Offset: 0x000F1822
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06004CE5 RID: 19685 RVA: 0x000F362A File Offset: 0x000F182A
		// (remove) Token: 0x06004CE6 RID: 19686 RVA: 0x000F3638 File Offset: 0x000F1838
		public override event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x000F3646 File Offset: 0x000F1846
		internal override Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x06004CE8 RID: 19688
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Module GetManifestModuleInternal();

		// Token: 0x06004CE9 RID: 19689
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern Module[] GetModulesInternal();

		// Token: 0x06004CEA RID: 19690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		// Token: 0x06004CEB RID: 19691 RVA: 0x000F3650 File Offset: 0x000F1850
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return EmptyArray<FileStream>.Value;
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x000F36E8 File Offset: 0x000F18E8
		public override FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x000F3732 File Offset: 0x000F1932
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x000F373A File Offset: 0x000F193A
		public override bool Equals(object o)
		{
			return this == o || (o != null && o is RuntimeAssembly && ((RuntimeAssembly)o)._mono_assembly == this._mono_assembly);
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x000F3767 File Offset: 0x000F1967
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x000F378A File Offset: 0x000F198A
		public override Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x000F3794 File Offset: 0x000F1994
		internal override Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x000F37E4 File Offset: 0x000F19E4
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x000F386C File Offset: 0x000F1A6C
		internal override PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x000F389E File Offset: 0x000F1A9E
		internal override PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x06004CF5 RID: 19701
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x06004CF6 RID: 19702 RVA: 0x000F38D0 File Offset: 0x000F1AD0
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (RuntimeAssembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x000F397A File Offset: 0x000F1B7A
		public override PermissionSet PermissionSet
		{
			get
			{
				return this.GrantedPermissionSet;
			}
		}

		// Token: 0x0400303B RID: 12347
		internal IntPtr _mono_assembly;

		// Token: 0x0400303C RID: 12348
		internal Evidence _evidence;

		// Token: 0x0400303D RID: 12349
		internal Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x0400303E RID: 12350
		internal PermissionSet _minimum;

		// Token: 0x0400303F RID: 12351
		internal PermissionSet _optional;

		// Token: 0x04003040 RID: 12352
		internal PermissionSet _refuse;

		// Token: 0x04003041 RID: 12353
		internal PermissionSet _granted;

		// Token: 0x04003042 RID: 12354
		internal PermissionSet _denied;

		// Token: 0x04003043 RID: 12355
		internal bool fromByteArray;

		// Token: 0x04003044 RID: 12356
		internal string assemblyName;

		// Token: 0x020008F2 RID: 2290
		internal class UnmanagedMemoryStreamForModule : UnmanagedMemoryStream
		{
			// Token: 0x06004CF8 RID: 19704 RVA: 0x000F3982 File Offset: 0x000F1B82
			public unsafe UnmanagedMemoryStreamForModule(byte* pointer, long length, Module module) : base(pointer, length)
			{
				this.module = module;
			}

			// Token: 0x06004CF9 RID: 19705 RVA: 0x000F3993 File Offset: 0x000F1B93
			protected override void Dispose(bool disposing)
			{
				if (this._isOpen)
				{
					this.module = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04003045 RID: 12357
			private Module module;
		}
	}
}
