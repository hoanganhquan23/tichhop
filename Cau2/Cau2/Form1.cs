using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cau2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void LoadDataGridView()
        {
            string link = "http://localhost:90/hocresful/api/sanpham";

            HttpWebRequest request = WebRequest.CreateHttp(link);

            WebResponse response = request.GetResponse();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(response.GetResponseStream());

            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }
        public void LoadComboBox()
        {
            string link = "http://localhost:90/hocresful/api/danhmuc";
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer (typeof(DanhMuc[]));
            object data = js.ReadObject(response.GetResponseStream());
            DanhMuc[] arr1 = data as DanhMuc[];
            cboDanhMuc.DataSource = arr1;
            cboDanhMuc.ValueMember = "MaDanhMuc";
            cboDanhMuc.DisplayMember = "TenDanhMuc";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadComboBox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtMaSP.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            txtTenSP.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            txtDonGia.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            txtMaDM.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string madm = txtMaDM.Text;
            string link = "http://localhost:90/hocresful/api/sanpham?madm=" + madm;
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(response.GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txtMaSP.Text, txtTenSP.Text, txtDonGia.Text, cboDanhMuc.SelectedValue);
            string link = "http://localhost:90/hocresful/api/sanpham" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "POST";
            Stream dataStream = request.GetRequestStream();
            DataContractJsonSerializer js  = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
                if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("Them san pham thanh cong");
            }
            else
            {
                MessageBox.Show("them san pham that bai");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txtMaSP.Text, txtTenSP.Text, txtDonGia.Text, cboDanhMuc.SelectedValue);
            string link = "http://localhost:90/hocresful/api/sanpham" + postString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "PUT";
            Stream dataStream = request.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq)
            {
                
                MessageBox.Show("sua san pham thanh cong");
                LoadDataGridView();
            }
            else
            {
                MessageBox.Show("sua san pham that bai");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string masp = txtMaSP.Text;
            string deleteString = string.Format("?id={0}", masp);          
            string link = "http://localhost:90/hocresful/api/sanpham" + deleteString;
            HttpWebRequest request = HttpWebRequest.CreateHttp(link);
            request.Method = "DELETE";
            Stream dataStream = request.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            bool kq = (bool)data;
            if (kq) { 
                LoadDataGridView();
                MessageBox.Show("Xoa san pham thanh cong");
            }

        }
    }
}
