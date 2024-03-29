﻿using booman.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace booman.Views
{
    public class ServiceItem
    {
        public string IdService { get; set; }
        public string NameService { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }

    }

    public partial class ServiceManagementView : UserControl
    {
        public ServiceManagementView()
        {
            InitializeComponent();
            LoadService();
        }

        public void LoadService()
        {
            MySQLDatabaseService connection = new MySQLDatabaseService();
            DataTable listService = connection.GetTableData("service");
            SeviceListView.ItemsSource = listService.DefaultView;
        }

        private void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)SeviceListView.SelectedItem;
            if (selectedRow != null)
            {
                ServiceItem selectedService = new ServiceItem()
                {
                    IdService = selectedRow["IdService"].ToString(),
                    NameService = selectedRow["NameService"].ToString(),
                    Price = Convert.ToDecimal(selectedRow["Price"]),
                    Unit = selectedRow["Unit"].ToString(),
                    Note = selectedRow["Note"].ToString(),
                };
                textServiceid.Text = selectedService.IdService;
                textServiceName.Text = selectedService.NameService;
                textPrice.Text = selectedService.Price.ToString();
                textUnit.Text = selectedService.Unit;
                textNote.Text = selectedService.Note;

            }
        }

        private void AddService(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            connectionDB.InsertService(textServiceid.Text, textServiceName.Text, Convert.ToDecimal(textPrice.Text),textUnit.Text,textNote.Text);
            MessageBox.Show("Thêm dịch vụ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadService(); // Cập nhật danh sách dịch vụ
        }

        private void UpdateService(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService();
            connectionDB.UpdateService(textServiceid.Text, textServiceName.Text, Convert.ToDecimal(textPrice.Text), textUnit.Text,textNote.Text);
            MessageBox.Show("Cập nhật dịch vụ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadService(); // Cập nhật danh sách dịch vụ
        }

        private void editService(object sender, RoutedEventArgs e)
        {
            MySQLDatabaseService connectionDB = new MySQLDatabaseService(); 
            connectionDB.UpdateService(textServiceid.Text.ToString(), textServiceName.Text.ToString(), Convert.ToDecimal(textPrice.Text), textUnit.Text.ToString(), textNote.Text.ToString());
            LoadService();
        }
        

        private void DeleteService(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá dịch vụ không?", "Xác nhận", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                MySQLDatabaseService connectionDB = new MySQLDatabaseService();
                connectionDB.DeleteService(textServiceid.Text.ToString());
                MessageBoxResult result_2 = MessageBox.Show("Xoá dịch vụ thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result_2 == MessageBoxResult.OK)
                {
                    LoadService();
                }
            }
        }
    }
}
