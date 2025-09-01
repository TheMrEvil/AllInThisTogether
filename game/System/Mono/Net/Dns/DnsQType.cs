using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B9 RID: 185
	internal enum DnsQType : ushort
	{
		// Token: 0x040002BC RID: 700
		A = 1,
		// Token: 0x040002BD RID: 701
		NS,
		// Token: 0x040002BE RID: 702
		[Obsolete]
		MD,
		// Token: 0x040002BF RID: 703
		[Obsolete]
		MF,
		// Token: 0x040002C0 RID: 704
		CNAME,
		// Token: 0x040002C1 RID: 705
		SOA,
		// Token: 0x040002C2 RID: 706
		[Obsolete]
		MB,
		// Token: 0x040002C3 RID: 707
		[Obsolete]
		MG,
		// Token: 0x040002C4 RID: 708
		[Obsolete]
		MR,
		// Token: 0x040002C5 RID: 709
		[Obsolete]
		NULL,
		// Token: 0x040002C6 RID: 710
		[Obsolete]
		WKS,
		// Token: 0x040002C7 RID: 711
		PTR,
		// Token: 0x040002C8 RID: 712
		[Obsolete]
		HINFO,
		// Token: 0x040002C9 RID: 713
		[Obsolete]
		MINFO,
		// Token: 0x040002CA RID: 714
		MX,
		// Token: 0x040002CB RID: 715
		TXT,
		// Token: 0x040002CC RID: 716
		[Obsolete]
		RP,
		// Token: 0x040002CD RID: 717
		AFSDB,
		// Token: 0x040002CE RID: 718
		[Obsolete]
		X25,
		// Token: 0x040002CF RID: 719
		[Obsolete]
		ISDN,
		// Token: 0x040002D0 RID: 720
		[Obsolete]
		RT,
		// Token: 0x040002D1 RID: 721
		[Obsolete]
		NSAP,
		// Token: 0x040002D2 RID: 722
		[Obsolete]
		NSAPPTR,
		// Token: 0x040002D3 RID: 723
		SIG,
		// Token: 0x040002D4 RID: 724
		KEY,
		// Token: 0x040002D5 RID: 725
		[Obsolete]
		PX,
		// Token: 0x040002D6 RID: 726
		[Obsolete]
		GPOS,
		// Token: 0x040002D7 RID: 727
		AAAA,
		// Token: 0x040002D8 RID: 728
		LOC,
		// Token: 0x040002D9 RID: 729
		[Obsolete]
		NXT,
		// Token: 0x040002DA RID: 730
		[Obsolete]
		EID,
		// Token: 0x040002DB RID: 731
		[Obsolete]
		NIMLOC,
		// Token: 0x040002DC RID: 732
		SRV,
		// Token: 0x040002DD RID: 733
		[Obsolete]
		ATMA,
		// Token: 0x040002DE RID: 734
		NAPTR,
		// Token: 0x040002DF RID: 735
		KX,
		// Token: 0x040002E0 RID: 736
		CERT,
		// Token: 0x040002E1 RID: 737
		[Obsolete]
		A6,
		// Token: 0x040002E2 RID: 738
		DNAME,
		// Token: 0x040002E3 RID: 739
		[Obsolete]
		SINK,
		// Token: 0x040002E4 RID: 740
		OPT,
		// Token: 0x040002E5 RID: 741
		[Obsolete]
		APL,
		// Token: 0x040002E6 RID: 742
		DS,
		// Token: 0x040002E7 RID: 743
		SSHFP,
		// Token: 0x040002E8 RID: 744
		IPSECKEY,
		// Token: 0x040002E9 RID: 745
		RRSIG,
		// Token: 0x040002EA RID: 746
		NSEC,
		// Token: 0x040002EB RID: 747
		DNSKEY,
		// Token: 0x040002EC RID: 748
		DHCID,
		// Token: 0x040002ED RID: 749
		NSEC3,
		// Token: 0x040002EE RID: 750
		NSEC3PARAM,
		// Token: 0x040002EF RID: 751
		HIP = 55,
		// Token: 0x040002F0 RID: 752
		NINFO,
		// Token: 0x040002F1 RID: 753
		RKEY,
		// Token: 0x040002F2 RID: 754
		TALINK,
		// Token: 0x040002F3 RID: 755
		SPF = 99,
		// Token: 0x040002F4 RID: 756
		[Obsolete]
		UINFO,
		// Token: 0x040002F5 RID: 757
		[Obsolete]
		UID,
		// Token: 0x040002F6 RID: 758
		[Obsolete]
		GID,
		// Token: 0x040002F7 RID: 759
		[Obsolete]
		UNSPEC,
		// Token: 0x040002F8 RID: 760
		TKEY = 249,
		// Token: 0x040002F9 RID: 761
		TSIG,
		// Token: 0x040002FA RID: 762
		IXFR,
		// Token: 0x040002FB RID: 763
		AXFR,
		// Token: 0x040002FC RID: 764
		[Obsolete]
		MAILB,
		// Token: 0x040002FD RID: 765
		[Obsolete]
		MAILA,
		// Token: 0x040002FE RID: 766
		ALL,
		// Token: 0x040002FF RID: 767
		URI,
		// Token: 0x04000300 RID: 768
		TA = 32768,
		// Token: 0x04000301 RID: 769
		DLV
	}
}
