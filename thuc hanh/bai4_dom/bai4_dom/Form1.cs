using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace bai4_dom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XmlDocument doc = new XmlDocument();
        string tentep = @"D:\Haui\ki 7 2022-2023\tich hop he thong phan mem\thuc hanh\bai4_dom\dsnhanvien.xml";
        int d;

        private void HienThi()
        {
            dg_Nhanvien.Rows.Clear();
            doc.Load(tentep);

            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;

            dg_Nhanvien.ColumnCount = 3;
            dg_Nhanvien.Rows.Add();

            foreach (XmlNode nhan_vien in DS)
            {
                XmlNode ma_nv = nhan_vien.SelectSingleNode("@manv");
                dg_Nhanvien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();

                XmlNode ho_ten = nhan_vien.SelectSingleNode("hoten");
                dg_Nhanvien.Rows[sd].Cells[1].Value = ho_ten.InnerText.ToString();

                XmlNode dia_chi = nhan_vien.SelectSingleNode("diachi");
                dg_Nhanvien.Rows[sd].Cells[2].Value = dia_chi.InnerText.ToString();

                dg_Nhanvien.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);

            XmlElement goc = doc.DocumentElement;

            XmlNode nhan_vien = doc.CreateElement("nhanvien");
            XmlAttribute ma_nv = doc.CreateAttribute("manv");
            ma_nv.InnerText = txt_manv.Text;
            nhan_vien.Attributes.Append(ma_nv);

            XmlNode ho_ten = doc.CreateElement("hoten");
            ho_ten.InnerText = txt_hoten.Text;
            nhan_vien.AppendChild(ho_ten);

            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien.AppendChild(dia_chi);

            goc.AppendChild(nhan_vien);

            doc.Save(tentep);
            HienThi();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                doc.Load(tentep);
                XmlElement goc = doc.DocumentElement;
                XmlNode nhan_vien_xoa = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
                goc.RemoveChild(nhan_vien_xoa);
                doc.Save(tentep);
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong co ma nv nay");
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nhan_vien_cu = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txt_manv.Text + "']");
            XmlNode nhan_vien_moi = doc.CreateElement("nhanvien");

            XmlAttribute ma_nv = doc.CreateAttribute("manv");
            ma_nv.InnerText = txt_manv.Text;
            nhan_vien_moi.Attributes.Append(ma_nv);

            XmlNode ho_ten = doc.CreateElement("hoten");
            ho_ten.InnerText = txt_hoten.Text;
            nhan_vien_moi.AppendChild(ho_ten);

            XmlNode dia_chi = doc.CreateElement("diachi");
            dia_chi.InnerText = txt_diachi.Text;
            nhan_vien_moi.AppendChild(dia_chi);

            goc.ReplaceChild(nhan_vien_moi, nhan_vien_cu);

            doc.Save(tentep);
            HienThi();
        }

        private void dg_Nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txt_manv.Text = dg_Nhanvien.Rows[d].Cells[0].Value.ToString();
            txt_hoten.Text = dg_Nhanvien.Rows[d].Cells[1].Value.ToString();
            txt_diachi.Text = dg_Nhanvien.Rows[d].Cells[2].Value.ToString();
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", tentep);
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlNodeList DS = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;

            XmlNode goc = doc.DocumentElement;
            XmlNode nhan_vien_tim = null;
            string manv_tim = txt_manv.Text;


            foreach (XmlNode nhan_vien in DS)
            {
                XmlNode ma_nv_tim = nhan_vien.SelectSingleNode("@manv");
                if (ma_nv_tim.InnerText.Equals(manv_tim))
                {
                    nhan_vien_tim = nhan_vien;
                    break;
                }
            }

            XmlNode ma_nv = nhan_vien_tim.SelectSingleNode("@manv");
            dg_Nhanvien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();

            XmlNode ho_ten = nhan_vien_tim.SelectSingleNode("hoten");
            dg_Nhanvien.Rows[sd].Cells[1].Value = ho_ten.InnerText.ToString();

            XmlNode dia_chi = nhan_vien_tim.SelectSingleNode("diachi");
            dg_Nhanvien.Rows[sd].Cells[2].Value = dia_chi.InnerText.ToString();


        }
    }
}
