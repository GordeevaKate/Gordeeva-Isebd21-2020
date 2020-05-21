﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractSyshiBarBusinessLogic.BusinessLogics;
using AbstractSyshiBarBusinessLogic.Interfaces;
using Unity;

namespace SyshiBarView
{
    public partial class FormReportStorageSeafoods : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        private readonly IStorageLogic storageLogic;
        public FormReportStorageSeafoods(ReportLogic logic, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.storageLogic = storageLogic;
        }
        private void ButtonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var dict = storageLogic.GetList();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        int sum = 0;
                        dataGridView.Rows.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.StorageSeafoods)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.SeafoodName, listElem.Count });
                            sum += listElem.Count;
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", sum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStorageFoodsToExcelFile(new ReportBindingModel { FileName = dialog.FileName });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}