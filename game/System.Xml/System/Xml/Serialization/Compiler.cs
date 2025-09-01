using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x0200027A RID: 634
	internal class Compiler
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0008D160 File Offset: 0x0008B360
		protected string[] Imports
		{
			get
			{
				string[] array = new string[this.imports.Values.Count];
				this.imports.Values.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0008D198 File Offset: 0x0008B398
		internal void AddImport(Type type, Hashtable types)
		{
			if (type == null)
			{
				return;
			}
			if (TypeScope.IsKnownType(type))
			{
				return;
			}
			if (types[type] != null)
			{
				return;
			}
			types[type] = type;
			Type baseType = type.BaseType;
			if (baseType != null)
			{
				this.AddImport(baseType, types);
			}
			Type declaringType = type.DeclaringType;
			if (declaringType != null)
			{
				this.AddImport(declaringType, types);
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				this.AddImport(type2, types);
			}
			ConstructorInfo[] constructors = type.GetConstructors();
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				for (int k = 0; k < parameters.Length; k++)
				{
					this.AddImport(parameters[k].ParameterType, types);
				}
			}
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int l = 0; l < genericArguments.Length; l++)
				{
					this.AddImport(genericArguments[l], types);
				}
			}
			TempAssembly.FileIOPermission.Assert();
			Assembly assembly = type.Module.Assembly;
			if (DynamicAssemblies.IsTypeDynamic(type))
			{
				DynamicAssemblies.Add(assembly);
				return;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes.Length != 0)
			{
				Assembly assembly2 = Assembly.Load((customAttributes[0] as TypeForwardedFromAttribute).AssemblyFullName);
				this.imports[assembly2] = assembly2.Location;
			}
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0008D314 File Offset: 0x0008B514
		internal void AddImport(Assembly assembly)
		{
			TempAssembly.FileIOPermission.Assert();
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0008D332 File Offset: 0x0008B532
		internal TextWriter Source
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0000B528 File Offset: 0x00009728
		internal void Close()
		{
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0008D33C File Offset: 0x0008B53C
		internal static string GetTempAssemblyPath(string baseDir, Assembly assembly, string defaultNamespace)
		{
			if (assembly.IsDynamic)
			{
				throw new InvalidOperationException(Res.GetString("Cannot pre-generate serialization assembly. Pre-generation of serialization assemblies is not supported for dynamic assemblies. Save the assembly and load it from disk to use it with XmlSerialization."));
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			try
			{
				if (baseDir != null && baseDir.Length > 0)
				{
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("Could not find directory to save XmlSerializer generated assembly: {0}.", new object[]
						{
							baseDir
						}));
					}
				}
				else
				{
					baseDir = Path.GetTempPath();
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("Could not find TEMP directory to save XmlSerializer generated assemblies."));
					}
				}
				baseDir = Path.Combine(baseDir, Compiler.GetTempAssemblyName(assembly.GetName(), defaultNamespace));
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return baseDir + ".dll";
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0008D40C File Offset: 0x0008B60C
		internal static string GetTempAssemblyName(AssemblyName parent, string ns)
		{
			return parent.Name + ".XmlSerializers" + ((ns == null || ns.Length == 0) ? "" : ("." + ns.GetHashCode().ToString()));
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008D454 File Offset: 0x0008B654
		internal Assembly Compile(Assembly parent, string ns, XmlSerializerCompilerParameters xmlParameters, Evidence evidence)
		{
			CodeDomProvider codeDomProvider = new CSharpCodeProvider();
			CompilerParameters codeDomParameters = xmlParameters.CodeDomParameters;
			codeDomParameters.ReferencedAssemblies.AddRange(this.Imports);
			if (this.debugEnabled)
			{
				codeDomParameters.GenerateInMemory = false;
				codeDomParameters.IncludeDebugInformation = true;
				codeDomParameters.TempFiles.KeepFiles = true;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			if (xmlParameters.IsNeedTempDirAccess)
			{
				permissionSet.AddPermission(TempAssembly.FileIOPermission);
			}
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlEvidence));
			permissionSet.Assert();
			if (parent != null && (codeDomParameters.OutputAssembly == null || codeDomParameters.OutputAssembly.Length == 0))
			{
				string text = Compiler.AssemblyNameFromOptions(codeDomParameters.CompilerOptions);
				if (text == null)
				{
					text = Compiler.GetTempAssemblyPath(codeDomParameters.TempFiles.TempDir, parent, ns);
				}
				codeDomParameters.OutputAssembly = text;
			}
			if (codeDomParameters.CompilerOptions == null || codeDomParameters.CompilerOptions.Length == 0)
			{
				codeDomParameters.CompilerOptions = "/nostdlib";
			}
			else
			{
				CompilerParameters compilerParameters = codeDomParameters;
				compilerParameters.CompilerOptions += " /nostdlib";
			}
			CompilerParameters compilerParameters2 = codeDomParameters;
			compilerParameters2.CompilerOptions += " /D:_DYNAMIC_XMLSERIALIZER_COMPILATION";
			codeDomParameters.Evidence = evidence;
			CompilerResults compilerResults = null;
			Assembly assembly = null;
			try
			{
				compilerResults = codeDomProvider.CompileAssemblyFromSource(codeDomParameters, new string[]
				{
					this.writer.ToString()
				});
				if (compilerResults.Errors.Count > 0)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					stringWriter.WriteLine(Res.GetString("Unable to generate a temporary class (result={0}).", new object[]
					{
						compilerResults.NativeCompilerReturnValue.ToString(CultureInfo.InvariantCulture)
					}));
					bool flag = false;
					foreach (object obj in compilerResults.Errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						compilerError.FileName = "";
						if (!compilerError.IsWarning || compilerError.ErrorNumber == "CS1595")
						{
							flag = true;
							stringWriter.WriteLine(compilerError.ToString());
						}
					}
					if (flag)
					{
						throw new InvalidOperationException(stringWriter.ToString());
					}
				}
				assembly = compilerResults.CompiledAssembly;
			}
			catch (UnauthorizedAccessException)
			{
				string currentUser = Compiler.GetCurrentUser();
				if (currentUser == null || currentUser.Length == 0)
				{
					throw new UnauthorizedAccessException(Res.GetString("Access to the temp directory is denied.  The process under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn�t have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location."));
				}
				throw new UnauthorizedAccessException(Res.GetString("Access to the temp directory is denied.  Identity '{0}' under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn�t have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location.", new object[]
				{
					currentUser
				}));
			}
			catch (FileLoadException innerException)
			{
				throw new InvalidOperationException(Res.GetString("Cannot load dynamically generated serialization assembly. In some hosting environments assembly load functionality is restricted, consider using pre-generated serializer. Please see inner exception for more information."), innerException);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (assembly == null)
			{
				throw new InvalidOperationException(Res.GetString("Internal error."));
			}
			return assembly;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0008D768 File Offset: 0x0008B968
		private static string AssemblyNameFromOptions(string options)
		{
			if (options == null || options.Length == 0)
			{
				return null;
			}
			string result = null;
			string[] array = options.ToLower(CultureInfo.InvariantCulture).Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.StartsWith("/out:", StringComparison.Ordinal))
				{
					result = text.Substring(5);
				}
			}
			return result;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0008D7C4 File Offset: 0x0008B9C4
		internal static string GetCurrentUser()
		{
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null && current.Name != null)
				{
					return current.Name;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
			return "";
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0008D824 File Offset: 0x0008BA24
		public Compiler()
		{
		}

		// Token: 0x0400189E RID: 6302
		private bool debugEnabled = DiagnosticsSwitches.KeepTempFiles.Enabled;

		// Token: 0x0400189F RID: 6303
		private Hashtable imports = new Hashtable();

		// Token: 0x040018A0 RID: 6304
		private StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
	}
}
