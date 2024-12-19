using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using De02.Models;

namespace De02
{
    public partial class frmSanpham : Form
    {
        DBSanPham dbSanPham = new DBSanPham();
        private List<Sanpham> inMemoryList = new List<Sanpham>(); // Danh sách tạm
        public frmSanpham()
        {
            InitializeComponent();
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            try
            {
                List<Sanpham> listSP = dbSanPham.Sanphams.ToList(); //lấy sinh viên
                List<LoaiSP> listLoai = dbSanPham.LoaiSPs.ToList(); //lấy các khoa
                FillLoaiSP(listLoai);
                BindGrid(listSP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<Sanpham> listSP)
        {
            dgvSanpham.Rows.Clear();
            foreach (Sanpham student in listSP)
            {
                int rowNew = dgvSanpham.Rows.Add();
                dgvSanpham.Rows[rowNew].Cells[0].Value = student.MaSP;
                dgvSanpham.Rows[rowNew].Cells[1].Value = student.TenSP;
                dgvSanpham.Rows[rowNew].Cells[2].Value = student.Ngaynhap;
                dgvSanpham.Rows[rowNew].Cells[3].Value = student.LoaiSP?.TenLoai;
            }
        }

        private void FillLoaiSP(List<LoaiSP> listLoai)
        {
            cbLoaiSP.DataSource = listLoai;
            cbLoaiSP.DisplayMember = "TenLoai";
            cbLoaiSP.ValueMember = "MaLoai";
        }

        private void dgvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hợp lệ
            {
                DataGridViewRow row = dgvSanpham.Rows[e.RowIndex];

                txtMaSP.Text = row.Cells[0].Value?.ToString();
                txtTenSP.Text = row.Cells[1].Value?.ToString();
                dtNgaynhap.Text = row.Cells[2].Value?.ToString();
                cbLoaiSP.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void loadForm()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
        }

        private void loadDGV()
        {
            List<Sanpham> newList = dbSanPham.Sanphams.ToList();
            BindGrid(newList);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckDataInput())
            {
                var spHienTai = inMemoryList.FirstOrDefault(s => s.MaSP == txtMaSP.Text);
                if (spHienTai == null)
                {
                    Sanpham newSP = new Sanpham
                    {
                        MaSP = txtMaSP.Text,
                        TenSP = txtTenSP.Text,
                        Ngaynhap = Convert.ToDateTime(dtNgaynhap.Text),
                        MaLoai = cbLoaiSP.SelectedValue.ToString()
                    };

                    inMemoryList.Add(newSP); // Lưu vào danh sách tạm
                    BindGrid(inMemoryList);
                    loadForm();

                    MessageBox.Show($"Thêm {newSP.TenSP} thành công (chưa lưu vào CSDL)!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Mã SP {txtMaSP.Text} đã tồn tại trong danh sách tạm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool CheckDataInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckDataInput())
                    return;

                var suaSP = dbSanPham.Sanphams.FirstOrDefault(s => s.MaSP == txtMaSP.Text);
                if (suaSP != null)
                {
                    // Cập nhật thông tin
                    suaSP.TenSP = txtTenSP.Text;
                    suaSP.Ngaynhap = Convert.ToDateTime(dtNgaynhap.Text);
                    suaSP.MaLoai = cbLoaiSP.SelectedValue.ToString();

                    dbSanPham.Sanphams.AddOrUpdate(suaSP);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

                dbSanPham.SaveChanges();
                loadDGV();
                loadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSP.Text))
                    throw new Exception("Vui lòng nhập mã SP để xóa!");

                var XoaSP = dbSanPham.Sanphams.FirstOrDefault(s => s.MaSP == txtMaSP.Text);
                if (XoaSP == null)
                    throw new Exception("Không tìm thấy SP.");

                DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa {XoaSP.TenSP} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    dbSanPham.Sanphams.Remove(XoaSP);
                    dbSanPham.SaveChanges();

                    loadDGV();
                    loadForm();
                    MessageBox.Show("Xóa dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var sp in inMemoryList)
                {
                    dbSanPham.Sanphams.AddOrUpdate(sp);
                }

                dbSanPham.SaveChanges();
                inMemoryList.Clear(); // Xóa danh sách tạm sau khi lưu
                loadDGV();

                MessageBox.Show("Lưu dữ liệu thành công vào CSDL!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            inMemoryList.Clear(); // Xóa danh sách tạm mà không lưu
            loadDGV();
            loadForm();

            MessageBox.Show("Hủy lưu dữ liệu tạm thời!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tenTim = txtTim.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tenTim))
            {
                var spTimThay = dbSanPham.Sanphams.FirstOrDefault(sp => sp.TenSP.Equals(tenTim, StringComparison.OrdinalIgnoreCase));
                if (spTimThay != null)
                {
                    txtMaSP.Text = spTimThay.MaSP;
                    txtTenSP.Text = spTimThay.TenSP;
                    dtNgaynhap.Value = Convert.ToDateTime( spTimThay.Ngaynhap);
                    cbLoaiSP.SelectedValue = spTimThay.MaLoai;
                    MessageBox.Show($"Đã tìm thấy sản phẩm: {spTimThay.TenSP}", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có tên này!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm để tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
