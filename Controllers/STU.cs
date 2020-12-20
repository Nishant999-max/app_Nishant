using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace app.Controllers
{
    public class Helper
    {
        private static ISessionFactory _sessionFactory;
        private static string _connectionString;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2017.ConnectionString(_connectionString))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Student>())
                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                        .BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession(string connectionString)
        {
            _connectionString = connectionString;
            return SessionFactory.OpenSession();
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private ISession Session;

        public StudentController()
        {
            Session = Helper.OpenSession("Server=192.158.1.70;User Id=AURORA;Password=Nishant.1;Database=Mach;Connection Timeout=30;");
        }

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            List<Student> Students = Session.Query<Student>().ToList();
            return Students;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(long id)
        {
            Student Student = Session.Get<Student>(id);
            return Student;
        }

        [HttpPost]
        public ActionResult<Student> Post(Student Student)
        {
            Session.Save(Student);
            Session.Flush();
            return Created(Student.Id.ToString(), Student);
        }

        [HttpPut]
        public ActionResult<Student> Put(Student Student)
        {
            Student updatedStudent = Session.Get<Student>(Student.Id);

            if (updatedStudent is null)
            {
                return NotFound();
            }

            updatedStudent.Name = Student.Name;
            updatedStudent.Author = Student.Author;
            updatedStudent.PublishedOn = Student.PublishedOn;

            Session.Update(updatedStudent);
            Session.Flush();
            return updatedStudent;
        }

        [HttpDelete]
        public ActionResult<long> Delete(long id)
        {
            Student deletedStudent = Session.Get<Student>(id);

            if (deletedStudent is null)
            {
                return NotFound();
            }

            Session.Delete(deletedStudent);
            Session.Flush();
            return id;
        }
    }
}