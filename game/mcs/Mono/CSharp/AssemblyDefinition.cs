using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using Mono.CompilerServices.SymbolWriter;
using Mono.Security.Cryptography;

namespace Mono.CSharp
{
	// Token: 0x02000100 RID: 256
	public abstract class AssemblyDefinition : IAssemblyDefinition
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002D840 File Offset: 0x0002BA40
		protected AssemblyDefinition(ModuleContainer module, string name)
		{
			this.module = module;
			this.name = Path.GetFileNameWithoutExtension(name);
			this.wrap_non_exception_throws = true;
			this.delay_sign = this.Compiler.Settings.StrongNameDelaySign;
			if (this.Compiler.Settings.HasKeyFileOrContainer)
			{
				this.LoadPublicKey(this.Compiler.Settings.StrongNameKeyFile, this.Compiler.Settings.StrongNameKeyContainer);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002D8BB File Offset: 0x0002BABB
		protected AssemblyDefinition(ModuleContainer module, string name, string fileName) : this(module, name)
		{
			this.file_name = fileName;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002D8CC File Offset: 0x0002BACC
		public Attribute CLSCompliantAttribute
		{
			get
			{
				return this.cls_attribute;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
		public CompilerContext Compiler
		{
			get
			{
				return this.module.Compiler;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002D8E1 File Offset: 0x0002BAE1
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0002D8E9 File Offset: 0x0002BAE9
		public Method EntryPoint
		{
			get
			{
				return this.entry_point;
			}
			set
			{
				this.entry_point = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002D8F2 File Offset: 0x0002BAF2
		public string FullName
		{
			get
			{
				return this.Builder.FullName;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0002D8FF File Offset: 0x0002BAFF
		public bool HasCLSCompliantAttribute
		{
			get
			{
				return this.cls_attribute != null;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002D90A File Offset: 0x0002BB0A
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0002D912 File Offset: 0x0002BB12
		public MetadataImporter Importer
		{
			[CompilerGenerated]
			get
			{
				return this.<Importer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Importer>k__BackingField = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002D91B File Offset: 0x0002BB1B
		public bool IsCLSCompliant
		{
			get
			{
				return this.is_cls_compliant;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000022F4 File Offset: 0x000004F4
		bool IAssemblyDefinition.IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002D923 File Offset: 0x0002BB23
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0002D92B File Offset: 0x0002BB2B
		public bool IsSatelliteAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsSatelliteAssembly>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsSatelliteAssembly>k__BackingField = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002D934 File Offset: 0x0002BB34
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002D93C File Offset: 0x0002BB3C
		public bool WrapNonExceptionThrows
		{
			get
			{
				return this.wrap_non_exception_throws;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002D944 File Offset: 0x0002BB44
		protected Report Report
		{
			get
			{
				return this.Compiler.Report;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002D951 File Offset: 0x0002BB51
		public MonoSymbolFile SymbolWriter
		{
			get
			{
				return this.symbol_writer;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002D959 File Offset: 0x0002BB59
		public void AddModule(ImportedModuleDefinition module)
		{
			if (this.added_modules == null)
			{
				this.added_modules = new List<ImportedModuleDefinition>();
				this.added_modules.Add(module);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002D97C File Offset: 0x0002BB7C
		public void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			if (a.Type == pa.AssemblyCulture)
			{
				string text = a.GetString();
				if (text == null || text.Length == 0)
				{
					return;
				}
				if (this.Compiler.Settings.Target == Target.Exe)
				{
					this.Report.Error(7059, a.Location, "Executables cannot be satellite assemblies. Remove the attribute or keep it empty");
					return;
				}
				if (text == "neutral")
				{
					text = "";
				}
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.SetCustomAttribute(ctor, cdata);
				}
				else
				{
					this.builder_extra.SetCulture(text, a.Location);
				}
				this.IsSatelliteAssembly = true;
				return;
			}
			else if (a.Type == pa.AssemblyVersion)
			{
				string @string = a.GetString();
				if (@string == null || @string.Length == 0)
				{
					return;
				}
				Version version = AssemblyDefinition.IsValidAssemblyVersion(@string, true);
				if (version == null)
				{
					this.Report.Error(7034, a.Location, "The specified version string `{0}' does not conform to the required format - major[.minor[.build[.revision]]]", @string);
					return;
				}
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.SetCustomAttribute(ctor, cdata);
					return;
				}
				this.builder_extra.SetVersion(version, a.Location);
				return;
			}
			else if (a.Type == pa.AssemblyAlgorithmId)
			{
				uint num = (uint)cdata[2];
				num |= (uint)((uint)cdata[3] << 8);
				num |= (uint)((uint)cdata[4] << 16);
				num |= (uint)((uint)cdata[5] << 24);
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.SetCustomAttribute(ctor, cdata);
					return;
				}
				this.builder_extra.SetAlgorithmId(num, a.Location);
				return;
			}
			else if (a.Type == pa.AssemblyFlags)
			{
				uint num2 = (uint)cdata[2];
				num2 |= (uint)((uint)cdata[3] << 8);
				num2 |= (uint)((uint)cdata[4] << 16);
				num2 |= (uint)((uint)cdata[5] << 24);
				if ((num2 & 1U) != 0U && this.public_key == null)
				{
					num2 &= 4294967294U;
				}
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.SetCustomAttribute(ctor, cdata);
					return;
				}
				this.builder_extra.SetFlags(num2, a.Location);
				return;
			}
			else if (a.Type == pa.TypeForwarder)
			{
				TypeSpec argumentType = a.GetArgumentType();
				if (argumentType == null || TypeManager.HasElementType(argumentType))
				{
					this.Report.Error(735, a.Location, "Invalid type specified as an argument for TypeForwardedTo attribute");
					return;
				}
				if (this.emitted_forwarders == null)
				{
					this.emitted_forwarders = new Dictionary<ITypeDefinition, Attribute>();
				}
				else if (this.emitted_forwarders.ContainsKey(argumentType.MemberDefinition))
				{
					this.Report.SymbolRelatedToPreviousError(this.emitted_forwarders[argumentType.MemberDefinition].Location, null);
					this.Report.Error(739, a.Location, "A duplicate type forward of type `{0}'", argumentType.GetSignatureForError());
					return;
				}
				this.emitted_forwarders.Add(argumentType.MemberDefinition, a);
				if (argumentType.MemberDefinition.DeclaringAssembly == this)
				{
					this.Report.SymbolRelatedToPreviousError(argumentType);
					this.Report.Error(729, a.Location, "Cannot forward type `{0}' because it is defined in this assembly", argumentType.GetSignatureForError());
					return;
				}
				if (argumentType.IsNested)
				{
					this.Report.Error(730, a.Location, "Cannot forward type `{0}' because it is a nested type", argumentType.GetSignatureForError());
					return;
				}
				this.builder_extra.AddTypeForwarder(argumentType.GetDefinition(), a.Location);
				return;
			}
			else
			{
				if (a.Type == pa.Extension)
				{
					a.Error_MisusedExtensionAttribute();
					return;
				}
				if (a.Type == pa.InternalsVisibleTo)
				{
					string string2 = a.GetString();
					if (string2 == null)
					{
						this.Report.Error(7030, a.Location, "Friend assembly reference cannot have `null' value");
						return;
					}
					if (string2.Length == 0)
					{
						return;
					}
				}
				else if (a.Type == pa.RuntimeCompatibility)
				{
					this.wrap_non_exception_throws_custom = true;
				}
				else if (a.Type == pa.AssemblyFileVersion)
				{
					this.vi_product_version = a.GetString();
					if (string.IsNullOrEmpty(this.vi_product_version) || AssemblyDefinition.IsValidAssemblyVersion(this.vi_product_version, false) == null)
					{
						this.Report.Warning(7035, 1, a.Location, "The specified version string `{0}' does not conform to the recommended format major.minor.build.revision", this.vi_product_version, a.Name);
						return;
					}
					CustomAttributeBuilder customAttribute = new CustomAttributeBuilder((ConstructorInfo)ctor.GetMetaInfo(), new object[]
					{
						this.vi_product_version
					});
					this.Builder.SetCustomAttribute(customAttribute);
					return;
				}
				else if (a.Type == pa.AssemblyProduct)
				{
					this.vi_product = a.GetString();
				}
				else if (a.Type == pa.AssemblyCompany)
				{
					this.vi_company = a.GetString();
				}
				else if (a.Type == pa.AssemblyCopyright)
				{
					this.vi_copyright = a.GetString();
				}
				else if (a.Type == pa.AssemblyTrademark)
				{
					this.vi_trademark = a.GetString();
				}
				else if (a.Type == pa.Debuggable)
				{
					this.has_user_debuggable = true;
				}
				this.SetCustomAttribute(ctor, cdata);
				return;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		private void CheckReferencesPublicToken()
		{
			foreach (IAssemblyDefinition assemblyDefinition in this.Importer.Assemblies)
			{
				ImportedAssemblyDefinition importedAssemblyDefinition = assemblyDefinition as ImportedAssemblyDefinition;
				if (importedAssemblyDefinition != null && !importedAssemblyDefinition.IsMissing)
				{
					if (this.public_key != null && !importedAssemblyDefinition.HasStrongName)
					{
						this.Report.Error(1577, "Referenced assembly `{0}' does not have a strong name", importedAssemblyDefinition.FullName);
					}
					CultureInfo cultureInfo = importedAssemblyDefinition.Assembly.GetName().CultureInfo;
					if (!cultureInfo.Equals(CultureInfo.InvariantCulture))
					{
						this.Report.Warning(8009, 1, "Referenced assembly `{0}' has different culture setting of `{1}'", importedAssemblyDefinition.Name, cultureInfo.Name);
					}
					if (importedAssemblyDefinition.IsFriendAssemblyTo(this))
					{
						AssemblyName assemblyVisibleToName = importedAssemblyDefinition.GetAssemblyVisibleToName(this);
						byte[] publicKeyToken = assemblyVisibleToName.GetPublicKeyToken();
						if (!ArrayComparer.IsEqual<byte>(this.GetPublicKeyToken(), publicKeyToken))
						{
							this.Report.SymbolRelatedToPreviousError(importedAssemblyDefinition.Location);
							this.Report.Error(281, "Friend access was granted to `{0}', but the output assembly is named `{1}'. Try adding a reference to `{0}' or change the output assembly name to match it", assemblyVisibleToName.FullName, this.FullName);
						}
					}
				}
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		protected AssemblyName CreateAssemblyName()
		{
			AssemblyName assemblyName = new AssemblyName(this.name);
			if (this.public_key != null && this.Compiler.Settings.Target != Target.Module)
			{
				if (this.delay_sign)
				{
					assemblyName.SetPublicKey(this.public_key);
				}
				else if (this.public_key.Length == 16)
				{
					this.Report.Error(1606, "Could not sign the assembly. ECMA key can only be used to delay-sign assemblies");
				}
				else if (this.private_key == null)
				{
					this.Error_AssemblySigning("The specified key file does not have a private key");
				}
				else
				{
					assemblyName.KeyPair = this.private_key;
				}
			}
			return assemblyName;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002E080 File Offset: 0x0002C280
		public virtual ModuleBuilder CreateModuleBuilder()
		{
			if (this.file_name == null)
			{
				throw new NotSupportedException("transient module in static assembly");
			}
			string fileName = Path.GetFileName(this.file_name);
			AssemblyBuilder builder = this.Builder;
			string fileName2 = fileName;
			return builder.DefineDynamicModule(fileName2, fileName2, false);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002E0BC File Offset: 0x0002C2BC
		public virtual void Emit()
		{
			if (this.Compiler.Settings.Target == Target.Module)
			{
				this.module_target_attrs = new AssemblyAttributesPlaceholder(this.module, this.name);
				this.module_target_attrs.CreateContainer();
				this.module_target_attrs.DefineContainer();
				this.module_target_attrs.Define();
				this.module.AddCompilerGeneratedClass(this.module_target_attrs);
			}
			else if (this.added_modules != null)
			{
				this.ReadModulesAssemblyAttributes();
			}
			if (this.Compiler.Settings.GenerateDebugInfo)
			{
				this.symbol_writer = new MonoSymbolFile();
			}
			this.module.EmitContainer();
			if (this.module.HasExtensionMethod)
			{
				PredefinedAttribute extension = this.module.PredefinedAttributes.Extension;
				if (extension.IsDefined)
				{
					this.SetCustomAttribute(extension.Constructor, AttributeEncoder.Empty);
				}
			}
			if (!this.IsSatelliteAssembly)
			{
				if (!this.has_user_debuggable && this.Compiler.Settings.GenerateDebugInfo)
				{
					PredefinedDebuggableAttribute debuggable = this.module.PredefinedAttributes.Debuggable;
					if (debuggable.IsDefined)
					{
						DebuggableAttribute.DebuggingModes debuggingModes = DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints;
						if (!this.Compiler.Settings.Optimize)
						{
							debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
						}
						debuggable.EmitAttribute(this.Builder, debuggingModes);
					}
				}
				if (!this.wrap_non_exception_throws_custom)
				{
					PredefinedAttribute runtimeCompatibility = this.module.PredefinedAttributes.RuntimeCompatibility;
					if (runtimeCompatibility.IsDefined && runtimeCompatibility.ResolveBuilder())
					{
						PropertySpec propertySpec = this.module.PredefinedMembers.RuntimeCompatibilityWrapNonExceptionThrows.Get();
						if (propertySpec != null)
						{
							AttributeEncoder attributeEncoder = new AttributeEncoder();
							attributeEncoder.EncodeNamedPropertyArgument(propertySpec, new BoolLiteral(this.Compiler.BuiltinTypes, true, Location.Null));
							this.SetCustomAttribute(runtimeCompatibility.Constructor, attributeEncoder.ToArray());
						}
					}
				}
				if (this.declarative_security != null)
				{
					throw new NotSupportedException("Assembly-level security");
				}
			}
			this.CheckReferencesPublicToken();
			this.SetEntryPoint();
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002E29C File Offset: 0x0002C49C
		public byte[] GetPublicKeyToken()
		{
			if (this.public_key == null || this.public_key_token != null)
			{
				return this.public_key_token;
			}
			byte[] array = SHA1.Create().ComputeHash(this.public_key);
			this.public_key_token = new byte[8];
			Buffer.BlockCopy(array, array.Length - 8, this.public_key_token, 0, 8);
			Array.Reverse(this.public_key_token, 0, 8);
			return this.public_key_token;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002E304 File Offset: 0x0002C504
		private void LoadPublicKey(string keyFile, string keyContainer)
		{
			if (keyContainer != null)
			{
				try
				{
					this.private_key = new StrongNameKeyPair(keyContainer);
					this.public_key = this.private_key.PublicKey;
				}
				catch
				{
					this.Error_AssemblySigning("The specified key container `" + keyContainer + "' does not exist");
				}
				return;
			}
			bool flag = File.Exists(keyFile);
			if (!flag && this.Compiler.Settings.StrongNameKeyFile == null)
			{
				string text = Path.Combine(Path.GetDirectoryName(this.file_name), keyFile);
				flag = File.Exists(text);
				if (flag)
				{
					keyFile = text;
				}
			}
			if (!flag)
			{
				this.Error_AssemblySigning("The specified key file `" + keyFile + "' does not exist");
				return;
			}
			using (FileStream fileStream = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
			{
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				if (array.Length == 16)
				{
					this.public_key = array;
				}
				else
				{
					try
					{
						byte[] array2 = CryptoConvert.ToCapiPublicKeyBlob(CryptoConvert.FromCapiKeyBlob(array));
						byte[] array3 = new byte[]
						{
							0,
							36,
							0,
							0,
							4,
							128,
							0,
							0
						};
						this.public_key = new byte[12 + array2.Length];
						Buffer.BlockCopy(array3, 0, this.public_key, 0, array3.Length);
						int num = this.public_key.Length - 12;
						this.public_key[8] = (byte)(num & 255);
						this.public_key[9] = (byte)(num >> 8 & 255);
						this.public_key[10] = (byte)(num >> 16 & 255);
						this.public_key[11] = (byte)(num >> 24 & 255);
						Buffer.BlockCopy(array2, 0, this.public_key, 12, array2.Length);
					}
					catch
					{
						this.Error_AssemblySigning("The specified key file `" + keyFile + "' has incorrect format");
						return;
					}
					if (!this.delay_sign)
					{
						try
						{
							CryptoConvert.FromCapiPrivateKeyBlob(array);
							this.private_key = new StrongNameKeyPair(array);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002E53C File Offset: 0x0002C73C
		private void ReadModulesAssemblyAttributes()
		{
			foreach (ImportedModuleDefinition importedModuleDefinition in this.added_modules)
			{
				List<Attribute> list = importedModuleDefinition.ReadAssemblyAttributes();
				if (list != null)
				{
					this.module.OptAttributes.AddAttributes(list);
				}
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		public void Resolve()
		{
			if (this.Compiler.Settings.Unsafe && this.module.PredefinedTypes.SecurityAction.Define())
			{
				Location @null = Location.Null;
				MemberAccess expr = new MemberAccess(new MemberAccess(new QualifiedAliasMember(QualifiedAliasMember.GlobalAlias, "System", @null), "Security", @null), "Permissions", @null);
				ConstSpec constSpec = this.module.PredefinedMembers.SecurityActionRequestMinimum.Resolve(@null);
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(constSpec.GetConstant(null)));
				Arguments arguments2 = new Arguments(1);
				arguments2.Add(new NamedArgument("SkipVerification", @null, new BoolLiteral(this.Compiler.BuiltinTypes, true, @null)));
				Attribute attribute = new Attribute("assembly", new MemberAccess(expr, "SecurityPermissionAttribute"), new Arguments[]
				{
					arguments,
					arguments2
				}, @null, false);
				attribute.AttachTo(this.module, this.module);
				this.Compiler.Report.DisableReporting();
				try
				{
					MethodSpec methodSpec = attribute.Resolve();
					if (methodSpec != null)
					{
						attribute.ExtractSecurityPermissionSet(methodSpec, ref this.declarative_security);
					}
				}
				finally
				{
					this.Compiler.Report.EnableReporting();
				}
			}
			if (this.module.OptAttributes == null)
			{
				return;
			}
			if (!this.module.OptAttributes.CheckTargets())
			{
				return;
			}
			this.cls_attribute = this.module.ResolveAssemblyAttribute(this.module.PredefinedAttributes.CLSCompliant);
			if (this.cls_attribute != null)
			{
				this.is_cls_compliant = this.cls_attribute.GetClsCompliantAttributeValue();
			}
			if (this.added_modules != null && this.Compiler.Settings.VerifyClsCompliance && this.is_cls_compliant)
			{
				foreach (ImportedModuleDefinition importedModuleDefinition in this.added_modules)
				{
					if (!importedModuleDefinition.IsCLSCompliant)
					{
						this.Report.Error(3013, "Added modules must be marked with the CLSCompliant attribute to match the assembly", importedModuleDefinition.Name);
					}
				}
			}
			Attribute attribute2 = this.module.ResolveAssemblyAttribute(this.module.PredefinedAttributes.RuntimeCompatibility);
			if (attribute2 != null)
			{
				BoolConstant boolConstant = attribute2.GetNamedValue("WrapNonExceptionThrows") as BoolConstant;
				if (boolConstant != null)
				{
					this.wrap_non_exception_throws = boolConstant.Value;
				}
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002E818 File Offset: 0x0002CA18
		protected void ResolveAssemblySecurityAttributes()
		{
			string text = null;
			string text2 = null;
			if (this.module.OptAttributes != null)
			{
				foreach (Attribute attribute in this.module.OptAttributes.Attrs)
				{
					if (!(attribute.ExplicitTarget != "assembly"))
					{
						string text3 = attribute.Name;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
						if (num <= 2226734543U)
						{
							if (num <= 1216203559U)
							{
								if (num != 1043911657U)
								{
									if (num != 1216203559U)
									{
										continue;
									}
									if (!(text3 == "AssemblyKeyName"))
									{
										continue;
									}
									goto IL_228;
								}
								else
								{
									if (!(text3 == "AssemblyDelaySign"))
									{
										continue;
									}
									goto IL_295;
								}
							}
							else if (num != 1957892582U)
							{
								if (num != 2226734543U)
								{
									continue;
								}
								if (!(text3 == "System.Reflection.AssemblyKeyNameAttribute"))
								{
									continue;
								}
								goto IL_228;
							}
							else if (!(text3 == "AssemblyKeyFileAttribute"))
							{
								continue;
							}
						}
						else if (num <= 2873267267U)
						{
							if (num != 2490792305U)
							{
								if (num != 2873267267U)
								{
									continue;
								}
								if (!(text3 == "AssemblyDelaySignAttribute"))
								{
									continue;
								}
								goto IL_295;
							}
							else
							{
								if (!(text3 == "System.Reflection.AssemblyDelaySignAttribute"))
								{
									continue;
								}
								goto IL_295;
							}
						}
						else if (num != 3054398234U)
						{
							if (num != 3096503613U)
							{
								if (num != 3808820256U)
								{
									continue;
								}
								if (!(text3 == "System.Reflection.AssemblyKeyFileAttribute"))
								{
									continue;
								}
							}
							else
							{
								if (!(text3 == "AssemblyKeyNameAttribute"))
								{
									continue;
								}
								goto IL_228;
							}
						}
						else if (!(text3 == "AssemblyKeyFile"))
						{
							continue;
						}
						if (this.Compiler.Settings.StrongNameKeyFile != null)
						{
							this.Report.SymbolRelatedToPreviousError(attribute.Location, attribute.GetSignatureForError());
							this.Report.Warning(1616, 1, "Option `{0}' overrides attribute `{1}' given in a source file or added module", "keyfile", "System.Reflection.AssemblyKeyFileAttribute");
							continue;
						}
						string @string = attribute.GetString();
						if (!string.IsNullOrEmpty(@string))
						{
							this.Error_ObsoleteSecurityAttribute(attribute, "keyfile");
							text = @string;
							continue;
						}
						continue;
						IL_228:
						if (this.Compiler.Settings.StrongNameKeyContainer != null)
						{
							this.Report.SymbolRelatedToPreviousError(attribute.Location, attribute.GetSignatureForError());
							this.Report.Warning(1616, 1, "Option `{0}' overrides attribute `{1}' given in a source file or added module", "keycontainer", "System.Reflection.AssemblyKeyNameAttribute");
							continue;
						}
						string string2 = attribute.GetString();
						if (!string.IsNullOrEmpty(string2))
						{
							this.Error_ObsoleteSecurityAttribute(attribute, "keycontainer");
							text2 = string2;
							continue;
						}
						continue;
						IL_295:
						bool boolean = attribute.GetBoolean();
						if (boolean)
						{
							this.Error_ObsoleteSecurityAttribute(attribute, "delaysign");
						}
						this.delay_sign = boolean;
					}
				}
			}
			if (this.public_key != null)
			{
				return;
			}
			if (text != null || text2 != null)
			{
				this.LoadPublicKey(text, text2);
				return;
			}
			if (this.delay_sign)
			{
				this.Report.Warning(1607, 1, "Delay signing was requested but no key file was given");
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002EB48 File Offset: 0x0002CD48
		public void EmbedResources()
		{
			if (this.Compiler.Settings.Win32ResourceFile != null)
			{
				this.Builder.DefineUnmanagedResource(this.Compiler.Settings.Win32ResourceFile);
			}
			else
			{
				this.Builder.DefineVersionInfoResource(this.vi_product, this.vi_product_version, this.vi_company, this.vi_copyright, this.vi_trademark);
			}
			if (this.Compiler.Settings.Win32IconFile != null)
			{
				this.builder_extra.DefineWin32IconResource(this.Compiler.Settings.Win32IconFile);
			}
			if (this.Compiler.Settings.Resources != null)
			{
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.Report.Error(1507, "Cannot link resource file when building a module");
					return;
				}
				int num = 0;
				foreach (AssemblyResource assemblyResource in this.Compiler.Settings.Resources)
				{
					if (!File.Exists(assemblyResource.FileName))
					{
						this.Report.Error(1566, "Error reading resource file `{0}'", assemblyResource.FileName);
					}
					else if (assemblyResource.IsEmbeded)
					{
						Stream stream;
						if (num++ < 10)
						{
							stream = File.OpenRead(assemblyResource.FileName);
						}
						else
						{
							stream = new MemoryStream(File.ReadAllBytes(assemblyResource.FileName));
						}
						this.module.Builder.DefineManifestResource(assemblyResource.Name, stream, assemblyResource.Attributes);
					}
					else
					{
						this.Builder.AddResourceFile(assemblyResource.Name, Path.GetFileName(assemblyResource.FileName), assemblyResource.Attributes);
					}
				}
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002ED04 File Offset: 0x0002CF04
		public void Save()
		{
			PortableExecutableKinds portableExecutableKinds = PortableExecutableKinds.ILOnly;
			ImageFileMachine imageFileMachine;
			switch (this.Compiler.Settings.Platform)
			{
			case Platform.AnyCPU32Preferred:
				throw new NotSupportedException();
			case Platform.Arm:
				throw new NotSupportedException();
			case Platform.X86:
				portableExecutableKinds |= PortableExecutableKinds.Required32Bit;
				imageFileMachine = ImageFileMachine.I386;
				goto IL_65;
			case Platform.X64:
				portableExecutableKinds |= PortableExecutableKinds.PE32Plus;
				imageFileMachine = ImageFileMachine.AMD64;
				goto IL_65;
			case Platform.IA64:
				imageFileMachine = ImageFileMachine.IA64;
				goto IL_65;
			}
			imageFileMachine = ImageFileMachine.I386;
			IL_65:
			this.Compiler.TimeReporter.Start(TimeReporter.TimerType.OutputSave);
			try
			{
				if (this.Compiler.Settings.Target == Target.Module)
				{
					this.SaveModule(portableExecutableKinds, imageFileMachine);
				}
				else
				{
					this.Builder.Save(this.module.Builder.ScopeName, portableExecutableKinds, imageFileMachine);
				}
			}
			catch (Exception ex)
			{
				this.Report.Error(16, "Could not write to file `" + this.name + "', cause: " + ex.Message);
			}
			this.Compiler.TimeReporter.Stop(TimeReporter.TimerType.OutputSave);
			if (this.symbol_writer != null && this.Compiler.Report.Errors == 0)
			{
				this.Compiler.TimeReporter.Start(TimeReporter.TimerType.DebugSave);
				string path = this.file_name + ".mdb";
				try
				{
					File.Delete(path);
				}
				catch
				{
				}
				this.module.WriteDebugSymbol(this.symbol_writer);
				using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
				{
					this.symbol_writer.CreateSymbolFile(this.module.Builder.ModuleVersionId, fileStream);
				}
				this.Compiler.TimeReporter.Stop(TimeReporter.TimerType.DebugSave);
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		protected virtual void SaveModule(PortableExecutableKinds pekind, ImageFileMachine machine)
		{
			this.Report.RuntimeMissingSupport(Location.Null, "-target:module");
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002EEEB File Offset: 0x0002D0EB
		private void SetCustomAttribute(MethodSpec ctor, byte[] data)
		{
			if (this.module_target_attrs != null)
			{
				this.module_target_attrs.AddAssemblyAttribute(ctor, data);
				return;
			}
			this.Builder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), data);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002EF1C File Offset: 0x0002D11C
		private void SetEntryPoint()
		{
			if (!this.Compiler.Settings.NeedsEntryPoint)
			{
				if (this.Compiler.Settings.MainClass != null)
				{
					this.Report.Error(2017, "Cannot specify -main if building a module or library");
				}
				return;
			}
			PEFileKinds fileKind;
			switch (this.Compiler.Settings.Target)
			{
			case Target.Library:
			case Target.Module:
				fileKind = PEFileKinds.Dll;
				goto IL_6D;
			case Target.WinExe:
				fileKind = PEFileKinds.WindowApplication;
				goto IL_6D;
			}
			fileKind = PEFileKinds.ConsoleApplication;
			IL_6D:
			if (this.entry_point != null)
			{
				this.Builder.SetEntryPoint(this.entry_point.MethodBuilder, fileKind);
				return;
			}
			string mainClass = this.Compiler.Settings.MainClass;
			if (mainClass == null)
			{
				string arg = (this.file_name == null) ? this.name : Path.GetFileName(this.file_name);
				this.Report.Error(5001, "Program `{0}' does not contain a static `Main' method suitable for an entry point", arg);
				return;
			}
			TypeSpec typeSpec = this.module.GlobalRootNamespace.LookupType(this.module, mainClass, 0, LookupMode.Probing, Location.Null);
			if (typeSpec == null)
			{
				this.Report.Error(1555, "Could not find `{0}' specified for Main method", mainClass);
				return;
			}
			ClassOrStruct classOrStruct = typeSpec.MemberDefinition as ClassOrStruct;
			if (classOrStruct == null)
			{
				this.Report.Error(1556, "`{0}' specified for Main method must be a valid class or struct", mainClass);
				return;
			}
			this.Report.Error(1558, classOrStruct.Location, "`{0}' does not have a suitable static Main method", classOrStruct.GetSignatureForError());
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002F089 File Offset: 0x0002D289
		private void Error_ObsoleteSecurityAttribute(Attribute a, string option)
		{
			this.Report.Warning(1699, 1, a.Location, "Use compiler option `{0}' or appropriate project settings instead of `{1}' attribute", option, a.Name);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002F0AE File Offset: 0x0002D2AE
		private void Error_AssemblySigning(string text)
		{
			this.Report.Error(1548, "Error during assembly signing. " + text);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsFriendAssemblyTo(IAssemblyDefinition assembly)
		{
			return false;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002F0CC File Offset: 0x0002D2CC
		private static Version IsValidAssemblyVersion(string version, bool allowGenerated)
		{
			string[] array = version.Split(new char[]
			{
				'.'
			});
			if (array.Length < 1 || array.Length > 4)
			{
				return null;
			}
			int[] array2 = new int[4];
			for (int i = 0; i < array.Length; i++)
			{
				if (!int.TryParse(array[i], out array2[i]))
				{
					if (array[i].Length == 1 && array[i][0] == '*' && allowGenerated)
					{
						if (i == 2)
						{
							if (array.Length > 3)
							{
								return null;
							}
							array2[i] = Math.Max((DateTime.Today - new DateTime(2000, 1, 1)).Days, 0);
							i = 3;
						}
						if (i == 3)
						{
							array2[i] = (int)(DateTime.Now - DateTime.Today).TotalSeconds / 2;
							goto IL_C5;
						}
					}
					return null;
				}
				if (array2[i] > 65535)
				{
					return null;
				}
				IL_C5:;
			}
			return new Version(array2[0], array2[1], array2[2], array2[3]);
		}

		// Token: 0x0400061B RID: 1563
		public AssemblyBuilder Builder;

		// Token: 0x0400061C RID: 1564
		protected AssemblyBuilderExtension builder_extra;

		// Token: 0x0400061D RID: 1565
		private MonoSymbolFile symbol_writer;

		// Token: 0x0400061E RID: 1566
		private bool is_cls_compliant;

		// Token: 0x0400061F RID: 1567
		private bool wrap_non_exception_throws;

		// Token: 0x04000620 RID: 1568
		private bool wrap_non_exception_throws_custom;

		// Token: 0x04000621 RID: 1569
		private bool has_user_debuggable;

		// Token: 0x04000622 RID: 1570
		protected ModuleContainer module;

		// Token: 0x04000623 RID: 1571
		private readonly string name;

		// Token: 0x04000624 RID: 1572
		protected readonly string file_name;

		// Token: 0x04000625 RID: 1573
		private byte[] public_key;

		// Token: 0x04000626 RID: 1574
		private byte[] public_key_token;

		// Token: 0x04000627 RID: 1575
		private bool delay_sign;

		// Token: 0x04000628 RID: 1576
		private StrongNameKeyPair private_key;

		// Token: 0x04000629 RID: 1577
		private Attribute cls_attribute;

		// Token: 0x0400062A RID: 1578
		private Method entry_point;

		// Token: 0x0400062B RID: 1579
		protected List<ImportedModuleDefinition> added_modules;

		// Token: 0x0400062C RID: 1580
		private Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x0400062D RID: 1581
		private Dictionary<ITypeDefinition, Attribute> emitted_forwarders;

		// Token: 0x0400062E RID: 1582
		private AssemblyAttributesPlaceholder module_target_attrs;

		// Token: 0x0400062F RID: 1583
		private string vi_product;

		// Token: 0x04000630 RID: 1584
		private string vi_product_version;

		// Token: 0x04000631 RID: 1585
		private string vi_company;

		// Token: 0x04000632 RID: 1586
		private string vi_copyright;

		// Token: 0x04000633 RID: 1587
		private string vi_trademark;

		// Token: 0x04000634 RID: 1588
		[CompilerGenerated]
		private MetadataImporter <Importer>k__BackingField;

		// Token: 0x04000635 RID: 1589
		[CompilerGenerated]
		private bool <IsSatelliteAssembly>k__BackingField;
	}
}
