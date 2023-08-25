using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public partial class frmSampleStatus : Form
    {
        public List<Protocol> AllProtocols { get; set; } = new List<Protocol>();
        public string CurUser { get; set; } = "";
        public frmSampleStatus()
        {
            InitializeComponent();
        }

        private void frmSampleStatus_Load(object sender, EventArgs e)
        {
            RepoSQL sqlService = new RepoSQL();
            List<Janus> allJanus = new List<Janus>();
            List<string> depts = new List<string>();
            //List<Protocol> allProtocols = new List<Protocol>();
            string[] dbtest;
            List<string> allDBtests = new List<string>();

            try
            {
                allJanus = sqlService.GetJanus("ALL");
                if (allJanus != null && allJanus.Count > 0)
                {
                    depts = allJanus.Select(s => s.Department).Distinct().ToList();
                    cbDept.Items.Add("");
                    cbDept.Items.AddRange(depts.ToArray());
                }

                ////allProtocols= sqlService.GetProtocols();
                //if (AllProtocols != null && AllProtocols.Count > 0)
                //{
                //    foreach (var prot in AllProtocols)
                //    {
                //        dbtest = prot.DBTest.Split('|');
                //        allDBtests.AddRange(dbtest);
                //    }
                //    allDBtests = allDBtests.Distinct().ToList();
                //    cklDbTests.Items.AddRange(allDBtests.ToArray());
                //}
            }
            catch (Exception ex)
            {
                string errMsg = "btnSampleStatus_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selDept = "";
            string[] dbtest;
            List<string> allDBtests = new List<string>();

            try
            {
                cklDbTests.Items.Clear();

                selDept = cbDept.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(selDept) && AllProtocols != null && AllProtocols.Count > 0)
                {
                    var deptDBTest = AllProtocols.Where(p => p.Department == selDept).ToList();
                    if (deptDBTest != null && deptDBTest.Count > 0)
                    {
                        foreach (var prot in deptDBTest)
                        {
                            dbtest = prot.DBTest.Split('|');
                            allDBtests.AddRange(dbtest);
                        }
                        allDBtests = allDBtests.Distinct().ToList();

                        cklDbTests.Items.AddRange(allDBtests.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "cbDept_SelectedIndexChanged() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                GetRejectSamples();


            }
            catch (Exception ex)
            {
                string errMsg = "frmSampleStatus.btnGo_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void GetRejectSamples()
        {
            string dept = "";
            string dbtests = "";
            RepoSQL sqlService = new RepoSQL();
            DeptSample dptSample = new DeptSample();

            List<PlateSample> deptSamples = new List<PlateSample>();

            try
            {
                dgvSamples.Rows.Clear();

                dept = cbDept.SelectedItem.ToString();

                for (int i = 0; i < cklDbTests.Items.Count; i++)
                {
                    if (cklDbTests.GetItemChecked(i))
                    {
                        if (string.IsNullOrEmpty(dbtests))
                        {
                            dbtests = (string)cklDbTests.Items[i];
                        }
                        else
                        {
                            dbtests += "|" + (string)cklDbTests.Items[i];
                        }

                    }
                }

                dptSample.Status = "REJECT";
                dptSample.DateRangeMonths = 1;
                dptSample.DBTest = dbtests;
                dptSample.Dept = dept;


                deptSamples = sqlService.GetDeptSamples(dptSample);
                if (deptSamples != null && deptSamples.Count > 0)
                {
                    foreach (var smp in deptSamples)
                    {
                        dgvSamples.Rows.Add();
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["UnReject"].Value = false;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["SampleId"].Value = smp.SampleId;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["DBTest"].Value = smp.DBTest;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["ModifiedDate"].Value = smp.ModifiedDate;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["PlateId"].Value = smp.PlateId;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["PlateVersion"].Value = smp.PlateVersion;
                        dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Id"].Value = smp.Id;
                    }

                    btnUnReject.Enabled = true;
                    lblTolSamples.Text = "Total Sample(s): " + deptSamples.Count.ToString();
                }



            }
            catch (Exception ex)
            {
                string errMsg = "GetRejectSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }



        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUnReject_Click(object sender, EventArgs e)
        {
            string unrejectResult = "SUCCESS";
            RepoSQL sqlService = new RepoSQL();
            List<SampleStatus> rejectSamples = new List<SampleStatus>();
            SampleStatus smpStatus = new SampleStatus();
            bool isUnreject = false;

            try
            {

                if (dgvSamples != null && dgvSamples.RowCount > 0)
                {
                    foreach (DataGridViewRow srw in dgvSamples.Rows)
                    {
                        isUnreject = Convert.ToBoolean(srw.Cells["UnReject"].Value);
                        if (isUnreject)
                        {
                            smpStatus = new SampleStatus();
                            smpStatus.Status = "";
                            smpStatus.User = CurUser;
                            smpStatus.Id = srw.Cells["Id"].Value.ToString();
                            smpStatus.SampleId = srw.Cells["SampleId"].Value.ToString();
                            smpStatus.PlateId = srw.Cells["PlateId"].Value.ToString();
                            smpStatus.PlateVersion = srw.Cells["PlateVersion"].Value.ToString();

                            rejectSamples.Add(smpStatus);
                        }
                    }

                    if (rejectSamples != null && rejectSamples.Count > 0)
                    {
                        unrejectResult = sqlService.UpdateSampleStatus(rejectSamples);
                        if (unrejectResult == "SUCCESS")
                        {
                            lblMsg.Text = "All Un-Reject processed";
                            GetRejectSamples();
                        }
                        else
                        {
                            lblMsg.Text = "Processing Un-Reject met issues: " + unrejectResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnUnReject_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void cklDbTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasValue = false;

            if (cbDept.SelectedItem != null && !string.IsNullOrEmpty(cbDept.SelectedItem.ToString()))
            {
                for (int i = 0; i < cklDbTests.Items.Count; i++)
                {
                    if (cklDbTests.GetItemChecked(i))
                    {
                        hasValue = true;
                        break;
                    }
                }
            }

            if (hasValue)
            {
                btnGo.Enabled = true;
            }
            else
            {
                btnGo.Enabled = false;
            }
        }
    }
}
