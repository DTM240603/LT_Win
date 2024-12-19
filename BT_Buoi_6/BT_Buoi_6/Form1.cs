using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BT_Buoi_6.Models;

namespace BT_Buoi_6
{
    public partial class Form1 : Form
    {
        // Khởi tạo đối tượng CSDL
        StudentContextDB context = new StudentContextDB();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                List<Student> listStudent = context.Student.ToList(); //lấy sinh viên
                List<Faculty> listFalcultys = context.Faculty.ToList(); //lấy các khoa
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<Student> listStudent)
        {
            dgvDS.Rows.Clear(); // Làm sạch dữ liệu
            foreach (Student student in listStudent)
            {
                int rowNew = dgvDS.Rows.Add();
                dgvDS.Rows[rowNew].Cells[0].Value = student.StudentID;
                dgvDS.Rows[rowNew].Cells[1].Value = student.FullName;
                dgvDS.Rows[rowNew].Cells[2].Value = student.Faculty.FacultyName;
                dgvDS.Rows[rowNew].Cells[3].Value = student.AverageScore;
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            cbFaculty.DataSource = listFalcultys;
            cbFaculty.DisplayMember = "FacultyName";
            cbFaculty.ValueMember = "FacultyID";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hợp lệ
            {
                DataGridViewRow row = dgvDS.Rows[e.RowIndex];

                txtID.Text = row.Cells[0].Value?.ToString();
                txtName.Text = row.Cells[1].Value?.ToString();
                cbFaculty.Text = row.Cells[2].Value?.ToString();
                txtScore.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckDataInput())
            {
                var existingStudent = context.Student.FirstOrDefault(s => s.StudentID == txtID.Text);
                if (existingStudent == null)
                {
                    Student newStudent = new Student
                    {
                        StudentID = txtID.Text,
                        FullName = txtName.Text,
                        AverageScore = Convert.ToDouble(txtScore.Text),
                        FacultyID = Convert.ToInt32(cbFaculty.SelectedValue)
                    };

                    // Thêm sinh viên mới vào cơ sở dữ liệu
                    context.Student.Add(newStudent);
                    context.SaveChanges();

                    // Nạp lại danh sách và hiển thị
                    List<Student> updatedList = context.Student.ToList();
                    BindGrid(updatedList);

                    // Đưa focus về cuối danh sách
                    dgvDS.FirstDisplayedScrollingRowIndex = dgvDS.RowCount - 1;
                    dgvDS.ClearSelection();
                    dgvDS.Rows[dgvDS.RowCount - 1].Selected = true;

                    loadForm();
                    MessageBox.Show($"Thêm sinh viên {newStudent.FullName} thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Sinh viên với MSSV {txtID.Text} đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void loadForm()
        {
            txtID.Clear();
            txtName.Clear();
            txtScore.Clear();
        }

        private void loadDGV()
        {
            List<Student> newList = context.Student.ToList();
            BindGrid(newList);
        }


        private bool CheckDataInput()
        {
            if (txtID.Text == "" || txtName.Text == "" || txtScore.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                float kq = 0;
                bool ketQua = float.TryParse(txtScore.Text, out kq);
                if (!ketQua)
                {
                    MessageBox.Show("Điểm sinh viên chưa đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

      

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckDataInput())
                    return;

                var studentToEdit = context.Student.FirstOrDefault(s => s.StudentID == txtID.Text);
                if (studentToEdit != null)
                {
                    // Cập nhật thông tin
                    studentToEdit.FullName = txtName.Text;
                    studentToEdit.AverageScore = Convert.ToDouble(txtScore.Text);
                    studentToEdit.FacultyID = Convert.ToInt32(cbFaculty.SelectedValue);

                    context.Student.AddOrUpdate(studentToEdit);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Thêm mới
                    Student newStudent = new Student
                    {
                        StudentID = txtID.Text,
                        FullName = txtName.Text,
                        AverageScore = Convert.ToDouble(txtScore.Text),
                        FacultyID = Convert.ToInt32(cbFaculty.SelectedValue)
                    };

                    context.Student.Add(newStudent);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                context.SaveChanges();
                loadDGV();
                loadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvDS.Rows.Count; i++)
            {
                if (dgvDS.Rows[i].Cells[0].Value != null && dgvDS.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                    throw new Exception("Vui lòng nhập mã số sinh viên để xóa!");

                var studentToDelete = context.Student.FirstOrDefault(s => s.StudentID == txtID.Text);
                if (studentToDelete == null)
                    throw new Exception("Không tìm thấy sinh viên có MSSV cần xóa.");

                DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa sinh viên {studentToDelete.FullName} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    context.Student.Remove(studentToDelete);
                    context.SaveChanges();

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
    }
}
