using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.XPath;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200049E RID: 1182
	internal class IteratorDescriptor
	{
		// Token: 0x06002E46 RID: 11846 RVA: 0x0010F474 File Offset: 0x0010D674
		public IteratorDescriptor(GenerateHelper helper)
		{
			this.Init(null, helper);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x0010F484 File Offset: 0x0010D684
		public IteratorDescriptor(IteratorDescriptor iterParent)
		{
			this.Init(iterParent, iterParent.helper);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x0010F499 File Offset: 0x0010D699
		private void Init(IteratorDescriptor iterParent, GenerateHelper helper)
		{
			this.helper = helper;
			this.iterParent = iterParent;
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0010F4A9 File Offset: 0x0010D6A9
		public IteratorDescriptor ParentIterator
		{
			get
			{
				return this.iterParent;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x0010F4B1 File Offset: 0x0010D6B1
		public bool HasLabelNext
		{
			get
			{
				return this.hasNext;
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x0010F4B9 File Offset: 0x0010D6B9
		public Label GetLabelNext()
		{
			return this.lblNext;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0010F4C1 File Offset: 0x0010D6C1
		public void SetIterator(Label lblNext, StorageDescriptor storage)
		{
			this.lblNext = lblNext;
			this.hasNext = true;
			this.storage = storage;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x0010F4D8 File Offset: 0x0010D6D8
		public void SetIterator(IteratorDescriptor iterInfo)
		{
			if (iterInfo.HasLabelNext)
			{
				this.lblNext = iterInfo.GetLabelNext();
				this.hasNext = true;
			}
			this.storage = iterInfo.Storage;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x0010F501 File Offset: 0x0010D701
		public void LoopToEnd(Label lblOnEnd)
		{
			if (this.hasNext)
			{
				this.helper.BranchAndMark(this.lblNext, lblOnEnd);
				this.hasNext = false;
			}
			this.storage = StorageDescriptor.None();
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x0010F52F File Offset: 0x0010D72F
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x0010F537 File Offset: 0x0010D737
		public LocalBuilder LocalPosition
		{
			get
			{
				return this.locPos;
			}
			set
			{
				this.locPos = value;
			}
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x0010F540 File Offset: 0x0010D740
		public void CacheCount()
		{
			this.PushValue();
			this.helper.CallCacheCount(this.storage.ItemStorageType);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x0010F560 File Offset: 0x0010D760
		public void EnsureNoCache()
		{
			if (this.storage.IsCached)
			{
				if (!this.HasLabelNext)
				{
					this.EnsureStack();
					this.helper.LoadInteger(0);
					this.helper.CallCacheItem(this.storage.ItemStorageType);
					this.storage = StorageDescriptor.Stack(this.storage.ItemStorageType, false);
					return;
				}
				LocalBuilder locBldr = this.helper.DeclareLocal("$$$idx", typeof(int));
				this.EnsureNoStack("$$$cache");
				this.helper.LoadInteger(-1);
				this.helper.Emit(OpCodes.Stloc, locBldr);
				Label lbl = this.helper.DefineLabel();
				this.helper.MarkLabel(lbl);
				this.helper.Emit(OpCodes.Ldloc, locBldr);
				this.helper.LoadInteger(1);
				this.helper.Emit(OpCodes.Add);
				this.helper.Emit(OpCodes.Stloc, locBldr);
				this.helper.Emit(OpCodes.Ldloc, locBldr);
				this.CacheCount();
				this.helper.Emit(OpCodes.Bge, this.GetLabelNext());
				this.PushValue();
				this.helper.Emit(OpCodes.Ldloc, locBldr);
				this.helper.CallCacheItem(this.storage.ItemStorageType);
				this.SetIterator(lbl, StorageDescriptor.Stack(this.storage.ItemStorageType, false));
			}
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x0010F6D0 File Offset: 0x0010D8D0
		public void SetBranching(BranchingContext brctxt, Label lblBranch)
		{
			this.brctxt = brctxt;
			this.lblBranch = lblBranch;
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x0010F6E0 File Offset: 0x0010D8E0
		public bool IsBranching
		{
			get
			{
				return this.brctxt > BranchingContext.None;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x0010F6EB File Offset: 0x0010D8EB
		public Label LabelBranch
		{
			get
			{
				return this.lblBranch;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x0010F6F3 File Offset: 0x0010D8F3
		public BranchingContext CurrentBranchingContext
		{
			get
			{
				return this.brctxt;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x0010F6FB File Offset: 0x0010D8FB
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x0010F703 File Offset: 0x0010D903
		public StorageDescriptor Storage
		{
			get
			{
				return this.storage;
			}
			set
			{
				this.storage = value;
			}
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x0010F70C File Offset: 0x0010D90C
		public void PushValue()
		{
			switch (this.storage.Location)
			{
			case ItemLocation.Stack:
				this.helper.Emit(OpCodes.Dup);
				return;
			case ItemLocation.Parameter:
				this.helper.LoadParameter(this.storage.ParameterLocation);
				return;
			case ItemLocation.Local:
				this.helper.Emit(OpCodes.Ldloc, this.storage.LocalLocation);
				return;
			case ItemLocation.Current:
				this.helper.Emit(OpCodes.Ldloca, this.storage.CurrentLocation);
				this.helper.Call(this.storage.CurrentLocation.LocalType.GetMethod("get_Current"));
				return;
			default:
				return;
			}
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x0010F7C4 File Offset: 0x0010D9C4
		public void EnsureStack()
		{
			switch (this.storage.Location)
			{
			case ItemLocation.Stack:
				return;
			case ItemLocation.Parameter:
			case ItemLocation.Local:
			case ItemLocation.Current:
				this.PushValue();
				break;
			case ItemLocation.Global:
				this.helper.LoadQueryRuntime();
				this.helper.Call(this.storage.GlobalLocation);
				break;
			}
			this.storage = this.storage.ToStack();
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x0010F836 File Offset: 0x0010DA36
		public void EnsureNoStack(string locName)
		{
			if (this.storage.Location == ItemLocation.Stack)
			{
				this.EnsureLocal(locName);
			}
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0010F850 File Offset: 0x0010DA50
		public void EnsureLocal(string locName)
		{
			if (this.storage.Location != ItemLocation.Local)
			{
				if (this.storage.IsCached)
				{
					this.EnsureLocal(this.helper.DeclareLocal(locName, typeof(IList<>).MakeGenericType(new Type[]
					{
						this.storage.ItemStorageType
					})));
					return;
				}
				this.EnsureLocal(this.helper.DeclareLocal(locName, this.storage.ItemStorageType));
			}
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x0010F8CB File Offset: 0x0010DACB
		public void EnsureLocal(LocalBuilder bldr)
		{
			if (this.storage.LocalLocation != bldr)
			{
				this.EnsureStack();
				this.helper.Emit(OpCodes.Stloc, bldr);
				this.storage = this.storage.ToLocal(bldr);
			}
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0010F904 File Offset: 0x0010DB04
		public void DiscardStack()
		{
			if (this.storage.Location == ItemLocation.Stack)
			{
				this.helper.Emit(OpCodes.Pop);
				this.storage = StorageDescriptor.None();
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x0010F92F File Offset: 0x0010DB2F
		public void EnsureStackNoCache()
		{
			this.EnsureNoCache();
			this.EnsureStack();
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0010F93D File Offset: 0x0010DB3D
		public void EnsureNoStackNoCache(string locName)
		{
			this.EnsureNoCache();
			this.EnsureNoStack(locName);
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x0010F94C File Offset: 0x0010DB4C
		public void EnsureLocalNoCache(string locName)
		{
			this.EnsureNoCache();
			this.EnsureLocal(locName);
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x0010F95B File Offset: 0x0010DB5B
		public void EnsureLocalNoCache(LocalBuilder bldr)
		{
			this.EnsureNoCache();
			this.EnsureLocal(bldr);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x0010F96C File Offset: 0x0010DB6C
		public void EnsureItemStorageType(XmlQueryType xmlType, Type storageTypeDest)
		{
			if (!(this.storage.ItemStorageType == storageTypeDest))
			{
				if (this.storage.IsCached)
				{
					if (this.storage.ItemStorageType == typeof(XPathNavigator))
					{
						this.EnsureStack();
						this.helper.Call(XmlILMethods.NavsToItems);
						goto IL_14D;
					}
					if (storageTypeDest == typeof(XPathNavigator))
					{
						this.EnsureStack();
						this.helper.Call(XmlILMethods.ItemsToNavs);
						goto IL_14D;
					}
				}
				this.EnsureStackNoCache();
				if (this.storage.ItemStorageType == typeof(XPathItem))
				{
					if (storageTypeDest == typeof(XPathNavigator))
					{
						this.helper.Emit(OpCodes.Castclass, typeof(XPathNavigator));
					}
					else
					{
						this.helper.CallValueAs(storageTypeDest);
					}
				}
				else if (!(this.storage.ItemStorageType == typeof(XPathNavigator)))
				{
					this.helper.LoadInteger(this.helper.StaticData.DeclareXmlType(xmlType));
					this.helper.LoadQueryRuntime();
					this.helper.Call(XmlILMethods.StorageMethods[this.storage.ItemStorageType].ToAtomicValue);
				}
			}
			IL_14D:
			this.storage = this.storage.ToStorageType(storageTypeDest);
		}

		// Token: 0x0400249F RID: 9375
		private GenerateHelper helper;

		// Token: 0x040024A0 RID: 9376
		private IteratorDescriptor iterParent;

		// Token: 0x040024A1 RID: 9377
		private Label lblNext;

		// Token: 0x040024A2 RID: 9378
		private bool hasNext;

		// Token: 0x040024A3 RID: 9379
		private LocalBuilder locPos;

		// Token: 0x040024A4 RID: 9380
		private BranchingContext brctxt;

		// Token: 0x040024A5 RID: 9381
		private Label lblBranch;

		// Token: 0x040024A6 RID: 9382
		private StorageDescriptor storage;
	}
}
