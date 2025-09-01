using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x02000333 RID: 819
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.BaseTypes" /> collection is accessed for the first time.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060019EC RID: 6636 RVA: 0x00060F94 File Offset: 0x0005F194
		// (remove) Token: 0x060019ED RID: 6637 RVA: 0x00060FCC File Offset: 0x0005F1CC
		public event EventHandler PopulateBaseTypes
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.PopulateBaseTypes;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateBaseTypes, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.PopulateBaseTypes;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateBaseTypes, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.Members" /> collection is accessed for the first time.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x00061004 File Offset: 0x0005F204
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x0006103C File Offset: 0x0005F23C
		public event EventHandler PopulateMembers
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.PopulateMembers;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateMembers, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.PopulateMembers;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateMembers, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x060019F0 RID: 6640 RVA: 0x00061071 File Offset: 0x0005F271
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type.</param>
		// Token: 0x060019F1 RID: 6641 RVA: 0x00061096 File Offset: 0x0005F296
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x000610C2 File Offset: 0x0005F2C2
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x000610CA File Offset: 0x0005F2CA
		public TypeAttributes TypeAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeAttributes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeAttributes>k__BackingField = value;
			}
		} = TypeAttributes.Public;

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000610D3 File Offset: 0x0005F2D3
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateBaseTypes = this.PopulateBaseTypes;
					if (populateBaseTypes != null)
					{
						populateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this._baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a class or reference type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0006110A File Offset: 0x0005F30A
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0006112A File Offset: 0x0005F32A
		public bool IsClass
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this._isEnum && !this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.NotPublic;
					this._isStruct = false;
					this._isEnum = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0006115A File Offset: 0x0005F35A
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00061162 File Offset: 0x0005F362
		public bool IsStruct
		{
			get
			{
				return this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isEnum = false;
				}
				this._isStruct = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00061184 File Offset: 0x0005F384
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0006118C File Offset: 0x0005F38C
		public bool IsEnum
		{
			get
			{
				return this._isEnum;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
				}
				this._isEnum = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000611AE File Offset: 0x0005F3AE
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x000611C0 File Offset: 0x0005F3C0
		public bool IsInterface
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
					this._isEnum = false;
					return;
				}
				this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>
		///   <see langword="true" /> if the class or structure declaration is a partial representation of the implementation; <see langword="false" /> if the declaration is a complete implementation of the class or structure. The default is <see langword="false" />.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006120C File Offset: 0x0005F40C
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00061214 File Offset: 0x0005F414
		public bool IsPartial
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPartial>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsPartial>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006121D File Offset: 0x0005F41D
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateMembers = this.PopulateMembers;
					if (populateMembers != null)
					{
						populateMembers(this, EventArgs.Empty);
					}
				}
				return this._members;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00061254 File Offset: 0x0005F454
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection result;
				if ((result = this._typeParameters) == null)
				{
					result = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return result;
			}
		}

		// Token: 0x04000DE2 RID: 3554
		private readonly CodeTypeReferenceCollection _baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x04000DE3 RID: 3555
		private readonly CodeTypeMemberCollection _members = new CodeTypeMemberCollection();

		// Token: 0x04000DE4 RID: 3556
		private bool _isEnum;

		// Token: 0x04000DE5 RID: 3557
		private bool _isStruct;

		// Token: 0x04000DE6 RID: 3558
		private int _populated;

		// Token: 0x04000DE7 RID: 3559
		private const int BaseTypesCollection = 1;

		// Token: 0x04000DE8 RID: 3560
		private const int MembersCollection = 2;

		// Token: 0x04000DE9 RID: 3561
		private CodeTypeParameterCollection _typeParameters;

		// Token: 0x04000DEA RID: 3562
		[CompilerGenerated]
		private EventHandler PopulateBaseTypes;

		// Token: 0x04000DEB RID: 3563
		[CompilerGenerated]
		private EventHandler PopulateMembers;

		// Token: 0x04000DEC RID: 3564
		[CompilerGenerated]
		private TypeAttributes <TypeAttributes>k__BackingField;

		// Token: 0x04000DED RID: 3565
		[CompilerGenerated]
		private bool <IsPartial>k__BackingField;
	}
}
