using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Datasimilaztion;
using StudentAPI.Model;
using StudentBusinessLayerAPI;
using StudentDataAccessLayerAPI;


namespace StudentAPI.Controllers
{
    [ApiController]

    [Route("api/Students")]
    public class StudentAPIControler : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDTO>> GetAllStudents()
        {
            
            List<StudentDTO> Allstudents = StudentBusiness.GetAllStudents();
            if (Allstudents.Count == 0)
            {
                return NotFound("No Studetnt Found");
            }
            return Ok(Allstudents);
        }
      
        //........................................................................................................................



        [HttpGet ("Passed",Name = "GetPassedStudents")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentDTO>> GetPassedStudents()
        {
            List<StudentDTO> AllPassedstudents = StudentBusiness._GetPassedStudents();
            if (AllPassedstudents.Count == 0)
            {
                return NotFound("No Studetnt Found");
            }
            return Ok(AllPassedstudents);
        }


        //........................................................................................................................



        [HttpGet("Avarage", Name = "GetAvarageGrades")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<float> GetAvarageGrades()
        {
            double avg = StudentBusiness.GetAvarageGrade();
            if (avg == 0)
            {
                return NotFound("No Studetnt Found");
            }
            return Ok(avg);
        }



        //........................................................................................................................



        [HttpGet("{id}", Name = "GetStudentByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentByID(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not Accesepted ID {id}");
            }

            StudentBusiness student = StudentBusiness.Find(id);
            if (student == null)
            {
                return NotFound($"Student Whith ID {id} Is Not Found");
            }
            StudentDTO studentDTO = student.StudentDTO;
            return Ok(studentDTO);
        }



        //........................................................................................................................



        [HttpPost( Name = "AddNewStudent")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> AddNewStudent(StudentDTO StudentInfo)
        {
            if (StudentInfo == null || string.IsNullOrEmpty(StudentInfo.Name) || StudentInfo.Age<5 || StudentInfo.Grade<0 )
            {
                return BadRequest("Invalid Student Data");
            }

            StudentBusiness NewStudent = new StudentBusiness(StudentInfo);

            if (NewStudent.Save())
            {
                StudentInfo.Id = NewStudent.StudentDTO.Id;
                return CreatedAtRoute("GetStudentByID", new { id = StudentInfo.Id }, StudentInfo);
            }
            else
            {
                return StatusCode(500, new { messege = "Error Adding New Student" });
            }

        }




        //........................................................................................................................




        [HttpDelete("{id}" ,Name = "DeleteStudent")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not Accesepted ID {id}");
            }

            StudentBusiness NewStudent = StudentBusiness.Find(id);
            if (NewStudent == null)
            {
                return NotFound($"The Student Whith ID {id} Not Found Sucesfully");
            }

            if (NewStudent.DeleteStudent())
            {
                return Ok($"The Student Whith ID {id} Deleted Sucesfully");
            }
            else
            {
                return StatusCode(500, new { messege = "Error Updating Student" });
            }

           
           
        }



        //........................................................................................................................





        [HttpPut("{id}", Name = "UpdateStudentInfo")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public ActionResult<StudentDTO> UpdateStudentInfoByID(int id,StudentDTO studentNewInfo)
        {
            if (id < 1)
            {
                return BadRequest($"Not Accesepted ID {id}");
            }
            if (studentNewInfo == null || string.IsNullOrEmpty(studentNewInfo.Name) || studentNewInfo.Age < 5 || studentNewInfo.Grade < 0)
            {
                return BadRequest("Invalid Student Data");
            }
            StudentBusiness NewStudent = StudentBusiness.Find(id);
            if (NewStudent == null)
            {
                return NotFound($"The Student Whith ID {id} Not Found Sucesfully");
            }
            
            NewStudent.Name = studentNewInfo.Name;
            NewStudent.Age = studentNewInfo.Age;
            NewStudent.Grade = studentNewInfo.Grade;


            if (NewStudent.Save())
            {
                studentNewInfo = NewStudent.StudentDTO;
                return Ok(studentNewInfo);
            }
            else
            {
                return StatusCode(500, new { messege = "Error Updating Student" });
            }




        }
    }
}
