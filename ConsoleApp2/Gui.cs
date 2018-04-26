using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;
using static Controller.CustomerController;
using static Controller.EventtController;
using static Controller.AttendingController;
using static Controller.AttendedController;
using static Controller.UtilsController;
using static Utilities.FeedbackHandler;
using static Utilities.ExceptionHandler;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Protocols;

namespace SilverApplesForm
{
    public partial class frmSilverApples : System.Windows.Forms.Form

    {
        private Customer lastCust = new Customer(null, null, null, null, null); //null;
        private Eventt lastEvent = new Eventt(null, null, 0, DateTime.MinValue); //null;
        SilverApples.SilverApples.Controller client = new SilverApples.SilverApples.Controller();

        public frmSilverApples()
        {
            InitializeComponent();
            FillEventEventComboBox();
            FillCustEventComboBox();
            FillEmplComboBox();
            FillEmplTableComboBox();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            UpdateCustSearch();
        }

        private void btnCustSearch_Click_1(object sender, EventArgs e)
        {
            Clear(txtMessage);
            ClearCustAddEvent();
            try
            {
                string sql = "SELECT * FROM Customer WHERE cPnr = '" + txtCustSearch.Text + "';";
                List<Customer> custList = GetAnything(sql, GetCustomer);
                if (CheckTextboxes(txtCustSearch))
                {
                    ShowMsg(GetMessage("NoInputPnr"));
                }
                else
                {
                    EnterCust(custList);
                    UpdateCustTables();
                    ShowMsg(GetMessage("CustFound"));
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
                ClearCustFields();
            }
        }

        private void btnCustCreate_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                if (CheckTextboxes(txtCustPnr, txtCustName, txtCustAddress, txtCustPhone, txtCustMail))
                    ShowMsg(GetMessage("FillAllFields"));
                else
                {
                    Customer c = new Customer(txtCustPnr.Text, txtCustName.Text, txtCustAddress.Text, txtCustPhone.Text, txtCustMail.Text);
                    int i = CreateUpdateDeleteAnything(c, CreateCustomer);
                    ShowMsg(GetMessage("CustCreate"));
                    UpdateCustSearch();
                    btnCustCreate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnCustUpdate_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Customer WHERE cPnr = '" + txtCustPnr.Text + "';";
                List<Customer> custList = GetAnything(sql, GetCustomer);
                Customer c = custList.First();
                if (CheckTextboxes(txtCustPnr, txtCustName, txtCustAddress, txtCustPhone, txtCustMail))
                    ShowMsg(GetMessage("FillAllFields"));
                else
                {
                    c = new Customer(txtCustPnr.Text, txtCustName.Text, txtCustAddress.Text, txtCustPhone.Text, txtCustMail.Text);
                    int i = CreateUpdateDeleteAnything(c, UpdateCustomer);
                    ShowMsg(GetMessage("CustUpdate"));
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnCustDelete_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Customer WHERE cPnr = '" + txtCustPnr.Text + "';";
                List<Customer> custList = GetAnything(sql, GetCustomer);
                Customer c = custList.First();

                DialogResult result = MessageBox.Show("You are about to delete a customer. Are you sure?", "Important Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sql = "SELECT * FROM Attending WHERE cPnr = '" + txtCustPnr.Text + "';";
                    List<Attending> attgList = GetAnything(sql, GetAttending);
                    foreach (Attending attg in attgList)
                    {
                        int i = CreateUpdateDeleteAnything(attg, DeleteAttendingCust);
                    }
                    sql = "SELECT * FROM Attended WHERE cPnr = '" + txtCustPnr.Text + "';";
                    List<Attended> attdList = GetAnything(sql, GetAttended);
                    foreach (Attended attd in attdList)
                    {
                        int i = CreateUpdateDeleteAnything(attd, DeleteAttendedCust);
                    }
                    int j = CreateUpdateDeleteAnything(c, DeleteCustomer);
                    ClearCustFields();
                    ShowMsg(GetMessage("CustDelete"));
                    UpdateCustSearch();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnCustClear_Click(object sender, EventArgs e)
        {
            ClearCustFields();
            Clear(txtMessage);
        }

        private void btnCustAdd_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sqlstrings = "SELECT * FROM Customer WHERE cPnr = '" + txtCustPnr.Text + "';"; // if customer wasn't searched, only entered pnr
                List<Customer> custList = GetAnything(sqlstrings, GetCustomer);
                EnterCust(custList);
                string sql = "SELECT * FROM Attending WHERE cPnr = '" + txtCustPnr.Text + "' AND eId = '" + cbCustEvent.Text + "';";
                List<Attending> attendingList = GetAnything(sql, GetAttending);
                if (CheckTextboxes(txtCustPnr) || cbCustEvent.Text == "" || (rbHasntPaid.Checked == false && rbHasPaid.Checked == false))
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else if ((!(CheckTextboxes(txtCustPnr)) && cbCustEvent.Text != "" && rbHasntPaid.Checked == true))
                {
                    Attending attg = new Attending(txtCustPnr.Text, cbCustEvent.Text, false);
                    int i = CreateUpdateDeleteAnything(attg, CreateAttending);
                    ShowMsg(GetMessage("CustAttgEvent"));
                    UpdateCustTables();
                    if (lastEvent.EId == cbCustEvent.Text)
                    {
                        UpdateEventTables();
                    }
                    ClearCustAddEvent();
                }
                else if ((!(CheckTextboxes(txtCustPnr)) && cbCustEvent.Text != "" && rbHasPaid.Checked == true))
                {
                    Attending attg = new Attending(txtCustPnr.Text, cbCustEvent.Text, true);
                    int i = CreateUpdateDeleteAnything(attg, CreateAttending);
                    ShowMsg(GetMessage("CustAttgEvent"));
                    UpdateCustTables();
                    if (lastEvent.EId == cbCustEvent.Text)
                    {
                        UpdateEventTables();
                    }
                    ClearCustAddEvent();
                }
                else
                    ShowMsg(GetMessage(""));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAttgUpdate_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Attending WHERE cPnr = '" + txtCustPnr.Text + "' AND eId = '" + cbCustEvent.Text + "';";
                List<Attending> attgList = GetAnything(sql, GetAttending);
                Attending a = GetAnything(sql, GetAttending).First();

                if (cbCustEvent.Text == "" || rbHasntPaid.Checked == false && rbHasPaid.Checked == false)
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else if (cbCustEvent.Text != "" && rbHasPaid.Checked == true)
                {
                    if (a.HasPaid == false)
                    {
                        a = new Attending(txtCustPnr.Text, cbCustEvent.Text, true);
                        int i = CreateUpdateDeleteAnything(a, UpdateAttending);
                        UpdateCustTables();
                        ShowMsg(GetMessage("CustPaid"));
                        if (lastEvent.EId != null)
                        {
                            UpdateEventTables();
                        }
                    }
                    else
                    {
                        ShowMsg(GetMessage("CustAlrPaid"));
                    }
                }
                else if (cbCustEvent.Text != "" && rbHasntPaid.Checked == true)
                {
                    if (a.HasPaid == true)
                    {
                        a = new Attending(txtCustPnr.Text, cbCustEvent.Text, false);
                        int i = CreateUpdateDeleteAnything(a, UpdateAttending);
                        UpdateCustTables();
                        ShowMsg(GetMessage("CustUnPaid"));
                        if (lastEvent.EId != null)
                        {
                            UpdateEventTables();
                        }
                    }
                    else
                    {
                        ShowMsg(GetMessage("CustNoPaid"));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void cbEventSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Eventt WHERE eId = '" + cbEventSearch.Text + "';";
                List<Eventt> eventList = GetAnything(sql, GetEventt);
                if (eventList.Count != 0)
                {
                    EnterEvent(eventList);
                    UpdateEventTables();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEventCreate_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Eventt WHERE eId = '" + txtEventId.Text + "';";
                List<Eventt> eventList = GetAnything(sql, GetEventt);
                if (CheckTextboxes(txtEventId, txtEventName, txtEventPrice))
                    ShowMsg(GetMessage("FillAllFields"));
                else
                {
                    Eventt ev = new Eventt(txtEventId.Text, txtEventName.Text, Int32.Parse(txtEventPrice.Text), new DateTime(dtpEventDate.Value.Year, dtpEventDate.Value.Month, dtpEventDate.Value.Day,
                        dtpEventTime.Value.Hour, dtpEventTime.Value.Minute, 0));
                    int i = CreateUpdateDeleteAnything(ev, CreateEventt);
                    ShowMsg(GetMessage("EventCreate"));
                    UpdateEventComboBoxes();
                    lastEvent = ev;
                    btnEventCreate.Enabled = false;
                    txtEventId.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEventUpdate_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Eventt WHERE eId = '" + txtEventId.Text + "';";
                List<Eventt> eventList = GetAnything(sql, GetEventt);
                Eventt ev = eventList.First();
                if (CheckTextboxes(txtEventId, txtEventName, txtEventPrice))
                    ShowMsg(GetMessage("FillAllFields"));
                else
                {
                    ev = new Eventt(txtEventId.Text, txtEventName.Text, Int32.Parse(txtEventPrice.Text), new DateTime(dtpEventDate.Value.Year, dtpEventDate.Value.Month, dtpEventDate.Value.Day,
                        dtpEventTime.Value.Hour, dtpEventTime.Value.Minute, 0));
                    int i = CreateUpdateDeleteAnything(ev, UpdateEventt);
                    ShowMsg(GetMessage("EventUpdate"));
                    UpdateEventComboBoxes();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }

        }

        private void btnEventtDelete_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string sql = "SELECT * FROM Eventt WHERE eId = '" + txtEventId.Text + "';";
                List<Eventt> eventtList = GetAnything(sql, GetEventt);
                Eventt ev = eventtList.First();

                DialogResult result = MessageBox.Show("You are about to delete an event. Are you sure?", "Important Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sql = "SELECT * FROM Attending where eId = '" + txtEventId.Text + "';";
                    List<Attending> attgList = GetAnything(sql, GetAttending);
                    foreach (Attending attg in attgList)
                    {
                        int i = CreateUpdateDeleteAnything(attg, DeleteAttendingEventt);
                    }
                    sql = "SELECT * FROM Attended where eId = '" + txtEventId.Text + "';";
                    List<Attended> attdList = GetAnything(sql, GetAttended);
                    foreach (Attended attd in attdList)
                    {
                        int i = CreateUpdateDeleteAnything(attd, DeleteAttendedEventt);
                    }
                    int j = CreateUpdateDeleteAnything(ev, DeleteEventt);
                    ClearEventFields();
                    ShowMsg(GetMessage("EventDelete"));
                    UpdateEventComboBoxes();
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEventClear_Click(object sender, EventArgs e)
        {
            ClearEventFields();
            Clear(txtMessage);
        }

        private void btnAddCert_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                if (CheckTextboxes(txtEventId) || dataGridViewAttgEvent.SelectedCells.Count <= 0)
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else if ((!CheckTextboxes(txtEventId)) && dataGridViewAttgEvent.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dataGridViewAttgEvent.SelectedCells[0].RowIndex;
                    DataGridViewRow row = dataGridViewAttgEvent.Rows[selectedRowIndex];
                    string cPnr = Convert.ToString(row.Cells["Personal Nr."].Value);
                    string sqlstringAttg = "SELECT * FROM Attending WHERE eId = '" + lastEvent.EId + "' AND cPnr = '" + cPnr + "';";
                    Attending a = GetAnything(sqlstringAttg, GetAttending).First();
                    CreateUpdateDeleteAnything(a, DeleteAttending);

                        if (a.HasPaid == true)
                        {
                            if (lastEvent.EDate < DateTime.Now)
                            {
                                //delete Attending
                                CreateUpdateDeleteAnything(a, DeleteAttending);

                                //add Attended
                                string sqlstringAttd = "SELECT * FROM Attending WHERE eId = '" + lastEvent.EId + "' AND cPnr = '" + cPnr + "';";
                                List<Attended> attdList = GetAnything(sqlstringAttd, GetAttended);
                                Attended attd = new Attended(cPnr, lastEvent.EId, true);
                                CreateUpdateDeleteAnything(attd, CreateAttended);
                                UpdateEventTables();
                                ShowMsg(GetMessage("CustAttdEvent"));
                                    if (lastCust.CPnr == cPnr)
                                    {
                                        UpdateCustTables();
                                    }
                            }
                            else
                            {
                                ShowMsg(GetMessage("NoEventYet"));
                            }
                        }
                        else
                        {
                            ShowMsg(GetMessage("CustNoPaid"));
                        }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllKeys_Click(object sender, EventArgs e)
        {
            try
            {
                string[][] outerList = client.GetAllKeys();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void cbEmployee_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedIndex != -1)
            {
                try
                {
                    UpdateCronusComboBoxes(cbTable);
                    string no = cbEmployee.SelectedItem.ToString();
                    string[][] outerList = client.GetEmplRelatives(no);
                    DataTable table = new DataTable();
                    string[] headers = outerList[0];
                    table = GetHeaders(table, headers);
                    table = FillTableWithHeaders(table, outerList);
                    dataGridViewERP.DataSource = table;
                    ShowMsg(GetMessage("EmplFound"));
                }
                catch (Exception ex)
                {
                    ShowMsg(HandleException(ex));
                }
            }
        }

        private void btnTopSick_Click_1(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetFirstNameTopSick();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllSick2004_Click_1(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetSick2004();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllConstraints_Click_1(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllTableConstraints();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllIndexes_Click_1(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllIndexes();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplColumnsOne_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllEmployeeColumnsOne();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplColumnsTwo_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllEmployeeColumnsTwo();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllTablesOne_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllTablesOne();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnAllTablesTwo_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateCronusComboBoxes(cbEmployee, cbTable);
                string[][] outerList = client.GetAllTablesTwo();
                DataTable table = new DataTable();
                string[] headers = outerList[0];
                table = GetHeaders(table, headers);
                table = FillTableWithHeaders(table, outerList);
                dataGridViewERP.DataSource = table;
                ShowMsg(GetMessage("TableDisplay"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void cbTables_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbTable.SelectedIndex != -1)
            {
                try
                {
                    UpdateCronusComboBoxes(cbEmployee);
                    string tablename = cbTable.SelectedItem.ToString();
                    DataTable table = new DataTable();
                    string[] headers = client.GetMetaDataColumnName(tablename);
                    string[][] outerList = client.GetAllInfo(tablename);
                    table = GetHeaders(table, headers);
                    table = FillTableWithoutHeaders(table, outerList);
                    dataGridViewERP.DataSource = table;
                    ShowMsg(GetMessage("TableDisplay"));
                }
                catch (Exception ex)
                {
                    ShowMsg(HandleException(ex));
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                txtOpenFile.Text = client.Search(ofd.FileName);

                if (txtOpenFile.Text != "")
                {
                    ShowMsg(GetMessage("FileOpen") + ofd.SafeFileName);
                    txtOpenFile.ReadOnly = true;
                }
                else
                {
                    ShowMsg(GetMessage("FileNotFound"));
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnWSSearch_Click(object sender, EventArgs e)
        {
            Clear(txtMessage);
            try
            {
                string[] s = client.GetCustomer(txtWSPnr.Text);
                if (txtWSPnr.Text == "")
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else if (s.Length > 0)
                {
                    DataTable table = new DataTable();
                    CustHeaders(table);
                    table.Rows.Add(s);
                    ShowMsg(GetMessage("CustFound"));
                    dataGridViewWSCust.DataSource = table;
                }
                else
                {
                    ShowMsg(GetMessage("MissingPnr"));
                    dataGridViewWSCust.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }


        private void btnWSFindAllCust_Click(object sender, EventArgs e)
        {
            Clear(txtMessage, txtWSPnr);
            try
            {
                DataTable table = new DataTable();
                CustHeaders(table);
                string[][] custList = client.GetCustomers();

                for (int i = 0; i < custList.Length; i++)
                {
                    string[] innerTemp = custList[i];
                    List<string> innerList = new List<string>();
                    for (int j = 0; j < innerTemp.Length; j++)
                    {
                        innerList.Add(innerTemp[j]);
                    }
                    string[] outerList = innerList.ToArray();
                    table.Rows.Add(outerList);
                }
                dataGridViewWSCust.DataSource = table;
                ShowMsg(GetMessage("AllCustFound"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplSearch_Click(object sender, EventArgs e)
        {
            Clear(txtEmplContractCode, txtEmplContractDescr);
            try
            {
                string[] emplList = client.GetEmplContract(txtEmplContractSearch.Text.ToUpper());
                txtEmplContractCode.Text = emplList[0];
                txtEmplContractDescr.Text =emplList[1];
                Clear(txtEmplContractSearch);
                ShowMsg(GetMessage("EmplContractFound"));
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckTextboxes(txtEmplContractCode, txtEmplContractDescr))
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else
                {
                    string[] list = client.GetEmplContract(txtEmplContractCode.Text);
                    if (list.Length == 0)
                    {
                        client.CreateEmplContract(txtEmplContractCode.Text.ToUpper(), txtEmplContractDescr.Text);
                        ShowMsg(GetMessage("EmplContractCreate"));
                    }
                    else
                    {
                        ShowMsg(GetMessage("EmplContractExist"));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckTextboxes(txtEmplContractCode, txtEmplContractDescr))
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else
                {
                    String[] emplList = client.GetEmplContract(txtEmplContractCode.Text);
                    Object o = emplList.First();
                    client.UpdateEmplContract(txtEmplContractCode.Text, txtEmplContractDescr.Text);
                    ShowMsg(GetMessage("EmplContractUpdate"));
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        private void btnEmplDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckTextboxes(txtEmplContractCode, txtEmplContractDescr))
                {
                    ShowMsg(GetMessage("FillAllFields"));
                }
                else
                {
                    string[] emplList = client.GetEmplContract(txtEmplContractCode.Text);
                    if (emplList.Length > 0)
                    {
                        DialogResult result = MessageBox.Show("You are about to delete an employment contract. Are you sure?", "Important Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            client.DeleteEmplContract(emplList[0]);
                            Clear(txtEmplContractCode, txtEmplContractDescr);
                            ShowMsg(GetMessage("EmplContractDelete"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        //------------------------------------------------------------------------------------------------------
        //Methods

        public void ClearCustFields()
        {
            Clear(txtCustPnr, txtCustName, txtCustPhone, txtCustAddress, txtCustMail, txtCustSearch);
            lastCust = new Customer(null, null, null, null, null);
            cbCustEvent.SelectedIndex = -1;
            rbHasntPaid.Checked = false;
            rbHasPaid.Checked = false;
            txtCustPnr.ReadOnly = false;
            dataGridViewAttgCust.DataSource = null;
            dataGridViewAttdCust.DataSource = null;

            btnCustCreate.Enabled = true;
            btnCustUpdate.Enabled = true;
            btnCustDelete.Enabled = true;
            btnCustClear.Enabled = true;

        }
        public void ClearCustAddEvent()
        {
            cbCustEvent.SelectedIndex = -1;
            rbHasntPaid.Checked = false;
            rbHasPaid.Checked = false;
        }

        public void ClearEventFields()
        {
            Clear(txtEventId, txtEventName, txtEventPrice);
            lastEvent = new Eventt(null, null, 0, DateTime.MinValue);
            cbEventSearch.Text = string.Empty;
            dtpEventDate.Value = DateTime.Now;
            dtpEventTime.Value = DateTime.Now;
            dataGridViewAttdEvent.DataSource = null;
            dataGridViewAttgEvent.DataSource = null;

            btnEventCreate.Enabled = true;
            btnCustUpdate.Enabled = true;
            txtCustPnr.ReadOnly = true;
            txtEventId.ReadOnly = false;
            btnCustCreate.Enabled = true;
            btnEventUpdate.Enabled = true;
            btnEventClear.Enabled = true;
        }

        public void FillEventEventComboBox()
        {
            string sql = "SELECT * FROM Eventt";
            List<Eventt> eventList = GetAnything(sql, GetEventt);
            foreach (Eventt ev in eventList)
            {
                cbEventSearch.Items.Add(ev.EId);
            }
        }

        public void FillCustEventComboBox()
        {
            string sql = "SELECT * FROM Eventt";
            List<Eventt> eventList = GetAnything(sql, GetEventt);
            foreach (Eventt ev in eventList)
            {
                if (ev.EDate > DateTime.Now)
                    cbCustEvent.Items.Add(ev.EId);
            }
        }

        public void UpdateEventComboBoxes()
        {
            string sql = "SELECT * FROM Eventt";
            List<Eventt> eventtList = GetAnything(sql, GetEventt);
            cbEventSearch.Items.Clear();
            cbCustEvent.Items.Clear();
            foreach (Eventt e in eventtList)
            {
                cbEventSearch.Items.Add(e.EId);
                cbCustEvent.Items.Add(e.EId);
            }
        }

        public void EnterCust(List<Customer> custList)
        {
            lastCust = custList.First();
            txtCustMail.Text = lastCust.CMail;
            txtCustPhone.Text = lastCust.CPhone;
            txtCustAddress.Text = lastCust.CAddress;
            txtCustName.Text = lastCust.CName;
            txtCustPnr.Text = lastCust.CPnr;

            btnCustCreate.Enabled = false;
            txtCustPnr.ReadOnly = true;
            btnCustUpdate.Enabled = true;
            btnCustClear.Enabled = true;
        }

        public void EnterEvent(List<Eventt> eventList)
        {
            lastEvent = eventList.First();
            txtEventId.Text = lastEvent.EId;
            txtEventName.Text = lastEvent.EName;
            txtEventPrice.Text = lastEvent.EPrice.ToString();
            dtpEventDate.Value = lastEvent.EDate;
            dtpEventTime.Value = lastEvent.EDate;

            btnEventCreate.Enabled = false;
            txtEventId.ReadOnly = true;
            btnEventUpdate.Enabled = true;
            btnEventClear.Enabled = true;
        }

        public bool CheckTextboxes(params TextBox[] boxes)
        {
            bool b = false;
            foreach (TextBox box in boxes)
            {
                if (box.Text == "")
                {
                    b = true;
                    return b;
                }
            }
            return b;
        }

        public void Clear(params TextBox[] boxes)
        {
            foreach (TextBox box in boxes)
            {
                box.Text = string.Empty;
            }
        }

        public void UpdateCustTables()
        {
            string sql = "SELECT * FROM Attending WHERE cPnr = '" + lastCust.CPnr + "';";
            List<Attending> attgList = GetAnything(sql, GetAttending);
            dataGridViewAttgCust.DataSource = ShowTable(attgList, TableCustomerAttg);

            //---

            sql = "SELECT * FROM Attended WHERE cpnr = '" + lastCust.CPnr + "';";
            List<Attended> attdList = GetAnything(sql, GetAttended);
            dataGridViewAttdCust.DataSource = ShowTable(attdList, TableCustomerAttd);

        }

        public void UpdateEventTables()
        {
            string sql = "SELECT * FROM Attending WHERE eid = '" + lastEvent.EId + "';";
            List<Attending> attgList = GetAnything(sql, GetAttending);
            dataGridViewAttgEvent.DataSource = ShowTable(attgList, TableEventtAttg);

            //---

            sql = "SELECT * FROM Attended WHERE eId = '" + lastEvent.EId + "';";
            List<Attended> attdList = GetAnything(sql, GetAttended);
            dataGridViewAttdEvent.DataSource = ShowTable(attdList, TableEventtAttd);
        }

        public void UpdateCustSearch()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            List<string> custList = new List<string>();
            string sql = "SELECT * FROM Customer";
            List<Customer> searchList = GetAnything(sql, GetCustomer);
            foreach (Customer c in searchList)
                custList.Add(c.CPnr);
            source.AddRange(custList.ToArray());
            txtCustSearch.AutoCompleteCustomSource = source;
            txtCustSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCustSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void ShowMsg(string s)
        {
            txtMessage.Text = s;
        }

        public static DataTable GetHeaders(DataTable table, string[] headers)
        {
            for (int i = 0; i < headers.Length; i++)
                table.Columns.Add(headers[i]);
            return table;
        }

        public static DataTable FillTableWithHeaders(DataTable table, string[][] listList)
        {
            for (int i = 1; i < listList.Length; i++)
            {
                string[] s = listList[i];
                List<string> innerList = new List<string>();
                for (int j = 0; j < s.Length; j++)
                {
                    innerList.Add(s[j]);
                }
                string[] outerList = innerList.ToArray();
                table.Rows.Add(outerList);
            }
            return table;
        }

        public static DataTable FillTableWithoutHeaders(DataTable table, string[][] listList)
        {
            for (int i = 0; i < listList.Length; i++)
            {
                string[] s = listList[i];
                List<string> innerList = new List<string>();
                for (int j = 0; j < s.Length; j++)
                {
                    innerList.Add(s[j]);
                }
                string[] outerList = innerList.ToArray();
                table.Rows.Add(outerList);
            }
            return table;
        }

        public void FillEmplTableComboBox()
        {
            try
            {
                string[] tableList = client.GetEmplTables();
                for (int i = 0; i < tableList.Length; i++)
                {
                    cbTable.Items.Add(tableList[i]);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        public void FillEmplComboBox()
        {
            try
            {
                string[] emplList = client.GetEmployees();
                for (int i = 0; i < emplList.Length; i++)
                {
                    cbEmployee.Items.Add(emplList[i]);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(HandleException(ex));
            }
        }

        public static void CustHeaders(DataTable dt)
        {
            DataTable table = dt;
            List<string> headers = new List<string>();
            headers.Add("Personal Nr.");
            headers.Add("Name");
            headers.Add("Address");
            headers.Add("Phone Nr.");
            headers.Add("Mail");

            foreach (string header in headers)
            {
                table.Columns.Add(header);
            }
        }
        
        public void UpdateCronusComboBoxes(params ComboBox[] comboBoxList)
        {
            foreach(ComboBox cb in comboBoxList)
            {
                cb.SelectedIndex = -1;
            }
        }
    }
}
