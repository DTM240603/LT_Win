using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop1
{
    internal class Student
    {
        private string Id { get; set; }
        private string Name { get; set; }
        private int Age { get; set; }

        // Constructor không tham số
        public Student()
        {
        }

        // Constructor có tham số
        public Student(string ma, string ten, int tuoi)
        {
            this.Id = ma;
            this.Name = ten;
            this.Age = tuoi;
        }

        // Constructor sao chép
        public Student(Student st)
        {
            Id = st.Id;
            Name = st.Name;
            Age = st.Age;
        }

        // Getter cho Id
        public string GetId()
        {
            return Id;
        }

        // Getter cho Name
        public string GetName()
        {
            return Name;
        }

        // Getter cho Age
        public int GetAge()
        {
            return Age;
        }

        // Nhập thông tin sinh viên từ bàn phím
        public void NhapThongTin()
        {
            Console.Write("Nhap ma sinh vien: ");
            Id = Console.ReadLine();

            Console.Write("Nhap ten sinh vien: ");
            Name = Console.ReadLine();

            Console.Write("Nhap tuoi sinh vien: ");
            Age = int.Parse(Console.ReadLine());
            
        }

        // Phương thức xuất thông tin sinh viên
        public string GetStudentInfo()
        {
            return $"Ma: {Id}, Ten: {Name}, Tuoi: {Age}";
        }
    }
}
