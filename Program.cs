using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Tạo danh sách học sinh
            List<Student> students = new List<Student>
            {
                new Student("1", "An", 16),
                new Student("2", "Binh", 15),
                new Student("3", "Cuong", 18),
                new Student("4", "Anh", 17),
                new Student("5", "Duy", 19)
            };

            // a. In danh sách toàn bộ học sinh
            Console.WriteLine("Danh sach hoc sinh:");
            students.ForEach(s => Console.WriteLine(s.GetStudentInfo()));

            // b. Tìm học sinh có tuổi từ 15 đến 18
            Console.WriteLine("\nHoc sinh co tuoi tu 15 den 18:");
            var ageRange = students.Where(s => s.GetAge() >= 15 && s.GetAge() <= 18).ToList();
            ageRange.ForEach(s => Console.WriteLine(s.GetStudentInfo()));

            // c. Tìm học sinh có tên bắt đầu bằng chữ "A"
            Console.WriteLine("\nHoc sinh co ten bat dau bang chu 'A':");
            var nameStartsWithA = students.Where(s => s.GetName().StartsWith("A", StringComparison.OrdinalIgnoreCase)).ToList();
            nameStartsWithA.ForEach(s => Console.WriteLine(s.GetStudentInfo()));

            // d. Tính tổng tuổi của tất cả học sinh
            int totalAge = students.Sum(s => s.GetAge());
            Console.WriteLine($"\nTong tuoi cua tat ca hoc sinh: {totalAge}");

            // e. Tìm học sinh có tuổi lớn nhất
            int maxAge = students.Max(s => s.GetAge());
            var oldestStudents = students.Where(s => s.GetAge() == maxAge).ToList();
            Console.WriteLine("\nHoc sinh co tuoi lon nhat:");
            oldestStudents.ForEach(s => Console.WriteLine(s.GetStudentInfo()));

            // f. Sắp xếp danh sách theo tuổi tăng dần
            Console.WriteLine("\nDanh sach hoc sinh sap xep theo tuoi tang dan:");
            var sortedStudents = students.OrderBy(s => s.GetAge()).ToList();
            sortedStudents.ForEach(s => Console.WriteLine(s.GetStudentInfo()));

            Console.ReadLine();
        }
    }
}
