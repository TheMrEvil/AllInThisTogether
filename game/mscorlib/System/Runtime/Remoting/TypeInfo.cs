using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000577 RID: 1399
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x060036F4 RID: 14068 RVA: 0x000C6630 File Offset: 0x000C4830
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length + 1];
				for (int i = 0; i < interfaces.Length; i++)
				{
					this.interfacesImplemented[i] = interfaces[i].AssemblyQualifiedName;
				}
				this.interfacesImplemented[interfaces.Length] = type.AssemblyQualifiedName;
				return;
			}
			this.serverType = type.AssemblyQualifiedName;
			int num = 0;
			Type baseType = type.BaseType;
			while (baseType != typeof(MarshalByRefObject) && baseType != null)
			{
				baseType = baseType.BaseType;
				num++;
			}
			this.serverHierarchy = new string[num];
			baseType = type.BaseType;
			for (int j = 0; j < num; j++)
			{
				this.serverHierarchy[j] = baseType.AssemblyQualifiedName;
				baseType = baseType.BaseType;
			}
			Type[] interfaces2 = type.GetInterfaces();
			this.interfacesImplemented = new string[interfaces2.Length];
			for (int k = 0; k < interfaces2.Length; k++)
			{
				this.interfacesImplemented[k] = interfaces2[k].AssemblyQualifiedName;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000C6765 File Offset: 0x000C4965
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000C676D File Offset: 0x000C496D
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000C6778 File Offset: 0x000C4978
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				string[] array = this.serverHierarchy;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				string[] array = this.interfacesImplemented;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400256A RID: 9578
		private string serverType;

		// Token: 0x0400256B RID: 9579
		private string[] serverHierarchy;

		// Token: 0x0400256C RID: 9580
		private string[] interfacesImplemented;
	}
}
