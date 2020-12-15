using LINQtoCSV;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationData.Models
{
    [Table("Employees")]
    public class Employee : IModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Значение не должно превышать 50 символов.")]
        [CsvColumn(Name = "Фамилия", FieldIndex = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Значение не должно превышать 50 символов.")]
        [CsvColumn(Name = "Имя", FieldIndex = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Значение не должно превышать 50 символов.")]
        [CsvColumn(Name = "Отчество", FieldIndex = 3)]
        public string FatherName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Дата должна быть в формате (dd.mm.yyyy)")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [CsvColumn(Name = "День рождения", FieldIndex = 4)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage ="Значение не может быть меньше 0 и не должно превышать заданного (4 разряда)")]
        [CsvColumn(Name = "Паспорт серия", FieldIndex = 5)]
        public int PasportSerial { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "Значение не может быть меньше 0 и не должно превышать заданного (6 разрядов)")]
        [CsvColumn(Name = "Паспорт номер", FieldIndex = 6)]
        public int PasportNumber { get; set; }

        [DataType(DataType.Text)]
        [CsvColumn(Name = "Примечание", FieldIndex = 7)]
        public string Notes { get; set; }
    }
}
