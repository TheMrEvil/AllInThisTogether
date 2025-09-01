using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000233 RID: 563
	[Serializable]
	internal class DelegateSerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x0005FA08 File Offset: 0x0005DC08
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info, 0);
				return;
			}
			Delegate[] array = new Delegate[num];
			delegateEntry2 = delegateEntry;
			for (int i = 0; i < num; i++)
			{
				array[i] = delegateEntry2.DeserializeDelegate(info, i);
				delegateEntry2 = delegateEntry2.delegateEntry;
			}
			this._delegate = Delegate.Combine(array);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0005FA98 File Offset: 0x0005DC98
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = (@delegate.Target != null) ? ("target" + i.ToString()) : null;
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
				info.AddValue("method" + i.ToString(), @delegate.Method);
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000472C8 File Offset: 0x000454C8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0005FB4E File Offset: 0x0005DD4E
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x0400170C RID: 5900
		private Delegate _delegate;

		// Token: 0x02000234 RID: 564
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x060019C8 RID: 6600 RVA: 0x0005FB58 File Offset: 0x0005DD58
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x060019C9 RID: 6601 RVA: 0x0005FBDC File Offset: 0x0005DDDC
			public Delegate DeserializeDelegate(SerializationInfo info, int index)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				string name = "method" + index.ToString();
				MethodInfo methodInfo = (MethodInfo)info.GetValueNoThrow(name, typeof(MethodInfo));
				Type type = Assembly.Load(this.assembly).GetType(this.type);
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj) && !Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName).IsInstanceOfType(obj))
					{
						throw new RemotingException("Unexpected proxy type.");
					}
					if (!(methodInfo == null))
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					return Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					if (methodInfo != null)
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					Type type2 = Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName);
					return Delegate.CreateDelegate(type, type2, this.methodName);
				}
			}

			// Token: 0x0400170D RID: 5901
			private string type;

			// Token: 0x0400170E RID: 5902
			private string assembly;

			// Token: 0x0400170F RID: 5903
			private object target;

			// Token: 0x04001710 RID: 5904
			private string targetTypeAssembly;

			// Token: 0x04001711 RID: 5905
			private string targetTypeName;

			// Token: 0x04001712 RID: 5906
			private string methodName;

			// Token: 0x04001713 RID: 5907
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
