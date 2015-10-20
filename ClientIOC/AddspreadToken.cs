using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using csv;
using Structure;
using System.Xml;
using System.IO;
using Microsoft.VisualBasic;
namespace Client
{


    public struct _SelectionOut2
    {
        public String PFName;
        public int Token1;
        public int Token2;
        public int Token3;
        public int Token4;
        public String Desc1;
        public String Desc2;
        public String Desc3;
        public String Desc4;
        public int ratio1;
        public int ratio2;
        public int ratio3;
        public int ratio4;
        public int lot1;
        public int lot2;
        public int lot3;
        public int lot4;
        public string buy_sell;
        public string buy_sell2;
        public string buy_sell3;
        public string buy_sell4;
        public string tok1_inst;
        public string tok2_inst;
        public string tok3_inst;
        public string Calc_type;

        public string Order_Type;
        public string first_LEG;
        public string OPT_TICK;
        public string ORDER_DEPTH;
        public string second_Leg;
        public string THRESHOLD;
        public string BIDDING_RANGE;
        public string ReqConnt;



        public string Strat_type;
        public string Symbol1;
        public string Symbol2;
        public string Expiry;
        public string Expiry2;
        public string OptionType;
        public string OptionType2;
        public string StrikePrice;
        public string StrikePrice2;

        public string Symbol3;
        public string Expiry3;
        public string OptionType3;
        public string StrikePrice3;

     //   public String OppositeDesc1;
    //    public String OppositeDesc2;
     //   public String OppositeDesc3;
     //   public String OppositeDesc4;

        public int OppositeToken1;
        public int OppositeToken2;
        public int OppositeToken3;
        public int OppositeToken4;
    }

    public partial class AddSpreadToken : Form
    {
        long date = 0;
        long date2 = 0;
        long date3 = 0;
        long date4 = 0;
        string token = "";

        List<long> T = new List<long>();
        public void firsttoken(string firs)
        {

            label14.Text = firs;

        }



        public _SelectionOut _objOut;
        public _SelectionOut2 _objOut2;



        //private static readonly AddToken instance = new AddToken();
        // public static AddToken Instance
        // {
        //     get
        //     {
        //         return instance;
        //     }
        // }


        public AddSpreadToken()
        {
            InitializeComponent();


        }

        /////////////////////////////////////////////    Exchange  ////////////////////////////////////////////////////////
        void Exchange()
        {
            string[] strexchange = { "NFO", "SPREAD" };
            EXcomboBox1.Items.AddRange(strexchange);
            EXcomboBox1.SelectedIndex = EXcomboBox1.Items.IndexOf("NFO");
            EXcomboBox2.Items.AddRange(strexchange);
            EXcomboBox2.SelectedIndex = EXcomboBox2.Items.IndexOf("NFO");
            EXcomboBox3.Items.AddRange(strexchange);
            EXcomboBox4.Items.AddRange(strexchange);

            string[] strordertype = { "Normal", "Spread" };
            ORcomboBox2.Items.AddRange(strordertype);
            ORcomboBox2.SelectedIndex = ORcomboBox2.Items.IndexOf("Normal");
            ORcomboBox3.Items.AddRange(strordertype);
            ORcomboBox3.SelectedIndex = ORcomboBox3.Items.IndexOf("Normal");
            ORcomboBox4.Items.AddRange(strordertype);
            ORcomboBox5.Items.AddRange(strordertype);

            string[] Strategy_type = { "2_LEG", "3_LEG", "4_LEG" };
            Strategy_type_comboBox1.Items.AddRange(Strategy_type);

            string[] Threshold = { "50", "75", "100", "125", "150", "200" };
            textBox_Threshold.Items.AddRange(Threshold);

            string[] ReqConnt = {"25" ,"50","100","150","200","400"};
            txtReqConnt.Items.AddRange(ReqConnt);

            string[] OrderType = { "Bidding", "IOC" };
            combox_OrderType.Items.AddRange(OrderType);
            string[] firstBid = { "NORMAL", "WEIGHTED", "BESTBID" };
            comb_1stleg.Items.AddRange(firstBid);
            string[] OPTTICK = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            combox_optTrick.Items.AddRange(OPTTICK);
            string[] ORDERDEPTH = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            comb_OrderDepth.Items.AddRange(ORDERDEPTH);
            string[] secondLeg = { "MKT", "LIMIT", "BEST BID" };
            com2nd_leg.Items.AddRange(secondLeg);
            string[] BIDDINGRANGE = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            comb_BidRange.Items.AddRange(BIDDINGRANGE);

            string[] Calc_type = { "BaseDiff", "ProdDiff" };
            Calc_typecomboBox1.Items.AddRange(Calc_type);
           // Calc_typecomboBox1.SelectedIndex = Calc_typecomboBox1.Items.IndexOf("ProdDiff");

            string[] buy_sel = { "Buy", "Sell" };
            //  cmd_buy_sell1.Items.AddRange(buy_sel);
            cmd_buy_sell2.Items.AddRange(buy_sel);
            cmd_buy_sell3.Items.AddRange(buy_sel);
            cmd_buy_sell4.Items.AddRange(buy_sel);

        }

 
        /////////////////////////////////////////////    ORDER Type  ////////////////////////////////////////////////////////

        void order_type()
        {
            List<string> list = new List<string>();
            list.Add("Normal");
            list.Add("Spread");
            foreach (string ex in list)
            {

                ORcomboBox2.Items.Add(ex);

            }


        }

        void InsertType_fun()
        {
          //  Holder.clliest_contractfile = CSV_Class.cimlist.Where(ab => ab.InstrumentName != null).ToList();

            string[] dis = CSV_Class.cimlist.Where(ab => ab.InstrumentName != null).Select(a => a.InstrumentName).Distinct().ToArray();
            INSTcomboBox3.Items.AddRange(dis);
            INSTcomboBox4.Items.AddRange(dis);
             INSTcomboBox5.Items.AddRange(dis);
          //  INSTcomboBox6.Items.AddRange(dis);
         
        }

        ////////////////////////////////////////////////////////////////////////////////////////// Exoirry  /////////////


        public static DateTime ConvertFromTimestamp(long timstamp)
        {
            DateTime datetime = new DateTime(1980, 1, 1, 0, 0, 0, 0);
            return datetime.AddSeconds(timstamp);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstType();
        }
        private void INSTcomboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstType();
        }
        /////////////////////////////////////////////////////////////////////////////////////// SYMBOL ////////////////////////

        void InstType()
        {
            SYMcomboBox4.Text = "";
            SYMcomboBox4.Items.Clear();

            EXPcomboBox5.Text = "";
             EXPcomboBox5.Items.Clear();
L_O_S.Text = "";
OPcomboBox6.Text = "";
            OPcomboBox6.Items.Clear();
            STRIKecomboBox7.Text = "";


            string[] symm = CSV_Class.cimlist.Where(a => a.InstrumentName == INSTcomboBox3.Text && a.InstrumentName != "" && a.InstrumentName != null).Select(q => q.Symbol).Distinct().ToArray();

            SYMcomboBox4.Items.AddRange(symm);

            OPcomboBox6.Enabled = true;
            STRIKecomboBox7.Enabled = true;
            EXPcomboBox5.Items.Clear();
            OPcomboBox6.Items.Clear();
        //    STRIKecomboBox7.Items.Clear();

            // }
            if (INSTcomboBox3.Text == "FUTIVX" || INSTcomboBox3.Text == "FUTIDX" || INSTcomboBox3.Text == "FUTSTK")
            {

                OPcomboBox6.Enabled = false;
                STRIKecomboBox7.Enabled = false;
                EXPcomboBox5.Items.Clear();
                OPcomboBox6.Items.Clear();
              //  STRIKecomboBox7.Items.Clear();


            }


        }
        void InstType2()
        {
            SYMcomboBox5.Text = "";
            SYMcomboBox5.Items.Clear();

            EXPcomboBox6.Text = "";
            EXPcomboBox6.Items.Clear();
L_O_S2.Text = "";
OPcomboBox7.Text = "";
OPcomboBox7.Items.Clear();
STRIKecomboBox8.Text = "";


            string[] symm = CSV_Class.cimlist.Where(a => a.InstrumentName == INSTcomboBox4.Text && a.InstrumentName != "" && a.InstrumentName != null).Select(q => q.Symbol).Distinct().ToArray();
 
            SYMcomboBox5.Items.AddRange(symm);

            OPcomboBox7.Enabled = true;
            STRIKecomboBox8.Enabled = true;
            EXPcomboBox6.Items.Clear();
            OPcomboBox7.Items.Clear();
            STRIKecomboBox8.Items.Clear();

            // }
            if (INSTcomboBox4.Text == "FUTIVX" || INSTcomboBox4.Text == "FUTIDX" || INSTcomboBox4.Text == "FUTSTK")
            {

                OPcomboBox7.Enabled = false;
                STRIKecomboBox8.Enabled = false;
                EXPcomboBox6.Items.Clear();
                OPcomboBox7.Items.Clear();
                STRIKecomboBox8.Items.Clear();


            }


        }
        void InstType3()
        {
            SYMcomboBox6.Text = "";
            SYMcomboBox6.Items.Clear();
            EXPcomboBox7.Items.Clear();
            EXPcomboBox7.Text = "";
            L_O_S3.Text = "";
                OPcomboBox8.Items.Clear();
                OPcomboBox8.Text = "";
                    STRIKecomboBox9.Text ="";

            string[] symm = CSV_Class.cimlist.Where(a => a.InstrumentName == INSTcomboBox5.Text && a.InstrumentName != "" && a.InstrumentName != null).Select(q => q.Symbol).Distinct().ToArray();

            SYMcomboBox6.Items.AddRange(symm);

            OPcomboBox8.Enabled = true;
            STRIKecomboBox9.Enabled = true;
            EXPcomboBox7.Items.Clear();
            OPcomboBox8.Items.Clear();
            STRIKecomboBox9.Items.Clear();

            // }
            if (INSTcomboBox5.Text == "FUTIVX" || INSTcomboBox5.Text == "FUTIDX" || INSTcomboBox5.Text == "FUTSTK")
            {

                OPcomboBox8.Enabled = false;
                STRIKecomboBox9.Enabled = false;
                EXPcomboBox7.Items.Clear();
                OPcomboBox8.Items.Clear();
                STRIKecomboBox9.Items.Clear();


            }


        }
        void InstType4()
        {
            SYMcomboBox7.Text = "";
            SYMcomboBox7.Items.Clear();

            string[] symm = CSV_Class.cimlist.Where(a => a.InstrumentName == INSTcomboBox6.Text && a.InstrumentName != "" && a.InstrumentName != null).Select(q => q.Symbol).Distinct().ToArray();

            SYMcomboBox7.Items.AddRange(symm);

            OPcomboBox9.Enabled = true;
            STRIKecomboBox10.Enabled = true;
            EXPcomboBox8.Items.Clear();
            OPcomboBox9.Items.Clear();
            STRIKecomboBox10.Items.Clear();

            // }
            if (INSTcomboBox6.Text == "FUTIVX" || INSTcomboBox6.Text == "FUTIDX" || INSTcomboBox6.Text == "FUTSTK")
            {

                OPcomboBox9.Enabled = false;
                STRIKecomboBox10.Enabled = false;
                EXPcomboBox8.Items.Clear();
                OPcomboBox9.Items.Clear();
                STRIKecomboBox10.Items.Clear();


            }


        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }
        private void SYMcomboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Expiry();
        }
        void Expiry()
        {
            EXPcomboBox5.Text = "";
            EXPcomboBox5.Items.Clear();

          
            L_O_S.Text = "";
            OPcomboBox6.Text = "";
            OPcomboBox6.Items.Clear();
            STRIKecomboBox7.Text = "";
  T = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox4.Text && a.InstrumentName == INSTcomboBox3.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();

            foreach (long ex in T)
            {

                string on = ConvertFromTimestamp(ex).ToShortDateString();

                EXPcomboBox5.Items.Add(on);

            }

            OPcomboBox6.Items.Clear();
            STRIKecomboBox7.Text = ""; 


        }
        void Expiry2()
        {
            EXPcomboBox6.Text = "";
            EXPcomboBox6.Items.Clear();


            L_O_S2.Text = "";
            OPcomboBox7.Text = "";
            OPcomboBox7.Items.Clear();
            STRIKecomboBox8.Text = "";

    T = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox5.Text && a.InstrumentName == INSTcomboBox4.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();

            foreach (long ex in T)
            {

                string on = ConvertFromTimestamp(ex).ToShortDateString();

                EXPcomboBox6.Items.Add(on);
                //  date = ex;

            }

            OPcomboBox7.Items.Clear();
           


            STRIKecomboBox8.Text = "";


        }
        void Expiry3()
        {
            EXPcomboBox7.Text = "";
            EXPcomboBox7.Items.Clear();
            L_O_S3.Text = "";
            OPcomboBox8.Items.Clear();
            OPcomboBox8.Text = "";
            L_O_S3.Text = "";
            STRIKecomboBox9.Text = "";


          
            // var exp = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox4.Text && a.InstrumentName == INSTcomboBox3.Text).OrderBy(r => r.ExpiryDate).Select(d=>d.ExpiryDate).Distinct().ToList();
            T = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox6.Text && a.InstrumentName == INSTcomboBox5.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();

            //   T = Holder.clliest_contractfile.Where(a => a.Symbol == SYMcomboBox4.Text && a.InstrumentName == INSTcomboBox3.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();

            // IEnumerable<long> exp = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox4.Text && a.InstrumentName == INSTcomboBox3.Text).Select(r => r.ExpiryDate).Distinct().ToList();

            foreach (long ex in T)
            {

                string on = ConvertFromTimestamp(ex).ToShortDateString();

                EXPcomboBox7.Items.Add(on);
                //  date = ex;

            }

            OPcomboBox8.Items.Clear();
            STRIKecomboBox9.Items.Clear();


        }
        void Expiry4()
        {
            EXPcomboBox8.Text = "";
            EXPcomboBox8.Items.Clear();
            T = CSV_Class.cimlist.Where(a => a.Symbol == SYMcomboBox7.Text && a.InstrumentName == INSTcomboBox6.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();

            foreach (long ex in T)
            {

                string on = ConvertFromTimestamp(ex).ToShortDateString();

                EXPcomboBox8.Items.Add(on);

            }

            OPcomboBox9.Items.Clear();
            STRIKecomboBox10.Items.Clear();


        }
        private void EXPcomboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int i = EXPcomboBox5.SelectedIndex;
         //   date = T[i];

         //   optionType();
        }
      
        private void EXPcomboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int i = EXPcomboBox5.SelectedIndex;
            date = T[i];

            optionType();
        }
        void optionType()
        {
            OPcomboBox6.Text = "";
            OPcomboBox6.Items.Clear();
            STRIKecomboBox7.Text = "";

            string df = INSTcomboBox3.Text;

            string[] op = CSV_Class.cimlist.Where(a => a.ExpiryDate == date && a.InstrumentName == INSTcomboBox3.Text && a.Symbol == SYMcomboBox4.Text).Select(s => s.OptionType).Distinct().ToArray();
            var tokenw = CSV_Class.cimlist.Where(a => a.ExpiryDate == date && a.Symbol == SYMcomboBox4.Text).First().Token;
            int k = CSV_Class.cimlist.FindIndex(a => a.Token == tokenw);
            var qnty = CSV_Class.cimlist[k].BoardLotQuantity;
            L_O_S.Text = qnty.ToString();
            token = tokenw.ToString();
            OPcomboBox6.Items.AddRange(op);


            STRIKecomboBox7.Text = "" ;



        }
        void optionType2()
        {

            OPcomboBox7.Text = "";
            OPcomboBox7.Items.Clear();


            L_O_S2.Text = "";
            STRIKecomboBox8.Text = "";

            string df = INSTcomboBox4.Text;

            string[] op = CSV_Class.cimlist.Where(a => a.ExpiryDate == date2 && a.InstrumentName == INSTcomboBox4.Text && a.Symbol == SYMcomboBox5.Text).Select(s => s.OptionType).Distinct().ToArray();

            var tokenw = CSV_Class.cimlist.Where(a => a.ExpiryDate == date2 && a.Symbol == SYMcomboBox5.Text).First().Token;
            int k = CSV_Class.cimlist.FindIndex(a => a.Token == tokenw);
            var qnty = CSV_Class.cimlist[k].BoardLotQuantity;
            L_O_S2.Text = qnty.ToString();
            token = tokenw.ToString();
            OPcomboBox7.Items.AddRange(op);

            // combo_Exoiry.Items.Clear();

            STRIKecomboBox8.Items.Clear();



        }
        void optionType3()
        {
            OPcomboBox8.Text = "";
            OPcomboBox8.Items.Clear();
            STRIKecomboBox9.Text = "";

            string df = INSTcomboBox5.Text;

            string[] op = CSV_Class.cimlist.Where(a => a.ExpiryDate == date3 && a.InstrumentName == INSTcomboBox5.Text && a.Symbol == SYMcomboBox6.Text).Select(s => s.OptionType).Distinct().ToArray();
            //   string[] op = Holder.clliest_contractfile.Where(a => a.ExpiryDate == date && a.InstrumentName == INSTcomboBox3.Text && a.Symbol == SYMcomboBox4.Text).Select(s => s.OptionType).Distinct().ToArray();

            var tokenw = CSV_Class.cimlist.Where(a => a.ExpiryDate == date3 && a.Symbol == SYMcomboBox6.Text).First().Token;
            int k = CSV_Class.cimlist.FindIndex(a => a.Token == tokenw);
            var qnty = CSV_Class.cimlist[k].BoardLotQuantity;
            L_O_S3.Text = qnty.ToString();
            token = tokenw.ToString();
            OPcomboBox8.Items.AddRange(op);

            // combo_Exoiry.Items.Clear();

            STRIKecomboBox9.Items.Clear();



        }
        void optionType4()
        {
            OPcomboBox9.Text = "";
            OPcomboBox9.Items.Clear();
            string df = INSTcomboBox6.Text;

            string[] op = CSV_Class.cimlist.Where(a => a.ExpiryDate == date4 && a.InstrumentName == INSTcomboBox6.Text && a.Symbol == SYMcomboBox7.Text).Select(s => s.OptionType).Distinct().ToArray();

            var tokenw = CSV_Class.cimlist.Where(a => a.ExpiryDate == date4 && a.Symbol == SYMcomboBox7.Text).First().Token;
            int k = CSV_Class.cimlist.FindIndex(a => a.Token == tokenw);
            var qnty = CSV_Class.cimlist[k].BoardLotQuantity;
            L_O_S4.Text = qnty.ToString();
            token = tokenw.ToString();
            OPcomboBox9.Items.AddRange(op);


            STRIKecomboBox10.Items.Clear();



        }
      
        void strike_prise()
        {  
           // STRIKecomboBox7.Items.Clear();
            STRIKecomboBox7.Text = "";
            var p = CSV_Class.cimlist.Where(a => a.ExpiryDate == date && a.InstrumentName == INSTcomboBox3.Text && a.Symbol == SYMcomboBox4.Text && a.OptionType == OPcomboBox6.Text).OrderBy(a=>a.StrikePrice).Select(a => a.StrikePrice/100).Distinct().ToList();
            STRIKecomboBox7.DataSource = p;
            STRIKecomboBox7.DisplayMember = "StrikePrice"; 
            
            //// int price=CSV_Class.cimlist[p].StrikePrice;
            //foreach (int x in p)
            //    STRIKecomboBox7.Items.Add(x/100);


        }
        void strike_prise2()
        {
           // STRIKecomboBox8.Items.Clear();
            STRIKecomboBox8.Text = "";

            var p = CSV_Class.cimlist.Where(a => a.ExpiryDate == date2 && a.InstrumentName == INSTcomboBox4.Text && a.Symbol == SYMcomboBox5.Text && a.OptionType == OPcomboBox7.Text).OrderBy(a => a.StrikePrice).Select(a => a.StrikePrice).Distinct().ToArray();
            // int price=CSV_Class.cimlist[p].StrikePrice;
            foreach (int x in p)
                STRIKecomboBox8.Items.Add(x/100);


        }
        void strike_prise3()
        {
            STRIKecomboBox9.Text = "";
            var p = CSV_Class.cimlist.Where(a => a.ExpiryDate == date3 && a.InstrumentName == INSTcomboBox5.Text && a.Symbol == SYMcomboBox6.Text && a.OptionType == OPcomboBox8.Text).OrderBy(a => a.StrikePrice).Select(a => a.StrikePrice).Distinct().ToArray();
            foreach (int x in p)
                STRIKecomboBox9.Items.Add(x/100);


        }
        void strike_prise4()
        {

            var p = CSV_Class.cimlist.Where(a => a.ExpiryDate == date4 && a.InstrumentName == INSTcomboBox6.Text && a.Symbol == SYMcomboBox7.Text && a.OptionType == OPcomboBox9.Text).OrderBy(a => a.StrikePrice).Select(a => a.StrikePrice).Distinct().ToArray();
            foreach (int x in p)
                STRIKecomboBox10.Items.Add(x/100);


        }

     public   void show()
        {
            switch(Strategy_type_comboBox1.Text)
            {
                case "2_LEG":
                    panel_leg1.Visible = true;panel_leg2.Visible=true; panel_leg3.Visible = false;panel_leg4.Visible=false;
                    break;
                case "3_LEG":
                     panel_leg1.Visible = true;panel_leg2.Visible=true;panel_leg3.Visible=true;panel_leg4.Visible=false;
                     break;
                case "4_LEG":
               // panel_leg1.Visible = true;panel_leg2.Visible=true;panel_leg3.Visible=true;panel_leg4.Visible=true;
                break;

               // case "2_LEG":
               //     panel_leg1.Visible = true;panel_leg2.Visible=true; panel_leg3.Visible = false;panel_leg4.Visible=false;

               // EXcomboBox1.Visible = true; ORcomboBox2.Visible = true; INSTcomboBox3.Visible = true; SYMcomboBox4.Visible = true;EXPcomboBox5.Visible = true; L_O_S.Visible = true;OPcomboBox6.Visible = true; STRIKecomboBox7.Visible = true; textBox_Ratio.Visible = true;
               // EXcomboBox2.Visible = true; ORcomboBox3.Visible = true; INSTcomboBox4.Visible = true; SYMcomboBox5.Visible = true; EXPcomboBox6.Visible = true; L_O_S2.Visible = true; OPcomboBox7.Visible = true; STRIKecomboBox8.Visible = true; textBox_Ratio2.Visible = true;
               // EXcomboBox3.Visible = false; ORcomboBox4.Visible = false; INSTcomboBox5.Visible = false; SYMcomboBox6.Visible = false; EXPcomboBox7.Visible = false; L_O_S3.Visible = false; OPcomboBox8.Visible = false; STRIKecomboBox9.Visible = false; textBox_Rati3.Visible = false;
               //                EXcomboBox4.Visible = false; ORcomboBox5.Visible = false; INSTcomboBox6.Visible = false; SYMcomboBox7.Visible = false; EXPcomboBox8.Visible = false; L_O_S4.Visible = false; OPcomboBox9.Visible = false; STRIKecomboBox10.Visible = false; textBox_Ratio4.Visible = false;
               //  checkBox1.Visible = false; checkBox2.Visible = false; checkBox3.Visible = false; checkBox4.Visible = false;
               //  cmd_buy_sell1.Visible = true;  cmd_buy_sell2.Visible = true;cmd_buy_sell3.Visible =false;cmd_buy_sell4.Visible =false;
               // break;

               // case "3_LEG":
               //  panel_leg1.Visible = true;panel_leg2.Visible=true;panel_leg3.Visible=true;panel_leg4.Visible=false;
               // EXcomboBox1.Visible = true; ORcomboBox2.Visible = true; INSTcomboBox3.Visible = true; SYMcomboBox4.Visible = true; EXPcomboBox5.Visible = true; L_O_S.Visible = true; OPcomboBox6.Visible = true; STRIKecomboBox7.Visible = true; textBox_Ratio.Visible = true;
               // EXcomboBox2.Visible = true; ORcomboBox3.Visible = true; INSTcomboBox4.Visible = true; SYMcomboBox5.Visible = true; EXPcomboBox6.Visible = true; L_O_S2.Visible = true; OPcomboBox7.Visible = true; STRIKecomboBox8.Visible = true; textBox_Ratio2.Visible = true;
               // EXcomboBox3.Visible = true; ORcomboBox4.Visible = true; INSTcomboBox5.Visible = true; SYMcomboBox6.Visible = true; EXPcomboBox7.Visible = true; L_O_S3.Visible = true; OPcomboBox8.Visible = true; STRIKecomboBox9.Visible = true; textBox_Rati3.Visible = true;
               //EXcomboBox4.Visible = false; ORcomboBox5.Visible = false; INSTcomboBox6.Visible = false; SYMcomboBox7.Visible = false; EXPcomboBox8.Visible = false; L_O_S4.Visible = false; OPcomboBox9.Visible = false; STRIKecomboBox10.Visible = false; textBox_Ratio4.Visible = false;
               //  checkBox1.Visible = false; checkBox2.Visible = false; checkBox3.Visible = false; checkBox4.Visible = false;
               //      cmd_buy_sell1.Visible = true;  cmd_buy_sell2.Visible = true;cmd_buy_sell3.Visible = true;cmd_buy_sell4.Visible =false;
               // break;
               // case "4_LEG":
               //      panel_leg1.Visible = true;panel_leg2.Visible=true;panel_leg3.Visible=true;panel_leg4.Visible=true;
               // EXcomboBox1.Visible = true; ORcomboBox2.Visible = true; INSTcomboBox3.Visible = true; SYMcomboBox4.Visible = true; EXPcomboBox5.Visible = true; L_O_S.Visible = true; OPcomboBox6.Visible = true; STRIKecomboBox7.Visible = true; textBox_Ratio.Visible = true;
               // EXcomboBox2.Visible = true; ORcomboBox3.Visible = true; INSTcomboBox4.Visible = true; SYMcomboBox5.Visible = true; EXPcomboBox6.Visible = true; L_O_S2.Visible = true; OPcomboBox7.Visible = true; STRIKecomboBox8.Visible = true; textBox_Ratio2.Visible = true;
               // EXcomboBox3.Visible = true; ORcomboBox4.Visible = true; INSTcomboBox5.Visible = true; SYMcomboBox6.Visible = true; EXPcomboBox7.Visible = true; L_O_S3.Visible = true; OPcomboBox8.Visible = true; STRIKecomboBox9.Visible = true; textBox_Rati3.Visible = true;
               //                 EXcomboBox4.Visible = true; ORcomboBox5.Visible = true; INSTcomboBox6.Visible = true; SYMcomboBox7.Visible = true; EXPcomboBox8.Visible = true; L_O_S4.Visible = true; OPcomboBox9.Visible = true; STRIKecomboBox10.Visible = true; textBox_Ratio4.Visible = true;
               //                 checkBox1.Visible = true; checkBox2.Visible = true; checkBox3.Visible = true; checkBox4.Visible = true;
               //         cmd_buy_sell1.Visible = true;  cmd_buy_sell2.Visible = true;cmd_buy_sell3.Visible = true;cmd_buy_sell4.Visible = true;
               // break;
            
            
            
        }
        
      
        
        
        }
        void Bid_Show()
        {
            switch (combox_OrderType.Text)
            {
                case "Bidding":
                    label9.Visible = true; comb_1stleg.Visible = true; label10.Visible = true; combox_optTrick.Visible = true; label11.Visible = true; comb_OrderDepth.Visible = true; label12.Visible = true; com2nd_leg.Visible = true; label13.Visible = true; textBox_Threshold.Visible = true; label15.Visible = true; comb_BidRange.Visible = true;label17.Visible = true;txtReqConnt.Visible=true;
                    break;
                case "IOC":
                    label9.Visible = false; comb_1stleg.Visible = false; label10.Visible = false; combox_optTrick.Visible = false; label11.Visible = false; comb_OrderDepth.Visible = false; label12.Visible = false; com2nd_leg.Visible = false; label13.Visible = false; textBox_Threshold.Visible = false; label15.Visible = false; comb_BidRange.Visible = false;label17.Visible = false ;txtReqConnt.Visible=false;

                    break;

            }


        }
        private void STRIKecomboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void leg2()
        {

            int t = 0, t2 = 0,tt=0,tt2=0;
            if (INSTcomboBox3.Text == "FUTIVX" || INSTcomboBox3.Text == "FUTIDX" || INSTcomboBox3.Text == "FUTSTK")
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text);
            }

            else
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text && q.OptionType == OPcomboBox6.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox7.Text) * 100);
                string ad3 = INSTcomboBox4.Text == "OPTSTK" ? "FUTSTK" : "FUTIDX";
                tt = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == ad3);

            }
            int Token = CSV_Class.cimlist[t].Token;
            int OppositeToken = CSV_Class.cimlist[tt].Token;

            if (INSTcomboBox4.Text == "FUTIVX" || INSTcomboBox4.Text == "FUTIDX" || INSTcomboBox4.Text == "FUTSTK")
            {
                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text);
            }
            else
            {

                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text && q.OptionType == OPcomboBox7.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox8.Text) * 100);
                string ad3 =   INSTcomboBox4.Text == "OPTSTK" ?"FUTSTK" :"FUTIDX";

                tt2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == ad3);
              
            
            }
            int Token2 = CSV_Class.cimlist[t2].Token;
            int OppositeToken2 = CSV_Class.cimlist[tt2].Token;

            _objOut2.PFName = txtpfName.Text;
            _objOut2.Token2 = Token2;
            _objOut2.Desc1 = CSV_Class.cimlist[t].EGMAGM;
            _objOut2.OppositeToken2 = OppositeToken2;
          //  _objOut2.OppositeDesc1 = CSV_Class.cimlist[tt].EGMAGM;

            Global.Instance.Ratio.TryAdd(_objOut2.Token2, Convert.ToInt32(textBox_Ratio.Text));
            _objOut2.Token1 = Token;
            _objOut2.Desc2 = CSV_Class.cimlist[t2].EGMAGM;

            _objOut2.OppositeToken1 = OppositeToken;
           // _objOut2.OppositeDesc2 = CSV_Class.cimlist[tt2].EGMAGM;

            _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
            _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
            _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
            _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
            _objOut2.tok2_inst = INSTcomboBox4.Text;
            _objOut2.tok1_inst = INSTcomboBox3.Text;
            _objOut2.Calc_type = Calc_typecomboBox1.Text;
           


            _objOut2.Order_Type = combox_OrderType.Text;
            _objOut2.first_LEG = comb_1stleg.Text;
            _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
            _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
            _objOut2.second_Leg = com2nd_leg.Text;
            _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
            _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;

            _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
            _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
            _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
            _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
            _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
            _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
            _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
            _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
            _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;
            _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
            
            Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));
          
        }
        void leg3()
        {
            int t = 0, t2 = 0, t3 = 0,tt =0, tt2 =0, tt3 =0;

            if (INSTcomboBox3.Text == "FUTIVX" || INSTcomboBox3.Text == "FUTIDX" || INSTcomboBox3.Text == "FUTSTK")
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text);
           
            }

            else
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text && q.OptionType == OPcomboBox6.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox7.Text)*100);
                string ad3 = INSTcomboBox4.Text == "OPTSTK" ? "FUTSTK" : "FUTIDX";
                tt = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == ad3);

            }
            int Token = CSV_Class.cimlist[t].Token;
            int OppositeToken = CSV_Class.cimlist[tt].Token;

            if (INSTcomboBox4.Text == "FUTIVX" || INSTcomboBox4.Text == "FUTIDX" || INSTcomboBox4.Text == "FUTSTK")
            {
                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text);
            }
            else
            {
                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text && q.OptionType == OPcomboBox7.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox8.Text) * 100);
                string ad3 = INSTcomboBox4.Text == "OPTSTK" ? "FUTSTK" : "FUTIDX";
                tt2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == ad3);
           
           
            }
            int Token2 = CSV_Class.cimlist[t2].Token;
            int OppositeToken2 = CSV_Class.cimlist[tt2].Token;
            if (INSTcomboBox5.Text == "FUTIVX" || INSTcomboBox5.Text == "FUTIDX" || INSTcomboBox5.Text == "FUTSTK")
            {
                t3 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox6.Text && q.ExpiryDate == this.date3 && q.InstrumentName == INSTcomboBox5.Text);
            }
            else
            {
                t3 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox6.Text && q.ExpiryDate == this.date3 && q.InstrumentName == INSTcomboBox5.Text && q.OptionType == OPcomboBox8.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox9.Text) * 100);
                string ad3 = INSTcomboBox4.Text == "OPTSTK" ? "FUTSTK" : "FUTIDX";
                tt3 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox6.Text && q.ExpiryDate == this.date3 && q.InstrumentName == ad3);
      
            }
            int Token3 = CSV_Class.cimlist[t3].Token;
            int OppositeToken3 = CSV_Class.cimlist[tt3].Token;


            _objOut2.PFName = txtpfName.Text;
            _objOut2.Token1 = Token;
            _objOut2.Desc1 = CSV_Class.cimlist[t].EGMAGM;
            _objOut2.OppositeToken1 = OppositeToken;
           // _objOut2.OppositeDesc1 = CSV_Class.cimlist[tt].EGMAGM;

            _objOut2.Token2 = Token2;
            _objOut2.Desc2 = CSV_Class.cimlist[t2].EGMAGM;
            _objOut2.OppositeToken2 = OppositeToken2;
           // _objOut2.OppositeDesc2 = CSV_Class.cimlist[tt2].EGMAGM;

            _objOut2.Token3= Token3;
            _objOut2.Desc3 = CSV_Class.cimlist[t3].EGMAGM;
            _objOut2.OppositeToken3 = OppositeToken3;
           // _objOut2.OppositeDesc3 = CSV_Class.cimlist[tt3].EGMAGM;

            _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
            _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
            _objOut2.ratio3 = Convert.ToInt32(textBox_Rati3.Text);
            _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
            _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
            _objOut2.buy_sell3 = Convert.ToString(cmd_buy_sell3.Text);


            _objOut2.Order_Type = combox_OrderType.Text;
            _objOut2.first_LEG = comb_1stleg.Text;
            _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
            _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
            _objOut2.second_Leg = com2nd_leg.Text;
            _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
            _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;
            _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;


           
         
           
           Global.Instance.Ratio.TryAdd(_objOut2.Token2,Convert.ToInt32(textBox_Ratio.Text));
           
            
            _objOut2.tok2_inst = INSTcomboBox4.Text;
            _objOut2.tok1_inst = INSTcomboBox3.Text;
            _objOut2.tok3_inst = INSTcomboBox5.Text;
            _objOut2.Calc_type = Calc_typecomboBox1.Text;

            _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
            _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
            _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
            _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
            _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
            _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
            _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
            _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
            _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;

            _objOut2.Symbol3 = SYMcomboBox6.Text == "" ? "0" : SYMcomboBox6.Text;
            _objOut2.Expiry3 = EXPcomboBox7.Text == "" ? "0" : EXPcomboBox7.Text;
            _objOut2.OptionType3 = OPcomboBox8.Text == "" ? "0" : OPcomboBox8.Text;
            _objOut2.StrikePrice3 = STRIKecomboBox9.Text == "" ? "0" : STRIKecomboBox9.Text;

            _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
           
            Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));
            Global.Instance.Ratio.TryAdd(_objOut2.Token3, Convert.ToInt32(textBox_Rati3.Text));  
        }
        void leg4()
        {
            int t = 0, t2 = 0, t3 = 0, t4= 0;
            if (INSTcomboBox3.Text == "FUTIVX" || INSTcomboBox3.Text == "FUTIDX" || INSTcomboBox3.Text == "FUTSTK")
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text);
            }

            else
            {
                t = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox4.Text && q.ExpiryDate == this.date && q.InstrumentName == INSTcomboBox3.Text && q.OptionType == OPcomboBox6.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox7.Text)*100);
            }
            int Token = CSV_Class.cimlist[t].Token;


            if (INSTcomboBox4.Text == "FUTIVX" || INSTcomboBox4.Text == "FUTIDX" || INSTcomboBox4.Text == "FUTSTK")
            {
                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text);
            }
            else
            {
                t2 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox5.Text && q.ExpiryDate == this.date2 && q.InstrumentName == INSTcomboBox4.Text && q.OptionType == OPcomboBox7.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox8.Text)*100);
            }
            int Token2 = CSV_Class.cimlist[t2].Token;

            if (INSTcomboBox5.Text == "FUTIVX" || INSTcomboBox5.Text == "FUTIDX" || INSTcomboBox5.Text == "FUTSTK")
            {
                t3 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox6.Text && q.ExpiryDate == this.date3 && q.InstrumentName == INSTcomboBox5.Text);
            }
            else
            {
                t3 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox6.Text && q.ExpiryDate == this.date3 && q.InstrumentName == INSTcomboBox5.Text && q.OptionType == OPcomboBox8.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox9.Text)*100);
            }
            int Token3 = CSV_Class.cimlist[t3].Token;

            if (INSTcomboBox6.Text == "FUTIVX" || INSTcomboBox6.Text == "FUTIDX" || INSTcomboBox6.Text == "FUTSTK")
            {
                t4 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox7.Text && q.ExpiryDate == this.date4 && q.InstrumentName == INSTcomboBox6.Text);
            }
            else
            {
                t4 = CSV_Class.cimlist.FindIndex(q => q.Symbol == SYMcomboBox7.Text && q.ExpiryDate == this.date4 && q.InstrumentName == INSTcomboBox6.Text && q.OptionType == OPcomboBox9.Text && q.StrikePrice == Convert.ToInt32(STRIKecomboBox10.Text)*100);
            }
            int Token4 = CSV_Class.cimlist[t4].Token;
            _objOut2.PFName = txtpfName.Text;
            _objOut2.Token1 = Token;
            _objOut2.Desc1 = CSV_Class.cimlist[t].EGMAGM;
            _objOut2.Token2 = Token2;
            _objOut2.Desc2 = CSV_Class.cimlist[t2].EGMAGM;
            _objOut2.Token3 = Token3;
            _objOut2.Desc3 = CSV_Class.cimlist[t3].EGMAGM;
            _objOut2.Token4 = Token4;
            _objOut2.Desc4 = CSV_Class.cimlist[t4].EGMAGM;
            _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
            _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
            _objOut2.ratio3 = Convert.ToInt32(textBox_Rati3.Text);
            _objOut2.ratio4 = Convert.ToInt32(textBox_Ratio4.Text);
            _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
            _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
            _objOut2.buy_sell3 = Convert.ToString(cmd_buy_sell3.Text);
            _objOut2.buy_sell4 = Convert.ToString(cmd_buy_sell4.Text);
            
        }

        int oldtoken = 0;
        public string Token1sp;
        public string Token2sp;
        public string NEARSP;
        public string FARSP;

        public string Token3sp;
        public string tok3sp;

        public string OppsitToken1;
        public string OppsitToken2;
        public string OppsitToken3;
        private void button1_Click_1(object sender, EventArgs e)
        {
            bool Flg = false;
            if (button1.Text == "Modify")
            {


                if (Strategy_type_comboBox1.Text == "3_LEG")
                {
                    _objOut2.Symbol3 = SYMcomboBox6.Text == "" ? "0" : SYMcomboBox6.Text;
                    _objOut2.Expiry3 = EXPcomboBox7.Text == "" ? "0" : EXPcomboBox7.Text;
                    _objOut2.OptionType3 = OPcomboBox8.Text == "" ? "0" : OPcomboBox8.Text;
                    _objOut2.StrikePrice3 = STRIKecomboBox9.Text == "" ? "0" : STRIKecomboBox9.Text;



                    _objOut2.PFName = txtpfName.Text;
                    _objOut2.Token1 = Convert.ToInt32(Token1sp);
                    _objOut2.Desc1 = NEARSP;
                    _objOut2.Token2 = Convert.ToInt32(Token2sp);
                    _objOut2.Desc2 = FARSP;
                    _objOut2.Token3 = Convert.ToInt32(Token3sp);
                    _objOut2.Desc3 = tok3sp;

                    _objOut2.OppositeToken2 = Convert.ToInt32(OppsitToken2);
                    _objOut2.OppositeToken1 = Convert.ToInt32(OppsitToken1);
                    _objOut2.OppositeToken3 = Convert.ToInt32(OppsitToken3); 

                    _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
                    _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
                    _objOut2.ratio3 = Convert.ToInt32(textBox_Rati3.Text);
                    _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
                    _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
                    _objOut2.buy_sell3 = Convert.ToString(cmd_buy_sell3.Text);


                    _objOut2.Order_Type = combox_OrderType.Text;
                    _objOut2.first_LEG = comb_1stleg.Text;
                    _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
                    _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
                    _objOut2.second_Leg = com2nd_leg.Text;
                    _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
                    _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;
                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;


                    Global.Instance.Ratio.TryAdd(_objOut2.Token2, Convert.ToInt32(textBox_Ratio.Text));





                    _objOut2.tok2_inst = INSTcomboBox4.Text;
                    _objOut2.tok1_inst = INSTcomboBox3.Text;
                    _objOut2.tok3_inst = INSTcomboBox5.Text;
                    _objOut2.Calc_type = Calc_typecomboBox1.Text;

                    _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
                    _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
                    _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
                    _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
                    _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
                    _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
                    _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
                    _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
                    _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;

                    _objOut2.Symbol3 = SYMcomboBox6.Text == "" ? "0" : SYMcomboBox6.Text;
                    _objOut2.Expiry3 = EXPcomboBox7.Text == "" ? "0" : EXPcomboBox7.Text;
                    _objOut2.OptionType3 = OPcomboBox8.Text == "" ? "0" : OPcomboBox8.Text;
                    _objOut2.StrikePrice3 = STRIKecomboBox9.Text == "" ? "0" : STRIKecomboBox9.Text;

                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
                    Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));
                    Global.Instance.Ratio.TryAdd(_objOut2.Token3, Convert.ToInt32(textBox_Rati3.Text));  
                }
                else
                {

                    _objOut2.PFName = txtpfName.Text;
                    _objOut2.Token2 = Convert.ToInt32(Token2sp);
                    _objOut2.Desc1 = NEARSP;
                    Global.Instance.Ratio.TryAdd(_objOut2.Token2, Convert.ToInt32(textBox_Ratio.Text));
                    _objOut2.Token1 = Convert.ToInt32(Token1sp);

                    _objOut2.OppositeToken2 = Convert.ToInt32(OppsitToken2);
                    _objOut2.OppositeToken1 = Convert.ToInt32(OppsitToken1); 

                    _objOut2.Desc2 = FARSP;
                    _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
                    _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
                    _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
                    _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
                    _objOut2.tok2_inst = INSTcomboBox4.Text;
                    _objOut2.tok1_inst = INSTcomboBox3.Text;
                    _objOut2.Calc_type = Calc_typecomboBox1.Text;


                    _objOut2.Order_Type = combox_OrderType.Text;
                    _objOut2.first_LEG = comb_1stleg.Text;
                    _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
                    _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
                    _objOut2.second_Leg = com2nd_leg.Text;
                    _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
                    _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;

                    _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
                    _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
                    _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
                    _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
                    _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
                    _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
                    _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
                    _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
                    _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;

                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
                    
     
                    
                    Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));

                }



            }
            else
            {
                switch (Strategy_type_comboBox1.Text)
                {
                    case "2_LEG":

                        if (INSTcomboBox3.Text == "OPTIDX")
                        {
                            foreach (var d in STRIKecomboBox7.Items)
                            {
                                if (Convert.ToString(STRIKecomboBox7.Text) == Convert.ToString(d))
                                {

                                    Flg = false;
                                    goto done;
                                }
                                else
                                {
                                    //  STRIKecomboBox7.Text = "";
                                    label19.Visible = true;
                                    label19.Text = "Strike price not valid";
                                    Flg = true;
                                }
                            }
                        done:
                            if (Flg != false)
                            {
                                STRIKecomboBox7.Text = "";
                                return;
                            }

                            //==================================================================
                            foreach (var dd in STRIKecomboBox8.Items)
                            {
                                if (Convert.ToString(STRIKecomboBox8.Text) == Convert.ToString(dd))
                                {

                                    Flg = false;
                                    goto done1;
                                }
                                else
                                {
                                    //  STRIKecomboBox7.Text = "";
                                    label19.Visible = true;
                                    label19.Text = "Strike price not valid";
                                    Flg = true;
                                }
                            }
                        done1:
                            if (Flg != false)
                            {
                                STRIKecomboBox8.Text = "";
                                return;
                            }
                            //=========================================================================

                        }
                       
                            leg2();
                            select();

                       
 
                        break;

                    case "3_LEG":


                        if (INSTcomboBox3.Text == "OPTIDX")
                        {
                            foreach (var d in STRIKecomboBox7.Items)
                            {
                                if (Convert.ToString(STRIKecomboBox7.Text) == Convert.ToString(d))
                                {

                                    Flg = false;
                                    goto done4;
                                }
                                else
                                {
                                    //  STRIKecomboBox7.Text = "";
                                    label19.Visible = true;
                                    label19.Text = "Strike price not valid";
                                    Flg = true;
                                }
                            }
                        done4:
                            if (Flg != false)
                            {
                                STRIKecomboBox7.Text = "";
                                return;
                            }

                            //==================================================================
                            foreach (var dd in STRIKecomboBox8.Items)
                            {
                                if (Convert.ToString(STRIKecomboBox8.Text) == Convert.ToString(dd))
                                {

                                    Flg = false;
                                    goto done5;
                                }
                                else
                                {
                                    //  STRIKecomboBox7.Text = "";
                                    label19.Visible = true;
                                    label19.Text = "Strike price not valid";
                                    Flg = true;
                                }
                            }
                        done5:
                            if (Flg != false)
                            {
                                STRIKecomboBox8.Text = "";
                                return;
                            }
                            //=========================================================================
                            //==================================================================
                            foreach (var ddd in STRIKecomboBox9.Items)
                            {
                                if (Convert.ToString(STRIKecomboBox9.Text) == Convert.ToString(ddd))
                                {

                                    Flg = false;
                                    goto done6;
                                }
                                else
                                {
                                    //  STRIKecomboBox7.Text = "";
                                    label19.Visible = true;
                                    label19.Text = "Strike price not valid";
                                    Flg = true;
                                }
                            }
                        done6:
                            if (Flg != false)
                            {
                                STRIKecomboBox9.Text = "";
                                return;
                            }
                            //=========================================================================
                            leg3();
                            select();
                        }
                        else
                        {

                            leg3();
                            select();
                        
                        }
                       
                        break;
                    case "4_LEG":
                        leg4();
                        break;
                }

            }

            this.DialogResult = DialogResult.OK;

            //  }
        }

        private void AddToken_Load(object sender, EventArgs e)
        {

            Exchange();
            InsertType_fun();

            if (button1.Text == "Modify")
            {



            }
            else
            {

                show1();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OPcomboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            strike_prise();
        }

        private void EXcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ORcomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void INSTcomboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstType2();
        }

        private void SYMcomboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Expiry2();
        }

        private void EXPcomboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = EXPcomboBox6.SelectedIndex;
            date2 = T[i];

            optionType2();
        }

        private void OPcomboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            strike_prise2();
        }

        private void SYMcomboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Expiry3();
        }

        private void EXPcomboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = EXPcomboBox7.SelectedIndex;
            date3 = T[i];

            optionType3();
        }

        private void INSTcomboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstType3();
            
        }

        private void OPcomboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            strike_prise3();
        }

        private void INSTcomboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstType4();
        }

        private void SYMcomboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Expiry4();
        }

        private void EXPcomboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = EXPcomboBox8.SelectedIndex;
            date4 = T[i];

            optionType4();
        }

        private void OPcomboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            strike_prise4();
        }

        private void Strategy_type_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            show();
        }

        private void EXcomboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ORcomboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void STRIKecomboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox4.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox1.Enabled = true;
            }
        }

        private void textBox_Ratio_TextChanged(object sender, EventArgs e)
        {

        }

        private void STRIKecomboBox7_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmd_buy_sell1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void validation()
        {

            if (Strategy_type_comboBox1.Text == "") { MessageBox.Show("PLZ Select Strategy Type"); return; }
        else    if (Calc_typecomboBox1.Text == "") { MessageBox.Show("PLZ Select Strategy Type"); return; }
            else if (EXcomboBox1.Text == "") { MessageBox.Show("PLZ Select Exchange"); return; }
            else if (EXcomboBox1.Text == "") { MessageBox.Show("PLZ Select Exchange"); return; }

            else if (ORcomboBox2.Text == "") { MessageBox.Show("PLZ Select Order Type"); return; }
            else if (INSTcomboBox3.Text == "") { MessageBox.Show("PLZ Select Inst type"); return; }
            else if (SYMcomboBox4.Text == "") { MessageBox.Show("PLZ Select Symbol "); return; }
            else if (EXPcomboBox5.Text == "") { MessageBox.Show("PLZ Select Expiry "); return; }

            else if (textBox_Ratio.Text == "") { MessageBox.Show("PLZ Fill Ratio "); return; }
         
         }

        private void cmd_buy_sell1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.B || e.KeyCode == Keys.S || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (cmd_buy_sell1.Text.Trim() == "Buy")
                {
                    cmd_buy_sell1.Text = "Sell";
                    cmd_buy_sell1.ForeColor = Color.Red;
                }
                else
                {
                    cmd_buy_sell1.Text = "Buy";
                    cmd_buy_sell1.ForeColor = Color.Blue;
                }
            }
        }

        private void cmd_buy_sell1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmd_buy_sell2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.B || e.KeyCode == Keys.S || e.KeyCode == Keys.Enter)
            {
                if (cmd_buy_sell2.Text.Trim() == "Buy")
                {
                    cmd_buy_sell2.Text = "Sell";
                    cmd_buy_sell2.ForeColor = Color.Red;
                }
                else
                {
                    cmd_buy_sell2.Text = "Buy";
                    cmd_buy_sell2.ForeColor = Color.Blue;
                }
            }

        }

        private void cmd_buy_sell2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmd_buy_sell3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.B || e.KeyCode == Keys.S || e.KeyCode == Keys.Enter)
            {
                if (cmd_buy_sell3.Text.Trim() == "Buy")
                {
                    cmd_buy_sell3.Text = "Sell";
                    cmd_buy_sell3.ForeColor = Color.Red;
                }
                else
                {
                    cmd_buy_sell3.Text = "Buy";
                    cmd_buy_sell3.ForeColor = Color.Blue;
                }
            }
        }

        private void cmd_buy_sell4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.B || e.KeyCode == Keys.S || e.KeyCode == Keys.Enter)
            {
                if (cmd_buy_sell4.Text.Trim() == "Buy")
                {
                    cmd_buy_sell4.Text = "Sell";
                    cmd_buy_sell4.ForeColor = Color.Red;
                }
                else
                {
                    cmd_buy_sell4.Text = "Buy";
                    cmd_buy_sell4.ForeColor = Color.Blue;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bool Flg = false;
            if (button1.Text == "Modify")
            {


                if (Strategy_type_comboBox1.Text == "3_LEG")
                {
                    _objOut2.Symbol3 = SYMcomboBox6.Text == "" ? "0" : SYMcomboBox6.Text;
                    _objOut2.Expiry3 = EXPcomboBox7.Text == "" ? "0" : EXPcomboBox7.Text;
                    _objOut2.OptionType3 = OPcomboBox8.Text == "" ? "0" : OPcomboBox8.Text;
                    _objOut2.StrikePrice3 = STRIKecomboBox9.Text == "" ? "0" : STRIKecomboBox9.Text;



                    _objOut2.PFName = txtpfName.Text;
                    _objOut2.Token1 = Convert.ToInt32(Token1sp);
                    _objOut2.Desc1 = NEARSP;
                    _objOut2.Token2 = Convert.ToInt32(Token2sp);
                    _objOut2.Desc2 = FARSP;
                    _objOut2.Token3 = Convert.ToInt32(Token3sp);
                    _objOut2.Desc3 = tok3sp;
                    _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
                    _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
                    _objOut2.ratio3 = Convert.ToInt32(textBox_Rati3.Text);
                    _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
                    _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
                    _objOut2.buy_sell3 = Convert.ToString(cmd_buy_sell3.Text);

                    _objOut2.OppositeToken2 = Convert.ToInt32(OppsitToken2);
                    _objOut2.OppositeToken1 = Convert.ToInt32(OppsitToken1);
                    _objOut2.OppositeToken3 = Convert.ToInt32(OppsitToken3); 

                    _objOut2.Order_Type = combox_OrderType.Text;
                    _objOut2.first_LEG = comb_1stleg.Text;
                    _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
                    _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
                    _objOut2.second_Leg = com2nd_leg.Text;
                    _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
                    _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;
                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;


                    Global.Instance.Ratio.TryAdd(_objOut2.Token2, Convert.ToInt32(textBox_Ratio.Text));





                    _objOut2.tok2_inst = INSTcomboBox4.Text;
                    _objOut2.tok1_inst = INSTcomboBox3.Text;
                    _objOut2.tok3_inst = INSTcomboBox5.Text;
                    _objOut2.Calc_type = Calc_typecomboBox1.Text;

                    _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
                    _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
                    _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
                    _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
                    _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
                    _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
                    _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
                    _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
                    _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;

                    _objOut2.Symbol3 = SYMcomboBox6.Text == "" ? "0" : SYMcomboBox6.Text;
                    _objOut2.Expiry3 = EXPcomboBox7.Text == "" ? "0" : EXPcomboBox7.Text;
                    _objOut2.OptionType3 = OPcomboBox8.Text == "" ? "0" : OPcomboBox8.Text;
                    _objOut2.StrikePrice3 = STRIKecomboBox9.Text == "" ? "0" : STRIKecomboBox9.Text;

                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
                    Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));
                    Global.Instance.Ratio.TryAdd(_objOut2.Token3, Convert.ToInt32(textBox_Rati3.Text));
                }
                else
                {

                    _objOut2.PFName = txtpfName.Text;
                    _objOut2.Token2 = Convert.ToInt32(Token2sp);
                    _objOut2.Desc1 = NEARSP;
                    Global.Instance.Ratio.TryAdd(_objOut2.Token2, Convert.ToInt32(textBox_Ratio.Text));
                    _objOut2.Token1 = Convert.ToInt32(Token1sp);
                    _objOut2.Desc2 = FARSP;
                    _objOut2.ratio2 = Convert.ToInt32(textBox_Ratio2.Text);
                    _objOut2.ratio1 = Convert.ToInt32(textBox_Ratio.Text);
                    _objOut2.buy_sell2 = Convert.ToString(cmd_buy_sell2.Text);
                    _objOut2.buy_sell = Convert.ToString(cmd_buy_sell1.Text);
                    _objOut2.tok2_inst = INSTcomboBox4.Text;
                    _objOut2.tok1_inst = INSTcomboBox3.Text;
                    _objOut2.Calc_type = Calc_typecomboBox1.Text;

                    _objOut2.OppositeToken2 = Convert.ToInt32(OppsitToken2);
                    _objOut2.OppositeToken1 = Convert.ToInt32(OppsitToken1);

                    _objOut2.Order_Type = combox_OrderType.Text;
                    _objOut2.first_LEG = comb_1stleg.Text;
                    _objOut2.OPT_TICK = combox_optTrick.Text == "" ? "0" : combox_optTrick.Text;
                    _objOut2.ORDER_DEPTH = comb_OrderDepth.Text == "" ? "0" : comb_OrderDepth.Text;
                    _objOut2.second_Leg = com2nd_leg.Text;
                    _objOut2.THRESHOLD = textBox_Threshold.Text == "" ? "0" : (textBox_Threshold.Text + "%");
                    _objOut2.BIDDING_RANGE = comb_BidRange.Text == "" ? "0" : comb_BidRange.Text;

                    _objOut2.Strat_type = Strategy_type_comboBox1.Text == "" ? "0" : Strategy_type_comboBox1.Text;
                    _objOut2.Symbol1 = SYMcomboBox4.Text == "" ? "0" : SYMcomboBox4.Text;
                    _objOut2.Symbol2 = SYMcomboBox5.Text == "" ? "0" : SYMcomboBox5.Text;
                    _objOut2.Expiry = EXPcomboBox5.Text == "" ? "0" : EXPcomboBox5.Text;
                    _objOut2.Expiry2 = EXPcomboBox6.Text == "" ? "0" : EXPcomboBox6.Text;
                    _objOut2.OptionType = OPcomboBox6.Text == "" ? "0" : OPcomboBox6.Text;
                    _objOut2.OptionType2 = OPcomboBox7.Text == "" ? "0" : OPcomboBox7.Text;
                    _objOut2.StrikePrice = STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text;
                    _objOut2.StrikePrice2 = STRIKecomboBox8.Text == "" ? "0" : STRIKecomboBox8.Text;

                    _objOut2.ReqConnt = txtReqConnt.Text == "" ? "0" : txtReqConnt.Text;
                    Global.Instance.Ratio.TryAdd(_objOut2.Token1, Convert.ToInt32(textBox_Ratio2.Text));
                    
                }

                this.DialogResult = DialogResult.OK;

            }
            else
            {
                this.Close();
            }
            
        }


        void select()
        {


            XmlDocument MyXmlDocument = new XmlDocument();

            if (!Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles"))
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles");
            }

            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml"))
            {

                XmlDocument doc = new XmlDocument();
                XmlNode rootNode = doc.CreateElement("spreadtrackerDetails");
                doc.AppendChild(rootNode);
                doc.Save(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml");


            }
            try
            {
                if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml"))
                {
                    MyXmlDocument.Load(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml");
                    XmlNode node = MyXmlDocument.SelectSingleNode("spreadtrackerDetails/spreadtracker[ID =1]");
                    if (node != null)
                        node.ParentNode.RemoveChild(node);
                }
            }
            catch { }
            if (Strategy_type_comboBox1.Text == "2_LEG")
            {

                EXcomboBox3.Items.Clear();
                EXcomboBox3.Text = "";
                ORcomboBox4.Items.Clear();
                ORcomboBox4.Text = "";
                INSTcomboBox5.Items.Clear();
                INSTcomboBox5.Text = "";
                SYMcomboBox6.Items.Clear();
                SYMcomboBox6.Text = "";
                EXPcomboBox7.Items.Clear();
                EXPcomboBox7.Text = "";

                L_O_S3.Text = "";
                OPcomboBox8.Items.Clear();
                OPcomboBox8.Text = "";
                STRIKecomboBox9.Items.Clear();
                STRIKecomboBox9.Text = "";

                textBox_Rati3.Text = "";

                cmd_buy_sell3.Items.Clear();
                cmd_buy_sell3.Text = "";





                //  MyXmlDocument.Load(Application.StartupPath + "\\Profiles\\Lostadtoken.xml");

                XmlElement ParentElement = MyXmlDocument.CreateElement("spreadtracker");
                XmlElement ID = MyXmlDocument.CreateElement("ID");
                ID.InnerText = "1";
                XmlElement Order_Type = MyXmlDocument.CreateElement("Order_Type");
                Order_Type.InnerText = combox_OrderType.Text;


                XmlElement Strategy_Type = MyXmlDocument.CreateElement("Strategy_Type");
                Strategy_Type.InnerText = Strategy_type_comboBox1.Text;
                XmlElement Calc_Type = MyXmlDocument.CreateElement("Calc_Type");
                Calc_Type.InnerText = Calc_typecomboBox1.Text;

                XmlElement Exchange1 = MyXmlDocument.CreateElement("Exchange1");
                Exchange1.InnerText = EXcomboBox1.Text;
                XmlElement Order_Type1 = MyXmlDocument.CreateElement("Order_Type1");
                Order_Type1.InnerText = ORcomboBox2.Text;
                XmlElement Inst_Type1 = MyXmlDocument.CreateElement("Inst_Type1");
                Inst_Type1.InnerText = INSTcomboBox3.Text;

                XmlElement Symbol1 = MyXmlDocument.CreateElement("Symbol1");
                Symbol1.InnerText = SYMcomboBox4.Text;
                XmlElement Expiry1 = MyXmlDocument.CreateElement("Expiry1");
                Expiry1.InnerText = EXPcomboBox5.Text;
                XmlElement Lot_Size1 = MyXmlDocument.CreateElement("Lot_Size1");
                Lot_Size1.InnerText = L_O_S.Text;
                XmlElement Option_Type1 = MyXmlDocument.CreateElement("Option_Type1");
                Option_Type1.InnerText = OPcomboBox6.Text;
                XmlElement Strike_Price1 = MyXmlDocument.CreateElement("Strike_Price1");
                Strike_Price1.InnerText = STRIKecomboBox7.Text;
                XmlElement Ratio1 = MyXmlDocument.CreateElement("Ratio1");
                Ratio1.InnerText = textBox_Ratio.Text;
                XmlElement Buy_Sell = MyXmlDocument.CreateElement("Buy_Sell");
                Buy_Sell.InnerText = cmd_buy_sell1.Text;
                XmlElement Exchange2 = MyXmlDocument.CreateElement("Exchange2");
                Exchange2.InnerText = EXcomboBox2.Text;
                XmlElement Order_Type2 = MyXmlDocument.CreateElement("Order_Type2");
                Order_Type2.InnerText = ORcomboBox3.Text;
                XmlElement Inst_Type2 = MyXmlDocument.CreateElement("Inst_Type2");
                Inst_Type2.InnerText = INSTcomboBox4.Text;

                XmlElement Symbol2 = MyXmlDocument.CreateElement("Symbol2");
                Symbol2.InnerText = SYMcomboBox5.Text;
                XmlElement Expiry2 = MyXmlDocument.CreateElement("Expiry2");
                Expiry2.InnerText = EXPcomboBox6.Text;
                XmlElement Lot_Size2 = MyXmlDocument.CreateElement("Lot_Size2");
                Lot_Size2.InnerText = L_O_S2.Text;
                XmlElement Option_Type2 = MyXmlDocument.CreateElement("Option_Type2");
                Option_Type2.InnerText = OPcomboBox7.Text;
                XmlElement Strike_Price2 = MyXmlDocument.CreateElement("Strike_Price2");
                Strike_Price2.InnerText = STRIKecomboBox8.Text;
                XmlElement Ratio2 = MyXmlDocument.CreateElement("Ratio2");
                Ratio2.InnerText = textBox_Ratio2.Text;
                XmlElement Buy_Sel2 = MyXmlDocument.CreateElement("Buy_Sel2");
                Buy_Sel2.InnerText = cmd_buy_sell2.Text;


                ParentElement.AppendChild(ID);
                ParentElement.AppendChild(Order_Type);
                ParentElement.AppendChild(Strategy_Type);
                ParentElement.AppendChild(Calc_Type);
                ParentElement.AppendChild(Exchange1);
                ParentElement.AppendChild(Order_Type1);
                ParentElement.AppendChild(Inst_Type1);
                ParentElement.AppendChild(Symbol1);
                ParentElement.AppendChild(Expiry1);
                ParentElement.AppendChild(Lot_Size1);
                ParentElement.AppendChild(Option_Type1);
                ParentElement.AppendChild(Strike_Price1);
                ParentElement.AppendChild(Ratio1);
                ParentElement.AppendChild(Buy_Sell);
                ParentElement.AppendChild(Exchange2);
                ParentElement.AppendChild(Order_Type2);
                ParentElement.AppendChild(Inst_Type2);
                ParentElement.AppendChild(Symbol2);
                ParentElement.AppendChild(Expiry2);
                ParentElement.AppendChild(Lot_Size2);
                ParentElement.AppendChild(Option_Type2);
                ParentElement.AppendChild(Strike_Price2);
                ParentElement.AppendChild(Ratio2);
                ParentElement.AppendChild(Buy_Sel2);

                MyXmlDocument.DocumentElement.AppendChild(ParentElement);
                MyXmlDocument.Save(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml");
            }
            else
            {
                XmlElement ParentElement = MyXmlDocument.CreateElement("spreadtracker");
                XmlElement ID = MyXmlDocument.CreateElement("ID");
                ID.InnerText = "1";
                XmlElement Order_Type = MyXmlDocument.CreateElement("Order_Type");
                Order_Type.InnerText = combox_OrderType.Text;
                XmlElement Strategy_Type = MyXmlDocument.CreateElement("Strategy_Type");
                Strategy_Type.InnerText = Strategy_type_comboBox1.Text;
                XmlElement Calc_Type = MyXmlDocument.CreateElement("Calc_Type");
                Calc_Type.InnerText = Calc_typecomboBox1.Text;

                XmlElement Exchange1 = MyXmlDocument.CreateElement("Exchange1");
                Exchange1.InnerText = EXcomboBox1.Text;
                XmlElement Order_Type1 = MyXmlDocument.CreateElement("Order_Type1");
                Order_Type1.InnerText = ORcomboBox2.Text;
                XmlElement Inst_Type1 = MyXmlDocument.CreateElement("Inst_Type1");
                Inst_Type1.InnerText = INSTcomboBox3.Text;

                XmlElement Symbol1 = MyXmlDocument.CreateElement("Symbol1");
                Symbol1.InnerText = SYMcomboBox4.Text;
                XmlElement Expiry1 = MyXmlDocument.CreateElement("Expiry1");
                Expiry1.InnerText = EXPcomboBox5.Text;
                XmlElement Lot_Size1 = MyXmlDocument.CreateElement("Lot_Size1");
                Lot_Size1.InnerText = L_O_S.Text;
                XmlElement Option_Type1 = MyXmlDocument.CreateElement("Option_Type1");
                Option_Type1.InnerText = OPcomboBox6.Text;
                XmlElement Strike_Price1 = MyXmlDocument.CreateElement("Strike_Price1");
                Strike_Price1.InnerText = STRIKecomboBox7.Text;
                XmlElement Ratio1 = MyXmlDocument.CreateElement("Ratio1");
                Ratio1.InnerText = textBox_Ratio.Text;
                XmlElement Buy_Sell = MyXmlDocument.CreateElement("Buy_Sell");
                Buy_Sell.InnerText = cmd_buy_sell1.Text;
                XmlElement Exchange2 = MyXmlDocument.CreateElement("Exchange2");
                Exchange2.InnerText = EXcomboBox2.Text;
                XmlElement Order_Type2 = MyXmlDocument.CreateElement("Order_Type2");
                Order_Type2.InnerText = ORcomboBox3.Text;
                XmlElement Inst_Type2 = MyXmlDocument.CreateElement("Inst_Type2");
                Inst_Type2.InnerText = INSTcomboBox4.Text;

                XmlElement Symbol2 = MyXmlDocument.CreateElement("Symbol2");
                Symbol2.InnerText = SYMcomboBox5.Text;
                XmlElement Expiry2 = MyXmlDocument.CreateElement("Expiry2");
                Expiry2.InnerText = EXPcomboBox6.Text;
                XmlElement Lot_Size2 = MyXmlDocument.CreateElement("Lot_Size2");
                Lot_Size2.InnerText = L_O_S2.Text;
                XmlElement Option_Type2 = MyXmlDocument.CreateElement("Option_Type2");
                Option_Type2.InnerText = OPcomboBox7.Text;
                XmlElement Strike_Price2 = MyXmlDocument.CreateElement("Strike_Price2");
                Strike_Price2.InnerText = STRIKecomboBox8.Text;
                XmlElement Ratio2 = MyXmlDocument.CreateElement("Ratio2");
                Ratio2.InnerText = textBox_Ratio2.Text;
                XmlElement Buy_Sel2 = MyXmlDocument.CreateElement("Buy_Sel2");
                Buy_Sel2.InnerText = cmd_buy_sell2.Text;

                //=====================================
                XmlElement Exchange3 = MyXmlDocument.CreateElement("Exchange3");
                Exchange3.InnerText = EXcomboBox3.Text;
                XmlElement Order_Type3 = MyXmlDocument.CreateElement("Order_Type3");
                Order_Type3.InnerText = ORcomboBox4.Text;
                XmlElement Inst_Type3 = MyXmlDocument.CreateElement("Inst_Type3");
                Inst_Type3.InnerText = INSTcomboBox5.Text;

                XmlElement Symbol3 = MyXmlDocument.CreateElement("Symbol3");
                Symbol3.InnerText = SYMcomboBox6.Text;
                XmlElement Expiry3 = MyXmlDocument.CreateElement("Expiry3");
                Expiry3.InnerText = EXPcomboBox7.Text;
                XmlElement Lot_Size3 = MyXmlDocument.CreateElement("Lot_Size3");
                Lot_Size3.InnerText = L_O_S3.Text;
                XmlElement Option_Type3 = MyXmlDocument.CreateElement("Option_Type3");
                Option_Type3.InnerText = OPcomboBox8.Text;
                XmlElement Strike_Price3 = MyXmlDocument.CreateElement("Strike_Price3");
                Strike_Price3.InnerText = STRIKecomboBox9.Text;
                XmlElement Ratio3 = MyXmlDocument.CreateElement("Ratio3");
                Ratio3.InnerText = textBox_Rati3.Text;
                XmlElement Buy_Sel3 = MyXmlDocument.CreateElement("Buy_Sel3");
                Buy_Sel3.InnerText = cmd_buy_sell3.Text;
                //======================================================
                ParentElement.AppendChild(ID);
                ParentElement.AppendChild(Order_Type);
                ParentElement.AppendChild(Strategy_Type);
                ParentElement.AppendChild(Calc_Type);
                ParentElement.AppendChild(Exchange1);
                ParentElement.AppendChild(Order_Type1);
                ParentElement.AppendChild(Inst_Type1);
                ParentElement.AppendChild(Symbol1);
                ParentElement.AppendChild(Expiry1);
                ParentElement.AppendChild(Lot_Size1);
                ParentElement.AppendChild(Option_Type1);
                ParentElement.AppendChild(Strike_Price1);
                ParentElement.AppendChild(Ratio1);
                ParentElement.AppendChild(Buy_Sell);

                ParentElement.AppendChild(Exchange2);
                ParentElement.AppendChild(Order_Type2);
                ParentElement.AppendChild(Inst_Type2);
                ParentElement.AppendChild(Symbol2);
                ParentElement.AppendChild(Expiry2);
                ParentElement.AppendChild(Lot_Size2);
                ParentElement.AppendChild(Option_Type2);
                ParentElement.AppendChild(Strike_Price2);
                ParentElement.AppendChild(Ratio2);
                ParentElement.AppendChild(Buy_Sel2);

                ParentElement.AppendChild(Exchange3);
                ParentElement.AppendChild(Order_Type3);
                ParentElement.AppendChild(Inst_Type3);
                ParentElement.AppendChild(Symbol3);
                ParentElement.AppendChild(Expiry3);
                ParentElement.AppendChild(Lot_Size3);
                ParentElement.AppendChild(Option_Type3);
                ParentElement.AppendChild(Strike_Price3);
                ParentElement.AppendChild(Ratio3);
                ParentElement.AppendChild(Buy_Sel3);

                MyXmlDocument.DocumentElement.AppendChild(ParentElement);
                MyXmlDocument.Save(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml");



            }

        }

        void show1()
        {
            object ob = new object();

            if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml"))
            {
                DataSet dst = new DataSet();
                dst.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "SpreadProfiles" + Path.DirectorySeparatorChar + "Lostadtoken.xml");
                Strategy_type_comboBox1.Text = dst.Tables[0].Rows[0]["Strategy_type"].ToString();

                if (dst.Tables[0].Rows[0]["Strategy_type"].ToString() == "2_LEG")
                {
                    combox_OrderType.Text = dst.Tables[0].Rows[0]["Order_Type"].ToString();
                    Calc_typecomboBox1.Text = dst.Tables[0].Rows[0]["Calc_Type"].ToString();
                    EXcomboBox1.Text = dst.Tables[0].Rows[0]["Exchange1"].ToString();
                    ORcomboBox2.Text = dst.Tables[0].Rows[0]["Order_Type1"].ToString();
                    INSTcomboBox3.Text = dst.Tables[0].Rows[0]["Inst_Type1"].ToString();
                    SYMcomboBox4.Text = dst.Tables[0].Rows[0]["Symbol1"].ToString();
                    EXPcomboBox5.Text = dst.Tables[0].Rows[0]["Expiry1"].ToString();
                    L_O_S.Text = dst.Tables[0].Rows[0]["Lot_Size1"].ToString();
                    OPcomboBox6.Text = dst.Tables[0].Rows[0]["Option_Type1"].ToString();
                    // STRIKecomboBox7.Text = (node.SelectSingleNode("Strike_Price1").InnerText);
                    STRIKecomboBox7.Text = dst.Tables[0].Rows[0]["Strike_Price1"].ToString();
                    textBox_Ratio.Text = dst.Tables[0].Rows[0]["Ratio1"].ToString();
                    cmd_buy_sell1.Text = dst.Tables[0].Rows[0]["Buy_Sell"].ToString();

                    EXcomboBox2.Text = dst.Tables[0].Rows[0]["Exchange2"].ToString();
                    ORcomboBox3.Text = dst.Tables[0].Rows[0]["Order_Type2"].ToString();
                    INSTcomboBox4.Text = dst.Tables[0].Rows[0]["Inst_Type2"].ToString();
                    SYMcomboBox5.Text = dst.Tables[0].Rows[0]["Symbol2"].ToString();
                    EXPcomboBox6.Text = dst.Tables[0].Rows[0]["Expiry2"].ToString();
                    L_O_S2.Text = dst.Tables[0].Rows[0]["Lot_Size2"].ToString();
                    OPcomboBox7.Text = dst.Tables[0].Rows[0]["Option_Type2"].ToString();
                    //   STRIKecomboBox8.Text = (node.SelectSingleNode("Strike_Price2").InnerText);
                    STRIKecomboBox8.Text = dst.Tables[0].Rows[0]["Strike_Price2"].ToString();
                    textBox_Ratio2.Text = dst.Tables[0].Rows[0]["Ratio2"].ToString();
                    cmd_buy_sell2.Text = dst.Tables[0].Rows[0]["Buy_Sel2"].ToString();
                }
                else
                {
                    combox_OrderType.Text = dst.Tables[0].Rows[0]["Order_Type"].ToString();
                    Calc_typecomboBox1.Text = dst.Tables[0].Rows[0]["Calc_Type"].ToString();
                    EXcomboBox1.Text = dst.Tables[0].Rows[0]["Exchange1"].ToString();
                    ORcomboBox2.Text = dst.Tables[0].Rows[0]["Order_Type1"].ToString();
                    INSTcomboBox3.Text = dst.Tables[0].Rows[0]["Inst_Type1"].ToString();
                    SYMcomboBox4.Text = dst.Tables[0].Rows[0]["Symbol1"].ToString();
                    EXPcomboBox5.Text = dst.Tables[0].Rows[0]["Expiry1"].ToString();
                    L_O_S.Text = dst.Tables[0].Rows[0]["Lot_Size1"].ToString();
                    OPcomboBox6.Text = dst.Tables[0].Rows[0]["Option_Type1"].ToString();
                    // STRIKecomboBox7.Text = (node.SelectSingleNode("Strike_Price1").InnerText);
                    STRIKecomboBox7.Text = dst.Tables[0].Rows[0]["Strike_Price1"].ToString();
                    textBox_Ratio.Text = dst.Tables[0].Rows[0]["Ratio1"].ToString();
                    cmd_buy_sell1.Text = dst.Tables[0].Rows[0]["Buy_Sell"].ToString();

                    EXcomboBox2.Text = dst.Tables[0].Rows[0]["Exchange2"].ToString();
                    ORcomboBox3.Text = dst.Tables[0].Rows[0]["Order_Type2"].ToString();
                    INSTcomboBox4.Text = dst.Tables[0].Rows[0]["Inst_Type2"].ToString();
                    SYMcomboBox5.Text = dst.Tables[0].Rows[0]["Symbol2"].ToString();
                    EXPcomboBox6.Text = dst.Tables[0].Rows[0]["Expiry2"].ToString();
                    L_O_S2.Text = dst.Tables[0].Rows[0]["Lot_Size2"].ToString();
                    OPcomboBox7.Text = dst.Tables[0].Rows[0]["Option_Type2"].ToString();
                    //   STRIKecomboBox8.Text = (node.SelectSingleNode("Strike_Price2").InnerText);
                    STRIKecomboBox8.Text = dst.Tables[0].Rows[0]["Strike_Price2"].ToString();
                    textBox_Ratio2.Text = dst.Tables[0].Rows[0]["Ratio2"].ToString();
                    cmd_buy_sell2.Text = dst.Tables[0].Rows[0]["Buy_Sel2"].ToString();
                    EXcomboBox3.Text = dst.Tables[0].Rows[0]["Exchange3"].ToString();
                    ORcomboBox4.Text = dst.Tables[0].Rows[0]["Order_Type3"].ToString();
                    INSTcomboBox5.Text = dst.Tables[0].Rows[0]["Inst_Type3"].ToString();
                    SYMcomboBox6.Text = dst.Tables[0].Rows[0]["Symbol3"].ToString();
                    EXPcomboBox7.Text = dst.Tables[0].Rows[0]["Expiry3"].ToString();
                    L_O_S3.Text = dst.Tables[0].Rows[0]["Lot_Size3"].ToString();
                    OPcomboBox8.Text = dst.Tables[0].Rows[0]["Option_Type3"].ToString();
                    //   STRIKecomboBox8.Text = (node.SelectSingleNode("Strike_Price2").InnerText);
                    STRIKecomboBox9.Text = dst.Tables[0].Rows[0]["Strike_Price3"].ToString();
                    textBox_Rati3.Text = dst.Tables[0].Rows[0]["Ratio3"].ToString();
                    cmd_buy_sell3.Text = dst.Tables[0].Rows[0]["Buy_Sel3"].ToString();
                }

            }


        }

        private void EXcomboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ORcomboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combox_OrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bid_Show();
        }

        private void textBox_Ratio_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtReqConnt_TextChanged(object sender, EventArgs e)
        {
            if (!Information.IsNumeric(txtReqConnt.Text))
            {
                if (txtReqConnt.Text.Length > 1)
                {
                    MessageBox.Show("Please Insert Numeric integer", "Information");
                    //txtReqConnt.Clear();
                    return;

                }

            }
            if (Information.IsNumeric(txtReqConnt.Text))
            {
                if (Convert.ToDouble(txtReqConnt.Text == "" ? "0" : txtReqConnt.Text) > 1000)
                {
                    MessageBox.Show("Please Insert > 1000");
                    //txtReqConnt.Clear();

                }
            }
        }

        private void STRIKecomboBox7_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void STRIKecomboBox7_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            
        }

        private void STRIKecomboBox7_KeyDown(object sender, KeyEventArgs e)
        {
        //    foreach (var d in STRIKecomboBox7.Items)
        //    {
        //        //if (Convert.ToInt32(STRIKecomboBox7.Text == "" ? "0" : STRIKecomboBox7.Text) > Convert.ToInt32(d)) 
        //        //{
        //        //  //  STRIKecomboBox7.Text = "";
        //        //    label19.Visible = true;
        //        //    label19.Text = "Strike price not valid";
        //        //}

        //    if (d.ToString().Length != STRIKecomboBox7.Text.Length)
        //        {
        //            //  STRIKecomboBox7.Text = "";
        //            label19.Visible = true;
        //            label19.Text = "Strike price not valid";
        //        }

        //    else
        //    { label19.Visible = false; }


                
            
        //   }

        //    //if (STRIKecomboBox7.Text == STRIKecomboBox7.DataSource)
            

        }
      

      

    }

    
}
