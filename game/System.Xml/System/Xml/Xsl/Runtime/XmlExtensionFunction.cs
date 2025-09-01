using System;
using System.Globalization;
using System.Reflection;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200046C RID: 1132
	internal class XmlExtensionFunction
	{
		// Token: 0x06002BDB RID: 11227 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlExtensionFunction()
		{
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x00105715 File Offset: 0x00103915
		public XmlExtensionFunction(string name, string namespaceUri, MethodInfo meth)
		{
			this.name = name;
			this.namespaceUri = namespaceUri;
			this.Bind(meth);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x00105732 File Offset: 0x00103932
		public XmlExtensionFunction(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			this.Init(name, namespaceUri, numArgs, objectType, flags);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00105748 File Offset: 0x00103948
		public void Init(string name, string namespaceUri, int numArgs, Type objectType, BindingFlags flags)
		{
			this.name = name;
			this.namespaceUri = namespaceUri;
			this.numArgs = numArgs;
			this.objectType = objectType;
			this.flags = flags;
			this.meth = null;
			this.argClrTypes = null;
			this.retClrType = null;
			this.argXmlTypes = null;
			this.retXmlType = null;
			this.hashCode = (namespaceUri.GetHashCode() ^ name.GetHashCode() ^ (int)((int)flags << 16) ^ numArgs);
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x001057B8 File Offset: 0x001039B8
		public MethodInfo Method
		{
			get
			{
				return this.meth;
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x001057C0 File Offset: 0x001039C0
		public Type GetClrArgumentType(int index)
		{
			return this.argClrTypes[index];
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x001057CA File Offset: 0x001039CA
		public Type ClrReturnType
		{
			get
			{
				return this.retClrType;
			}
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x001057D2 File Offset: 0x001039D2
		public XmlQueryType GetXmlArgumentType(int index)
		{
			return this.argXmlTypes[index];
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x001057DC File Offset: 0x001039DC
		public XmlQueryType XmlReturnType
		{
			get
			{
				return this.retXmlType;
			}
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x001057E4 File Offset: 0x001039E4
		public bool CanBind()
		{
			MethodInfo[] methods = this.objectType.GetMethods(this.flags);
			StringComparison comparisonType = ((this.flags & BindingFlags.IgnoreCase) > BindingFlags.Default) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.Name.Equals(this.name, comparisonType) && (this.numArgs == -1 || methodInfo.GetParameters().Length == this.numArgs) && !methodInfo.IsGenericMethodDefinition)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x00105860 File Offset: 0x00103A60
		public void Bind()
		{
			MethodInfo[] methods = this.objectType.GetMethods(this.flags);
			MethodInfo methodInfo = null;
			StringComparison comparisonType = ((this.flags & BindingFlags.IgnoreCase) > BindingFlags.Default) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			foreach (MethodInfo methodInfo2 in methods)
			{
				if (methodInfo2.Name.Equals(this.name, comparisonType) && (this.numArgs == -1 || methodInfo2.GetParameters().Length == this.numArgs))
				{
					if (methodInfo != null)
					{
						throw new XslTransformException("Ambiguous method call. Extension object '{0}' contains multiple '{1}' methods that have {2} parameter(s).", new string[]
						{
							this.namespaceUri,
							this.name,
							this.numArgs.ToString(CultureInfo.InvariantCulture)
						});
					}
					methodInfo = methodInfo2;
				}
			}
			if (methodInfo == null)
			{
				foreach (MethodInfo methodInfo3 in this.objectType.GetMethods(this.flags | BindingFlags.NonPublic))
				{
					if (methodInfo3.Name.Equals(this.name, comparisonType) && methodInfo3.GetParameters().Length == this.numArgs)
					{
						throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it is not public.", new string[]
						{
							this.namespaceUri,
							this.name
						});
					}
				}
				throw new XslTransformException("Extension object '{0}' does not contain a matching '{1}' method that has {2} parameter(s).", new string[]
				{
					this.namespaceUri,
					this.name,
					this.numArgs.ToString(CultureInfo.InvariantCulture)
				});
			}
			if (methodInfo.IsGenericMethodDefinition)
			{
				throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it is generic.", new string[]
				{
					this.namespaceUri,
					this.name
				});
			}
			this.Bind(methodInfo);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x001059FC File Offset: 0x00103BFC
		private void Bind(MethodInfo meth)
		{
			ParameterInfo[] parameters = meth.GetParameters();
			this.meth = meth;
			this.argClrTypes = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.argClrTypes[i] = this.GetClrType(parameters[i].ParameterType);
			}
			this.retClrType = this.GetClrType(this.meth.ReturnType);
			this.argXmlTypes = new XmlQueryType[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.argXmlTypes[i] = this.InferXmlType(this.argClrTypes[i]);
				if (this.namespaceUri.Length == 0)
				{
					if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeNotRtf)
					{
						this.argXmlTypes[i] = XmlQueryTypeFactory.Node;
					}
					else if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeSDod)
					{
						this.argXmlTypes[i] = XmlQueryTypeFactory.NodeS;
					}
				}
				else if (this.argXmlTypes[i] == XmlQueryTypeFactory.NodeSDod)
				{
					this.argXmlTypes[i] = XmlQueryTypeFactory.NodeNotRtfS;
				}
			}
			this.retXmlType = this.InferXmlType(this.retClrType);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00105B0C File Offset: 0x00103D0C
		public object Invoke(object extObj, object[] args)
		{
			object result;
			try
			{
				result = this.meth.Invoke(extObj, this.flags, null, args, CultureInfo.InvariantCulture);
			}
			catch (TargetInvocationException ex)
			{
				throw new XslTransformException(ex.InnerException, "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.", new string[]
				{
					this.name
				});
			}
			catch (Exception ex2)
			{
				if (!XmlException.IsCatchableException(ex2))
				{
					throw;
				}
				throw new XslTransformException(ex2, "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.", new string[]
				{
					this.name
				});
			}
			return result;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00105B98 File Offset: 0x00103D98
		public override bool Equals(object other)
		{
			XmlExtensionFunction xmlExtensionFunction = other as XmlExtensionFunction;
			return this.hashCode == xmlExtensionFunction.hashCode && this.name == xmlExtensionFunction.name && this.namespaceUri == xmlExtensionFunction.namespaceUri && this.numArgs == xmlExtensionFunction.numArgs && this.objectType == xmlExtensionFunction.objectType && this.flags == xmlExtensionFunction.flags;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00105C11 File Offset: 0x00103E11
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x00105C19 File Offset: 0x00103E19
		private Type GetClrType(Type clrType)
		{
			if (clrType.IsEnum)
			{
				return Enum.GetUnderlyingType(clrType);
			}
			if (clrType.IsByRef)
			{
				throw new XslTransformException("Method '{1}' of extension object '{0}' cannot be called because it has one or more ByRef parameters.", new string[]
				{
					this.namespaceUri,
					this.name
				});
			}
			return clrType;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00105C56 File Offset: 0x00103E56
		private XmlQueryType InferXmlType(Type clrType)
		{
			return XsltConvert.InferXsltType(clrType);
		}

		// Token: 0x040022BD RID: 8893
		private string namespaceUri;

		// Token: 0x040022BE RID: 8894
		private string name;

		// Token: 0x040022BF RID: 8895
		private int numArgs;

		// Token: 0x040022C0 RID: 8896
		private Type objectType;

		// Token: 0x040022C1 RID: 8897
		private BindingFlags flags;

		// Token: 0x040022C2 RID: 8898
		private int hashCode;

		// Token: 0x040022C3 RID: 8899
		private MethodInfo meth;

		// Token: 0x040022C4 RID: 8900
		private Type[] argClrTypes;

		// Token: 0x040022C5 RID: 8901
		private Type retClrType;

		// Token: 0x040022C6 RID: 8902
		private XmlQueryType[] argXmlTypes;

		// Token: 0x040022C7 RID: 8903
		private XmlQueryType retXmlType;
	}
}
