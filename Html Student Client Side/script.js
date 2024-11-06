// // const apiUrl = "http://localhost:5000/api/students/";
// const apiUrl = "http://localhost:5253/api/Students/";

// class StudentDto {
//   constructor(id, name, age, grade) {
//     this.id = id;
//     this.name = name;
//     this.age = age;
//     this.grade = grade;
//   }

//   // Getters
//   getId() {
//     return this.id;
//   }

//   getName() {
//     return this.name;
//   }

//   getAge() {
//     return this.age;
//   }

//   getGrade() {
//     return this.grade;
//   }

//   // Setters
//   setId(id) {
//     this.id = id;
//   }

//   setName(name) {
//     this.name = name;
//   }

//   setAge(age) {
//     this.age = age;
//   }

//   setGrade(grade) {
//     this.grade = grade;
//   }
// }

// const studentinfo = new StudentDto();

// async function getAllStudents() {
//   const response = await fetch(apiUrl + "All");
//   const students = await response.json();
//   // studentinfo = students;
//   displayResult(students);
// }

// async function getPassedStudents() {
//   const response = await fetch(`${apiUrl}/passed`);
//   const students = await response.json();
//   displayResult(students);
// }

// async function getStudentById() {
//   const id = document.getElementById("studentId").value;
//   const response = await fetch(`${apiUrl}/${id}`);
//   const student = await response.json();
//   displayResult(student);
// }

// async function addNewStudent() {
//   const name = document.getElementById("studentName").value;
//   const grade = document.getElementById("studentGrade").value;

//   const response = await fetch(apiUrl, {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify({ name, grade }),
//   });
//   const result = await response.json();
//   displayResult(result);
// }

// async function updateStudent() {
//   const id = document.getElementById("studentId").value;
//   const name = document.getElementById("studentName").value;
//   const grade = document.getElementById("studentGrade").value;

//   const response = await fetch(`${apiUrl}/${id}`, {
//     method: "PUT",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify({ name, grade }),
//   });
//   const result = await response.json();
//   displayResult(result);
// }

// async function deleteStudent() {
//   const id = document.getElementById("studentId").value;
//   const response = await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
//   const result = await response.json();
//   displayResult(result);
// }

// function displayResult(students) {
//   const tbody = document.getElementById("studentsTable").querySelector("tbody");

//   // Clear any existing rows
//   tbody.innerHTML = "";

//   // Populate the table rows
//   students.forEach((student) => {
//     let selectedStudent = null;
//     const row = document.createElement("tr");
//     row.innerHTML = `
//           <td>${student.id}</td>
//           <td>${student.name}</td>
//           <td>${student.age}</td>
//           <td>${student.grade}</td>
//       `;
//     row.onclick = () => {
//       selectedStudent = student; // Set the selected student
//       showOptionsModal(); // Show the options modal
//       tbody.appendChild(row);
//     };
//   });
// }

const apiUrl = "http://localhost:5253/api/Students/";

async function getAllStudents() {
  try {
    const response = await fetch(apiUrl + "All");
    const students = await response.json();
    displayResult(students);
  } catch (error) {
    console.error(error);
  }
}

async function GetAvarageStudentsGrade() {
  try {
    const response = await fetch(apiUrl + "Avarage");
    const avg = await response.text();
    showMessage("The Avarage Of Students Grade is " + avg);
    // displayResult(students);
  } catch (error) {
    console.error(error);
  }
}

async function getPassedStudents() {
  try {
    const response = await fetch(`${apiUrl}Passed`);
    const students = await response.json();
    displayResult(students);
  } catch (error) {
    console.error(error);
  }
}

async function getStudentById() {
  const id = document.getElementById("studentId").value;
  try {
    const response = await fetch(`${apiUrl}${id}`);
    if (response.ok) {
      const student = await response.json();
      displayResult([student]); // Pass an array for consistent display
    } else if (response.status === 400) {
      // Handle bad request
      console.error(`Bad Request: Not accepted ID ${id}`);
      alert(`Bad Request: Not accepted ID ${id}`);
    } else if (response.status === 404) {
      // Handle not found
      console.error(`Not Found: Student with ID ${id} not found.`);
      alert(`Not Found: Student with ID ${id} not found.`);
    } else {
      // Handle other types of errors
      console.error(`Error: ${response.status} - ${response.statusText}`);
      alert(`Error: ${response.status} - ${response.statusText}`);
    }
  } catch (error) {
    console.error("The Error From GetstudentbyID Func" + error);
  }
}

async function addNewStudent() {
  try {
    const name = document.getElementById("studentName").value;
    const grade = document.getElementById("studentGrade").value;
    const Age = document.getElementById("studentAge").value;

    if (name === "" || Age < 5) {
      console.error("Invalid Inputs");
      return;
    }

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name, grade, Age }),
    });

    if (!response.ok) throw new Error("Failed to add student");

    const result = await response.json();
    displayResult(result);
  } catch (error) {
    console.error(error);
    alert("Error adding student: " + error.message);
  }
}

async function updateStudent() {
  const id = document.getElementById("studentId").value;
  const name = document.getElementById("studentName").value;
  const grade = document.getElementById("studentGrade").value;
  const Age = document.getElementById("studentAge").value;

  try {
    const response = await fetch(`${apiUrl}${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name, grade, Age }),
    });

    if (response.ok) {
      const result = await response.json();
      displayResult(result);
    } else if (response.status === 400) {
      // Handle bad request
      console.error(`Bad Request: Not accepted ID ${id}`);
      alert(`Bad Request: Not accepted ID ${id}`);
    } else if (response.status === 404) {
      // Handle not found
      console.error(`Not Found: Student with ID ${id} not found.`);
      alert(`Not Found: Student with ID ${id} not found.`);
    } else {
      // Handle other types of errors
      console.error(`Error: ${response.status} - ${response.statusText}`);
      alert(`Error: ${response.status} - ${response.statusText}`);
    }
  } catch (error) {
    console.error(error);
    alert("Error updating student: " + error.message);
  }
}

async function deleteStudent() {
  const id = document.getElementById("studentId").value;
  try {
    const response = await fetch(`${apiUrl}${id}`, { method: "DELETE" });

    if (response.ok) {
      const result = await response.text();
      showMessage(result);
      // displayResult(result);
    } else if (response.status === 400) {
      // Handle bad request
      console.error(`Bad Request: Not accepted ID ${id}`);
      alert(`Bad Request: Not accepted ID ${id}`);
    } else if (response.status === 404) {
      // Handle not found
      console.error(`Not Found: Student with ID ${id} not found.`);
      alert(`Not Found: Student with ID ${id} not found.`);
    } else {
      // Handle other types of errors
      console.error(`Error: ${response.status} - ${response.statusText}`);
      alert(`Error: ${response.status} - ${response.statusText}`);
    }
  } catch (error) {
    console.error(error);
    alert("Error deleting student: " + error.message);
  }
}

function displayResult(students) {
  const tbody = document.getElementById("studentsTable").querySelector("tbody");
  tbody.innerHTML = "";
  if (!Array.isArray(students)) {
    students = [students]; // Wrap single student in an array
  }

  students.forEach((student) => {
    const row = document.createElement("tr");
    row.innerHTML = `
          <td>${student.id}</td>
          <td>${student.name}</td>
          <td>${student.age}</td>
          <td>${student.grade}</td>
      `;
    row.onclick = () => {
      document.getElementById("studentId").value = student.id;
      document.getElementById("studentName").value = student.name;
      document.getElementById("studentGrade").value = student.grade;
      document.getElementById("studentAge").value = student.age;
      showOptionsModal();
    };

    tbody.appendChild(row);
  });
}

function showOptionsModal() {
  document.getElementById("optionsModal").style.display = "block";
}

function hideOptionsModal() {
  document.getElementById("optionsModal").style.display = "none";
}

document.getElementById("closeButton").onclick = hideOptionsModal;

document.getElementById("updateButton").onclick = () => {
  updateStudent();
  hideOptionsModal();
};

document.getElementById("deleteButton").onclick = () => {
  deleteStudent();
  hideOptionsModal();
};

function showMessage(message) {
  const modal = document.getElementById("messageModal");
  const modalMessage = document.getElementById("modalMessage");
  const closeModal = document.getElementById("closeModal");

  modalMessage.textContent = message; // Set the message text
  modal.style.display = "block"; // Show the modal

  closeModal.onclick = function () {
    modal.style.display = "none"; // Hide the modal when clicked
  };

  // Close modal when clicking outside of it
  window.onclick = function (event) {
    if (event.target === modal) {
      modal.style.display = "none";
    }
  };
}
