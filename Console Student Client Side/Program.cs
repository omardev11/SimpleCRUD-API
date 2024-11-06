using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StudentAPIClient
{
    class program
    {
        static readonly HttpClient _httpClient = new HttpClient();


        static async Task Main(string[] args)
        {
            _httpClient.BaseAddress = new Uri("http://localhost:5253/api/Students/");



            await GetAllStudents();

            await GetPassedStudents();

            await GetAvarageOfGrades();

            await GetStudentById(1);

            Student newstudent = new Student
            {
                FullName = "Mohamed ismail",
                Age = 19,
                Grade = 78
            };

            //await AddNewStudent(newstudent);

            //await DeleteStudentByID(3);


            await UpdateStudent(2, newstudent);

            await GetAllStudents();




        }

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("\n__________________________");
                Console.WriteLine("\n Fetching All Students... \n");

                var student = await _httpClient.GetFromJsonAsync<List<Student>>("All");

                if (student != null)
                {
                    foreach (var stu in student)
                    {
                        Console.WriteLine($"ID:       {stu.ID}   ");
                        Console.WriteLine($"FullName: {stu.FullName}   ");
                        Console.WriteLine($"Age:      {stu.Age}   ");
                        Console.WriteLine($"Grade:    {stu.Grade}   ");
                        Console.WriteLine("\n__________________________");

                    }
                }

            }
            catch( Exception ex ) 
            {
                Console.WriteLine("An Error Occurd : " + ex.Message);
            }
        }

        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("\n__________________________");
                Console.WriteLine("\n Fetching All Passed Students... \n");

                var student = await _httpClient.GetFromJsonAsync<List<Student>>("Passed");

                if (student != null)
                {
                    foreach (var stu in student)
                    {
                        Console.WriteLine($"ID:       {stu.ID}   ");
                        Console.WriteLine($"FullName: {stu.FullName}   ");
                        Console.WriteLine($"Age:      {stu.Age}   ");
                        Console.WriteLine($"Grade:    {stu.Grade}   ");
                        Console.WriteLine("\n__________________________");

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurd : " + ex.Message);
            }
        }

        static async Task GetAvarageOfGrades()
        {
            try
            {
                Console.WriteLine("\n__________________________");

                var avg = await _httpClient.GetFromJsonAsync<float>("Avarage");

                Console.WriteLine($"\n Avarage of Students Grades is...  {avg} \n");


              

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurd : " + ex.Message);
            }
        }

        static async Task GetStudentById(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nFetching student with ID {id}...\n");

                var response = await _httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        Console.WriteLine($"ID: {student.ID}, Name: {student.FullName}, Age: {student.Age}, Grade: {student.Grade}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task AddNewStudent(Student NewStudent)
        {
            try
            {
               
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nAdding New student...\n");

                var response = await _httpClient.PostAsJsonAsync("",NewStudent);

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    if (student != null)
                    {
                        Console.WriteLine($"\nAdded New student with ID {student.ID}...\n");
                        Console.WriteLine("\n_____________________________");
                        Console.WriteLine($"ID: {student.ID}, Name: {student.FullName}, Age: {student.Age}, Grade: {student.Grade}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Student Data is Invalid ");
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task DeleteStudentByID(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nDeleting student with ID {id}...\n");

                var response = await _httpClient.DeleteAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var Info = await response.Content.ReadAsStringAsync();
                    if (Info != "")
                    {
                        Console.WriteLine($"{Info}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task UpdateStudent(int id ,Student NewStudent)
        {
            try
            {

                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nUpdating  student...\n");

                var response = await _httpClient.PutAsJsonAsync($"{id}", NewStudent);

                if (response.IsSuccessStatusCode)
                {
                    var Info = await response.Content.ReadAsStringAsync();
                    if (Info != "")
                    {
                        Console.WriteLine($"{Info}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }







        public class Student
        {
            public int ID { get; set; }
            public string FullName { get; set; }
            public int Age { get; set; }
            public int Grade { get; set; }
        }
    }
}