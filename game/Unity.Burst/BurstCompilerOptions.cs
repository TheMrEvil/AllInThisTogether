using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x0200000B RID: 11
	public sealed class BurstCompilerOptions
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000268C File Offset: 0x0000088C
		internal static string SerialiseCompilationOptionsSafe(string[] roots, string[] folders, string options)
		{
			return SafeStringArrayHelper.SerialiseStringArraySafe(new string[]
			{
				SafeStringArrayHelper.SerialiseStringArraySafe(roots),
				SafeStringArrayHelper.SerialiseStringArraySafe(folders),
				options
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026B0 File Offset: 0x000008B0
		[return: TupleElementNames(new string[]
		{
			"roots",
			"folders",
			"options"
		})]
		internal static ValueTuple<string[], string[], string> DeserialiseCompilationOptionsSafe(string from)
		{
			string[] array = SafeStringArrayHelper.DeserialiseStringArraySafe(from);
			return new ValueTuple<string[], string[], string>(SafeStringArrayHelper.DeserialiseStringArraySafe(array[0]), SafeStringArrayHelper.DeserialiseStringArraySafe(array[1]), array[2]);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026DC File Offset: 0x000008DC
		private BurstCompilerOptions() : this(false)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026E5 File Offset: 0x000008E5
		internal BurstCompilerOptions(bool isGlobal)
		{
			this.IsGlobal = isGlobal;
			this.EnableBurstCompilation = true;
			this.EnableBurstSafetyChecks = true;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002702 File Offset: 0x00000902
		private bool IsGlobal
		{
			[CompilerGenerated]
			get
			{
				return this.<IsGlobal>k__BackingField;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000270A File Offset: 0x0000090A
		public bool IsEnabled
		{
			get
			{
				return this.EnableBurstCompilation && !BurstCompilerOptions.ForceDisableBurstCompilation;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002728 File Offset: 0x00000928
		public bool EnableBurstCompilation
		{
			get
			{
				return this._enableBurstCompilation;
			}
			set
			{
				if (this.IsGlobal && BurstCompilerOptions.ForceDisableBurstCompilation)
				{
					value = false;
				}
				bool flag = this._enableBurstCompilation != value;
				this._enableBurstCompilation = value;
				if (this.IsGlobal)
				{
					JobsUtility.JobCompilerEnabled = value;
					BurstCompiler._IsEnabled = value;
				}
				if (flag)
				{
					this.OnOptionsChanged();
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002776 File Offset: 0x00000976
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000277E File Offset: 0x0000097E
		public bool EnableBurstCompileSynchronously
		{
			get
			{
				return this._enableBurstCompileSynchronously;
			}
			set
			{
				bool flag = this._enableBurstCompileSynchronously != value;
				this._enableBurstCompileSynchronously = value;
				if (flag)
				{
					this.OnOptionsChanged();
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000027A3 File Offset: 0x000009A3
		public bool EnableBurstSafetyChecks
		{
			get
			{
				return this._enableBurstSafetyChecks;
			}
			set
			{
				bool flag = this._enableBurstSafetyChecks != value;
				this._enableBurstSafetyChecks = value;
				if (flag)
				{
					this.OnOptionsChanged();
					this.MaybeTriggerRecompilation();
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000027C6 File Offset: 0x000009C6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000027CE File Offset: 0x000009CE
		public bool ForceEnableBurstSafetyChecks
		{
			get
			{
				return this._forceEnableBurstSafetyChecks;
			}
			set
			{
				bool flag = this._forceEnableBurstSafetyChecks != value;
				this._forceEnableBurstSafetyChecks = value;
				if (flag)
				{
					this.OnOptionsChanged();
					this.MaybeTriggerRecompilation();
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000027F1 File Offset: 0x000009F1
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000027F9 File Offset: 0x000009F9
		public bool EnableBurstDebug
		{
			get
			{
				return this._enableBurstDebug;
			}
			set
			{
				bool flag = this._enableBurstDebug != value;
				this._enableBurstDebug = value;
				if (flag)
				{
					this.OnOptionsChanged();
					this.MaybeTriggerRecompilation();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000281C File Offset: 0x00000A1C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000281F File Offset: 0x00000A1F
		[Obsolete("This property is no longer used and will be removed in a future major release")]
		public bool DisableOptimizations
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002821 File Offset: 0x00000A21
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002824 File Offset: 0x00000A24
		[Obsolete("This property is no longer used and will be removed in a future major release. Use the [BurstCompile(FloatMode = FloatMode.Fast)] on the method directly to enable this feature")]
		public bool EnableFastMath
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002826 File Offset: 0x00000A26
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000282E File Offset: 0x00000A2E
		internal bool EnableBurstTimings
		{
			get
			{
				return this._enableBurstTimings;
			}
			set
			{
				bool flag = this._enableBurstTimings != value;
				this._enableBurstTimings = value;
				if (flag)
				{
					this.OnOptionsChanged();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000284B File Offset: 0x00000A4B
		internal bool RequiresSynchronousCompilation
		{
			get
			{
				return this.EnableBurstCompileSynchronously || BurstCompilerOptions.ForceBurstCompilationSynchronously;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002864 File Offset: 0x00000A64
		internal Action OptionsChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<OptionsChanged>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OptionsChanged>k__BackingField = value;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002870 File Offset: 0x00000A70
		internal BurstCompilerOptions Clone()
		{
			return new BurstCompilerOptions
			{
				EnableBurstCompilation = this.EnableBurstCompilation,
				EnableBurstCompileSynchronously = this.EnableBurstCompileSynchronously,
				EnableBurstSafetyChecks = this.EnableBurstSafetyChecks,
				EnableBurstTimings = this.EnableBurstTimings,
				EnableBurstDebug = this.EnableBurstDebug,
				ForceEnableBurstSafetyChecks = this.ForceEnableBurstSafetyChecks
			};
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000028CA File Offset: 0x00000ACA
		private static bool TryGetAttribute(MemberInfo member, out BurstCompileAttribute attribute)
		{
			attribute = null;
			if (member == null)
			{
				return false;
			}
			attribute = BurstCompilerOptions.GetBurstCompileAttribute(member);
			return attribute != null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000028E9 File Offset: 0x00000AE9
		private static bool TryGetAttribute(Assembly assembly, out BurstCompileAttribute attribute)
		{
			if (assembly == null)
			{
				attribute = null;
				return false;
			}
			attribute = assembly.GetCustomAttribute<BurstCompileAttribute>();
			return attribute != null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002908 File Offset: 0x00000B08
		private static BurstCompileAttribute GetBurstCompileAttribute(MemberInfo memberInfo)
		{
			BurstCompileAttribute customAttribute = memberInfo.GetCustomAttribute<BurstCompileAttribute>();
			if (customAttribute != null)
			{
				return customAttribute;
			}
			using (IEnumerator<Attribute> enumerator = memberInfo.GetCustomAttributes().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().FullName == "Burst.Compiler.IL.Tests.TestCompilerAttribute")
					{
						List<string> list = new List<string>();
						return new BurstCompileAttribute(FloatPrecision.Standard, FloatMode.Default)
						{
							CompileSynchronously = true,
							Options = list.ToArray()
						};
					}
				}
			}
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002998 File Offset: 0x00000B98
		internal static bool HasBurstCompileAttribute(MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			BurstCompileAttribute burstCompileAttribute;
			return BurstCompilerOptions.TryGetAttribute(member, out burstCompileAttribute);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000029C4 File Offset: 0x00000BC4
		internal static void MergeAttributes(ref BurstCompileAttribute memberAttribute, in BurstCompileAttribute assemblyAttribute)
		{
			if (memberAttribute.FloatMode == FloatMode.Default)
			{
				memberAttribute.FloatMode = assemblyAttribute.FloatMode;
			}
			if (memberAttribute.FloatPrecision == FloatPrecision.Standard)
			{
				memberAttribute.FloatPrecision = assemblyAttribute.FloatPrecision;
			}
			if (memberAttribute.OptimizeFor == OptimizeFor.Default)
			{
				memberAttribute.OptimizeFor = assemblyAttribute.OptimizeFor;
			}
			if (memberAttribute._compileSynchronously == null && assemblyAttribute._compileSynchronously != null)
			{
				memberAttribute._compileSynchronously = assemblyAttribute._compileSynchronously;
			}
			if (memberAttribute._debug == null && assemblyAttribute._debug != null)
			{
				memberAttribute._debug = assemblyAttribute._debug;
			}
			if (memberAttribute._disableDirectCall == null && assemblyAttribute._disableDirectCall != null)
			{
				memberAttribute._disableDirectCall = assemblyAttribute._disableDirectCall;
			}
			if (memberAttribute._disableSafetyChecks == null && assemblyAttribute._disableSafetyChecks != null)
			{
				memberAttribute._disableSafetyChecks = assemblyAttribute._disableSafetyChecks;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002AC0 File Offset: 0x00000CC0
		internal bool TryGetOptions(MemberInfo member, out string flagsOut, bool isForILPostProcessing = false, bool isForCompilerClient = false)
		{
			flagsOut = null;
			BurstCompileAttribute attr;
			if (!BurstCompilerOptions.TryGetAttribute(member, out attr))
			{
				return false;
			}
			BurstCompileAttribute burstCompileAttribute;
			if (BurstCompilerOptions.TryGetAttribute(member.Module.Assembly, out burstCompileAttribute))
			{
				BurstCompilerOptions.MergeAttributes(ref attr, burstCompileAttribute);
			}
			flagsOut = this.GetOptions(attr, isForILPostProcessing, isForCompilerClient);
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B08 File Offset: 0x00000D08
		internal string GetOptions(BurstCompileAttribute attr = null, bool isForILPostProcessing = false, bool isForCompilerClient = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!isForCompilerClient && ((attr != null && attr.CompileSynchronously) || this.RequiresSynchronousCompilation))
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("enable-synchronous-compilation", null));
			}
			BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("debug=", "LineOnly"));
			if (isForILPostProcessing)
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("compilation-priority=", CompilationPriority.ILPP));
			}
			if (attr != null)
			{
				if (attr.FloatMode != FloatMode.Default)
				{
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("float-mode=", attr.FloatMode));
				}
				if (attr.FloatPrecision != FloatPrecision.Standard)
				{
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("float-precision=", attr.FloatPrecision));
				}
				if (attr.DisableSafetyChecks)
				{
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("disable-safety-checks", null));
				}
				if (attr.Options != null)
				{
					foreach (string text in attr.Options)
					{
						if (!string.IsNullOrEmpty(text))
						{
							BurstCompilerOptions.AddOption(stringBuilder, text);
						}
					}
				}
				switch (attr.OptimizeFor)
				{
				case OptimizeFor.Default:
				case OptimizeFor.Balanced:
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("opt-level=", 2));
					break;
				case OptimizeFor.Performance:
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("opt-level=", 3));
					break;
				case OptimizeFor.Size:
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("opt-for-size", null));
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("opt-level=", 3));
					break;
				case OptimizeFor.FastCompilation:
					BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("opt-level=", 1));
					break;
				}
			}
			if (this.ForceEnableBurstSafetyChecks)
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("global-safety-checks-setting=", GlobalSafetyChecksSettingKind.ForceOn));
			}
			else if (this.EnableBurstSafetyChecks)
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("global-safety-checks-setting=", GlobalSafetyChecksSettingKind.On));
			}
			else
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("global-safety-checks-setting=", GlobalSafetyChecksSettingKind.Off));
			}
			if (this.EnableBurstTimings)
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("log-timings", null));
			}
			if (this.EnableBurstDebug || (attr != null && attr.Debug))
			{
				BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("debugMode", null));
			}
			BurstCompilerOptions.AddOption(stringBuilder, BurstCompilerOptions.GetOption("temp-folder=", Path.Combine(Environment.CurrentDirectory, "Temp", "Burst")));
			return stringBuilder.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D56 File Offset: 0x00000F56
		private static void AddOption(StringBuilder builder, string option)
		{
			if (builder.Length != 0)
			{
				builder.Append('\n');
			}
			builder.Append(option);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D71 File Offset: 0x00000F71
		internal static string GetOption(string optionName, object value = null)
		{
			if (optionName == null)
			{
				throw new ArgumentNullException("optionName");
			}
			string str = "--";
			object obj = value ?? string.Empty;
			return str + optionName + ((obj != null) ? obj.ToString() : null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DA2 File Offset: 0x00000FA2
		private void OnOptionsChanged()
		{
			Action optionsChanged = this.OptionsChanged;
			if (optionsChanged == null)
			{
				return;
			}
			optionsChanged();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private void MaybeTriggerRecompilation()
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DB8 File Offset: 0x00000FB8
		static BurstCompilerOptions()
		{
			foreach (string a in Environment.GetCommandLineArgs())
			{
				if (!(a == "--burst-disable-compilation"))
				{
					if (a == "--burst-force-sync-compilation")
					{
						BurstCompilerOptions.ForceBurstCompilationSynchronously = true;
					}
				}
				else
				{
					BurstCompilerOptions.ForceDisableBurstCompilation = true;
				}
			}
			if (BurstCompilerOptions.CheckIsSecondaryUnityProcess())
			{
				BurstCompilerOptions.ForceDisableBurstCompilation = true;
				BurstCompilerOptions.IsSecondaryUnityProcess = true;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("UNITY_BURST_DISABLE_COMPILATION");
			if (!string.IsNullOrEmpty(environmentVariable) && environmentVariable != "0")
			{
				BurstCompilerOptions.ForceDisableBurstCompilation = true;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E40 File Offset: 0x00001040
		private static bool CheckIsSecondaryUnityProcess()
		{
			return false;
		}

		// Token: 0x04000022 RID: 34
		private const string DisableCompilationArg = "--burst-disable-compilation";

		// Token: 0x04000023 RID: 35
		private const string ForceSynchronousCompilationArg = "--burst-force-sync-compilation";

		// Token: 0x04000024 RID: 36
		internal const string DefaultLibraryName = "lib_burst_generated";

		// Token: 0x04000025 RID: 37
		internal const string BurstInitializeName = "burst.initialize";

		// Token: 0x04000026 RID: 38
		internal const string BurstInitializeExternalsName = "burst.initialize.externals";

		// Token: 0x04000027 RID: 39
		internal const string BurstInitializeStaticsName = "burst.initialize.statics";

		// Token: 0x04000028 RID: 40
		internal const string OptionBurstcSwitch = "+burstc";

		// Token: 0x04000029 RID: 41
		internal const string OptionGroup = "group";

		// Token: 0x0400002A RID: 42
		internal const string OptionPlatform = "platform=";

		// Token: 0x0400002B RID: 43
		internal const string OptionBackend = "backend=";

		// Token: 0x0400002C RID: 44
		internal const string OptionGlobalSafetyChecksSetting = "global-safety-checks-setting=";

		// Token: 0x0400002D RID: 45
		internal const string OptionDisableSafetyChecks = "disable-safety-checks";

		// Token: 0x0400002E RID: 46
		internal const string OptionDisableOpt = "disable-opt";

		// Token: 0x0400002F RID: 47
		internal const string OptionFastMath = "fastmath";

		// Token: 0x04000030 RID: 48
		internal const string OptionTarget = "target=";

		// Token: 0x04000031 RID: 49
		internal const string OptionOptLevel = "opt-level=";

		// Token: 0x04000032 RID: 50
		internal const string OptionLogTimings = "log-timings";

		// Token: 0x04000033 RID: 51
		internal const string OptionOptForSize = "opt-for-size";

		// Token: 0x04000034 RID: 52
		internal const string OptionFloatPrecision = "float-precision=";

		// Token: 0x04000035 RID: 53
		internal const string OptionFloatMode = "float-mode=";

		// Token: 0x04000036 RID: 54
		internal const string OptionBranchProtection = "branch-protection=";

		// Token: 0x04000037 RID: 55
		internal const string OptionDisableWarnings = "disable-warnings=";

		// Token: 0x04000038 RID: 56
		internal const string OptionAssemblyDefines = "assembly-defines=";

		// Token: 0x04000039 RID: 57
		internal const string OptionDump = "dump=";

		// Token: 0x0400003A RID: 58
		internal const string OptionFormat = "format=";

		// Token: 0x0400003B RID: 59
		internal const string OptionDebugTrap = "debugtrap";

		// Token: 0x0400003C RID: 60
		internal const string OptionDisableVectors = "disable-vectors";

		// Token: 0x0400003D RID: 61
		internal const string OptionDebug = "debug=";

		// Token: 0x0400003E RID: 62
		internal const string OptionDebugMode = "debugMode";

		// Token: 0x0400003F RID: 63
		internal const string OptionStaticLinkage = "generate-static-linkage-methods";

		// Token: 0x04000040 RID: 64
		internal const string OptionJobMarshalling = "generate-job-marshalling-methods";

		// Token: 0x04000041 RID: 65
		internal const string OptionTempDirectory = "temp-folder=";

		// Token: 0x04000042 RID: 66
		internal const string OptionEnableDirectExternalLinking = "enable-direct-external-linking";

		// Token: 0x04000043 RID: 67
		internal const string OptionLinkerOptions = "linker-options=";

		// Token: 0x04000044 RID: 68
		internal const string OptionEnableAutoLayoutFallbackCheck = "enable-autolayout-fallback-check";

		// Token: 0x04000045 RID: 69
		internal const string OptionGenerateLinkXml = "generate-link-xml=";

		// Token: 0x04000046 RID: 70
		internal const string OptionMetaDataGeneration = "meta-data-generation=";

		// Token: 0x04000047 RID: 71
		internal const string OptionDisableStringInterpolationInExceptionMessages = "disable-string-interpolation-in-exception-messages";

		// Token: 0x04000048 RID: 72
		internal const string OptionPlatformConfiguration = "platform-configuration=";

		// Token: 0x04000049 RID: 73
		internal const string OptionCacheDirectory = "cache-directory=";

		// Token: 0x0400004A RID: 74
		internal const string OptionJitDisableFunctionCaching = "disable-function-caching";

		// Token: 0x0400004B RID: 75
		internal const string OptionJitDisableAssemblyCaching = "disable-assembly-caching";

		// Token: 0x0400004C RID: 76
		internal const string OptionJitEnableAssemblyCachingLogs = "enable-assembly-caching-logs";

		// Token: 0x0400004D RID: 77
		internal const string OptionJitEnableSynchronousCompilation = "enable-synchronous-compilation";

		// Token: 0x0400004E RID: 78
		internal const string OptionJitCompilationPriority = "compilation-priority=";

		// Token: 0x0400004F RID: 79
		internal const string OptionJitIsForFunctionPointer = "is-for-function-pointer";

		// Token: 0x04000050 RID: 80
		internal const string OptionJitManagedFunctionPointer = "managed-function-pointer=";

		// Token: 0x04000051 RID: 81
		internal const string OptionJitManagedDelegateHandle = "managed-delegate-handle=";

		// Token: 0x04000052 RID: 82
		internal const string OptionEnableInterpreter = "enable-interpreter";

		// Token: 0x04000053 RID: 83
		internal const string OptionAotAssemblyFolder = "assembly-folder=";

		// Token: 0x04000054 RID: 84
		internal const string OptionRootAssembly = "root-assembly=";

		// Token: 0x04000055 RID: 85
		internal const string OptionIncludeRootAssemblyReferences = "include-root-assembly-references=";

		// Token: 0x04000056 RID: 86
		internal const string OptionAotMethod = "method=";

		// Token: 0x04000057 RID: 87
		internal const string OptionAotType = "type=";

		// Token: 0x04000058 RID: 88
		internal const string OptionAotAssembly = "assembly=";

		// Token: 0x04000059 RID: 89
		internal const string OptionAotOutputPath = "output=";

		// Token: 0x0400005A RID: 90
		internal const string OptionAotKeepIntermediateFiles = "keep-intermediate-files";

		// Token: 0x0400005B RID: 91
		internal const string OptionAotNoLink = "nolink";

		// Token: 0x0400005C RID: 92
		internal const string OptionAotOnlyStaticMethods = "only-static-methods";

		// Token: 0x0400005D RID: 93
		internal const string OptionMethodPrefix = "method-prefix=";

		// Token: 0x0400005E RID: 94
		internal const string OptionAotNoNativeToolchain = "no-native-toolchain";

		// Token: 0x0400005F RID: 95
		internal const string OptionAotEmitLlvmObjects = "emit-llvm-objects";

		// Token: 0x04000060 RID: 96
		internal const string OptionAotKeyFolder = "key-folder=";

		// Token: 0x04000061 RID: 97
		internal const string OptionAotDecodeFolder = "decode-folder=";

		// Token: 0x04000062 RID: 98
		internal const string OptionVerbose = "verbose";

		// Token: 0x04000063 RID: 99
		internal const string OptionValidateExternalToolChain = "validate-external-tool-chain";

		// Token: 0x04000064 RID: 100
		internal const string OptionCompilerThreads = "threads=";

		// Token: 0x04000065 RID: 101
		internal const string OptionChunkSize = "chunk-size=";

		// Token: 0x04000066 RID: 102
		internal const string OptionPrintLogOnMissingPInvokeCallbackAttribute = "print-monopinvokecallbackmissing-message";

		// Token: 0x04000067 RID: 103
		internal const string OptionOutputMode = "output-mode=";

		// Token: 0x04000068 RID: 104
		internal const string OptionAlwaysCreateOutput = "always-create-output=";

		// Token: 0x04000069 RID: 105
		internal const string OptionAotPdbSearchPaths = "pdb-search-paths=";

		// Token: 0x0400006A RID: 106
		internal const string OptionSafetyChecks = "safety-checks";

		// Token: 0x0400006B RID: 107
		internal const string OptionLibraryOutputMode = "library-output-mode=";

		// Token: 0x0400006C RID: 108
		internal const string OptionCompilationId = "compilation-id=";

		// Token: 0x0400006D RID: 109
		internal const string OptionTargetFramework = "target-framework=";

		// Token: 0x0400006E RID: 110
		internal const string OptionDiscardAssemblies = "discard-assemblies=";

		// Token: 0x0400006F RID: 111
		internal const string OptionSaveExtraContext = "save-extra-context";

		// Token: 0x04000070 RID: 112
		internal const string CompilerCommandShutdown = "$shutdown";

		// Token: 0x04000071 RID: 113
		internal const string CompilerCommandCancel = "$cancel";

		// Token: 0x04000072 RID: 114
		internal const string CompilerCommandEnableCompiler = "$enable_compiler";

		// Token: 0x04000073 RID: 115
		internal const string CompilerCommandDisableCompiler = "$disable_compiler";

		// Token: 0x04000074 RID: 116
		internal const string CompilerCommandSetDefaultOptions = "$set_default_options";

		// Token: 0x04000075 RID: 117
		internal const string CompilerCommandTriggerSetupRecompilation = "$trigger_setup_recompilation";

		// Token: 0x04000076 RID: 118
		internal const string CompilerCommandIsCurrentCompilationDone = "$is_current_compilation_done";

		// Token: 0x04000077 RID: 119
		internal const string CompilerCommandTriggerRecompilation = "$trigger_recompilation";

		// Token: 0x04000078 RID: 120
		internal const string CompilerCommandInitialize = "$initialize";

		// Token: 0x04000079 RID: 121
		internal const string CompilerCommandDomainReload = "$domain_reload";

		// Token: 0x0400007A RID: 122
		internal const string CompilerCommandVersionNotification = "$version";

		// Token: 0x0400007B RID: 123
		internal const string CompilerCommandGetTargetCpuFromHost = "$get_target_cpu_from_host";

		// Token: 0x0400007C RID: 124
		internal const string CompilerCommandSetProfileCallbacks = "$set_profile_callbacks";

		// Token: 0x0400007D RID: 125
		internal const string CompilerCommandUnloadBurstNatives = "$unload_burst_natives";

		// Token: 0x0400007E RID: 126
		internal const string CompilerCommandIsNativeApiAvailable = "$is_native_api_available";

		// Token: 0x0400007F RID: 127
		internal const string CompilerCommandILPPCompilation = "$ilpp_compilation";

		// Token: 0x04000080 RID: 128
		internal const string CompilerCommandIsArmTestEnv = "$is_arm_test_env";

		// Token: 0x04000081 RID: 129
		internal const string CompilerCommandNotifyAssemblyCompilationNotRequired = "$notify_assembly_compilation_not_required";

		// Token: 0x04000082 RID: 130
		internal const string CompilerCommandNotifyAssemblyCompilationFinished = "$notify_assembly_compilation_finished";

		// Token: 0x04000083 RID: 131
		internal const string CompilerCommandNotifyCompilationStarted = "$notify_compilation_started";

		// Token: 0x04000084 RID: 132
		internal const string CompilerCommandNotifyCompilationFinished = "$notify_compilation_finished";

		// Token: 0x04000085 RID: 133
		internal const string CompilerCommandAotCompilation = "$aot_compilation";

		// Token: 0x04000086 RID: 134
		internal const string CompilerCommandRequestInitialiseDebuggerCommmand = "$request_debug_command";

		// Token: 0x04000087 RID: 135
		internal const string CompilerCommandInitialiseDebuggerCommmand = "$load_debugger_interface";

		// Token: 0x04000088 RID: 136
		internal const string CompilerCommandRequestSetProtocolVersionEditor = "$request_set_protocol_version_editor";

		// Token: 0x04000089 RID: 137
		internal const string CompilerCommandSetProtocolVersionBurst = "$set_protocol_version_burst";

		// Token: 0x0400008A RID: 138
		internal static readonly bool ForceDisableBurstCompilation;

		// Token: 0x0400008B RID: 139
		private static readonly bool ForceBurstCompilationSynchronously;

		// Token: 0x0400008C RID: 140
		internal static readonly bool IsSecondaryUnityProcess;

		// Token: 0x0400008D RID: 141
		private bool _enableBurstCompilation;

		// Token: 0x0400008E RID: 142
		private bool _enableBurstCompileSynchronously;

		// Token: 0x0400008F RID: 143
		private bool _enableBurstSafetyChecks;

		// Token: 0x04000090 RID: 144
		private bool _enableBurstTimings;

		// Token: 0x04000091 RID: 145
		private bool _enableBurstDebug;

		// Token: 0x04000092 RID: 146
		private bool _forceEnableBurstSafetyChecks;

		// Token: 0x04000093 RID: 147
		[CompilerGenerated]
		private readonly bool <IsGlobal>k__BackingField;

		// Token: 0x04000094 RID: 148
		[CompilerGenerated]
		private Action <OptionsChanged>k__BackingField;
	}
}
