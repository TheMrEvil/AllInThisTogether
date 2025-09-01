using System;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002A5 RID: 677
	internal sealed class AssemblyGen
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0003DD04 File Offset: 0x0003BF04
		private static AssemblyGen Assembly
		{
			get
			{
				if (AssemblyGen.s_assembly == null)
				{
					Interlocked.CompareExchange<AssemblyGen>(ref AssemblyGen.s_assembly, new AssemblyGen(), null);
				}
				return AssemblyGen.s_assembly;
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0003DD24 File Offset: 0x0003BF24
		private AssemblyGen()
		{
			AssemblyName assemblyName = new AssemblyName("Snippets");
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			this._myModule = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0003DD5C File Offset: 0x0003BF5C
		private TypeBuilder DefineType(string name, Type parent, TypeAttributes attr)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(parent, "parent");
			StringBuilder stringBuilder = new StringBuilder(name);
			int value = Interlocked.Increment(ref this._index);
			stringBuilder.Append("$");
			stringBuilder.Append(value);
			stringBuilder.Replace('+', '_').Replace('[', '_').Replace(']', '_').Replace('*', '_').Replace('&', '_').Replace(',', '_').Replace('\\', '_');
			name = stringBuilder.ToString();
			return this._myModule.DefineType(name, attr, parent);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0003DDFB File Offset: 0x0003BFFB
		internal static TypeBuilder DefineDelegateType(string name)
		{
			return AssemblyGen.Assembly.DefineType(name, typeof(MulticastDelegate), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AutoClass);
		}

		// Token: 0x04000A8C RID: 2700
		private static AssemblyGen s_assembly;

		// Token: 0x04000A8D RID: 2701
		private readonly ModuleBuilder _myModule;

		// Token: 0x04000A8E RID: 2702
		private int _index;
	}
}
