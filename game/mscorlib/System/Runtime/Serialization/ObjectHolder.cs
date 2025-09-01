using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000662 RID: 1634
	internal sealed class ObjectHolder
	{
		// Token: 0x06003D10 RID: 15632 RVA: 0x000D3D46 File Offset: 0x000D1F46
		internal ObjectHolder(long objID) : this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000D3D58 File Offset: 0x000D1F58
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000D3E14 File Offset: 0x000D2014
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000D3E9D File Offset: 0x000D209D
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000D3EAD File Offset: 0x000D20AD
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000D3ECD File Offset: 0x000D20CD
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000D3EDC File Offset: 0x000D20DC
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000D3F1C File Offset: 0x000D211C
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000D3F47 File Offset: 0x000D2147
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000D3F68 File Offset: 0x000D2168
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000D3FF3 File Offset: 0x000D21F3
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000D3FFC File Offset: 0x000D21FC
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000D407C File Offset: 0x000D227C
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000D4089 File Offset: 0x000D2289
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000D40AC File Offset: 0x000D22AC
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000D40B9 File Offset: 0x000D22B9
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000D40C6 File Offset: 0x000D22C6
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000D40FA File Offset: 0x000D22FA
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000D4111 File Offset: 0x000D2311
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000D411E File Offset: 0x000D231E
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000D412B File Offset: 0x000D232B
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000D4151 File Offset: 0x000D2351
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000D416D File Offset: 0x000D236D
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000D4175 File Offset: 0x000D2375
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000D4184 File Offset: 0x000D2384
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000D418C File Offset: 0x000D238C
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000D4195 File Offset: 0x000D2395
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000D41A0 File Offset: 0x000D23A0
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x000D41A8 File Offset: 0x000D23A8
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000D41B1 File Offset: 0x000D23B1
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000D41B9 File Offset: 0x000D23B9
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000D41F6 File Offset: 0x000D23F6
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000D41FE File Offset: 0x000D23FE
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000D4207 File Offset: 0x000D2407
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000D420F File Offset: 0x000D240F
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000D4217 File Offset: 0x000D2417
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000D4220 File Offset: 0x000D2420
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000D4247 File Offset: 0x000D2447
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000D4271 File Offset: 0x000D2471
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000D4279 File Offset: 0x000D2479
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000D428E File Offset: 0x000D248E
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x0400274E RID: 10062
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x0400274F RID: 10063
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x04002750 RID: 10064
		internal const int HAS_SURROGATE = 4;

		// Token: 0x04002751 RID: 10065
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x04002752 RID: 10066
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x04002753 RID: 10067
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x04002754 RID: 10068
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x04002755 RID: 10069
		private object m_object;

		// Token: 0x04002756 RID: 10070
		internal long m_id;

		// Token: 0x04002757 RID: 10071
		private int m_missingElementsRemaining;

		// Token: 0x04002758 RID: 10072
		private int m_missingDecendents;

		// Token: 0x04002759 RID: 10073
		internal SerializationInfo m_serInfo;

		// Token: 0x0400275A RID: 10074
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x0400275B RID: 10075
		internal FixupHolderList m_missingElements;

		// Token: 0x0400275C RID: 10076
		internal LongList m_dependentObjects;

		// Token: 0x0400275D RID: 10077
		internal ObjectHolder m_next;

		// Token: 0x0400275E RID: 10078
		internal int m_flags;

		// Token: 0x0400275F RID: 10079
		private bool m_markForFixupWhenAvailable;

		// Token: 0x04002760 RID: 10080
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x04002761 RID: 10081
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x04002762 RID: 10082
		private bool m_reachable;
	}
}
