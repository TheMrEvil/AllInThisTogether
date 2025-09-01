using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002B8 RID: 696
	internal abstract class BaseInvokableCall
	{
		// Token: 0x06001D2E RID: 7470 RVA: 0x00008CBB File Offset: 0x00006EBB
		protected BaseInvokableCall()
		{
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool flag = function == null;
			if (flag)
			{
				throw new ArgumentNullException("function");
			}
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag2 = target != null;
				if (flag2)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag3 = target == null;
				if (flag3)
				{
					throw new ArgumentNullException("target");
				}
			}
		}

		// Token: 0x06001D30 RID: 7472
		public abstract void Invoke(object[] args);

		// Token: 0x06001D31 RID: 7473 RVA: 0x0002EC28 File Offset: 0x0002CE28
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0002EC78 File Offset: 0x0002CE78
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Object @object = target as Object;
				bool flag2 = @object != null;
				result = (!flag2 || @object != null);
			}
			return result;
		}

		// Token: 0x06001D33 RID: 7475
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
