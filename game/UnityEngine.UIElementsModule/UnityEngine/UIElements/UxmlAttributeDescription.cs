using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D0 RID: 720
	public abstract class UxmlAttributeDescription
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x000644E1 File Offset: 0x000626E1
		protected UxmlAttributeDescription()
		{
			this.use = UxmlAttributeDescription.Use.Optional;
			this.restriction = null;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000644FB File Offset: 0x000626FB
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00064503 File Offset: 0x00062703
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x0006450C File Offset: 0x0006270C
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x00064524 File Offset: 0x00062724
		public IEnumerable<string> obsoleteNames
		{
			get
			{
				return this.m_ObsoleteNames;
			}
			set
			{
				this.m_ObsoleteNames = value.ToArray<string>();
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00064533 File Offset: 0x00062733
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x0006453B File Offset: 0x0006273B
		public string type
		{
			[CompilerGenerated]
			get
			{
				return this.<type>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<type>k__BackingField = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00064544 File Offset: 0x00062744
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x0006454C File Offset: 0x0006274C
		public string typeNamespace
		{
			[CompilerGenerated]
			get
			{
				return this.<typeNamespace>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<typeNamespace>k__BackingField = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001848 RID: 6216
		public abstract string defaultValueAsString { get; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00064555 File Offset: 0x00062755
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x0006455D File Offset: 0x0006275D
		public UxmlAttributeDescription.Use use
		{
			[CompilerGenerated]
			get
			{
				return this.<use>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<use>k__BackingField = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00064566 File Offset: 0x00062766
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0006456E File Offset: 0x0006276E
		public UxmlTypeRestriction restriction
		{
			[CompilerGenerated]
			get
			{
				return this.<restriction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<restriction>k__BackingField = value;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00064578 File Offset: 0x00062778
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value)
		{
			bool flag = this.name == null && (this.m_ObsoleteNames == null || this.m_ObsoleteNames.Length == 0);
			bool result;
			if (flag)
			{
				Debug.LogError("Attribute description has no name.");
				value = null;
				result = false;
			}
			else
			{
				string text;
				bag.TryGetAttributeValue("name", out text);
				bool flag2 = !string.IsNullOrEmpty(text) && cc.attributeOverrides != null;
				if (flag2)
				{
					for (int i = 0; i < cc.attributeOverrides.Count; i++)
					{
						bool flag3 = cc.attributeOverrides[i].m_ElementName != text;
						if (!flag3)
						{
							bool flag4 = cc.attributeOverrides[i].m_AttributeName != this.name;
							if (flag4)
							{
								bool flag5 = this.m_ObsoleteNames != null;
								if (!flag5)
								{
									goto IL_147;
								}
								bool flag6 = false;
								for (int j = 0; j < this.m_ObsoleteNames.Length; j++)
								{
									bool flag7 = cc.attributeOverrides[i].m_AttributeName == this.m_ObsoleteNames[j];
									if (flag7)
									{
										flag6 = true;
										break;
									}
								}
								bool flag8 = !flag6;
								if (flag8)
								{
									goto IL_147;
								}
							}
							value = cc.attributeOverrides[i].m_Value;
							return true;
						}
						IL_147:;
					}
				}
				bool flag9 = this.name == null;
				if (flag9)
				{
					for (int k = 0; k < this.m_ObsoleteNames.Length; k++)
					{
						bool flag10 = bag.TryGetAttributeValue(this.m_ObsoleteNames[k], out value);
						if (flag10)
						{
							bool flag11 = cc.visualTreeAsset != null;
							if (flag11)
							{
							}
							return true;
						}
					}
					value = null;
					result = false;
				}
				else
				{
					bool flag12 = !bag.TryGetAttributeValue(this.name, out value);
					if (flag12)
					{
						bool flag13 = this.m_ObsoleteNames != null;
						if (flag13)
						{
							for (int l = 0; l < this.m_ObsoleteNames.Length; l++)
							{
								bool flag14 = bag.TryGetAttributeValue(this.m_ObsoleteNames[l], out value);
								if (flag14)
								{
									bool flag15 = cc.visualTreeAsset != null;
									if (flag15)
									{
									}
									return true;
								}
							}
						}
						value = null;
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000647DC File Offset: 0x000629DC
		protected bool TryGetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue, ref T value)
		{
			string arg;
			bool flag = this.TryGetValueFromBagAsString(bag, cc, out arg);
			bool result;
			if (flag)
			{
				bool flag2 = converterFunc != null;
				if (flag2)
				{
					value = converterFunc(arg, defaultValue);
				}
				else
				{
					value = defaultValue;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0006482C File Offset: 0x00062A2C
		protected T GetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue)
		{
			bool flag = converterFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("converterFunc");
			}
			string arg;
			bool flag2 = this.TryGetValueFromBagAsString(bag, cc, out arg);
			T result;
			if (flag2)
			{
				result = converterFunc(arg, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04000A70 RID: 2672
		protected const string xmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x04000A71 RID: 2673
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <name>k__BackingField;

		// Token: 0x04000A72 RID: 2674
		private string[] m_ObsoleteNames;

		// Token: 0x04000A73 RID: 2675
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <type>k__BackingField;

		// Token: 0x04000A74 RID: 2676
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <typeNamespace>k__BackingField;

		// Token: 0x04000A75 RID: 2677
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UxmlAttributeDescription.Use <use>k__BackingField;

		// Token: 0x04000A76 RID: 2678
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UxmlTypeRestriction <restriction>k__BackingField;

		// Token: 0x020002D1 RID: 721
		public enum Use
		{
			// Token: 0x04000A78 RID: 2680
			None,
			// Token: 0x04000A79 RID: 2681
			Optional,
			// Token: 0x04000A7A RID: 2682
			Prohibited,
			// Token: 0x04000A7B RID: 2683
			Required
		}
	}
}
