using AutoMapper;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<CreateStudentDto, Student>();

            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
