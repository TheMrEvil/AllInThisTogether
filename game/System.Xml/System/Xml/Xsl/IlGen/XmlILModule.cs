using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004AD RID: 1197
	internal class XmlILModule
	{
		// Token: 0x06002EC8 RID: 11976 RVA: 0x00111254 File Offset: 0x0010F454
		static XmlILModule()
		{
			XmlILModule.CreateModulePermissionSet = new PermissionSet(PermissionState.None);
			XmlILModule.CreateModulePermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
			XmlILModule.CreateModulePermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlEvidence));
			XmlILModule.AssemblyId = 0L;
			AssemblyName name = XmlILModule.CreateAssemblyName();
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			try
			{
				XmlILModule.CreateModulePermissionSet.Assert();
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Transparent, new object[0]));
				XmlILModule.LREModule = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", false);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00111358 File Offset: 0x0010F558
		public XmlILModule(TypeBuilder typeBldr)
		{
			this.typeBldr = typeBldr;
			this.emitSymbols = (((ModuleBuilder)this.typeBldr.Module).GetSymWriter() != null);
			this.useLRE = false;
			this.persistAsm = false;
			this.methods = new Hashtable();
			if (this.emitSymbols)
			{
				this.urlToSymWriter = new Hashtable();
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x001113BC File Offset: 0x0010F5BC
		public bool EmitSymbols
		{
			get
			{
				return this.emitSymbols;
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x001113C4 File Offset: 0x0010F5C4
		public XmlILModule(bool useLRE, bool emitSymbols)
		{
			this.useLRE = useLRE;
			this.emitSymbols = emitSymbols;
			this.persistAsm = false;
			this.methods = new Hashtable();
			if (!useLRE)
			{
				AssemblyName name = XmlILModule.CreateAssemblyName();
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, this.persistAsm ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Transparent, new object[0]));
				if (emitSymbols)
				{
					this.urlToSymWriter = new Hashtable();
					DebuggableAttribute.DebuggingModes debuggingModes = DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints;
					assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.Debuggable, new object[]
					{
						debuggingModes
					}));
				}
				ModuleBuilder moduleBuilder;
				if (this.persistAsm)
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", this.modFile + ".dll", emitSymbols);
				}
				else
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("System.Xml.Xsl.CompiledQuery", emitSymbols);
				}
				this.typeBldr = moduleBuilder.DefineType("System.Xml.Xsl.CompiledQuery.Query", TypeAttributes.Public);
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x001114AC File Offset: 0x0010F6AC
		public MethodInfo DefineMethod(string name, Type returnType, Type[] paramTypes, string[] paramNames, XmlILMethodAttributes xmlAttrs)
		{
			int num = 1;
			string str = name;
			bool flag = (xmlAttrs & XmlILMethodAttributes.Raw) > XmlILMethodAttributes.None;
			while (this.methods[name] != null)
			{
				num++;
				name = str + " (" + num.ToString() + ")";
			}
			if (!flag)
			{
				Type[] array = new Type[paramTypes.Length + 1];
				array[0] = typeof(XmlQueryRuntime);
				Array.Copy(paramTypes, 0, array, 1, paramTypes.Length);
				paramTypes = array;
			}
			MethodInfo methodInfo;
			if (!this.useLRE)
			{
				MethodBuilder methodBuilder = this.typeBldr.DefineMethod(name, MethodAttributes.Private | MethodAttributes.Static, returnType, paramTypes);
				if (this.emitSymbols && (xmlAttrs & XmlILMethodAttributes.NonUser) != XmlILMethodAttributes.None)
				{
					methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.StepThrough, new object[0]));
					methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(XmlILConstructors.NonUserCode, new object[0]));
				}
				if (!flag)
				{
					methodBuilder.DefineParameter(1, ParameterAttributes.None, "{urn:schemas-microsoft-com:xslt-debug}runtime");
				}
				for (int i = 0; i < paramNames.Length; i++)
				{
					if (paramNames[i] != null && paramNames[i].Length != 0)
					{
						methodBuilder.DefineParameter(i + (flag ? 1 : 2), ParameterAttributes.None, paramNames[i]);
					}
				}
				methodInfo = methodBuilder;
			}
			else
			{
				DynamicMethod dynamicMethod = new DynamicMethod(name, returnType, paramTypes, XmlILModule.LREModule);
				dynamicMethod.InitLocals = true;
				if (!flag)
				{
					dynamicMethod.DefineParameter(1, ParameterAttributes.None, "{urn:schemas-microsoft-com:xslt-debug}runtime");
				}
				for (int j = 0; j < paramNames.Length; j++)
				{
					if (paramNames[j] != null && paramNames[j].Length != 0)
					{
						dynamicMethod.DefineParameter(j + (flag ? 1 : 2), ParameterAttributes.None, paramNames[j]);
					}
				}
				methodInfo = dynamicMethod;
			}
			this.methods[name] = methodInfo;
			return methodInfo;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00111648 File Offset: 0x0010F848
		public static ILGenerator DefineMethodBody(MethodBase methInfo)
		{
			DynamicMethod dynamicMethod = methInfo as DynamicMethod;
			if (dynamicMethod != null)
			{
				return dynamicMethod.GetILGenerator();
			}
			MethodBuilder methodBuilder = methInfo as MethodBuilder;
			if (methodBuilder != null)
			{
				return methodBuilder.GetILGenerator();
			}
			return ((ConstructorBuilder)methInfo).GetILGenerator();
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0011168E File Offset: 0x0010F88E
		public MethodInfo FindMethod(string name)
		{
			return (MethodInfo)this.methods[name];
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x001116A1 File Offset: 0x0010F8A1
		public FieldInfo DefineInitializedData(string name, byte[] data)
		{
			return this.typeBldr.DefineInitializedData(name, data, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x001116B2 File Offset: 0x0010F8B2
		public FieldInfo DefineField(string fieldName, Type type)
		{
			return this.typeBldr.DefineField(fieldName, type, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x001116C3 File Offset: 0x0010F8C3
		public ConstructorInfo DefineTypeInitializer()
		{
			return this.typeBldr.DefineTypeInitializer();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x001116D0 File Offset: 0x0010F8D0
		public ISymbolDocumentWriter AddSourceDocument(string fileName)
		{
			ISymbolDocumentWriter symbolDocumentWriter = this.urlToSymWriter[fileName] as ISymbolDocumentWriter;
			if (symbolDocumentWriter == null)
			{
				symbolDocumentWriter = ((ModuleBuilder)this.typeBldr.Module).DefineDocument(fileName, XmlILModule.LanguageGuid, XmlILModule.VendorGuid, Guid.Empty);
				this.urlToSymWriter.Add(fileName, symbolDocumentWriter);
			}
			return symbolDocumentWriter;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00111728 File Offset: 0x0010F928
		public void BakeMethods()
		{
			if (!this.useLRE)
			{
				Type type = this.typeBldr.CreateType();
				if (this.persistAsm)
				{
					((AssemblyBuilder)this.typeBldr.Module.Assembly).Save(this.modFile + ".dll");
				}
				Hashtable hashtable = new Hashtable(this.methods.Count);
				foreach (object obj in this.methods.Keys)
				{
					string text = (string)obj;
					hashtable[text] = type.GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
				}
				this.methods = hashtable;
				this.typeBldr = null;
				this.urlToSymWriter = null;
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00111804 File Offset: 0x0010FA04
		public Delegate CreateDelegate(string name, Type typDelegate)
		{
			if (!this.useLRE)
			{
				return Delegate.CreateDelegate(typDelegate, (MethodInfo)this.methods[name]);
			}
			return ((DynamicMethod)this.methods[name]).CreateDelegate(typDelegate);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0011183D File Offset: 0x0010FA3D
		private static AssemblyName CreateAssemblyName()
		{
			Interlocked.Increment(ref XmlILModule.AssemblyId);
			return new AssemblyName
			{
				Name = "System.Xml.Xsl.CompiledQuery." + XmlILModule.AssemblyId.ToString()
			};
		}

		// Token: 0x04002506 RID: 9478
		public static readonly PermissionSet CreateModulePermissionSet;

		// Token: 0x04002507 RID: 9479
		private static long AssemblyId;

		// Token: 0x04002508 RID: 9480
		private static ModuleBuilder LREModule;

		// Token: 0x04002509 RID: 9481
		private TypeBuilder typeBldr;

		// Token: 0x0400250A RID: 9482
		private Hashtable methods;

		// Token: 0x0400250B RID: 9483
		private Hashtable urlToSymWriter;

		// Token: 0x0400250C RID: 9484
		private string modFile;

		// Token: 0x0400250D RID: 9485
		private bool persistAsm;

		// Token: 0x0400250E RID: 9486
		private bool useLRE;

		// Token: 0x0400250F RID: 9487
		private bool emitSymbols;

		// Token: 0x04002510 RID: 9488
		private static readonly Guid LanguageGuid = new Guid(1177373246U, 45655, 19182, 151, 205, 89, 24, 199, 83, 23, 88);

		// Token: 0x04002511 RID: 9489
		private static readonly Guid VendorGuid = new Guid(2571847108U, 59113, 4562, 144, 63, 0, 192, 79, 163, 2, 161);

		// Token: 0x04002512 RID: 9490
		private const string RuntimeName = "{urn:schemas-microsoft-com:xslt-debug}runtime";
	}
}
