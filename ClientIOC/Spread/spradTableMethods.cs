using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Structure;
using System.Net;
using LogWriter;
using System.Drawing;
using System.Collections.Concurrent;

namespace Client.Spread
{
    class spradTableMethods
    {
        private static readonly object LockTableOperation = new object();

        delegate void OnLZOArrivedDelegate(Object o, ReadOnlyEventArgs<MS_SPD_MKT_INFO_7211> Stat);
         
            private static DataGridViewCellStyle _makeItBlack;
            private static DataGridViewCellStyle _makeItBlue;
            private static DataGridViewCellStyle _makeItRed;
            public static readonly ConcurrentDictionary<string, DataGridViewRow> _SprdwatchDict = new ConcurrentDictionary<string, DataGridViewRow>();
        public static void CreateOrderTable()
        {
            if (CommonData.dtSpreadMktWatch == null)
            {
                _makeItRed = new DataGridViewCellStyle();
                _makeItBlue = new DataGridViewCellStyle();
                _makeItBlack = new DataGridViewCellStyle();

                _makeItRed.BackColor = Color.LightPink;

                _makeItBlue.BackColor = Color.DeepSkyBlue;
              // _makeItBlack.BackColor = Color.Yellow;
                CommonData.dtSpreadMktWatch = new DataTable("spdMktWatch");
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Symbol1, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.ExpiryDate1, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.ExpiryDate2, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Bid, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.BidQ, typeof(int));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Ask, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.AskQ, typeof(int));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.LTP, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.TradedVolume, typeof(decimal));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.TotalBuyQty, typeof(int));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.TotalSellQty, typeof(int));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.OpenPrice, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.HighPrice, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.LowPrice, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.ClosePrice, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.ATP, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.TotalTradedValue, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.PercentChange, typeof(decimal));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.LastActiveTime, typeof(DateTime));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.LastUpdateTime, typeof(DateTime));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.InstrumentName2, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.InstrumentName1, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Token1, typeof(int));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Token2, typeof(int));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.OptionType1, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.OptionType2, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.CALevel1, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.CALevel2, typeof(string));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.StrikePrice1, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.StrikePrice2, typeof(decimal));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Symbol2, typeof(string));

                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.UnixExpiry1, typeof(Int32));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.UnixExpiry2, typeof(Int32));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.BoardLotQuantity1, typeof(Int32));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.BoardLotQuantity2, typeof(Int32));
                CommonData.dtSpreadMktWatch.Columns.Add(SpreadContract.Price_Diff, typeof(Int32));

               

            }
        }
        public static void InsertRecord(int a)
        {
           
           
            

        }
        private static void SetData(DataGridViewCell DGCell, double ValueOne)
        {
            if (DGCell != null)
            {
                double ValueTwo = Convert.ToDouble(DGCell.Value==DBNull.Value?0:DGCell.Value);
                if (ValueOne > ValueTwo)
                {
                    DGCell.Style = _makeItBlue;
                }
                else if (ValueOne < ValueTwo)
                {
                    DGCell.Style = _makeItRed;
                }
                //else if (ValueOne == ValueTwo)
                //{
                //    DGCell.Style = _makeItBlack;
                //}
            }

            DGCell.Value = ValueOne;
        }
        public static void UpdateRecord(object sender, ReadOnlyEventArgs<MS_SPD_MKT_INFO_7211> Stat)
        {
            
                
            
        }
    }
}
