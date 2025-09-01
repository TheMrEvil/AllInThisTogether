using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Data;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006F RID: 111
	public class ParseRelationOperation : IParseFieldOperation
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000116E1 File Offset: 0x0000F8E1
		private IList<string> Additions
		{
			[CompilerGenerated]
			get
			{
				return this.<Additions>k__BackingField;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000116E9 File Offset: 0x0000F8E9
		private IList<string> Removals
		{
			[CompilerGenerated]
			get
			{
				return this.<Removals>k__BackingField;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000116F1 File Offset: 0x0000F8F1
		private IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000116F9 File Offset: 0x0000F8F9
		private ParseRelationOperation(IParseObjectClassController classController)
		{
			this.ClassController = classController;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00011708 File Offset: 0x0000F908
		private ParseRelationOperation(IParseObjectClassController classController, IEnumerable<string> adds, IEnumerable<string> removes, string targetClassName) : this(classController)
		{
			this.TargetClassName = targetClassName;
			this.Additions = new ReadOnlyCollection<string>(adds.ToList<string>());
			this.Removals = new ReadOnlyCollection<string>(removes.ToList<string>());
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001173C File Offset: 0x0000F93C
		public ParseRelationOperation(IParseObjectClassController classController, IEnumerable<ParseObject> adds, IEnumerable<ParseObject> removes) : this(classController)
		{
			if (adds == null)
			{
				adds = new ParseObject[0];
			}
			if (removes == null)
			{
				removes = new ParseObject[0];
			}
			this.TargetClassName = (from entity in adds.Concat(removes)
			select entity.ClassName).FirstOrDefault<string>();
			this.Additions = new ReadOnlyCollection<string>(this.GetIdsFromObjects(adds).ToList<string>());
			this.Removals = new ReadOnlyCollection<string>(this.GetIdsFromObjects(removes).ToList<string>());
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000117CC File Offset: 0x0000F9CC
		public object Encode(IServiceHub serviceHub)
		{
			List<object> list = (from id in this.Additions
			select PointerOrLocalIdEncoder.Instance.Encode(this.ClassController.CreateObjectWithoutData(this.TargetClassName, id, serviceHub), serviceHub)).ToList<object>();
			List<object> list2 = (from id in this.Removals
			select PointerOrLocalIdEncoder.Instance.Encode(this.ClassController.CreateObjectWithoutData(this.TargetClassName, id, serviceHub), serviceHub)).ToList<object>();
			Dictionary<string, object> dictionary2;
			if (list.Count != 0)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["__op"] = "AddRelation";
				dictionary2 = dictionary;
				dictionary["objects"] = list;
			}
			else
			{
				dictionary2 = null;
			}
			Dictionary<string, object> dictionary3 = dictionary2;
			Dictionary<string, object> dictionary5;
			if (list2.Count != 0)
			{
				Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
				dictionary4["__op"] = "RemoveRelation";
				dictionary5 = dictionary4;
				dictionary4["objects"] = list2;
			}
			else
			{
				dictionary5 = null;
			}
			Dictionary<string, object> dictionary6 = dictionary5;
			if (dictionary3 != null && dictionary6 != null)
			{
				Dictionary<string, object> dictionary7 = new Dictionary<string, object>();
				dictionary7["__op"] = "Batch";
				dictionary7["ops"] = new Dictionary<string, object>[]
				{
					dictionary3,
					dictionary6
				};
				return dictionary7;
			}
			return dictionary3 ?? dictionary6;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			ParseRelationOperation result;
			if (previous != null)
			{
				if (previous is ParseDeleteOperation)
				{
					throw new InvalidOperationException("You can't modify a relation after deleting it.");
				}
				ParseRelationOperation parseRelationOperation = previous as ParseRelationOperation;
				if (parseRelationOperation == null)
				{
					throw new InvalidOperationException("Operation is invalid after previous operation.");
				}
				if (parseRelationOperation.TargetClassName != this.TargetClassName)
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Related object must be of class ",
						parseRelationOperation.TargetClassName,
						", but ",
						this.TargetClassName,
						" was passed in."
					}));
				}
				IParseObjectClassController classController = parseRelationOperation.ClassController;
				ParseRelationOperation parseRelationOperation2 = parseRelationOperation;
				result = new ParseRelationOperation(classController, this.Additions.Union(parseRelationOperation2.Additions.Except(this.Removals)).ToList<string>(), this.Removals.Union(parseRelationOperation2.Removals.Except(this.Additions)).ToList<string>(), this.TargetClassName);
			}
			else
			{
				result = this;
			}
			return result;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000119B8 File Offset: 0x0000FBB8
		public object Apply(object oldValue, string key)
		{
			ParseRelationBase result;
			if (this.Additions.Count == 0 && this.Removals.Count == 0)
			{
				result = null;
			}
			else if (oldValue != null)
			{
				ParseRelationBase parseRelationBase = oldValue as ParseRelationBase;
				if (parseRelationBase == null)
				{
					throw new InvalidOperationException("Operation is invalid after previous operation.");
				}
				string targetClassName = parseRelationBase.TargetClassName;
				if (targetClassName != null)
				{
					if (targetClassName != this.TargetClassName)
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"Related object must be a ",
							targetClassName,
							", but a ",
							this.TargetClassName,
							" was passed in."
						}));
					}
				}
				result = new ValueTuple<ParseRelationBase, string>(parseRelationBase, parseRelationBase.TargetClassName = this.TargetClassName).Item1;
			}
			else
			{
				result = this.ClassController.CreateRelation(null, key, this.TargetClassName);
			}
			return result;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00011A87 File Offset: 0x0000FC87
		public string TargetClassName
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetClassName>k__BackingField;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00011A90 File Offset: 0x0000FC90
		private IEnumerable<string> GetIdsFromObjects(IEnumerable<ParseObject> objects)
		{
			foreach (ParseObject parseObject in objects)
			{
				if (parseObject.ObjectId == null)
				{
					throw new ArgumentException("You can't add an unsaved ParseObject to a relation.");
				}
				if (parseObject.ClassName != this.TargetClassName)
				{
					throw new ArgumentException("Tried to create a ParseRelation with 2 different types: " + this.TargetClassName + " and " + parseObject.ClassName);
				}
			}
			return (from entity in objects
			select entity.ObjectId).Distinct<string>();
		}

		// Token: 0x040000F9 RID: 249
		[CompilerGenerated]
		private readonly IList<string> <Additions>k__BackingField;

		// Token: 0x040000FA RID: 250
		[CompilerGenerated]
		private readonly IList<string> <Removals>k__BackingField;

		// Token: 0x040000FB RID: 251
		[CompilerGenerated]
		private readonly IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x040000FC RID: 252
		[CompilerGenerated]
		private readonly string <TargetClassName>k__BackingField;

		// Token: 0x0200013D RID: 317
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000805 RID: 2053 RVA: 0x00017D09 File Offset: 0x00015F09
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x00017D15 File Offset: 0x00015F15
			public <>c()
			{
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x00017D1D File Offset: 0x00015F1D
			internal string <.ctor>b__11_0(ParseObject entity)
			{
				return entity.ClassName;
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x00017D25 File Offset: 0x00015F25
			internal string <GetIdsFromObjects>b__18_0(ParseObject entity)
			{
				return entity.ObjectId;
			}

			// Token: 0x040002E7 RID: 743
			public static readonly ParseRelationOperation.<>c <>9 = new ParseRelationOperation.<>c();

			// Token: 0x040002E8 RID: 744
			public static Func<ParseObject, string> <>9__11_0;

			// Token: 0x040002E9 RID: 745
			public static Func<ParseObject, string> <>9__18_0;
		}

		// Token: 0x0200013E RID: 318
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06000809 RID: 2057 RVA: 0x00017D2D File Offset: 0x00015F2D
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x00017D35 File Offset: 0x00015F35
			internal object <Encode>b__0(string id)
			{
				return PointerOrLocalIdEncoder.Instance.Encode(this.<>4__this.ClassController.CreateObjectWithoutData(this.<>4__this.TargetClassName, id, this.serviceHub), this.serviceHub);
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x00017D69 File Offset: 0x00015F69
			internal object <Encode>b__1(string id)
			{
				return PointerOrLocalIdEncoder.Instance.Encode(this.<>4__this.ClassController.CreateObjectWithoutData(this.<>4__this.TargetClassName, id, this.serviceHub), this.serviceHub);
			}

			// Token: 0x040002EA RID: 746
			public ParseRelationOperation <>4__this;

			// Token: 0x040002EB RID: 747
			public IServiceHub serviceHub;
		}
	}
}
