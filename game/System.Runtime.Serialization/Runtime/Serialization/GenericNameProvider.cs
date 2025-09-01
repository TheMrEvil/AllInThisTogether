using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000BE RID: 190
	internal class GenericNameProvider : IGenericNameProvider
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0002FE74 File Offset: 0x0002E074
		internal GenericNameProvider(Type type)
		{
			string clrTypeFullName = DataContract.GetClrTypeFullName(type.GetGenericTypeDefinition());
			object[] genericArguments = type.GetGenericArguments();
			this..ctor(clrTypeFullName, genericArguments);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002FE9C File Offset: 0x0002E09C
		internal GenericNameProvider(string genericTypeName, object[] genericParams)
		{
			this.genericTypeName = genericTypeName;
			this.genericParams = new object[genericParams.Length];
			genericParams.CopyTo(this.genericParams, 0);
			string typeName;
			string text;
			DataContract.GetClrNameAndNamespace(genericTypeName, out typeName, out text);
			this.nestedParamCounts = DataContract.GetDataContractNameForGenericName(typeName, null);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		public int GetParameterCount()
		{
			return this.genericParams.Length;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002FEF2 File Offset: 0x0002E0F2
		public IList<int> GetNestedParameterCounts()
		{
			return this.nestedParamCounts;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002FEFA File Offset: 0x0002E0FA
		public string GetParameterName(int paramIndex)
		{
			return this.GetStableName(paramIndex).Name;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002FF08 File Offset: 0x0002E108
		public string GetNamespaces()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.GetParameterCount(); i++)
			{
				stringBuilder.Append(" ").Append(this.GetStableName(i).Namespace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002FF4F File Offset: 0x0002E14F
		public string GetGenericTypeName()
		{
			return this.genericTypeName;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002FF58 File Offset: 0x0002E158
		public bool ParametersFromBuiltInNamespaces
		{
			get
			{
				bool flag = true;
				int num = 0;
				while (num < this.GetParameterCount() && flag)
				{
					flag = DataContract.IsBuiltInNamespace(this.GetStableName(num).Namespace);
					num++;
				}
				return flag;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002FF90 File Offset: 0x0002E190
		private XmlQualifiedName GetStableName(int i)
		{
			object obj = this.genericParams[i];
			XmlQualifiedName xmlQualifiedName = obj as XmlQualifiedName;
			if (xmlQualifiedName == null)
			{
				Type type = obj as Type;
				if (type != null)
				{
					xmlQualifiedName = (this.genericParams[i] = DataContract.GetStableName(type));
				}
				else
				{
					xmlQualifiedName = (this.genericParams[i] = ((DataContract)obj).StableName);
				}
			}
			return xmlQualifiedName;
		}

		// Token: 0x04000484 RID: 1156
		private string genericTypeName;

		// Token: 0x04000485 RID: 1157
		private object[] genericParams;

		// Token: 0x04000486 RID: 1158
		private IList<int> nestedParamCounts;
	}
}
