using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200060D RID: 1549
	[Serializable]
	internal class CADMethodRef
	{
		// Token: 0x06003A95 RID: 14997 RVA: 0x000CD1D8 File Offset: 0x000CB3D8
		private Type[] GetTypes(string[] typeArray)
		{
			Type[] array = new Type[typeArray.Length];
			for (int i = 0; i < typeArray.Length; i++)
			{
				array[i] = Type.GetType(typeArray[i], true);
			}
			return array;
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000CD20C File Offset: 0x000CB40C
		public MethodBase Resolve()
		{
			Type type = Type.GetType(this.typeName, true);
			Type[] types = this.GetTypes(this.param_names);
			MethodBase methodBase;
			if (this.ctor)
			{
				methodBase = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			}
			else
			{
				methodBase = type.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			}
			if (methodBase != null && this.generic_arg_names != null && !methodBase.IsGenericMethodDefinition)
			{
				methodBase = null;
			}
			if (methodBase != null && this.generic_arg_names != null)
			{
				methodBase = ((MethodInfo)methodBase).MakeGenericMethod(this.GetTypes(this.generic_arg_names));
			}
			if (methodBase == null && this.generic_arg_names != null)
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					if (!(methodInfo.Name != this.methodName) && methodInfo.IsGenericMethodDefinition && methodInfo.GetGenericArguments().Length == this.generic_arg_names.Length)
					{
						methodBase = methodInfo.MakeGenericMethod(this.GetTypes(this.generic_arg_names));
						ParameterInfo[] parameters = methodBase.GetParameters();
						if (this.param_names.Length == parameters.Length)
						{
							for (int j = 0; j < parameters.Length; j++)
							{
								if (parameters[j].ParameterType.AssemblyQualifiedName != this.param_names[j])
								{
									methodBase = null;
									break;
								}
							}
							if (methodBase != null)
							{
								break;
							}
						}
					}
				}
			}
			if (methodBase == null)
			{
				throw new RemotingException(string.Concat(new string[]
				{
					"Method '",
					this.methodName,
					"' not found in type '",
					this.typeName,
					"'"
				}));
			}
			return methodBase;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		public CADMethodRef(IMethodMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			this.typeName = methodBase.DeclaringType.AssemblyQualifiedName;
			this.ctor = methodBase.IsConstructor;
			this.methodName = methodBase.Name;
			ParameterInfo[] parameters = methodBase.GetParameters();
			this.param_names = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.param_names[i] = parameters[i].ParameterType.AssemblyQualifiedName;
			}
			if (!this.ctor && methodBase.IsGenericMethod)
			{
				Type[] genericArguments = methodBase.GetGenericArguments();
				this.generic_arg_names = new string[genericArguments.Length];
				for (int j = 0; j < genericArguments.Length; j++)
				{
					this.generic_arg_names[j] = genericArguments[j].AssemblyQualifiedName;
				}
			}
		}

		// Token: 0x04002673 RID: 9843
		private bool ctor;

		// Token: 0x04002674 RID: 9844
		private string typeName;

		// Token: 0x04002675 RID: 9845
		private string methodName;

		// Token: 0x04002676 RID: 9846
		private string[] param_names;

		// Token: 0x04002677 RID: 9847
		private string[] generic_arg_names;
	}
}
