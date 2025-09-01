using System;

namespace IKVM.Reflection
{
	// Token: 0x02000069 RID: 105
	internal struct TypeName : IEquatable<TypeName>
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x000118F0 File Offset: 0x0000FAF0
		internal TypeName(string ns, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.ns = ns;
			this.name = name;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001190E File Offset: 0x0000FB0E
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00011916 File Offset: 0x0000FB16
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001191E File Offset: 0x0000FB1E
		public static bool operator ==(TypeName o1, TypeName o2)
		{
			return o1.ns == o2.ns && o1.name == o2.name;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00011946 File Offset: 0x0000FB46
		public static bool operator !=(TypeName o1, TypeName o2)
		{
			return o1.ns != o2.ns || o1.name != o2.name;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001196E File Offset: 0x0000FB6E
		public override int GetHashCode()
		{
			if (this.ns != null)
			{
				return this.ns.GetHashCode() * 37 + this.name.GetHashCode();
			}
			return this.name.GetHashCode();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public override bool Equals(object obj)
		{
			TypeName? typeName = obj as TypeName?;
			return typeName != null && typeName.Value == this;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000119D6 File Offset: 0x0000FBD6
		public override string ToString()
		{
			if (this.ns != null)
			{
				return this.ns + "." + this.name;
			}
			return this.name;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000119FD File Offset: 0x0000FBFD
		bool IEquatable<TypeName>.Equals(TypeName other)
		{
			return this == other;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00011A0C File Offset: 0x0000FC0C
		internal bool Matches(string fullName)
		{
			if (this.ns == null)
			{
				return this.name == fullName;
			}
			return this.ns.Length + 1 + this.name.Length == fullName.Length && (fullName.StartsWith(this.ns, StringComparison.Ordinal) && fullName[this.ns.Length] == '.') && fullName.EndsWith(this.name, StringComparison.Ordinal);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00011A84 File Offset: 0x0000FC84
		internal TypeName ToLowerInvariant()
		{
			return new TypeName((this.ns == null) ? null : this.ns.ToLowerInvariant(), this.name.ToLowerInvariant());
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00011AAC File Offset: 0x0000FCAC
		internal static TypeName Split(string name)
		{
			int num = name.LastIndexOf('.');
			if (num == -1)
			{
				return new TypeName(null, name);
			}
			return new TypeName(name.Substring(0, num), name.Substring(num + 1));
		}

		// Token: 0x04000211 RID: 529
		private readonly string ns;

		// Token: 0x04000212 RID: 530
		private readonly string name;
	}
}
