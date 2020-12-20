using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase{
        private static readonly List<Student>Students = new List<Student>(){
            new Student(){
                Id = 1,
                Name =  "Nishant",
                Roll = 15,
                DOB = new DateTime(1999,1,1),
                age = 21
                },
            new Student(){
               Id = 2,
               Name = "Viknis",
               Roll = 16,
               DOB = new DateTime(1999,1,1),
               age = 22
            }
        };
            
    
       

        [HttpGet]
        public List<Student> Get()
        { 
            return Students; 
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(long id)
        {
            Student student = Session.Get<Student>(id);
            return student;
        }
        [HttpPost]
        public ActionResult<Book> Post(Book book)
        {
            student.Id = Students.Select(x=>x.Id).Max + 1;
            Students.Add(student);
            return student;
        }
        [HttpPut]
        public Student Put(Student student)
        {
            Student updatedStudent = Students.Single(x => x.Id == student.Id);
            updatedStudent = student;
            return student;
        }
    }
}