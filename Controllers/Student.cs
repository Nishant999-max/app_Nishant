using System;
namespace app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private static readonly List<Student>Students = new List<Student>(){
            new Student(){
            
    
        },
        new Student(){
            
        }
        }

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {  
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(long id)
        {   
        }

        [HttpPost]
        public ActionResult<Student> Post(Student Student)
        {   
        }

        [HttpPut]
        public ActionResult<Student> Put(Student Student)
        {
        }

        [HttpDelete]
        public ActionResult<long> Delete(long id)
        {
        }
    }