using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200058C RID: 1420
	internal class DynamicPropertyCollection
	{
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000C87EC File Offset: 0x000C69EC
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000C87FC File Offset: 0x000C69FC
		public bool RegisterDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				if (this.FindProperty(prop.Name) != -1)
				{
					throw new InvalidOperationException("Another property by this name already exists");
				}
				ArrayList arrayList = new ArrayList(this._properties);
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = new DynamicPropertyCollection.DynamicPropertyReg();
				dynamicPropertyReg.Property = prop;
				IContributeDynamicSink contributeDynamicSink = prop as IContributeDynamicSink;
				if (contributeDynamicSink != null)
				{
					dynamicPropertyReg.Sink = contributeDynamicSink.GetDynamicSink();
				}
				arrayList.Add(dynamicPropertyReg);
				this._properties = arrayList;
				result = true;
			}
			return result;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000C8894 File Offset: 0x000C6A94
		public bool UnregisterDynamicProperty(string name)
		{
			bool result;
			lock (this)
			{
				int num = this.FindProperty(name);
				if (num == -1)
				{
					throw new RemotingException("A property with the name " + name + " was not found");
				}
				this._properties.RemoveAt(num);
				result = true;
			}
			return result;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000C88FC File Offset: 0x000C6AFC
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				using (IEnumerator enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
						if (dynamicPropertyReg.Sink != null)
						{
							dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
						}
					}
					return;
				}
			}
			foreach (object obj2 in properties)
			{
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
				if (dynamicPropertyReg2.Sink != null)
				{
					dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
				}
			}
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000C89C0 File Offset: 0x000C6BC0
		private int FindProperty(string name)
		{
			for (int i = 0; i < this._properties.Count; i++)
			{
				if (((DynamicPropertyCollection.DynamicPropertyReg)this._properties[i]).Property.Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000C8A09 File Offset: 0x000C6C09
		public DynamicPropertyCollection()
		{
		}

		// Token: 0x040025AE RID: 9646
		private ArrayList _properties = new ArrayList();

		// Token: 0x0200058D RID: 1421
		private class DynamicPropertyReg
		{
			// Token: 0x060037B4 RID: 14260 RVA: 0x0000259F File Offset: 0x0000079F
			public DynamicPropertyReg()
			{
			}

			// Token: 0x040025AF RID: 9647
			public IDynamicProperty Property;

			// Token: 0x040025B0 RID: 9648
			public IDynamicMessageSink Sink;
		}
	}
}
