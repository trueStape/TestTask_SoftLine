using System;

namespace TestTaskSoftline.BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string About { get; set; }
        public bool Decree { get; set; }
    }
}