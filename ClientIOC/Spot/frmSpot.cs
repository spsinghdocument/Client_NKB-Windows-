using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Client.Spot
{
    public partial class frmSpot : Form
    {
        DataView dvSpotWatch;
        public frmSpot()
        {
            InitializeComponent();
        }

        private void frmSpot_Load(object sender, EventArgs e)
        {
            dvSpotWatch = new DataView(Client.Spread.CommonData.dtSpotWatch);

           
            dgvMktWatch.BindSourceView = dvSpotWatch;

           Global.Instance.cashDataSection.OSpotnIndexChange += SpotTableMethods.UpdateRecord;

        }

        private void frmSpot_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Instance.cashDataSection.OSpotnIndexChange -= SpotTableMethods.UpdateRecord;
        }

        private void dgvMktWatch_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                dgvMktWatch.PerformLayout();
                if (dgvMktWatch.InvokeRequired)
                {
                    dgvMktWatch.Invoke(new On_DataPaintdDelegate(dgvMktWatch_RowPrePaint), sender, e);
                    return;
                }
                if (Convert.ToString(dgvMktWatch.Rows[e.RowIndex].Cells["Indicator"].Value) == "+")
                {
                    //  DGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    dgvMktWatch.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {

                    dgvMktWatch.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
