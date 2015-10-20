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
using System.Configuration;
using System.Xml;
using Microsoft.VisualBasic;
using System.Threading;

namespace Client
{
    public partial class Fo_Fo_mktwatch : Form
    {

        private readonly Dictionary<int, DataGridViewRow> _mwatchDict2 = new Dictionary<int, DataGridViewRow>();
        private readonly Dictionary<int, Data1> _DataDict2 = new Dictionary<int, Data1>();
        private DataGridViewCellStyle _makeItBlack;
        private DataGridViewCellStyle _makeItBlue;
        private DataGridViewCellStyle _makeItRed;
        public  int str_price1;
        internal DataTable SpreadTable;
        string strv = "";
    
      

        //private static readonly Fo_Fo_mktwatch instance = new Fo_Fo_mktwatch();
        //public static Fo_Fo_mktwatch Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}
        // this.gvw1.Columns[0].HeaderText = "The new header";

        internal event EventHandler _logoutstatus;
       
        private int portFolioCounter = 1;

        public Fo_Fo_mktwatch()
        {
            InitializeComponent();
            
        }

       public bool _foSpread__logoutstatus()
        {
            bool flag = false;
            foreach (DataGridViewRow row in DGV1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    return false;
                }
            }
            return true;
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
            //  var rowlist = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["Token1"]) == Stat.Parameter.Token || Convert.ToInt32(x["Token2"]) == Stat.Parameter.Token).ToList();
            //DGV1.Rows[0].Cells[""].Value = Global.Instance.OrdetTable.Compute("SUM(STATUS ='Traded')","Token1")
        }


        public void TRADE_CONFIRMATION_TR(byte[] buffer) //-- 20222  
        {
            try
            {    
              //  Client.LogWriterClass.logwritercls.logs("Tradecount.txt", "Time : " + System.DateTime.Now.ToShortTimeString());

                var obj = (MS_TRADE_CONFIRM_TR)DataPacket.RawDeserialize(buffer, typeof(MS_TRADE_CONFIRM_TR));
                           
                DataRow[] dr = SpreadTable.Select("Token1 =" + IPAddress.HostToNetworkOrder(obj.Token) + "");
                if (dr.Length > 0)
                {
                  //  LogWriterClass.logwritercls.logs("tradeconfermation" , (IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator)).ToString());

                    DataGridViewRow row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token1"].Value.ToString().Equals(Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))).First();
                    string _fartoken = DGV1.Rows[row.Index].Cells["Token2"].Value.ToString();
                  
                    string[] drNeartoken = Global.Instance.OrdetTable.AsEnumerable().Where(a => a.Field<string>("status") == "Traded" && a.Field<string>("TokenNo") == Convert.ToString(_fartoken)) // _fartoken)
                                          .Select(av => av.Field<string>("Price")).ToArray() ;
                    if (drNeartoken.Length > 0)
                    {
                    if (IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator) == 1)
                    {                       
                            DGV1.Rows[row.Index].Cells["BNSFTD"].Value = Convert.ToDouble(drNeartoken[drNeartoken.Length - 1]) - ((IPAddress.HostToNetworkOrder(obj.Price))/100.00);
                    }
                    else
                    {                       
                        DGV1.Rows[row.Index].Cells["BFSNTD"].Value = ((IPAddress.HostToNetworkOrder(obj.Price))/100.00) - Convert.ToDouble(drNeartoken[drNeartoken.Length-1]);
                    }
                    }
                   
                }
            }
            catch(Exception ex)
            {           
               Client.LogWriterClass.logwritercls.logs("ErrorValuecheck.txt", "Value Check update in gridview" + ex.Message);
            }
       Task.Factory.StartNew(()=>   Fillqty_ingrd(buffer));
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (portFolioCounter==0)
             portFolioCounter = 1; 
            using (AddToken _AddToken = new AddToken())
            {
                _AddToken.txtpfName.Text = portFolioCounter.ToString();
                _AddToken.lblPfName.Visible = true;
                _AddToken.txtpfName.Visible = true;
                _AddToken.Text = "Add Near Month Token";
                _AddToken.button1.Text = "Add Next Token";
                if (_AddToken.ShowDialog() == DialogResult.OK)
                {
                     DataRow dr = SpreadTable.NewRow();

                    dr["PF"] = _AddToken._objOut.PFName;
                    dr["NEAR"] = _AddToken._objOut.Desc1;
                    dr["Token1"] = _AddToken._objOut.Token1;

                    dr["FAR"] = _AddToken._objOut.Desc2;
                    dr["Token2"] = _AddToken._objOut.Token2;

                    SpreadTable.Rows.Add(dr);
                    //dr["BFSNDIFF"] =  0.0000;
                    //dr["BNSFDIFF"]=0.0000;

                    //dr["BNSFMNQ"] = 0.0000;
                    //dr["BFSNMNQ"] = 0.0000;

                    //dr["BNSFMXQ"] = 0.0000;
                    //dr["BFSNMXQ"] = 0.0000;

                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BNSFDIFF"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BFSNDIFF"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BNSFMNQ"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BFSNMNQ"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BNSFMXQ"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count-1].Cells["BFSNMXQ"].Value = 0.00;
                    DGV1.Rows[DGV1.Rows.Count - 1].Cells["TICKS"].Value = 0;


                   
                    
                    UDP_Reciever.Instance.Subscribe = _AddToken._objOut.Token1;
                    UDP_Reciever.Instance.Subscribe = _AddToken._objOut.Token2;
                  
                    
                    
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
                    // this.DGV1.Columns[dc.HeaderText.Replace(" ", "")].Visible = true;
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
                if(ds.Tables.Count ==  0)
                {
                    return;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                     string st = ds.Tables[0].Rows[i]["Input"].ToString();
                     if (st==null)
                         continue;
                    this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ","")].Visible = false;
                }
            }
            else
            {
                //String GetProfileName = frmprf.GetProfileName();
                //DataSet ds = new DataSet();
                //ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MarketCol.xml");
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    string st = ds.Tables[0].Rows[i]["Input"].ToString();
                //    this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ", "")].Visible = true;
                //}
            }
           
        }

        delegate void OnDataArrivedDelegate(Object o, ReadOnlyEventArgs<FinalPrice> Stat);
        public void OnDataArrived(Object o, ReadOnlyEventArgs<FinalPrice> Stat)
        {
            try
            {
                if (DGV1.InvokeRequired)
                {
                    DGV1.Invoke(new OnDataArrivedDelegate(OnDataArrived), o, new ReadOnlyEventArgs<FinalPrice>(Stat.Parameter));
                    return;
                }
         
                var rowlist = SpreadTable.Rows.Cast<DataRow>().Where(x => Convert.ToInt32(x["Token1"]) == Stat.Parameter.Token || Convert.ToInt32(x["Token2"]) == Stat.Parameter.Token).ToList();
                try
                {
                    foreach (var i in rowlist)
                    {
                        if (DGV1.Rows.Count==0)
                        {
                            return;
                        }
                       
                        if (Convert.ToInt32(i["Token1"]) == Stat.Parameter.Token)
                        {
                            i["NBID"] = Math.Round(Convert.ToDouble(Stat.Parameter.MAXBID) / 100, 4);
                            i["NASK"] = Math.Round(Convert.ToDouble(Stat.Parameter.MINASK) / 100, 4);
                            i["NLTP"] = Math.Round(Convert.ToDouble(Stat.Parameter.LTP) / 100, 4);
                            
                        }
                        else if (Convert.ToInt32(i["Token2"]) == Stat.Parameter.Token)
                        {
                                       
                            i["FBID"] = Math.Round(Convert.ToDouble(Stat.Parameter.MAXBID) / 100, 4);
                            i["FASK"] = Math.Round(Convert.ToDouble(Stat.Parameter.MINASK) / 100, 4);
                            i["FLTP"] = Math.Round(Convert.ToDouble(Stat.Parameter.LTP) / 100, 4);
                        }
                        Global.Instance.MTMDIct.AddOrUpdate(Stat.Parameter.Token, Stat.Parameter.LTP, (k, v) => Stat.Parameter.LTP);
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

        private void SetData(DataGridViewCell DGCell, double ValueOne)
        {
            if (DGCell != null)
            {
                double ValueTwo = DGCell.Value == DBNull.Value || String.IsNullOrWhiteSpace(DGCell.Value.ToString()) ? 0 : Convert.ToDouble(DGCell.Value);           //Convert.ToDouble(DGCell.Value);
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



        private void SetData2(DataGridViewCell DGCell, double ValueOne)
        {
          
            if (DGCell != null)
            {
                double ValueTwo = DGCell.Value == DBNull.Value || String.IsNullOrWhiteSpace(DGCell.Value.ToString()) ? 0 : Convert.ToDouble(DGCell.Value);          
            }

            DGCell.Value = ValueOne;
            Console.WriteLine(ValueOne + "  " + DGCell);
           
        }

        private void Fo_Fo_mktwatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            applyFun();
            //Form xForm = sender as Form;
            //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //if (ConfigurationManager.AppSettings.AllKeys.Contains(xForm.Name))
            //    config.AppSettings.Settings[xForm.Name].Value = String.Format("{0};{1};{2};{3}", xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height);
            //else
            //    config.AppSettings.Settings.Add(xForm.Name, String.Format("{0};{1};{2};{3}", xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height));
            //config.Save(ConfigurationSaveMode.Full);
           
            
            //e.Cancel = true;
            //this.Hide();
            //if ((ModifierKeys & Keys.Shift) == 0)
            //{
            //    Point location = Location;
            //    Size size = Size;
            //    if (WindowState != FormWindowState.Normal)
            //    {
            //        location = RestoreBounds.Location;
            //        size = RestoreBounds.Size;
            //    }
            //    string initLocation = string.Join(",", location.X, location.Y, size.Width, size.Height);
            //    Properties.Settings.Default.InitialLocation = initLocation;
            //    Properties.Settings.Default.Save();
            //}

        }
   
        public  void Fillqty_ingrd(byte[] buffer)
        {
            try
            {
                object ob = new object();
                lock (ob)
                {

                    var obj = (MS_TRADE_CONFIRM_TR)DataPacket.RawDeserialize(buffer, typeof(MS_TRADE_CONFIRM_TR));


                    DataGridViewRow row = DGV1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Token1"].Value.ToString().Equals(Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))).FirstOrDefault();
                    if (row == null)
                    {                        
                      //  LogWriterClass.logwritercls.logs("Qtyfiletokenno", IPAddress.HostToNetworkOrder(obj.Token).ToString());
                        return;
                    }
                    string _neartoken = DGV1.Rows[row.Index].Cells["Token1"].Value.ToString();
                    string _fartoken = DGV1.Rows[row.Index].Cells["Token2"].Value.ToString();
                    if (_neartoken == Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))
                    {
                        if (IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator) == 1)
                        {
                            if (DGV1.Rows[row.Index].Cells["BNSFTQ"].Value == null || Convert.ToString(DGV1.Rows[row.Index].Cells["BNSFTQ"].Value) == "")
                            {
                                DGV1.Rows[row.Index].Cells["BNSFTQ"].Value = 0;
                            }
                            DGV1.Rows[row.Index].Cells["BNSFTQ"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["BNSFTQ"].Value) + 1;
                        }
                        else
                        {
                            if (DGV1.Rows[row.Index].Cells["BFSNTQ"].Value == null || Convert.ToString(DGV1.Rows[row.Index].Cells["BFSNTQ"].Value) == "")
                            {
                                DGV1.Rows[row.Index].Cells["BFSNTQ"].Value = 0;
                            }
                            DGV1.Rows[row.Index].Cells["BFSNTQ"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["BFSNTQ"].Value) + 1;

                        }
                    }
                    else if (_fartoken == Convert.ToString(IPAddress.HostToNetworkOrder(obj.Token)))
                    {
                        if(IPAddress.HostToNetworkOrder(obj.Buy_SellIndicator) == 1)
                        { 
                        if (DGV1.Rows[row.Index].Cells["BFSNTQ"].Value == null || Convert.ToString(DGV1.Rows[row.Index].Cells["BFSNTQ"].Value) == "")
                        {
                            DGV1.Rows[row.Index].Cells["BFSNTQ"].Value = 0;
                        }

                        DGV1.Rows[row.Index].Cells["BFSNTQ"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["BFSNTQ"].Value) + 1;
                        }
                        else
                        {
                            if (DGV1.Rows[row.Index].Cells["BNSFTQ"].Value == null || Convert.ToString(DGV1.Rows[row.Index].Cells["BNSFTQ"].Value) == "")
                            {
                                DGV1.Rows[row.Index].Cells["BNSFTQ"].Value = 0;
                            }
                            DGV1.Rows[row.Index].Cells["BNSFTQ"].Value = Convert.ToInt32(DGV1.Rows[row.Index].Cells["BNSFTQ"].Value) + 1;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Client.LogWriterClass.logwritercls.logs("ErrorValue_check.txt", "Value Check update in gridview" + ex.Message);

            }
        }

        //private void DGV1_KeyDown(object sender, KeyEventArgs e)  
        //{
        //    if (e.Modifiers == Keys.Shift)
        //    {
        //        if (e.KeyCode == Keys.F2)
        //        {
        //            using (frmDiff _frmDIff = new frmDiff())
        //            { 
                        
        //              //_frmDIff._FOPairDiff.TokenNear = DGV1.SelectedRows[]
       


        //                _frmDIff._FOPairDiff.BFSNDIFF = Convert.ToDouble(Convert.IsDBNull(DGV1.SelectedRows[0].Cells["BFSNDIFF"].Value) ? 0 : DGV1.SelectedRows[0].Cells["BFSNDIFF"].Value);
        //                _frmDIff._FOPairDiff.BNSFDIFF = Convert.ToDouble(Convert.IsDBNull(DGV1.SelectedRows[0].Cells["BNSFDIFF"].Value) ? 0 : DGV1.SelectedRows[0].Cells["BNSFDIFF"].Value);
        //                _frmDIff._FOPairDiff.MINQTY = Convert.ToInt32(Convert.IsDBNull(DGV1.SelectedRows[0].Cells["MINQTY"].Value) ? 0 : DGV1.SelectedRows[0].Cells["MINQTY"].Value); 
        //                _frmDIff._FOPairDiff.MAXQTY=  Convert.ToInt32( Convert.IsDBNull(DGV1.SelectedRows[0].Cells["MAXQTY"].Value)  ? 0 :DGV1.SelectedRows[0].Cells["MAXQTY"].Value);


        //                if (_frmDIff.ShowDialog() == DialogResult.OK)
        //                {
        //                    DGV1.SelectedRows[0].Cells["BFSNDIFF"].Value = _frmDIff._FOPairDiff.BFSNDIFF;
        //                    DGV1.SelectedRows[0].Cells["BNSFDIFF"].Value = _frmDIff._FOPairDiff.BNSFDIFF;
        //                    DGV1.SelectedRows[0].Cells["MINQTY"].Value = _frmDIff._FOPairDiff.MINQTY;
        //                    DGV1.SelectedRows[0].Cells["MAXQTY"].Value = _frmDIff._FOPairDiff.MAXQTY;


        //                    _frmDIff._FOPairDiff.PORTFOLIONAME =Convert.ToInt32( DGV1.SelectedRows[0].Cells["PF"].Value);
        //                    _frmDIff._FOPairDiff.TokenNear =Convert.ToInt32( DGV1.SelectedRows[0].Cells["Token1"].Value);
        //                    _frmDIff._FOPairDiff.TokenFar =Convert.ToInt32( DGV1.SelectedRows[0].Cells["Token2"].Value);

        //                    byte[] buffer = DataPacket.RawSerialize(_frmDIff._FOPairDiff);
        //                    NNFHandler.Instance.Publisher("FOPAIRDIFF", buffer);

        //                }

        //            }
        //        }
        //    }

        //}

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
                SpreadTable = (DataTable)DGV1.DataSource;
                savd.DefaultExt = ".xml";
                SpreadTable.WriteXml(savd.FileName );
             //   SpreadTable.WriteXml(savd.FileName);
            //SpreadTable.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + "FOWATCH.xml");
            }

            DataTable dt_save = new DataTable("lastvalue");
            dt_save.Columns.Add("BNSFDIFF", typeof(Double));
            dt_save.Columns.Add("BFSNDIFF", typeof(Double));
            dt_save.Columns.Add("BNSFMNQ", typeof(int));
            dt_save.Columns.Add("BFSNMNQ", typeof(int));
            dt_save.Columns.Add("BNSFMXQ", typeof(int));
            dt_save.Columns.Add("BFSNMXQ", typeof(int));
            dt_save.Columns.Add("TICKS", typeof(int));

            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataRow dRow = dt_save.NewRow();
                dRow["BNSFDIFF"] = row.Cells["BNSFDIFF"].Value;
                dRow["BFSNDIFF"] = row.Cells["BFSNDIFF"].Value;
                dRow["BNSFMNQ"] = row.Cells["BNSFMNQ"].Value;
                dRow["BFSNMNQ"] = row.Cells["BFSNMNQ"].Value;
                dRow["BNSFMXQ"] = row.Cells["BNSFMXQ"].Value;
                dRow["BFSNMXQ"] = row.Cells["BFSNMXQ"].Value;
                dRow["TICKS"] = row.Cells["TICKS"].Value;
                dt_save.Rows.Add(dRow);
            }
             dt_save.WriteXml(Application.StartupPath+Path.DirectorySeparatorChar+"Lastvalue.xml");
           
        }

        private void btnLoadMktWatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();

            opn.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            if(opn.ShowDialog() == DialogResult.OK)
            {
                SpreadTable.Clear();
                DataSet ds_set = new DataSet();
                ds_set.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Lastvalue.xml");
                SpreadTable.ReadXml(opn.FileName);
          //  SpreadTable.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "FOWATCH.xml");
                for (int i = 0; i < ds_set.Tables[0].Rows.Count; i++)
                {
                    DGV1.Rows[i].Cells["BNSFDIFF"].Value = ds_set.Tables[0].Rows[i]["BNSFDIFF"];
                    DGV1.Rows[i].Cells["BFSNDIFF"].Value = ds_set.Tables[0].Rows[i]["BFSNDIFF"];
                    DGV1.Rows[i].Cells["BNSFMNQ"].Value = ds_set.Tables[0].Rows[i]["BNSFMNQ"];
                    DGV1.Rows[i].Cells["BFSNMNQ"].Value = ds_set.Tables[0].Rows[i]["BFSNMNQ"];
                    DGV1.Rows[i].Cells["BNSFMXQ"].Value = ds_set.Tables[0].Rows[i]["BNSFMXQ"];
                    DGV1.Rows[i].Cells["BFSNMXQ"].Value = ds_set.Tables[0].Rows[i]["BFSNMXQ"];
                    DGV1.Rows[i].Cells["TICKS"].Value = ds_set.Tables[0].Rows[i]["TICKS"];
                }
            }

            _SelectionOut _objOut = new _SelectionOut();

            for (int i = 0; i <SpreadTable.Rows.Count ; i++)
            {
                UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
                UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());
                portFolioCounter++;

            }
            if(DGV1.Rows.Count==0)
            { return; }
            portFolioCounter  = Convert.ToInt32( SpreadTable.Compute("MAX(PF)", ""))+1;          
        }
        public static int[] LoadFormLocationAndSize(Form xForm)
        {
            int[] t = { 0, 0, 900, 300 };
            if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "formclose.xml"))
                return t;
            DataSet dset = new DataSet();
            dset.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "formclose.xml");
            int[] LocationAndSize = new int[] { xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height };
           
            try
            {              
                var AbbA =   dset.Tables[0].Rows[0]["Input"].ToString().Split(';');
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
            return LocationAndSize;
        }

        public static void SaveFormLocationAndSize(object sender, FormClosingEventArgs e)
        { 
            Form xForm = sender as Form;
         //   ini.IniWriteValue("FOFOFORM","Location", String.Format("{0};{1};{2};{3}", xForm.Location.X, xForm.Location.Y, xForm.Size.Width, xForm.Size.Height));

            var settings = new XmlWriterSettings { Indent = true };

            XmlWriter writer = XmlWriter.Create(Application.StartupPath + Path.DirectorySeparatorChar + "formclose.xml", settings);

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
          
            txt.Visible = false;
            _makeItRed = new DataGridViewCellStyle();
            _makeItBlue = new DataGridViewCellStyle(); 
            _makeItBlack = new DataGridViewCellStyle();

            _makeItRed.BackColor = Color.Red;

            _makeItBlue.BackColor = Color.Blue;
            _makeItBlack.BackColor = Color.Black;
          SpreadTable = new DataTable("SPREADFO");
         

            SpreadTable.Columns.Add("PF", typeof(String));
            SpreadTable.Columns.Add("Token1", typeof(Int32));
            SpreadTable.Columns.Add("Token2", typeof(Int32));
            SpreadTable.Columns.Add("NEAR", typeof(String));
            SpreadTable.Columns.Add("FAR", typeof(String));
            SpreadTable.Columns.Add("NBID", typeof(Double));
            SpreadTable.Columns.Add("NASK", typeof(Double));
            SpreadTable.Columns.Add("NLTP", typeof(Double));
            SpreadTable.Columns.Add("FBID", typeof(Double));
            SpreadTable.Columns.Add("FASK", typeof(Double));
            SpreadTable.Columns.Add("FLTP", typeof(Double));

            SpreadTable.Columns.Add("NBD", typeof(Double), "FASK-NASK");
            SpreadTable.Columns.Add("NHD", typeof(Double), "FBID -NASK");
            SpreadTable.Columns.Add("FBD", typeof(Double), "FBID-NBID");
            SpreadTable.Columns.Add("FHD", typeof(Double), "FASK-NBID");
           
            DGV1.DataSource = SpreadTable;
        

            DGV1.Columns["Token1"].Visible = false;
           
            DGV1.Columns["Token2"].Visible = false;

            DGV1.Columns["FAR"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NEAR"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NBID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["NBID"].SortMode = DataGridViewColumnSortMode.NotSortable;

            DGV1.Columns["PF"].SortMode = DataGridViewColumnSortMode.NotSortable;
            DGV1.Columns["FLTP"].SortMode = DataGridViewColumnSortMode.NotSortable;
             DGV1.Columns["NBD"].SortMode = DataGridViewColumnSortMode.NotSortable;
             DGV1.Columns["FBD"].SortMode = DataGridViewColumnSortMode.NotSortable;

             DGV1.Columns["NHD"].SortMode = DataGridViewColumnSortMode.NotSortable;
             DGV1.Columns["FHD"].SortMode = DataGridViewColumnSortMode.NotSortable;

             DGV1.Columns["Token1"].SortMode = DataGridViewColumnSortMode.NotSortable;
             DGV1.Columns["Token2"].SortMode = DataGridViewColumnSortMode.NotSortable;


            SetDisplayRules(this.DGV1.Columns["PF"], "PF");

            SetDisplayRules(this.DGV1.Columns["NEAR"], "NEAR");


            SetDisplayRules(this.DGV1.Columns["FAR"], "FAR");
            
            SetDisplayRules(this.DGV1.Columns["NBID"], "N BID");
            SetDisplayRules(this.DGV1.Columns["NASK"], "N ASK");
            
            SetDisplayRules(this.DGV1.Columns["NLTP"], "N LTP"); 
            SetDisplayRules(this.DGV1.Columns["FBID"], "F BID");
       
            SetDisplayRules(this.DGV1.Columns["FASK"], "F ASK");   // Token2Ask
            SetDisplayRules(this.DGV1.Columns["FLTP"], "F LTP");   // Token2Ltp

            SetDisplayRules(this.DGV1.Columns["NBD"], "NBD");   //NearBidDiff
            SetDisplayRules(this.DGV1.Columns["NHD"], "NHD");   // NearHitDiff
          
            SetDisplayRules(this.DGV1.Columns["FBD"], "FBD");   // FarBidDiff
            SetDisplayRules(this.DGV1.Columns["FHD"], "FHD");   //FarHitDiff

            this.DGV1.Columns["NBD"].DefaultCellStyle.Format = "0.0000##";
            this.DGV1.Columns["NHD"].DefaultCellStyle.Format = "0.0000##";
            this.DGV1.Columns["FBD"].DefaultCellStyle.Format = "0.0000##";
            this.DGV1.Columns["FHD"].DefaultCellStyle.Format = "0.0000##";

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BNSFDIFF",
                HeaderText = "BNSFDIFF",               

            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BFSNDIFF",
                HeaderText = "BFSNDIFF",

            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BNSFMNQ",
                HeaderText = "BNSFMNQ",
            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BFSNMNQ",
                HeaderText = "BFSNMNQ",
            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BNSFMXQ",
                HeaderText = "BNSFMXQ",
            });
            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {       
                Name = "BFSNMXQ",                
                HeaderText = "BFSNMXQ",
            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TICKS",
                HeaderText = "TICKS",
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
                Name = "BFSNTQ",
                HeaderText = "BFSNTQ",
                ReadOnly = true
            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BNSFTQ",
                HeaderText = "BNSFTQ",
                ReadOnly = true
            });
           this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BFSNTD",
                HeaderText = "BFSNTD",
                ReadOnly = true
            });

            this.DGV1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BNSFTD",
                HeaderText = "BNSFTD",
                ReadOnly = true
            });
            DGV1.Columns["BNSFDIFF"].DefaultCellStyle.NullValue = 0.00;
            DGV1.Columns["BFSNDIFF"].DefaultCellStyle.NullValue = 0.00;
            DGV1.Columns["BNSFMNQ"].DefaultCellStyle.NullValue = 0.00;
            DGV1.Columns["BFSNMNQ"].DefaultCellStyle.NullValue = 0.00;
            DGV1.Columns["BNSFMXQ"].DefaultCellStyle.NullValue = 0.00;
            DGV1.Columns["BFSNMXQ"].DefaultCellStyle.NullValue = 0.000;
            DGV1.Columns["TICKS"].DefaultCellStyle.NullValue = 0;

            this.DGV1.Columns["BNSFTD"].DefaultCellStyle.Format = "0.##";
            this.DGV1.Columns["BFSNTD"].DefaultCellStyle.Format = "0.##";

            _makeItRed = new DataGridViewCellStyle();
            _makeItBlue = new DataGridViewCellStyle();
            _makeItBlack = new DataGridViewCellStyle();

            _makeItRed.BackColor = Color.Red;
            _makeItBlue.BackColor = Color.Blue;
            _makeItBlack.BackColor = Color.Black;
            SpreadTable.TableNewRow += new DataTableNewRowEventHandler(SpreadTable_NewRow);
         //   NNFHandler.eOrderTRADE_ERROR += Fillqty_ingrd;  

            btnStopAll.Enabled = true;
            btnStartAll.Enabled = true;
            DGV1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        
            Type controlType = DGV1.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(DGV1, true, null);

            try
            {
                foreach (DataGridViewColumn dc in DGV1.Columns)
                {

                    this.DGV1.Columns[dc.HeaderText.Replace(" ", "")].Visible = true;
                }

                DataSet ds = new DataSet();
                if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MarketCol.xml"))
                {
                    ds.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MarketCol.xml");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string st = ds.Tables[0].Rows[i]["Input"].ToString();
                        this.DGV1.Columns[ds.Tables[0].Rows[i]["Input"].ToString().Replace(" ", "")].Visible = false;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Defauft Profile Not Create" , "Error");
            }
            Task.Factory.StartNew(() =>
            {

                //Thread.Sleep(100);



                if (this.InvokeRequired)
                {
                    this.BeginInvoke((ThreadStart)delegate() { defaultLoadfun(); });

                    return;
                }
                else
                {
                    defaultLoadfun();
                }


            });
        }
         private void defaultLoadfun()
        {

            if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "FOFODefault.xml"))
            {
               
                SpreadTable.Clear();
                DataSet ds_set = new DataSet();
                ds_set.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "FOFO.xml");
                SpreadTable.ReadXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "FOFODefault.xml");
                 for (int i = 0; i < ds_set.Tables[0].Rows.Count; i++)
                {
                    DGV1.Rows[i].Cells["BNSFDIFF"].Value = ds_set.Tables[0].Rows[i]["BNSFDIFF"];
                    DGV1.Rows[i].Cells["BFSNDIFF"].Value = ds_set.Tables[0].Rows[i]["BFSNDIFF"];
                    DGV1.Rows[i].Cells["BNSFMNQ"].Value = ds_set.Tables[0].Rows[i]["BNSFMNQ"];
                    DGV1.Rows[i].Cells["BFSNMNQ"].Value = ds_set.Tables[0].Rows[i]["BFSNMNQ"];
                    DGV1.Rows[i].Cells["BNSFMXQ"].Value = ds_set.Tables[0].Rows[i]["BNSFMXQ"];
                    DGV1.Rows[i].Cells["BFSNMXQ"].Value = ds_set.Tables[0].Rows[i]["BFSNMXQ"];
                    DGV1.Rows[i].Cells["TICKS"].Value = ds_set.Tables[0].Rows[i]["TICKS"];
                  
                }

                for (int i = 0; i < SpreadTable.Rows.Count; i++)
                {
                    UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token1"].ToString());
                    UDP_Reciever.Instance.Subscribe = Convert.ToInt32(SpreadTable.Rows[i]["Token2"].ToString());
                    portFolioCounter++;

                }
                if (DGV1.Rows.Count == 0)
                { return; }
                portFolioCounter = Convert.ToInt32(SpreadTable.Compute("MAX(PF)", "")) + 1;
            }
         }

        private void DGV1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
           
        }

        private void DGV1_CellClick(object sender, DataGridViewCellEventArgs e)
        {        
            btnApply_Click(e.RowIndex, e.ColumnIndex);
        }


        private void btnApply_Click(int RowIndex, int ColumnIndex)
        {
            if (RowIndex <= -1)
                return;


            if (DGV1.Rows[RowIndex].Cells[ColumnIndex] is DataGridViewButtonCell)
            {
                using (frmDiff _frmDIff = new frmDiff())
                {
                    _frmDIff._FOPairDiff.BFSNDIFF = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BFSNDIFF"].Value);
                    _frmDIff._FOPairDiff.BNSFDIFF = Convert.ToDouble(DGV1.Rows[RowIndex].Cells["BNSFDIFF"].Value);

                    _frmDIff._FOPairDiff.BNSFMNQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["BNSFMNQ"].Value);
                    _frmDIff._FOPairDiff.BFSNMNQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["BFSNMNQ"].Value);

                    _frmDIff._FOPairDiff.BNSFMXQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["BNSFMXQ"].Value);
                    _frmDIff._FOPairDiff.BFSNMXQ = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["BFSNMXQ"].Value);

                    _frmDIff._FOPairDiff.PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["PF"].Value);
                    _frmDIff._FOPairDiff.TokenNear = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token1"].Value);
                    _frmDIff._FOPairDiff.TokenFar = Convert.ToInt32(DGV1.Rows[RowIndex].Cells["Token2"].Value);
                    if (Convert.ToDouble(DGV1.Rows[RowIndex].Cells["TICKS"].Value) %1 != 0)
                    {
                        MessageBox.Show("Please insert valid value");
                        return;
                    }

                    _frmDIff._FOPairDiff.TickCount =(int)Math.Round(Convert.ToDouble(DGV1.Rows[RowIndex].Cells["TICKS"].Value));
                    byte[] buffer = DataPacket.RawSerialize(_frmDIff._FOPairDiff);
                    NNFHandler.Instance.Publisher( MessageType.FOPAIRDIFF , buffer);
                     Task.Factory.StartNew(() => applyFun());
                }
            }
        }

        private void applyFun()
        {
            if (DGV1.Rows.Count == 0)
                return;
            SpreadTable = (DataTable)DGV1.DataSource;
            SpreadTable.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "FOFODefault.xml");
            DataTable dt_save = new DataTable("lastvalue");
            dt_save.Columns.Add("BNSFDIFF", typeof(Double));
            dt_save.Columns.Add("BFSNDIFF", typeof(Double));
            dt_save.Columns.Add("BNSFMNQ", typeof(int));
            dt_save.Columns.Add("BFSNMNQ", typeof(int));
            dt_save.Columns.Add("BNSFMXQ", typeof(int));
            dt_save.Columns.Add("BFSNMXQ", typeof(int));
            dt_save.Columns.Add("TICKS", typeof(int));
            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataRow dRow = dt_save.NewRow();
                dRow["BNSFDIFF"] = row.Cells["BNSFDIFF"].Value == null ? 0 : row.Cells["BNSFDIFF"].Value;
                dRow["BFSNDIFF"] = row.Cells["BFSNDIFF"].Value == null ? 0 : row.Cells["BFSNDIFF"].Value;
                dRow["BNSFMNQ"] = row.Cells["BNSFMNQ"].Value == null ? 0 : row.Cells["BNSFMNQ"].Value;
                dRow["BFSNMNQ"] = row.Cells["BFSNMNQ"].Value == null ? 0 : row.Cells["BFSNMNQ"].Value;
                dRow["BNSFMXQ"] = row.Cells["BNSFMXQ"].Value == null ? 0 : row.Cells["BNSFMXQ"].Value;
                dRow["BFSNMXQ"] = row.Cells["BFSNMXQ"].Value == null ? 0 : row.Cells["BFSNMXQ"].Value;
                dRow["TICKS"] = row.Cells["TICKS"].Value == null || row.Cells["TICKS"].Value == DBNull.Value ? 0 :(int)Math.Round(Convert.ToDouble(row.Cells["TICKS"].Value));
                dt_save.Rows.Add(dRow);
            }


            dt_save.WriteXml(Application.StartupPath + Path.DirectorySeparatorChar + System.DateTime.Now.Date.ToString("dddd, MMMM d, yyyy") + "FOFO.xml");
        }

        private void btnStartAll_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in DGV1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
            }

            btnStopAll.Enabled = true;
            btnStartAll.Enabled = false;
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow VARIABLE in DGV1.Rows)
            {
                DataGridViewCheckBoxCell cb = (VARIABLE.Cells["Enable"]) as DataGridViewCheckBoxCell;
                
                cb.Value = false;
            }
            btnStopAll.Enabled = false;
            btnStartAll.Enabled = true;
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
        private void DGV1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DGV1.IsCurrentCellDirty)
            {
              
              DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1) return;

            

            if (DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
            DataGridViewCheckBoxCell checkCell =(DataGridViewCheckBoxCell)DGV1.Rows[e.RowIndex].Cells["Enable"];

            FOPAIR v;

            if ((bool) checkCell.Value == true)
            {
               
                byte[] buffer = DataPacket.RawSerialize(v=new FOPAIR()
                {
                    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                    TokenNear = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                    TokenFar = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value)
                });
                Global.Instance.Fopairbool = true;
                NNFHandler.Instance.Publisher(MessageType.FOPAIR, buffer);

                if (Holder._DictLotSize.ContainsKey(v.TokenNear) == false || v.TokenNear != 0)
                {
                    Holder._DictLotSize.TryAdd(v.TokenNear, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenNear).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }

                if (Holder._DictLotSize.ContainsKey(v.TokenFar) == false || v.TokenFar != 0)
                {
                    Holder._DictLotSize.TryAdd(v.TokenFar, new Csv_Struct()
                    {
                        lotsize = CSV_Class.cimlist.Where(q => q.Token == v.TokenFar).Select(a => a.BoardLotQuantity).First()
                    }
                    );
                }

                
            }
            else
            {
                byte[] buffer = DataPacket.RawSerialize(v = new FOPAIR()
                {
                    PORTFOLIONAME = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["PF"].Value),
                    TokenNear = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token1"].Value),
                    TokenFar = Convert.ToInt32(DGV1.Rows[e.RowIndex].Cells["Token2"].Value)
                });
                Global.Instance.Fopairbool = false;
                NNFHandler.Instance.Publisher( MessageType.FOPAIRUNSUB, buffer);

                //if (Holder._DictLotSize.ContainsKey(v.TokenNear) == false || v.TokenNear != 0)
                //{
                //    Csv_Struct o = new Csv_Struct();
                //    Holder._DictLotSize.TryRemove(v.TokenNear, out o);                   
                //}

                //if (Holder._DictLotSize.ContainsKey(v.TokenFar) == false || v.TokenFar != 0)
                //{
                //    Csv_Struct o = new Csv_Struct();
                //    Holder._DictLotSize.TryRemove(v.TokenFar, out o);
                //}
            }
            
            }
        }

        private void DGV1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==  Keys.Delete)
            {
              //  --portFolioCounter;
                DGV1.Rows.RemoveAt(DGV1.SelectedRows[0].Index);

                DataTable dt = SpreadTable;
            }
          else  if (e.KeyCode == Keys.Enter)
            {

                DataGridView _dgLoc = sender as DataGridView;

                if (_dgLoc.CurrentCell.EditedFormattedValue.ToString() == "Apply")
                {
                  btnApply_Click(_dgLoc.CurrentRow.HeaderCell.RowIndex, _dgLoc.CurrentRow.Cells["Apply"].ColumnIndex);
                }
            }
        }

        private void DGV1_SelectionChanged(object sender, EventArgs e)
        {
          //  DGV1.ClearSelection();
        }

        private void DGV1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (!this.valueChanged)
            {             
                this.valueChanged = true;
                this.DGV1.NotifyCurrentCellDirty(true);
            }
        }
        private void DGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)// "BNSFDIFF")
            {                
                txt.Show();
                strv = e.ColumnIndex.ToString() + "," + e.RowIndex.ToString();
                txt.Text =Convert.ToString( DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                DGV1.Controls.Add(txt);
                txt.Location = this.DGV1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                                        
                txt.Width = DGV1.Columns[0].Width;
                txt.Focus();             
            }           
        }
       

        private void DGV1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)// "BNSFDIFF")
            {
                if (Information.IsNumeric(txt.Text) == true)
                DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = txt.Text;
                txt.Focus();
                txt.Hide(); 
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
        }

        private void txt_MouseClick(object sender, MouseEventArgs e)
        {
            txt.Focus();
        }

        private void DGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (!Information.IsNumeric(txt.Text) && Convert.ToDouble(txt.Text) % 1 != 0)
            {

                if (txt.Text.Length > 1)
                {
                    MessageBox.Show("Please Insert Numeric Value", "Information");
                    txt.Clear();
                    txt.Text = "0";
                }
                else
                {
                  txt.Focus();
                }


            }
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            if (!Information.IsNumeric(txt.Text) && Convert.ToDouble(txt.Text)%1!=0)
            {

                if (txt.Text.Length > 0)
                {
                    MessageBox.Show("Please Insert Numeric Value", "Information");
                    txt.Clear();
                    txt.Text = "0";
                }
                else
                    txt.Focus();
            }
        }
    }

    public class Data1
    {
        public int Tok1;
        public int Tok2;
        public int pricediff;

    }
}


/*
DataRow[] drow = SpreadTable.Select("PF = '"+DGV1.SelectedRows[0].Cells["PF"].Value+"'");
                drow[0]["BFSNDIFF"] = DGV1.SelectedRows[0].Cells["BFSNDIFF"].Value;
                drow[0]["BFSNDIFF"] = DGV1.SelectedRows[0].Cells["BFSNDIFF"].Value;

                drow[0]["BNSFMNQ"] = DGV1.SelectedRows[0].Cells["BNSFMNQ"].Value;
                drow[0]["BFSNMNQ"] = DGV1.SelectedRows[0].Cells["BFSNMNQ"].Value;

                drow[0]["BNSFMXQ"] = DGV1.SelectedRows[0].Cells["BNSFMXQ"].Value;
                drow[0]["BFSNMXQ"] = DGV1.SelectedRows[0].Cells["BFSNMXQ"].Value;
*/