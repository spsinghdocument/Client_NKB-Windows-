


using System;
using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Concurrent;

namespace Structure
{
	public class holder
	{
		public static ConcurrentDictionary<double,Order> HolderOrder=new ConcurrentDictionary<double,Order>();
	}

	public class Order
	{
		readonly int _type;
		public Order(int iType)
		{
			_type = iType;
		}

		public MS_OE_REQUEST MsOeRequest;
		public MS_OE_RESPONSE_TR MsOeResponseTr;
		public MS_SPD_OE_REQUEST MsSpdOeRequest;

		public new int GetType()
		{
			return _type;
		}

		public string OrderTypeName(int ival)
		{
			var retval="None";
			switch (GetType ()) {
				case  (int)type.MS_OE_REQUEST :
				retval ="ORDER ENTRY REQUEST";
				break;
				case (int)type.MS_OE_RESPONSE_TR:
				retval ="ORDER a REQUEST";
				break;
				case (int)type.MS_SPD_OE_REQUEST:
				retval ="ORDER d REQUEST";
				break;
			}
			return retval;
		}
	}

	public enum type
	{
		MS_OE_REQUEST=1,
		MS_OE_RESPONSE_TR=2,
		MS_SPD_OE_REQUEST=3,

	}



	
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct StrPacketFormatePacketCrack
	{
		public short Length;
		public Int32 SequenceNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] ResrvSequenceNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] CheckSum;
	}
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct StrPacketFormate
	{
		public short Length;
		public Int32 SequenceNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] ResrvSequenceNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] CheckSum;
		public short MsgCount;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INVITATION_MESSAGE
	{
		public Int16 TransactionCode;
		public Int16 InvitationCount;
	}

	/*+++
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct Message_Header
	{
		public Int16 TransactionCode;
		public Int32 LogTime;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] AlphaChar;
		public Int32 TraderId;
		public short ErrorCode;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] Timestamp;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] TimeStamp1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte [] TimeStamp2;
		public Int16 MessageLength;
	}
*/
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_BROKER_ELIGIBILITY_PER_MKT
	{
		/*Auction market: 1 BIT
        Spot market : 1 BIT
        Oddlot market : 1 BIT
        Normal market: 1 BIT*/
	// in above comment used 8 bits so i am using below one byte .....
	public byte market;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1)]
	public byte[] Reserved1;
}


[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct MS_SIGNON 
{
	public Message_Header header_obj;
	public Int32 UserId;//40
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
	public byte[] Password; //44
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
	public byte[] NewPassword;//52
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=26)]
	public byte[] TraderName;//60
	public Int32 LastPasswordChangeDate;		 //86
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
	public byte[] BrokerId;//90
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1)]
	public byte[] Reserved1;//95
	public short BranchId;//96
	public Int32 VersionNumber;//98
	public Int32 Batch2StartTime;//102
	public byte HostSwitchContext;//106
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=50)]
	public byte[] Colour;//107
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1)]
	public byte[] Reserved2;//157
	public short UserType;//158-//184
	[MarshalAs(UnmanagedType.R8)]
	public double SequenceNumber;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=14)]
	public byte[] WsClassName;//194
	public byte BrokerStatus;
	public byte ShowIndex;//
	public ST_BROKER_ELIGIBILITY_PER_MKT st_brk_elig_prmk_obj;
	public short MemberType;
	public byte ClearingStatus;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
	public byte[] BrokerName;

}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct SIGNOFF_OUT
{
	public Message_Header header_obj;
	public Int32 userid;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=145)]
	public byte [] Reserved;
}


[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct EXCH_PORFOLIO_REQ
{
	public Message_Header header_obj;
	public Int32 LastUpdateDtTime;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ST_MARKET_STATUS
{

	public Int16 Normal;
	public Int16 Oddlot;
	public Int16 Spot;
	public Int16 Auction;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ST_EX_MARKET_STATUS
{

	public Int16 Normal;
	public Int16 Oddlot;
	public Int16 Spot;
	public Int16 Auction;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ST_PL_MARKET_STATUS
{

	public Int16 Normal;
	public Int16 Oddlot;
	public Int16 Spot;
	public Int16 Auction;
}


[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct MS_UPDATE_LOCAL_DATABASE
{
	public Message_Header header_obj;

	public Int32 LastUpdateSecurityTime;
	public Int32 LastUpdateParticipantTime;
	public Int32 LastUpdateInstrumentTime;
	public Int32 LastUpdateIndexTime;
	public byte	RequestForOpenOrdersMessage;
	public byte Reserved;
	public ST_MARKET_STATUS st_obj;
	public ST_EX_MARKET_STATUS st_pl_obj;
	public ST_PL_MARKET_STATUS st_ex_obj;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ST_STOCK_ELIGIBLE_INDICATORS
{
	/*Reserved: 5 BIT
		Books Merged : 1 BIT
		Minimum Fill: 1 BIT
		AON: 1 BIT*/
	// in above comment used 8 bits so i am using below one byte .....
	public byte Reserved;
	public byte Reserved2;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct MS_SYSTEM_INFO_DATA
{
	public Message_Header header_obj;
	public ST_MARKET_STATUS st_obj;
		public ST_EX_MARKET_STATUS st_ex_obj;
		public ST_PL_MARKET_STATUS st_pl_obj;
	public byte UpdatePortfolio;
	public Int32 MarketIndex;
	public Int16 DefaultSettlementPeriod_normal;
	public Int16 DefaultSettlementPeriod_spot;
	public Int16 DefaultSettlementPeriod_auction;
	public Int16 CompetitorPeriod;
	public Int16 SolicitorPeriod;
	public Int16 WarningPercent;
	public Int16 VolumeFreezePercent;
	public Int16 SnapQuoteTime;
	public Int16 Reserved1;
	public Int32 BoardLotQuantity;
	public Int32 TickSize;
	public Int16 MaximumGtcDays;
	public ST_STOCK_ELIGIBLE_INDICATORS st_stoc_obj;
	public Int16 DisclosedQuantityPercentAllowed;
	public Int32 RiskFreeInterestRate;

}



[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct MS_SYSTEM_INFO_REQ
{
	//public StrPacketFormate pf_obj;
	public Message_Header header_obj;
	public Int32 LastUpdatePortfolioTime;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct PORTFOLIO_DATA
{
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
	public byte [] Portfolio;
	public Int32 Token;
	public Int32 LastUpdtDtTime;
	public byte DeleteFlag;
}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct EXCH_PORTFOLIO_RESP
{
	public Message_Header header_obj;
	public Int16 NoOfRecords;
	public byte MoreRecords;
	public byte Filler;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
	public PORTFOLIO_DATA [] prt_data;

}

/*+++
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct CONTRACT_DESC
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte []	InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte []	Symbol;
		public Int32 ExpiryDate;
		public Int32 StrikePrice;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte []	OptionType;
		public Int16 CALevel;

	}
*/

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INNER_MESSAGE_HEADER
	{
		public Int32 TraderId;
		public Int32 LogTime;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] AlphaChar;
		public Int16 TransactionCode;
		public short ErrorCode;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] Timestamp;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte[] TimeStamp1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
		public byte [] TimeStamp2;
		public Int16 MessageLength;
	}

	
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_MESSAGE_DOWNLOAD
	{
		public Message_Header header_obj;
		public double SequenceNumber;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct UPDATE_LDB_HEADER
	{
		//public StrPacketFormate pf_obj;
		public Message_Header header_obj;
		public Int16 Reserved1;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INDEX_DETAILS
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=15)]
		public byte [] IndexName;
		public Int32 Token;
		public Int32 LastUpdateDateTime;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_DOWNLOAD_INDEX
	{
		public Message_Header header_obj;
		public Int16 NoOfRecords;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
		public INDEX_DETAILS[] ind_obj;//there i will be work in latter......size 17
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_SEC_STATUS_PER_MARKET //[4]
	{
		public Int16 Status;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct TOKEN_AND_ELIGIBILITY// [35]
	{
		public Int32 Token;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public ST_SEC_STATUS_PER_MARKET[]ST_SEC_STATUS_PER_MARKET_obj;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_SECURITY_STATUS_UPDATE_INFO
	{
		public Message_Header header_obj;
		public Int16 NumberOfRecords;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)]
		public TOKEN_AND_ELIGIBILITY[] TOKEN_AND_ELIGIBILITY_obj;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct PARTICIPANT_UPDATE_INFO
	{
		public Message_Header header_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte []Participant_Id;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte[]ParticipantName;
		public byte	ParticipantStatus;
		public Int32 ParticipantUpdateDateTime;
		public byte	DeleteFlag;

	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_INSTRUMENT_UPDATE_INFO
	{
		public Message_Header header_obj;
		public Int16 InstrumentId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte InstrumentDescription;
		public Int32 InstrumentUpdateDateTime;
		public byte DeleteFlag;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct BCAST_INDEX_MAP_DETAILS
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=26)]
		public byte [] BcastName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] ChangedName;
		public byte DeleteFlag;
		public Int32 LastUpdateDateTime;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_DOWNLOAD_INDEX_MAP
	{
		public Message_Header header_obj;
		public Int16 NoOfRecords;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public BCAST_INDEX_MAP_DETAILS [] sIndicesMap;////there i will be work in latter......size 10

	}









//................Order
/*+++
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_ORDER_FLAGS
	{

		public byte STOrderFlagIn;
		public byte STOrderFlagOut;


	}
*/
/*++
 	 [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ADDITIONAL_ORDER_FLAGS
	{

		public byte Reserved1;

	}

*/




	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_OE_REQUEST
	{



		public Message_Header header_obj;
		public byte	ParticipantType;
		public byte Reserved1;
		public Int16 CompetitorPeriod;
		public Int16 SolicitorPeriod;
		public byte	Modified_CancelledBy;
		public byte Reserved2;
		public Int16 ReasonCode;
		public Int32 Reserved3;
		public Int32 TokenNo;

		public CONTRACT_DESC contract_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] CounterPartyBrokerId;
		public byte Reserved4;
		public Int16 Reserved5;
		public byte	CloseoutFlag;
		public byte Reserved6;
		public Int16 OrderType;
		[MarshalAs(UnmanagedType.R8)]
		public double OrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 BookType;
		public Int16 Buy_SellIndicator;
		public Int32 DisclosedVolume;
		public Int32 DisclosedVolumeRemaining;
		public Int32 TotalVolumeRemaining;
		public Int32 Volume;
		public Int32 VolumeFilledToday;
		public Int32 Price;
		public Int32 TriggerPrice;
		public Int32 GoodTillDate;
		public Int32 EntryDateTime;
		public Int32 MinimumFill_AONVolumel;
		public Int32 LastModified;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int16 BranchId;
		public Int32 TraderId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=24)]
		public byte [] cOrdFiller;
		public byte	Open_Close;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Settlor;
		public Int16 Pro_ClientIndicator;
		public Int16 SettlementPeriod;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;
		public Int16 GiveupFlag1;
		public byte filler1;
		public byte filler2; 
		[MarshalAs(UnmanagedType.R8)]
		public double nnffield;
		[MarshalAs(UnmanagedType.R8)]
		public double mkt_replay;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct CONTRACT_DESC_TR
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte []	InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte []	Symbol;
		public Int32 ExpiryDate;
		public Int32 StrikePrice;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte []	OptionType;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_OE_REQUEST_TR
	{
		public short TransactionCode;
		public Int32 UserId;
		public Int16 ReasonCode;
		public Int32 TokenNo;
		public CONTRACT_DESC_TR Contr_dec_tr_Obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 BookType;
		public Int16 Buy_SellIndicator;
		public Int32 DisclosedVolume;
		public Int32 Volume;
		public Int32 Price;
		public Int32 GoodTillDate;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int16 BranchId;
		public Int32 TraderId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public byte	Open_Close;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Settlor;
		public Int16 Pro_ClientIndicator;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;
		public Int32 filler; 
		[MarshalAs(UnmanagedType.R8)]
		public double nnffield;

}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_OM_REQUEST_TR
	{
		public short TransactionCode;
		public Int32 UserId;
		public byte Modified_CancelledBy;
		public Int32 TokenNo;
		public CONTRACT_DESC_TR Contr_dec_tr_Obj;
		[MarshalAs(UnmanagedType.R8)]
		public double OrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 BookType;
		public Int16 Buy_SellIndicator;
		public Int32 DisclosedVolume;
		public Int32 DisclosedVolumeRemaining;
		public Int32 TotalVolumeRemaining;
		public Int32 Volume;
		public Int32 VolumeFilledToday;
		public Int32 Price;
		public Int32 GoodTillDate;
		public Int32 EntryDateTime;
		public Int32 LastModified;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int16 BranchId;
		public Int32 TraderId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public byte	Open_Close;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Settlor;
		public Int16 Pro_ClientIndicator;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;
		public Int32 filler; 
		[MarshalAs(UnmanagedType.R8)]
		public double nnffield;
	}




	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_OE_RESPONSE_TR
	{
		public short TransactionCode;
		public Int32 LogTime;
		public Int32 UserId;
		public Int16 ErrorCode;
		[MarshalAs(UnmanagedType.R8)]
		public double TimeStamp1;//20
		public byte TimeStamp2;
		public byte Modified_CancelledBy;//22
		public Int16 ReasonCode;
		public Int32 TokenNo;//28
		public CONTRACT_DESC_TR Contr_dec_tr_Obj;
		public byte CloseoutFlag;
		[MarshalAs(UnmanagedType.R8)]
		public double OrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 BookType;
		public Int16 Buy_SellIndicator;
		public Int32 DisclosedVolume;
		public Int32 DisclosedVolumeRemaining;
		public Int32 TotalVolumeRemaining;
		public Int32 Volume;
		public Int32 VolumeFilledToday;
		public Int32 Price;
		public Int32 GoodTillDate;
		public Int32 EntryDateTime;
		public Int32 LastModified;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int16 BranchId;
		public Int32 TraderId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public byte	Open_Close;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Settlor;
		public Int16 Pro_ClientIndicator;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;
		public Int32 filler; 
		[MarshalAs(UnmanagedType.R8)]
		public double nnffield;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct MS_TRADE_CONFIRM_TR
	{
		public short TransactionCode;
		public Int32 LogTime;
		public Int32 TraderId;
		[MarshalAs(UnmanagedType.R8)]
		public double	Timestamp;
		[MarshalAs(UnmanagedType.R8)]
		public double Timestamp1;
		[MarshalAs(UnmanagedType.R8)]
		public double	Timestamp2;
		[MarshalAs(UnmanagedType.R8)]
		public double ResponseOrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte[]BrokerId;
		public byte Reserved;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 Buy_SellIndicator;
		public Int32 OriginalVolume;
		public Int32 DisclosedVolume;
		public Int32 RemainingVolume;
		public Int32 DisclosedVolumeRemaining;
		public Int32 Price;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int32 GoodTillDate;
		public Int32 FillNumber;
		public Int32 FillQuantity;
		public Int32 FillPrice;
		public Int32 VolumeFilledToday;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] ActivityType;
		public Int32 ActivityTime;
		public Int32 Token;
		public CONTRACT_DESC_TR Contr_dec_tr_Obj;
		public byte OpenClose;
		public byte BookType;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte[] Participant;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;

	}





	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct PRICE_VOL_MOD
	{
		public Message_Header header_obj;
		public Int32 TokenNo;
		public Int32 Trader_ID;
		[MarshalAs(UnmanagedType.R8)]
		public double OrderNumber;
		public Int32 Price;
		public Int32 Volume;
		public Int32 LastModified;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=4)]
		public byte [] Reference;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_TRADE_INQ_DATA
	{
		public Message_Header header_obj;
		public Int32 TokenNo;
		public CONTRACT_DESC ms_oe_obj;
		public Int32 FillNumber;
		public Int32 FillQuantity;
		public Int32 FillPrice;
		public byte MktType;
		public byte BuyOpenClose;
		public Int32 NewVolume;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BuyBrokerId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] SellBrokerId;
		public Int32 TraderId;
		public byte RequestedBy;
		public byte SellOpenClose;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] BuyAccountNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] SellAccountNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] BuyParticipant;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] SellParticipant;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] ReservedFiller;
		public byte BuyGiveupFlag;
		public byte SellGiveupFlag;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_TRADER_INT_MSG
	{
		public Message_Header header_obj;
		public Int32 TraderId;
		public Int32 Reserved; 
		public Int16 BroadCastMessage_Length;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=239)]
		public byte [] BroadCastMessage;
	}
/*+++
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_SPD_LEG_INFO
	{
		public Int32 token;
		public CONTRACT_DESC ms_oe_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] OpBrokerId2;
		public byte Fillerx2;
		public Int16 OrderType2;
		public Int16 BuySell2;
		public Int32 DisclosedVol2;
		public Int32 DisclosedVolRemaining2;
		public Int32 TotalVolRemaining2;
		public Int32 Volume2;
		public Int32 VolumeFilledToday2;
		public Int32 Price2;
		public Int32 TriggerPrice2;
		public Int32 MinFillAon2;
		public ST_ORDER_FLAGS st_ord_flg_obj2;
		//public ST_ORDER_FLAGS st_ord_flg_obj3;
		public ADDITIONAL_ORDER_FLAGS objadd;
		public byte OpenClose2;
		public byte GiveUpFlage2;
		public byte Fillery;

	}
*/

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_TRADE_CONFIRM
	{
		public Message_Header header_obj;
		[MarshalAs(UnmanagedType.R8)]
		public double ResponseOrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public byte Reserved;
		public Int32 TraderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int16 Buy_SellIndicator;
		public Int32 OriginalVolume;
		public Int32 DisclosedVolume;
		public Int32 RemainingVolume;
		public Int32 DisclosedVolume_Remaining;
		public Int32 Price;
		public ST_ORDER_FLAGS obj_stflg;
		public Int32 GoodTillDate;
		public Int32 FillNumber;
		public Int32 FillQuantity;
		public Int32 FillPrice;
		public Int32 VolumeFilledToday;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] ActivityType;
		public Int32 ActivityTime;
		[MarshalAs(UnmanagedType.R8)]
		public double CounterTraderOrderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] CounterBrokerId;
		public Int32 Token;
		public CONTRACT_DESC obj_comtrct_dsc;
		public byte OpenClose;
		public byte OldOpenClose;
		public byte BookType;
		public Int32 NewVolume;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] OldAccountNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Participant;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] OldParticipant;
		public ADDITIONAL_ORDER_FLAGS obj_add_ord;
		public byte  ReservedFiller;
		public byte	GiveUpTrade;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INSTRUMENT_USER
	{
		[MarshalAs(UnmanagedType.R8)]
		public double BranchBuyValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double BranchSellValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double BranchUsedBuyValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double BranchUsedSellValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double UserOrderBuyValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double UserOrderSellValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double UserOrderUsedBuyValueLimit;
		[MarshalAs(UnmanagedType.R8)]
		public double UserOrderUsedSellValueLimit;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_USER_ORDER_VAL_LIMIT_DATA//size not match....
	{

		public Message_Header header_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte[] BrokerId;
		public Int16 BranchId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte [] UserName;
		public Int32 UserId;
		public Int16 UserType;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public INSTRUMENT_USER[]obj_instru;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct DEALER_ORD_LMT
	{
		public Message_Header header_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public Int32 UserId;
		[MarshalAs(UnmanagedType.R8)]
		public double OrdQtyBuff;
		[MarshalAs(UnmanagedType.R8)]
		public double OrdValBuff;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct SPD_ORD_LMT
	{
		public Message_Header header_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		public Int32 UserId;
		[MarshalAs(UnmanagedType.R8)]
		public double SpdOrdQtyBuff;
		[MarshalAs(UnmanagedType.R8)]
		public double SpdOrdValBuff;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_BCAST_DESTINATION
	{
		public byte Reserved1;
		public byte Reserved2;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_BCAST_MESSAGE
	{
		public Message_Header header_obj;
		public Int16 BranchNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=3)]
		public byte [] ActionCode;
		public ST_BCAST_DESTINATION obj_st_bcst;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
		public byte[]Reserved;//26 byte
		public Int16 BroadCastMessage_Length;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=239)]
		public byte [] BroadCastMessage;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_RP_HDR
	{
		public Message_Header header_obj;
		public byte MessageType;
		public Int32 ReportDate;
		public Int16 UserType;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId ;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte [] FirmName;
		public Int32 TraderNumber;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=26)]
		public byte [] TraderName;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MKT_STATS_DATA
	{
		public CONTRACT_DESC obj_contract_dsc;
		public Int16 MarketType;
		public Int32 OpenPrice;
		public Int32 HighPrice;
		public Int32 LowPrice;
		public Int32 ClosingPrice;
		public Int32 TotalQuantityTraded;
		[MarshalAs(UnmanagedType.R8)]
		public double TotalValueTraded;
		public Int32 PreviousClosePrice;
		public Int32 OpenInterest;
		public Int32 ChgOpenInterest;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=4)]
		public byte [] Indicator;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_RP_MARKET_STATS
	{
		public Message_Header header_obj;
		public byte MessageType;
		public byte Reserved;
		public Int16 NumberOfRecords;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public MKT_STATS_DATA[] obj_mkt_stats_dat;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MKT_INDEX
	{
		public Int32 opening;
		public Int32 high;
		public Int32 low;
		public Int32 closing;
		public Int32 start;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MKT_IDX_RPT_DATA //size not match...
	{
		public Message_Header header_obj;
		public byte MessageType;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=15)]
		public byte [] Index_name;
		public MKT_INDEX obj_mkt_indx;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INDUSTRY_INDEX
	{
		public byte Industry_name;
		public Int32 Opening;
		public Int32 High;
		public Int32 Low;
		public Int32 Closing;
		public Int32 Start;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct IND_IDX_RPT_DATA //size not match...
	{
		public Message_Header header_obj;
		public byte MessageType;
		public byte Reserved;
		public Int16 NumberOf_Industry_Records;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public INDUSTRY_INDEX[] obj_industr_indx;
	}
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct INDEX_DATA
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=15)]
		public byte [] Sector_name;
		public Int32 Index_value;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct SECT_IDX_RPT_DATA
	{
		public Message_Header header_obj;
		public byte MessageType;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=15)]
		public byte [] Industry_name;
		public Int16 NumberOf_Industry_Records;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public INDEX_DATA[] obj_indx_dt;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_RP_TRAILER
	{
		public Message_Header header_obj;
		public byte MessageType;
		public Int32 NumberOfPackets;
		public byte	Reserved;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct SPD_STATS_DATA
	{
		public Int16 MARKETTYPE;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte [] INSTRUMENTNAME1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] SYMBOL1;
		public Int32 EXPIRYDATE1;
		public Int32 STRIKEPRICE1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] OPTIONTYPE1;
		public Int16 CALEVEL1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte [] INSTRUMENTNAME2;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] SYMBOL2;
		public Int32 EXPIRYDATE2;
		public Int32 STRIKEPRICE2;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] OPTIONTYPE2;
		public Int16 CALEVEL2;
		public Int32 OPENPD;
		public Int32 HIPD;
		public Int32 LOWPD;
		public Int32 LASTTRADEDPD;
		public Int32 NOOFCONTRACTSTRADED;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct RP_SPD_MKT_STATS
	{
		public Message_Header header_obj;
		public byte MessageType;
		public byte Reserved;
		public Int16 NumberOfRecords;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public SPD_STATS_DATA[] obj_spdstsdta;

	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct EX_PL_INFO
	{
		public Int32 Token;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte [] InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] Symbol;
		public Int32 ExpiryDate;
		public Int32 StrikePrice;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] OptionType;
		public Int16 CALevel;
		public Int16 ExplFlag;
		public Double ExplNumber;
		public Int16 MarketType;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber;
		public Int32 Quantity;
		public Int16 ProCLi;
		public Int16 ExerciseType;
		public Int32 EntryDateTime;
		public Int16 BranchId;
		public Int32 TraderId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=30)]
		public byte [] cOrdFiller;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] ParticipantId;
	}
		
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct EX_PL_REQUEST
	{
		public Message_Header header_obj;
		public Int16 ReasonCode;
		public EX_PL_INFO obj_explinfo;
	}

	//---------------------++++++++++++++++++++++++




	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_SPD_OE_REQUEST//size not not match....
	{
		public Message_Header header_obj;
		public byte ParticipantType1;
		public byte Filler1;
		public Int16 CompetitorPeriod1;
		public Int16 SolicitorPeriod1;
		public byte ModCxlBy1;
		public byte Filler9;
		public Int16 ReasonCode1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] StartAlpha1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] EndAlpha1;
		public Int32 Token1;
		public CONTRACT_DESC ms_oe_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] OpBrokerId1;
		public byte Fillerx1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=3)]
		public byte [] FillerOptions1;
		public byte Fillery1;
		public Int16 OrderType1;
		[MarshalAs(UnmanagedType.R8)]
		public double OrderNumber1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] AccountNumber1;
		public Int16 BookType1;
		public Int16 BuySell1;
		public Int32 DisclosedVol1;
		public Int32 DisclosedVolRemaining1;
		public Int32 TotalVolRemaining1;
		public Int32 Volume1;
		public Int32 VolumeFilledToday1;
		public Int32 Price1;
		public Int32 TriggerPrice1;
		public Int32 GoodTillDate1;
		public Int32 EntryDateTime1;
		public Int32 MinFillAon1;
		public Int32 LastModified1;
		public ST_ORDER_FLAGS st_ord_flg_obj;
		public Int16 BranchId1;
		public Int32 TraderId1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte [] BrokerId1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=24)]
		public byte [] cOrdFiller;
		public byte OpenClose1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte [] Settlor1 ;
		public Int16 ProClient1;
		public Int16 SettlementPeriod1;
		public ADDITIONAL_ORDER_FLAGS obj_add_order_flg;
		public byte GiveupFlag1;
		public UInt16 filler1;
		/*public UInt16 filler2;
		public UInt16 filler3;
		public UInt16 filler4;
		public UInt16 filler5;
		public UInt16 filler6;
		public UInt16 filler7;
		public UInt16 filler8;
		public UInt16 filler9;
		public UInt16 filler10;
		public UInt16 filler11;
		public UInt16 filler12;
		public UInt16 filler13;
		public UInt16 filler14;
		public UInt16 filler15;
		public UInt16 filler16;*/
public byte	filler17;
public byte filler18;
public double NnfField;
public long MktReplay;
public Int32 PriceDiff;
public MS_SPD_LEG_INFO leg2;
public MS_SPD_LEG_INFO leg3;



}





[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct Message_Header
{
	public Int16 TransactionCode;
	public Int32 LogTime;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
	public byte[] AlphaChar;
	public Int32 TraderId;
	public short ErrorCode;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
	public byte[] Timestamp;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
	public byte[] TimeStamp1;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=8)]
	public byte [] TimeStamp2;
	public Int16 MessageLength;
}




[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct CONTRACT_DESC
{
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
	public byte []	InstrumentName;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
	public byte []	Symbol;
	public Int32 ExpiryDate;
	public Int32 StrikePrice;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
	public byte []	OptionType;
	public Int16 CALevel;

}



[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ST_ORDER_FLAGS
{
	/*AON : 1 BIT
			IOC
				: 1 BIT
				GTC : 1 BIT
				Day
				: 1 BIT
				MIT
				: 1 BIT
				SL
				: 1 BIT
				Market
				: 1 BIT
				ATO
				: 1 BIT
				Reserved : 3 BIT
				Frozen
				: 1 BIT
				Modified : 1 BIT
				Traded
				: 1 BIT
				MatchedInd: 1 BIT
				MF
				: 1 BIT*/
	public byte STOrderFlagIn;
	public byte STOrderFlagOut;
}



[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct ADDITIONAL_ORDER_FLAGS
{
	/*Reserved : 1 bit
			COL
				: 1 bit
				Reserved : 6 bits*/
	public byte Reserved1;

}

[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
public struct MS_SPD_LEG_INFO
{
	public Int32 token;
	public CONTRACT_DESC ms_oe_obj;
	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
	public byte [] OpBrokerId2;
	public byte Fillerx2;
	public Int16 OrderType2;
	public Int16 BuySell2;
	public Int32 DisclosedVol2;
	public Int32 DisclosedVolRemaining2;
	public Int32 TotalVolRemaining2;
	public Int32 Volume2;
	public Int32 VolumeFilledToday2;
	public Int32 Price2;
	public Int32 TriggerPrice2;
	public Int32 MinFillAon2;
	public ST_ORDER_FLAGS st_ord_flg_obj2;
	//public ST_ORDER_FLAGS st_ord_flg_obj3;
	public byte OpenClose2;
	public ADDITIONAL_ORDER_FLAGS objadd;
	public byte GiveUpFlage2;
	public byte Fillery;

}



	
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct SEC_INFO
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte [] InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte [] Symbol;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] Series;
		public Int32 ExpiryDate;
		public Int32 StrikePrice;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte [] OptionType;
		public Int16 CALevel;
	}
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_SEC_ELIGIBILITY_PER_MARKET
	{
		public byte Reserved;
		public Int16 status;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_ELIGIBLITY_INDICATORS
	{
		public byte reserved1;
		public byte Reserved2;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_PURPOSE
	{
		public byte reserved1;
		public byte reserved2;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct MS_SECURITY_UPDATE_INFO
	{
		public Message_Header header_obj;
		public Int32 Token;
		public SEC_INFO sec_info_obj;
		public Int16 PermittedToTrade;
		public long IssuedCapital;
		public Int32 WarningQuantity;
		public Int32 FreezeQuantity;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte []CreditRating;// [12]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public ST_SEC_ELIGIBILITY_PER_MARKET[]ST_SEC_ELIGIBILITY_PER_MARKET_obj;
		public Int16 IssueRate;
		public Int32 IssueStartDate;
		public Int32 InterestPaymentDate;
		public Int32 IssueMaturityDate;
		public Int32 MarginPercentage;
		public Int32 MinimumLotQuantity;
		public Int32 BoardLotQuantity;
		public Int32 TickSize;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte[] Name;// [25]
		public byte Reserved;
		public Int32 ListingDate;
		public Int32 ExpulsionDate;
		public Int32 ReAdmissionDate;
		public Int32 RecordDate;
		public Int32 LowPriceRange;
		public Int32 HighPriceRange;
		public Int32 ExpiryDate;
		public Int32 NoDeliveryStartDate;
		public Int32 NoDeliveryEndDate;
		public ST_STOCK_ELIGIBLE_INDICATORS ST_STOCK_ELIGIBLE_INDICATORS_obj;
		public Int32 BookClosureStartDate;
		public Int32 BookClosureEndDate;
		public Int32 ExerciseStartDate;
		public Int32 ExerciseEndDate;
		public Int32 OldToken;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte[]AssetInstrument;// [ 6 ]
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte[]AssetName;
		public Int32 AssetToken;
		public Int32 IntrinsicValue;
		public Int32 ExtrinsicValue;
		public ST_PURPOSE ST_PURPOSE_obj;
		public Int32 LocalUpdateDateTime;
		public byte DeleteFlag;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte[]Remark;// [25]
		public Int32 BasePrice;

	}


	#region ClientStruct


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  C_SignIn
	{
		public short TransectionCode;
		public long ClintId;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=9)]
		public string Password;
		public short Status;
		public STGTYPE StgType;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  C_PktCreck
	{
		public short TransectionCode;
		public long OrderNo;
		public long ClintId;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  C_OrderReject
	{
		public long OrderNo;
		public Int16 Reasoncode;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct C_Contract_Desc
	{
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=7)]
		public string	InstrumentName;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=11)]
		public string	Symbol;
		public int ExpiryDate;
		public Int32 StrikePrice;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=3)]
		public string	OptionType;
		public Int16 CALevel;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  C_LotIN
	{
		public short TransectionCode;
		public long OrderNo;
		public long ClintId;
		public Int32 TokenNo;
		public C_Contract_Desc contract_obj;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=11)]
		public string AccountNumber;
		public Int16 Buy_SellIndicator;
		public Int32 DisclosedVolume;
		public Int32 Volume;
		public Int32 Price;
		public Int32 TriggerPrice;
		public byte Open_Close;
		public Int16 Reasoncode;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct C_MS_SPD_LEG_INFO
	{
		public int token; 
		public C_Contract_Desc contract_obj;	
		public short BuySell2;
		public Int32 Volume2;
		public byte OpenClose2;
		public Int32 Price2;
		public byte res; //Only for ThreeLeg U & V.
		public ST_ORDER_FLAGS st_ordFlg;
	}

	//
	//

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct C_Spread_lot_In
	{
		public short TransectionCode;
		public long OrderNo;
		public long ClintId;
		public Int32 TokenNo;
		public Int16 ReasonCode;
		public C_Contract_Desc contract_obj;//
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=11)]
		public string AccountNumber;
		public Int16 Buy_SellIndicator;
		public Int32 Volume;
		public Int32 Price;
		public int PriceDiff;
		public byte Open_Close;
		public ST_ORDER_FLAGS st_ordFlg;
		public C_MS_SPD_LEG_INFO obj_leg2;
		public C_MS_SPD_LEG_INFO obj_leg3;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  A_Information
	{
		public InfoCodeA Code;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=21)]
		public string Message;
		public bool flag;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  HeartBeatInfo
	{
		public bool tapStatus;
		public bool dataStatus;
		public short tapQueue;
		public short ClientQueue;
	}


	public enum InfoCodeA
	{
		Subscribe,	//LogIn of Admin
		Add,
		Update,
		LogOutUser,
		Cancel,
		Message,

	}
	/*
	public enum MsgTypeA
	{

		A_LOGIN,	//LogIn of Admin
		A_UPDATE,	
		A_UPDATEClient,
		A_UPDATEOrder,
		A_UPDATEComplete,
		A_MESSAGE

	}

	public enum MsgTypeC
	{
		ORDER,
		ORDERRej,
		LOGIN,
		MESSAGE,
		HEARTBEAT,
		FOPAIR,
		FOPAIRDIFF,
		FOPAIRUNSUB,
		IOCPAIR,
		IOCPAIRUNSUB,
		IOCPAIRDIFF
	}

*/
	public enum MessageType
	{
		ORDER=(byte)'A',
		ORDERRej=(byte)'B',
		LOGIN=(byte)'C',
		MESSAGE=(byte)'D',
		HEARTBEAT=(byte)'E',
		FOPAIR=(byte)'F',
		FOPAIRDIFF=(byte)'G',
		FOPAIRUNSUB=(byte)'H',
		IOCPAIR=(byte)'I',
		IOCPAIRUNSUB=(byte)'J',
		IOCPAIRDIFF=(byte)'K',
		A_LOGIN=(byte)'L',	//LogIn of Admin
		A_UPDATE=(byte)'M',	
		A_UPDATEClient=(byte)'N',
		A_UPDATEOrder=(byte)'O',
		A_UPDATEComplete=(byte)'P',
		A_MESSAGE=(byte)'Q',
		CANCELALL=(byte)'R',
		STOP_ALL=(byte)'S',
        DELETE=(byte)'T'

	/*	ORDER=,		//ORDER
		ORDRJ=,		//ORDERRej
		LOGIN=,
		MESSA=,		//MESSAGE
		HEART=,		//HEARTBEAT
		FOPR=,		//FOPAIR
		FOPDF=,	//FOPAIRDIFF
		FOPAIRUNSUB=,//FOPAIRUNSUB
		IOCPAIR=,	//IOCPAIR
		IOCPAIRUNSUB=,//IOCPAIRUNSUB
		IOCPAIRDIFF=, //IOCPAIRDIFF
		A_LOGIN=,	 //A_LOGIN	
		A_UPDATE=,	 //A_UPDATE	
		A_UPDATEClient=,//A_UPDATEClient
		A_UPDATEOrder=,	//A_UPDATEOrder
		A_UPDATEComplete=,//A_UPDATEComplete
		A_MESSAGE=		//A_MESSAGE
		*/
	}




	public enum LogInStatus
	{
		LogIn=201,
		PwdError=202,
		UserAlreadyLogIn=203,
		PwdExpire=204,
		LogOutStatus=205,
		LogOutbyAdmin=206,
		LogOutNoheartbeat=207,
		LogOut=208,
		UserAlreadyLogOut=209,

	}

	public enum ReasoncodeC
	{
		Block=101,
		LogOut=102,
		RecordDoesNotExist=103,
	}

	public enum BUYSELL
	{
		BUY=1,
		SELL=2
	}


	public enum BP
	{
		PROD = 1,
		BASE =2

	}

	public enum STGTYPE
	{
		FOFO =1,
		TWOLEGOPT =2
	}
	#endregion


	#region AdminStruct

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  A_SignIn
	{
		public short TransectionCode;
		public long ClintId;
		public short Password;
		public short NewPassword;
		public byte Status;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct  A_PktCreck
	{
		public short TransectionCode;
	}



		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct STREAM_HEADER:IDisposable
		{
			public Int16 msg_len;
			public Int16 stream_id;
			public int  seq_no;
			public void Dispose()
			{

			}
		}

		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Master_Data_Header:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte	Message_Type; 
			public Int32 Token_Count;
			public void Dispose()
			{

			}
		}

		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Contract_Information_Message:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte Message_Type;
			public Int16 Stream_ID;
			public Int32 Token_Number;
			[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=6)]
			public string Instrument;
			[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=10)]
			public string Symbol;
			public Int32 Expiry_Date;
			public Int32 Strike_Price;
			[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst=2)]
			public string Option_Type;
			public void Dispose()
			{

			}
		}

		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Spread_Contract_Information:IDisposable 
		{
			public STREAM_HEADER Global_Header; 
			public byte	Message_Type; 
			public Int16 stream_id;
			public Int32 Tokken1;
			public Int32 Tokken2;
			public void Dispose()
			{

			}
		}

		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Trailer_Message:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte	Message_Type; 
			public Int32 Token_Count;
			public void Dispose()
			{

			}
		}

		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public  struct Order_Message:IDisposable //:IComparable<Order_Message>
		{
			public STREAM_HEADER Global_Header; 
			public byte Message_Type;
			public long Timestamp;
			public double Order_Id;
			public Int32 Token;
			public byte Order_Type;
			public Int32 Price;
			public Int32 Quantity;

			public void Dispose()
			{

			}
		}


		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Trade_Message:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte Message_Type;
			public long Timestamp;
			public double Buyorder_Id;
			public double Selloder_Id;
			public Int32 Token;
			public Int32 Trade_Price;
			public Int32 Trade_Quantity;

			public void Dispose()
			{

			}
		}


		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Spread_Order_Message:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte Message_Type;
			public long Timestamp;
			public double Order_ID;
			public int Token;
			public byte Order_Type;
			public int Price;
			public int Quantity;
			public void Dispose()
			{

			}
		}


		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct Spread_Trade_Message:IDisposable
		{
			public STREAM_HEADER Global_Header; 
			public byte Message_Type;
			public long Timestamp;
			public double Buy_Order_ID;
			public double Sell_Order_ID;
			public int Token;
			public int Trade_Price;
			public int Quantity;
			public void Dispose()
			{

			}
		}





		[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
		public struct FinalPrice:IDisposable
		{
			public int Token;
			public int MAXBID; 
			public int MINASK;
			public int LTP;

			public void Dispose()
			{

			}
		}
	#endregion

	#region contractfile
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct Contract_File_Header
	{


		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte[]NEATFO;
		public byte res1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=5)]
		public byte[] VersionNumber;
		public byte res2;

		//public Int32 VersionNumber;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct ST_SEC_ELIGIBILITY_PER_MARKET2// [4]
	{
		public Int16 Security_Status;
		public byte res1;
		public byte Eligibility;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] Reserved;// [2]
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
	public struct Contract_File
	{
		public Contract_File_Header obj_contract_header;
		public Int32 Token;
		public byte res1;
		public Int32 AssetToken;
		public byte res2;
	//	[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public string InstrumentName;// [6]

		public byte res3;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public string Symbol;// [10]
		public byte res4;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[]Series;// [2]
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public byte[] resl; //[2]
		public long ExpiryDate;// (in seconds from January 1, 1980)
		public byte res5;
		public Int32 StrikePrice;
		public byte res6;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public string OptionType;// [2]
		public byte res7;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1)]
		public byte[]Category;// [1]
		public byte res8;
		public Int16 CALevel;
		public byte res9;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1)]
		public byte[]ReservedIdentifier;//[1]
		public byte res10;
		public Int16 PermittedToTrade;
		public byte res11;
		public Int16 IssueRate;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4)]
		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public ST_SEC_ELIGIBILITY_PER_MARKET2 []obj_st_sec;
		public byte res12;
		public Int32 IssueStartDate;
		public byte res13;
		public Int32 InterestPaymentDate;
		public byte res14;
		public Int32 Issue_Maturity_Date;
		public byte res15;
		public Int32 MarginPercentage;
		public byte res16;
		public Int32 MinimumLotQuantity;
		public byte res17;
		public Int32 BoardLotQuantity;
		public byte res18;
		public Int32 TickSize;
		public byte res19;
		public double IssuedCapital;
		public byte res20;
		public Int32 FreezeQuantity;
		public byte res21;
		public Int32 WarningQuantity;
		public byte res22;
		public Int32 ListingDate;
		public byte res23;
		public Int32 ExpulsionDate;
		public byte res24;
		public Int32 ReadmissionDate;
		public byte res25;
		public Int32 RecordDate;
		public byte res26;
		public Int32 NoDeliveryStartDate;
		public byte res27;
		public Int32 NoDeliveryEndDate;
		public byte res28;
		public Int32 LowPriceRange;
		public byte res29;
		public Int32 HighPriceRange;
		public byte res30;
		public Int32 ExDate;
		public byte res31;
		public Int32 BookClosureStartDate;
		public byte res32;
		public Int32 BookClosureEndDate;
		public byte res33;
		public Int32 LocalLDBUpdateDateTime;
		public byte res34;
		public Int32 ExerciseStartDate;
		public byte res35;
		public Int32 ExerciseEndDate;
		public byte res36;
		public Int16 TickerSelection;
		public byte res37;
		public Int32 OldTokenNumber;
		public byte res38;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=12)]
		public byte[]CreditRating;// [12]
		public byte res39;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=25)]
		public byte[]Name;// [25]
		public byte res40;
		public string EGMAGM;
		public byte res41;
		public byte InterestDivident;
		public byte res42;
		public byte RightsBonus;
		public byte res43;
		public byte MFAON;
		public byte res44;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=24)]
		public byte[] Remarks;// [24]
		public byte res45;
		public byte ExStyle;
		public byte res46;
		public char ExAllowed;
		public byte res47;
		public char ExRejectionAllowed;
		public byte res48;
		public char PlAllowed;
		public byte res49;
		public char CheckSum;
		public byte res50;
		public char IsCOrporateAdjusted;
		public byte res51;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public byte []SymbolForAsset;// [10]
		public byte res52;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public byte []InstrumentOfAsset;// [6]
		public byte res53;
		public Int32 BasePrice;
		public byte res54;
		public char DeleteFlag;
	}


	public struct spread_contract_file
	{
		public Contract_File_Header obj_contract_header;
		public Int32 Token1;
		public Int32 Token2;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public	byte []InstrumentName1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=10)]
		public	byte[]	Symbol1;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public	byte[]Series1;
		public	Int32	ExpiryDate1; 
		public	Int32	StrikePrice1; 
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public	byte[]OptionType1; 
		public	Int32	CALevel1; 
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=6)]
		public	byte []InstrumentName2;
		public	byte[]	Symbol2;
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public	byte []Series2;
		public	Int32	ExpiryDate2; 
		public	Int32	StrikePrice2; 
		[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=2)]
		public	byte[]OptionType2;
		public	Int32	CALevel2; 
		public	Int32	ReferencePrice; 
		public	Int32	DayLowPriceDiffRange; 
		public	Int32	DayHighPriceDiffRange; 
		public	Int32	OpLowPriceDiffRange; 
		public	Int32	OpHighPriceDiffRange; 
		public	Int32	BoardLotQuantity1; 
		public	Int32	MinimumLotQuantity1; 
		public	Int32	TickSize1; 
		public	Int32	BoardLotQuantity2; 
		public	Int32	MinimumLotQuantity2; 
		public	Int32	TickSize2; 
		public	char	Eligibility; 
		public char DeleteFlag;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack=1)]
	public struct strStreamHeader 
	{
		public short msg_len;
		public short stream_id;
		public int seq_no;
	}


	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct strTickDataRecoveryRequestMsg
	{
		public byte msgType;
		public short StreamId;
		public int StartSeqNo;
		public int EndSeqNo;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential,Pack=1 )]
	public struct strTickDataRecoveryResponseMsg
	{
		public strStreamHeader GlobalHeader;
		public byte msgType;
		public byte RequestStatus;
	}
	#endregion


	#region packet
	public class DataPacket:IDisposable
	{
		public static byte[] RawSerialize(object anything)
		{
			int rawsize = Marshal.SizeOf(anything);
			IntPtr buffer = Marshal.AllocHGlobal(rawsize);
			Marshal.StructureToPtr(anything, buffer, false);
			byte[] rawdatas = new byte[rawsize];
			Marshal.Copy(buffer, rawdatas, 0, rawsize);
			Marshal.FreeHGlobal(buffer);
			return rawdatas;
		}
		public static object RawDeserialize(byte[] rawdatas, Type anytype)
		{
			int rawsize = Marshal.SizeOf(anytype);
			if (rawsize > rawdatas.Length)
			{
				return null;
			}
			IntPtr buffer = Marshal.AllocHGlobal(rawsize);
			Marshal.Copy(rawdatas, 0, buffer, rawsize);
			object retobj = Marshal.PtrToStructure(buffer, anytype);
			Marshal.FreeHGlobal(buffer);
			return retobj;
		}

		public void Dispose()
		{
			//	GC.SuppressFinalize(this);
		}

		~DataPacket()
		{

		}
	}

	#endregion
	
	#region Structure GetString

	public class StructGetString
	{
		
		public double DoubleEndianChange(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse((Array) bytes, 0, bytes.Length);
			return BitConverter.ToDouble(bytes, 0);
		}

		public string PktFormate_GetString (StrPacketFormate obj)
		{
			return	"PktFormate.Length=" + IPAddress.HostToNetworkOrder (obj.Length)
				+ ",PktFormate.SequenceNumber=" + IPAddress.HostToNetworkOrder (obj.SequenceNumber)
					+ ",PktFormate.ResrvSequenceNumber=" + System.Text.Encoding.UTF8.GetString (obj.ResrvSequenceNumber)
					+ ",PktFormate.CheckSum="// + System.Text.Encoding.UTF8.GetString (obj.CheckSum)
					+ ",PktFormate.MsgCount=" + IPAddress.HostToNetworkOrder (obj.MsgCount);
		}

		public string MsgHeader_GetString (Message_Header obj)
		{
			return	"MsgHeader.TransactionCode=" + IPAddress.HostToNetworkOrder (obj.TransactionCode)
				+ ",MsgHeader.LogTime=" + IPAddress.HostToNetworkOrder (obj.LogTime)
					+ ",MsgHeader.AlphaChar=" + System.Text.Encoding.UTF8.GetString (obj.AlphaChar)
					+ ",MsgHeader.TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ ",MsgHeader.ErrorCode=" + IPAddress.HostToNetworkOrder (obj.ErrorCode)
					+ ",MsgHeader.TimeStamp=" + System.Text.Encoding.UTF8.GetString (obj.Timestamp)
					+ ",MsgHeader.TimeStamp1=" + System.Text.Encoding.UTF8.GetString (obj.TimeStamp1)
					+ ",MsgHeader.TimeStamp2=" + System.Text.Encoding.UTF8.GetString (obj.TimeStamp2)
					+ ",MsgHeader.MessageLength=" + IPAddress.HostToNetworkOrder (obj.MessageLength);
		}

		public string MS_SIGNON_GetString (MS_SIGNON obj)
		{
			return	"SIGNON.UserId=" + IPAddress.HostToNetworkOrder (obj.UserId)
				+ ",SIGNON.Password=" + System.Text.Encoding.UTF8.GetString (obj.Password)
					+ ",SIGNON.NewPassword=" + System.Text.Encoding.UTF8.GetString (obj.NewPassword)
					+ ",SIGNON.TraderName=" + System.Text.Encoding.UTF8.GetString (obj.TraderName)
					+ ",SIGNON.LastPasswordChangeDate=" + IPAddress.HostToNetworkOrder (obj.LastPasswordChangeDate)
					+ ",SIGNON.BrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId)
					+ ",SIGNON.Reserved1=" + System.Text.Encoding.UTF8.GetString (obj.Reserved1)
					+ ",SIGNON.BranchId=" + IPAddress.HostToNetworkOrder (obj.BranchId)
					+ ",SIGNON.VersionNumber=" + IPAddress.HostToNetworkOrder (obj.VersionNumber)
					+ ",SIGNON.Batch2StartTime=" + IPAddress.HostToNetworkOrder (obj.Batch2StartTime)
					+ ",SIGNON.HostSwitchContext=" + obj.HostSwitchContext
					+ ",SIGNON.Colour=" + System.Text.Encoding.UTF8.GetString (obj.Colour)
					+ ",SIGNON.Reserved2=" + System.Text.Encoding.UTF8.GetString (obj.Reserved2)
					+ ",SIGNON.UserType=" + IPAddress.HostToNetworkOrder (obj.UserType)
					+ ",SIGNON.SequenceNumber=" + (long)DoubleEndianChange(obj.SequenceNumber)
					+ ",SIGNON.WsClassName=" + System.Text.Encoding.UTF8.GetString (obj.WsClassName)
					+ ",SIGNON.BrokerStatus=" + obj.BrokerStatus
					+ ",SIGNON.ShowIndex=" + obj.ShowIndex
					+ ",SIGNON.st_brk_elig_prmk_obj.market=" + obj.st_brk_elig_prmk_obj.market
					+ ",SIGNON.st_brk_elig_prmk_obj.Reserved1=" + System.Text.Encoding.UTF8.GetString (obj.st_brk_elig_prmk_obj.Reserved1)
					+ ",SIGNON.MemberType=" + IPAddress.HostToNetworkOrder (obj.MemberType)
					+ ",SIGNON.ClearingStatus=" + obj.ClearingStatus
					+ ",SIGNON.BrokerName=" + System.Text.Encoding.UTF8.GetString (obj.BrokerName);		
		}

		public string MS_SYSTEM_INFO_DATA_GetString (MS_SYSTEM_INFO_DATA obj)
		{
			return "BoardLotQuantity=" + IPAddress.HostToNetworkOrder (obj.BoardLotQuantity)				
				+ ",CompetitorPeriod=" + IPAddress.HostToNetworkOrder (obj.CompetitorPeriod)
					+ ",DefaultSettlementPeriod_auction=" + IPAddress.HostToNetworkOrder (obj.DefaultSettlementPeriod_auction)
					+ ",DefaultSettlementPeriod_normal=" + IPAddress.HostToNetworkOrder (obj.DefaultSettlementPeriod_normal)				
					+ ",DefaultSettlementPeriod_spot=" + IPAddress.HostToNetworkOrder (obj.DefaultSettlementPeriod_spot)
					+ ",DisclosedQuantityPercentAllowed=" + IPAddress.HostToNetworkOrder (obj.DisclosedQuantityPercentAllowed)					
					+ ",AlphaChar=" + System.Text.Encoding.UTF8.GetString (obj.header_obj.AlphaChar)
					+ ",ErrorCode=" + IPAddress.HostToNetworkOrder (obj.header_obj.ErrorCode)					
					+ ",LogTime=" + IPAddress.HostToNetworkOrder (obj.header_obj.LogTime)
					+ ",MessageLength=" + IPAddress.HostToNetworkOrder (obj.header_obj.MessageLength)					
					+ ",Timestamp=" + System.Text.Encoding.UTF8.GetString (obj.header_obj.Timestamp)
					+ ",TimeStamp1=" + System.Text.Encoding.UTF8.GetString (obj.header_obj.TimeStamp1)					
					+ ",TimeStamp2=" + System.Text.Encoding.UTF8.GetString (obj.header_obj.TimeStamp2)
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.header_obj.TraderId)					
					+ ",TransactionCode=" + IPAddress.HostToNetworkOrder (obj.header_obj.TransactionCode)
					+ ",MarketIndex=" + IPAddress.HostToNetworkOrder (obj.MarketIndex)					
					+ ",MaximumGtcDays=" + IPAddress.HostToNetworkOrder (obj.MaximumGtcDays)
					+ ",Reserved1=" + IPAddress.HostToNetworkOrder (obj.Reserved1)					
					+ ",RiskFreeInterestRate=" + IPAddress.HostToNetworkOrder (obj.RiskFreeInterestRate)
					+ ",SnapQuoteTime=" + IPAddress.HostToNetworkOrder (obj.SnapQuoteTime)
					+ ",SolicitorPeriod=" + IPAddress.HostToNetworkOrder (obj.SolicitorPeriod)
					+ ",st_ex_obj Auction=" + IPAddress.HostToNetworkOrder (obj.st_ex_obj.Auction)
					+ ",st_ex_obj Normal=" + IPAddress.HostToNetworkOrder (obj.st_ex_obj.Normal)
					+ ",st_ex_obj Oddlot=" + IPAddress.HostToNetworkOrder (obj.st_ex_obj.Oddlot)
					+ ",st_ex_obj Spot=" + IPAddress.HostToNetworkOrder (obj.st_ex_obj.Spot)
					+ ",st_obj Auction=" + IPAddress.HostToNetworkOrder (obj.st_obj.Auction)
					+ ",st_obj Normal=" + IPAddress.HostToNetworkOrder (obj.st_obj.Normal)
					+ ",st_obj Oddlot=" + IPAddress.HostToNetworkOrder (obj.st_obj.Oddlot)
					+ ",st_obj Spot=" + IPAddress.HostToNetworkOrder (obj.st_obj.Spot)
					+ ",st_pl_obj Auction=" + IPAddress.HostToNetworkOrder (obj.st_pl_obj.Auction)
					+ ",st_pl_obj Normal=" + IPAddress.HostToNetworkOrder (obj.st_pl_obj.Normal)					
					+ ",st_pl_obj Oddlot=" + IPAddress.HostToNetworkOrder (obj.st_pl_obj.Oddlot)
					+ ",st_pl_obj Spot=" + IPAddress.HostToNetworkOrder (obj.st_pl_obj.Spot)
					+ ",st_stoc_obj.Reserved=" + Convert.ToChar (obj.st_stoc_obj.Reserved)					
					+ ",Reserved2=" + Convert.ToByte (obj.st_stoc_obj.Reserved2)
					+ ",TickSize=" + IPAddress.HostToNetworkOrder (obj.TickSize)
					+ ",UpdatePortfolio=" + Convert.ToByte (obj.UpdatePortfolio)
					+ ",VolumeFreezePercent=" + IPAddress.HostToNetworkOrder (obj.VolumeFreezePercent)					
					+ ",WarningPercent=" + IPAddress.HostToNetworkOrder (obj.WarningPercent);
		}

		public string CONTRACT_DESC_GetString (CONTRACT_DESC  obj)
		{
			return	"InstrumentName=" + System.Text.Encoding.UTF8.GetString (obj.InstrumentName)	
				+ ",Symbol=" + System.Text.Encoding.UTF8.GetString (obj.Symbol)	
					+ ",ExpiryDate=" + IPAddress.HostToNetworkOrder (obj.ExpiryDate)	
					+ ",StrikePrice=" + IPAddress.HostToNetworkOrder (obj.StrikePrice)	
					+ ",OptionType=" + System.Text.Encoding.UTF8.GetString (obj.OptionType)
					+ ",CALevel=" + IPAddress.HostToNetworkOrder (obj.CALevel);
		}

		public string MS_OE_REQUEST_GetString (MS_OE_REQUEST obj)
		{			
			return	"ParticipantType=" + obj.ParticipantType	
					+ ",Reserved1=" + obj.Reserved1	
					+ ",CompetitorPeriod=" + IPAddress.HostToNetworkOrder (obj.CompetitorPeriod)
					+ ",SolicitorPeriod=" + IPAddress.HostToNetworkOrder (obj.SolicitorPeriod)
					+ ",Modified_CancelledBy=" + obj.Modified_CancelledBy	
					+ ",Reserved2=" + obj.Reserved2	
					+ ",ReasonCode=" + IPAddress.HostToNetworkOrder (obj.ReasonCode)	
					+ ",Reserved3=" + IPAddress.HostToNetworkOrder (obj.Reserved3)	
					+ ",TokenNo=" + IPAddress.HostToNetworkOrder (obj.TokenNo)
					+ "," + CONTRACT_DESC_GetString (obj.contract_obj)
					+ ",CounterPartyBrokerId=" + System.Text.Encoding.UTF8.GetString (obj.CounterPartyBrokerId)	
					+ ",Reserved4=" + obj.Reserved4	
					+ ",Reserved5=" + IPAddress.HostToNetworkOrder (obj.Reserved5)	
					+ ",CloseoutFlag=" + obj.CloseoutFlag	
					+ ",Reserved6=" + obj.Reserved6	
					+ ",OrderType=" + IPAddress.HostToNetworkOrder (obj.OrderType)	
					+ ",OrderNumber=" + (long)DoubleEndianChange (obj.OrderNumber) //IPAddress.HostToNetworkOrder ((long)obj.OrderNumber)	
					+ ",AccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.AccountNumber)	
					+ ",BookType=" + IPAddress.HostToNetworkOrder (obj.BookType)	
					+ ",Buy_SellIndicator=" + IPAddress.HostToNetworkOrder (obj.Buy_SellIndicator)	
					+ ",DisclosedVolume=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolume)
					+ ",DisclosedVolumeRemaining=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolumeRemaining)
					+ ",TotalVolumeRemaining=" + IPAddress.HostToNetworkOrder (obj.TotalVolumeRemaining)
					+ ",Volume=" + IPAddress.HostToNetworkOrder (obj.Volume)	
					+ ",VolumeFilledToday=" + IPAddress.HostToNetworkOrder (obj.VolumeFilledToday)
					+ ",Price=" + IPAddress.HostToNetworkOrder (obj.Price)	
					+ ",TriggerPrice=" + IPAddress.HostToNetworkOrder (obj.TriggerPrice)	
					+ ",GoodTillDate=" + IPAddress.HostToNetworkOrder (obj.GoodTillDate)	
					+ ",EntryDateTime=" + IPAddress.HostToNetworkOrder (obj.EntryDateTime)	
					+ ",MinimumFill_AONVolumel=" + IPAddress.HostToNetworkOrder (obj.MinimumFill_AONVolumel)	
					+ ",LastModified=" + IPAddress.HostToNetworkOrder (obj.LastModified)
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagIn	
					+ ",STOrderFlagOut=" + obj.st_ord_flg_obj.STOrderFlagOut	
					+ ",BranchId=" + IPAddress.HostToNetworkOrder (obj.BranchId)	
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ ",BrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId)	
					+ ",cOrdFiller=" + System.Text.Encoding.UTF8.GetString (obj.cOrdFiller)	
					+ ",Open_Close=" + obj.Open_Close	
					+ ",Settlor=" + System.Text.Encoding.UTF8.GetString (obj.Settlor)		
					+ ",Pro_ClientIndicator=" + IPAddress.HostToNetworkOrder (obj.Pro_ClientIndicator)	
					+ ",SettlementPeriod=" + IPAddress.HostToNetworkOrder (obj.SettlementPeriod)	
					+ ",obj_add_order_flg=" + obj.obj_add_order_flg.Reserved1	
					+ ",GiveupFlag1=" + IPAddress.HostToNetworkOrder (obj.GiveupFlag1)
					+ ",filler1=" + obj.filler1	
					+ ",filler2=" + obj.filler2	
					+ ",nnffield=" + (long)DoubleEndianChange(obj.nnffield)
					+ ",mkt_replay=" + (long)DoubleEndianChange(obj.mkt_replay);
		}

		public string CONTRACT_DESC_TR_GetString (CONTRACT_DESC_TR  obj)
		{
			return	"InstrumentName=" + System.Text.Encoding.UTF8.GetString (obj.InstrumentName)	
				+ ",Symbol=" + System.Text.Encoding.UTF8.GetString (obj.Symbol)	
					+ ",ExpiryDate=" + IPAddress.HostToNetworkOrder (obj.ExpiryDate)	
					+ ",StrikePrice=" + IPAddress.HostToNetworkOrder (obj.StrikePrice)	
					+ ",OptionType=" + System.Text.Encoding.UTF8.GetString (obj.OptionType);
		}


		public string MS_OE_REQUEST_TR_GetString (MS_OE_REQUEST_TR obj)
		{
			return "TransactionCode=" + IPAddress.HostToNetworkOrder (obj.TransactionCode)	
				+ ",UserId=" + IPAddress.HostToNetworkOrder (obj.UserId)	
					+ ",ReasonCode=" + IPAddress.HostToNetworkOrder (obj.ReasonCode)	
					+ ",TokenNo=" + IPAddress.HostToNetworkOrder (obj.TokenNo)
					+ "," + CONTRACT_DESC_TR_GetString (obj.Contr_dec_tr_Obj)
					+ ",AccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.AccountNumber)	
					+ ",BookType=" + IPAddress.HostToNetworkOrder (obj.BookType)	
					+ ",Buy_SellIndicator=" + IPAddress.HostToNetworkOrder (obj.Buy_SellIndicator)	
					+ ",DisclosedVolume=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolume)	
					+ ",Volume=" + IPAddress.HostToNetworkOrder (obj.Volume)	
					+ ",Price=" + IPAddress.HostToNetworkOrder (obj.Price)	
					+ ",GoodTillDate=" + IPAddress.HostToNetworkOrder (obj.GoodTillDate)	
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagIn	
					+ ",STOrderFlagOut=" + obj.st_ord_flg_obj.STOrderFlagOut	
					+ ",BranchId=" + IPAddress.HostToNetworkOrder (obj.BranchId)	
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ ",BrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId)	
					+ ",Open_Close=" + obj.Open_Close	
					+ ",Settlor=" + obj.Settlor	
					+ ",Pro_ClientIndicator=" + IPAddress.HostToNetworkOrder (obj.Pro_ClientIndicator)	
					+ ",obj_add_order_flg=" + obj.obj_add_order_flg.Reserved1	
					+ ",filler=" + IPAddress.HostToNetworkOrder (obj.filler)	
					+ ",nnffield=" + (long)DoubleEndianChange(obj.nnffield);
		}

		public string MS_OE_RESPONSE_TR_GetString (MS_OE_RESPONSE_TR  obj)
		{
			return "TransactionCode=" + IPAddress.HostToNetworkOrder (obj.TransactionCode)	
				+ ",LogTime=" + IPAddress.HostToNetworkOrder (obj.LogTime)	
					+ ",UserId=" + IPAddress.HostToNetworkOrder (obj.UserId)	
					+ ",ErrorCode=" + IPAddress.HostToNetworkOrder (obj.ErrorCode)	
					+ ",TimeStamp1=" + (long)DoubleEndianChange(obj.TimeStamp1)
					+ ",TimeStamp2=" + obj.TimeStamp2	
					+ ",Modified_CancelledBy=" + obj.Modified_CancelledBy	
					+ ",ReasonCode=" + IPAddress.HostToNetworkOrder (obj.ReasonCode)			
					+ ",TokenNo=" + IPAddress.HostToNetworkOrder (obj.TokenNo)	
					+ "," + CONTRACT_DESC_TR_GetString (obj.Contr_dec_tr_Obj)
					+ ",CloseoutFlag=" + obj.CloseoutFlag	
					+ ",OrderNumber=" + (long)DoubleEndianChange (obj.OrderNumber)// IPAddress.HostToNetworkOrder ((long)obj.OrderNumber)
					+ ",AccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.AccountNumber)	
					+ ",BookType=" + IPAddress.HostToNetworkOrder (obj.BookType)	
					+ ",Buy_SellIndicator=" + IPAddress.HostToNetworkOrder (obj.Buy_SellIndicator)	
					+ ",DisclosedVolume=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolume)	
					+ ",DisclosedVolumeRemaining=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolumeRemaining)	
					+ ",TotalVolumeRemaining=" + IPAddress.HostToNetworkOrder (obj.TotalVolumeRemaining)	
					+ ",Volume=" + IPAddress.HostToNetworkOrder (obj.Volume)	
					+ ",VolumeFilledToday=" + IPAddress.HostToNetworkOrder (obj.VolumeFilledToday)	
					+ ",Price=" + IPAddress.HostToNetworkOrder (obj.Price)	
					+ ",GoodTillDate=" + IPAddress.HostToNetworkOrder (obj.GoodTillDate)	
					+ ",EntryDateTime=" + IPAddress.HostToNetworkOrder (obj.EntryDateTime)	
					+ ",LastModified=" + IPAddress.HostToNetworkOrder (obj.LastModified)	
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagIn	
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagOut	
					+ ",BranchId=" + IPAddress.HostToNetworkOrder (obj.BranchId)	
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ ",BrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId)
					+ ",Open_Close=" + obj.Open_Close	
					+ ",Settlor=" + System.Text.Encoding.UTF8.GetString (obj.Settlor)	
					+ ",Pro_ClientIndicator=" + IPAddress.HostToNetworkOrder (obj.Pro_ClientIndicator)	
					+ ",obj_add_order_flg=" + obj.obj_add_order_flg.Reserved1	
					+ ",filler=" + IPAddress.HostToNetworkOrder (obj.filler)	
					+ ",nnffield=" + (long)DoubleEndianChange(obj.nnffield);
		}



		public string MS_OM_REQUEST_TR_GetString (MS_OM_REQUEST_TR obj)
		{	
			return "TransactionCode : " + IPAddress.HostToNetworkOrder (obj.TransactionCode)	
				+ ",UserId : " + IPAddress.HostToNetworkOrder (obj.UserId)	
					+ ",Modified_CancelledBy: " + obj.Modified_CancelledBy	
					+ ",TokenNo : " + IPAddress.HostToNetworkOrder (obj.TokenNo)
					+ "," + CONTRACT_DESC_TR_GetString (obj.Contr_dec_tr_Obj)
					+ ",OrderNumber : " + (long)DoubleEndianChange (obj.OrderNumber) //IPAddress.HostToNetworkOrder ((long)obj.OrderNumber)	
					+ ",AccountNumber : " + System.Text.Encoding.UTF8.GetString (obj.AccountNumber)	
					+ ",BookType : " + IPAddress.HostToNetworkOrder (obj.BookType)	
					+ ",Buy_SellIndicator : " + IPAddress.HostToNetworkOrder (obj.Buy_SellIndicator)	
					+ ",DisclosedVolume : " + IPAddress.HostToNetworkOrder (obj.DisclosedVolume)	
					+ ",DisclosedVolumeRemaining : " + IPAddress.HostToNetworkOrder (obj.DisclosedVolumeRemaining)	
					+ ",TotalVolumeRemaining : " + IPAddress.HostToNetworkOrder (obj.TotalVolumeRemaining)	
					+ ",Volume : " + IPAddress.HostToNetworkOrder (obj.Volume)	
					+ ",VolumeFilledToday : " + IPAddress.HostToNetworkOrder (obj.VolumeFilledToday)	
					+ ",Price : " + IPAddress.HostToNetworkOrder (obj.Price)	
					+ ",GoodTillDate : " + IPAddress.HostToNetworkOrder (obj.GoodTillDate)	
					+ ",EntryDateTime : " + IPAddress.HostToNetworkOrder (obj.EntryDateTime)	
					+ ",LastModified : " + IPAddress.HostToNetworkOrder (obj.LastModified)	
					+ ",st_ord_flg_obj : " + obj.st_ord_flg_obj.STOrderFlagIn	
					+ ",st_ord_flg_obj : " + obj.st_ord_flg_obj.STOrderFlagOut	
					+ ",BranchId : " + IPAddress.HostToNetworkOrder (obj.BranchId)	
					+ ",TraderId : " + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ ",BrokerId : " + System.Text.Encoding.UTF8.GetString (obj.BrokerId)
					+ ",Open_Close : " + obj.Open_Close	
					+ ",Settlor : " + System.Text.Encoding.UTF8.GetString (obj.Settlor)	
					+ ",Pro_ClientIndicator : " + IPAddress.HostToNetworkOrder (obj.Pro_ClientIndicator)	
					+ ",obj_add_order_flg : " + obj.obj_add_order_flg.Reserved1	
					+ ",filler : " + IPAddress.HostToNetworkOrder (obj.filler)	
					+ ",nnffield : " + (long)DoubleEndianChange(obj.nnffield);
		}

		public string MS_TRADE_CONFIRM_TR_GetString (MS_TRADE_CONFIRM_TR obj)
		{			
			return 	"TransactionCode=" + IPAddress.HostToNetworkOrder (obj.TransactionCode)	
				+ ",LogTime=" + IPAddress.HostToNetworkOrder (obj.LogTime)	
					+ ",TraderId: " + IPAddress.HostToNetworkOrder (obj.TraderId)	
					+ ",Timestamp: " + (long)DoubleEndianChange(obj.Timestamp)	
					+ ",Timestamp1=" + (long)DoubleEndianChange(obj.Timestamp1)	
					+ ",Timestamp2=" + (long)DoubleEndianChange(obj.Timestamp2)	
					+ ",ResponseOrderNumber=" + (long)DoubleEndianChange (obj.ResponseOrderNumber)// IPAddress.HostToNetworkOrder ((long)obj.ResponseOrderNumber)	
					+ ",BrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId)		
					+ ",Reserved=" + obj.Reserved	
					+ ",AccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.AccountNumber)		
					+ ",Buy_SellIndicator=" + IPAddress.HostToNetworkOrder (obj.Buy_SellIndicator)	
					+ ",OriginalVolume=" + IPAddress.HostToNetworkOrder (obj.OriginalVolume)	
					+ ",DisclosedVolume=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolume)	
					+ ",RemainingVolume=" + IPAddress.HostToNetworkOrder (obj.RemainingVolume)	
					+ ",DisclosedVolumeRemaining=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolumeRemaining)	
					+ ",Price=" + IPAddress.HostToNetworkOrder (obj.Price)	
					+ ",GoodTillDate=" + IPAddress.HostToNetworkOrder (obj.GoodTillDate)
					+ ",FillNumber=" + IPAddress.HostToNetworkOrder (obj.FillNumber)	
					+ ",FillQuantity=" + IPAddress.HostToNetworkOrder (obj.FillQuantity)	
					+ ",FillPrice=" + IPAddress.HostToNetworkOrder (obj.FillPrice)	
					+ ",VolumeFilledToday=" + IPAddress.HostToNetworkOrder (obj.VolumeFilledToday)			
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagIn	
					+ ",st_ord_flg_obj=" + obj.st_ord_flg_obj.STOrderFlagOut	
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)	
					+ ",ActivityType=" + System.Text.Encoding.UTF8.GetString (obj.ActivityType)	
					+ ",Token=" + IPAddress.HostToNetworkOrder (obj.Token)	
					+ "," + CONTRACT_DESC_TR_GetString (obj.Contr_dec_tr_Obj)
					+ ",OpenClose=" + obj.OpenClose	
					+ ",BookType=" + obj.BookType	
					+ ",Participant=" + System.Text.Encoding.UTF8.GetString (obj.Participant)	
					+ ",obj_add_order_flg=" + obj.obj_add_order_flg.Reserved1;	
		}

		public string PRICE_VOL_MOD_GetString (PRICE_VOL_MOD obj)
		{	
			return "TokenNo : " + IPAddress.HostToNetworkOrder (obj.TokenNo)
				+ ",Trader_ID : " + IPAddress.HostToNetworkOrder (obj.Trader_ID)	
					+ ",OrderNumber : " + (long)DoubleEndianChange (obj.OrderNumber) //IPAddress.HostToNetworkOrder ((long)obj.OrderNumber)	
					+ ",Price : " + IPAddress.HostToNetworkOrder (obj.Price)
					+ ",Volume : " + IPAddress.HostToNetworkOrder (obj.Volume)	
					+ ",LastModified : " + IPAddress.HostToNetworkOrder (obj.LastModified)	
					+ ",Reference : " + System.Text.Encoding.UTF8.GetString (obj.Reference);
		}

		public string PORTFOLIO_DATA__GetString (PORTFOLIO_DATA obj)
		{	
			return "Portfolio :  " + System.Text.Encoding.UTF8.GetString (obj.Portfolio)
				+ ",Token : " + IPAddress.HostToNetworkOrder (obj.Token)	
					+ ",LastUpdtDtTime : " + IPAddress.HostToNetworkOrder (obj.LastUpdtDtTime)
					+ ",DeleteFlag : " + obj.DeleteFlag;
		}

	

		public string MS_SPD_OE_REQUEST_GetString(MS_SPD_OE_REQUEST obj)
		{
			return "AccountNumber1=" + System.Text.Encoding.UTF8.GetString (obj.AccountNumber1) 
					+ ",BookType1=" + IPAddress.HostToNetworkOrder (obj.BookType1)				
					+ ",BranchId1" + IPAddress.HostToNetworkOrder (obj.BranchId1) 
					+ ",BrokerId1=" + System.Text.Encoding.UTF8.GetString (obj.BrokerId1)					
					+ ",BuySell1=" + IPAddress.HostToNetworkOrder (obj.BuySell1) 
					+ ",CompetitorPeriod1=" + IPAddress.HostToNetworkOrder (obj.CompetitorPeriod1)					
					+ ",cOrdFiller=" + System.Text.Encoding.UTF8.GetString (obj.cOrdFiller) 
					+ ",DisclosedVol1=" + IPAddress.HostToNetworkOrder (obj.DisclosedVol1)					
					+ ",DisclosedVolRemaining1=" + IPAddress.HostToNetworkOrder (obj.DisclosedVolRemaining1) 
					+ ",EndAlpha1=" + System.Text.Encoding.UTF8.GetString (obj.EndAlpha1) 
					+ ",EntryDateTime1=" + IPAddress.HostToNetworkOrder (obj.EntryDateTime1)					
					+ ",filler1=" + IPAddress.HostToNetworkOrder (obj.filler1) 
					+ ",Filler=" + Convert.ToChar (obj.Filler1) 
					+ ",filler17=" + Convert.ToChar (obj.filler17)					
					+ ",filler18=" + Convert.ToChar (obj.filler18) 
					+ ",Filler9=" + Convert.ToChar (obj.Filler9) 
					+ ",FillerOptions1=" + System.Text.Encoding.UTF8.GetString (obj.FillerOptions1)					
					+ ",Fillerx1=" + Convert.ToChar (obj.Fillerx1) 
					+ ",Fillery1=" + Convert.ToChar (obj.Fillery1) 
					+ ",GiveupFlag1=" + Convert.ToChar (obj.GiveupFlag1)					
					+ ",GoodTillDate1=" + IPAddress.HostToNetworkOrder (obj.GoodTillDate1) 
					+ ",LastModified1=" + IPAddress.HostToNetworkOrder (obj.LastModified1)					
					+ ",leg2BuySell2=" + IPAddress.HostToNetworkOrder (obj.leg2.BuySell2) 
					+ ",leg2DisclosedVol2=" + IPAddress.HostToNetworkOrder (obj.leg2.DisclosedVol2)					
					+ ",leg2DisclosedVolRemaining2=" + IPAddress.HostToNetworkOrder (obj.leg2.DisclosedVolRemaining2) 
					+ ",leg2Fillerx2=" + Convert.ToChar (obj.leg2.Fillerx2)					
					+ ",leg2Fillery" + Convert.ToChar (obj.leg2.Fillery) 
					+ ",leg2.GiveUpFlage2=" + (obj.leg2.GiveUpFlage2) 
					+ ",leg2.MinFillAon2=" + IPAddress.HostToNetworkOrder (obj.leg2.MinFillAon2)					
					+ ",leg2.ms_oe_obj.CALevel=" + IPAddress.HostToNetworkOrder (obj.leg2.ms_oe_obj.CALevel) 
					+ ",leg2.ms_oe_obj.ExpiryDate=" + IPAddress.HostToNetworkOrder (obj.leg2.ms_oe_obj.ExpiryDate)					
					+ ",leg2.ms_oe_obj.InstrumentName=" + System.Text.Encoding.UTF8.GetString (obj.leg2.ms_oe_obj.InstrumentName) 
					+ ",leg2.ms_oe_obj.OptionType=" + System.Text.Encoding.UTF8.GetString (obj.leg2.ms_oe_obj.OptionType)					
					+ ",leg2.ms_oe_obj.StrikePrice=" + IPAddress.HostToNetworkOrder (obj.leg2.ms_oe_obj.StrikePrice) 
					+ ",leg2.ms_oe_obj.Symbol=" + System.Text.Encoding.UTF8.GetString (obj.leg2.ms_oe_obj.Symbol)					
					+ ",leg2.objadd.Reserved1=" + Convert.ToChar (obj.leg2.objadd.Reserved1) 
					+ ",obj.leg2.OpBrokerId2=" + System.Text.Encoding.UTF8.GetString (obj.leg2.OpBrokerId2)					
					+ ",leg2.OpenClose2=" + Convert.ToChar (obj.leg2.OpenClose2) 
					+ ",leg2.OrderType2=" + IPAddress.HostToNetworkOrder (obj.leg2.OrderType2) 
					+ ",leg2.Price2=" + IPAddress.HostToNetworkOrder (obj.leg2.Price2)					
					+ ",leg2.st_ord_flg_obj2.STOrderFlagIn=" +obj.leg2.st_ord_flg_obj2.STOrderFlagIn 
					+ ",leg2.st_ord_flg_obj2.STOrderFlagout=" + obj.leg2.st_ord_flg_obj2.STOrderFlagOut					
					+ ",obj.leg2.token=" + IPAddress.HostToNetworkOrder (obj.leg2.token) 
					+ ",leg2.TotalVolRemaining2=" + IPAddress.HostToNetworkOrder (obj.leg2.TotalVolRemaining2)					
					+ ",leg2.TriggerPrice2=" + IPAddress.HostToNetworkOrder (obj.leg2.TriggerPrice2) 
					+ ",leg2.Volume2=" + IPAddress.HostToNetworkOrder (obj.leg2.Volume2)					
					+ ",leg2.VolumeFilledToday2=" + IPAddress.HostToNetworkOrder (obj.leg2.VolumeFilledToday2) 
					+ ",MinFillAon1=" + IPAddress.HostToNetworkOrder (obj.MinFillAon1)					
					+ ",MktReplay=" + (long)DoubleEndianChange (obj.MktReplay) 
					+ ",ModCxlBy1=" + Convert.ToChar (obj.ModCxlBy1) 
					+ ",ms_oe_obj.CALevel=" + IPAddress.HostToNetworkOrder (obj.ms_oe_obj.CALevel)					
					+ ",ms_oe_obj.ExpiryDate=" + IPAddress.HostToNetworkOrder (obj.ms_oe_obj.ExpiryDate) 
					+ ",ms_oe_obj.InstrumentName=" + System.Text.Encoding.UTF8.GetString (obj.ms_oe_obj.InstrumentName)					
					+ ",ms_oe_obj.OptionType=" + System.Text.Encoding.UTF8.GetString (obj.ms_oe_obj.OptionType) 
					+ ",ms_oe_obj.StrikePrice=" + IPAddress.HostToNetworkOrder (obj.ms_oe_obj.StrikePrice)					
					+ ",ms_oe_obj.Symbol" + System.Text.Encoding.UTF8.GetString (obj.ms_oe_obj.Symbol) 
					+ ",NnfField=" + (long)DoubleEndianChange(obj.NnfField)					
					+ ",obj_add_order_flg.Reserved1=" + Convert.ToChar (obj.obj_add_order_flg.Reserved1) 
					+ ",OpBrokerId1=" + System.Text.Encoding.UTF8.GetString (obj.OpBrokerId1) 
					+ ",OpenClose1=" + Convert.ToChar (obj.OpenClose1)					
					+ ",OrderNumber1=" +(long)DoubleEndianChange(obj.OrderNumber1) 
					+ ",OrderType1=" + (long)DoubleEndianChange(obj.OrderType1)					
					+ ",ParticipantType1=" + Convert.ToChar (obj.ParticipantType1) 
					+ ",Price1=" + IPAddress.HostToNetworkOrder (obj.Price1) 
					+ ",PriceDiff=" + IPAddress.HostToNetworkOrder (obj.PriceDiff) 
					+ ",ProClient1=" + IPAddress.HostToNetworkOrder (obj.ProClient1)					
					+ ",ReasonCode1=" + IPAddress.HostToNetworkOrder (obj.ReasonCode1) 
					+ ",SettlementPeriod1=" + IPAddress.HostToNetworkOrder (obj.SettlementPeriod1) 
					+ ",Settlor1=" + System.Text.Encoding.UTF8.GetString (obj.Settlor1)					
					+ ",SolicitorPeriod1=" + IPAddress.HostToNetworkOrder (obj.SolicitorPeriod1) 
					+ ",StartAlpha1=" + System.Text.Encoding.UTF8.GetString (obj.StartAlpha1) 
					+ ",st_ord_flg_obj.STOrderFlagIn=" + obj.st_ord_flg_obj.STOrderFlagIn					
					+ ",st_ord_flg_obj.STOrderFlagout=" + obj.st_ord_flg_obj.STOrderFlagOut 
					+ ",Token1=" + IPAddress.HostToNetworkOrder (obj.Token1) 
					+ ",TotalVolRemaining1" + IPAddress.HostToNetworkOrder (obj.TotalVolRemaining1)					
					+ ",TraderId1=" + IPAddress.HostToNetworkOrder (obj.TraderId1) 
					+ ",TriggerPrice1=" + IPAddress.HostToNetworkOrder (obj.TriggerPrice1)					
					+ ",Volume1=" + IPAddress.HostToNetworkOrder (obj.Volume1) 
					+ ",VolumeFilledToday1=" + IPAddress.HostToNetworkOrder (obj.VolumeFilledToday1);
		}	



		public string MS_UPDATE_LOCAL_DATABASE_GetString (MS_UPDATE_LOCAL_DATABASE obj)
		{
			return	"LastUpdateSecurityTime=" + IPAddress.HostToNetworkOrder (obj.LastUpdateSecurityTime)
				+ ",LastUpdateParticipantTime=" + IPAddress.HostToNetworkOrder (obj.LastUpdateParticipantTime)
					+ ",LastUpdateInstrumentTime=" + IPAddress.HostToNetworkOrder (obj.LastUpdateInstrumentTime)
					+ ",LastUpdateIndexTime=" + IPAddress.HostToNetworkOrder (obj.LastUpdateIndexTime)

					+ ",RequestForOpenOrdersMessage=" + obj.RequestForOpenOrdersMessage
					+ ",Reserved=" + obj.Reserved
					+ "," + ST_MARKET_STATUS_GetString (obj.st_obj)
					+ "," + ST_EX_MARKET_STATUS_GetString (obj.st_pl_obj)
					+ "," + ST_PL_MARKET_STATUS_GetString (obj.st_ex_obj);


		}

		public string ST_MARKET_STATUS_GetString(ST_MARKET_STATUS obj)
		{
			return "st_obj.Normal=" + IPAddress.HostToNetworkOrder (obj.Normal)
				+ ",st_obj.Oddlot=" + IPAddress.HostToNetworkOrder (obj.Oddlot)
					+ ",st_obj.Spot=" + IPAddress.HostToNetworkOrder (obj.Spot)
					+ ",st_obj.Auction=" + IPAddress.HostToNetworkOrder (obj.Auction);
		}
		public string ST_EX_MARKET_STATUS_GetString(ST_EX_MARKET_STATUS obj)
		{
			return "st_obj.Normal=" + IPAddress.HostToNetworkOrder (obj.Normal)
				+ ",st_obj.Oddlot=" + IPAddress.HostToNetworkOrder (obj.Oddlot)
					+ ",st_obj.Spot=" + IPAddress.HostToNetworkOrder (obj.Spot)
					+ ",st_obj.Auction=" + IPAddress.HostToNetworkOrder (obj.Auction);
		}
		public string ST_PL_MARKET_STATUS_GetString(ST_PL_MARKET_STATUS obj)
		{
			return "st_obj.Normal=" + IPAddress.HostToNetworkOrder (obj.Normal)
				+ ",st_obj.Oddlot=" + IPAddress.HostToNetworkOrder (obj.Oddlot)
					+ ",st_obj.Spot=" + IPAddress.HostToNetworkOrder (obj.Spot)
					+ ",st_obj.Auction=" + IPAddress.HostToNetworkOrder (obj.Auction);
		}



		public string UPDATE_LDB_HEADER_GetString (UPDATE_LDB_HEADER obj)
		{
			return "Reserved1" + IPAddress.HostToNetworkOrder (obj.Reserved1);
		}

		public string MS_DOWNLOAD_INDEX_GetString(MS_DOWNLOAD_INDEX obj)
		{
			return "NoOfRecords=" + IPAddress.HostToNetworkOrder (obj.NoOfRecords)
				+"," + INDEX_DETAILS_GetString (obj.ind_obj,17);



		}
		public string INDEX_DETAILS_GetString(INDEX_DETAILS[] obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj[i].IndexName="
					+ System.Text.Encoding.UTF8.GetString (obj [i].IndexName)
						+ " obj[i].Token=" + obj [i].Token
						+ " obj[i].LastUpdateDateTime=" + IPAddress.HostToNetworkOrder (obj [i].LastUpdateDateTime)
						+"} ";
			}
			return str;
		}


		public string MS_SECURITY_STATUS_UPDATE_INFO_GetString(MS_SECURITY_STATUS_UPDATE_INFO obj)
		{
			return "NoOfRecords=" + IPAddress.HostToNetworkOrder (obj.NumberOfRecords)
				+"," + TOKEN_AND_ELIGIBILITY_GetString (obj.TOKEN_AND_ELIGIBILITY_obj,35);



		}
		public string TOKEN_AND_ELIGIBILITY_GetString(TOKEN_AND_ELIGIBILITY[] obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj["+i+"].Token="
					+ IPAddress.HostToNetworkOrder(obj [i].Token)
						+ " obj ["+i+"].ST_SEC_STATUS_PER_MARKET_obj=" +ST_SEC_STATUS_PER_MARKET_GetString(obj [i].ST_SEC_STATUS_PER_MARKET_obj,4)
						+"} ";
			}
			return str;
		}

		public string ST_SEC_STATUS_PER_MARKET_GetString(ST_SEC_STATUS_PER_MARKET[] obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj["+i+"].Status="
					+ IPAddress.HostToNetworkOrder (obj [i].Status)
						+"} ";
			}
			return str;
		}


		public string PARTICIPANT_UPDATE_INFO_GetString (PARTICIPANT_UPDATE_INFO obj)
		{
			return	"obj.Participant_Id=" + System.Text.Encoding.UTF8.GetString (obj.Participant_Id)
				+ ",obj.ParticipantName=" + System.Text.Encoding.UTF8.GetString(obj.ParticipantName)
					+ ",obj.ParticipantStatus=" + obj.ParticipantStatus
					+ ",LastUpdateIndexTime=" + IPAddress.HostToNetworkOrder (obj.ParticipantUpdateDateTime)
					+ ",obj.DeleteFlag=" + obj.DeleteFlag;
		}


		public string MS_INSTRUMENT_UPDATE_INFO_GetString (MS_INSTRUMENT_UPDATE_INFO obj)
		{
			return	"obj.InstrumentId=" + IPAddress.HostToNetworkOrder (obj.InstrumentId)
				+ ",obj.InstrumentName=" + obj.InstrumentName
					+ ",obj.InstrumentDescription=" + obj.InstrumentDescription
					+ ",obj.InstrumentUpdateDateTime=" + IPAddress.HostToNetworkOrder (obj.InstrumentUpdateDateTime)
					+ ",obj.DeleteFlag=" + obj.DeleteFlag;
		}




		public string MS_DOWNLOAD_INDEX_MAP_GetString (MS_DOWNLOAD_INDEX_MAP obj)
		{
			return	"obj.NoOfRecords=" + IPAddress.HostToNetworkOrder (obj.NoOfRecords)

				+ ",obj.DeleteFlag=" + BCAST_INDEX_MAP_DETAILS_GetString (obj.sIndicesMap, 10);
		}

		public string BCAST_INDEX_MAP_DETAILS_GetString(BCAST_INDEX_MAP_DETAILS[] obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj["+i+"].BcastName="
					+ System.Text.Encoding.UTF8.GetString(obj[i].BcastName)
						+"obj["+i+"].ChangedName="+System.Text.Encoding.UTF8.GetString(obj[i].ChangedName)
						+"obj["+i+"].DeleteFlag="+obj[i].DeleteFlag
						+"obj["+i+"].LastUpdateDateTime="+IPAddress.HostToNetworkOrder (obj[i].LastUpdateDateTime)
						+"} ";
			}
			return str;
		}


		public string EXCH_PORFOLIO_REQ_GetString(EXCH_PORFOLIO_REQ obj)
		{
			return "LastUpdateDtTime" + IPAddress.HostToNetworkOrder (obj.LastUpdateDtTime);
		}

		public string EXCH_PORTFOLIO_RESP_GetString(EXCH_PORTFOLIO_RESP obj)
		{
			return "obj.NoOfRecords"+IPAddress.HostToNetworkOrder (obj.NoOfRecords)
				+",obj.MoreRecords"+obj.MoreRecords
					+",obj.Filler"+obj.Filler
					+","+PORTFOLIO_DATA_GetString(obj.prt_data,15);

		}
		public string PORTFOLIO_DATA_GetString(PORTFOLIO_DATA[] obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj["+i+"].Portfolio="
					+ System.Text.Encoding.UTF8.GetString(obj[i].Portfolio)
						+"obj["+i+"].Token="+IPAddress.HostToNetworkOrder (obj[i].Token)
						+"obj["+i+"].LastUpdtDtTime="+IPAddress.HostToNetworkOrder (obj[i].LastUpdtDtTime)
						+"obj["+i+"].DeleteFlag="+obj[i].DeleteFlag
						+"} ";
			}
			return str;
		}





		public string MS_SECURITY_UPDATE_INFO_GetString(MS_SECURITY_UPDATE_INFO obj)
		{
			return "Token=" + IPAddress.HostToNetworkOrder (obj.Token)
				+","+SEC_INFO_GetString(obj.sec_info_obj)
					+",PermittedToTrade"+ IPAddress.HostToNetworkOrder (obj.PermittedToTrade)
					+",IssuedCapital"+ IPAddress.HostToNetworkOrder (obj.IssuedCapital)
					+",WarningQuantity"+ IPAddress.HostToNetworkOrder (obj.WarningQuantity)
					+",FreezeQuantity"+ IPAddress.HostToNetworkOrder (obj.FreezeQuantity)
					+",CreditRating"+ System.Text.Encoding.UTF8.GetString(obj.CreditRating)
					+","+ST_SEC_ELIGIBILITY_PER_MARKET_GetString(obj.ST_SEC_ELIGIBILITY_PER_MARKET_obj,4)
					+",IssueRate"+ IPAddress.HostToNetworkOrder (obj.IssueRate)
					+",IssueStartDate"+ IPAddress.HostToNetworkOrder (obj.IssueStartDate)
					+",InterestPaymentDate"+ IPAddress.HostToNetworkOrder (obj.InterestPaymentDate)
					+",IssueMaturityDate"+ IPAddress.HostToNetworkOrder (obj.IssueMaturityDate)
					+",MarginPercentage"+ IPAddress.HostToNetworkOrder (obj.MarginPercentage)
					+",MinimumLotQuantity"+ IPAddress.HostToNetworkOrder (obj.MinimumLotQuantity)
					+",BoardLotQuantity"+ IPAddress.HostToNetworkOrder (obj.BoardLotQuantity)
					+",TickSize"+ IPAddress.HostToNetworkOrder (obj.TickSize)
					+",Name"+ System.Text.Encoding.UTF8.GetString(obj.Name)
					+",Reserved"+obj.Reserved
					+",ListingDate"+ IPAddress.HostToNetworkOrder (obj.ListingDate)
					+",IssueRate"+ IPAddress.HostToNetworkOrder (obj.IssueRate)
					+",ExpulsionDate"+ IPAddress.HostToNetworkOrder (obj.ExpulsionDate)
					+",ReAdmissionDate"+ IPAddress.HostToNetworkOrder (obj.ReAdmissionDate)
					+",LowPriceRange"+ IPAddress.HostToNetworkOrder (obj.LowPriceRange)
					+",HighPriceRange"+ IPAddress.HostToNetworkOrder (obj.HighPriceRange)
					+",ExpiryDate"+ IPAddress.HostToNetworkOrder (obj.ExpiryDate)
					+",NoDeliveryStartDate"+ IPAddress.HostToNetworkOrder (obj.NoDeliveryStartDate)
					+",NoDeliveryEndDate"+ IPAddress.HostToNetworkOrder (obj.NoDeliveryEndDate)
					+",ST_STOCK_ELIGIBLE_INDICATORS_obj.Reserved"+obj.ST_STOCK_ELIGIBLE_INDICATORS_obj.Reserved
					+",ST_STOCK_ELIGIBLE_INDICATORS_obj.Reserved2"+obj.ST_STOCK_ELIGIBLE_INDICATORS_obj.Reserved2
					+",BookClosureStartDate"+ IPAddress.HostToNetworkOrder (obj.BookClosureStartDate)
					+",BookClosureEndDate"+ IPAddress.HostToNetworkOrder (obj.BookClosureEndDate)
					+",ExerciseStartDate"+ IPAddress.HostToNetworkOrder (obj.ExerciseStartDate)
					+",ExerciseEndDate"+ IPAddress.HostToNetworkOrder (obj.ExerciseEndDate)
					+",OldToken"+ IPAddress.HostToNetworkOrder (obj.OldToken)
					+",AssetInstrument"+ System.Text.Encoding.UTF8.GetString(obj.AssetInstrument)
					+",AssetName"+ System.Text.Encoding.UTF8.GetString(obj.AssetName)
					+",AssetToken"+ IPAddress.HostToNetworkOrder (obj.AssetToken)
					+",IntrinsicValue"+ IPAddress.HostToNetworkOrder (obj.IntrinsicValue)
					+",ExtrinsicValue"+ IPAddress.HostToNetworkOrder (obj.ExtrinsicValue)
					+",ST_PURPOSE_obj.reserved1"+ obj.ST_PURPOSE_obj.reserved1
					+",ST_PURPOSE_obj.reserved2"+ obj.ST_PURPOSE_obj.reserved2
					+",LocalUpdateDateTime"+ IPAddress.HostToNetworkOrder (obj.LocalUpdateDateTime)
					+",DeleteFlag"+ obj.DeleteFlag
					+",Remark"+ System.Text.Encoding.UTF8.GetString(obj.Remark)
					+",BasePrice"+ IPAddress.HostToNetworkOrder (obj.BasePrice)
					;

		}





		public string SEC_INFO_GetString(SEC_INFO obj)
		{
			return "InstrumentName=" +System.Text.Encoding.UTF8.GetString(obj.InstrumentName)
				+",Symbol="+System.Text.Encoding.UTF8.GetString(obj.Symbol)
					+",Series="+System.Text.Encoding.UTF8.GetString(obj.Series)
					+",ExpiryDate="+IPAddress.HostToNetworkOrder(obj.ExpiryDate)
					+",StrikePrice"+IPAddress.HostToNetworkOrder(obj.StrikePrice)
					+",OptionType="+System.Text.Encoding.UTF8.GetString(obj.OptionType)
					+",CALevel="+IPAddress.HostToNetworkOrder(obj.CALevel)
					;
		}


		public string ST_SEC_ELIGIBILITY_PER_MARKET_GetString(ST_SEC_ELIGIBILITY_PER_MARKET[]obj,int j)
		{
			string str="";
			for (int i=0; i<j; i++) 
			{
				str = "{ obj["+i+"].Status="
					+ IPAddress.HostToNetworkOrder (obj [i].status)
						+"{ obj["+i+"].Reserved="
						+ IPAddress.HostToNetworkOrder (obj [i].Reserved)+"} ";
			}
			return str;
		}

		public string MS_SYSTEM_INFO_REQ_GetString(MS_SYSTEM_INFO_REQ obj)
		{
			return "LastUpdatePortfolioTime=" + IPAddress.HostToNetworkOrder(obj.LastUpdatePortfolioTime);
		}

		public string MS_MESSAGE_DOWNLOAD_GetString(MS_MESSAGE_DOWNLOAD obj)
		{
			return "SequenceNumber"+(long)DoubleEndianChange (obj.SequenceNumber);
		}


	
		public string SIGNOFF_OUT_GetString(SIGNOFF_OUT obj)
		{
			return "Reserved="+System.Text.Encoding.UTF8.GetString(obj.Reserved)
				+",userid="+IPAddress.HostToNetworkOrder(obj.userid);
		}


	
		public string MS_TRADE_INQ_DATA_GetString(MS_TRADE_INQ_DATA obj)
		{
			return "TokenNo=" + IPAddress.HostToNetworkOrder (obj.TokenNo)
				+ "," + CONTRACT_DESC_GetString (obj.ms_oe_obj)
					+ ",FillNumber=" + IPAddress.HostToNetworkOrder (obj.FillNumber)
					+ ",FillQuantity=" + IPAddress.HostToNetworkOrder (obj.FillQuantity)
					+ ",FillPrice=" + IPAddress.HostToNetworkOrder (obj.FillPrice)
					+ "MktType=" + obj.MktType
					+ "BuyOpenClose=" + obj.BuyOpenClose
					+ ",NewVolume=" + IPAddress.HostToNetworkOrder (obj.NewVolume)
					+ ",BuyBrokerId=" + System.Text.Encoding.UTF8.GetString (obj.BuyBrokerId)	
					+ ",SellBrokerId=" + System.Text.Encoding.UTF8.GetString (obj.SellBrokerId)	
					+ ",TraderId=" + IPAddress.HostToNetworkOrder (obj.TraderId)
					+ "RequestedBy=" + obj.RequestedBy
					+ "SellOpenClose=" + obj.SellOpenClose
					+ ",BuyAccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.BuyAccountNumber)	
					+ ",SellAccountNumber=" + System.Text.Encoding.UTF8.GetString (obj.SellAccountNumber)	
					+ ",BuyParticipant=" + System.Text.Encoding.UTF8.GetString (obj.BuyParticipant)	
					+ ",SellParticipant=" + System.Text.Encoding.UTF8.GetString (obj.SellParticipant)	
					+ ",ReservedFiller=" + System.Text.Encoding.UTF8.GetString (obj.ReservedFiller)	
					+ "BuyGiveupFlag=" + obj.BuyGiveupFlag
					+ "SellGiveupFlag=" + obj.SellGiveupFlag;

		}



	}


	#endregion

	#region Enums
public class Enums
	{

	#region Transaction Code
	public enum Transaction_Code
	{
		INVITATION_MESSAGE=15000,
		SYSTEM_INFORMATION_IN=1600,
		SYSTEM_INFORMATION_OUT=1601,
		EXCH_PORTF_IN=1775,
		EXCH_PORTF_OUT=1776,
		RPRT_MARKET_STATS_OUT_RPT=1833,
		BOARD_LOT_IN=2000,
		NEG_ORDER_TO_BL=2008,
		NEG_ORDER_BY_CPID=2009,
		ORDER_MOD_IN=2040,
		ORDER_MOD_REJECT=2042,
		ORDER_CANCEL_IN=2070,
		ORDER_CANCEL_REJECT=2072,
		ORDER_CONFIRMATION=2073,
		ORDER_MOD_CONFIRMATION=2074,
		ORDER_CANCEL_CONFIRMATION=2075,
		CANCEL_NEG_ORDER=2076,
		PRICE_MOD_IN=27013,
		SP_BOARD_LOT_IN=2100,
		TWOL_BOARD_LOT_IN=2102,
		THRL_BOARD_LOT_IN=2104,
		SP_ORDER_CANCEL_IN=2106,
		SP_ORDER_MOD_IN=2118,
		SP_ORDER_CONFIRMATION=2124,
		TWOL_ORDER_CONFIRMATION=2125,
		THRL_ORDER_CONFIRMATION=2126,
		SP_ORDER_CXL_REJ_OUT=2127,
		SP_ORDER_CXL_CONFIRMATION=2130,
		TWOL_ORDER_CXL_CONFIRMATION=2131,
		THRL_ORDER_CXL_CONFIRMATION=2132,
		SP_ORDER_MOD_REJ_OUT=2133,
		SP_ORDER_MOD_CON_OUT=2136,
		TWOL_ORDER_ERROR=2155,
		THRL_ORDER_ERROR=2156,
		FREEZE_TO_CONTROL=2170,
		ON_STOP_NOTIFICATION=2212,
		TRADE_CONFIRMATION=2222,
		TRADE_ERROR=2223,
		ORDER_ERROR=2231,
		TRADE_CANCEL_CONFIRM=2282,
		TRADE_CANCEL_REJECT=2286,
		TRADE_MODIFY_CONFIRM=2287,
		TRADE_MODIFY_REJECT=2288,
		SIGN_ON_REQUEST_IN=2300,
		SIGN_ON_REQUEST_OUT=2301,
		SIGN_OFF_REQUEST_OUT=2321,
		EX_PL_ENTRY_IN=4000,
		EX_PL_ENTRY_OUT=4001,
		EX_PL_CONFIRMATION=4002,
		EX_PL_MOD_IN=4005,
		EX_PL_MOD_CONFIRMATION=4007,
		EX_PL_CXL_IN=4008,
		EX_PL_CXL_OUT=4009,
		EX_PL_CXL_CONFIRMATION=4010,
		GIVEUP_APPROVED_IN=4500,
		GIVEUP_APPROVED_OUT=4501,
		GIVEUP_APP_CONFIRM=4502,
		GIVEUP_REJECTED_IN=4503,
		GIVEUP_REJECTED_OUT=4504,
		GIVEUP_REJ_CONFIRM=4505,
		GIVEUP_APPROVE_ALL=4513,
		CTRL_MSG_TO_TRADER=5295,
		TRADE_CANCEL_IN=5440,
		TRADE_CANCEL_OUT=5441,
		TRADE_MOD_IN=5445,
		SECURITY_OPEN_PRICE=6013,
		BCAST_JRNL_VCT_MSG=6501,
		BC_OPEN_MESSAGE=6511,
		BC_CLOSE_MESSAGE=6521,
		BC_PREOPEN_SHUTDOWN_MSG=6531,
		BC_CIRCUIT_CHECK=6541,
		BC_NORMAL_MKT_PREOPEN_ENDED=6571,
		DOWNLOAD_REQUEST=7000,
		HEADER_RECORD=7011,
		MESSAGE_RECORD=7021,
		TRAILER_RECORD=7031,
		MKT_MVMT_CM_OI_IN=7130,
		BCAST_MBO_MBP_UPDATE=7200,
		BCAST_MW_ROUND_ROBIN=7201,
		BCAST_TICKER_AND_MKT_INDEX=7202,
		BCAST_INDUSTRY_INDEX_UPDATE=7203,
		BCAST_SYSTEM_INFORMATION_OUT=7206,
		BCAST_ONLY_MBP=7208,
		BCAST_SECURITY_STATUS_CHG_PREOPEN=7210,
		BCAST_SPD_MBP_DELTA=7211,
		UPDATE_LOCALDB_IN=7300,
		UPDATE_LOCALDB_DATA=7304,
		BCAST_SECURITY_MSTR_CHG=7305,
		BCAST_PART_MSTR_CHG=7306,
		UPDATE_LOCALDB_HEADER=7307,
		UPDATE_LOCALDB_TRAILER=7308,
		BCAST_SECURITY_STATUS_CHG=7320,
		PARTIAL_SYSTEM_INFORMATION=7321,
		BCAST_INSTR_MSTR_CHG=7324,
		BCAST_INDEX_MSTR_CHG=7325,
		BCAST_INDEX_MAP_PRICE=7326,
		BATCH_ORDER_CANCEL=9002,
		BCAST_TURNOVER_EXCEEDED=9010,
		BROADCAST_BROKER_REACTIVATED=9011,
		BOARD_LOT_IN_TR=20000,
		ORDER_MOD_IN_TR=20040,
		ORDER_CANCEL_IN_TR=20070,
		ORDER_QUICK_CANCEL_IN_TR=20060,
		ORDER_MOD_REJECT_TR=20042,
		ORDER_CANCEL_REJECT_TR=20072,
		ORDER_CONFIRMATION_TR=20073,
		ORDER_MOD_CONFIRMATION_TR=20074,
		ORDER_CXL_CONFIRMATION_TR=20075,
		ORDER_ERROR_TR=20231,
		PRICE_CONFIRMATION_TR=20012,
		TRADE_CONFIRMATION_TR=20222,
	}
	#endregion Transaction Code

	#region ReasonCode
	public enum ReasonCode
	{
		Exercise			 =	2	,
		Position_liquidation			 =	3	,
		Expl_Security			 =	4	,
		Security			 =	5	,
		Broker			 =	6	,
		Branch			 =	7	,
		User			 =	8	,
		Participant			 =	9	,
		Counter_Party			 =	10	,
		Order_Number			 =	11	,
		Auction_Number			 =	15	,
		Price_Freeze			 =	17	,
		Quantity_Freeze			 =	18	,
		Contract			 =	20	,
		Invalid_EXPL			 =	29	,
		Exercise_Mode_Mismatch			 =	30	,
		EX_PL_Number			 =	31	,

	}
	#endregion ReasonCode


	#region List of Error Codes
	public enum Error_Codes
	{
		DealerId_Desabled	 =	16134	,
		Invalid_instrument_type	 =	293	,
		Order_does_not_exit	 =	509	,
		Initiator_is_not_allowed_to_cancel_auction_order	 =	8049	,
		Auction_number_does_not_exit	 =	8485	,
		The_trading_system_is_not_available_for_trading	 =	16000	,
		Header_user_ID_is_not_equal_to_user_ID_in_the_order_packet	 =	16001	,
		System_error_was_encountered_Please_call_the_Exchange	 =	16003	,
		The_user_is_already_signed_on	 =	16004	,
		System_error_while_trying_to_signoff_Please_call_the_Exchange	 =	16005	,
		Invalid_sign_on_Please_try_again	 =	16006	,
		Signing_onto_the_trading_system_is_restricted_Please_try_later_on	 =	16007	,
		Invalid_contract_descriptor	 =	16012	,
		This_order_is_not_yours	 =	16014	,
		This_trade_is_not_yours	 =	16015	,
		Invalid_trade_number	 =	16016	,
		Stock_not_found	 =	16019	,
		Security_is_unavailable_for_trading_at_this_time_Please_try_later	 =	16035	,
		Trading_member_does_not_exit_in_the_system	 =	16041	,
		Dealer_does_not_exist_in_the_system	 =	16042	,
		This_record_already_exits_on_the_NEAT_system	 =	16043	,
		Order_has_been_modified_Please_try_again	 =	16044	,
		Stock_is_suspended	 =	16049	,
		Function_Not_Available	 =	16052	,
		Your_password_has_expired_must_be_changed	 =	16053	,
		Invalid_branch_for_trading_member	 =	16054	,
		Program_error	 =	16056	,
		Duplicate_trade_cancel_request	 =	16086	,
		Invalid_trader_ID_for_buyer_AS_BUYER_USER_ID	 =	16098	,
		Invalid_trader_ID_for_buyer_AS_SELLER_USER_ID	 =	16099	,
		Your_system_version_has_not_been_updated	 =	16100	,
		System_could_not_complete_your_transaction_Admin_notified	 =	16104	,
		Invalid_Dealer_ID_entered	 =	16148	,
		Invalid_Trader_ID_entered	 =	16154	,
		Order_priced_ATO_cannot_be_entered_when_a_security_is_open	 =	16169	,
		Duplicate_modification_or_cancellation_request_for_the_same_trade_has_been_encountered	 =	16198	,
		Only_market_orders_are_allowed_in_postclose	 =	16227	,
		SL,_MIT_or_NT_orders_are_not_allowed_during_Post_Close	 =	16228	,
		GTC_GTD_or_GTDays_orders_are_Not_Allowed_during_Post_Close	 =	16229	,
		Continuous_session_orders_cannot_be_modified	 =	16230	,
		Continuous_session_trades_cannot_be_changed	 =	16231	,
		Proprietary_requests_cannot_be_made_for_participant	 =	16233	,
		Invalid_Price	 =	16247	,
		Trade_modification_with_different_quantities_is_received	 =	16251	,
		Record_does_not_exit	 =	16273	,
		The_markets_have_not_been_opened_for_trading	 =	16278	,
		The_contract_has_not_yet_been_admitted_for_trading	 =	16279	,
		The_Contract_has_matured	 =	16280	,
		The_security_has_been_expelled	 =	16281	,
		The_order_quantity_is_greater_than_the_issued_capital	 =	16282	,
		The_order_price_is_not_multiple_of_the_tick_size	 =	16283	,
		The_order_price_is_out_of_the_days_price_range	 =	16284	,
		The_broker_is_not_active	 =	16285	,
		System_is_in_a_wrong_state_to_make_the_requested_change	 =	16300	,
		The_auction_is_pending	 =	16303	,
		The_order_has_been_canceled_due_to_quantity_freeze	 =	16307	,
		The_order_has_been_canceled_due_to_price_freeze	 =	16308	,
		The_Solicitor_period_for_the_Auction_is_over	 =	16311	,
		The_Competitor_period_for_the_Auction_is_over	 =	16312	,
		The_Auction_period_will_cross_market_close_time	 =	16313	,
		The_limit_price_is_worse_than_the_trigger_price	 =	16315	,
		The_trigger_price_is_not_a_multiple_of_tick_size	 =	16316	,
		AON_attribute_not_allowed	 =	16317	,
		MF_attribute_not_allowed	 =	16318	,
		AON_attribute_not_allowed_at_Security_level	 =	16319	,
		MF_attribute_not_allowed_at_security_level	 =	16320	,
		MF_quantity_is_greater_than_disclosed_quantity	 =	16321	,
		MF_quantity_is_not_a_multiple_of_regular_lot	 =	16322	,
		MF_quantity_is_greater_than_Original_quantity	 =	16323	,
		Disclosed_quantity_is_greater_than_Original_quantity	 =	16324	,
		Disclosed_quantity_is_not_a_multiple_of_regular_lot	 =	16325	,
		GTD_is_greater_than_that_specified_at_the_trading_system	 =	16326	,
		Odd_lot_quantity_cannot_be_greater_than_or_equal_to_regular_lot_size	 =	16327	,
		Quantity_is_not_a_multiple_of_regular_lot	 =	16328	,
		Trading_Member_not_permitted_in_the_market	 =	16329	,
		Security_is_suspended	 =	16330	,
		Branch_Order_Value_Limit_has_been_exceeded	 =	16333	,
		The_order_to_be_cancelled_has_changed	 =	16343	,
		The_order_cannot_be_cancelled	 =	16344	,
		Initiator_order_cannot_be_cancelled	 =	16345	,
		OE_Order_cannot_be_modified	 =	16346	,
		Trading_is_not_allowed_in_this_market	 =	16348	,
		Control_has_rejected_the_Negotiated_Trade	 =	16357	,
		Status_is_in_the_required_state	 =	16363	,
		Contract_is_in_preopen	 =	16369	,
		Order_entry_not_allowed_for_user_as_it_is_of_inquiry_type	 =	16372	,
		Contract_not_allowed_to_trader_in	 =	16387	,
		Order_Cancelled_By_System	 =	16388	,
		Turnover_limit_not_provided_Please_contact_Exchange	 =	16392	,
		DQ_is_less_than_minimum_quantity_allowed	 =	16400	,
		Order_has_been_cancelled_due_to_freeze_admin_suspension	 =	16404	,
		BUY__SELL_type_entered_is_invalid	 =	16405	,
		BOOK_type_entered_is_invalid	 =	16406	,
		trigger_price_entered_has_invalid_characters	 =	16408	,
		Pro_Client_should_be_either_1_client_or_2_broker	 =	16414	,
		Invalid_combination_of_book_type_and_instructions_order_type	 =	16415	,
		Invalid_combination_of_mf_aon_disclosed_volume	 =	16416	,
		This_error_code_will_be_returned_for_invalid_data_in_the_order_packet	 =	16419	,
		GTD_is_greater_than_Maturity_date	 =	16440	,
		DQ_orders_are_not_allowed_in_preopen	 =	16441	,
		ST_orders_are_not_allowed_in_preopen	 =	16442	,
		Order_value_exceeds_the_order_limit_value	 =	16443	,
		Stop_Loss_orders_are_not_allowed	 =	16445	,
		Market_If_Touched_orders_are_not_allowed	 =	16446	,
		Order_entry_not_allowed_in_Pre_open	 =	16447	,
		Ex_Pl_not_allowed	 =	16500	,
		Invalid_ExPl_flag_value	 =	16501	,
		Ex_Pl_rejection_not_allowed	 =	16513	,
		Not_modifiable	 =	16514	,
		Clearing_member,_Trading_Member_link_not_found	 =	16518	,
		Not_a_clearing_member	 =	16521	,
		User_in_not_a_corporate_manager	 =	16523	,
		Clearing_member_Participant_link_not_found	 =	16532	,
		Enter_either_TM_or_Participant	 =	16533	,
		Participant_is_invalid	 =	16541	,
		Trade_cannot_be_modified__cancelled_It_has_already_been_approved_by_CM	 =	16550	,
		Stock_has_been_suspended	 =	16552	,
		Trading_Member_not_permitted_in_Futures	 =	16554	,
		Trading_Member_not_permitted_in_Options	 =	16555	,
		Quantity_less_than_the_minimum_lot_size	 =	16556	,
		Disclose_quantity_less_than_the_minimum_lot_size	 =	16557	,
		Minimum_fill_is_less_than_the_minimum_lot_size	 =	16558	,
		The_give_up_trade_has_already_been_rejected	 =	16560	,
		Negotiated_Orders_not_allowed	 =	16561	,
		Negotiated_Trade_not_allowed	 =	16562	,
		User_does_not_belong_to_Broker_or_Branch	 =	16566	,
		The_market_is_in_post_close	 =	16570	,
		The_Closing_Session_has_ended	 =	16571	,
		Closing_Session_trades_have_been_generated	 =	16572	,
		Message_length_is_invalid	 =	16573	,
		Open__Close_type_entered_is_invalid	 =	16574	,
		No_of_nnf_inquiry_requests_exceeded	 =	16576	,
		Both_participant_and_volume_changed	 =	16577	,
		Cover__Uncover_type_entered_is_invalid	 =	16578	,
		Giveup_requested_for_wrong_CM_ID	 =	16579	,
		Order_does_not_belong_to_the_given_participant	 =	16580	,
		Invalid_trade_price	 =	16581	,
		For_Pro_order_participant_entry_not_allowed	 =	16583	,
		Not_a_valid_account_number	 =	16585	,
		Participant_Order_Entry_Not_Allowed	 =	16586	,
		All_continuous_session_orders_are_being_deleted_now	 =	16589	,
		After_giveup_approve_reject_trade_quantity_cannot_be_modified	 =	16594	,
		Trading_member_cannot_put_Ex_Pl_request_for_Participant	 =	16596	,
		Branch_limit_should_be_greater_than_sum_of_user_limits	 =	16597	,
		Branch_limit_should_be_greater_than_used_limit	 =	16598	,
		Dealer_value_limit_exceeds_the_set_limit	 =	16602	,
		Participant_not_found	 =	16604	,
		One_leg_of_spread_2L_failed	 =	16605	,
		Quantity_greater_than_Freeze_quantity	 =	16606	,
		Spread_not_allowed	 =	16607	,
		Spread_allowed_only_when_market_is_open	 =	16608	,
		Spread_allowed_only_when_stock_is_open	 =	16609	,
		Both_legs_should_have_same_quantity	 =	16610	,
		Modified_order_qty_freeze_not_allowed	 =	16611	,
		The_trade_record_has_been_modified	 =	16612	,
		eStm_Order_cannot_be_modified	 =	16615	,
		Order_can_not_be_cancelled	 =	16616	,
		Trade_can_not_be_manipulated	 =	16617	,
		PCM_can_not_ex_pl_for_himself	 =	16619	,
		Ex_Pl_by_clearing_member_for_TM_not_allowed	 =	16620	,
		Clearing_member_cannot_change_the_Ex_Pl_requests_placed_by_Trading_Member	 =	16621	,
		Clearing_member_is_suspended	 =	16625	,
		Expiry_date_not_in_ascending_order	 =	16626	,
		Invalid_contract_combination	 =	16627	,
		Branch_manager_cannot_cancel_corporate_managers_order	 =	16628	,
		Branch_manager_cannot_cancel_other_branch_managers_order	 =	16629	,
		Corporate_manager_cannot_cancel_other_corporate_managers_order	 =	16630	,
		Spread_not_allowed_for_different_underlying	 =	16631	,
		Cli_Ac_number_cannot_be_modified_as_trading_member_ID	 =	16632	,
		Futures_buy_branch_Order_Value_Limit_has_been_exceeded	 =	16636	,
		Futures_sell_branch_Order_Value_Limit_has_been_exceeded	 =	16637	,
		Options_buy_branch_Order_Value_Limit_has_been_exceeded	 =	16638	,
		Options_sell_branch_Order_Value_Limit_has_been_exceeded	 =	16639	,
		Futures_buy_used_limit_execeeded_the_user_limit	 =	16640	,
		Futures_sell_used_limit_execeeded_the_user_limit	 =	16641	,
		Options_buy_used_limit_execeeded_the_user_limit	 =	16642	,
		Options_sell_used_limit_execeeded_the_user_limit	 =	16643	,
		Cannot_approve_Bhav_Copy_generated	 =	16645	,
		Cannot_modify	 =	16646	,
		No_address_in_the_database	 =	16656	,
		Contract_is_opening_Please_wait_for_the_contract_to_open	 =	16662	,
		Invalid_NNF_field	 =	16666	,
		GTC_GTD_Orders_not_allowed	 =	16667	,
		This_error_code_will_be_returned_if_Close_out_order_rejected_by_the_system	 =	16686	,
		This_error_code_will_be_returned_if_the_close_out_order_entered_is_going_into_freeze_Since_Freeze_is_not_allowed_for_close_out_orders	 =	16687	,
		This_error_code_will_be_returned_if_the_close_out_order_is_not_allowed_in_the_system	 =	16688	,
		This_error_code_will_be_returned_when_a_Trade_MOD_request_is_placed_by_a_broker_in_Close_out	 =	16690	,
		Cancelled_by_the_System	 =	16706	,
		System_Error_Orders_not_completely_cancelled_by_the_system_Please_request_Quick_CXL_again	 =	16708	,
		Price_difference_is_beyond_Operating_Range	 =	16713	,
		eSvc_Order_Entered_has_invalid_data	 =	16793	,
		eSssd_Order_Entered_has_invalid_data	 =	16794	,
		Order_cancelled_mdue_to_voluntary_close_out	 =	16795	,
		Order_cancelled_due_to_OI_violation	 =	16796	,
		Bulk_Order_rejected_due_to_price_freeze	 =	16803	,
		Bulk_Order_rejected_due_to_quantity_Freeze	 =	16804	,
		Trader_not_eligible_for_Bulk_Order	 =	16805	,
		Trader_allowed_to_enter_only_Bulk_Order	 =	16806	,
		The_Account_is_Debarred_by_Exchange	 =	16807	,
		Client_code_Participant_modification_not_allowed	 =	17039	,
		Order_Quantity_Exceeds_Quantity_Value_Limit_for_User	 =	17045	,
		Trade_Modification_Not_Allowed_for_User	 =	17048	,
		_The_Price_is_out_of_the_current_execution_range	 =	17070	,
	}
	#endregion List of Error Codes

	#region MarketTypes
	public enum MarketTypes
	{
		Normal_Market	 =	1	,
		Odd_Lot_Market_Not_used	 =	2	,
		Spot_Market_Not_used	 =	3	,
		Auction_Market_Not_used	 =	4	,
	}
	#endregion MarketTypes

	#region MarketStatus
	public enum MarketStatus
	{
		PreOpen_onlyForNormalMarket	 =	0	,
		Open	 =	1	,
		Closed	 =	2	,
		PreOpen_Ended	 =	3	,
		Postclose	 =	4	
	}
	#endregion MarketStatus

	#region BookTypes
	public enum BookTypes
	{
		Regular_lot_order	 =	1	,
		Special_terms_order	 =	2	,
		Stop_loss_MIT_order	 =	3	,
		Negotiated_order_NotUsed	 =	4	,
		Odd_lot_order_NotUsed	 =	5	,
		Spot_order_NotUsed	 =	6	,
		Auction_order_NotUsed	 =	7	

	}
	#endregion BookTypes

	#region SecurityStatus
	public enum SecurityStatus
	{
		Preopen	 =	1	,
		Open	 =	2	,
		Suspended	 =	3	,
		Preopen_Extended	 =	4	,
		Open_With_Market	 =	5	,

	}
	#endregion SecurityStatus

	#region ActivityTypes
	public enum ActivityTypes
	{
		ORIGINAL_ORDER_When_the_order_is_entered_it_is_taken_as_original_order_GTC_GTD_orders_still_in_the_book_also_come_with_this_activity_type	 =	1	,
		ACTIVITY_TRADE_The_trade_done	 =	2	,
		ACTIVITY_ORDER_CXL_The_order_is_cancelled	 =	3	,
		ACTIVITY_ORDER_MOD_The_order_is_modified	 =	4	,
		ACTIVITY_TRADE_MOD_The_trade_is_modified	 =	5	,
		ACTIVITY_TRADE_CXL_1_The_trade_cancellation_was_requested	 =	6	,
		ACTIVITY_TRADE_CXL_2_Action_has_been_taken_on_this_request	 =	7	,
		ACTIVITY_BATCH_ORDER_CXL_At_the_end_of_the_day_all_un_traded_Day_orders_are_cancelled_GTC_GTD_orders_due_for_cancellation_are_also_cancelled =	8	,
		ACTIVITY_ORDER_MOD_REJECT_When_the_order_modification_is_rejected	 =	9	,
		ACTIVITY_TRADE_MOD_REJECT_When_the_trade_modification_is_rejected	 =	10	,
		ACTIVITY_TRADE_CXL_REJECT_When_the_trade_cancellation_is_rejected	 =	11	,
		ACTIVITY_ORDER_REJECTED_When_the_order_entry_is_rejected	 =	12	,
		ACTIVITY_ORDER_IN_BOOK_ACTIVITY_ORDER_IN_BOOK	 =	13	,
		ACTIVITY_ORDER_CXL_REJECT_When_order_cancel_requested,_gets_rejected	 =	14	,
		ACTIVITY_PRICE_FREEZE_IN_Order_entered,_caused_price_freeze	 =	15	,
		ACTIVITY_PRICE_FREEZE_CXLD_Order_in_price_freeze_is_canceled_from_CWS	 =	16	,
		ACTIVITY_FREEZE_ADMIN_SUSP_Order_is_rejected_through_Adimin_suspension_when_Quantity_is_freezed	 =	17	,
		ACTIVITY_QTY_FREEZE_IN_Order_entered,_caused_quantity_freeze	 =	18	,
		ACTIVITY_QTY_FREEZE_CXLD_Order_in_quantity_freeze_is_canceled_from_CWS	 =	19	,
		ACTIVITY_ORD_BROKER_SUSP_When_order_is_canceled_because_of_broker_suspension	 =	20	,
		ACTIVITY_SPREAD_TRADE_CXL_When_spread_trade_is_cancelled	 =	43
	}
	#endregion ActivityTypes

}
	public class ReadOnlyEventArgs<T> : EventArgs
	{
		public ReadOnlyEventArgs(T input)
		{
			Parameter = input;
		}

		public T Parameter { get; private set; }
	}

	public static class EventHandlerExtensions
	{
		public static ReadOnlyEventArgs<T> CreateReadOnlyArgs<T>(this EventHandler<ReadOnlyEventArgs<T>> handler,
		                                                         T input)
		{
			return new ReadOnlyEventArgs<T>(input);
		}
	}

	public static class StaticExtension
	{
		public static void Raise<TEventArgs>(this EventHandler<TEventArgs> myEvent, object sender, TEventArgs e)
			where TEventArgs : EventArgs
		{
			if (myEvent != null)

				myEvent(sender, e);
		}
	}

	
	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct FOPAIR
	{
		public int PORTFOLIONAME;
		public int TokenNear;
		public int TokenFar;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct FOPAIRDIFF
	{
		public int PORTFOLIONAME;
		public int TokenNear;
		public int TokenFar;
		public double BNSFDIFF;
		public double BFSNDIFF;
		public int BNSFMNQ;
		public int BFSNMNQ;
		public int BNSFMXQ;
		public int BFSNMXQ;
		public int Divisor;
		public int TickCount;

	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct FOPAIRLEG2
	{
		public int PORTFOLIONAME;
		public int Token1;
		public int Token2;
		public int Token3;
		public int Token4;
		public short Token1side;
		public short Token2side;
		public short Token3side;
		public short Token4side;
		public int Token1Ratio;
		public int Token2Ratio;
		public int Token3Ratio;
		public int Token4Ratio;
		public short CALCTYPE;
	}

	[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct FOPAIRDIFFLEG2
	{
		public int PORTFOLIONAME;
		public int SPREADBUY;
		public int SPREADSELL;
		public int Token1Ratio;
		public int Token2Ratio;
		public int Token3Ratio;
		public int Token4Ratio;
		public int BuyMin;
		public int BuyMax;
		public int SellMin;
		public int SellMax;
		public int Divisor;

		public Int16 Order_Type;
		public Int16 firstbid;
		public Int16 Opt_Tick;
		public Int16 Order_Depth;
		public Int16 second_leg;
		public Int16 Threshold;
		public Int16 Bidding_Range;
		public Int16 Req_count;
        public Int16 Pay_up_Ticks;


	}


	public enum FirstBid
	{
		Normal	 =	1,
		WEIGHTED	 =	2,
		BESTBID	 =	3,
		defaul=0
	}

	public enum SecondLeg
	{
		MKT	 =	1,
		LIMIT	 =	2,
		BESTBID	 =	3,
		defaul=0
	}

	public enum OrderType
	{
		Bidding	 =	1,
		IOC	 =	2,
		defaul=0
	}


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
    public struct DeletePortfolio
    {
        public int PORTFOLIONAME;
        public int Token1;
        public int Token2;
        public int Token3;
        public int Token4;
       
        
    }

}


#endregion




