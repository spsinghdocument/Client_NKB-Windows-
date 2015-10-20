using csv;
using Microsoft.VisualBasic;
using Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Client
{
    public partial class frmMWatch : Form
    {
      
        public string GetProfileName { get; set; }
        private readonly Dictionary<int, DataGridViewRow> _mwatchDict = new Dictionary<int, DataGridViewRow>();
        private DataGridViewCellStyle _makeItBlack;
        private DataGridViewCellStyle _makeItBlue;
        private DataGridViewCellStyle _makeItRed;
        DataTable dt_mktwatch = new DataTable("mktwatch");
        List<long> T = new List<long>();
        DataTable _table = new DataTable("frmwatch");
        long date = 0;
        int a = 0;

        private int PgSize = 20;
        private int CurrentPageIndex = 1;
        private int TotalPage = 0; 



        private static readonly frmMWatch Instance = new frmMWatch();
        public static frmMWatch instance
        {
            get
            {
                return Instance;
            }
        }



               public frmMWatch()
        {
            InitializeComponent();

           // this.WindowState = FormWindowState.Maximized;
            //=================================================================================================================================================
        
            
            //===================================================================================================================================================

           
            _makeItRed = new DataGridViewCellStyle();
            _makeItBlue = new DataGridViewCellStyle();
            _makeItBlack = new DataGridViewCellStyle();

            _makeItRed.BackColor = Color.IndianRed;

            _makeItBlue.BackColor = Color.Blue;
            _makeItBlack.BackColor = Color.Black;
            if (!Directory.Exists(Application.StartupPath +Path.DirectorySeparatorChar +"Mwatch"))
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "Mwatch");
            }
            if (!Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles"))
            {
                Directory.CreateDirectory(Application.StartupPath + "Profiles");
            }         
        }


        void InstType()
        {
            comboB_Symbol.Items.Clear();
            string[] symm = CSV_Class.cimlist.Where(a => a.InstrumentName == comboBInstType.Text).OrderBy(we => we.Symbol).OrderBy(q1 => q1.Symbol).Select(q => q.Symbol).Distinct().ToArray();
               
            comboB_Symbol.Items.AddRange(symm);


            combo_OptionType.Enabled = true;
            combo_StrikePrice.Enabled = true;
            combo_Exoiry.Items.Clear();
            combo_OptionType.Items.Clear();
            combo_StrikePrice.Items.Clear();


            if (comboBInstType.Text == "FUTIVX" || comboBInstType.Text == "FUTIDX" || comboBInstType.Text == "FUTSTK")
            {

                combo_OptionType.Enabled = false;
                combo_StrikePrice.Enabled = false;
                combo_Exoiry.Items.Clear();
                combo_OptionType.Items.Clear();
                combo_StrikePrice.Items.Clear();


            }
        }

        public static DateTime ConvertFromTimestamp(long timstamp)
        {
            DateTime datetime = new DateTime(1980, 1, 1, 0, 0, 0, 0);
            return datetime.AddSeconds(timstamp);
        }
        void Exoirry()
        {
            combo_Exoiry.Items.Clear();
            combo_OptionType.Items.Clear();
            combo_StrikePrice.Items.Clear();
            combo_Exoiry.Text = "";

            T = CSV_Class.cimlist.Where(a => a.Symbol == comboB_Symbol.Text && a.InstrumentName == comboBInstType.Text).OrderBy(s => s.ExpiryDate).Select(d => d.ExpiryDate).Distinct().ToList();
            // EXPcomboBox5.Items.AddRange(Enumerable.Range(1, T.Count()).Select(x => x.ToString()));
            foreach (long ex in T)
            {
                string on = ConvertFromTimestamp(ex).ToShortDateString();
                combo_Exoiry.Items.Add(on);
            }

            combo_OptionType.Text = "";
            combo_StrikePrice.Text = "";

        }
        ///////////////////////////////////////////////////////////////////////////
        void optionType()
        {
            combo_OptionType.Text = "";
            combo_OptionType.Items.Clear();
            combo_StrikePrice.Items.Clear();

            string[] op = CSV_Class.cimlist.Where(a => a.ExpiryDate == date && a.InstrumentName == comboBInstType.Text && a.Symbol == comboB_Symbol.Text).Select(s => s.OptionType).Distinct().ToArray();

            combo_OptionType.Items.AddRange(op);
            combo_StrikePrice.Text = "";

        }
        /// ////////////////////////////////////////////////////////////// //
        void strike_prise()
        {
            combo_StrikePrice.Items.Clear();
            var p = CSV_Class.cimlist.Where(a => a.ExpiryDate == date && a.InstrumentName == comboBInstType.Text && a.Symbol == comboB_Symbol.Text && a.OptionType == combo_OptionType.Text).OrderBy(w => w.StrikePrice).Select(a => a.StrikePrice).Distinct().ToArray();
            foreach (int x in p)
                combo_StrikePrice.Items.Add(x/100);

        }
        /////////////////////////////////////////////////////////////////////////  ///
        public void lavesho(string token1)
        {

            //label8.Text = token1;


        }
                
        private void btnprofile_Click(object sender, EventArgs e)
        {
            var frmprf = new frmProfile();

            foreach (DataGridViewColumn dc in DGV.Columns)
            {
                frmprf.lbxPrimary.Items.Add(dc.HeaderText);
                //if (!frmprf.lbxSecondary.Items.Contains(dc.HeaderText))
                //{
                //    frmprf.lbxPrimary.Items.Add(dc.HeaderText);
                //    this.DGV.Columns[dc.HeaderText].Visible = false;
                //}
                //else
                //{
                //    this.DGV.Columns[dc.HeaderText].Visible = true;
                //}
            }
            if (frmprf.ShowDialog() == DialogResult.OK)
            {
                GetProfileName = frmprf.GetProfileName();

                LoadDgcOlumns(Application.StartupPath +Path.DirectorySeparatorChar+ "Profiles" + Path.DirectorySeparatorChar + GetProfileName + ".xml");
            }
        }

        private void LoadDgcOlumns(String fileName)
        {
            var clmns = new ArrayList();

            if (File.Exists(fileName))
            {
                var settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;

                using (XmlReader reader = XmlReader.Create(fileName, settings))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && "Column" == reader.LocalName)
                        {
                            reader.MoveToFirstAttribute();
                            clmns.Add(reader.Value);
                        }
                    }
                }
            }
        }

        public static DataGridViewColumn GetGridColumn(string ColumnName, string DataType, Boolean KeepNull = true, Boolean KeepUnique = false)
        {
  


            var dataGridViewColumn = new DataGridViewColumn();
            DataGridViewCell dataGridViewCell = new DataGridViewTextBoxCell();
            
            dataGridViewCell.ValueType = Type.GetType("System." + DataType);
            dataGridViewColumn.DataPropertyName = ColumnName.Trim();
            dataGridViewColumn.HeaderText = ColumnName.Trim();
            dataGridViewColumn.CellTemplate = dataGridViewCell;
            dataGridViewColumn.Name = ColumnName.Trim();

            //dc.Caption  = "Abc" + ColumnName;
            return dataGridViewColumn;
        }


        private void ReadyDatatable()
        {
           
          
            _table.Columns.Add("InstrumentName(A)",typeof(string));
            _table.Columns.Add("DescriptionB", typeof(string));
            _table.Columns.Add("Symbol(C)", typeof(string));
            _table.Columns.Add("ExpiryDate(D)", typeof(string));
            _table.Columns.Add("OptionType(E)", typeof(string));
            _table.Columns.Add("StrikePrice(F)", typeof(string));
            _table.Columns.Add("Bid(G)", typeof(string));
            _table.Columns.Add("Ask(H)", typeof(string));
            _table.Columns.Add("LTP(I)", typeof(string));

            _table.Columns.Add("LTP_Vol(J)", typeof(string));
            _table.Columns.Add("Bid_Vol(K)", typeof(string));
            _table.Columns.Add("Ask_Vol(L)", typeof(string));
            _table.Columns.Add("Delta(M)", typeof(string));
            _table.Columns.Add("Gamma(N)", typeof(string));

            _table.Columns.Add("Vega(O)", typeof(string));
            _table.Columns.Add("Theta(P)", typeof(string));
            _table.Columns.Add("Rho(Q)", typeof(string));


            

            _table.Columns.Add("R", typeof(string));
            _table.Columns.Add("S", typeof(string));
            _table.Columns.Add("T", typeof(string));
            _table.Columns.Add("U", typeof(string));
            _table.Columns.Add("V", typeof(string));
            _table.Columns.Add("W", typeof(string));
            _table.Columns.Add("X", typeof(string));
            _table.Columns.Add("Y", typeof(string));
            _table.Columns.Add("Z", typeof(string));

            _table.Columns.Add("UniqueIdentifier_FUT", typeof(string));
            _table.Columns.Add("LTP_FUT", typeof(string));

            _table.Columns.Add("UniqueIdentifier", typeof(string));
           
            _table.Columns.Add("BidQ", typeof(string));
            _table.Columns.Add("TBidOrder", typeof(string));
            _table.Columns.Add("AskQ", typeof(string));
            _table.Columns.Add("TAskOrder", typeof(string));
            _table.Columns.Add("LTQ", typeof(string));
          
            _table.Columns.Add("Open", typeof(string));
            _table.Columns.Add("High", typeof(string));
            _table.Columns.Add("Low", typeof(string));
            _table.Columns.Add("Close", typeof(string));
            _table.Columns.Add("Volume", typeof(string));
            _table.Columns.Add("OI", typeof(string));
            _table.Columns.Add("ATP", typeof(string));
            _table.Columns.Add("TBQ", typeof(string));
            _table.Columns.Add("TSQ", typeof(string));
            _table.Columns.Add("TotalTrades", typeof(string));
            _table.Columns.Add("TotalQtyTraded", typeof(string));
            _table.Columns.Add("TotalTradedValue", typeof(string));
            _table.Columns.Add("HighestPriceEver", typeof(string));
            _table.Columns.Add("LowestPriceever", typeof(string));
           

            _table.Columns.Add("Exchange", typeof(string));
            _table.Columns.Add("Lotsize", typeof(string));

           

         

            DGV.DataSource = _table;
            DGV.Columns["UniqueIdentifier_FUT"].Visible = false;
            DGV.Columns["LTP_FUT"].Visible = false;
            DGV.Columns["BidQ"].Visible = false;
            DGV.Columns["TBidOrder"].Visible = false;
               DGV.Columns["UniqueIdentifier"].Visible = false;

            DGV.Columns["TBidOrder"].Visible = false;
            DGV.Columns["AskQ"].Visible = false;
            DGV.Columns["TAskOrder"].Visible = false;
            DGV.Columns["LTQ"].Visible = false;


            DGV.Columns["Open"].Visible = false;
            DGV.Columns["High"].Visible = false;
            DGV.Columns["Low"].Visible = false;
            DGV.Columns["Close"].Visible = false;
            DGV.Columns["Volume"].Visible = false;
            DGV.Columns["OI"].Visible = false;
            DGV.Columns["ATP"].Visible = false;
            DGV.Columns["TBQ"].Visible = false;
            DGV.Columns["TSQ"].Visible = false;
            DGV.Columns["TotalTrades"].Visible = false;
            DGV.Columns["TotalQtyTraded"].Visible = false;
            DGV.Columns["TotalTradedValue"].Visible = false;
            DGV.Columns["HighestPriceEver"].Visible = false;
            DGV.Columns["LowestPriceever"].Visible = false;
         

            DGV.Columns["Exchange"].Visible = false;
            DGV.Columns["Lotsize"].Visible = false;

           // this.DGV.Columns["Delta(M)"].DefaultCellStyle.Format = "#.000"; 


            #region cal

            for (int r = 0; r < DGV.Rows.Count - 2; r++)
            {
                var row = _table.Rows[r];
                for (int c = -2; c > _table.Columns.Count; c++)
                {
                    row[c] = string.Format("={0}*{1}", r + 1, c + 1);
                }
            }

            DGV.DataSource = _table;

            // update address and status bar when selection changes
            DGV.SelectionChanged += _grid_SelectionChanged;


            // update content in formula bar
            _txtFormula.Validating += _txtFormula_Validating;
            _txtFormula.KeyPress += _txtFormula_KeyPress;

            // show list of available functions
            _lblFunctions.MouseDown += _lblFunctions_MouseDown;
            #endregion

           
        }

        private void combo_Exchange_SelectedIndexChanged(object sender, EventArgs e)
        {
           // InsertType_fun();

         //   InstType();
        }

        private void comboBInstType_SelectedIndexChanged(object sender, EventArgs e)
        {
           InstType();
        }

        private void comboB_Symbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Exoirry();

         //   combo_Exoiry.SelectedIndex = 0;

        }

        private void combo_Exoiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = combo_Exoiry.SelectedIndex;
            date = T[i];
            optionType();
        }

        private void combo_OptionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strike_prise();
        }
        public static int[] LoadFormLocationAndSize(Form xForm)
        {
            int[] t={0,0,900,300};
            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "formmktclose.xml"))
                return t ;
            DataSet dset = new DataSet();
            dset.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "formmktclose.xml");
            int[] LocationAndSize = new int[] { xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height };

            try
            {
                var AbbA = dset.Tables[0].Rows[0]["Input"].ToString().Split(';');              
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
            var settings = new XmlWriterSettings { Indent = true };
            XmlWriter writer = XmlWriter.Create(Application.StartupPath + Path.DirectorySeparatorChar + "formmktclose.xml", settings);
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
       
        private void frmMktWatch_Load(object sender, EventArgs e)
        {
            
                var AbbA = LoadFormLocationAndSize(this);
                this.Location = new Point(AbbA[0], AbbA[1]);
                this.Size = new Size(AbbA[2], AbbA[3]);

                this.FormClosing += new FormClosingEventHandler(SaveFormLocationAndSize);
                //   this.WindowState = FormWindowState.Normal;

                Type controlType = DGV.GetType();
                PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(DGV, true, null);

                ReadyDatatable();
                Exchange();
                frmmktdefaultfun();
                DGV.AllowDrop = true;               


                foreach (DataGridViewColumn dgvcol in DGV.Columns)
                {
                    dgvcol.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                _makeItRed = new DataGridViewCellStyle();
                _makeItBlue = new DataGridViewCellStyle();
                _makeItBlack = new DataGridViewCellStyle();

                _makeItRed.BackColor = Color.Red;

                _makeItBlue.BackColor = Color.Blue;
                _makeItBlack.BackColor = Color.Black;
             
            //DataGridViewColumnSelector cl = new DataGridViewColumnSelector(DGV);
                //cl.MaxHeight = 200;
                //cl.Width = 100;
        }

        private void frmmktdefaultfun()
        {            
            //DGV.Rows.Clear();
            DataSet dst = new DataSet();
            if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Defaultfrmwatch.xml"))// System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "Defaultfrmwatch.xml"))
            {
                dst.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Defaultfrmwatch.xml"); // System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "Defaultfrmwatch.xml");
              
                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                    {

          

                      //  int RowIndex = DGV.Rows.Add();
                        DataRow dr = _table.NewRow();

                        dr["UniqueIdentifier"] = dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString();
                        dr["InstrumentName(A)"] = dst.Tables[0].Rows[i]["InstrumentName(A)"].ToString();
                        dr["DescriptionB"] = dst.Tables[0].Rows[i]["DescriptionB"].ToString();
                        dr["ExpiryDate(D)"] = dst.Tables[0].Rows[i]["ExpiryDate(D)"].ToString();
                        dr["OptionType(E)"] = dst.Tables[0].Rows[i]["OptionType(E)"].ToString();
                        dr["StrikePrice(F)"] = dst.Tables[0].Rows[i]["StrikePrice(F)"].ToString();
                        dr["Symbol(C)"] = dst.Tables[0].Rows[i]["Symbol(C)"].ToString();
                        dr["Bid(G)"] = dst.Tables[0].Rows[i]["Bid(G)"].ToString();
                        dr["BidQ"] = dst.Tables[0].Rows[i]["BidQ"].ToString();
                        dr["Ask(H)"] = dst.Tables[0].Rows[i]["Ask(H)"].ToString();
                        dr["AskQ"] = dst.Tables[0].Rows[i]["AskQ"].ToString();
                        dr["LTP(I)"] = dst.Tables[0].Rows[i]["LTP(I)"].ToString();
                        dr["LTP_Vol(J)"] = dst.Tables[0].Rows[i]["LTP_Vol(J)"].ToString();
                        dr["Bid_Vol(K)"] = dst.Tables[0].Rows[i]["Bid_Vol(K)"].ToString();
                        dr["Ask_Vol(L)"] = dst.Tables[0].Rows[i]["Ask_Vol(L)"].ToString();
                        dr["Delta(M)"] = dst.Tables[0].Rows[i]["Delta(M)"].ToString();
                        dr["Gamma(N)"] = dst.Tables[0].Rows[i]["Gamma(N)"].ToString();
                        dr["Vega(O)"] = dst.Tables[0].Rows[i]["Vega(O)"].ToString();
                        dr["Theta(P)"] = dst.Tables[0].Rows[i]["Theta(P)"].ToString();
                        dr["Rho(Q)"] = dst.Tables[0].Rows[i]["Rho(Q)"].ToString();
                      
                        dr["R"] = dst.Tables[0].Rows[i]["R"].ToString();
                        dr["S"] = dst.Tables[0].Rows[i]["S"].ToString();
                        dr["T"] = dst.Tables[0].Rows[i]["T"].ToString();
                        dr["U"] = dst.Tables[0].Rows[i]["U"].ToString();
                        dr["V"] = dst.Tables[0].Rows[i]["V"].ToString();
                        dr["W"] = dst.Tables[0].Rows[i]["W"].ToString();
                        dr["X"] = dst.Tables[0].Rows[i]["X"].ToString();
                        dr["Y"] = dst.Tables[0].Rows[i]["Y"].ToString();
                        dr["Z"] = dst.Tables[0].Rows[i]["Z"].ToString();

                        dr["UniqueIdentifier_FUT"] = dst.Tables[0].Rows[i]["UniqueIdentifier_FUT"].ToString();
                        //dr["InstrumentName_FUT"] = dst.Tables[0].Rows[i]["InstrumentName_FUT"].ToString();
                        //dr["DescriptionB_FUT"] = dst.Tables[0].Rows[i]["DescriptionB_FUT"].ToString();
                        //dr["OptionType_FUT"] = dst.Tables[0].Rows[i]["OptionType_FUT"].ToString();
                        //dr["StrikePrice_FUT"] = dst.Tables[0].Rows[i]["StrikePrice_FUT"].ToString();
                        dr["LTP_FUT"] = dst.Tables[0].Rows[i]["LTP_FUT"].ToString();
       //===================================================================================================================
                        dr["LTQ"] = dst.Tables[0].Rows[i]["LTQ"].ToString();
                     
                        dr["Open"] = dst.Tables[0].Rows[i]["Open"].ToString();
                        dr["High"] = dst.Tables[0].Rows[i]["High"].ToString();
                        dr["Low"] = dst.Tables[0].Rows[i]["Low"].ToString();
                        dr["Close"] = dst.Tables[0].Rows[i]["Close"].ToString();
                        dr["Volume"] = dst.Tables[0].Rows[i]["Volume"].ToString();
                        dr["OI"] = dst.Tables[0].Rows[i]["OI"].ToString();
                        dr["ATP"] = dst.Tables[0].Rows[i]["ATP"].ToString();
                        dr["TBQ"] = dst.Tables[0].Rows[i]["TBQ"].ToString();
                        dr["TSQ"] = dst.Tables[0].Rows[i]["TSQ"].ToString();
                        dr["TotalTrades"] = dst.Tables[0].Rows[i]["TotalTrades"].ToString();
                        dr["TotalQtyTraded"] = dst.Tables[0].Rows[i]["TotalQtyTraded"].ToString();
                        dr["TotalTradedValue"] = dst.Tables[0].Rows[i]["TotalTradedValue"].ToString();
                        dr["HighestPriceEver"] = dst.Tables[0].Rows[i]["HighestPriceEver"].ToString();
                        dr["LowestPriceever"] = dst.Tables[0].Rows[i]["LowestPriceever"].ToString();
                    

                        dr["Lotsize"] = dst.Tables[0].Rows[i]["Lotsize"].ToString();
                        dr["Exchange"] = dst.Tables[0].Rows[i]["Exchange"].ToString();
                      

                
                        _table.Rows.Add(dr);
                        UDP_Reciever.Instance.Subscribe = Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString());
                      
                        Holder._DictLotSize.TryAdd(Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString()), new Csv_Struct()
                        {
                            lotsize = CSV_Class.cimlist.Where(q => q.Token == Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString())).Select(a => a.BoardLotQuantity).First()
                        });
                       
                }
            }

        }
        private void SetData(DataGridViewCell DGCell, double ValueOne)
        {
            if (DGCell != null)
            {
                double ValueTwo = Convert.ToDouble(DGCell.Value);
                if (ValueOne > ValueTwo)
                {
                    DGCell.Style = _makeItBlue;
                }
                else if (ValueOne < ValueTwo)
                {
                    DGCell.Style = _makeItRed;
                }
                else if (ValueOne == ValueTwo)
                {
                    DGCell.Style = _makeItBlack;
                }
            }

            DGCell.Value = ValueOne;
        }

      
        private void AddSymbolFirstTime()
        {
            int token=0 ;
            int token1 = 0;
            if (comboB_Symbol.Text == "" && comboBInstType.Text == "")
            {
                MessageBox.Show("Selected Token not find ");
                return ;
            }
            if (comboBInstType.Text == "FUTIVX" || comboBInstType.Text == "FUTIDX" || comboBInstType.Text == "FUTSTK")
            {
                int t = CSV_Class.cimlist.FindIndex(q => q.Symbol == comboB_Symbol.Text && q.ExpiryDate == this.date && q.InstrumentName == comboBInstType.Text);

                token = CSV_Class.cimlist[t].Token;
                if (Holder._DictLotSize.ContainsKey(token) == false || token != 0)
                {
                    Holder._DictLotSize.TryAdd(token, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == token).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }
            }
            else
            {
                if (comboB_Symbol.Text == "" || combo_Exoiry.Text == "" || combo_OptionType.Text == "" || comboBInstType.Text == "" || combo_StrikePrice.Text == "")
                {
                    MessageBox.Show("Please Select All Fiels ...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }    
                int t = CSV_Class.cimlist.FindIndex(q => q.Symbol == comboB_Symbol.Text && q.ExpiryDate == this.date && q.InstrumentName == comboBInstType.Text && q.OptionType == combo_OptionType.Text && q.StrikePrice == Convert.ToInt32(combo_StrikePrice.Text)*100);
                int tt = CSV_Class.cimlist.FindIndex(q => q.Symbol == comboB_Symbol.Text && q.ExpiryDate == this.date && q.InstrumentName == "FUTIDX");



                token = CSV_Class.cimlist[t].Token;

                if (Holder._DictLotSize.ContainsKey(token) == false || token != 0)
                {
                    Holder._DictLotSize.TryAdd(token, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == token).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }
                //=============================================================================================================
                token1 = CSV_Class.cimlist[tt].Token;

                if (Holder._DictLotSize.ContainsKey(token1) == false || token1 != 0)
                {
                    Holder._DictLotSize.TryAdd(token1, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == token1).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }

            }
            
        
            
            if (!_mwatchDict.ContainsKey(token))
            {
              
                DataRow dr = _table.NewRow();
               // if (chkb.Checked == false)
               // {
                    dr["UniqueIdentifier"] = token;
                    dr["InstrumentName(A)"] = CSV_Class.cimlist.Where(q => q.Token == token).Select(a => a.InstrumentName).First();
                    dr["DescriptionB"] = ASCIIEncoding.ASCII.GetString(CSV_Class.cimlist.Where(q => q.Token == token).Select(a => a.Name).First());
                    dr["ExpiryDate(D)"] = LogicClass.ConvertFromTimestamp(date);
                    dr["OptionType(E)"] = combo_OptionType.Text.Length > 0 ? combo_OptionType.Text : "XX";
                    dr["StrikePrice(F)"] = combo_StrikePrice.Text.Length > 0 ? combo_StrikePrice.Text : "-1";
                    dr["Symbol(C)"] = comboB_Symbol.SelectedItem.ToString();
                    dr["Bid(G)"] = "";
                    dr["BidQ"] = 0;
                    dr["Ask(H)"] = "";
                    dr["AskQ"] = 0;
                    dr["LTP(I)"] = "";
                    dr["LTQ"] = 0;
                  
                    dr["Open"] = 0;
                    dr["High"] = 0;
                    dr["Low"] = 0;
                    dr["Close"] = 0;
                    dr["Volume"] = 0;
                    dr["OI"] = 0;
                    dr["ATP"] = 0;
                    dr["TBQ"] = 0;
                    dr["TSQ"] = 0;
                    dr["LTP_Vol(J)"] = 0;
                    dr["Bid_Vol(K)"] = 0;
                    dr["Ask_Vol(L)"] = 0;
                    dr["Delta(M)"] = 0;
                    dr["Gamma(N)"] = 0;
                    dr["Vega(O)"] = 0;
                    dr["Theta(P)"] = 0;
                    dr["Rho(Q)"] = 0;

                    
                    dr["R"] = "";
                    dr["S"] = "";
                    dr["T"] = "";
                    dr["U"] = "";
                    dr["V"] = "";
                    dr["W"] = "";
                    dr["X"] = "";
                    dr["Y"] = "";
                    dr["Z"] = "";
                    dr["UniqueIdentifier_FUT"] = token1;
                    dr["LTP_FUT"] = 0;



                    //==============================================================================================================================  ======
                    dr["TotalTrades"] = 0;
                    dr["TotalQtyTraded"] = 0;
                    dr["TotalTradedValue"] = 0;
                    dr["HighestPriceEver"] = 0;
                    dr["LowestPriceever"] = 0;
                 
    
              
             

    

               dr["Lotsize"] = CSV_Class.cimlist.Where(q => q.Token == token).Select(a => a.BoardLotQuantity).First();
               dr["Exchange"] = combo_Exchange.SelectedItem.ToString();


               if(DGV.SelectedRows.Count==0)
               {

                   _table.Rows.Add(dr);
               }
               else
               {
                   a = DGV.SelectedRows[0].Index;
                   _table.Rows.InsertAt(dr, a);
               }
               // UDP_Reciever.Instance.Subscribe = token;
             //   UDP_Reciever.Instance.Subscribe = token1;

                LZO_NanoData.LzoNanoData.Instance.Subscribe = token;
                LZO_NanoData.LzoNanoData.Instance.Subscribe = token1;
            }

        }

        private void comboB_Symbol_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void combo_Exoiry_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                if(!comboBInstType.Text.Substring(1,3).Equals("OPT"))
                    AddSymbolFirstTime();
        }

        private void DGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.F1 || e.KeyCode == Keys.Oemplus)
            {
                foreach (DataGridViewRow DGVR in DGV.SelectedRows)
                {
                    BuyOrder(DGVR);
                }
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.F2 || e.KeyCode == Keys.OemMinus)
            {
                foreach (DataGridViewRow DGVR in DGV.SelectedRows)
                {
                    SellOrder(DGVR);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow DGVR in DGV.SelectedRows)
                {
                    _mwatchDict.Remove(Convert.ToInt32(DGVR.Cells["UniqueIdentifier"].Value.ToString()));
                }
            }
        }


        internal string ok()
        {
            string token = "";
            if (comboB_Symbol.Text == "" && comboBInstType.Text == "")
            {
                return "";
            }
            if (comboBInstType.Text == "FUTIVX" || comboBInstType.Text == "FUTIDX" || comboBInstType.Text == "FUTSTK")
            {
                int t = CSV_Class.cimlist.FindIndex(q => q.Symbol == comboB_Symbol.Text && q.ExpiryDate == this.date && q.InstrumentName == comboBInstType.Text);

                token = CSV_Class.cimlist[t].Token.ToString();
            }
            else
            {
                int t = CSV_Class.cimlist.FindIndex(q => q.Symbol == comboB_Symbol.Text && q.ExpiryDate == this.date && q.InstrumentName == comboBInstType.Text && q.OptionType == combo_OptionType.Text && q.StrikePrice == Convert.ToInt32(combo_StrikePrice.Text));

                token = CSV_Class.cimlist[t].Token.ToString();
            }
            return token;
               
        }
        
        private void BuyOrder(DataGridViewRow Dr)
        {
            using (var frmord = new FrmOrderEntry())
            {
                frmord.lblOrderMsg.Text = "Buy " + Dr.Cells["Symbol(C)"].Value + "(" + Dr.Cells["UniqueIdentifier"].Value +
                                          ")  ";
                frmord.lblOrderMsg.BackColor = Color.Blue;
                frmord.LEG_PRICE = Convert.ToDouble(Dr.Cells["Bid(G)"].Value);
                frmord.LEG_SIZE = Convert.ToInt32(Dr.Cells["BidQ"].Value);
                // frmord.DesktopLocation = new Point(100, 100);
                int x = (Screen.PrimaryScreen.WorkingArea.Width - frmord.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - frmord.Height) - 50;
                frmord.Location = new Point(x, y);

                var v = Convert.ToInt32(Dr.Cells["ExpiryDate(D)"].Value);
                if (frmord.ShowDialog(this) == DialogResult.OK)
                {
                    if (frmord.FormDialogResult == (int)OrderEntryButtonCase.SUBMIT)
                    {
                        NNFInOut.Instance.BOARD_LOT_IN_TR(Convert.ToInt32(Dr.Cells["UniqueIdentifier"].Value),
                              Dr.Cells["InstrumentName(A)"].Value.ToString(),
                             Dr.Cells["Symbol(C)"].Value.ToString(),
                             Convert.ToInt32(Dr.Cells["ExpiryDate(D)"].Value),
                              Convert.ToInt32(Dr.Cells["StrikePrice(F)"].Value),
                              Dr.Cells["OptionType(E)"].Value.ToString(),
                              1,
                              frmord.LEG_SIZE * Convert.ToInt32(Dr.Cells["Lotsize"].Value),
                                Convert.ToInt32(frmord.LEG_PRICE * 100));

                    }
                }
            }
        }

        private void SellOrder(DataGridViewRow Dr)
        {
            using (var frmord = new FrmOrderEntry())
            {
                frmord.lblOrderMsg.Text = "Sell " + Dr.Cells["Symbol(C)"].Value + "(" + Dr.Cells["UniqueIdentifier"].Value +   ")  ";
                frmord.lblOrderMsg.BackColor = Color.Red;
                frmord.LEG_PRICE = Convert.ToDouble(Dr.Cells["Ask(H)"].Value);
                frmord.LEG_SIZE = Convert.ToInt32(Dr.Cells["AskQ"].Value);
                int x = (Screen.PrimaryScreen.WorkingArea.Width - frmord.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - frmord.Height) - 50;
                frmord.Location = new Point(x, y);

                if (frmord.ShowDialog(this) == DialogResult.OK)
                {
                    if (frmord.FormDialogResult == (int)OrderEntryButtonCase.SUBMIT)
                    {
                        NNFInOut.Instance.BOARD_LOT_IN_TR(Convert.ToInt32(Dr.Cells["UniqueIdentifier"].Value),
                             Dr.Cells["InstrumentName(A)"].Value.ToString(),
                            Dr.Cells["Symbol(C)"].Value.ToString(),
                            Convert.ToInt32(Dr.Cells["ExpiryDate(D)"].Value),
                             Convert.ToInt32(Dr.Cells["StrikePrice(F)"].Value),
                             Dr.Cells["OptionType(E)"].Value.ToString(),
                             2,
                             frmord.LEG_SIZE * Convert.ToInt32(Dr.Cells["Lotsize"].Value),
                               Convert.ToInt32(frmord.LEG_PRICE)*100);
                    }
                }
            }
        }

        delegate void OnDataArrivedmktDelegate(Object o, ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP> Stat);
        public void OnDataArrived(Object o, ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP> Stat)
        {

            string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
         
                if (DGV.Rows.Count == 0)
                {
                    return;
                }
                if (DGV.InvokeRequired == true)
                {

                    DGV.Invoke(new OnDataArrivedmktDelegate(OnDataArrived), o, new ReadOnlyEventArgs<INTERACTIVE_ONLY_MBP>(Stat.Parameter));
                        return;
                  
                }

                var rowlist1 = _table.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["UniqueIdentifier_FUT"] == DBNull.Value ? 0 : x["UniqueIdentifier_FUT"] == "" ? 0 : x["UniqueIdentifier_FUT"]) ==IPAddress.HostToNetworkOrder( Stat.Parameter.Token)).ToList();
                 var rowlist = _table.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["UniqueIdentifier"] == DBNull.Value ? 0 : x["UniqueIdentifier"] == "" ? 0 : x["UniqueIdentifier"]) == IPAddress.HostToNetworkOrder(Stat.Parameter.Token)).ToList();


                 try
                {
                    foreach (var i in rowlist)
                    {
                        if (DGV.Rows.Count==0)
                        {
                            return;
                        } 
                        //i["Bid(G)"] = Math.Round(Convert.ToDouble(Stat.Parameter.MAXBID) / 100,2 );
                        //i["Ask(H)"] = Math.Round(Convert.ToDouble(Stat.Parameter.MINASK) / 100,2 );
                        //    i["LTP(I)"] = Math.Round(Convert.ToDouble(Stat.Parameter.LTP)/100 , 2);

                        i["Bid(G)"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[0].Price)) / 100, 4);

                        i["Ask(H)"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.RecordBuffer[5].Price)) / 100, 4);


                        i["LTP(I)"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);
                            //====================================================================================
                            string ExpiryDate = i["ExpiryDate(D)"].ToString();
                            int DTE = (Convert.ToDateTime(ExpiryDate) - DateTime.Today).Days;
                            decimal DTE_in_Years = DTE / 365.00m;

                            TimeSpan diff = DateTime.Parse(ExpiryDate) - DateTime.Now;



                            double Risk_free_rate = 0;
                            double Dividend_Yield = 0;
                        //==================================================================  == =  
                         
                                DataRow[] drow = null;
                              
                                    i["OI"] = Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield));
                                    i["ATP"] = Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield));

                                    var task1  = Task.Factory.StartNew(() => i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Bid(G)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Bid(G)"].ToString()), Dividend_Yield)) * 100, 4));
                                   
                        i["Bid_Vol(K)"] = task1.Result;
                        task1.Dispose();
                        task1 = null;
                      //  Parallel.ForEach(task1 , t => t.DoSomethingInBackground()); 
                                   // i["Bid_Vol(K)"] = i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Bid(G)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Bid(G)"].ToString()), Dividend_Yield)) * 100, 4);
                       
                        var task2 = Task.Factory.StartNew(() => i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Ask(H)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Ask(H)"].ToString()), Dividend_Yield)) * 100, 4));
                        i["Ask_Vol(L)"] = task2.Result;
                        task2.Dispose();
                        task2 = null;

                       // i["Ask_Vol(L)"] = i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Ask(H)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["Ask(H)"].ToString()), Dividend_Yield)) * 100, 4);
                        var task3 = Task.Factory.StartNew(() => i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield)) * 100, 4));
                        i["LTP_Vol(J)"] = task3.Result;
                        task3.Dispose();
                        task3 = null;
                        //i["LTP_Vol(J)"] = i["OptionType(E)"].ToString() == "CE" ? Math.Round(Convert.ToDouble(ImpliedCallVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield)) * 100, 4) : Math.Round(Convert.ToDouble(ImpliedPutVolatility(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["LTP(I)"].ToString()), Dividend_Yield)) * 100, 4);
                        var task4 = Task.Factory.StartNew(() => (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallDelta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutDelta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Delta(M)"] = task4.Result;
                        task4.Dispose();
                        task4 = null;
                                   //i["Delta(M)"] = (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallDelta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutDelta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4");
                        var task5 = Task.Factory.StartNew(() => (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(Gamma(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(Gamma(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Gamma(N)"] = task5.Result;
                        task5.Dispose();
                        task5 = null;
                        //i["Gamma(N)"] = (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(Gamma(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(Gamma(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield))).ToString("f4");
                        var task6 = Task.Factory.StartNew(() => (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(Vega(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(Vega(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Vega(O)"] = task6.Result;
                        task6.Dispose();
                        task6 = null;
                        // i["Vega(O)"] = (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(Vega(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(Vega(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4");
                        var task7 = Task.Factory.StartNew(() => (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallTheta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutTheta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Theta(P)"] = task7.Result;
                        task7.Dispose();
                        task7 = null;
                      //  i["Theta(P)"] = (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallTheta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutTheta(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4");
                        var task8 = Task.Factory.StartNew(() => (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallRho(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutRho(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4"));
                        i["Rho(Q)"] = task8.Result;
                        task8.Dispose();
                        task8 = null;
                        


                        //i["Rho(Q)"] = (i["OptionType(E)"].ToString() == "CE" ? Convert.ToDouble(CallRho(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["OI"].ToString()), Dividend_Yield)) : Convert.ToDouble(PutRho(Convert.ToDouble(i["LTP_FUT"].ToString()), Convert.ToDouble(i["StrikePrice(F)"].ToString()), Convert.ToDouble(DTE_in_Years), Risk_free_rate, Convert.ToDouble(i["ATP"].ToString()), Dividend_Yield))).ToString("f4");

                     
                          
                           DGV.Refresh();                      
                    }
                }
              
            catch (DataException a)
            {
                MessageBox.Show("From Live Data fill " + Environment.NewLine + a.Message);
            }
try
                {
                    foreach (var i in rowlist1)
                    {
                        if (DGV.Rows.Count==0)
                        {
                            return;
                        }
                        i["LTP_FUT"] = Math.Round(Convert.ToDouble(IPAddress.HostToNetworkOrder(Stat.Parameter.LastTradedPrice)) / 100, 4);
                         //  DGV.Refresh();
                       // DGV.Update();
                    }
                }

              catch (DataException a)
              {
                  MessageBox.Show("From Live Data fill " + Environment.NewLine + a.Message);
              }
        }
        #region Option Tradeing excel
  
double ImpliedCallVolatility(double UnderlyingPrice,double ExercisePrice, double Time,double Interest, double Target,double Dividend)
{
double	High = 5;
double	Low = 0;
	while ((High - Low) > 0.0001) {
		if (CallOption(UnderlyingPrice, ExercisePrice, Time, Interest, (High + Low) / 2, Dividend) > Target) {
			High = (High + Low) / 2;
		} else {
			Low = (High + Low) / 2;
		}
	}
	return (High + Low) / 2;
}


        double CallOption(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return Math.Exp(-Dividend * Time) * UnderlyingPrice * NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - ExercisePrice * Math.Exp(-Interest * Time) * NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend) - Volatility * Math.Sqrt(Time));
        }
        //================================================================================================
        private double NormSDist(double d)
        {
            double L = 0.0;
            double K = 0.0;
            double dCND = 0.0;
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;
            L = Math.Abs(d);
            K = 1.0 / (1.0 + 0.2316419 * L);

            dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI)) * Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) + a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));

            if (d < 0)
            {
                return 1.0 - dCND;
            }
            else
            {
                return dCND;
            }
        }
        //=========================================================================================================================================================
        double dOne(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
           // string on = ConvertFromTimestamp(Convert.to).ToShortDateString();
           // string on = DateTime.Now.AddMinutes(Time).ToString("HH:mm:ss"); 
            double on = (Volatility * Math.Sqrt(Time));
            double a = (Math.Log(UnderlyingPrice / ExercisePrice) + Math.Pow(Interest - Dividend + 0.5 * Volatility, 2) * (Time)) / on;
            //return (Math.Log(UnderlyingPrice / ExercisePrice) + Math.Pow(Interest - Dividend + 0.5 * Volatility, 2) * Time) / (Volatility * (Math.Sqrt(Time)));   // main
         
            return a;

        }
        //==================================================================================================================================
        double CallDelta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
            //'CallDelta = Application.NormSDist((Log(UnderlyingPrice / ExercisePrice) + (Interest - Dividend) * Time) / (Volatility * Sqr(Time)) + 0.5 * Volatility * Sqr(Time))
        }
        //========================================================================================================================================
        double Gamma(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
           

            return NdOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend) / (UnderlyingPrice * (Volatility * Math.Sqrt(Time)));
        }
        //=========================================================================================================================================
        double NdOne(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return (Math.Exp(-Math.Pow(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend), 2) / 2) / (Math.Sqrt(2 * 3.14159265358979)));
        }
        //=========================================================================================================================================
       
        double Vega(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return 0.01 * UnderlyingPrice * Math.Sqrt(Time) * NdOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend);
        }
        //==========================================================================================================================================
        double CallTheta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            double CT = -(UnderlyingPrice * Volatility * NdOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) / (2 * Math.Sqrt(Time)) - Interest * ExercisePrice * Math.Exp(-Interest * (Time)) * NdTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend);
            return CT / 365;
        }
        //============================================================================================================================================
        double CallRho(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return 0.01 * ExercisePrice * Time * Math.Exp(-Interest * Time) * NormSDist(dTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
        }
        //==========================================================================================================================================
        double dTwo(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend) - Volatility * Math.Sqrt(Time);
        }
        //=====================================================================================================================================================================
        double NdTwo(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return NormSDist(dTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
        }
        //=========================================================================================================  PUT       =================================================================
        double ImpliedPutVolatility(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Target, double Dividend)
        {
            double High = 5;
            double Low = 0;
            while ((High - Low) > 0.0001)
            {
                if (PutOption(UnderlyingPrice, ExercisePrice, Time, Interest, (High + Low) / 2, Dividend) > Target)
                {
                    High = (High + Low) / 2;
                }
                else Low = (High + Low) / 2;


            }
            return (High + Low) / 2;
        }
        //===================================================================================================================================

        double PutOption(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return ExercisePrice * Math.Exp(-Interest * Time) * NormSDist(-dTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - Math.Exp(-Dividend * Time) * UnderlyingPrice * NormSDist(-dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
        }
        //===========================================================================================================
        double PutDelta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return NormSDist(dOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) - 1;
          //'PutDelta = Application.NormSDist((Log(UnderlyingPrice / ExercisePrice) + (Interest - Dividend) * Time) / (Volatility * Sqr(Time)) + 0.5 * Volatility * Sqr(Time)) - 1
        }
        //=========================================================================================================================
        double PutTheta(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            double PT = -(UnderlyingPrice * Volatility * NdOne(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)) / (2 * Math.Sqrt(Time)) + Interest * ExercisePrice * Math.Exp(-Interest * (Time)) * (1 - NdTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend));
            return PT / 365;
        }
        //====================================================================================================================================
        double PutRho(double UnderlyingPrice, double ExercisePrice, double Time, double Interest, double Volatility, double Dividend)
        {
            return -0.01 * ExercisePrice * Time * Math.Exp(-Interest * Time) * (1 - NormSDist(dTwo(UnderlyingPrice, ExercisePrice, Time, Interest, Volatility, Dividend)));
        }
        //========================================================================================================================================
        #endregion
        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMktWatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DGV.Rows.Count == 0)
                return;
          
            DataTable dt = new DataTable("frmwatch");
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
           
            
            //_table.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "Defaultfrmwatch.xml", true);
            _table.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + "Defaultfrmwatch.xml");
        }
       
        private void combo_Exoiry_Click(object sender, EventArgs e)
        {
          //============================== =================================  
            combo_OptionType.Items.Clear();
        }


        void Exchange()
        {
            combo_Exchange.Items.Clear();
            comboB_OrderType.Items.Clear();
            combo_Exchange.Items.Add("NFO");
            combo_Exchange.Items.Add("SPREAD");
            comboB_OrderType.Items.Add("Normal");
            comboB_OrderType.Items.Add("Spread");
        }

        private void comboB_OrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBInstType.Items.Clear();
            string[] dis = CSV_Class.cimlist.Where(ab => ab.InstrumentName != "" && ab.InstrumentName != null).Select(a => a.InstrumentName).Distinct().ToArray();
            comboBInstType.Items.AddRange(dis);
        }
      
        private void btnsaveMktwatch_Click(object sender, EventArgs e)
        {
          

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _table.WriteXml(saveFileDialog1.FileName ,true);
            }

       
        }

        private void btnLoadMktWatch_Click(object sender, EventArgs e)
        {
          //  DGV.Rows.Clear();
            DataSet dst = new DataSet();
            OpenFileDialog OpenFile = new OpenFileDialog();

            //  System.Text.ASCIIEncoding.ASCII.GetString(csv.CSV_Class.cimlist.First(tkn => tkn.Token == IPAddress.NetworkToHostOrder(obj.TokenNo)).Name)

           
          
            if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dst.ReadXml(OpenFile.FileName);
            }
            for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
            {
                //  int RowIndex = DGV.Rows.Add();
                DataRow dr = _table.NewRow();

            

                dr["UniqueIdentifier"] = dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString();
                dr["InstrumentName(A)"] = dst.Tables[0].Rows[i]["InstrumentName(A)"].ToString();
                dr["DescriptionB"] = dst.Tables[0].Rows[i]["DescriptionB"].ToString();
                dr["ExpiryDate(D)"] = dst.Tables[0].Rows[i]["ExpiryDate(D)"].ToString();
                dr["OptionType(E)"] = dst.Tables[0].Rows[i]["OptionType(E)"].ToString();
                dr["StrikePrice(F)"] = dst.Tables[0].Rows[i]["StrikePrice(F)"].ToString();
                dr["Symbol(C)"] = dst.Tables[0].Rows[i]["Symbol(C)"].ToString();
                dr["Bid(G)"] = dst.Tables[0].Rows[i]["Bid(G)"].ToString();
                dr["BidQ"] = dst.Tables[0].Rows[i]["BidQ"].ToString();
                dr["Ask(H)"] = dst.Tables[0].Rows[i]["Ask(H)"].ToString();
                dr["AskQ"] = dst.Tables[0].Rows[i]["AskQ"].ToString();
                dr["LTP(I)"] = dst.Tables[0].Rows[i]["LTP(I)"].ToString();
                dr["LTP_Vol(J)"] = dst.Tables[0].Rows[i]["LTP_Vol(J)"].ToString();
                dr["Bid_Vol(K)"] = dst.Tables[0].Rows[i]["Bid_Vol(K)"].ToString();
                dr["Ask_Vol(L)"] = dst.Tables[0].Rows[i]["Ask_Vol(L)"].ToString();
                dr["Delta(M)"] = dst.Tables[0].Rows[i]["Delta(M)"].ToString();
                dr["Gamma(N)"] = dst.Tables[0].Rows[i]["Gamma(N)"].ToString();
                dr["Vega(O)"] = dst.Tables[0].Rows[i]["Vega(O)"].ToString();
                dr["Theta(P)"] = dst.Tables[0].Rows[i]["Theta(P)"].ToString();
                dr["Rho(Q)"] = dst.Tables[0].Rows[i]["Rho(Q)"].ToString();
               

                dr["R"] = dst.Tables[0].Rows[i]["R"].ToString();
                dr["S"] = dst.Tables[0].Rows[i]["S"].ToString();
                dr["T"] = dst.Tables[0].Rows[i]["T"].ToString();
                dr["U"] = dst.Tables[0].Rows[i]["U"].ToString();
                dr["V"] = dst.Tables[0].Rows[i]["V"].ToString();
                dr["W"] = dst.Tables[0].Rows[i]["W"].ToString();
                dr["X"] = dst.Tables[0].Rows[i]["X"].ToString();
                dr["Y"] = dst.Tables[0].Rows[i]["Y"].ToString();
                dr["Z"] = dst.Tables[0].Rows[i]["Z"].ToString();
                dr["UniqueIdentifier_FUT"] = dst.Tables[0].Rows[i]["UniqueIdentifier_FUT"].ToString();

                dr["LTP_FUT"] = dst.Tables[0].Rows[i]["LTP_FUT"].ToString();
                //=================================================================================================================== ===  == ==  
                dr["LTQ"] = dst.Tables[0].Rows[i]["LTQ"].ToString();
              
                dr["Open"] = dst.Tables[0].Rows[i]["Open"].ToString();
                dr["High"] = dst.Tables[0].Rows[i]["High"].ToString();
                dr["Low"] = dst.Tables[0].Rows[i]["Low"].ToString();
                dr["Close"] = dst.Tables[0].Rows[i]["Close"].ToString();
                dr["Volume"] = dst.Tables[0].Rows[i]["Volume"].ToString();
                dr["OI"] = dst.Tables[0].Rows[i]["OI"].ToString();
                dr["ATP"] = dst.Tables[0].Rows[i]["ATP"].ToString();
                dr["TBQ"] = dst.Tables[0].Rows[i]["TBQ"].ToString();
                dr["TSQ"] = dst.Tables[0].Rows[i]["TSQ"].ToString();
                dr["TotalTrades"] = dst.Tables[0].Rows[i]["TotalTrades"].ToString();
                dr["TotalQtyTraded"] = dst.Tables[0].Rows[i]["TotalQtyTraded"].ToString();
                dr["TotalTradedValue"] = dst.Tables[0].Rows[i]["TotalTradedValue"].ToString();
                dr["HighestPriceEver"] = dst.Tables[0].Rows[i]["HighestPriceEver"].ToString();
                dr["LowestPriceever"] = dst.Tables[0].Rows[i]["LowestPriceever"].ToString();
              

                dr["Lotsize"] = dst.Tables[0].Rows[i]["Lotsize"].ToString();
                dr["Exchange"] = dst.Tables[0].Rows[i]["Exchange"].ToString();
              

                //  _mwatchDict.Add(Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString()), DGV.Rows[RowIndex]);
                _table.Rows.Add(dr);
                UDP_Reciever.Instance.Subscribe = Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString());
                // UDP_Reciever.Instance.Subscribe = Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString()) = null ? Convert.ToInt32("") : Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString());

                Holder._DictLotSize.TryAdd(Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString()), new Csv_Struct()
                {
                    lotsize = CSV_Class.cimlist.Where(q => q.Token == Convert.ToInt32(dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString() == "" ? "0" : dst.Tables[0].Rows[i]["UniqueIdentifier"].ToString())).Select(a => a.BoardLotQuantity).First()
                });
            }
        }

        // show current address and cell content when selection changes
        int row ,col;
        void _grid_SelectionChanged(object sender, EventArgs e)
        {
            String se = "";
          //  se = DGV.SelectedCells[0].Value.ToString();
         //   var spp = DGV.Rows[row-1].Cells[col-1].Value;
           // var sev = DGV.Rows[row].Cells[col];
            // show cell content above grid

            string address = null;
            string text = null;
            string status = "Ready";


            row = DGV.CurrentCellAddress.Y;
            col = DGV.CurrentCellAddress.X;
         
            if (row > -1 && col > -1)
            {
                var cell = DGV.Rows[row].Cells[col];
              
              
                var val = cell.Value;
                text = val != null ? val.ToString() : null;
                address = DGV.GetAddress(row, col);
                var selection = new CellRange(DGV.SelectedCells);
                if (!selection.IsSingleCell)
                {
                    var sel = DGV.GetAddress(selection);
                    try
                    {
                        var avg = DGV.Evaluate(string.Format("Average({0})", sel));
                        var count = DGV.Evaluate(string.Format("Count({0})", sel));
                        var sum = DGV.Evaluate(string.Format("Sum({0})", sel));
                        if ((double)count > 0)
                        {
                            status = string.Format("Average: {0:#,##0.##} Count: {1:n0} Sum: {2:#,##0.##}",
                                avg, count, sum);
                        }
                    }
                    catch
                    {
                        // the selection contains errors...
                    }
                }
            }
           
            _lblAddress.Text = address;
           
            _txtFormula.Text = text;
            _txtFormula.SelectAll();
            _lblStatus.Text = status;
            if (text == "=")
            {
            //  CalcEngine.  Calculation ca = new Calculation();
                List<string> SelectedRows = new List<string>();
                foreach (DataGridViewColumn _dc in DGV.Columns)
                {
                    if (_dc.Visible == false)
                    {

                        //  MessageBox.Show("Column Name " + _dc.Name + " Visible " + _dc.Visible);
                        // SelectedRows.Add(_dc.Name.ToString());
                    }
                    else
                    {
                        SelectedRows.Add(_dc.Name.ToString());
                    }
                }
                List<string> Selectedcolumn = new List<string>();
                List<int> _Selectrowcount = new List<int>();
                foreach (DataGridViewRow _dcc in DGV.Rows)
                {
                    int a = 0;
                    for (int i = 0; i < DGV.RowCount; i++)
                    {
                        a = i;
                    }

                    _Selectrowcount.Add(a);

                    String data = "";
                    data = Convert.ToString(_dcc.Cells[1].Value);

                    if (data == "")
                    {

                    }

                    Selectedcolumn.Add(data);

                }


                //for (int i = 0; i <= _table.Columns.Count; i++)
                //{

                //    string a = Convert.ToString(i);

                //}

                // DGV.Columns("DescriptionB").Selected = true;
                // DGV.Rows(intIndex).Selected = true;
                // ca.Show();
            //    ca.sho(SelectedRows, Selectedcolumn, row, col);

             //   ca.Show();
            }

        }

        public void _grid_SelectionChangedComedata(string addre,string ro,string co )
        {
            string address = null;
            string text = null;
            string status = "Ready";
            int row =Convert.ToInt32( ro);
            int col = Convert.ToInt32(co);
          // DGV[row, col].Value = addre;
            DGV.Rows[row].Cells[col].Value = addre;
            if (row > -1 && col > -1)
            {
                var cell = "";
                if(DGV.Rows.Count>0)
                 cell = DGV.Rows[row].Cells[col].ToString();
           
               // var val = cell.Value;
                var val = addre;
                text = val != null ? val.ToString() : null;
                address = DGV.GetAddress(row, col);
                var selection = new CellRange(DGV.SelectedCells);
                if (!selection.IsSingleCell)
                {
                    var sel = DGV.GetAddress(selection);
                    try
                    {
                        var avg = DGV.Evaluate(string.Format("Average({0})", sel));
                        var count = DGV.Evaluate(string.Format("Count({0})", sel));
                        var sum = DGV.Evaluate(string.Format("Sum({0})", sel));
                        if ((double)count > 0)
                        {
                            status = string.Format("Average: {0:#,##0.##} Count: {1:n0} Sum: {2:#,##0.##}",
avg, count, sum);
                        }
                    }
                    catch
                    {
                        // the selection contains errors...
                    }
                }
            }
            _lblAddress.Text = address;
            _txtFormula.Text = text;
            _txtFormula.SelectAll();
            _lblStatus.Text = status;
            if (text == "=")
            {
             //   Calculation ca = new Calculation();
                List<string> SelectedRows = new List<string>();
                foreach (DataGridViewColumn _dc in DGV.Columns)
                {
                    if (_dc.Visible == false)
                    {
                        //  MessageBox.Show("Column Name " + _dc.Name + " Visible " + _dc.Visible);

                        // SelectedRows.Add(_dc.Name.ToString());



                    }
                    else
                    {

                        SelectedRows.Add(_dc.Name.ToString());
                    }
                }


                List<string> Selectedcolumn = new List<string>();

                // foreach (DataGridViewRow _dcc in DGV.Rows[0].Cells)
                foreach (DataGridViewRow _dcc in DGV.Rows)
                {
                    String data = "";
                    data = Convert.ToString(_dcc.Cells[1].Value);
                    // data = _dcc.Cells[1].Value.ToString();
                    if (data == "")
                    {

                    }
                    //  MessageBox.Show("Column Name Visible " + _dcc.Name.ToString());
                    //  Selectedcolumn.Add ( _dcc.Cells[1].Value.ToString());
                    //MessageBox.Show("Column Name Visible " + _dcc.Visible);
                    Selectedcolumn.Add(data);

                }


                //for (int i = 0; i <= _table.Columns.Count; i++)
                //{

                //    string a = Convert.ToString(i);

                //}

                // DGV.Columns("DescriptionB").Selected = true;
                // DGV.Rows(intIndex).Selected = true;
                // ca.Show();
              //  ca.sho(SelectedRows, Selectedcolumn, row, col);

              //  ca.Show();
            }
        
        
        
        
        }

        void _txtFormula_Validating(object sender, CancelEventArgs e)
        {
            var pt = DGV.CurrentCellAddress;
            var row = pt.Y;
            var col = pt.X;
            if (row > -1 && col > -1)
            {
                var cell = DGV.Rows[row].Cells[col];
                cell.Value = _txtFormula.Text;
            }
        }

        void _txtFormula_KeyPress(object sender, KeyPressEventArgs e)        
        {
            switch ((int)e.KeyChar)
            {
                case 13: // enter
                    e.Handled = true;
                    DGV.Focus();
                    break;
                case 27: // escape
                    e.Handled = true;
                    _grid_SelectionChanged(sender, e);
                    break;
            }
        }
        // show menu with available functions
        void _lblFunctions_MouseDown(object sender, MouseEventArgs e)
        {
            var menu = new FunctionMenu();
            menu.ItemClicked += (ss, ee) =>
            {
                var fn = ee.ClickedItem.Text;
                _txtFormula.SelectedText = fn;
            };
            menu.Show(Control.MousePosition);
        }
        bool trueval = false;
        private void DGV_KeyDown_1(object sender, KeyEventArgs e)
        {
         //  MessageBox.Show( e.KeyCode.ToString());
            if (e.KeyCode == Keys.F3)
            {

              //  MessageBox.Show("F3");
                DataRow dr = _table.NewRow();
                dr["UniqueIdentifier"] = 0;
                _table.Rows.Add();
            }
                

            if(e.KeyCode ==  Keys.Space)
            {
                string strtxt = txt.Text.TrimEnd().TrimStart().Trim();
                if (strtxt != "")
                {
                    if (Regex.IsMatch(strtxt[strtxt.Length - 1].ToString(), @"[=+-/*()]"))
                    {
                        txt.Text += _lblAddress.Text;
                        DGV.Rows[fixindexrow].Cells[fixindexcell].Value = txt.Text.TrimEnd().TrimStart().Trim(); ;
                    }
                    txt.Focus();
                    txt.SelectionStart = txt.Text.Length;
                    
                }
               
            }
            if (e.KeyCode == Keys.Escape)
                txt.Hide();

            if(e.KeyCode == Keys.F2)
            {
                int colind = DGV.SelectedCells[0].ColumnIndex;
                int rowind = DGV.SelectedCells[0].RowIndex;
                if (colind != 1 && colind != 2 && colind != 3 && colind != 4 && colind != 5 && colind != 6)// "BNSFDIFF")
                {
                    txt.Show();
                    //strv = e.ColumnIndex.ToString() + "," + colind.ToString();
                    txt.Text = Convert.ToString(DGV.Rows[rowind].Cells[colind].Value);
                    DGV.Controls.Add(txt);
                    txt.Location = this.DGV.GetCellDisplayRectangle( colind,rowind, true).Location;

                   


                    txt.Width = DGV.Columns[0].Width;
                    txt.Focus();
                    q = 0;
                    fixindexcell = colind;
                    fixindexrow = rowind;
                    txt.SelectionStart = txt.MaxLength;
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
              
               

                DataTable dt = _table;
            }

            if ((e.Control && e.KeyCode == Keys.Delete) || (e.Shift && e.KeyCode == Keys.Delete))
            {
                CopyClipboard();
            }
            if ((e.Control && e.KeyCode == Keys.Insert) || (e.Shift && e.KeyCode == Keys.Insert))
            {
                PasteClipboard();
            }
            if (e.KeyData == (Keys.Shift| Keys.Enter))
 
            {
                string _OutVal = "";
               
                string[] s = Clipboard.GetText().Split('#');
                int spltval =  s.Length-1;
               // for (int i = DGV.SelectedCells.Count; i >=0 ; i--)
                bool LastChar = false;
                bool LastNumIncr = false;
                char strrepval ;
                foreach( DataGridViewCell _dgvc in DGV.SelectedCells)
                {                
                    Console.WriteLine(_dgvc.ColumnIndex + " " + _dgvc.RowIndex +" "+ _dgvc.Value);
                    foreach (char c in s[spltval])
                    {
                        if (Information.IsNumeric(c) && LastChar)
                        {
                            // int icout = int.Parse(c.ToString());
                            // icout++;
                            //_OutVal += icout;
                            strrepval = _OutVal[_OutVal.Length-1];
                            if (strrepval == '$')
                            {

                                _OutVal += ((int)Char.GetNumericValue(c)).ToString();
                         
                            }
                            else
                                _OutVal += _dgvc.RowIndex + 1;
                            
                            LastChar = false;
                            LastNumIncr = true;
                        }
                        else
                        {
                            if (Regex.IsMatch(c.ToString(), @"[=+-/*()]"))
                                LastNumIncr = false;

                            if (!LastNumIncr)
                            _OutVal += c;

                            //  Regex r = ;  //Regex Check If A-Z then true
                            if(Regex.IsMatch( c.ToString() ,@"[a-zA-Z]"))                           
                            LastChar = true;                           
                        }
                    }
                   //DGV.Rows[DGV.SelectedCells[i].RowIndex].Cells[DGV.SelectedCells[0].ColumnIndex].Value = _OutVal;
                  // DGV.Rows[_dgvc.RowIndex].Cells[_dgvc.ColumnIndex].Value = _OutVal;
                    _dgvc.Value = _OutVal;
                    if (spltval == 0)
                    {
                        if (spltval<s.Length-1)
                        spltval++;
                    }
                        
                    else if (spltval == s.Length - 1)
                        spltval--;
                    else
                        spltval--;
                 //   s[spltval] = _OutVal;
                    _OutVal = "";
                }
            }
        }
       
        private void CopyClipboard()
        {
            //DataObject d = DGV.GetClipboardContent();
            //Clipboard.SetDataObject(d);
            int val = 0;
            int minval = 0;
            int maxval = 0;
            int swap = 0;
            int rowind = 0;
            string strval = string.Empty;
            int selectindex = DGV.SelectedCells[0].ColumnIndex;
            foreach (DataGridViewCell _dgvc in DGV.SelectedCells)
            {
                if(val == 0)
                {
                    minval = _dgvc.ColumnIndex;
                    rowind = _dgvc.RowIndex;
                }
                else
               maxval = _dgvc.ColumnIndex;

                val++;

               
            }
            if(minval >maxval && maxval!=0)
            {
                swap = maxval;
                maxval = minval;
                minval = swap;
                swap = 0;
            }
            for (int i = 0; i < DGV.SelectedCells.Count; i++)
            {
                strval += DGV.Rows[rowind].Cells[minval].Value + "#";
                minval++;
            }
            Clipboard.SetDataObject(strval.Remove(strval.Length -1), true);
          
        }
        private void PasteClipboard()
        {
            try
            {
                int sptval = 0;
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                int iFail = 0, iRow = DGV.CurrentCell.RowIndex;
                int iCol = DGV.CurrentCell.ColumnIndex;
                DataGridViewCell oCell;
                foreach (string line in lines)
                {
                    if (iRow < DGV.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.DGV.ColumnCount)
                            {
                                oCell = DGV[iCol + i, iRow];
                                if (!oCell.ReadOnly)
                                {
                                    if (oCell.Value.ToString() != sCells[i])
                                    {
                                        oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                        oCell.Style.BackColor = Color.Tomato;
                                    }
                                    else
                                        iFail++;
                                    //only traps a fail if the data has changed and you are pasting into a read only cell
                                }
                            }
                            else
                            { break; }
                        }
                        iRow++;
                    }
                    else
                    { break; }
                    if (iFail > 0)
                        MessageBox.Show(string.Format("{0} updates failed due to read only column setting", iFail));
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }
        int fixindexrow = -1; int fixindexcell = -1;
        private void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            return;
            row = DGV.CurrentCellAddress.Y;
            col = DGV.CurrentCellAddress.X;
            if (row > -1 && col > -1)
            {
                var cell = DGV.Rows[row].Cells[col];
                string val =Convert.ToString( cell.Value);
               
                if (val == "=")
                {
                   // fixindexrow = -1;
                  //  fixindexrow = -1;
                   // Calculation ca = new Calculation();
                    List<string> SelectedRows = new List<string>();
                    foreach (DataGridViewColumn _dc in DGV.Columns)
                    {
                        if (_dc.Visible == false)
                        {
                          //  fixindexrow = e.RowIndex;
                         //   fixindexcell = e.RowIndex;
                            //  MessageBox.Show("Column Name " + _dc.Name + " Visible " + _dc.Visible);

                            // SelectedRows.Add(_dc.Name.ToString());

                        }
                        else
                        {

                            SelectedRows.Add(_dc.Name.ToString());
                        }
                    }
                    
                    List<string> Selectedcolumn = new List<string>();
                    List<int> _Selectrowcount = new List<int>();

                    foreach (DataGridViewRow _dcc in DGV.Rows)
                    {
                        int a = 0;
                        for (int i = 0; i < DGV.RowCount; i++)
                        {
                            a = i;
                        }

                        _Selectrowcount.Add(a);

                        String data = "";
                        data = Convert.ToString(_dcc.Cells[1].Value);

                        if (data == "")
                        {

                        }

                        Selectedcolumn.Add(data);

                    }


                    //for (int i = 0; i <= _table.Columns.Count; i++)
                    //{

                    //    string a = Convert.ToString(i);

                    //}

                    // DGV.Columns("DescriptionB").Selected = true;
                    // DGV.Rows(intIndex).Selected = true;
                    // ca.Show();
                 //   ca.sho(SelectedRows, Selectedcolumn, row, col);

               //     ca.ShowDialog();
                
                }
              
            }

           
        }

        private void DGV_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        private Rectangle dragBoxFromMouseDown;
        private object valueFromMouseDown;
        private void DGV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = DGV.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    if (info.RowIndex >= 0 && info.ColumnIndex >= 0)
                    {
                        string text = Convert.ToString( DGV.Rows[info.RowIndex].Cells[info.ColumnIndex].Value);
                        if (text != null)
                        {
                            //Need to put braces here  CHANGE

                            DGV.DoDragDrop(text, DragDropEffects.Copy);
                        }
                    }
                }
            }
        }
        private void DGV_MouseMove(object sender, MouseEventArgs e)
        {
            //================================================================  //
            //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            //{
            //    // If the mouse moves outside the rectangle, start the drag.
            //    if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
            //    {
            //        // Proceed with the drag and drop, passing in the list item.                    
            //        DragDropEffects dropEffect = DGV.DoDragDrop(valueFromMouseDown, DragDropEffects.Copy);
            //    }
            //}
        }

     
        private void DGV_DragOver(object sender, DragEventArgs e)
        {
         //   e.Effect = DragDropEffects.Copy;
        }

        private void DGV_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            //Point clientPoint = DGV.PointToClient(new Point(e.X, e.Y));

            //// If the drag operation was a copy then add the row to the other control.
            //if (e.Effect == DragDropEffects.Copy)
            //{
            //    string cellvalue = e.Data.GetData(typeof(string)) as string;
            //    var hittest = DGV.HitTest(clientPoint.X, clientPoint.Y);
            //    if (hittest.ColumnIndex != -1
            //        && hittest.RowIndex != -1)
            //        DGV[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;

            //}
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            CopyClipboard();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyClipboard();
        }

        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteClipboard();
        }

        private void DGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
        //int indeccell = 0;
        //int indecrow = 0;
        //string strv = "";
        int q = 0;
        private void DGV_CellLeave(object sender, DataGridViewCellEventArgs e)
        {     
            try
            {
                if (e.ColumnIndex != 0)
                {
                    
                    if (e.ColumnIndex != 1 || e.ColumnIndex != 2 || e.ColumnIndex != 3 || e.ColumnIndex != 4 || e.ColumnIndex != 5 || e.ColumnIndex != 6)// "BNSFDIFF")
                    {
                        //  if (Information.IsNumeric(txt.Text) == true)
                        if (q == 0)
                        {
                            DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToString(txt.Text);
                            txt.Focus();
                        //    txt.Hide();
                            q++;
                        }
                    }                
                }
            }
            catch { }
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           //  DGV.Rows[indecrow].Cells[indeccell].Value = DGV.Rows[indecrow].Cells[indeccell].Value.ToString() + DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void DGV_CellEnter(object sender, DataGridViewCellEventArgs e)
        {          
        // MessageBox.Show(e.ColumnIndex.ToString());
        // if (fixindexcell > -1 && fixindexrow > -1)
        // DGV.Rows[fixindexrow].Cells[fixindexcell].Value += _lblAddress.Text; // DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();            
        }

        private void DGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sr = DGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(0);
            if (sr == "=")
            {
               // fixindexrow = e.RowIndex;
               // fixindexcell = e.ColumnIndex;
            }
        }
        private void DGV_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!this.valueChanged)
            {
                this.valueChanged = true;
                this.DGV.NotifyCurrentCellDirty(true);
            }
        }

        private bool valueChanged;
        public virtual bool EditingControlValueChanged
        {
            get
            {
                return this.valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }
       
       

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Shift | Keys.Space)) 
            {
                DGV.Rows[fixindexrow].Cells[fixindexcell].Value = txt.Text.TrimEnd().TrimStart().Trim();
                txt.Hide();
                DGV.Focus();
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                DGV.Focus();
           if(e.KeyCode ==  Keys.Space)
           {
               txt.Text = txt.Text.TrimEnd().Trim().TrimStart() ;
               txt.SelectionStart = txt.Text.Length;
           }         
        }
        private void DGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void DGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {            
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex == -1)
                {
                    DGV.ContextMenuStrip = null;
                     cl = new DataGridViewColumnSelector(DGV);
                }
            }   
        }
  
        DataGridViewColumnSelector cl = null;
        private void DGV_MouseClick(object sender, MouseEventArgs e)
        {
            DGV.ContextMenuStrip = null;
             cl = null;
        }

        private void DGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex!=-1)
            DGV.ContextMenuStrip = contextMenuStrip1;
        }

        private void comboB_OrderType_Click(object sender, EventArgs e)
        {
            comboBInstType.Items.Clear();
        }

        private void comboBInstType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        int skipval = 0;
        int takeval = 10;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DGV.DataSource = _table.AsEnumerable().Skip(skipval).Take(takeval).CopyToDataTable();
            skipval += DGV.Rows.Count - 1;
           //    this.CurrentPageIndex = 1;
          //  this.DGV.DataSource = GetCurrentRecords(this.CurrentPageIndex, con);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            skipval -= DGV.Rows.Count - 1;
            DGV.DataSource = _table.AsEnumerable().Skip(skipval).Take(takeval).CopyToDataTable();
        }
        private void CalculateTotalPages()
        {
            //int rowCount = ds.Tables["Customers"].Rows.Count;
            //TotalPage = rowCount / PgSize;
            //// if any row left after calculated pages, add one more page 
            //if (rowCount % PgSize > 0)
            //    TotalPage += 1;
        }
        private void changeentity()
        {
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                DataRow[] drow = null;
                if (DGV.Rows[i].Cells["DescriptionB"].Value != null && DGV.Rows[i].Cells["DescriptionB"].Value != "" )
               // if ( DGV.Rows[i].Cells["DescriptionB"].Value != "")
                {
                    string oldgrdval = DGV.Rows[i].Cells["DescriptionB"].Value.ToString();
                    string[] splitval = DGV.Rows[i].Cells["DescriptionB"].Value.ToString().Split('5');
                    string stval = splitval[1].Substring(0, 3);
                    DateTime dtime1 = Convert.ToDateTime(DGV.Rows[i].Cells["ExpiryDate(D)"].Value).AddMonths(1);
                    string stkmonth = dtime1.ToString("MMM").ToUpper();

                    var v = CSV_Class.cimlist.Where(a => a.EGMAGM == (DGV.Rows[i].Cells["DescriptionB"].Value.ToString().Replace(stval, stkmonth))).Select(ab => new logicclass
                    {
                        expirydate = LogicClass.ConvertFromTimestamp(ab.ExpiryDate),
                        fullname = ab.EGMAGM,
                        tockenno = ab.Token,
                        Unique_Identifier = ab.Token.ToString()
                    }).ToList();

                    string expression = "DescriptionB ='" + oldgrdval + "'";
                    if (v.Count > 0)
                    {

                        //var rowlisttt = _table.Rows.Cast<DataRow>().Where(x => (x["DescriptionB"] == null ? "" : x["Description"]) == oldgrdval).ToList();

                        var rowlist = _table.Rows.Cast<DataRow>().Where(x => (x["DescriptionB"] == null ? "" : x["DescriptionB"]) == oldgrdval).ToList();
                        foreach (var i1 in rowlist)
                        {
                            if (DGV.Rows.Count == 0)
                            {
                                return;
                            }


                            i1["DescriptionB"] = v[0].fullname;
                            i1["ExpiryDate(D)"] = v[0].expirydate;
                            i1["UniqueIdentifier"] = v[0].Unique_Identifier;
                            UDP_Reciever.Instance.Subscribe = v[0].tockenno;


                        }
                    }



                }
            }




        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            changeentity();

            
        }

        private void comboB_Symbol_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
                    var s = cell.Value;
                    if (s == null)
                    {
                        goto Finish;
   
                    }
               
                    if (Regex.IsMatch(s.ToString(), @"[=]") && s !=null )
                    {
 
                   string[] operands = Regex.Split(s.ToString(), @"\D+");
                   string[] operands2 = Regex.Split(s.ToString(), @"\d+");
                   string ar = "";   
                   foreach (string value in operands)
                   {
                       //
                       // Parse the value to get the number.
                       //
                       int number1;
                       if (int.TryParse(value, out number1))
                       {
                           int er = Convert.ToInt32(value) + 1;
                           
                          // MessageBox.Show(er.ToString());
                           ar += Convert.ToString(er)+",";

                          
                       }
                   }
                   string[] operan3 = Regex.Split(ar, @"\D+");
                   string[] arr = new string[operan3.Length];
                   string ar2 = ""; 
                   for (int i = 0, j = 0; i < operan3.Length; i++, j++)
                   {
                      // arr[i] = operands2[i] + operan3[j];
                       ar2 += Convert.ToString(operands2[i] + operan3[j]);
                   }
                  

                 


                   s = ar2;


                    }
                Finish :
                    dRow[cell.ColumnIndex] = s;
                }
                dt.Rows.Add(dRow);
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "*.xls|*.*";
           
            
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Excel.ExcelUtlity obj = new Excel.ExcelUtlity();
                    obj.WriteDataTableToExcel(dt, "Excel Report", saveFileDialog1.FileName, "Details");
                }
            
        }

       

        //private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //    string sd = "";

        //    sd = DGV.SelectedCells[0].Value.ToString();

        //    if (sd == "=")
        //    {
        //        Calculation ca = new Calculation();
        //        List<string> SelectedRows = new List<string>();
        //        foreach (DataGridViewColumn _dc in DGV.Columns)
        //        {
        //            if (_dc.Visible == false)
        //            {
        //                //  MessageBox.Show("Column Name " + _dc.Name + " Visible " + _dc.Visible);

        //                // SelectedRows.Add(_dc.Name.ToString());



        //            }
        //            else
        //            {

        //                SelectedRows.Add(_dc.Name.ToString());
        //            }
        //        }


        //        List<string> Selectedcolumn = new List<string>();
        //        List<int> _Selectrowcount = new List<int>();

        //        foreach (DataGridViewRow _dcc in DGV.Rows)
        //        {
        //            int a = 0;
        //            for (int i = 0; i < DGV.RowCount; i++)
        //            {

        //                a = i;
        //            }

        //            _Selectrowcount.Add(a);

        //            String data = "";
        //            data = Convert.ToString(_dcc.Cells[1].Value);

        //            if (data == "")
        //            {

        //            }

        //            Selectedcolumn.Add(data);

        //        }


        //        //for (int i = 0; i <= _table.Columns.Count; i++)
        //        //{

        //        //    string a = Convert.ToString(i);

        //        //}

        //        // DGV.Columns("DescriptionB").Selected = true;
        //        // DGV.Rows(intIndex).Selected = true;
        //        // ca.Show();
        //        ca.sho(SelectedRows, Selectedcolumn, row, col);

        //        ca.Show();

        //    }

        //}

       

    }

    public class logicclass
    {
        public DateTime expirydate { get; set; }

        public int  tockenno { get; set; }

        public string fullname { get; set; }

        public string Unique_Identifier { get; set; }
    }


}
