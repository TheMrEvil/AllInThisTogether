using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mono.CSharp
{
	// Token: 0x02000243 RID: 579
	public class LocationsBag
	{
		// Token: 0x06001CC5 RID: 7365 RVA: 0x0008AF2A File Offset: 0x0008912A
		[Conditional("FULL_AST")]
		public void AddLocation(object element, params Location[] locations)
		{
			this.simple_locs.Add(element, new List<Location>(locations));
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0008AF40 File Offset: 0x00089140
		[Conditional("FULL_AST")]
		public void InsertLocation(object element, int index, Location location)
		{
			List<Location> list;
			if (!this.simple_locs.TryGetValue(element, out list))
			{
				list = new List<Location>();
				this.simple_locs.Add(element, list);
			}
			list.Insert(index, location);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0008AF78 File Offset: 0x00089178
		[Conditional("FULL_AST")]
		public void AddStatement(object element, params Location[] locations)
		{
			if (locations.Length == 0)
			{
				throw new ArgumentException("Statement is missing semicolon location");
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0008AF89 File Offset: 0x00089189
		[Conditional("FULL_AST")]
		public void AddMember(MemberCore member, IList<Tuple<Modifiers, Location>> modLocations)
		{
			this.member_locs.Add(member, new LocationsBag.MemberLocations(modLocations));
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0008AF9D File Offset: 0x0008919D
		[Conditional("FULL_AST")]
		public void AddMember(MemberCore member, IList<Tuple<Modifiers, Location>> modLocations, Location location)
		{
			this.member_locs.Add(member, new LocationsBag.MemberLocations(modLocations, location));
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0008AFB2 File Offset: 0x000891B2
		[Conditional("FULL_AST")]
		public void AddMember(MemberCore member, IList<Tuple<Modifiers, Location>> modLocations, params Location[] locations)
		{
			this.member_locs.Add(member, new LocationsBag.MemberLocations(modLocations, locations));
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0008AFC7 File Offset: 0x000891C7
		[Conditional("FULL_AST")]
		public void AddMember(MemberCore member, IList<Tuple<Modifiers, Location>> modLocations, List<Location> locations)
		{
			this.member_locs.Add(member, new LocationsBag.MemberLocations(modLocations, locations));
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0008AFDC File Offset: 0x000891DC
		[Conditional("FULL_AST")]
		public void AppendTo(object element, Location location)
		{
			List<Location> list;
			if (!this.simple_locs.TryGetValue(element, out list))
			{
				list = new List<Location>();
				this.simple_locs.Add(element, list);
			}
			list.Add(location);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0008B014 File Offset: 0x00089214
		[Conditional("FULL_AST")]
		public void AppendToMember(MemberCore existing, params Location[] locations)
		{
			LocationsBag.MemberLocations memberLocations;
			if (this.member_locs.TryGetValue(existing, out memberLocations))
			{
				memberLocations.AddLocations(locations);
				return;
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0008B03C File Offset: 0x0008923C
		public List<Location> GetLocations(object element)
		{
			List<Location> result;
			this.simple_locs.TryGetValue(element, out result);
			return result;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0008B05C File Offset: 0x0008925C
		public LocationsBag.MemberLocations GetMemberLocation(MemberCore element)
		{
			LocationsBag.MemberLocations result;
			this.member_locs.TryGetValue(element, out result);
			return result;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008B079 File Offset: 0x00089279
		public LocationsBag()
		{
		}

		// Token: 0x04000A9D RID: 2717
		private Dictionary<object, List<Location>> simple_locs = new Dictionary<object, List<Location>>(ReferenceEquality<object>.Default);

		// Token: 0x04000A9E RID: 2718
		private Dictionary<MemberCore, LocationsBag.MemberLocations> member_locs = new Dictionary<MemberCore, LocationsBag.MemberLocations>(ReferenceEquality<MemberCore>.Default);

		// Token: 0x020003CF RID: 975
		public class MemberLocations
		{
			// Token: 0x0600276B RID: 10091 RVA: 0x000BC2BB File Offset: 0x000BA4BB
			public MemberLocations(IList<Tuple<Modifiers, Location>> mods)
			{
				this.Modifiers = mods;
			}

			// Token: 0x0600276C RID: 10092 RVA: 0x000BC2CA File Offset: 0x000BA4CA
			public MemberLocations(IList<Tuple<Modifiers, Location>> mods, Location loc) : this(mods)
			{
				this.AddLocations(loc);
			}

			// Token: 0x0600276D RID: 10093 RVA: 0x000BC2DA File Offset: 0x000BA4DA
			public MemberLocations(IList<Tuple<Modifiers, Location>> mods, Location[] locs) : this(mods)
			{
				this.AddLocations(locs);
			}

			// Token: 0x0600276E RID: 10094 RVA: 0x000BC2EA File Offset: 0x000BA4EA
			public MemberLocations(IList<Tuple<Modifiers, Location>> mods, List<Location> locs) : this(mods)
			{
				this.locations = locs;
			}

			// Token: 0x170008E2 RID: 2274
			public Location this[int index]
			{
				get
				{
					return this.locations[index];
				}
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06002770 RID: 10096 RVA: 0x000BC308 File Offset: 0x000BA508
			public int Count
			{
				get
				{
					return this.locations.Count;
				}
			}

			// Token: 0x06002771 RID: 10097 RVA: 0x000BC315 File Offset: 0x000BA515
			public void AddLocations(Location loc)
			{
				if (this.locations == null)
				{
					this.locations = new List<Location>();
				}
				this.locations.Add(loc);
			}

			// Token: 0x06002772 RID: 10098 RVA: 0x000BC336 File Offset: 0x000BA536
			public void AddLocations(params Location[] additional)
			{
				if (this.locations == null)
				{
					this.locations = new List<Location>(additional);
					return;
				}
				this.locations.AddRange(additional);
			}

			// Token: 0x040010C9 RID: 4297
			public readonly IList<Tuple<Modifiers, Location>> Modifiers;

			// Token: 0x040010CA RID: 4298
			private List<Location> locations;
		}
	}
}
