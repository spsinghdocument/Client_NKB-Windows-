using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using Client.Spread;
//using LzoNseFO;
using Structure;

namespace Client.Spot
{
    class SpotTableMethods
    {
        private static readonly object LockTableOperation = new object();

        delegate void OnLZOArrivedDelegate(Object o, ReadOnlyEventArgs<MS_INDICES_7207> Stat);

        public static void CreateOrderTable()
        {
            if (CommonData.dtSpotWatch == null)
            {
                CommonData.dtSpotWatch = new DataTable("spotMktWatch");
                CommonData.dtSpotWatch.Columns.Add(SpreadContract.Symbol, typeof(string));
                CommonData.dtSpotWatch.Columns.Add(SpreadContract.Price, typeof(decimal));
                CommonData.dtSpotWatch.Columns.Add(SpreadContract.ChangeIndicator, typeof(string));
                CommonData.dtSpotWatch.Columns.Add(SpreadContract.PercentChange, typeof(decimal));
                CommonData.dtSpotWatch.Columns.Add(SpreadContract.ClosePrice, typeof(decimal));
                CommonData.dtSpotWatch.Columns.Add("PointChange", typeof(decimal), "ClosePrice - Price");
            }
        }
      
        public static void UpdateRecord(object sender, ReadOnlyEventArgs<MS_INDICES_7207> Stat)
        {
             try
             {
            if (AppGlobal.frmSpotIndex != null && AppGlobal.frmSpotIndex.InvokeRequired)
            {
                AppGlobal.frmSpotIndex.BeginInvoke(new OnLZOArrivedDelegate(UpdateRecord), sender, new ReadOnlyEventArgs<MS_INDICES_7207>(Stat.Parameter));
            }
            else
            {
                if (CommonData.dtSpotWatch == null)
                    return;
                lock (LockTableOperation)
                {
                   
                        DataRow[] dr = CommonData.dtSpotWatch.Select("Symbol='" + Stat.Parameter.IndexName.Trim() +"'");
                        if (dr.Length > 0)
                        {
                            dr[0][SpreadContract.Price] = (decimal)(IPAddress.NetworkToHostOrder(Stat.Parameter.IndexValue)) / 100;
                            dr[0][SpreadContract.ChangeIndicator] = Convert.ToChar(Stat.Parameter.NetChangeIndicator);
                            dr[0][SpreadContract.PercentChange] = (decimal)IPAddress.NetworkToHostOrder(Stat.Parameter.PercentChange)/100;
                            dr[0][SpreadContract.ClosePrice] = (decimal)(IPAddress.NetworkToHostOrder(Stat.Parameter.ClosingIndex)) / 100;

                        }
                        else
                        {
                            DataRow drRec = CommonData.dtSpotWatch.NewRow();
                          drRec[SpreadContract.Symbol] = Stat.Parameter.IndexName.Trim();
                            drRec[SpreadContract.Price] = (decimal)(IPAddress.NetworkToHostOrder(Stat.Parameter.IndexValue)) / 100;
                            drRec[SpreadContract.ChangeIndicator] = Convert.ToChar(Stat.Parameter.NetChangeIndicator);
                            drRec[SpreadContract.PercentChange] = (decimal)IPAddress.NetworkToHostOrder(Stat.Parameter.PercentChange)/100;
                            drRec[SpreadContract.ClosePrice] = (decimal)(IPAddress.NetworkToHostOrder(Stat.Parameter.ClosingIndex)) / 100;


                            //===============
                            if (drRec[SpreadContract.Symbol].ToString() == "CNX Nifty" || drRec[SpreadContract.Symbol].ToString() == "BANK Nifty" || drRec[SpreadContract.Symbol].ToString() == "India VIX")
                           {
                                // var filtered = CommonData.AsEnumerable().Where(r => r.Field<String>(SpreadContract.Symbol).Contains("NV20"));
                                //=============================

                                CommonData.dtSpotWatch.Rows.Add(drRec);
                            }
                        }


                }
            }
             }
                catch (Exception e)
                    {
                       // AppGlobal.Logger.WriteinFileWindowAndBox(e, LogEnums.WriteOption.LogWindow_ErrorLogFile, color: AppLog.RedColor);
                    }
        }
    }
}
 

