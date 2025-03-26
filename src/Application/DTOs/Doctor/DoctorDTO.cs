using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgenda.src.Application.DTOs.Patient
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage="O nome do Doutor é obrigatório",AllowEmptyStrings=false)]
        public required string Name { get; set; }
        [Required(ErrorMessage="O status do Doutor é obrigatório",AllowEmptyStrings=false)]
        public required int StatusId { get; set; }
    }
}