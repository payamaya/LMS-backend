# LMS-backend
# Learning Platform Application

## Frameworks and Technologies
The application will have a backend built with **.NET**. The database will be constructed using **Entity Framework Core** following the **code first** method. The frontend will use **React**. Other frameworks and libraries such as **Bootstrap** can also be utilized.

### More Information: 
For more details, visit: [Learning Platform on Wikipedia](https://sv.wikipedia.org/wiki/Lärplattform)

---

## Entities, Relationships, and Attributes

The following entities and attributes represent a minimum set of requirements and may need to be expanded as the system is further planned.

### 1. **User**
The application will manage users in the roles of students and teachers, all of whom will have login credentials and accounts. Users should have at least the following attributes:
- Name
- Email Address

### 2. **Course**
Each student is enrolled in a single course, which will have the following attributes:
- Course Name (e.g., "Lexicon LTU")
- Description
- Start Date

### 3. **Module**
Each course can contain one or more modules. A module will have the following attributes:
- Module Name (e.g., "Database Design", "JavaScript")
- Description
- Start Date
- End Date

Modules must not overlap with each other or extend beyond the course duration.

### 4. **Activity**
Each module contains multiple activities such as e-learning sessions, lectures, exercises, or assignments. An activity will have the following attributes:
- Type (e.g., lecture, assignment)
- Name
- Start/End Time
- Description

Activities must not overlap with each other or extend beyond the module duration.

### 5. **Document**
All entities above (users, courses, modules, activities) can be associated with documents. Examples of documents include:
- Student submissions
- Module documents
- General course documents
- Lecture materials or exercises tied to activities

Documents should have the following attributes:
- Name
- Description
- Timestamp of upload
- Information on the user who uploaded the document

---

## Use Cases

### Minimal Requirements

#### As a Non-logged-in Visitor
- **Log in** to the platform.

#### As a Student
- View the **course** they are enrolled in and the other students in the course.
- View the **modules** they are studying.
- View the **activities** for a specific module (module schedule).

#### As a Teacher
- View all **courses**.
- View all **modules** within a course.
- View all **activities** within a module.
- Create and edit **users** (students and teachers).
- Create and edit **courses**.
- Create and edit **modules**.
- Create and edit **activities**.

### Desired Use Cases

#### As a Student
- View and download documents associated with a specific module or activity.
- View **assignments**, including:
  - Submission status
  - Due date
  - Late submission status
- Upload **files** as assignment submissions.

#### As a Teacher
- Upload **documents** for courses, modules, and activities.
- Accept **student submissions**.

### Optional (Extra) Use Cases

#### As a Non-logged-in Visitor
- Request a **password reset**.

#### As a Student
- Share **documents** with their course or module.
- Receive **notifications** when a teacher makes changes to the course (e.g., uploads a new document, adds a module or activity).
- Receive **feedback** on submitted assignments.
- Receive **notifications** when feedback is provided on an assignment.
- **Register** after receiving an invitation via email.
- **Delete** their account and all associated information per **GDPR**.

#### As a Teacher
- Provide **feedback** on student submissions.

---

## API Specifications

### Minimal Requirements
- Basic **error handling** with appropriate HTTP status codes:
  - `404` for resource not found.
  - `500` for server errors.
- **Validation** and error handling for all requests.
- Use of **DTOs** (Data Transfer Objects) for request/response handling.
- Documented using **Swagger** for easily accessible endpoints, parameters, and response types.
- Support for **JWT authentication**.
- **Refresh tokens** support.
- Support for **pagination**:
  - The client can send a page size.
  - The client can search/filter based on properties.

### Endpoints
- All endpoints should require **authentication**, except for the login endpoint.

---

## Conclusion
This project will be a fully functional learning platform application with comprehensive backend and frontend development, utilizing modern frameworks like .NET and React. The API will be robust, secure, and documented with Swagger to ensure clarity and ease of use.

