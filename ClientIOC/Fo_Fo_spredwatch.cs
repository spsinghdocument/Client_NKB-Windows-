using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Structure;
using System.IO;
using csv;
using System.Reflection;
using System.Net;
using Microsoft.VisualBasic;
using System.Xml;
using System.Threading;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Client
{
   
    public partial class fofospreadwatch : Form
    {
       
        private readonly Dictionary<int, DataGridViewRow> _mwatchDict2 = new Dictionary<int, DataGridViewRow>();
        private readonly Dictionary<int, Data1> _DataDict2 = new Dictionary<int, Data1>();
        private DataGridViewCellStyle _makeItBlack;
        private DataGridViewCellStyle _makeItBlue;
        private DataGridViewCellStyle _makeItRed;
        internal event EventHandler<ReadOnlyEventArgs<LTPTONETBOOK>> FOR_Net_BOOK;
        public  int str_price1;
        internal DataTable SpreadTable;
        Scroller.IniFile _inifile = null;
        public string before1 = "";
       // TextBox txt = new TextBox();

        //private static readonly Fo_Fo_mktwatch instance = new Fo_Fo_mktwatch();
        //public static Fo_Fo_mktwatch Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}
        // this.gvw1.Columns[0].HeaderText = "The new header";
       
        private int portFolioCounter = 1;
        public fofospreadwatch()
        {
            InitializeComponent();
            _inifile = new Scroller.IniFile(Application.StartupPath+ Path.DirectorySeparatorChar+"lastcloseini.ini");
            
        }
        private void SetDisplayRules(DataGridViewColumn dgvCol, String Value)
        {
            dgvCol.HeaderText = Value;
            dgvCol.ReadOnly = true;
        }

        private static void SpreadTable_NewRow(object sender, DataTableNewRowEventArgs e)
        {
          
        }

        public void eOrderTRADED_OUT(byte[] buffer)
        {

        }

        public void TRADE_CONFIRMATION_TR(byte[] buffer) //-- 20222  
        {

            var v=Convert.ToInt32(buffer.Length==153?0:buffer[153]);
            if (v == 77)
                return;
            var obj = (MS_TRADE_CONFIRM_TR)DataPacket.RawDeserialize(buffer, typeof(MS_TRADE_CONFIRM_TR));

            Fillqty_ingrd(IPAddress.HostToNetworkOrder(obj.Token), IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator), obj);

        //   FillATP(obj);


           // *********************
        
          

        
        }
        public void FillATP(MS_TRADE_CONFIRM_TR obj)
        {
            try
            {
               
                DataGridViewRow row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token1"].Value.ToString().Equals(Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))).FirstOrDefault();
                if (row == null)
                {
                   // row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token2"].Value.ToString().Equals(Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))).FirstOrDefault();
                    return;
                }

                var val1 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio1"].Value);
                var val2 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio2"].Value);
                var val3 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio3"].Value);



                var query1 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token1"].Value)).Select(a => new SelectListItem
                {
                    TokenNo = Convert.ToInt32(a.Field<Int32>("Token")),
                    BuyAvg = a.Field<double>("BuyAvg"),
                    SellAvg = a.Field<double>("SellAvg")
                }).ToList();
                var query2 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value)).Select(a => new SelectListItem
                {
                    TokenNo = a.Field<Int32>("Token"),
                    BuyAvg = a.Field<double>("BuyAvg"),
                    SellAvg = a.Field<double>("SellAvg")
                           }).ToList();


                if (IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator) == 1 )
                {
                    DGV1.Rows[row.Index].Cells["ATP(B)"].Value = -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) + (query2.FirstOrDefault().SellAvg * Convert.ToInt32(val2));
                    DGV1.Rows[row.Index].Cells["ATP(S)"].Value = query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1) - query2.FirstOrDefault().BuyAvg * Convert.ToInt32(val2);
                    
                }
                else
                {
                    DGV1.Rows[row.Index].Cells["ATP(S)"].Value = query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1) - query2.FirstOrDefault().BuyAvg * Convert.ToInt32(val2);
                    DGV1.Rows[row.Index].Cells["ATP(B)"].Value = -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) + (query2.FirstOrDefault().SellAvg * Convert.ToInt32(val2));
                }


            }
            catch (Exception ex)
            {
                Client.LogWriterClass.logwritercls.logs("ErrorValuecheckavg", "Value Check update in gridview" + ex.Message);
            }
        }
        private List<SelectListItem> query1;
        private List<SelectListItem> query2;
        private List<SelectListItem> query3;
        TradeTrac trd_obj = new TradeTrac();


        //double trp1;
        //double trp11;
        //double trp2;
        //double trp22;
        public void Fillqty_ingrd(int tokenno, int buy_sell,MS_TRADE_CONFIRM_TR obj)
        {
           
            try
            {

                bool sp;
                string strbuysell = buy_sell == 1 ? "Buy" : "Sell";
                string Token1Side,Token2Side, Token3Side;
    
                Client.Csv_Struct _lotsize = new Csv_Struct();
                object ob = new object();
                lock (ob)
                {
                   // DataGridViewRow row=null;
                    DataGridViewRow row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token1"].Value.ToString().Equals(Convert.ToString(tokenno))).FirstOrDefault();
                    //foreach(DataGridViewRow row1 in DGV1.Rows)
                    //{
                    //    if(row1.Cells["Token1"].Value.ToString()==Convert.ToString(tokenno))
                    //    {
                    //        row = row1;
                    //        break;
                    //    }
                    //}
                 
                    if (row == null)
                    {
                        row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token2"].Value.ToString().Equals(Convert.ToString(Convert.ToString(tokenno)))).FirstOrDefault();
                        if (row == null)
                       {
                           row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token3"].Value.ToString().Equals(Convert.ToString(Convert.ToString(tokenno)))).FirstOrDefault();
                           if (row == null)
                           {
                               return;
                           }
                           }
//return;
                    }
                    //var v = Global.Instance.Ratio.Where(a => a.Key == (tokenno)).Select(b => b.Value).ToList();
                    //val = Convert.ToInt32(v.FirstOrDefault().ToString());
                    var pf = Convert.ToInt32(DGV1.Rows[row.Index].Cells["PF"].Value);
                    var val1 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio1"].Value);
                    var val2 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio2"].Value);
                    var val3 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["ratio3"].Value);
                   int near_tok1= Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token1"].Value);
                   int far_tok2 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value);
                   int _tok3 = Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token3"].Value);
                   //  int buy= Convert.ToInt32(DGV1.Rows[row.Index].Cells["Buy"].Value);
                   //int sell = Convert.ToInt32(DGV1.Rows[row.Index].Cells["Sell"].Value);
                    if (Holder._DictLotSize.ContainsKey(tokenno) == false || tokenno != 0)
                    {
                        Holder._DictLotSize.TryAdd(tokenno, new Csv_Struct()
                        {
                            lotsize = CSV_Class.cimlist.Where(q => q.Token == tokenno).Select(a => a.BoardLotQuantity).First()
                        }
                        );
                    }
                     query1 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token1"].Value)).Select(a => new SelectListItem
                    {
                        TokenNo = Convert.ToInt32(a.Field<Int32>("Token")),
                        BuyAvg = a.Field<double>("BuyAvg"),
                        SellAvg = a.Field<double>("SellAvg")
                    }).ToList();
                     var foundRow = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value)).Count()>0;
                    //if(foundRow != null) {
                    ////You have it ...
                    //     }
                  
                 //   if(Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value)))
                    if (foundRow ==true)
                    {
                     query2 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value)).Select(a => new SelectListItem
                    {
                        TokenNo = a.Field<Int32>("Token"),
                        BuyAvg = a.Field<double>("BuyAvg"),
                        SellAvg = a.Field<double>("SellAvg")
                    }).ToList();
                    }


                    var foundRow2 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token3"].Value)).Count() > 0;
                    //if(foundRow != null) {
                    ////You have it ...
                    //     }

                    //   if(Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token2"].Value)))
                    if (foundRow2 == true)
                    {
                        query3 = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<Int32>("Token") == Convert.ToInt32(DGV1.Rows[row.Index].Cells["Token3"].Value)).Select(a => new SelectListItem
                        {
                            TokenNo = a.Field<Int32>("Token"),
                            BuyAvg = a.Field<double>("BuyAvg"),
                            SellAvg = a.Field<double>("SellAvg")
                        }).ToList();
                    }


                    
          
                    _lotsize = Holder._DictLotSize[tokenno];

      



                    if (near_tok1 == tokenno)
                    {

                         if (strbuysell == Convert.ToString(DGV1.Rows[row.Index].Cells["Tok1B_S"].Value))//create
                        {

                         
                            DGV1.Rows[row.Index].Cells["TRP1"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;
                            sp = true;

                            Token2Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok2B_S"].Value);
                            Token3Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok3B_S"].Value);

                           var ob_o = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), strbuysell.ToUpper()) && (a.Field<string>("Status") == "Traded")).Sum(a => Convert.ToInt32(a.Field<string>("Volume")));
                          // DGV1.Rows[row.Index].Cells["TRDQTY(B)"].Value = Convert.ToInt32(ob_o) / (Convert.ToInt32(_lotsize.lotsize) * (Convert.ToInt32(val1)));

                           double ob1 = Convert.ToDouble(ob_o);
                           double rev_ratio = _lotsize.lotsize * val1;
                           DGV1.Rows[row.Index].Cells["TRDQTY(B)"].Value = Math.Round(ob1 / rev_ratio, 2);
                             
                             //=========================================================================
          
                          //  int TrdqtyBuy = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), strbuysell.ToUpper()) && (a.Field<string>("Status") == "Traded")).ToList().Count;


                         //   DGV1.Rows[row.Index].Cells["TRDQTY(B)"].Value = TrdqtyBuy;
                             
                             //=======================================================

                           
                         

                            double Result1 = strbuysell == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                            double Result2 = Token2Side == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                             double Result3 =  Token3Side == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                             DGV1.Rows[row.Index].Cells["ATP(B)"].Value =

                           Math.Round(  Result1
                                 +
                                 Result2
                                 +
                                Result3,3);
                       

                            
                        }
                        else //reverse
                        {

                      
                            DGV1.Rows[row.Index].Cells["TRP11"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;

                            var ob_o = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), strbuysell.ToUpper()) && (a.Field<string>("Status") == "Traded")).Sum(a => Convert.ToInt32(a.Field<string>("Volume")));
                            double ob1 = Convert.ToDouble(ob_o);
                            double cr_ratio = _lotsize.lotsize * val2;
                            DGV1.Rows[row.Index].Cells["TRDQTY(S)"].Value = Math.Round( ob1 / cr_ratio,2);
                            // DGV1.Rows[row.Index].Cells["TRDQTY(S)"].Value = Convert.ToInt32(ob_o) / (Convert.ToInt32(_lotsize.lotsize) * (Convert.ToInt32(val2)));

                             //==============================================

                           // int TrdqtySel = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), strbuysell.ToUpper()) && (a.Field<string>("Status") == "Traded")).ToList().Count;

                         //  DGV1.Rows[row.Index].Cells["TRDQTY(S)"].Value = TrdqtySel;
                             //========================================================================
                      

                             Token2Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok2B_S"].Value) =="Buy" ? "Sell" : "Buy";
                             Token3Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok3B_S"].Value)=="Buy" ? "Sell" : "Buy";

                             sp = false;
                      

                            double ResultT1 = strbuysell == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                            double ResultT2 = Token2Side == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                             double ResultT3 = Token3Side == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                             DGV1.Rows[row.Index].Cells["ATP(S)"].Value =
                               Math.Round(  ResultT1

                               +
                               ResultT2
                               +
                               ResultT3,3);
                            
                          
                        }

                        //====== ========  ========  ======  ========= =============  ====== ========= =============  ====== ====== ========  ========  ==== ====  ======= ========== ==  == ==  ==  == ==  ==  ==  ==  ==  ==



                       
                    }
                    if (far_tok2 == tokenno)
                    {
                        if (strbuysell == Convert.ToString(DGV1.Rows[row.Index].Cells["Tok2B_S"].Value))//create
                        {
                           
                         DGV1.Rows[row.Index].Cells["TRP2"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;

                            Token1Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok1B_S"].Value);
                            Token3Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok3B_S"].Value);
                            double res1 = Token1Side == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                            double res2 = strbuysell == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                            double res3 = Token3Side == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                            DGV1.Rows[row.Index].Cells["ATP(B)"].Value =
                              Math.Round(   res1
                               +
                              res2
                               +
                               res3,3);

                            sp = true;




                        }
                        else   //reverse
                        {
                           
                            
                            DGV1.Rows[row.Index].Cells["TRP22"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;
                            
                            Token1Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok1B_S"].Value) == "Buy" ? "Sell" : "Buy";
                            Token3Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok3B_S"].Value) == "Buy" ? "Sell" : "Buy";

                            double resS1 = Token1Side == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                            double resS2 = strbuysell == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                            double resS3 = Token3Side == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                            DGV1.Rows[row.Index].Cells["ATP(S)"].Value =
         Math.Round(  resS1

             +
             resS2
             +

     resS3,3);
                            sp = false;

                        }


                        if (Convert.ToString(DGV1.Rows[row.Index].Cells["Strategy_Type"].Value) == "2_LEG")
                        {

                            if (Global.Instance.TradeTrac_dict.ContainsKey(IPAddress.HostToNetworkOrder(obj.Token)))
                            {
                                
                                double trp1 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP1"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP1"].Value) * Convert.ToInt32(val1);
                                double trp11 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP11"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP11"].Value) * Convert.ToInt32(val1);
                                double trp2 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP2"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP2"].Value) * Convert.ToInt32(val2);
                                double trp22 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP22"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP22"].Value) * Convert.ToInt32(val2);

                                trd_obj = new TradeTrac();
                              //  var a = trp1 + trp2;
                        
                           // double    v =v + a;
                                var creat = trp1 + trp2 ;
                               var Rev = trp11 + trp22;
                               int TrdqtyBuy = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), "BUY") && (a.Field<string>("Status") == "Traded")).ToList().Count;

                               int TrdqtySell = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), "SELL") && (a.Field<string>("Status") == "Traded")).ToList().Count;



                                trd_obj = Global.Instance.TradeTrac_dict[near_tok1];
                                trd_obj.ACTUALPRICE = sp == true ? creat : Rev;
                                trd_obj.B_S = IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator);
                                trd_obj.QTy = IPAddress.HostToNetworkOrder(obj.FillQuantity);

                               

                                trd_obj.SYMBOL = Encoding.ASCII.GetString(obj.Contr_dec_tr_Obj.Symbol);
                                trd_obj.TIME = LogicClass.ConvertFromTimestamp(IPAddress.HostToNetworkOrder(obj.LogTime)).ToString("HH:mm:ss.fff");

                                Global.Instance.TradeTrac_dict.AddOrUpdate(IPAddress.HostToNetworkOrder(obj.Token), trd_obj, (k, v1) => trd_obj);

                                //DataRow[] dr = Global.Instance.TradeTracker.Select("PF_ID  = '" + Convert.ToInt32(trd_obj.PF_ID) + "'");
                                DataRow[] dr = Global.Instance.TradeTracker.Select("Unique_id  = '" + ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() +  (IPAddress.HostToNetworkOrder(obj.Token)).ToString() +"'");


                                if (dr.Length > 0)
                                {
                                    dr[0]["PF_ID"] = Convert.ToString(trd_obj.PF_ID);
                                   // dr[0]["B/S"] = trd_obj.B_S == 1 ? "BUY" : "SELL";
                                    dr[0]["B/S"] = sp == true ? "BUY" : "SELL";
                                    //dr[0]["QTY"] = Convert.ToString(trd_obj.QTy);
                                   // dr[0]["QTY"] = trd_obj.B_S == 1 ? Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRDQTY(B)"].Value) : Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRDQTY(S)"].Value);
                                    double vo = Convert.ToDouble(IPAddress.HostToNetworkOrder(obj.FillQuantity));
                                    double createratio = Convert.ToDouble(_lotsize.lotsize * val2);
                                    dr[0]["QTY"] = vo / createratio >= Convert.ToDouble(1.0) ? "Complete" : "Partial";
                                    dr[0]["QTY"] = dr[0]["QTY"] + " \t" + Convert.ToString(Math.Round(vo / createratio, 2)); //"1";
                                  //  dr[0]["QTY"] = trd_obj.B_S == 1 ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                   // dr[0]["QTY"] =Convert.ToDecimal(IPAddress.HostToNetworkOrder(obj.FillQuantity)/ (Convert.ToInt32(_lotsize.lotsize) * (Convert.ToInt32(val2)))); //"1";
                                  //  dr[0]["QTY"] = sp == true ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                    dr[0]["ACTUALPRICE"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.ACTUALPRICE), 3));

                                    //dr[0]["GIVENPRICEBUY"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3));
                                    //dr[0]["GIVENPRICESELL"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));

                                    dr[0]["GIVENPRICEBUY"] = sp == true ? Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3)) : "0";
                                    dr[0]["GIVENPRICESELL"] = sp == true ? "0" : Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));
                                       
                                    dr[0]["SYMBOL"] = Convert.ToString(trd_obj.SYMBOL);
                                    dr[0]["TIME"] = Convert.ToString(trd_obj.TIME);
                                    dr[0]["Unique_id"] = ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() + (IPAddress.HostToNetworkOrder(obj.Token)).ToString();
                                    
                                }
                                else
                                {

                                    BeginInvoke((Action)delegate
                                    {
                                        DataRow dr2 = Global.Instance.TradeTracker.NewRow();
                                        //  dr2["Sno2"] = trd_ob.ToString();

                                        dr2["PF_ID"] = Convert.ToString(trd_obj.PF_ID);
                                        //dr2["B/S"] = trd_obj.B_S == 1 ? "BUY" : "SELL";
                                        dr2["B/S"] = sp == true ? "BUY" : "SELL";

                                        // dr2["GIVENPRICEBUY"] = sp == true ? "BUY" : "SELL";
                                        //  dr2["QTY"] = Convert.ToString(trd_obj.QTy);
                                        //  dr2["QTY"] = trd_obj.B_S == 1 ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                        //dr2["QTY"] = sp == true ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);

                                        double vo = Convert.ToDouble(IPAddress.HostToNetworkOrder(obj.FillQuantity));
                                        double createratio = Convert.ToDouble(_lotsize.lotsize * val2);
                                        dr2["QTY"] = vo / createratio >= Convert.ToDouble(1.0) ? "Complete" : "Partial";
                                        dr2["QTY"] = dr2["QTY"] + " \t" + Convert.ToString(Math.Round(vo / createratio, 2)); //"1";
                                        dr2["ACTUALPRICE"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.ACTUALPRICE), 3));

                                        dr2["GIVENPRICEBUY"] = sp == true ? Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3)) : "0";
                                        dr2["GIVENPRICESELL"] = sp == true ? "0" : Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));

                                        dr2["SYMBOL"] = Convert.ToString(trd_obj.SYMBOL);
                                        dr2["TIME"] = Convert.ToString(trd_obj.TIME);
                                        dr2["Unique_id"] = ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() + (IPAddress.HostToNetworkOrder(obj.Token)).ToString();
                                        Global.Instance.TradeTracker.Rows.Add(dr2);
                                    });
                                        //Trade_Tracker.Instance.DGV.ScrollBars = ScrollBars.Vertical;
                                }
//================== ================= =============== ========== ========== ====== ==== ===== ===== ===== =======  ====== ============== ==== ==      == == ==  == ==  == == ==  ==

                                double WTC_txt1 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["W_T_C"].Value);
                                if(sp == true)  // crea
                                {
                                    if ((Math.Abs((trd_obj.Given_Price_Buy)) + Math.Abs(WTC_txt1)) < Math.Abs(trd_obj.ACTUALPRICE))
                                    {
                                        DGV1.Rows[row.Index].Cells["WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) + 1;
                                    }

                                 

                                    DGV1.Rows[row.Index].Cells["_con_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) + 1;

                                    if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == Convert.ToInt32(WTC_txt.Text) && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))
                                      //  if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == 5 && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))

                                    
                                    {
                                        DGV1.Rows[row.Index].Cells["WTC"].Value = 0;
                                        DGV1.Rows[row.Index].Cells["_con_WTC"].Value = 0;
                                      //  DGV1.Rows[row.Index].Cells["Enable"].Value = false;
                                    }
                                    if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) != Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value))
                                    {
                                        DGV1.Rows[row.Index].Cells["WTC"].Value = 0;
                                        DGV1.Rows[row.Index].Cells["_con_WTC"].Value = 0;
                                    }

                                // \\\\ ///  

                                
                                }
                                else   // rev
                                {

                                    if ((Math.Abs((trd_obj.Given_Price_Sell)) - Math.Abs(WTC_txt1)) > Math.Abs(trd_obj.ACTUALPRICE))
                                    {
                                        DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) + 1;
                                    }
                                  //  var _T_v = (IPAddress.HostToNetworkOrder(obj.FillPrice) / 100) + trd_obj.ACTUALPRICE;
                                    DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value) + 1;
                                    if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) == Convert.ToInt32(WTC_txt.Text) && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value))
                                      //  if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == 5 && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))
                               
                                    
                                    {
                                        DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = 0;
                                        DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = 0;
                                      // DGV1.Rows[row.Index].Cells["Enable"].Value = false;
                                    }

                                    if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) != Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value))
                                    {
                                        DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = 0;
                                        DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = 0;
                                    }
                                
                                
                                
                                }

                                //===============================================================================================================



                            }

                        }
                    }


                    //=================================  
                    if (_tok3 == tokenno)
                    {
                       
                        if (strbuysell == Convert.ToString(DGV1.Rows[row.Index].Cells["Tok3B_S"].Value))//create
                        {
                            DGV1.Rows[row.Index].Cells["TRP3"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;

                       //  var t   = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;
                        // DGV1.Rows[row.Index].Cells["TRP3"].Value = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP3"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP3"].Value) + t;

                            Token1Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok1B_S"].Value);
                            Token2Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok2B_S"].Value);
                            double res1 = Token1Side == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                                 double res2 =  Token2Side == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                                      double res3 =   strbuysell == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                                      DGV1.Rows[row.Index].Cells["ATP(B)"].Value =
                                         Math.Round(  res1

                                         +
                                        res2
                                         +
                                       res3,3);

                                      sp = true;

                        }
                        else //reverse
                        {
                        //  var t = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;
                        //  DGV1.Rows[row.Index].Cells["TRP33"].Value = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP33"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP33"].Value) + t;
                          DGV1.Rows[row.Index].Cells["TRP33"].Value = strbuysell == "Buy" ? -IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00 : IPAddress.HostToNetworkOrder(obj.FillPrice) / 100.00;
                            // DGV1.Rows[row.Index].Cells["ATP(S)"].Value = query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1) - (query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2) - (query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3);
                            Token1Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok1B_S"].Value) == "Buy" ?"Sell" : "Buy";
                            Token2Side = Convert.ToString(DGV1.Rows[row.Index].Cells["Tok2B_S"].Value) == "Buy" ? "Sell" : "Buy";

                            double resS1 = Token1Side == "Buy" ? -(query1.FirstOrDefault().BuyAvg * Convert.ToInt32(val1)) : (query1.FirstOrDefault().SellAvg * Convert.ToInt32(val1));
                                double resS2 =  Token2Side == "Buy" ? -((query2 == null ? 0 : query2.FirstOrDefault().BuyAvg) * Convert.ToInt32(val2)) : ((query2 == null ? 0 : query2.FirstOrDefault().SellAvg) * Convert.ToInt32(val2));
                                    double resS3 = strbuysell == "Buy" ? -((query3 == null ? 0 : query3.FirstOrDefault().BuyAvg) * Convert.ToInt32(val3)) : ((query3 == null ? 0 : query3.FirstOrDefault().SellAvg) * Convert.ToInt32(val3));

                                    DGV1.Rows[row.Index].Cells["ATP(S)"].Value =
                                        Math.Round( resS1

                                       +
                                      resS2
                                       +
                                       resS3,3);

                                    sp = false;
                            
                        
                        }

                        if (Convert.ToString(DGV1.Rows[row.Index].Cells["Strategy_Type"].Value) == "3_LEG")
                        {
                        if (Global.Instance.TradeTrac_dict.ContainsKey(IPAddress.HostToNetworkOrder(obj.Token)))
                        {
                           // double trp13 = Token1Side == "Buy" ? -Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP1"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP1"].Value) : Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP1"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP1"].Value);
                          //  double trp23 = Token2Side == "Buy" ? -Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP2"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP2"].Value) : Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP2"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP2"].Value);
                         //   double trp33 = strbuysell == "Buy" ? -Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP3"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP3"].Value) : Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP3"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP3"].Value);

                            double trp1 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP1"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP1"].Value) * Convert.ToInt32(val1);
                            double trp11 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP11"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP11"].Value) * Convert.ToInt32(val1);
                            double trp2 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP2"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP2"].Value) * Convert.ToInt32(val2);
                            double trp22 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP22"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP22"].Value) * Convert.ToInt32(val2);

                            double trp3 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP3"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP3"].Value) * Convert.ToInt32(val3);
                            double trp33 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["TRP33"].Value == DBNull.Value ? 0 : DGV1.Rows[row.Index].Cells["TRP33"].Value) * Convert.ToInt32(val3);

                            int TrdqtyBuy = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), "BUY") && (a.Field<string>("Status") == "Traded")).ToList().Count;

                            int TrdqtySell = Global.Instance.OrdetTable.AsEnumerable().Where(a => (Convert.ToInt32(a.Field<string>("TokenNo")) == tokenno) && String.Equals(a.Field<string>("Buy_SellIndicator"), "SELL") && (a.Field<string>("Status") == "Traded")).ToList().Count;

                            
                            
                            trd_obj = new TradeTrac();
                            var crea = trp1 + trp2 + trp3;
                            var rev = trp11 + trp22 + trp33;
                            
                            trd_obj = Global.Instance.TradeTrac_dict[near_tok1];
                            trd_obj.ACTUALPRICE = sp == true ? crea : rev;
                            trd_obj.B_S = IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator);
                            trd_obj.QTy = IPAddress.HostToNetworkOrder(obj.FillQuantity);
                            trd_obj.SYMBOL = Encoding.ASCII.GetString(obj.Contr_dec_tr_Obj.Symbol);
                            trd_obj.TIME = LogicClass.ConvertFromTimestamp(IPAddress.HostToNetworkOrder(obj.LogTime)).ToString("HH:mm:ss.fff");

                            Global.Instance.TradeTrac_dict.AddOrUpdate(IPAddress.HostToNetworkOrder(obj.Token), trd_obj, (k, v1) => trd_obj);
                           
                            DataRow[] dr = Global.Instance.TradeTracker.Select("Unique_id  = '" + ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() + (IPAddress.HostToNetworkOrder(obj.Token)).ToString() + "'");

                            

                            if (dr.Length > 0)
                            {
                                dr[0]["PF_ID"] = Convert.ToString(trd_obj.PF_ID);
                                //dr[0]["B/S"] = trd_obj.B_S == 1 ? "BUY" : "SELL";
                                dr[0]["B/S"] = sp == true ? "BUY" : "SELL";

                              //  dr[0]["QTY"] = Convert.ToString(trd_obj.QTy);
                                double vo = Convert.ToDouble(IPAddress.HostToNetworkOrder(obj.FillQuantity));
                                double createratio = Convert.ToDouble(_lotsize.lotsize * val3);
                                dr[0]["QTY"] = vo / createratio >= Convert.ToDouble(1.0) ? "Complete" : "Partial";
                                dr[0]["QTY"] = dr[0]["QTY"] + " \t" + Convert.ToString(Math.Round(vo / createratio, 2)); //"1";
                                      
                               // dr[0]["QTY"]  = trd_obj.B_S == 1 ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                               // dr[0]["QTY"] = Convert.ToDecimal(IPAddress.HostToNetworkOrder(obj.OriginalVolume) / (Convert.ToInt32(_lotsize.lotsize) * (Convert.ToInt32(val3))));
                              //  dr[0]["QTY"] = sp == true ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                dr[0]["ACTUALPRICE"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.ACTUALPRICE), 3));

                                dr[0]["GIVENPRICEBUY"] = sp == true ? Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3)) : "0";
                                dr[0]["GIVENPRICESELL"] = sp == true ? "0" : Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));

                                dr[0]["SYMBOL"] = Convert.ToString(trd_obj.SYMBOL);
                                dr[0]["TIME"] = Convert.ToString(trd_obj.TIME);
                                dr[0]["Unique_id"] = ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() + (IPAddress.HostToNetworkOrder(obj.Token)).ToString();
                              // Trade_Tracker.Instance.DGV.Refresh();
                             
                            }
                            else
                            {
                                BeginInvoke((Action)delegate
                                    {
                                        DataRow dr2 = Global.Instance.TradeTracker.NewRow();
                                        //  dr2["Sno2"] = trd_ob.ToString();
                                        dr2["PF_ID"] = Convert.ToString(trd_obj.PF_ID);
                                        //  dr2["B/S"] = trd_obj.B_S == 1 ? "BUY" : "SELL";
                                        dr2["B/S"] = sp == true ? "BUY" : "SELL";
                                        //  dr2["QTY"] = Convert.ToString(trd_obj.QTy);
                                        double vo = Convert.ToDouble(IPAddress.HostToNetworkOrder(obj.FillQuantity));
                                        double createratio = Convert.ToDouble(_lotsize.lotsize * val3);
                                        dr2["QTY"] = vo / createratio >= Convert.ToDouble(1.0) ? "Complete" : "Partial";
                                        dr2["QTY"] = dr2["QTY"] + " \t" + Convert.ToString(Math.Round(vo / createratio, 2)); //"1";

                                        // dr2["QTY"] = trd_obj.B_S == 1 ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                        // dr[0]["QTY"] =Convert.ToDecimal(IPAddress.HostToNetworkOrder(obj.OriginalVolume) / (Convert.ToInt32(_lotsize.lotsize) * (Convert.ToInt32(val3))));                                //dr2["QTY"] = sp == true ? Convert.ToDouble(TrdqtyBuy) : Convert.ToDouble(TrdqtySell);
                                        dr2["ACTUALPRICE"] = Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.ACTUALPRICE), 3));

                                        // dr2["GIVENPRICEBUY"] = trd_obj.B_S == 1 ? Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3)) : "0";
                                        // dr2["GIVENPRICESELL"] = trd_obj.B_S == 1 ? "0" : Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));


                                        dr2["GIVENPRICEBUY"] = sp == true ? Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Buy), 3)) : "0";
                                        dr2["GIVENPRICESELL"] = sp == true ? "0" : Convert.ToString(Math.Round(Convert.ToDouble(trd_obj.Given_Price_Sell), 3));
                                        dr2["SYMBOL"] = Convert.ToString(trd_obj.SYMBOL);
                                        dr2["TIME"] = Convert.ToString(trd_obj.TIME);
                                        dr2["Unique_id"] = ((long)LogicClass.DoubleEndianChange((obj.ResponseOrderNumber))).ToString() + (IPAddress.HostToNetworkOrder(obj.Token)).ToString();
                                        Global.Instance.TradeTracker.Rows.Add(dr2);
                                        //Trade_Tracker.Instance.DGV.Refresh();

                                    });
                            }

                            //================================================================================================================================

                            double WTC_txt1 = Convert.ToDouble(DGV1.Rows[row.Index].Cells["W_T_C"].Value);
                            if (sp == true)  // crea
                            {
                                if ((Math.Abs((trd_obj.Given_Price_Buy)) + Math.Abs(WTC_txt1)) < Math.Abs(trd_obj.ACTUALPRICE))
                                {
                                    DGV1.Rows[row.Index].Cells["WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) + 1;
                                }



                                DGV1.Rows[row.Index].Cells["_con_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) + 1;

                                if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == Convert.ToInt32(WTC_txt.Text) && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))
                                   // if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == 5 && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))
                               
                                {
                                    DGV1.Rows[row.Index].Cells["WTC"].Value = 0;
                                    DGV1.Rows[row.Index].Cells["_con_WTC"].Value = 0;
                                  //  DGV1.Rows[row.Index].Cells["Enable"].Value = false;
                                }
                                if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) != Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value))
                                {
                                    DGV1.Rows[row.Index].Cells["WTC"].Value = 0;
                                    DGV1.Rows[row.Index].Cells["_con_WTC"].Value = 0;
                                }



                            }
                            else   // rev
                            {

                                if ((Math.Abs((trd_obj.Given_Price_Sell)) - Math.Abs(WTC_txt1)) > Math.Abs(trd_obj.ACTUALPRICE))
                                {
                                    DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) + 1;
                                }
                              //  var _T_v = (IPAddress.HostToNetworkOrder(obj.FillPrice) / 100) + trd_obj.ACTUALPRICE;
                                DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value) + 1;
                              
                                if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) == Convert.ToInt32(WTC_txt.Text) && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value))
                                //    if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value) == 5 && Convert.ToInt32(DGV1.Rows[row.Index].Cells["_con_WTC"].Value) == Convert.ToInt32(DGV1.Rows[row.Index].Cells["WTC"].Value))
                               
                                {
                                    DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = 0;
                                    DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = 0;
                                //  DGV1.Rows[row.Index].Cells["Enable"].Value = false;
                                }

                                if (Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_WTC"].Value) != Convert.ToInt32(DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value))
                                {
                                    DGV1.Rows[row.Index].Cells["_sell_WTC"].Value = 0;
                                    DGV1.Rows[row.Index].Cells["_sell_con_WTC"].Value = 0;
                                }



                            }

                            //=====   ===  ==== ===  === ====  === ===  == === ====  =====  == === ===  ===== ====  ===== =====  ==== ==== =====  ====   === === == === ===== ==== ===== ====

                            }
                        }
                       

                       
                    }
                    
                   

                }

            }
            catch (Exception ex)
            {
                Client.LogWriterClass.logwritercls.logs("ErrorValue_check", "Value Check update in gridview" + ex.Message);

            }
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (portFolioCounter == 0)
                portFolioCounter = 1;
            using (AddSpreadToken _AddToken = new AddSpreadToken())
            {
                _AddToken.txtpfName.Text = portFolioCounter.ToString();
                _AddToken.lblPfName.Visible = true;
                _AddToken.txtpfName.Visible = true;
                _AddToken.Text = "Add Near Month Token";
                _AddToken.button1.Text = "Add Token";

                _AddToken.combox_OrderType.Text = "Bidding";
                _AddToken.comb_1stleg.Text = "NORMAL";
                _AddToken.combox_optTrick.Text = "1";
                _AddToken.comb_OrderDepth.Text = "1";
                _AddToken.com2nd_leg.Text = "MKT";
                _AddToken.textBox_Threshold.Text = "50";
                _AddToken.comb_BidRange.Text = "1";
                _AddToken.txtReqConnt.Text = "25";

               

                if (_AddToken.ShowDialog() == DialogResult.OK)
                {
                    DataRow dr = SpreadTable.NewRow();

                    dr["PF"] = _AddToken._objOut2.PFName;
                    dr["NEAR"] = _AddToken._objOut2.Desc1;
                    dr["Token1"] = _AddToken._objOut2.Token1;

                    dr["FAR"] = _AddToken._objOut2.Desc2;
                    dr["Token2"] = _AddToken._objOut2.Token2;
                    dr["tok3"] = _AddToken._objOut2.Desc3;
                    dr["Token3"] = _AddToken._objOut2.Token3;
                    dr["Token4"] = _AddToken._objOut2.Token4;
                    dr["Calc_type"] = _AddToken._objOut2.Calc_type;


                    dr["ratio1"] = Convert.ToString(_AddToken._objOut2.ratio1) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio1);
                    dr["ratio2"] = Convert.ToString(_AddToken._objOut2.ratio2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio2);
                    dr["ratio3"] = Convert.ToString(_AddToken._objOut2.ratio3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio3);
                    dr["ratio4"] = Convert.ToString(_AddToken._objOut2.ratio4) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio4);

                    dr["Strategy_Type"] = Convert.ToString(_AddToken._objOut2.Strat_type) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Strat_type);
                    dr["Symbol1"] = Convert.ToString(_AddToken._objOut2.Symbol1) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol1);
                    dr["Symbol2"] = Convert.ToString(_AddToken._objOut2.Symbol2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol2);
                    dr["Expiry"] = Convert.ToString(_AddToken._objOut2.Expiry) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry);
                    dr["Expiry2"] = Convert.ToString(_AddToken._objOut2.Expiry2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry2);
                    dr["OptionType"] = Convert.ToString(_AddToken._objOut2.OptionType) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType);
                    dr["OptionType2"] = Convert.ToString(_AddToken._objOut2.OptionType2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType2);
                    dr["StrikePrice"] = Convert.ToString(_AddToken._objOut2.StrikePrice) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice);
                    dr["StrikePrice2"] = Convert.ToString(_AddToken._objOut2.StrikePrice2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice2);
                    dr["ReqConnt"] = Convert.ToString(_AddToken._objOut2.ReqConnt) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ReqConnt);

                    dr["Symbol3"] = Convert.ToString(_AddToken._objOut2.Symbol3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol3);
                    dr["Expiry3"] = Convert.ToString(_AddToken._objOut2.Expiry3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry3);
                    dr["OptionType3"] = Convert.ToString(_AddToken._objOut2.OptionType3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType3);
                    dr["StrikePrice3"] = Convert.ToString(_AddToken._objOut2.StrikePrice3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice3);



                    dr["Order_Type"] = Convert.ToString(_AddToken._objOut2.Order_Type) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Order_Type);
                    dr["First_LEG"] = Convert.ToString(_AddToken._objOut2.first_LEG) == "" ? "0" : Convert.ToString(_AddToken._objOut2.first_LEG);
                    dr["OPT_TICK"] = Convert.ToString(_AddToken._objOut2.OPT_TICK) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OPT_TICK);
                    dr["ORDER_DEPTH"] = Convert.ToString(_AddToken._objOut2.ORDER_DEPTH) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ORDER_DEPTH);
                    dr["Second_Leg"] = Convert.ToString(_AddToken._objOut2.second_Leg) == "" ? "0" : Convert.ToString(_AddToken._objOut2.second_Leg);
                    dr["THRESHOLD"] = Convert.ToString(_AddToken._objOut2.THRESHOLD) == "" ? "0" : Convert.ToString(_AddToken._objOut2.THRESHOLD);
                    dr["BIDDING_RANGE"] = Convert.ToString(_AddToken._objOut2.BIDDING_RANGE) == "" ? "0" : Convert.ToString(_AddToken._objOut2.BIDDING_RANGE);



                    dr["Tok1B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell);
                    dr["Tok2B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell2);
                    dr["Tok3B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell3);
                    dr["Tok4B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell4) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell4);


                    dr["tok1inst"] = _AddToken._objOut2.tok1_inst;
                    dr["tok2inst"] = _AddToken._objOut2.tok2_inst;
                    dr["tok3inst"] = _AddToken._objOut2.tok3_inst;
                    dr["F_BID"] = 0;
                    dr["F_ASK"] = 0;

                    dr["F_LTP"] = 0;
                    dr["NBID"] = 0;

                    dr["NASK"] = 0;
                    dr["NLTP"] = 0;

                    dr["FBID"] = 0;
                    dr["FASK"] = 0;

                    dr["FLTP"] = 0;
                    dr["NETDelta1"] = 0;
                    dr["NETDelta2"] = 0;
                    dr["NETDelta3"] = 0;


                    dr["OppsitToken1"] = _AddToken._objOut2.OppositeToken1;
                    dr["OppsitToken2"] = _AddToken._objOut2.OppositeToken2;
                    dr["OppsitToken3"] = _AddToken._objOut2.OppositeToken3;
                  

                    ///   \\\\\    /////   \\\\\\\\  /////   \\\\   ////   \\\\   
                    //dr["OppsitDesc1"] = _AddToken._objOut2.OppositeDesc1;
                   // dr["OppsitDesc2"] = _AddToken._objOut2.OppositeDesc2;
                   // dr["OppsitDesc3"] = _AddToken._objOut2.OppositeDesc3;
                    dr["OppsitLTP1"] = 0;
                    dr["OppsitLTP2"] = 0;
                    dr["OppsitLTP3"] = 0;
                    //  dr["ratio2"] = Convert.ToString(_AddToken._objOut2.ratio2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio2);


                    SpreadTable.Rows.Add(dr);
                    
                    //dr["BFSNDIFF"] =  0.0000;
                    //dr["BNSFDIFF"]=0.0000;
                    //dr["BNSFMNQ"] = 0.0000;
                    //dr["BFSNMNQ"] = 0.0000;
                    //dr["BNSFMXQ"] = 0.0000;
                    //dr["BFSNMXQ"] = 0.0000;

                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["BUYPRICE"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["SELLPRICE"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ORDQTY(B)"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ORDQTY(S)"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TOTALQTY(B)"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TOTALQTY(S)"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["PAYUPTIEKS"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["W_T_C"].Value = 0.00;


                    //UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token1;
                    //UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token2;
                    //UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token3;
                    //UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token4;


                  



                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token1;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token2;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token3;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token4;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken1;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken2;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken3;

                    //   FOPAIRDIFF

                    portFolioCounter++;

                }

            }

        
            
        
            
        }

        private void btnprofile_Click(object sender, EventArgs e)
        {
            var frmprf = new frmProfile();

            foreach (DataGridViewColumn dc in DGV1.Columns)
            {
             //   frmprf.lbxPrimary.Items.Add(dc.HeaderText);
                if (!frmprf.lbxSecondary.Items.Contains(dc.HeaderText))
                {
                    frmprf.lbxPrimary.Items.Add(dc.HeaderText);
                   
                }
               
            }
            if (frmprf.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewColumn dc in DGV1.Columns)
                {
                    this.DGV1.Columns[dc.HeaderText.Replace(" ", "")].Visible = true;
                }
                String GetProfileName = frmprf.GetProfileName();

                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + GetProfileName + ".xml");
                if (ds.Tables.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                     string st = ds.Tables[0].Rows[i]["Input"].ToString();
                    this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ","")].Visible = false;
                }
            }
            else
            {
                String GetProfileName = frmprf.GetProfileName();

                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MarketCol.xml");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string st = ds.Tables[0].Rows[i]["Input"].ToString();
                    this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ", "")].Visible = true;
                }
            }
           
        }

        delegate void OnDataArrivedDelegate(Object o, ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP> Stat);
        
        public void OnDataArrived(Object o, ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP> Stat)
        {
            try
            {
                if (DGV1.InvokeRequired)
                {
                    DGV1.Invoke(new OnDataArrivedDelegate(OnDataArrived), o, new ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP>(Stat.Parameter));
                    return;
                }
              var rowlist = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["Token1"]) ==IPAddress.HostToNetworkOrder(Stat.Parameter.Token) ||  Convert.ToInt32(x["Token2"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token)||Convert.ToInt32(x["Token3"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token)).ToList();//there is doubt in leg4.......
               // var rowlist = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["OppsitToken1"]) == Stat.Parameter.Token || Convert.ToInt32(x["OppsitToken2"]) == Stat.Parameter.Token || Convert.ToInt32(x["OppsitToken3"]) == Stat.Parameter.Token || Convert.ToInt32(x["Token1"]) == Stat.Parameter.Token || Convert.ToInt32(x["Token2"]) == Stat.Parameter.Token || Convert.ToInt32(x["Token3"]) == Stat.Parameter.Token).ToList();//there is doubt in leg4.......
                var rowlist2 = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["OppsitToken1"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token) || Convert.ToInt32(x["OppsitToken2"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token) || Convert.ToInt32(x["OppsitToken3"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token) );//there is doubt in leg4.......
            //  var rowlist2 = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["OppsitToken1"] == DBNull.Value ? 0 : x["OppsitToken1"] == "" ? 0 : x["OppsitToken1"]) == Stat.Parameter.Token).ToList();
                try
                {
                    foreach (var i in rowlist2)
                    {
                        if (DGV1.Rows.Count == 0)
                        {
                            return;
                        }

                        if (Convert.ToInt32(i["OppsitToken1"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token))
                        {
                            i["OppsitLTP1"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);

                        }
                        if (Convert.ToInt32(i["OppsitToken2"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token))
                        {
                            i["OppsitLTP2"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);
 
                        }
                        if (Convert.ToInt32(i["OppsitToken3"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token))
                        {
                            i["OppsitLTP3"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);

                        }
                                   //   ====================================================================================
                        string ExpiryDate = i["Expiry"].ToString();
                        int DTE = (Convert.ToDateTime(ExpiryDate) - DateTime.Today).Days;
                        decimal DTE_in_Years = DTE / 365.00m;

                        TimeSpan diff = DateTime.Parse(ExpiryDate) - DateTime.Now;



                        double Risk_free_rate = 0;
                        double Dividend_Yield = 0;




                        i["OI"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedCallVolatility(Convert.ToDouble(i["OppsitLTP1"].ToString()), Convert.ToDouble(i["StrikePrice"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["NLTP"].ToString()), Dividend_Yield));
                        i["ATP"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedPutVolatility(Convert.ToDouble(i["OppsitLTP1"].ToString()), Convert.ToDouble(i["StrikePrice"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["NLTP"].ToString()), Dividend_Yield));


                        var task4 = Task.Factory.StartNew(() => (i["OptionType"].ToString() == "CE" ? Convert.ToDouble(Delta_Cal.Instance.CallDelta(Convert.ToDouble(i["OppsitLTP1"].ToString()), Convert.ToDouble(i["StrikePrice"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(Delta_Cal.Instance.PutDelta(Convert.ToDouble(i["OppsitLTP1"].ToString()), Convert.ToDouble(i["StrikePrice"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Delta1"] = task4.Result;

                      //  Global.Instance.MTMDIct.AddOrUpdate(Stat.Parameter.Token,Convert.ToDouble(task4.Result), (k, v) => Convert.ToDouble(task4.Result));
                        //task4 = null;

                    //    Global.Instance.MTMDIct.AddOrUpdate(Convert.ToInt32(i["Token1"]), Convert.ToDouble(task4.Result), (k, v) => Convert.ToDouble(task4.Result));
                        //==================================================================  == = 

                        string ExpiryDate2 = i["Expiry2"].ToString();
                        int DTE2 = (Convert.ToDateTime(ExpiryDate2) - DateTime.Today).Days;
                        decimal DTE_in_Years2 = DTE2 / 365.00m;

                        TimeSpan diff2 = DateTime.Parse(ExpiryDate2) - DateTime.Now;



                        double Risk_free_rate2 = 0;
                        double Dividend_Yield2 = 0;




                        i["OI2"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedCallVolatility(Convert.ToDouble(i["OppsitLTP2"].ToString()), Convert.ToDouble(i["StrikePrice2"].ToString()), Convert.ToDouble(DTE_in_Years2), Risk_free_rate2, Convert.ToDouble(i["FLTP"].ToString()), Dividend_Yield2));
                        i["ATP2"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedPutVolatility(Convert.ToDouble(i["OppsitLTP2"].ToString()), Convert.ToDouble(i["StrikePrice2"].ToString()), Convert.ToDouble(DTE_in_Years2), Risk_free_rate2, Convert.ToDouble(i["FLTP"].ToString()), Dividend_Yield2));


                        var task5 = Task.Factory.StartNew(() => (i["OptionType2"].ToString() == "CE" ? Convert.ToDouble(Delta_Cal.Instance.CallDelta(Convert.ToDouble(i["OppsitLTP2"].ToString()), Convert.ToDouble(i["StrikePrice2"].ToString()), Convert.ToDouble(DTE_in_Years2), Risk_free_rate2, Convert.ToDouble(i["OI2"].ToString()), Dividend_Yield2)) : Convert.ToDouble(Delta_Cal.Instance.PutDelta(Convert.ToDouble(i["OppsitLTP2"].ToString()), Convert.ToDouble(i["StrikePrice2"].ToString()), Convert.ToDouble(DTE_in_Years2), Risk_free_rate2, Convert.ToDouble(i["ATP2"].ToString()), Dividend_Yield2))).ToString("f4"));
                        i["Delta2"] = task5.Result;
                        //task5.Dispose();
                       // Global.Instance.MTMDIct.AddOrUpdate(Stat.Parameter.Token, Convert.ToDouble(task5.Result), (k, v) => Convert.ToDouble(task5.Result));
                      //  Global.Instance.MTMDIct.AddOrUpdate(Convert.ToInt32(i["Token2"]), Convert.ToDouble(task5.Result), (k, v) => Convert.ToDouble(task5.Result));
                        //task5 = null;
                        //======== ====== ====== ======== ==== ======== ======== ========  ===== =====  == = 
                        if (i["Strategy_Type"].ToString() == "3_LEG")
                            
                        {
                        string ExpiryDate3 = i["Expiry3"].ToString();
                        int DTE3 = (Convert.ToDateTime(ExpiryDate3) - DateTime.Today).Days;
                        decimal DTE_in_Years3 = DTE3 / 365.00m;

                        TimeSpan diff3 = DateTime.Parse(ExpiryDate3) - DateTime.Now;



                        double Risk_free_rate3 = 0;
                        double Dividend_Yield3 = 0;




                        i["OI3"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedCallVolatility(Convert.ToDouble(i["OppsitLTP3"].ToString()), Convert.ToDouble(i["StrikePrice3"].ToString()), Convert.ToDouble(DTE_in_Years3), Risk_free_rate3, Convert.ToDouble(i["F_LTP"].ToString()), Dividend_Yield3));
                        i["ATP3"] = Convert.ToDouble(Delta_Cal.Instance.ImpliedPutVolatility(Convert.ToDouble(i["OppsitLTP3"].ToString()), Convert.ToDouble(i["StrikePrice3"].ToString()), Convert.ToDouble(DTE_in_Years3), Risk_free_rate3, Convert.ToDouble(i["F_LTP"].ToString()), Dividend_Yield3));


                        var task6 = Task.Factory.StartNew(() => (i["OptionType3"].ToString() == "CE" ? Convert.ToDouble(Delta_Cal.Instance.CallDelta(Convert.ToDouble(i["OppsitLTP3"].ToString()), Convert.ToDouble(i["StrikePrice3"].ToString()), Convert.ToDouble(DTE_in_Years3), Risk_free_rate3, Convert.ToDouble(i["OI3"].ToString()), Dividend_Yield3)) : Convert.ToDouble(Delta_Cal.Instance.PutDelta(Convert.ToDouble(i["OppsitLTP3"].ToString()), Convert.ToDouble(i["StrikePrice3"].ToString()), Convert.ToDouble(DTE_in_Years3), Risk_free_rate3, Convert.ToDouble(i["ATP3"].ToString()), Dividend_Yield3))).ToString("f4"));
                        i["Delta3"] = task6.Result;
                        //task6.Dispose();
                      ///  Global.Instance.MTMDIct.AddOrUpdate(Convert.ToInt32(i["Token3"]), Convert.ToDouble(task6.Result), (k, v) => Convert.ToDouble(task6.Result));
                        //task6 = null;
                        }
                        //=======  =================  =============  =============  ================  == = 

                    }
                  }
                
                catch (Exception Ex)
                {
                   // MessageBox.Show(" Exception " + Ex.StackTrace.ToString());
                }

                //===================================================================================================
                try
                {
                    
                    foreach (var i in rowlist)
                    {
                        if (DGV1.Rows.Count == 0)
                        {
                            return;
                        }
                        NetDelta ND;
                        if (Convert.ToInt32(i["Token1"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token))
                        {
                                    

                            i["NBID"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4);
                            i["NASK"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4);
                            i["NLTP"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);


                            i["t1"] = GetExpectedProdPrice(i["Tok1B_S"].ToString(), Stat, Convert.ToInt32(i["ratio1"]));
                            i["t3"] = GetExpectedProdPrice(i["Tok1B_S"].ToString(), Stat, Convert.ToInt32(i["ratio1"]), true);


                            i["tok1_cost"] = Convert.ToString(i["Tok1B_S"]) == "Buy" ?
                                i["tok1inst"].ToString() == "OPTIDX"
                                ? Convert.ToDouble(i["NASK"]) * Convert.ToInt32(i["ratio1"]) * 2 * 0.0007 :
                                
                                 Convert.ToDouble(i["NASK"]) * Convert.ToInt32(i["ratio1"]) * 2 * 0.00009 
                                :
                                //else

                                 i["tok1inst"].ToString() == "OPTIDX"
                                ? Convert.ToDouble(i["NBID"]) * Convert.ToInt32(i["ratio1"]) * 2 * 0.0007 :
                                
                                 Convert.ToDouble(i["NBID"]) * 2 * Convert.ToInt32(i["ratio1"]) * 0.00009;

                            

                          //  i["cost"] = Convert.ToDouble(i["tok1_cost"] == DBNull.Value ? "0" : i["tok1_cost"]) + Convert.ToDouble(i["tok2_cost"] == DBNull.Value ? "0" : i["tok2_cost"]) + Convert.ToDouble(i["tok3_cost"] == DBNull.Value ? "0" : i["tok3_cost"]);
                           // i["cost"] = Convert.ToDouble(i["tok1_cost"] == DBNull.Value ? "0" : i["tok1_cost"]) + Convert.ToDouble(i["tok2_cost"] == DBNull.Value ? "0" : i["tok2_cost"]);
                            i["NETDelta1"] = Delta_Cal.Instance.Get_NetDelta(Convert.ToDouble(i["Delta1"].ToString() == "NaN" ? "0" : i["Delta1"] == DBNull.Value ? "0" : i["Delta1"]), Convert.ToString(i["tok1inst"] == DBNull.Value ? "0" : i["tok1inst"]), Convert.ToDouble(i["ratio1"] == DBNull.Value ? "0" : i["ratio1"]), Convert.ToString(i["Tok1B_S"] == DBNull.Value ? "0" : i["Tok1B_S"]));

                            ND.Delta = Convert.ToDouble(i["Delta1"].ToString() == "NaN" ? "0" : i["Delta1"] == DBNull.Value ? "0" : i["Delta1"]);
                            ND.LTP = Convert.ToDouble(i["NLTP"].ToString() == "NaN" ? "0" : i["NLTP"] == DBNull.Value ? "0" : i["NLTP"]);
                            Global.Instance.ltp.AddOrUpdate(Convert.ToInt32(i["Token1"]), ND, (k, v) => ND);
                            var obj = new LTPTONETBOOK();
                            obj.TOKEN = Stat.Parameter.Token;
                            obj.LTP = Stat.Parameter.LastTradedPrice;
                            obj.DELTA = ND.Delta;
                            FOR_Net_BOOK.Raise(FOR_Net_BOOK, FOR_Net_BOOK.CreateReadOnlyArgs(obj));

                        }
                        if (Convert.ToInt32(i["Token2"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token))
                        {

                            i["FBID"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4);
                            i["FASK"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4);
                            i["FLTP"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);

                            i["t2"] = GetExpectedProdPrice(i["Tok2B_S"].ToString(), Stat, Convert.ToInt32(i["ratio2"]));
                            i["t4"] = GetExpectedProdPrice(i["Tok2B_S"].ToString(), Stat, Convert.ToInt32(i["ratio2"]), true);
                           
                            i["tok2_cost"] = Convert.ToString(i["Tok2B_S"]) == "Buy" ?
                            i["tok2inst"].ToString() == "OPTIDX"
                              ? Convert.ToDouble(i["FASK"]) * Convert.ToInt32(i["ratio2"]) * 2 * 0.0007

                              : Convert.ToDouble(i["FASK"]) * Convert.ToInt32(i["ratio2"]) * 2 * 0.00009 :
                                //else

                                 i["tok2inst"].ToString() == "OPTIDX"
                                ? Convert.ToDouble(i["FBID"]) * Convert.ToInt32(i["ratio2"]) * 2 * 0.0007

                                : Convert.ToDouble(i["FBID"]) * Convert.ToInt32(i["ratio2"]) * 2 * 0.00009;


                          //  i["cost"] = Convert.ToDouble(i["tok1_cost"] == DBNull.Value ? "0" : i["tok1_cost"]) + Convert.ToDouble(i["tok2_cost"] == DBNull.Value ? "0" : i["tok2_cost"]) + Convert.ToDouble(i["tok3_cost"] == DBNull.Value ? "0" : i["tok3_cost"]);
                            //i["cost"] = Convert.ToDouble(i["tok1_cost"] == DBNull.Value ? "0" : i["tok1_cost"]) + Convert.ToDouble(i["tok2_cost"] == DBNull.Value ? "0" : i["tok2_cost"]);

                            i["NETDelta2"] = Delta_Cal.Instance.Get_NetDelta(Convert.ToDouble(i["Delta2"].ToString() == "NaN" ? "0" : i["Delta2"] == DBNull.Value ? "0" : i["Delta2"]), Convert.ToString(i["tok2inst"] == DBNull.Value ? "0" : i["tok2inst"]), Convert.ToDouble(i["ratio2"] == DBNull.Value ? "0" : i["ratio2"]), Convert.ToString(i["Tok2B_S"] == DBNull.Value ? "0" : i["Tok2B_S"]));

                            ND.Delta = Convert.ToDouble(i["Delta2"].ToString() == "NaN" ? "0" : i["Delta2"] == DBNull.Value ? "0" : i["Delta2"]);
                            ND.LTP = Convert.ToDouble(i["FLTP"].ToString() == "NaN" ? "0" : i["FLTP"] == DBNull.Value ? "0" : i["FLTP"]);
                            Global.Instance.ltp.AddOrUpdate(Convert.ToInt32(i["Token2"]), ND, (k, v) => ND);

                            var obj = new LTPTONETBOOK();
                            obj.TOKEN = Stat.Parameter.Token;
                            obj.LTP = Stat.Parameter.LastTradedPrice;
                            obj.DELTA = ND.Delta;
                            FOR_Net_BOOK.Raise(FOR_Net_BOOK, FOR_Net_BOOK.CreateReadOnlyArgs(obj));


                        }

                        if (Convert.ToInt32(i["Token3"]) == IPAddress.HostToNetworkOrder( Stat.Parameter.Token))
                        {
                            i["F_BID"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4);
                            i["F_ASK"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4);
                            i["F_LTP"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);

                            i["t5"] = GetExpectedProdPrice(i["Tok3B_S"].ToString(), Stat, Convert.ToInt32(i["ratio3"]));
                            i["t6"] = GetExpectedProdPrice(i["Tok3B_S"].ToString(), Stat, Convert.ToInt32(i["ratio3"]), true);


                            i["tok3_cost"] = Convert.ToString(i["Tok3B_S"]) == "Buy" ?
                            i["tok3inst"].ToString() == "OPTIDX"
                              ? Convert.ToDouble(i["F_ASK"]) * Convert.ToInt32(i["ratio3"]) * 2 * 0.0007 
                             
                              : Convert.ToDouble(i["F_ASK"]) * Convert.ToInt32(i["ratio3"]) * 2 * 0.00009 :
                                //else

                                 i["tok3inst"].ToString() == "OPTIDX" 
                                ? Convert.ToDouble(i["F_BID"]) * Convert.ToInt32(i["ratio3"]) * 2 * 0.0007 
                               
                                : Convert.ToDouble(i["F_BID"]) * Convert.ToInt32(i["ratio3"]) * 2 * 0.00009;

                                   i["NETDelta3"] = Delta_Cal.Instance.Get_NetDelta(Convert.ToDouble(i["Delta3"].ToString() == "NaN" ? 1 : i["Delta3"] == DBNull.Value ? "0" : i["Delta3"]), Convert.ToString(i["tok3inst"] == DBNull.Value ? "0" : i["tok3inst"]), Convert.ToDouble(i["ratio3"] == DBNull.Value ? "0" : i["ratio3"]), Convert.ToString(i["Tok3B_S"] == DBNull.Value ? "0" : i["Tok3B_S"]));

                                   ND.Delta = Convert.ToDouble(i["Delta3"].ToString() == "NaN" ? "0" : i["Delta3"] == DBNull.Value ? "0" : i["Delta3"]);
                                   ND.LTP = Convert.ToDouble(i["F_LTP"].ToString() == "NaN" ? "0" : i["F_LTP"] == DBNull.Value ? "0" : i["F_LTP"]);
                                   Global.Instance.ltp.AddOrUpdate(Convert.ToInt32(i["Token3"]), ND, (k, v) => ND);

                                   var obj = new LTPTONETBOOK();
                                   obj.TOKEN = Stat.Parameter.Token;
                                   obj.LTP = Stat.Parameter.LastTradedPrice;
                                   obj.DELTA = ND.Delta;
                                   FOR_Net_BOOK.Raise(FOR_Net_BOOK, FOR_Net_BOOK.CreateReadOnlyArgs(obj));


                        }
                               i["cost"] = Convert.ToDouble(i["tok1_cost"] == DBNull.Value ? "0" : i["tok1_cost"]) + Convert.ToDouble(i["tok2_cost"] == DBNull.Value ? "0" : i["tok2_cost"]) + Convert.ToDouble(i["tok3_cost"] == DBNull.Value ? "0" : i["tok3_cost"]);
                     
                        
                        i["CMP(B)"] =
                           Convert.ToDouble(i["t1"] == DBNull.Value ? "0" : i["t1"]) + Convert.ToDouble(i["t2"] == DBNull.Value ? "0" : i["t2"]) + Convert.ToDouble(i["t5"] == DBNull.Value ? "0" : i["t5"]);
                        i["CMP(S)"] = Convert.ToDouble(i["t4"] == DBNull.Value ? "0" : i["t4"]) + Convert.ToDouble(i["t3"] == DBNull.Value ? "0" : i["t3"])+ Convert.ToDouble(i["t6"] == DBNull.Value ? "0" : i["t6"]);

                       // Global.Instance.MTMDIct.AddOrUpdate(Stat.Parameter.Token, Stat.Parameter.LTP, (k, v) => Stat.Parameter.LTP);

                        i["NETDelta"] = Math.Round( Delta_Cal.Instance.GetNetDelta(Convert.ToDouble(i["NETDelta1"].ToString() == "NaN" ? "0" : i["NETDelta1"] == DBNull.Value ? "0" : i["NETDelta1"]), Convert.ToDouble(i["NETDelta2"].ToString() == "NaN" ? "0" : i["NETDelta2"] == DBNull.Value ? "0" : i["NETDelta2"]), Convert.ToDouble(i["NETDelta3"].ToString() == "NaN" ? "0" : i["NETDelta3"] == DBNull.Value ? "0" : i["NETDelta3"])),4);


                      
                       }

                   
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(" Exception " + Ex.StackTrace.ToString());
                }


            }
            catch (DataException a)
            {
                MessageBox.Show("From Live Data fill " + Environment.NewLine + a.Message);
            }
        }
        private double GetExpectedProdPrice(string BS, ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP> Stat, int Ratio, bool reverse = false)
        {

            double RetVal = 0;

            if (!reverse)
            {
                //THis case calculates the price to generate buy spread
                //Math.Round(Convert.ToDouble(Stat.Parameter.MAXBID) / 100, 4)

                RetVal = BS == "Buy" ? (Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4) * Ratio * -1) : (Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4) * Ratio);
            }
            else
            {
                // Here in case of sale actual stg with buy mode token will be sold just to make a complete trade
                RetVal = BS == "Buy" ? (Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4) * Ratio) : (Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4) * Ratio * -1);
            }
            return RetVal;

        }

        


       
        private void Fo_Fo_mktwatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((ThreadStart)delegate() { applyFun(); });

                return;
            }
            else
            {
                applyFun();
            }
           
            //if (DGV1.Rows.Count > 0)
            //{
            //    if (MessageBox.Show("Want To Save This portfolio", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            //    {

            //        btnsaveMktwatch_Click(sender, e);
            //    }
            //}
            //this.Dispose();
            //e.Cancel = true;
            //this.Hide();
        }


        private void applyFun()
        {
            if (DGV1.Rows.Count == 0)
                return;

            SpreadTable = (DataTable)DGV1.DataSource;
            SpreadTable.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "IOCDefault.xml");

            DataTable dt_save = new DataTable("lastvalue");
            dt_save.Columns.Add("ORDQTY(B)", typeof(Double));
            dt_save.Columns.Add("TOTALQTY(B)", typeof(Double));
            dt_save.Columns.Add("BUYPRICE", typeof(Double));
            dt_save.Columns.Add("SELLPRICE", typeof(Double));
            dt_save.Columns.Add("ORDQTY(S)", typeof(Double));
            dt_save.Columns.Add("TOTALQTY(S)", typeof(Double));
            dt_save.Columns.Add("PAYUPTIEKS", typeof(Double));

            dt_save.Columns.Add("W_T_C", typeof(Double));

            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataRow dRow = dt_save.NewRow();
                dRow["ORDQTY(B)"] = row.Cells["ORDQTY(B)"].Value == null ? 0 : row.Cells["ORDQTY(B)"].Value;
                dRow["TOTALQTY(B)"] = row.Cells["TOTALQTY(B)"].Value == null ? 0 :row.Cells["TOTALQTY(B)"].Value;
                dRow["BUYPRICE"] = row.Cells["BUYPRICE"].Value == null ? 0 :row.Cells["BUYPRICE"].Value;
                dRow["SELLPRICE"] = row.Cells["SELLPRICE"].Value == null ? 0 : row.Cells["SELLPRICE"].Value;
                dRow["ORDQTY(S)"] = row.Cells["ORDQTY(S)"].Value == null ? 0 : row.Cells["ORDQTY(S)"].Value;
                dRow["TOTALQTY(S)"] = row.Cells["TOTALQTY(S)"].Value == null ? 0 :row.Cells["TOTALQTY(S)"].Value;
                dRow["PAYUPTIEKS"] = row.Cells["PAYUPTIEKS"].Value == null ? 0 : row.Cells["PAYUPTIEKS"].Value;
                dRow["W_T_C"] = row.Cells["W_T_C"].Value == null ? 0 : row.Cells["W_T_C"].Value;

                dt_save.Rows.Add(dRow);
            }
            string p_last = Application.StartupPath + Path.DirectorySeparatorChar + "Lastvalue1.xml";
            dt_save.WriteXml(p_last);
           // _inifile.IniWriteValue("MasterPath", "LastUpdate", p_last);
        }

        private void btnsaveMktwatch_Click(object sender, EventArgs e)
        {
            if (DGV1.Rows.Count == 0)
                return;
            SaveFileDialog savd = new SaveFileDialog();
            savd.AddExtension = true;
            savd.DefaultExt = "xml";
            savd.Filter = "*.xml|*.*";
            if(savd.ShowDialog() == DialogResult.OK)
            {
                DataTable dsave = (DataTable)DGV1.DataSource;
                dsave.WriteXml(savd.FileName);

            }
            DataTable dt_save = new DataTable("lastvalue");
            dt_save.Columns.Add("ORDQTY(B)", typeof(Double));
            dt_save.Columns.Add("TOTALQTY(B)", typeof(Double));
            dt_save.Columns.Add("BUYPRICE", typeof(Double));
            dt_save.Columns.Add("SELLPRICE", typeof(Double));
            dt_save.Columns.Add("ORDQTY(S)", typeof(int));
            dt_save.Columns.Add("TOTALQTY(S)", typeof(int));
            dt_save.Columns.Add("PAYUPTIEKS", typeof(Int16));
            dt_save.Columns.Add("W_T_C", typeof(Int16));
            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataRow dRow = dt_save.NewRow();
              dRow["ORDQTY(B)"] = row.Cells["ORDQTY(B)"].Value==null?0:Convert.ToDouble(row.Cells["ORDQTY(B)"].Value);
              dRow["TOTALQTY(B)"] = row.Cells["TOTALQTY(B)"].Value == null ? 0 : Convert.ToDouble(row.Cells["TOTALQTY(B)"].Value);
              dRow["BUYPRICE"] = row.Cells["BUYPRICE"].Value == null ? 0 : Convert.ToDouble(row.Cells["BUYPRICE"].Value);
              dRow["SELLPRICE"] = row.Cells["SELLPRICE"].Value == null ? 0 : Convert.ToDouble(row.Cells["SELLPRICE"].Value);
              dRow["ORDQTY(S)"] = row.Cells["ORDQTY(S)"].Value == null ? 0 : Convert.ToInt32(row.Cells["ORDQTY(S)"].Value);
              dRow["TOTALQTY(S)"] = row.Cells["TOTALQTY(S)"].Value == null ? 0 : Convert.ToInt32(row.Cells["TOTALQTY(S)"].Value);
              dRow["PAYUPTIEKS"] = row.Cells["PAYUPTIEKS"].Value == null ? 0 : Convert.ToInt16(row.Cells["PAYUPTIEKS"].Value);
              dRow["W_T_C"] = row.Cells["W_T_C"].Value == null ? 0 : Convert.ToInt16(row.Cells["W_T_C"].Value);
                
                dt_save.Rows.Add(dRow);
            }
            string p_last = Application.StartupPath + Path.DirectorySeparatorChar + "Lastvalue1.xml";
            dt_save.WriteXml(p_last);
            _inifile.IniWriteValue("MasterPath","LastUpdate",p_last ) ;

        }
        
        private void btnLoadMktWatch_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            if (opn.ShowDialog() == DialogResult.OK)
            {
                SpreadTable.Clear();
                DataSet ds_set = new DataSet();
                ds_set.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Lastvalue1.xml");
                SpreadTable.ReadXml(opn.FileName);
                //  SpreadTable.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "FOWATCH.xml");
                for (int i = 0; i < ds_set.Tables[0].Rows.Count; i++)
                {
                    DGV1.Rows[i].Cells["ORDQTY(B)"].Value = ds_set.Tables[0].Rows[i]["ORDQTY(B)"] == null ? 0 : ds_set.Tables[0].Rows[i]["ORDQTY(B)"];
                    DGV1.Rows[i].Cells["TOTALQTY(B)"].Value = ds_set.Tables[0].Rows[i]["TOTALQTY(B)"] == null ? 0 : ds_set.Tables[0].Rows[i]["TOTALQTY(B)"];
                    DGV1.Rows[i].Cells["BUYPRICE"].Value = ds_set.Tables[0].Rows[i]["BUYPRICE"] == null ? 0 : ds_set.Tables[0].Rows[i]["BUYPRICE"]; 
                    DGV1.Rows[i].Cells["SELLPRICE"].Value = ds_set.Tables[0].Rows[i]["SELLPRICE"] == null ? 0 : ds_set.Tables[0].Rows[i]["SELLPRICE"];
                    DGV1.Rows[i].Cells["ORDQTY(S)"].Value = ds_set.Tables[0].Rows[i]["ORDQTY(S)"] == null ? 0 : ds_set.Tables[0].Rows[i]["ORDQTY(S)"];
                    DGV1.Rows[i].Cells["TOTALQTY(S)"].Value = ds_set.Tables[0].Rows[i]["TOTALQTY(S)"] == null ? 0 : ds_set.Tables[0].Rows[i]["TOTALQTY(S)"];
                    DGV1.Rows[i].Cells["PAYUPTIEKS"].Value = ds_set.Tables[0].Rows[i]["PAYUPTIEKS"] == null ? 0 : ds_set.Tables[0].Rows[i]["PAYUPTIEKS"];
                    DGV1.Rows[i].Cells["W_T_C"].Value = ds_set.Tables[0].Rows[i]["W_T_C"] == null ? 0 : ds_set.Tables[0].Rows[i]["W_T_C"];

                }
            }

           // _SelectionOut _objOut = new _SelectionOut();

            for (int i = 0; i <SpreadTable.Rows.Count ; i++)
            {
               // UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
              //  UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());

                LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
                LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());
                LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token3"].ToString());
                LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token4"].ToString());

                portFolioCounter++;

            }
            if(DGV1.Rows.Count==0)
            { return; }
            portFolioCounter  = Convert.ToInt32( SpreadTable.Compute("MAX(PF)", ""))+1;
          
        }
        public static int[] LoadFormLocationAndSize(Form xForm)
        {
            int[] t = { 0, 0, 900, 300 };
            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "formclose1.xml"))
                return t;
            DataSet dset = new DataSet();
            dset.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "formclose1.xml");
            int[] LocationAndSize = new int[] { xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height };

            try
            {
                var AbbA = dset.Tables[0].Rows[0]["Input"].ToString().Split(';');
                //---//
                LocationAndSize[0] = Convert.ToInt32(AbbA[0]);
                LocationAndSize[1] = Convert.ToInt32(AbbA[1]);
                LocationAndSize[2] = Convert.ToInt32(AbbA[2]);
                LocationAndSize[3] = Convert.ToInt32(AbbA[3]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //---//
            return LocationAndSize;
        }

        public static void SaveFormLocationAndSize(object sender, FormClosingEventArgs e)
        {


            Form xForm = sender as Form;

            //   ini.IniWriteValue("FOFOFORM","Location", String.Format("{0};{1};{2};{3}", xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height));

            var settings = new XmlWriterSettings { Indent = true };

            XmlWriter writer = XmlWriter.Create(Application.StartupPath + Path.DirectorySeparatorChar + "formclose1.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Columns");

            string encodedXml = String.Format("{0};{1};{2};{3}", xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height);
            writer.WriteStartElement("Column");
            writer.WriteAttributeString("Input", encodedXml);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

        }
     
        private void Fo_Fo_mktwatch_Load(object sender, EventArgs e)
        {

            var AbbA = LoadFormLocationAndSize(this);
            this.Location = new Point(AbbA[0], AbbA[1]);
            this.Size = new Size(AbbA[2], AbbA[3]);

            this.FormClosing += new FormClosingEventHandler(SaveFormLocationAndSize);


            _makeItRed = new DataGridViewCellStyle();
            _makeItBlue = new DataGridViewCellStyle();
            _makeItBlack = new DataGridViewCellStyle();

            _makeItRed.BackColor = Color.Red;

            _makeItBlue.BackColor = Color.Blue;
            _makeItBlack.BackColor = Color.Black;
            SpreadTable = new DataTable("SPREADFO");


            SpreadTable.Columns.Add("PF", typeof(String));
            SpreadTable.Columns.Add("Expiry", typeof(string));
            SpreadTable.Columns.Add("Expiry2", typeof(string));
            SpreadTable.Columns.Add("Expiry3", typeof(string));
            SpreadTable.Columns.Add("Symbol1", typeof(string));
            SpreadTable.Columns.Add("Symbol2", typeof(string));
            SpreadTable.Columns.Add("Symbol3", typeof(string));
            SpreadTable.Columns.Add("StrikePrice", typeof(string));
            SpreadTable.Columns.Add("StrikePrice2", typeof(string));
            SpreadTable.Columns.Add("StrikePrice3", typeof(string));
            SpreadTable.Columns.Add("OptionType", typeof(string));
            SpreadTable.Columns.Add("OptionType2", typeof(string));
            SpreadTable.Columns.Add("OptionType3", typeof(string));
            SpreadTable.Columns.Add("ratio1", typeof(Int32));
            SpreadTable.Columns.Add("ratio2", typeof(Int32));
            SpreadTable.Columns.Add("ratio3", typeof(Int32));
            SpreadTable.Columns.Add("ratio4", typeof(Int32));
            SpreadTable.Columns.Add("CMP(B)", typeof(Double));//, "FASK-NASK");
            SpreadTable.Columns.Add("CMP(S)", typeof(Double));//, "FBID-NBID");
            SpreadTable.Columns.Add("cost", typeof(double));

            SpreadTable.Columns.Add("Token1", typeof(Int32));
            SpreadTable.Columns.Add("Token2", typeof(Int32));
            SpreadTable.Columns.Add("Token3", typeof(Int32));
            SpreadTable.Columns.Add("Token4", typeof(Int32));

            SpreadTable.Columns.Add("Tok1B_S", typeof(string));
            SpreadTable.Columns.Add("Tok2B_S", typeof(string));
            SpreadTable.Columns.Add("Tok3B_S", typeof(string));
            SpreadTable.Columns.Add("Tok4B_S", typeof(string));
            SpreadTable.Columns.Add("NEAR", typeof(String));
            SpreadTable.Columns.Add("FAR", typeof(String));
            SpreadTable.Columns.Add("tok3", typeof(String));
            SpreadTable.Columns.Add("NBID", typeof(Double));
            SpreadTable.Columns.Add("NASK", typeof(Double));
            SpreadTable.Columns.Add("NLTP", typeof(Double));
            SpreadTable.Columns.Add("FBID", typeof(Double));
            SpreadTable.Columns.Add("FASK", typeof(Double));
            SpreadTable.Columns.Add("FLTP", typeof(Double));
            SpreadTable.Columns.Add("SPREADBUY", typeof(Double));
            SpreadTable.Columns.Add("SPREADSELL", typeof(Double));

            SpreadTable.Columns.Add("tok1_cost", typeof(double));
            SpreadTable.Columns.Add("tok2_cost", typeof(double));
            SpreadTable.Columns.Add("tok3_cost", typeof(double));
         
            SpreadTable.Columns.Add("tok1inst", typeof(string));
            SpreadTable.Columns.Add("tok2inst", typeof(string));
            SpreadTable.Columns.Add("tok3inst", typeof(string));

            SpreadTable.Columns.Add("F_BID", typeof(Double));
            SpreadTable.Columns.Add("F_ASK", typeof(Double));
            SpreadTable.Columns.Add("F_LTP", typeof(Double));

          

            SpreadTable.Columns.Add("t1", typeof(double));
            SpreadTable.Columns.Add("t2", typeof(double));
            SpreadTable.Columns.Add("t3", typeof(double));
            SpreadTable.Columns.Add("t4", typeof(double));
            SpreadTable.Columns.Add("t5", typeof(double));
            SpreadTable.Columns.Add("t6", typeof(double));

            SpreadTable.Columns.Add("Calc_type", typeof(string));

            SpreadTable.Columns.Add("BUY_Price", typeof(Int32));//, "(NBID*ratio1) + (FBID*ratio2)+(F_BID*ratio3)-(NASK*ratio1)+(FASK*ratio2)+(F_ASK*ratio3)");
            // SpreadTable.Columns.Add("BUY_Price", typeof(Int32), "((NBID*ratio1) + (FBID*ratio2))-((NASK*ratio1)+(FASK*ratio2))");
          
            SpreadTable.Columns.Add("NHD", typeof(Double), "FBID -NASK");
           
            SpreadTable.Columns.Add("FHD", typeof(Double), "FASK-NBID");
            SpreadTable.Columns.Add("BUY_price_leg2", typeof(Double));

            SpreadTable.Columns.Add("Order_Type", typeof(string));
            SpreadTable.Columns.Add("First_LEG", typeof(string));
            SpreadTable.Columns.Add("OPT_TICK", typeof(string));
            SpreadTable.Columns.Add("ORDER_DEPTH", typeof(string));
            SpreadTable.Columns.Add("Second_Leg", typeof(string));
            SpreadTable.Columns.Add("THRESHOLD", typeof(string));
            SpreadTable.Columns.Add("BIDDING_RANGE", typeof(string));
            SpreadTable.Columns.Add("ReqConnt", typeof(string));

            SpreadTable.Columns.Add("Strategy_Type", typeof(string));
//============================================================================================================================

            SpreadTable.Columns.Add("OppsitToken1", typeof(Int32));
            SpreadTable.Columns.Add("OppsitToken2", typeof(Int32));
            SpreadTable.Columns.Add("OppsitToken3", typeof(Int32));

           // SpreadTable.Columns.Add("OppsitDesc1", typeof(string));
           // SpreadTable.Columns.Add("OppsitDesc2", typeof(string));
           // SpreadTable.Columns.Add("OppsitDesc3", typeof(string));

            SpreadTable.Columns.Add("Delta1", typeof(string));
            SpreadTable.Columns.Add("Delta2", typeof(string));
            SpreadTable.Columns.Add("Delta3", typeof(string));
            SpreadTable.Columns.Add("OI", typeof(string));
            SpreadTable.Columns.Add("ATP", typeof(string));
            SpreadTable.Columns.Add("OI2", typeof(string));
            SpreadTable.Columns.Add("ATP2", typeof(string));
            SpreadTable.Columns.Add("OI3", typeof(string));
            SpreadTable.Columns.Add("ATP3", typeof(string));

            SpreadTable.Columns.Add("OppsitLTP1", typeof(string));
            SpreadTable.Columns.Add("OppsitLTP2", typeof(string));
            SpreadTable.Columns.Add("OppsitLTP3", typeof(string));

            SpreadTable.Columns.Add("NETDelta1", typeof(string));
            SpreadTable.Columns.Add("NETDelta2", typeof(string));
            SpreadTable.Columns.Add("NETDelta3", typeof(string));
            SpreadTable.Columns.Add("NETDelta", typeof(string));

            SpreadTable.Columns.Add("TRP1", typeof(double));
            SpreadTable.Columns.Add("TRP2", typeof(double));
            SpreadTable.Columns.Add("TRP3", typeof(double));

            SpreadTable.Columns.Add("TRP11", typeof(double));
            SpreadTable.Columns.Add("TRP22", typeof(double));
            SpreadTable.Columns.Add("TRP33", typeof(double));


            DGV1.DataSource = SpreadTable;
            //DGV1.Columns["Order_Type"].Visible = false;
            //DGV1.Columns["First_LEG"].Visible = false;
            //DGV1.Columns["OPT_TICK"].Visible = false;
            //DGV1.Columns["ORDER_DEPTH"].Visible = false;
            //DGV1.Columns["Second_Leg"].Visible = false;
            //DGV1.Columns["THRESHOLD"].Visible = false;
            //DGV1.Columns["BIDDING_RANGE"].Visible = false;
            //DGV1.Columns["Strategy_Type"].Visible = false;
            //DGV1.Columns["Symbol1"].Visible = false;
            //DGV1.Columns["Symbol2"].Visible = false;
            //DGV1.Columns["OptionType"].Visible = false;
            //DGV1.Columns["OptionType2"].Visible = false;
            //DGV1.Columns["Expiry"].Visible = false;
            //DGV1.Columns["Expiry2"].Visible = false;
            //DGV1.Columns["StrikePrice"].Visible = false;
            //DGV1.Columns["StrikePrice2"].Visible = false;
            DGV1.Columns["ReqConnt"].Visible = false;
            DGV1.Columns["Expiry3"].Visible = false;
            DGV1.Columns["Symbol3"].Visible = false;
            DGV1.Columns["OptionType3"].Visible = false;
            DGV1.Columns["StrikePrice3"].Visible = false;

            DGV1.Columns["Token1"].Visible = false;
            DGV1.Columns["Token2"].Visible = false;

            DGV1.Columns["Token3"].Visible = false;
            DGV1.Columns["Token4"].Visible = false;

            DGV1.Columns["t1"].Visible = false;
            DGV1.Columns["t2"].Visible = false;
            DGV1.Columns["t3"].Visible = false;
            DGV1.Columns["t4"].Visible = false;
            DGV1.Columns["t5"].Visible = false;
            DGV1.Columns["t6"].Visible = false;


            DGV1.Columns["FAR"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NEAR"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NBID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["tok3"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NBID"].SortMode = DataGridViewColumnSortMode.NotSortable;

            DGV1.Columns["PF"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["FLTP"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["CMP(B)"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["CMP(S)"].SortMode = DataGridViewColumnSortMode.NotSortable;

            DGV1.Columns["NHD"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["FHD"].SortMode = DataGridViewColumnSortMode.NotSortable;


            SetDisplayRules(this.DGV1.Columns["PF"], "PF");

            SetDisplayRules(this.DGV1.Columns["NEAR"], "NEAR");


            SetDisplayRules(this.DGV1.Columns["FAR"], "FAR");
            SetDisplayRules(this.DGV1.Columns["NBID"], "N BID");
            SetDisplayRules(this.DGV1.Columns["NASK"], "N ASK");
            SetDisplayRules(this.DGV1.Columns["NLTP"], "N LTP");
            SetDisplayRules(this.DGV1.Columns["FBID"], "F BID");
            SetDisplayRules(this.DGV1.Columns["FASK"], "F ASK");   // Token2Ask
            SetDisplayRules(this.DGV1.Columns["FLTP"], "F LTP");   // Token2Ltp

            SetDisplayRules(this.DGV1.Columns["CMP(B)"], "CMP(B)");   //NearBidDiff
            SetDisplayRules(this.DGV1.Columns["NHD"], "NHD");   // NearHitDiff
            SetDisplayRules(this.DGV1.Columns["CMP(S)"], "CMP(S)");   // FarBidDiff
            SetDisplayRules(this.DGV1.Columns["FHD"], "FHD");   //FarHitDiff

            this.DGV1.Columns["CMP(B)"].DefaultCellStyle.Format = "0.00##";
            this.DGV1.Columns["NHD"].DefaultCellStyle.Format = "0.0#";
            this.DGV1.Columns["CMP(S)"].DefaultCellStyle.Format = "0.00##";
            this.DGV1.Columns["FHD"].DefaultCellStyle.Format = "0.0#";
            this.DGV1.Columns["cost"].DefaultCellStyle.Format = "0.0#";
            this.DGV1.Columns["tok1_cost"].DefaultCellStyle.Format = "0.0#";
            this.DGV1.Columns["tok2_cost"].DefaultCellStyle.Format = "0.0#";
            this.DGV1.Columns["tok3_cost"].DefaultCellStyle.Format = "0.0#";
           

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ORDQTY(B)",
                HeaderText = "ORDQTY(B)",


            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TOTALQTY(B)",
                HeaderText = "TOTALQTY(B)",


            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BUYPRICE",
                HeaderText = "BUYPRICE",

            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SELLPRICE",
                HeaderText = "SELLPRICE",

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TOTALQTY(S)",

                HeaderText = "TOTALQTY(S)",

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ORDQTY(S)",
                HeaderText = "ORDQTY(S)",

            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PAYUPTIEKS",
                HeaderText = "PAYUPTIEKS",

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "W_T_C",
                HeaderText = "W_T_C",

            });

            //////////////////////////////////////////////////////////////////////////////////////////////////////


            this.DGV1.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Apply",
                HeaderText = "Apply",
                Text = "Apply",
                UseColumnTextForButtonValue = true

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TRDQTY(B)",
                HeaderText = "TRDQTY(B)",
                ReadOnly = true
            }
           );
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TRDQTY(S)",
                HeaderText = "TRDQTY(S)",
                ReadOnly = true
            }
              );


            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ATP(B)",
                HeaderText = "ATP(B)",
                ReadOnly = true
            }
               );

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ATP(S)",
                HeaderText = "ATP(S)",
                ReadOnly = true
            }
           );
            //==============================================================     ============================================================= ----  ---

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "WTC",
                HeaderText = "WTC",

            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "_con_WTC",
                HeaderText = "_con_WTC",

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "_sell_WTC",
                HeaderText = "_sell_WTC",

            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "_sell_con_WTC",
                HeaderText = "_sell_con_WTC",

            });
            DGV1.Columns["WTC"].DefaultCellStyle.NullValue = 0;
            DGV1.Columns["_con_WTC"].DefaultCellStyle.NullValue = 0;
            DGV1.Columns["_sell_WTC"].DefaultCellStyle.NullValue = 0;
            DGV1.Columns["_sell_con_WTC"].DefaultCellStyle.NullValue = 0;


            DGV1.Columns["WTC"].ReadOnly = true;
            DGV1.Columns["_con_WTC"].ReadOnly = true;
            DGV1.Columns["_sell_WTC"].ReadOnly = true;
            DGV1.Columns["_sell_con_WTC"].ReadOnly = true;

          
            //===================================================================================================================


            /* DGV1.Columns["BUYPRICE"].DefaultCellStyle.NullValue = 0.00;
              DGV1.Columns["SELLPRICE"].DefaultCellStyle.NullValue = 0.00;
              DGV1.Columns["ORDQTY(B)"].DefaultCellStyle.NullValue = 0.00;
              DGV1.Columns["ORDQTY(S)"].DefaultCellStyle.NullValue = 0.00;
              DGV1.Columns["TOTALQTY(B)"].DefaultCellStyle.NullValue = 0.00;
              DGV1.Columns["TOTALQTY(S)"].DefaultCellStyle.NullValue = 0.00;*/




            //DGV1.Columns["BUY_Price"].ReadOnly = true;

            _makeItRed = new DataGridViewCellStyle();
            _makeItBlue = new DataGridViewCellStyle();
            _makeItBlack = new DataGridViewCellStyle();

            _makeItRed.BackColor = Color.Red;

            _makeItBlue.BackColor = Color.Blue;
            _makeItBlack.BackColor = Color.Black;
            SpreadTable.TableNewRow += new DataTableNewRowEventHandler(SpreadTable_NewRow);
            //  NNFHandler.eOrderTRADE_ERROR += Fillqty_ingrd;  

            btnStopAll.Enabled = true;
            btnStartAll.Enabled = true;

            // read only columns 
            /*  DGV1.Columns["FAR"].ReadOnly = true;
              DGV1.Columns["NEAR"].ReadOnly = true;
              DGV1.Columns["NBID"].ReadOnly = true;
              DGV1.Columns["tok3"].ReadOnly = true;
              DGV1.Columns["NBID"].ReadOnly = true;

              DGV1.Columns["PF"].ReadOnly = true;
              DGV1.Columns["FLTP"].ReadOnly = true;
              DGV1.Columns["CMP(B)"].ReadOnly = true;
              DGV1.Columns["CMP(S)"].ReadOnly = true;

              DGV1.Columns["NHD"].ReadOnly = true;
              DGV1.Columns["FHD"].ReadOnly = true;
              */

            DGV1.Columns["ratio1"].ReadOnly = true;
            DGV1.Columns["ratio2"].ReadOnly = true;
            DGV1.Columns["ratio3"].ReadOnly = true;
            DGV1.Columns["ratio4"].ReadOnly = true;

            DGV1.Columns["Cost"].ReadOnly = true;
            DGV1.Columns["Calc_type"].ReadOnly = true;

            DGV1.Columns["Order_Type"].ReadOnly = true;
            DGV1.Columns["First_LEG"].ReadOnly = true;
            DGV1.Columns["OPT_TICK"].ReadOnly = true;
            DGV1.Columns["ORDER_DEPTH"].ReadOnly = true;
            DGV1.Columns["Second_Leg"].ReadOnly = true;
            DGV1.Columns["THRESHOLD"].ReadOnly = true;
            DGV1.Columns["BIDDING_RANGE"].ReadOnly = true;
            DGV1.Columns["NETDelta"].ReadOnly = true;

            
            DGV1.Columns["Token1"].ReadOnly = true;
            DGV1.Columns["Token2"].ReadOnly = true;
            DGV1.Columns["Token3"].ReadOnly = true;
            DGV1.Columns["Tok1B_S"].ReadOnly = true;
            DGV1.Columns["Tok2B_S"].ReadOnly = true;
            DGV1.Columns["Tok3B_S"].ReadOnly = true;
            DGV1.Columns["tok1inst"].ReadOnly = true;
            DGV1.Columns["tok2inst"].ReadOnly = true;
            DGV1.Columns["tok3inst"].ReadOnly = true;

            DGV1.Columns["Expiry"].ReadOnly = true;
            DGV1.Columns["Expiry2"].ReadOnly = true;
            DGV1.Columns["Expiry3"].ReadOnly = true;
            DGV1.Columns["Symbol1"].ReadOnly = true;
            DGV1.Columns["Symbol2"].ReadOnly = true;
            DGV1.Columns["Symbol3"].ReadOnly = true;
            DGV1.Columns["StrikePrice"].ReadOnly = true;
            DGV1.Columns["StrikePrice2"].ReadOnly = true;
            DGV1.Columns["StrikePrice3"].ReadOnly = true;
            DGV1.Columns["OptionType"].ReadOnly = true;
            DGV1.Columns["OptionType2"].ReadOnly = true;
            DGV1.Columns["OptionType3"].ReadOnly = true;
            DGV1.Columns["NETDelta1"].ReadOnly = true;
            DGV1.Columns["NETDelta2"].ReadOnly = true;
            DGV1.Columns["NETDelta3"].ReadOnly = true;
            //  end read only columns 

            try  
            {
                foreach (DataGridViewColumn dc in DGV1.Columns)
                {
                    this.DGV1.Columns[dc.HeaderText.Replace(" ", "")].Visible = true;
                }

                DataSet ds = new DataSet();
                if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "nkb.xml"))
                {
                    ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "nkb.xml");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ", "")].Visible = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Deafult Profile nOT cREATE", "eRROR");
            }
            DGV1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            Type controlType = DGV1.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(DGV1, true, null);
            // Thread.Sleep(1000);
            if (this.InvokeRequired)
            {
                this.BeginInvoke((ThreadStart)delegate() { defaultLoadfun(); });
                //  this.BeginInvoke((ThreadStart)delegate() { Temp(); });
                return;
            }
            else
            {
                this.BeginInvoke((ThreadStart)delegate() { defaultLoadfun(); });
                //  Temp();
            }
          
           

        }
        private void defaultLoadfun()
        {

            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar +System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy")+ "IOCDefault.xml"))
                return;
                SpreadTable.Clear();
                DataSet ds_set = new DataSet();
                ds_set.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Lastvalue1.xml");
                SpreadTable.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "IOCDefault.xml");
                //  SpreadTable.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "FOWATCH.xml");
                for (int i = 0; i < ds_set.Tables[0].Rows.Count; i++)
                {
                    DGV1.Rows[i].Cells["BUYPRICE"].Value = 0.00;
                    DGV1.Rows[i].Cells["SELLPRICE"].Value = 0.00;
                    DGV1.Rows[i].Cells["ORDQTY(B)"].Value = 0.00;
                    DGV1.Rows[i].Cells["ORDQTY(S)"].Value = 0.00;
                    DGV1.Rows[i].Cells["TOTALQTY(B)"].Value = 0.00;
                    DGV1.Rows[i].Cells["TOTALQTY(S)"].Value = 0.00;
                    DGV1.Rows[i].Cells["PAYUPTIEKS"].Value = 0.00;
                    DGV1.Rows[i].Cells["W_T_C"].Value = 0.00;

                    DGV1.Rows[i].Cells["ORDQTY(B)"].Value = ds_set.Tables[0].Rows[i]["ORDQTY(B)"] == null ? 0 : ds_set.Tables[0].Rows[i]["ORDQTY(B)"];
                    DGV1.Rows[i].Cells["TOTALQTY(B)"].Value = ds_set.Tables[0].Rows[i]["TOTALQTY(B)"] == null ? 0 : ds_set.Tables[0].Rows[i]["TOTALQTY(B)"];
                    DGV1.Rows[i].Cells["BUYPRICE"].Value = ds_set.Tables[0].Rows[i]["BUYPRICE"] == null ? 0 : ds_set.Tables[0].Rows[i]["BUYPRICE"];
                    DGV1.Rows[i].Cells["SELLPRICE"].Value = ds_set.Tables[0].Rows[i]["SELLPRICE"] == null ? 0 : ds_set.Tables[0].Rows[i]["SELLPRICE"];
                    DGV1.Rows[i].Cells["TOTALQTY(S)"].Value = ds_set.Tables[0].Rows[i]["TOTALQTY(S)"] == null ? 0 : ds_set.Tables[0].Rows[i]["TOTALQTY(S)"];
                    DGV1.Rows[i].Cells["ORDQTY(S)"].Value = ds_set.Tables[0].Rows[i]["ORDQTY(S)"] == null ? 0 : ds_set.Tables[0].Rows[i]["ORDQTY(S)"];
                    DGV1.Rows[i].Cells["PAYUPTIEKS"].Value = ds_set.Tables[0].Rows[i]["PAYUPTIEKS"] == null ? 0 : ds_set.Tables[0].Rows[i]["PAYUPTIEKS"];

                    DGV1.Rows[i].Cells["W_T_C"].Value = ds_set.Tables[0].Rows[i]["W_T_C"] == null ? 0 : ds_set.Tables[0].Rows[i]["W_T_C"];

                }




                for (int i = 0; i < SpreadTable.Rows.Count; i++)
                {
                 //   UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
                  //  UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token3"].ToString());
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token4"].ToString());
                    Global.Instance.Ratio.TryAdd(Convert.ToInt32(SpreadTable.Rows[i]["Token1"]), Convert.ToInt32(SpreadTable.Rows[i]["ratio1"]));
                    Global.Instance.Ratio.TryAdd(Convert.ToInt32(SpreadTable.Rows[i]["Token2"]), Convert.ToInt32(SpreadTable.Rows[i]["ratio2"]));
                    portFolioCounter++;

                }
                if (DGV1.Rows.Count == 0)
                { return; }
                portFolioCounter = Convert.ToInt32(SpreadTable.Compute("MAX(PF)", "")) + 1;
                
          
        }

       
           
        private void DGV1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            //string s = DGV1.EditingControl.Text;
            Console.WriteLine("Error happened " + anError.Context.ToString());
            
            
         
        }

        private void DGV1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         //   DGV1_CellValueChanged( sender,  e);
       
            btnApply_Click(e.RowIndex, e.ColumnIndex);
            
        }

       

      
         private void btnApply_Click(int RowIndex, int ColumnIndex)
        {

            if (RowIndex <= -1)
                return;
           
          
            if (DGV1.Rows[RowIndex].Cells[ColumnIndex] is DataGridViewButtonCell)
            {
            //=======================================================================================================================================================

            var se = DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value;
            var bpy = DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value;
            var sep = DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value;
            var ods = DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value;
            var totalqB = DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value;
            var totalqS = DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value == "" ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value;

            var cmpS = DGV1.Rows[RowIndex].Cells["CMP(S)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["CMP(S)"].Value;
            var cmpB = DGV1.Rows[RowIndex].Cells["CMP(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["CMP(B)"].Value;
            double ORDQTYB = Convert.ToDouble(se);
            double BUYPRICE = Convert.ToDouble(bpy);
            double SELLPRICE = Convert.ToDouble(sep);
            double ORDQTYs = Convert.ToDouble(ods);
            double TOTALQTYB = Convert.ToDouble(totalqB);
            double TOTALQTYS = Convert.ToDouble(totalqS);
            double cmpS1 = ((Convert.ToDouble(cmpS) * 5) / 100) + Convert.ToDouble(cmpS);
            double cmpSdou = Convert.ToDouble(cmpS) - ((Convert.ToDouble(cmpS) * 5) / 100);
            double cmpB1 = ((Convert.ToDouble(cmpB) * 5) / 100) + Convert.ToDouble(cmpB);
            double cmpBdou = Convert.ToDouble(cmpB) - ((Convert.ToDouble(cmpB) * 5) / 100);


            if (ORDQTYB > 100)
            {
                label1.Visible = true;
                label1.Text = "PLZ Enter ORDQTY(B) > 100";
                DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value = 0;
                return;
            }
            else
            {
                label1.Visible = false;
                label1.Text = "*";
            }

            if (ORDQTYs > 100)
            {
                label2.Visible = true;
                label2.Text = "PLZ Enter ORDQTY(S) > 100";
                DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value = 0;
                return;
            }
            else
            {
                label2.Visible = false;
                label2.Text = "*";
            }

            if (TOTALQTYB > 500)
            {
                label5.Visible = true;
                label5.Text = "PLZ Enter TOTALQTY(B) > 500";
                DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value = 0;
                return;
            }
            else
            {
                label5.Visible = false;
                label5.Text = "*";
            }
            if (TOTALQTYS > 500)
            {
                label6.Visible = true;
                label6.Text = "PLZ Enter TOTALQTY(S) > 500";
                DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value = 0;
                return;
            }
            else
            {
                label6.Visible = false;
                label6.Text = "*";

            }

            if (Regex.IsMatch(Convert.ToString(cmpB.ToString()), @"[-/*()]"))
            {

                if (BUYPRICE < cmpB1)
                {
                    label3.Visible = true;
                    label3.Text = "PLZ Enter BUYPRICE >  CMP(B) 5% ";
                    DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value = 0;
                    return;
                }

                else
                {
                    // before = BUYPRICE;
                    label3.Visible = false;
                    label3.Text = "*";

                }


            }
            else
            {

                if (BUYPRICE < cmpBdou)
                {
                    label3.Visible = true;
                    label3.Text = "PLZ Enter BUYPRICE >  CMP(B) 5% ";
                    DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value = 0;
                    return;
                }

                else
                {
                    // before = BUYPRICE;

                    label3.Visible = false;
                    label3.Text = "*";
                }



            }




            if (Regex.IsMatch(Convert.ToString(cmpB.ToString()), @"[-/*()]"))
            {
                if (SELLPRICE > cmpSdou)
                {
                    label4.Visible = false;
                    label4.Text = "*";

                }
                else
                {

                    label4.Visible = true;
                    label4.Text = "PLZ Enter SELLPRICE >  CMP(S) 5% ";
                    DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value = 0;

                    return;

                }
            }
            else
            {

                // if (SELLPRICE > cmpSdou)
                if (SELLPRICE >= Convert.ToDouble(cmpS))
                {
                    label4.Visible = false;
                    label4.Text = "*";

                }
                else
                {

                    label4.Visible = true;
                    label4.Text = "PLZ Enter SELLPRICE >  CMP(S) 5% ";
                    DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value = 0;

                    return;

                }

            }


       //    ====================== ============== ===============  =========== ========== ======== ========== ===== ========== =========== ======= =======


            double _buy= Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value);
                double _sell = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value);
                string we = Convert.ToString(DGV1.SelectedRows[0].Cells["THRESHOLD"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["THRESHOLD"].Value);
                string THRESHOLD2 = "";
                if (we =="0")
                {
                    THRESHOLD2 = we;
                }
                else
                {
                   THRESHOLD2= we.Remove(we.Length - 1);
                }
               // FOPAIRDIFFLEG2 _frmDIff = new FOPAIRDIFFLEG2;
                //{
                //    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["PF"].Value),

                   //  BuyMin = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value),
                  //  BuyMax = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value),

                //    // Divisor = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value),
                   

                //    SPREADBUY = Convert.ToInt32(_buy * 100),
                //    SPREADSELL = Convert.ToInt32(_sell * 100),

                //    SellMin = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value),

                //    SellMax = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value),


                //    Token1Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio1"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ratio1"].Value),
                //    Token2Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio2"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ratio2"].Value),
                //    Token3Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio3"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ratio3"].Value),
                //    Token4Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio4"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ratio4"].Value),
                ////    // Order_Type = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["Order_Type"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["Order_Type"].Value),
                //    Order_Type = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["Order_Type"].Value == DBNull.Value ? OrderType.defaul :
                //Convert.ToString(DGV1.Rows[RowIndex].Cells["Order_Type"].Value) == "Bidding" ? OrderType.Bidding :
                //   Convert.ToString(DGV1.Rows[RowIndex].Cells["Order_Type"].Value) == "IOC" ? OrderType.IOC : OrderType.defaul
                //     ),

                //    firstbid =(short) (DGV1.Rows[RowIndex].Cells["First_LEG"].Value == DBNull.Value ? FirstBid.defaul :
                //    Convert.ToString(DGV1.Rows[RowIndex].Cells["First_LEG"].Value) == "NORMAL" ? FirstBid.Normal :
                //   Convert.ToString(DGV1.Rows[RowIndex].Cells["First_LEG"].Value) == "WEIGHTED" ? FirstBid.WEIGHTED :
                //   Convert.ToString(DGV1.Rows[RowIndex].Cells["First_LEG"].Value) == "BESTBID" ? FirstBid.BESTBID : FirstBid.defaul),


                //    second_leg =(short) (DGV1.Rows[RowIndex].Cells["Second_Leg"].Value == DBNull.Value ? SecondLeg.defaul :
                // Convert.ToString(DGV1.Rows[RowIndex].Cells["Second_Leg"].Value) == "MKT" ? SecondLeg.MKT :
                //     Convert.ToString(DGV1.Rows[RowIndex].Cells["Second_Leg"].Value) == "LIMIT" ? SecondLeg.LIMIT :
                //      Convert.ToString(DGV1.Rows[RowIndex].Cells["Second_Leg"].Value) == "BEST BID" ? SecondLeg.BESTBID : SecondLeg.defaul),

                //    Req_count = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["ReqConnt"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ReqConnt"].Value),
                //    Opt_Tick = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["OPT_TICK"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["OPT_TICK"].Value),
                //    Order_Depth = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["ORDER_DEPTH"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["ORDER_DEPTH"].Value),

                //    // Threshold = Convert.ToInt16(THRESHOLD2),
                //    Bidding_Range = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["BIDDING_RANGE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BIDDING_RANGE"].Value),
                //    //Pay_up_Ticks = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["PAYUPTIEKS"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["PAYUPTIEKS"].Value),
                //};



                using (frmDiff _frmDIff = new frmDiff())
                {


                    _frmDIff._FOPairDiff.BFSNDIFF = Convert.ToInt32(_buy * 100);
                    _frmDIff._FOPairDiff.BNSFDIFF = Convert.ToInt32(_sell * 100);

                    _frmDIff._FOPairDiff.BNSFMNQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ORDQTY(S)"].Value);
                    _frmDIff._FOPairDiff.BFSNMNQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ORDQTY(B)"].Value);

                    _frmDIff._FOPairDiff.BNSFMXQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["TOTALQTY(S)"].Value);
                    _frmDIff._FOPairDiff.BFSNMXQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["TOTALQTY(B)"].Value);

                    _frmDIff._FOPairDiff.PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    _frmDIff._FOPairDiff.TokenNear = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token2"].Value);
                    _frmDIff._FOPairDiff.TokenFar = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token1"].Value);
                    // _frmDIff._FOPairDiff.Depth_Best =Convert.ToBoolean(DGV1.Rows[RowIndex].Cells["D_B"].Value) == true ? Convert.ToInt16(db.Best) : Convert.ToInt16(db.Depth);
                    _frmDIff._FOPairDiff._obj_FOPair = new FOPAIR()
               {
                   PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value),
                   TokenNear = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token2"].Value),
                   TokenFar = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token1"].Value),
                   Token1Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio2"].Value),
                   Token2Ratio = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["ratio1"].Value),
                   Token1side = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["Tok2B_S"].Value.ToString() == "Buy" ? 1 : 2),
                   Token2side = Convert.ToInt16(DGV1.Rows[RowIndex].Cells["Tok1B_S"].Value.ToString() == "Buy" ? 1 : 2)


               };

                    NNFHandler.Instance.Publisher(MessageType.FOPAIRDIFF, DataPacket.RawSerialize(_frmDIff._FOPairDiff));

                    TradeTrac trd_struct = new TradeTrac();
                    trd_struct.PF_ID = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    trd_struct.Given_Price_Buy = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value);
                    trd_struct.Given_Price_Sell = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value);
                    //trd_struct.B_S = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    Global.Instance.TradeTrac_dict.AddOrUpdate(Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token1"].Value), trd_struct, (k, v1) => trd_struct);

                    TradeTrac trd_struct2 = new TradeTrac();
                    trd_struct2.PF_ID = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    //trd_struct2.B_S = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);

                    trd_struct2.Given_Price_Buy = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value);
                    trd_struct2.Given_Price_Sell = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value);
                    Global.Instance.TradeTrac_dict.AddOrUpdate(Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token2"].Value), trd_struct2, (k, v1) => trd_struct2);
                    Global.Instance.Refresh_dict.AddOrUpdate(Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value), _frmDIff, (k, v1) => _frmDIff);

                    TradeTrac trd_struct3 = new TradeTrac();
                    trd_struct3.PF_ID = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    //trd_struct2.B_S = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);

                    trd_struct3.Given_Price_Buy = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["BUYPRICE"].Value);
                    trd_struct3.Given_Price_Sell = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.Rows[RowIndex].Cells["SELLPRICE"].Value);
                    Global.Instance.TradeTrac_dict.AddOrUpdate(Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token3"].Value), trd_struct3, (k, v1) => trd_struct3);
                    Global.Instance.Refresh_dict.AddOrUpdate(Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value), _frmDIff, (k, v1) => _frmDIff);

                    //NNFHandler.Instance.Publisher(MessageType.IOCPAIRDIFF, DataPacket.RawSerialize(_frmDIff));
                }
                Task.Factory.StartNew(() => applyFun());
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)DGV1.Rows[RowIndex].Cells["Enable"];
               if ( checkCell.Value == null)
               {
                   checkCell.Value = false;

               }

                if ((bool)checkCell.Value == true && checkCell.Value != null)
                {
                    DGV1.Rows[RowIndex].Cells["PF"].Style.BackColor = Color.Green;
                }
                // DGV1.Rows[RowIndex].Cells["Apply"].Style.ForeColor = Color.Green;
                // DGV1.Rows[RowIndex].Cells["ratio4"].CurrentCell.Style.SelectionBackColor = Color.Red;

            }
        }
        

        private void btnStartAll_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
                Thread.Sleep(10);
            }

            //foreach (DataGridViewRow  VARIABLE in DGV1.Rows)
            //{
            //    DataGridViewCheckBoxCell cb = (VARIABLE.Cells["Enable"]) as DataGridViewCheckBoxCell;
                
            //    cb.Value = true;
          //  }

            btnStopAll.Enabled = true;
            btnStartAll.Enabled = false;
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow VARIABLE in DGV1.Rows)
            {
                DataGridViewCheckBoxCell cb = (VARIABLE.Cells["Enable"]) as DataGridViewCheckBoxCell;
                
                cb.Value = false;

                Thread.Sleep(10);
            }
            btnStopAll.Enabled = false;
            btnStartAll.Enabled = true;
        }
        
       

        private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1) return;           

            if (DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
            DataGridViewCheckBoxCell checkCell =(DataGridViewCheckBoxCell)DGV1.Rows[e.RowIndex].Cells["Enable"];
                

            Client.Csv_Struct _lotsize = new Csv_Struct();
            if ((bool) checkCell.Value == true)
            {
               
                 int val = 0;
               
                  
               // FOPAIRLEG2 v;
           
                //byte[] buffer = DataPacket.RawSerialize(v=new FOPAIRLEG2()
                //{
                //    //Tok1B_S
                //    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                //    Token1 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                //    Token2 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value== DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token2"].Value),
                //    Token3 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token3"].Value== DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token3"].Value),
                //    Token4 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token4"].Value== DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token4"].Value),
                //    Token1side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok1B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token2side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok2B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token3side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok3B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token4side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok4B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token1Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio1"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio1"].Value),
                //    Token2Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio2"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio2"].Value),
                //    Token3Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio3"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio3"].Value),
                //    Token4Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio4"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio4"].Value),
                   
                //    CALCTYPE = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Calc_type"].Value.ToString() == "BaseDiff" ? 2 : 1),
                    
                //});
                 FOPAIR v;
                 byte[] buffer = DataPacket.RawSerialize(v = new FOPAIR()
                 {
                     PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                     TokenFar = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                     Token2Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio1"].Value),
                     Token2side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok1B_S"].Value.ToString() == "Buy" ? 1 : 2),


                     TokenNear = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value),
                     Token1Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio2"].Value),
                     Token1side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok2B_S"].Value.ToString() == "Buy" ? 1 : 2)


                 });

                NNFHandler.Instance.Publisher(MessageType.FOPAIR, buffer);


                int _buycount = 0;
                int _sellcount = 0;
                var v2 = Global.Instance.Ratio.Where(a => a.Key == v.TokenNear).Select(b => b.Value).ToList();
                val = Convert.ToInt32(v2.FirstOrDefault().ToString());
                if (Holder._DictLotSize.ContainsKey(v.TokenNear) && v.TokenNear != 0)
                {
                    Holder._DictLotSize[v.TokenNear] = new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenNear).Select(a => a.BoardLotQuantity).First()
                    };
                }
                else if (v.TokenNear != 0)
                {
                    Holder._DictLotSize.TryAdd(v.TokenNear, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenNear).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }
                _lotsize = Holder._DictLotSize[v.TokenNear];
                if (Holder._DictLotSize.ContainsKey(v.TokenFar) && v.TokenFar != 0)
                {
                    Holder._DictLotSize[v.TokenFar] = new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenFar).Select(a => a.BoardLotQuantity).First()
                    };
                }
                else if (v.TokenFar != 0)
                {
                    Holder._DictLotSize.TryAdd(v.TokenFar, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenFar).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }

                //if (Holder._DictLotSize.ContainsKey(v.Token3) && v.Token3 != 0)
                //{
                //    Holder._DictLotSize[v.Token3] = new Csv_Struct()
                //    {
                //        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.Token3).Select(a => a.BoardLotQuantity).First()
                //    };
                //}
                //else if (v.Token3 != 0)
                //{
                //    Holder._DictLotSize.TryAdd(v.Token3, new Csv_Struct()
                //    {
                //        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.Token3).Select(a => a.BoardLotQuantity).First()
                //    }
                //);
                //}

            }
            else 
            {
                DGV1.Rows[e.RowIndex].Cells["PF"].Style.BackColor = Color.Red;
                //FOPAIRLEG2 v;
                //byte[] buffer = DataPacket.RawSerialize(v = new FOPAIRLEG2()
                //{
                //    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                //    Token1 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                //    Token2 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token2"].Value),
                //    Token3 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token3"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token3"].Value),
                //    Token4 = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token4"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["Token4"].Value),
                //    Token1side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok1B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token2side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok2B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token3side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok3B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token4side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok4B_S"].Value.ToString() == "Buy" ? 1 : 2),
                //    Token1Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio1"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio1"].Value),
                //    Token2Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio2"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio2"].Value),
                //    Token3Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio3"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio3"].Value),
                //    Token4Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio4"].Value == DBNull.Value ? "0" : DGV1.Rows[e.RowIndex].Cells["ratio4"].Value),
                //    CALCTYPE = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Calc_type"].Value.ToString() == "BaseDiff" ? 2 : 1),

                //});
                FOPAIR v;
                byte[] buffer = DataPacket.RawSerialize(v = new FOPAIR()
                {
                    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                    TokenFar = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                    Token2Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio1"].Value),
                    Token2side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok1B_S"].Value.ToString() == "Buy" ? 1 : 2),


                    TokenNear = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value),
                    Token1Ratio = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["ratio2"].Value),                  
                    Token1side = Convert.ToInt16(DGV1.Rows[e.RowIndex].Cells["Tok2B_S"].Value.ToString() == "Buy" ? 1 : 2)


                });
                NNFHandler.Instance.Publisher(MessageType.FOPAIRUNSUB, buffer);
            }
            
            }
        }

        private void DGV1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.F2)
            {
                modified();
            }
            if(e.KeyCode ==  Keys.Delete)
            {
                
              //  --portFolioCounter;
                DialogResult resul = MessageBox.Show("Are you sure you want to Delete row ", "Optimus", MessageBoxButtons.YesNo);
                   if(resul==DialogResult.No)
                   {
                       return;
                   }
                   else
                   {

                       //DeletePortfolio v;

                       //byte[] buffer = DataPacket.RawSerialize(v = new DeletePortfolio()
                       //{
                       //    //Tok1B_S

                       //    PORTFOLIONAME = Convert.ToInt32(DGV1.SelectedRows[0].Cells["PF"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["PF"].Value),
                       //    Token1 = Convert.ToInt32(DGV1.SelectedRows[0].Cells["Token1"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token1"].Value),
                       //    Token2 = Convert.ToInt32(DGV1.SelectedRows[0].Cells["Token2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token2"].Value),
                       //    Token3 = Convert.ToInt32(DGV1.SelectedRows[0].Cells["Token3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token3"].Value),
                       //    Token4 = Convert.ToInt32(DGV1.SelectedRows[0].Cells["Token4"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token4"].Value),


                       //});


                       //NNFHandler.Instance.Publisher(MessageType.DELETE, buffer);

                DGV1.Rows.RemoveAt(DGV1.SelectedRows[0].Index);
                   }
                DataTable dt = SpreadTable;
            }
          else  if (e.KeyCode == Keys.Enter)
            {

                DataGridView _dgLoc = sender as DataGridView;

                if (_dgLoc.CurrentCell.EditedFormattedValue.ToString() == "Apply")
                {
                   // _dgLoc.CurrentCell.Style.SelectionBackColor = Color.Red;
                  btnApply_Click(_dgLoc.CurrentRow.HeaderCell.RowIndex, _dgLoc.CurrentRow.Cells["Apply"].ColumnIndex);
                }
            }


            //=================================================================================
            

          
            if (e.Control && e.KeyCode ==  Keys.A)
            {
             

                btnApply_Click(DGV1.SelectedRows[0].Index, 7);

              
            }
      

            //============================================================================= 

        }
        double before = 0;
        private void DGV1_SelectionChanged(object sender, EventArgs e)
        {
            //  DGV1.ClearSelection();
        }

        private void DGV1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (DGV1.IsCurrentCellDirty)
            {
                DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //this.valueChanged = true;
                //this.DGV1.NotifyCurrentCellDirty(true);
            }
        }
        
   
        string strv = "";
        
        private void DGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            

            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)// "BNSFDIFF")
            {
               
                tv = 1;
               
             
            
                txt.Show();
              
                txt.Location = this.DGV1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                DGV1.Controls.Add(txt);
                txt.Width = DGV1.Columns[e.ColumnIndex].Width;
                txt.Text = Convert.ToString(DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                txt.Focus();
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                txt.SelectAll();
               // txt.SelectionStart = txt.Text.Length;
                
              
                }

           // }
       }

        int tv = 1;
        private void DGV1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)// "BNSFDIFF")
            {
              //  if (tv > 2) return;

                if (Information.IsNumeric(txt.Text) == true)
                    DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = txt.Text;
                txt.Focus();
                txt.Hide(); 
               
                tv++;
              
               
               
            }



           }
            catch { }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter key pressed");
            }
            if (e.Control && e.KeyCode == Keys.A)
            {


                btnApply_Click(DGV1.SelectedRows[0].Index, 7);

                txt.Hide();
            }
          //  MessageBox.Show(e.KeyCode.ToString());


        }

        private void txt_MouseClick(object sender, MouseEventArgs e)
        {
            txt.Focus();
            txt.SelectAll();
              
            //BeginInvoke((Action)delegate
            //{

            //    txt.SelectAll();
            //});
        }

        private void DGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    
        private void txt_TextChanged(object sender, EventArgs e)
        {
           // txt.SelectAll();
            if (!Information.IsNumeric(txt.Text))
            {
                if (txt.Text.Length > 1)
                {
                    MessageBox.Show("Please Insert Numeric integer", "Information");
                    txt.Clear();
                    txt.Text = "0";
                   
                }

                txt.Focus();
               

            
               
            }
           


        }

        private void txt_Leave(object sender, EventArgs e)
        {
            
            this.txt.Hide();
        }

        private void DGV1_Scroll(object sender, ScrollEventArgs e)
        {
          //string st = sender.ToString();
            txt.Hide();
            DGV1.Focus();
        }

        private void DGV1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void DGV1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void DGV1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        void modified()
        {

            string pf = Convert.ToString(DGV1.SelectedRows[0].Cells["PF"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["PF"].Value);

            string Order_Type = Convert.ToString(DGV1.SelectedRows[0].Cells["Order_Type"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Order_Type"].Value);

            string Strategy_Type = Convert.ToString(DGV1.SelectedRows[0].Cells["Strategy_Type"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Strategy_Type"].Value);
            string Calc_type = Convert.ToString(DGV1.SelectedRows[0].Cells["Calc_type"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Calc_type"].Value);

            string InstType = Convert.ToString(DGV1.SelectedRows[0].Cells["tok1inst"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["tok1inst"].Value);

            string InstType2 = Convert.ToString(DGV1.SelectedRows[0].Cells["tok2inst"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["tok2inst"].Value);
            string Symbol1 = Convert.ToString(DGV1.SelectedRows[0].Cells["Symbol1"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Symbol1"].Value);
            string Symbol2 = Convert.ToString(DGV1.SelectedRows[0].Cells["Symbol2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Symbol2"].Value);
            string Expiry = Convert.ToString(DGV1.SelectedRows[0].Cells["Expiry"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Expiry"].Value);
            string Expiry2 = Convert.ToString(DGV1.SelectedRows[0].Cells["Expiry2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Expiry2"].Value);
            string OptionType = Convert.ToString(DGV1.SelectedRows[0].Cells["OptionType"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OptionType"].Value);
            string OptionType2 = Convert.ToString(DGV1.SelectedRows[0].Cells["OptionType2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OptionType2"].Value);
            string StrikePrice = Convert.ToString(DGV1.SelectedRows[0].Cells["StrikePrice"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["StrikePrice"].Value);
            string StrikePrice2 = Convert.ToString(DGV1.SelectedRows[0].Cells["StrikePrice2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["StrikePrice2"].Value);
            string ReqConnt1 = Convert.ToString(DGV1.SelectedRows[0].Cells["ReqConnt"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ReqConnt"].Value);
            string ratio1 = Convert.ToString(DGV1.SelectedRows[0].Cells["ratio1"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ratio1"].Value);
            string ratio2 = Convert.ToString(DGV1.SelectedRows[0].Cells["ratio2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ratio2"].Value);
            string buy_sel = Convert.ToString(DGV1.SelectedRows[0].Cells["Tok1B_S"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Tok1B_S"].Value);
            string buy_sel2 = Convert.ToString(DGV1.SelectedRows[0].Cells["Tok2B_S"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Tok2B_S"].Value);

            string First_LEG = Convert.ToString(DGV1.SelectedRows[0].Cells["First_LEG"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["First_LEG"].Value);
            string OPT_TICK = Convert.ToString(DGV1.SelectedRows[0].Cells["OPT_TICK"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OPT_TICK"].Value);
            string ORDER_DEPTH = Convert.ToString(DGV1.SelectedRows[0].Cells["ORDER_DEPTH"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ORDER_DEPTH"].Value);
            string Second_Leg = Convert.ToString(DGV1.SelectedRows[0].Cells["Second_Leg"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Second_Leg"].Value);
            string we = Convert.ToString(DGV1.SelectedRows[0].Cells["THRESHOLD"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["THRESHOLD"].Value);
            string THRESHOLD = we.Remove(we.Length - 1);
            string BIDDING_RANGE = Convert.ToString(DGV1.SelectedRows[0].Cells["BIDDING_RANGE"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["BIDDING_RANGE"].Value);
            string Token1 = Convert.ToString(DGV1.SelectedRows[0].Cells["Token1"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token1"].Value);
            string Token2 = Convert.ToString(DGV1.SelectedRows[0].Cells["Token2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token2"].Value);
            string Token3 = Convert.ToString(DGV1.SelectedRows[0].Cells["Token3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Token3"].Value);

            string NEAR = Convert.ToString(DGV1.SelectedRows[0].Cells["NEAR"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["NEAR"].Value);
            string FAR = Convert.ToString(DGV1.SelectedRows[0].Cells["FAR"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["FAR"].Value);

            string reqCON = Convert.ToString(DGV1.SelectedRows[0].Cells["ReqConnt"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ReqConnt"].Value);

            string Symbol3 = Convert.ToString(DGV1.SelectedRows[0].Cells["Symbol3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Symbol3"].Value);
            string Expiry3 = Convert.ToString(DGV1.SelectedRows[0].Cells["Expiry3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Expiry3"].Value);
            string OptionType3 = Convert.ToString(DGV1.SelectedRows[0].Cells["OptionType3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OptionType3"].Value);
            string StrikePrice3 = Convert.ToString(DGV1.SelectedRows[0].Cells["StrikePrice3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["StrikePrice3"].Value);
            string InstType3 = Convert.ToString(DGV1.SelectedRows[0].Cells["tok3inst"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["tok3inst"].Value);
            string ratio3 = Convert.ToString(DGV1.SelectedRows[0].Cells["ratio3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ratio3"].Value);
            string buy_sel3 = Convert.ToString(DGV1.SelectedRows[0].Cells["Tok3B_S"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["Tok3B_S"].Value);
            string tok3 = Convert.ToString(DGV1.SelectedRows[0].Cells["tok3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["tok3"].Value);

            string OppsitToken1 = Convert.ToString(DGV1.SelectedRows[0].Cells["OppsitToken1"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OppsitToken1"].Value);
            string OppsitToken2 = Convert.ToString(DGV1.SelectedRows[0].Cells["OppsitToken2"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OppsitToken2"].Value);
            string OppsitToken3 = Convert.ToString(DGV1.SelectedRows[0].Cells["OppsitToken3"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["OppsitToken3"].Value);
            //====================
      

            string BUYPRICE = Convert.ToString(DGV1.SelectedRows[0].Cells["BUYPRICE"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["BUYPRICE"].Value);
            string SELLPRICE = Convert.ToString(DGV1.SelectedRows[0].Cells["SELLPRICE"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["SELLPRICE"].Value);
            string ORDQTY_b = Convert.ToString(DGV1.SelectedRows[0].Cells["ORDQTY(B)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ORDQTY(B)"].Value);
            string ORDQTY_S = Convert.ToString(DGV1.SelectedRows[0].Cells["ORDQTY(S)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ORDQTY(S)"].Value);
            string TOTALQTY_B = Convert.ToString(DGV1.SelectedRows[0].Cells["TOTALQTY(B)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["TOTALQTY(B)"].Value);
            string TOTALQTY_S = Convert.ToString(DGV1.SelectedRows[0].Cells["TOTALQTY(S)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["TOTALQTY(S)"].Value);
            string PAYUPTIEKS = Convert.ToString(DGV1.SelectedRows[0].Cells["PAYUPTIEKS"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["PAYUPTIEKS"].Value);
            string W_T_C = Convert.ToString(DGV1.SelectedRows[0].Cells["W_T_C"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["W_T_C"].Value);

            string ATP_S = Convert.ToString(DGV1.SelectedRows[0].Cells["ATP(S)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ATP(S)"].Value);
            string ATP_b = Convert.ToString(DGV1.SelectedRows[0].Cells["ATP(B)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["ATP(B)"].Value);
            string TRDQTY_s = Convert.ToString(DGV1.SelectedRows[0].Cells["TRDQTY(S)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["TRDQTY(S)"].Value);
            string TRDQTY_b = Convert.ToString(DGV1.SelectedRows[0].Cells["TRDQTY(B)"].Value == DBNull.Value ? "0" : DGV1.SelectedRows[0].Cells["TRDQTY(B)"].Value);


            //=========================================================================================================

         //   DGV1.Rows.RemoveAt(DGV1.SelectedRows[0].Index);

            DataTable dt = SpreadTable;

            using (AddSpreadToken _AddToken = new AddSpreadToken())
            {
                _AddToken.Token1sp = Token1;
                _AddToken.Token2sp = Token2;
                _AddToken.NEARSP = NEAR;
                _AddToken.FARSP = FAR;
                _AddToken.button1.Text = "Modify";
                _AddToken.lblPfName.Visible = true;
                _AddToken.txtpfName.Visible = true;
                _AddToken.Text = "Modify ";

                _AddToken.txtpfName.Text = pf;
                _AddToken.combox_OrderType.Text = Order_Type;

                _AddToken.OppsitToken1 = OppsitToken1;

                _AddToken.OppsitToken2 = OppsitToken2;

                
               // _AddToken.combox_OrderType.SelectedIndex = _AddToken.combox_OrderType.FindStringExact(Order_Type);

               // var item = _AddToken.combox_OrderType.GetItemText(_AddToken.combox_OrderType.Order_Type);

                if (Order_Type == "Bidding")
                {
                    _AddToken.comb_1stleg.Visible = true;
                    _AddToken.combox_optTrick.Visible = true;
                    _AddToken.comb_OrderDepth.Visible = true;
                    _AddToken.com2nd_leg.Visible = true;
                    _AddToken.textBox_Threshold.Visible = true;
                    _AddToken.comb_BidRange.Visible = true;
                    _AddToken.label9.Visible = true;
                    _AddToken.label10.Visible = true;
                    _AddToken.label11.Visible = true;
                    _AddToken.label12.Visible = true;
                    _AddToken.label13.Visible = true;
                    _AddToken.label15.Visible = true;
                    _AddToken.label17.Visible = true;
                    _AddToken.txtReqConnt.Visible = true;

                    if (Strategy_Type == "3_LEG")
                    {
                        _AddToken.Strategy_type_comboBox1.Text = Strategy_Type;
                       _AddToken.EXcomboBox3.Visible = true;
                        _AddToken.ORcomboBox4.Visible = true;
                        _AddToken.INSTcomboBox5.Visible = true;
                        _AddToken.SYMcomboBox6.Visible = true;
                        _AddToken.EXPcomboBox7.Visible = true;
                        _AddToken.OPcomboBox8.Visible = true;
                        _AddToken.STRIKecomboBox9.Visible = true;
                        _AddToken.textBox_Rati3.Visible = true;
                        _AddToken.cmd_buy_sell3.Visible = true;

                        _AddToken.INSTcomboBox5.Text = InstType3;
                        _AddToken.SYMcomboBox6.Text = Symbol3;
                        _AddToken.EXPcomboBox7.Text = Expiry3;
                        _AddToken.OPcomboBox8.Text = OptionType3;
                        _AddToken.STRIKecomboBox9.Text = StrikePrice3;
                        _AddToken.textBox_Rati3.Text = ratio3;
                        _AddToken.textBox_Rati3.Text = buy_sel3;
                        _AddToken.Token3sp = Token3;
                        _AddToken.tok3sp = tok3;
                        _AddToken.OppsitToken3 = OppsitToken3;

                        _AddToken.INSTcomboBox5.Enabled = false;
                        _AddToken.SYMcomboBox6.Enabled = false;
                        _AddToken.EXPcomboBox7.Enabled = false;
                        _AddToken.OPcomboBox8.Enabled = false;
                        _AddToken.STRIKecomboBox9.Enabled = false;
                        _AddToken.textBox_Rati3.Enabled = false;
                        _AddToken.cmd_buy_sell3.Enabled = false;
                        _AddToken.show();
                    }

                }



                _AddToken.EXcomboBox1.Enabled = false;
                _AddToken.ORcomboBox2.Enabled = false;
                _AddToken.EXcomboBox2.Enabled = false;
                _AddToken.ORcomboBox3.Enabled = false;
                _AddToken.OPcomboBox6.Enabled = false;
                _AddToken.STRIKecomboBox7.Enabled = false;
             _AddToken.EXcomboBox3.Enabled = false;
             _AddToken.ORcomboBox4.Enabled = false;
                

                _AddToken.combox_OrderType.Enabled = false;
                _AddToken.Strategy_type_comboBox1.Text = Strategy_Type;
                _AddToken.Strategy_type_comboBox1.Enabled = false;
                _AddToken.Calc_typecomboBox1.Text = Calc_type;
                _AddToken.Calc_typecomboBox1.Enabled = false;
                _AddToken.INSTcomboBox3.Text = InstType;
                _AddToken.INSTcomboBox3.Enabled = false;
                _AddToken.SYMcomboBox4.Text = Symbol1;
                _AddToken.SYMcomboBox4.Enabled = false;
                _AddToken.EXPcomboBox5.Text = Expiry;
                _AddToken.EXPcomboBox5.Enabled = false;
                _AddToken.OPcomboBox6.Text = OptionType;
                _AddToken.OPcomboBox6.Enabled = false;
                _AddToken.STRIKecomboBox7.Text = StrikePrice;
                _AddToken.STRIKecomboBox7.Enabled = false;
                _AddToken.textBox_Ratio.Text = ratio1;
                _AddToken.textBox_Ratio.Enabled = false;
                _AddToken.cmd_buy_sell1.Text = buy_sel;
                _AddToken.cmd_buy_sell1.Enabled = false;
                _AddToken.INSTcomboBox4.Text = InstType2;
                _AddToken.INSTcomboBox4.Enabled = false;
                _AddToken.SYMcomboBox5.Text = Symbol2;
                _AddToken.SYMcomboBox5.Enabled = false;
                _AddToken.EXPcomboBox6.Text = Expiry2;
                _AddToken.EXPcomboBox6.Enabled = false;
                _AddToken.OPcomboBox7.Text = OptionType2;
                _AddToken.OPcomboBox7.Enabled = false;
                _AddToken.STRIKecomboBox8.Text = StrikePrice2;
                _AddToken.STRIKecomboBox8.Enabled = false;
                _AddToken.textBox_Ratio2.Text = " ";
                _AddToken.txtReqConnt.Text = ReqConnt1;


                _AddToken.textBox_Ratio2.Text = ratio2;
                _AddToken.textBox_Ratio2.Enabled = false;
                _AddToken.cmd_buy_sell2.Text = buy_sel2;
                _AddToken.cmd_buy_sell2.Enabled = false;
                _AddToken.comb_1stleg.Text = First_LEG;
                _AddToken.comb_1stleg.Enabled = false;
                _AddToken.combox_optTrick.Text = OPT_TICK;
                // _AddToken.combox_optTrick.Enabled = false;
                _AddToken.comb_OrderDepth.Text = ORDER_DEPTH;
                // _AddToken.comb_OrderDepth.Enabled = false;
                _AddToken.com2nd_leg.Text = Second_Leg;
                _AddToken.com2nd_leg.Enabled = false;
                _AddToken.textBox_Threshold.Text = THRESHOLD;

                // _AddToken.textBox_Threshold.Enabled = false;
                _AddToken.comb_BidRange.Text = BIDDING_RANGE;

                // _AddToken.comb_BidRange.Enabled = false;
                _AddToken.txtReqConnt.Text = reqCON;

                if (_AddToken.ShowDialog() == DialogResult.OK)
                {
                  //  DataRow dr1 = SpreadTable.NewRow();

                    DataRow[] dr = SpreadTable.Select("PF  = '" + (_AddToken._objOut2.PFName).ToString() + "'");

               //     DataRow[] dr = Global.Instance.TradeTracker.("PF_ID  = '" + (_AddToken._objOut2.PFName.ToString()) + "'");

                    dr[0]["PF"] = _AddToken._objOut2.PFName;
                    dr[0]["NEAR"] = _AddToken._objOut2.Desc1;
                    dr[0]["Token1"] = _AddToken._objOut2.Token1;

                    dr[0]["FAR"] = _AddToken._objOut2.Desc2;
                    dr[0]["Token2"] = _AddToken._objOut2.Token2;
                    dr[0]["tok3"] = _AddToken._objOut2.Desc3;
                    dr[0]["Token3"] = _AddToken._objOut2.Token3;
                    dr[0]["Token4"] = _AddToken._objOut2.Token4;
                    dr[0]["Calc_type"] = _AddToken._objOut2.Calc_type;


                    dr[0]["ratio1"] = Convert.ToString(_AddToken._objOut2.ratio1) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio1);
                    dr[0]["ratio2"] = Convert.ToString(_AddToken._objOut2.ratio2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio2);
                    dr[0]["ratio3"] = Convert.ToString(_AddToken._objOut2.ratio3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio3);
                    dr[0]["ratio4"] = Convert.ToString(_AddToken._objOut2.ratio4) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio4);

                    dr[0]["Strategy_Type"] = Convert.ToString(_AddToken._objOut2.Strat_type) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Strat_type);
                    dr[0]["Symbol1"] = Convert.ToString(_AddToken._objOut2.Symbol1) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol1);
                    dr[0]["Symbol2"] = Convert.ToString(_AddToken._objOut2.Symbol2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol2);
                    dr[0]["Expiry"] = Convert.ToString(_AddToken._objOut2.Expiry) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry);
                    dr[0]["Expiry2"] = Convert.ToString(_AddToken._objOut2.Expiry2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry2);
                    dr[0]["OptionType"] = Convert.ToString(_AddToken._objOut2.OptionType) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType);
                    dr[0]["OptionType2"] = Convert.ToString(_AddToken._objOut2.OptionType2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType2);
                    dr[0]["StrikePrice"] = Convert.ToString(_AddToken._objOut2.StrikePrice) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice);
                    dr[0]["StrikePrice2"] = Convert.ToString(_AddToken._objOut2.StrikePrice2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice2);
                    dr[0]["ReqConnt"] = Convert.ToString(_AddToken._objOut2.ReqConnt) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ReqConnt);

                    dr[0]["Symbol3"] = Convert.ToString(_AddToken._objOut2.Symbol3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Symbol3);
                    dr[0]["Expiry3"] = Convert.ToString(_AddToken._objOut2.Expiry3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Expiry3);
                    dr[0]["OptionType3"] = Convert.ToString(_AddToken._objOut2.OptionType3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OptionType3);
                    dr[0]["StrikePrice3"] = Convert.ToString(_AddToken._objOut2.StrikePrice3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.StrikePrice3);


                    dr[0]["Order_Type"] = Convert.ToString(_AddToken._objOut2.Order_Type) == "" ? "0" : Convert.ToString(_AddToken._objOut2.Order_Type);
                    dr[0]["First_LEG"] = Convert.ToString(_AddToken._objOut2.first_LEG) == "" ? "0" : Convert.ToString(_AddToken._objOut2.first_LEG);
                    dr[0]["OPT_TICK"] = Convert.ToString(_AddToken._objOut2.OPT_TICK) == "" ? "0" : Convert.ToString(_AddToken._objOut2.OPT_TICK);
                    dr[0]["ORDER_DEPTH"] = Convert.ToString(_AddToken._objOut2.ORDER_DEPTH) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ORDER_DEPTH);
                    dr[0]["Second_Leg"] = Convert.ToString(_AddToken._objOut2.second_Leg) == "" ? "0" : Convert.ToString(_AddToken._objOut2.second_Leg);
                    dr[0]["THRESHOLD"] = Convert.ToString(_AddToken._objOut2.THRESHOLD) == "" ? "0" : Convert.ToString(_AddToken._objOut2.THRESHOLD);
                    dr[0]["BIDDING_RANGE"] = Convert.ToString(_AddToken._objOut2.BIDDING_RANGE) == "" ? "0" : Convert.ToString(_AddToken._objOut2.BIDDING_RANGE);



                    dr[0]["Tok1B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell);
                    dr[0]["Tok2B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell2);
                    dr[0]["Tok3B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell3) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell3);
                    dr[0]["Tok4B_S"] = Convert.ToString(_AddToken._objOut2.buy_sell4) == "" ? "0" : Convert.ToString(_AddToken._objOut2.buy_sell4);


                    dr[0]["tok1inst"] = _AddToken._objOut2.tok1_inst;
                    dr[0]["tok2inst"] = _AddToken._objOut2.tok2_inst;
                    dr[0]["tok3inst"] = _AddToken._objOut2.tok3_inst;
                    dr[0]["F_BID"] = 0;
                    dr[0]["F_ASK"] = 0;

                    dr[0]["F_LTP"] = 0;
                    dr[0]["NBID"] = 0;

                    dr[0]["NASK"] = 0;
                    dr[0]["NLTP"] = 0;

                    dr[0]["FBID"] = 0;
                    dr[0]["FASK"] = 0;

                    dr[0]["FLTP"] = 0;
                    dr[0]["NETDelta1"] = 0;
                    dr[0]["NETDelta2"] = 0;
                    dr[0]["NETDelta3"] = 0;

                    dr[0]["OppsitLTP1"] = 0;
                    dr[0]["OppsitLTP2"] = 0;
                    dr[0]["OppsitLTP3"] = 0;
                    dr[0]["OppsitToken1"] = _AddToken._objOut2.OppositeToken1;
                    dr[0]["OppsitToken2"] = _AddToken._objOut2.OppositeToken2;
                    dr[0]["OppsitToken3"] = _AddToken._objOut2.OppositeToken3;

                    //  dr["ratio2"] = Convert.ToString(_AddToken._objOut2.ratio2) == "" ? "0" : Convert.ToString(_AddToken._objOut2.ratio2);


                  //  SpreadTable.Rows.Add(dr);


                    //dr["BFSNDIFF"] =  0.0000;
                    //dr["BNSFDIFF"]=0.0000;
                    //dr["BNSFMNQ"] = 0.0000;
                    //dr["BFSNMNQ"] = 0.0000;
                    //dr["BNSFMXQ"] = 0.0000;
                    //dr["BFSNMXQ"] = 0.0000;

                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["BUYPRICE"].Value = BUYPRICE;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["SELLPRICE"].Value = SELLPRICE;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ORDQTY(B)"].Value = ORDQTY_b;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ORDQTY(S)"].Value = ORDQTY_S;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TOTALQTY(B)"].Value = TOTALQTY_B;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TOTALQTY(S)"].Value = TOTALQTY_S;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["PAYUPTIEKS"].Value = PAYUPTIEKS;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["W_T_C"].Value = W_T_C;

                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ATP(S)"].Value = ATP_S;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["ATP(B)"].Value = ATP_b;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TRDQTY(S)"].Value = TRDQTY_s;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TRDQTY(B)"].Value = TRDQTY_b;




      
                  //  UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token1;
                //    UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token2;
                //    UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token3;
                //    UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.Token4;

                 //   UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.OppositeToken1;
                 //   UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.OppositeToken2;
                 //   UDP_Reciever.Instance.Subscribe = _AddToken._objOut2.OppositeToken3;
                   

                        LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token1;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token2;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token3;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.Token4;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken1;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken2;
                    LZO_NanoData.LzoNanoData.Instance.Subscribe = _AddToken._objOut2.OppositeToken3;


                    //   FOPAIRDIFF

                    // portFolioCounter++;


                }
            }





        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in DGV1.Rows)
            {
                if (Global.Instance.Refresh_dict.ContainsKey(Convert.ToInt32(dr.Cells["PF"].Value)))
                {
                    dr.Cells["ORDQTY(B)"].Value = Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BNSFMNQ;
                    dr.Cells["BUYPRICE"].Value =Convert.ToDouble(Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BNSFDIFF)/100;
                    dr.Cells["SELLPRICE"].Value = Convert.ToDouble(Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BFSNDIFF)/100;
                    dr.Cells["ORDQTY(S)"].Value =Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BFSNMNQ;
                    dr.Cells["TOTALQTY(B)"].Value = Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BFSNMXQ;
                    dr.Cells["TOTALQTY(S)"].Value =Global.Instance.Refresh_dict[Convert.ToInt32(dr.Cells["PF"].Value)]._FOPairDiff.BNSFMXQ;
                 
                }
            }

        }
        DataGridViewColumnSelector cl = null;
        private void DGV1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1)
                {
                    DGV1.ContextMenuStrip = null;
                    cl = new DataGridViewColumnSelector(DGV1);
                }

            }  
        }

        private void WTC_txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WTC_txt.Text == "" || Convert.ToInt32(this.WTC_txt.Text) <= 0)
                {
                    this.WTC_txt.BackColor = Color.Red;
                    return;
                }
                else
                {
                    this.WTC_txt.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DGV1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(e.Control is DataGridViewTextBoxEditingControl)
            {
                

            }
        }

        private void DGV1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // MessageBox.Show(e.KeyCode.ToString());
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {

        }

        //==================================================================
      
        
        // ===  ===  ====


       //==========================================================================

       
    
    }

   
}


