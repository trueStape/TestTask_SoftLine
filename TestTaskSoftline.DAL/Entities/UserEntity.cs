using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskSoftline.DAL.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string About { get; set; }
        public bool Decree { get; set; }
        public bool IsDeleted { get; set; }
    }
}