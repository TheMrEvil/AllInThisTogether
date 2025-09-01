using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Represents a function exported by a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</summary>
	// Token: 0x02000095 RID: 149
	public class ExportedDelegate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportedDelegate" /> class.</summary>
		// Token: 0x060003F1 RID: 1009 RVA: 0x00002BAC File Offset: 0x00000DAC
		protected ExportedDelegate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportedDelegate" /> class for the specified part and method.</summary>
		/// <param name="instance">The part exporting the method.</param>
		/// <param name="method">The method to be exported.</param>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B3F7 File Offset: 0x000095F7
		public ExportedDelegate(object instance, MethodInfo method)
		{
			Requires.NotNull<MethodInfo>(method, "method");
			this._instance = instance;
			this._method = method;
		}

		/// <summary>Gets a delegate of the specified type.</summary>
		/// <param name="delegateType">The type of the delegate to return.</param>
		/// <returns>A delegate of the specified type, or <see langword="null" /> if no such delegate can be created.</returns>
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B418 File Offset: 0x00009618
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			Requires.NotNull<Type>(delegateType, "delegateType");
			if (delegateType == typeof(Delegate) || delegateType == typeof(MulticastDelegate))
			{
				delegateType = this.CreateStandardDelegateType();
			}
			return Delegate.CreateDelegate(delegateType, this._instance, this._method, false);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B470 File Offset: 0x00009670
		private Type CreateStandardDelegateType()
		{
			ParameterInfo[] parameters = this._method.GetParameters();
			Type[] array = new Type[parameters.Length + 1];
			array[parameters.Length] = this._method.ReturnType;
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return Expression.GetDelegateType(array);
		}

		// Token: 0x04000187 RID: 391
		private object _instance;

		// Token: 0x04000188 RID: 392
		private MethodInfo _method;
	}
}
