using Client.Properties;
using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Client
{
    public struct dstruct
    {
       
      public String InstrumentName;
      public String FullName;
      public String  Symbol;
      public String TokenNo;   // int32
      public String Buy_SellIndicator;
      public String OptionType;
      public Int32  StrikePrice;
      public String      Price;  
      public double FillPrice;
      public long   FillNumber;
      public String      Volume;
      public String     Status;         
      public String      AccountNumber;
      public String      BookType;
      public Int32 BranchId;
      public String      BrokerId;
      public String      CloseoutFlag;
      public String      ExpiryDate;
      public Int32 DisclosedVolume;
      public Int32 DisclosedVolumeRemaining;
      public String      EntryDateTime;
      public Int32 filler;
      public String      GoodTillDate;
      public String      LastModified;
      public String      LogTime;
      public char Modified_CancelledBy;
      public Int64 nnffield;
      public String      Open_Close;
      public String      OrderNumber;
      public String      RejectReason;
      public Int16  Pro_ClientIndicator;
      public Int16  ReasonCode;
      public String      Settlor;
      public String      TimeStamp1;
      public String      TimeStamp2;
      public Int32 TotalVolumeRemaining;
      public Int32 TraderId;
      public Int16 TransactionCode;
      public Int32 UserId;
      public String     VolumeFilledToday;
      public String      Unique_id;
     
    }
    public partial class frmNetBook : Form
    {
        List<dstruct> studentDetails = new List<dstruct>(); 
        private static readonly frmNetBook instance = new frmNetBook();
        public static frmNetBook Instance
        {
            get
            {
                return instance;
            }
        }
        private frmNetBook()
        {
            InitializeComponent();
        }
        public static int[] LoadFormLocationAndSize(Form xForm)
        {
            int[] t = { 0, 0, 900, 300 };
            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "formnetorderclose.xml"))
                return t;
            DataSet dset = new DataSet();
            dset.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "formnetorderclose.xml");
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

            XmlWriter writer = XmlWriter.Create(Application.StartupPath + Path.DirectorySeparatorChar + "formnetorderclose.xml", settings);

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
        private void frmNetBook_Load(object sender, EventArgs e)
        {
            //if (Settings.Default.Window_LocationNBook != null)
            //{
            //    this.Location = Settings.Default.Window_LocationNBook;
            //}

            //// Set window size
            //if (Settings.Default.Window_SizeNBook != null)
            //{
            //    this.Size = Settings.Default.Window_SizeNBook;
            //}
            var AbbA = LoadFormLocationAndSize(this);
            this.Location = new Point(AbbA[0], AbbA[1]);
            this.Size = new Size(AbbA[2], AbbA[3]);

            this.FormClosing += new FormClosingEventHandler(SaveFormLocationAndSize);
            DataTable netbooks = new DataTable();
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(DGV);
            cs.MaxHeight = 200;
            cs.Width = 150;
            DGV.DataSource = Global.Instance.NetBookTable;
            
          netposion(1,1,1,1,1);
          this.DGV.Columns["SellAvg"].DefaultCellStyle.Format = "0.##";
          this.DGV.Columns["NetQty"].DefaultCellStyle.Format = "0.##";
         // this.DGV.Columns["SellQty"].DefaultCellStyle.Format = "0.0##";
          this.DGV.Columns["BuyAvg"].DefaultCellStyle.Format = "0.##";
          this.DGV.Columns["NetValue"].DefaultCellStyle.Format = "0.##";
         // this.DGV.Columns["BEP"].DefaultCellStyle.Format = "0.##";

          
         //DataGridViewColumnSelector cs = new DataGridViewColumnSelector(DGV);
         //cs.MaxHeight = 200;
         //cs.Width = 150;

          Type controlType = DGV.GetType();
          PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
          pi.SetValue(DGV, true, null);
         

        }


        public List<SelectListItem> query1 = new List<SelectListItem>();
        public void netposion2(int tok, int lotsize, int Tlot, double b_s, double rate)
        {
           try
            {
                int t_1 = 0;
          
             query1 = Global.Instance.OrdetTable.AsEnumerable().Where(p => p.Field<string>("Status") == orderStatus.Traded.ToString()).GroupBy(r => Convert.ToInt32(r.Field<string>("TokenNo")))
                      .Select(store => new SelectListItem
                      {
                          TredingSymbol = System.Text.ASCIIEncoding.ASCII.GetString(csv.CSV_Class.cimlist.First(tkn => tkn.Token == Convert.ToInt32(store.Key)).Name),
                           TokenNo =Convert.ToInt32(store.Key),
                          BuyQty =(store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))),

                          BuyAvg = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => ((Convert.ToInt32(p.Field<string>("Volume"))) * Convert.ToDouble(p.Field<string>("FillPrice")))) /
                          ((store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))) == 0 ? 1 : (store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p8 => Convert.ToInt32(p8.Field<string>("Volume")))))),

                          SellQty = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))),

                          SellAvg = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => (Convert.ToInt32(p.Field<string>("Volume"))) * Convert.ToDouble(p.Field<string>("FillPrice"))) /
                             ((store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))) == 0 ? 1 : (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p8 => Convert.ToInt32(p8.Field<string>("Volume")))))) ,

                          NetQty =
                          ((store.Where(a1 => a1.Field<String>("Buy_SellIndicator") == "BUY").Sum(p1 => Convert.ToInt32(p1.Field<string>("Volume"))))) -
                       ((store.Where(a2 => a2.Field<String>("Buy_SellIndicator") == "SELL").Sum(p2 => Convert.ToInt32(p2.Field<string>("Volume"))))),

                           
                      }).ToList();
                if(b_s==1)
                {
                    query1.FirstOrDefault().buy_rate = rate;  
                }
                else
                {
                    query1.FirstOrDefault().Sell_rate = rate;  
                }

             
          
                 //==================================  ===  ==  ==  ==  == == == ==  == ==  == 

             for (int j = 0; j < query1.Count(); j++)
             {
                 var TempList = csv.CSV_Class.cimlist.Where(a => a.Token == query1[j].TokenNo).ToList();

                 var checktok = TempList.First().EGMAGM;
                 DataRow i = Global.Instance.NetBookTable.NewRow();

                 i["Token"] = query1[j].TokenNo;
                 i["TredingSymbol"] =  query1[j].TredingSymbol;
                 i["BuyQty"] = query1[j].BuyQty;
                   i["BuyAvg"] =  query1[j].BuyAvg;
                 i["SellQty"] = query1[j].SellQty;
                 i["SellAvg"] = query1[j].SellAvg;
                 i["NetQty"] = query1[j].BuyAvg - query1[j].SellQty;
               //  i["NetQty"] = query1[j].NetQty;
               //  i["NetQty"] = query1[j].NetQty;
               //  i["BEP"] = query1[j].BEP;
               //   i["NetValue"] = query1[j].NetValue;
                 i["NetValue"] = Math.Round(Convert.ToDouble((query1[j].Sell_rate) * (query1[j].SellQty)), 3) + Math.Round(Convert.ToDouble((query1[j].buy_rate) * (query1[j].BuyQty)), 3);
                 i["InstrumentName"] = TempList.FirstOrDefault().InstrumentName;
                 i["Symbol"] = TempList.FirstOrDefault().Symbol;
                 i["OptionType"] = TempList.FirstOrDefault().OptionType;
                 i["StrikePrice"] = Convert.ToDouble(TempList.FirstOrDefault().StrikePrice / 100);
                
                 i["ExpiryDate"] = LogicClass.ConvertFromTimestamp(TempList.FirstOrDefault().ExpiryDate);
                 i["LTP"] = 0;

                 i["Delta"] = 0;
                  i["NetQtyDelta"] = 0;
                     

                 i["BUYRATE"] = Math.Round(Convert.ToDouble(query1[j].buy_rate), 3);
                 i["SELLRATE"] = Math.Round(Convert.ToDouble(query1[j].Sell_rate), 3);
                 i["BUYVALUE"] = Math.Round(Convert.ToDouble((query1[j].buy_rate) * (query1[j].BuyQty)), 3);
                 i["SELLVALUE"] = Math.Round(Convert.ToDouble((query1[j].Sell_rate) * (query1[j].SellQty)), 3);
                 i["MTM"] = 0;
                 //======= ==========  ========= =============  ========== ===========  =======================  ============  ==========
               //  lblbq.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("BuyQty")).ToString();
               //  lblsq.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("SellQty")).ToString();
               //  lvlNetQt.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("NetQty")).ToString();

               // // lvelNetVal.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("NetValue")).ToString();
               //  lblstrike.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("StrikePrice")).ToString();
               //  lvlLTP.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("LTP")).ToString();
               //  lvlDelta.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("Delta")).ToString();
               //  lvlNetQD.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("NetQtyDelta")).ToString();
               //  lbllblBR.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BUYRATE")).ToString();
               //  lvlSealRate.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SELLRATE")).ToString();
               //  lblBV.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BUYVALUE")).ToString();
               // lvlSealval.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SELLVALUE")).ToString();
               ////  lvlMtm.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("MTM")).ToString();

                 //============ ============  ========== ========= ==============  ==================  ==================== =============  ======== =======
                 Global.Instance.NetBookTable.Rows.Add(i);


             }
           
            DGV.DataSource = Global.Instance.NetBookTable;
          
            }
            catch(Exception ex)
            {
                Task.Factory.StartNew(()=>LogWriterClass.logwritercls.logs("tradeerror",ex.StackTrace.ToString()));

            }

           
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    DGV.Refresh();                 

                };
                this.Invoke(del);
                return;
            }
        
        }
        public void netposion(int tok,int lotsize,int Tlot,double b_s,double rate)
       {
 
            try
            {
                int t_1 = 0;
  

             query1 = Global.Instance.OrdetTable.AsEnumerable().Where(p => p.Field<string>("Status") == orderStatus.Traded.ToString()&& Convert.ToInt32(p.Field<string>("TokenNo"))==tok).GroupBy(r => Convert.ToInt32(r.Field<string>("TokenNo")))
                      .Select(store => new SelectListItem
                      {
                          TredingSymbol = System.Text.ASCIIEncoding.ASCII.GetString(csv.CSV_Class.cimlist.First(tkn => tkn.Token == Convert.ToInt32(store.Key)).Name),
                           TokenNo =Convert.ToInt32(store.Key),
                          //   InstrumentName = store.First().Field<string>("InstrumentName"),
                          BuyQty =(store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))),

                          BuyAvg = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => ((Convert.ToInt32(p.Field<string>("Volume"))) * Convert.ToDouble(p.Field<string>("FillPrice")))) /
                          ((store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))) == 0 ? 1 : (store.Where(a => a.Field<String>("Buy_SellIndicator") == "BUY").Sum(p8 => Convert.ToInt32(p8.Field<string>("Volume")))))),

                          SellQty = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))),

                          SellAvg = (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => (Convert.ToInt32(p.Field<string>("Volume"))) * Convert.ToDouble(p.Field<string>("FillPrice"))) /
                             ((store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p => Convert.ToInt32(p.Field<string>("Volume")))) == 0 ? 1 : (store.Where(a => a.Field<String>("Buy_SellIndicator") == "SELL").Sum(p8 => Convert.ToInt32(p8.Field<string>("Volume")))))) ,

                          NetQty =
                          ((store.Where(a1 => a1.Field<String>("Buy_SellIndicator") == "BUY").Sum(p1 => Convert.ToInt32(p1.Field<string>("Volume"))))) -
                       ((store.Where(a2 => a2.Field<String>("Buy_SellIndicator") == "SELL").Sum(p2 => Convert.ToInt32(p2.Field<string>("Volume"))))),

                       

                   
                      }).ToList();
                //if(b_s==1)
                //{
                //    query1.FirstOrDefault().buy_rate = rate;  
                //}
                //else
                //{
                //    query1.FirstOrDefault().Sell_rate = rate;  
                //}

     var TempList = csv.CSV_Class.cimlist.Where(a => a.Token == tok).ToList();
         var checktok = TempList.First().EGMAGM;
       

             if (Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<string>("TredingSymbol") == checktok).Count() > 0)
                {
                    //var rows = Global.Instance.NetBookTable.AsEnumerable().ToList();
                    //var fg = (rows.FindIndex(a => Convert.ToString(a.Field<string>("Symbol")) == "Total"));
                    //if (fg >= 0)
                    //{
                    //    Global.Instance.NetBookTable.Rows[fg].Delete();
                    //}

                    var rowlist = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<string>("TredingSymbol") == checktok).ToList();
                  
                    try
                    {
                        foreach (var i in rowlist)
                            {
                            i["Token"] = query1.FirstOrDefault().TokenNo;
                             i["TredingSymbol"]=query1.FirstOrDefault().TredingSymbol;
                            i["BuyQty"]=query1.FirstOrDefault().BuyQty;
                         i["BuyAvg"]=query1.FirstOrDefault().BuyAvg;
                             i["SellQty"]=-query1.FirstOrDefault().SellQty;
                         i["SellAvg"]=query1.FirstOrDefault().SellAvg;
                           //==========================================================================
                     //    i["NetQty"] = query1.FirstOrDefault().BuyQty + -query1.FirstOrDefault().SellQty;


                     //    i["NetValue"] = Math.Round(Convert.ToDouble((-query1.FirstOrDefault().SellAvg) * (-query1.FirstOrDefault().SellQty)), 3) + Math.Round(Convert.ToDouble((-query1.FirstOrDefault().BuyAvg) * (query1.FirstOrDefault().BuyQty)), 3);
                    //------------------------------------------------------------------------------
                   
                      i["InstrumentName"] = TempList.FirstOrDefault().InstrumentName;
                      i["Symbol"] = TempList.FirstOrDefault().Symbol;
                      i["OptionType"] = TempList.FirstOrDefault().OptionType;
                      i["StrikePrice"] =Convert.ToDouble( TempList.FirstOrDefault().StrikePrice/100);
                     i["ExpiryDate"] = LogicClass.ConvertFromTimestamp(TempList.FirstOrDefault().ExpiryDate);
                      i["LTP"] = Global.Instance.ltp[tok].LTP;
                      i["Delta"] = Math.Round(Convert.ToDouble(Global.Instance.ltp[tok].Delta),3);
                     i["NetQtyDelta"] = Math.Round(Convert.ToDouble((query1.FirstOrDefault().NetQty) * (Global.Instance.ltp[tok].Delta)),3);
                  //  i["BUYRATE"] = Math.Round(Convert.ToDouble(query1.FirstOrDefault().buy_rate),3);
                  //  i["SELLRATE"] = Math.Round(Convert.ToDouble(query1.FirstOrDefault().Sell_rate),3);
                    i["BUYVALUE"] = Math.Round(Convert.ToDouble((-query1.FirstOrDefault().BuyAvg) * (query1.FirstOrDefault().BuyQty)), 3);


                    i["SELLVALUE"] = Math.Round(Convert.ToDouble((-query1.FirstOrDefault().SellAvg) * (-query1.FirstOrDefault().SellQty)), 3);

                    i["MTM"] = Math.Round((Convert.ToDouble((-query1.FirstOrDefault().SellAvg) * (-query1.FirstOrDefault().SellQty)) + Convert.ToDouble((-query1.FirstOrDefault().BuyAvg) * (query1.FirstOrDefault().BuyQty))) - ((Global.Instance.ltp[tok].LTP) * (-query1.FirstOrDefault().BuyQty + -query1.FirstOrDefault().SellQty)), 3);
             
                   
               
                   //         //=========================================

                //    Global.Instance.NetBookTable.Rows.Add("Total", "-", Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("StrikePrice")), "-", "-", Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("BuyQty")),
                //Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BuyAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BUYVALUE")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("SellQty")),
                //     Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SellAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SELLVALUE")),
                //       0, 0,
                //       Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("MTM")),
                //       Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("LTP")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("Delta")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("NetQtyDelta")), 0, 0);
                //            //=============
                       
    
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(" Exception " + Ex.StackTrace.ToString());
                    }

                }
                else
                {
          
                 //==================================  ===  ==  ==  ==  == == ==
                    var rows = Global.Instance.NetBookTable.AsEnumerable().ToList();

                    //var fg = (rows.FindIndex(a => Convert.ToString(a.Field<string>("Symbol")) == "Total"));
                    //if (fg >= 0)
                    //{
                    //    Global.Instance.NetBookTable.Rows[fg].Delete();
                    //}
                    DataRow i = Global.Instance.NetBookTable.NewRow();

                    i["Token"] = query1.FirstOrDefault().TokenNo;
                    i["TredingSymbol"] = query1.FirstOrDefault().TredingSymbol;
                    i["BuyQty"] = query1.FirstOrDefault().BuyQty;
               i["BuyAvg"] = query1.FirstOrDefault().BuyAvg;
                    i["SellQty"] = -query1.FirstOrDefault().SellQty;
              i["SellAvg"] = query1.FirstOrDefault().SellAvg;

              
                    i["LTP"] = Global.Instance.ltp[tok].LTP;
                 i["Delta"] = Math.Round(Convert.ToDouble(Global.Instance.ltp[tok].Delta),3);
                 i["NetQtyDelta"] = Math.Round(Convert.ToDouble((Convert.ToDouble(query1.FirstOrDefault().NetQty) * (Global.Instance.ltp[tok].Delta))),3);
                  i["InstrumentName"] = TempList.FirstOrDefault().InstrumentName;
                    i["Symbol"] = TempList.FirstOrDefault().Symbol;
                    i["OptionType"] = TempList.FirstOrDefault().OptionType;
                    i["StrikePrice"] =Convert.ToDouble( TempList.FirstOrDefault().StrikePrice/100);
              i["ExpiryDate"] = LogicClass.ConvertFromTimestamp(TempList.FirstOrDefault().ExpiryDate);

              i["BUYVALUE"] = Math.Round(Convert.ToDouble((-query1.FirstOrDefault().BuyAvg) * (query1.FirstOrDefault().BuyQty)), 3);


              i["SELLVALUE"] = Math.Round(Convert.ToDouble((-query1.FirstOrDefault().SellAvg) * (-query1.FirstOrDefault().SellQty)), 3);

              i["MTM"] = Math.Round((Convert.ToDouble((-query1.FirstOrDefault().SellAvg) * (-query1.FirstOrDefault().SellQty)) + Convert.ToDouble((-query1.FirstOrDefault().BuyAvg) * (query1.FirstOrDefault().BuyQty))) - ((Global.Instance.ltp[tok].LTP) * (-query1.FirstOrDefault().BuyQty + -query1.FirstOrDefault().SellQty)), 3);
             
              //   i["MTM"] = Math.Round(Convert.ToDouble( (query1.FirstOrDefault().NetValue) - ((Global.Instance.ltp[tok].LTP) * (-query1.FirstOrDefault().NetQty))),3);
                 
             
                   
                    Global.Instance.NetBookTable.Rows.Add(i);





                    //Global.Instance.NetBookTable.Rows.Add("Total", "-", Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("StrikePrice")), "-", "-", Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("BuyQty")),
                    //            Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BuyAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BUYVALUE")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("SellQty")),
                    //                 Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SellAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SELLVALUE")),
                    //                   0, 0,
                    //                   Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("MTM")),
                    //                   Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("LTP")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("Delta")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("NetQtyDelta")), 0, 0);
                 
               

             }

             //DataRow ii = Global.Instance.NetBookTable.NewRow();
             //ii["InstrumentName"] = "Total";
             //Global.Instance.NetBookTable.Rows.Add(ii);
    

           DGV.DataSource = Global.Instance.NetBookTable;

          
            }
            catch(Exception ex)
            {
                Task.Factory.StartNew(()=>LogWriterClass.logwritercls.logs("tradeerror",ex.StackTrace.ToString()));

            }

           
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    DGV.Refresh();                 

                };
                this.Invoke(del);
                return;
            }
        }

        public void Test(Object o, ReadOnlyEventArgs<LTPTONETBOOK> obj)
        {
            var rows = Global.Instance.NetBookTable.AsEnumerable().ToList();
            var fg = (rows.FindIndex(a => Convert.ToString(a.Field<string>("Symbol")) == "Total"));
            if (fg >= 0)
            {
                Global.Instance.NetBookTable.Rows[fg].Delete();
            }

            var rowlist = Global.Instance.NetBookTable.AsEnumerable().Where(p => p.Field<int>("Token") == (IPAddress.HostToNetworkOrder(obj.Parameter.TOKEN))).ToList();
            foreach (var i in rowlist)
            {
              
                i["LTP"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(obj.Parameter.LTP)) / 100, 4);
                i["Delta"] = obj.Parameter.DELTA;
                i["NetQtyDelta"] = Math.Round(Convert.ToDouble(Convert.ToDouble(i["NetQty"].ToString()) * (obj.Parameter.DELTA)), 3);
                 i["MTM"] = Math.Round( Convert.ToDouble(Convert.ToDouble(i["NetValue"].ToString()) - (Convert.ToDouble(i["LTP"].ToString()) * (-Convert.ToDouble(i["NetQty"].ToString())) )),3);

               //  lvlMtm.Text = Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("MTM")).ToString();
            }

            Global.Instance.NetBookTable.Rows.Add("Total", "-",0, "-", "-", Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("BuyQty")),
                               Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BuyAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("BUYVALUE")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<int>("SellQty")),
                                    Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SellAvg")), Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("SELLVALUE")),
                                      0, 0,
                                      Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("MTM")),
                                    0, Math.Round( Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("Delta")),4), Math.Round( Global.Instance.NetBookTable.AsEnumerable().Sum(x => x.Field<double>("NetQtyDelta")),4), 0, 0);
                
     
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }  
        private void frmNetBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Settings.Default.Window_LocationNBook = this.Location;

            //// Copy window size to app settings
            //if (this.WindowState == FormWindowState.Normal)
            //{
            //    Settings.Default.Window_SizeNBook = this.Size;
            //}
            //else
            //{
            //    Settings.Default.Window_SizeNBook = this.RestoreBounds.Size;
            //}
            //// Save settings
            //Settings.Default.Save();
            //e.Cancel = true;
            //this.Hide();
        }

   
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in DGV.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in DGV.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "*.xlsx|*.*";
         
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Excel.ExcelUtlity obj = new Excel.ExcelUtlity();
                obj.WriteDataTableToExcel(dt, "Excel Report", saveFileDialog1.FileName, "Details");
            }
        }

        private void frmNetBook_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void lvelNetVal_Click(object sender, EventArgs e)
        {

        }

        private void lvlNetQt_Click(object sender, EventArgs e)
        {

        }

        private void DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                DGV.PerformLayout();
                if (DGV.InvokeRequired)
                {
                    DGV.Invoke(new On_DataPaintdDelegate(DGV_RowPrePaint), sender, e);
                    return;
                }
                if (Convert.ToString(DGV.Rows[e.RowIndex].Cells["Symbol"].Value) == "Total")
                {
                    //  DGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    DGV.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
               
               
            }
            catch (Exception ex)
            {

            }
        }             

    }

    public class SelectListItem
    {
        public string TredingSymbol { get; set; }
     //   public string InstrumentName { get; set; }
       public Int32 TokenNo { get; set; }
        public int BuyQty { get; set; }
        public double BuyAvg { get; set; }
        public int SellQty { get; set; }
        public double SellAvg { get; set; }
        public int NetQty { get; set; }       
        public double BEP { get; set; }
        public double MTOM { get; set; }
        public double NetValue { get; set; }

        public double Sell_rate { get; set; }
        public double buy_rate { get; set; }

     //   public double dev1 { get; set; }
        // public string SellAvg { get; set; }
    }

}
