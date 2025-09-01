using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies types of network interfaces.</summary>
	// Token: 0x0200070B RID: 1803
	public enum NetworkInterfaceType
	{
		/// <summary>The interface type is not known.</summary>
		// Token: 0x040021DE RID: 8670
		Unknown = 1,
		/// <summary>The network interface uses an Ethernet connection. Ethernet is defined in IEEE standard 802.3.</summary>
		// Token: 0x040021DF RID: 8671
		Ethernet = 6,
		/// <summary>The network interface uses a Token-Ring connection. Token-Ring is defined in IEEE standard 802.5.</summary>
		// Token: 0x040021E0 RID: 8672
		TokenRing = 9,
		/// <summary>The network interface uses a Fiber Distributed Data Interface (FDDI) connection. FDDI is a set of standards for data transmission on fiber optic lines in a local area network.</summary>
		// Token: 0x040021E1 RID: 8673
		Fddi = 15,
		/// <summary>The network interface uses a basic rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x040021E2 RID: 8674
		BasicIsdn = 20,
		/// <summary>The network interface uses a primary rate interface Integrated Services Digital Network (ISDN) connection. ISDN is a set of standards for data transmission over telephone lines.</summary>
		// Token: 0x040021E3 RID: 8675
		PrimaryIsdn,
		/// <summary>The network interface uses a Point-To-Point protocol (PPP) connection. PPP is a protocol for data transmission using a serial device.</summary>
		// Token: 0x040021E4 RID: 8676
		Ppp = 23,
		/// <summary>The network interface is a loopback adapter. Such interfaces are often used for testing; no traffic is sent over the wire.</summary>
		// Token: 0x040021E5 RID: 8677
		Loopback,
		/// <summary>The network interface uses an Ethernet 3 megabit/second connection. This version of Ethernet is defined in IETF RFC 895.</summary>
		// Token: 0x040021E6 RID: 8678
		Ethernet3Megabit = 26,
		/// <summary>The network interface uses a Serial Line Internet Protocol (SLIP) connection. SLIP is defined in IETF RFC 1055.</summary>
		// Token: 0x040021E7 RID: 8679
		Slip = 28,
		/// <summary>The network interface uses asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040021E8 RID: 8680
		Atm = 37,
		/// <summary>The network interface uses a modem.</summary>
		// Token: 0x040021E9 RID: 8681
		GenericModem = 48,
		/// <summary>The network interface uses a Fast Ethernet connection over twisted pair and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-T.</summary>
		// Token: 0x040021EA RID: 8682
		FastEthernetT = 62,
		/// <summary>The network interface uses a connection configured for ISDN and the X.25 protocol. X.25 allows computers on public networks to communicate using an intermediary computer.</summary>
		// Token: 0x040021EB RID: 8683
		Isdn,
		/// <summary>The network interface uses a Fast Ethernet connection over optical fiber and provides a data rate of 100 megabits per second. This type of connection is also known as 100Base-FX.</summary>
		// Token: 0x040021EC RID: 8684
		FastEthernetFx = 69,
		/// <summary>The network interface uses a wireless LAN connection (IEEE 802.11 standard).</summary>
		// Token: 0x040021ED RID: 8685
		Wireless80211 = 71,
		/// <summary>The network interface uses an Asymmetric Digital Subscriber Line (ADSL).</summary>
		// Token: 0x040021EE RID: 8686
		AsymmetricDsl = 94,
		/// <summary>The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).</summary>
		// Token: 0x040021EF RID: 8687
		RateAdaptDsl,
		/// <summary>The network interface uses a Symmetric Digital Subscriber Line (SDSL).</summary>
		// Token: 0x040021F0 RID: 8688
		SymmetricDsl,
		/// <summary>The network interface uses a Very High Data Rate Digital Subscriber Line (VDSL).</summary>
		// Token: 0x040021F1 RID: 8689
		VeryHighSpeedDsl,
		/// <summary>The network interface uses the Internet Protocol (IP) in combination with asynchronous transfer mode (ATM) for data transmission.</summary>
		// Token: 0x040021F2 RID: 8690
		IPOverAtm = 114,
		/// <summary>The network interface uses a gigabit Ethernet connection and provides a data rate of 1,000 megabits per second (1 gigabit per second).</summary>
		// Token: 0x040021F3 RID: 8691
		GigabitEthernet = 117,
		/// <summary>The network interface uses a tunnel connection.</summary>
		// Token: 0x040021F4 RID: 8692
		Tunnel = 131,
		/// <summary>The network interface uses a Multirate Digital Subscriber Line.</summary>
		// Token: 0x040021F5 RID: 8693
		MultiRateSymmetricDsl = 143,
		/// <summary>The network interface uses a High Performance Serial Bus.</summary>
		// Token: 0x040021F6 RID: 8694
		HighPerformanceSerialBus,
		/// <summary>The network interface uses a mobile broadband interface for WiMax devices.</summary>
		// Token: 0x040021F7 RID: 8695
		Wman = 237,
		/// <summary>The network interface uses a mobile broadband interface for GSM-based devices.</summary>
		// Token: 0x040021F8 RID: 8696
		Wwanpp = 243,
		/// <summary>The network interface uses a mobile broadband interface for CDMA-based devices.</summary>
		// Token: 0x040021F9 RID: 8697
		Wwanpp2
	}
}
