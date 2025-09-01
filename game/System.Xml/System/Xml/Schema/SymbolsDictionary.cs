using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004F5 RID: 1269
	internal class SymbolsDictionary
	{
		// Token: 0x060033F7 RID: 13303 RVA: 0x001271EE File Offset: 0x001253EE
		public SymbolsDictionary()
		{
			this.names = new Hashtable();
			this.particles = new ArrayList();
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060033F8 RID: 13304 RVA: 0x00127213 File Offset: 0x00125413
		public int Count
		{
			get
			{
				return this.last + 1;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x0012721D File Offset: 0x0012541D
		public int CountOfNames
		{
			get
			{
				return this.names.Count;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x0012722A File Offset: 0x0012542A
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x00127232 File Offset: 0x00125432
		public bool IsUpaEnforced
		{
			get
			{
				return this.isUpaEnforced;
			}
			set
			{
				this.isUpaEnforced = value;
			}
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x0012723C File Offset: 0x0012543C
		public int AddName(XmlQualifiedName name, object particle)
		{
			object obj = this.names[name];
			if (obj != null)
			{
				int num = (int)obj;
				if (this.particles[num] != particle)
				{
					this.isUpaEnforced = false;
				}
				return num;
			}
			this.names.Add(name, this.last);
			this.particles.Add(particle);
			int num2 = this.last;
			this.last = num2 + 1;
			return num2;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x001272B0 File Offset: 0x001254B0
		public void AddNamespaceList(NamespaceList list, object particle, bool allowLocal)
		{
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				this.particleLast = particle;
				return;
			case NamespaceList.ListType.Other:
				this.AddWildcard(list.Excluded, null);
				if (!allowLocal)
				{
					this.AddWildcard(string.Empty, null);
					return;
				}
				break;
			case NamespaceList.ListType.Set:
				foreach (object obj in list.Enumerate)
				{
					string wildcard = (string)obj;
					this.AddWildcard(wildcard, particle);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x0012734C File Offset: 0x0012554C
		private void AddWildcard(string wildcard, object particle)
		{
			if (this.wildcards == null)
			{
				this.wildcards = new Hashtable();
			}
			object obj = this.wildcards[wildcard];
			if (obj == null)
			{
				this.wildcards.Add(wildcard, this.last);
				this.particles.Add(particle);
				this.last++;
				return;
			}
			if (particle != null)
			{
				this.particles[(int)obj] = particle;
			}
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x001273C4 File Offset: 0x001255C4
		public ICollection GetNamespaceListSymbols(NamespaceList list)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.names.Keys)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (xmlQualifiedName != XmlQualifiedName.Empty && list.Allows(xmlQualifiedName))
				{
					arrayList.Add(this.names[xmlQualifiedName]);
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards.Keys)
				{
					string text = (string)obj2;
					if (list.Allows(text))
					{
						arrayList.Add(this.wildcards[text]);
					}
				}
			}
			if (list.Type == NamespaceList.ListType.Any || list.Type == NamespaceList.ListType.Other)
			{
				arrayList.Add(this.last);
			}
			return arrayList;
		}

		// Token: 0x1700093A RID: 2362
		public int this[XmlQualifiedName name]
		{
			get
			{
				object obj = this.names[name];
				if (obj != null)
				{
					return (int)obj;
				}
				if (this.wildcards != null)
				{
					obj = this.wildcards[name.Namespace];
					if (obj != null)
					{
						return (int)obj;
					}
				}
				return this.last;
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x0012752A File Offset: 0x0012572A
		public bool Exists(XmlQualifiedName name)
		{
			return this.names[name] != null;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x0012753D File Offset: 0x0012573D
		public object GetParticle(int symbol)
		{
			if (symbol != this.last)
			{
				return this.particles[symbol];
			}
			return this.particleLast;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x0012755C File Offset: 0x0012575C
		public string NameOf(int symbol)
		{
			foreach (object obj in this.names)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if ((int)dictionaryEntry.Value == symbol)
				{
					return ((XmlQualifiedName)dictionaryEntry.Key).ToString();
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if ((int)dictionaryEntry2.Value == symbol)
					{
						return (string)dictionaryEntry2.Key + ":*";
					}
				}
			}
			return "##other:*";
		}

		// Token: 0x040026CB RID: 9931
		private int last;

		// Token: 0x040026CC RID: 9932
		private Hashtable names;

		// Token: 0x040026CD RID: 9933
		private Hashtable wildcards;

		// Token: 0x040026CE RID: 9934
		private ArrayList particles;

		// Token: 0x040026CF RID: 9935
		private object particleLast;

		// Token: 0x040026D0 RID: 9936
		private bool isUpaEnforced = true;
	}
}
