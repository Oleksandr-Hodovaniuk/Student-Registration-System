using Application.Seeders;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

internal class DataSeeder(StudentRegistrationSystemDbContext context) : ISeeder
{
    public async Task SeedAsync()
    {
        if (await context.Database.CanConnectAsync())
        {
            if (!await context.Topics.AnyAsync() && !await context.Courses.AnyAsync() && !await context.Users.AnyAsync())
            {
                var topics = GetTopics();
                await context.Topics.AddRangeAsync(topics);

                var courses = GetCourses();

                courses[0].Topics.AddRange(new List<Topic> { topics[0], topics[4], topics[5] });
                courses[1].Topics.AddRange(new List<Topic> { topics[5], topics[6], topics[7], topics[14], topics[25] });
                courses[2].Topics.AddRange(new List<Topic> { topics[16], topics[17], topics[18] });
                courses[3].Topics.AddRange(new List<Topic> { topics[18], topics[19] });
                courses[4].Topics.AddRange(new List<Topic> { topics[20], topics[21], topics[22], topics[23] });

                await context.Courses.AddRangeAsync(courses);

                var users = GetUsers();

                users[0].Courses.Add(courses[0]);
                users[0].Courses.Add(courses[1]);
                users[1].Courses.Add(courses[1]);
                users[2].Courses.Add(courses[2]);
                users[3].Courses.Add(courses[3]);
                users[4].Courses.Add(courses[4]);

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                var userCourses = new List<UserCourse>();

                userCourses.Add(new UserCourse { UserId = users[0].Id, CourseId = courses[0].Id, RegistrationDate = DateTime.Now });
                userCourses.Add(new UserCourse { UserId = users[0].Id, CourseId = courses[1].Id, RegistrationDate = DateTime.Now });
                userCourses.Add(new UserCourse { UserId = users[1].Id, CourseId = courses[1].Id, RegistrationDate = DateTime.Now });
                userCourses.Add(new UserCourse { UserId = users[2].Id, CourseId = courses[2].Id, RegistrationDate = DateTime.Now });
                userCourses.Add(new UserCourse { UserId = users[3].Id, CourseId = courses[3].Id, RegistrationDate = DateTime.Now });
                userCourses.Add(new UserCourse { UserId = users[4].Id, CourseId = courses[4].Id, RegistrationDate = DateTime.Now });

                await context.UserCourses.AddRangeAsync(userCourses);
                await context.SaveChangesAsync();
            }
        }    
    }
    private List<Topic> GetTopics()
    {
        return new List<Topic> {
            new() { Name = "C#"},
            new() { Name = "PHP"},
            new() { Name = "Java"},
            new() { Name = "C++"},
            new() { Name = "Python"},
            new() { Name = "JavaScript"},
            new() { Name = "HTML"},
            new() { Name = "CSS"},
            new() { Name = "SQL"},
            new() { Name = "Entity Framework"},
            new() { Name = "ADO.NET"},
            new() { Name = "WPF"},
            new() { Name = "OOP"},
            new() { Name = "Angular"},
            new() { Name = "React"},
            new() { Name = "R"},
            new() { Name = "Data Science"},
            new() { Name = "Machine Learning"},
            new() { Name = "Algorithms"},
            new() { Name = "Cybersecurity"},
            new() { Name = "Android"},
            new() { Name = "iOS"},
            new() { Name = "SQLite"},
            new() { Name = "Firebase"},
            new() { Name = "GitHub"},
            new() { Name = "Node.js"},
            new() { Name = "RESTful APIs"}
        };
    }
    private List<Course> GetCourses()
    {
        return new List<Course> {
            new()
            {
               Name = "CodeQuest",
               Description = "An intensive training course that combines" +
                " the fundamentals of programming with modern approaches " +
                "to software development. Throughout the course, you will" +
                " master key programming principles, learn to work with " +
                "various languages such as C#, JavaScript, and Python, and" +
                " create real-world projects using frameworks and libraries." +
                " The course will help you gain practical skills and prepare" +
                " you for a professional path in the IT industry.",
               Author = "SkillForge Academy",
               СreationDate = DateTime.Now,
               Beginning = DateTime.Parse("2024-12-1"),
               Duration = 56
            },
            new()
            {
               Name = "Web Wizardry: Full-Stack Development",
               Description = "This course is designed for those who want to" +
                " become masters of web development. Over 12 weeks, you will" +
                " learn about all stages of creating web applications, from" +
                " design and development to testing and deployment. You will" +
                " master key technologies such as HTML, CSS, and JavaScript," +
                " as well as frameworks like React for frontend and Node.js" +
                " for backend.\r\n\r\nThe curriculum also includes working" +
                " with databases, using RESTful APIs, version control with" +
                " Git, and principles of Agile methodology. The course features" +
                " numerous hands-on projects that will provide you with real" +
                " teamwork experience.\r\n\r\nBy the end of the course, you will" +
                " create your own complete web application, which can be showcased" +
                " to potential employers, providing an excellent foundation for" +
                " your career in the IT industry.",
               Author = "ProCode Institute",
               СreationDate = DateTime.Now,
               Beginning = DateTime.Parse("2025-1-1"),
               Duration = 84
            },
            new()
            {
               Name = "Data Science Mastery",
               Description = "Dive into the world of data science with this" +
                " comprehensive course designed for aspiring data scientists" +
                " and analysts. Over 10 weeks, you will explore the entire" +
                " data science pipeline, from data collection and cleaning to" +
                " analysis and visualization.\r\n\r\nYou will learn how to" +
                " utilize popular programming languages like Python and R," +
                " along with essential libraries such as Pandas, NumPy, and" +
                " Matplotlib. The course will cover statistical methods, machine" +
                " learning algorithms, and data visualization techniques that are" +
                " critical for making data-driven decisions.\r\n\r\nThroughout the" +
                " course, you will work on real-world datasets, gaining practical" +
                " experience in data manipulation, predictive modeling, and interpretation" +
                " of results. By the end of the program, you will have developed a portfolio" +
                " of projects that showcase your ability to turn raw data into actionable" +
                " insights, equipping you with the skills necessary for a successful" +
                " career in data science.",
               Author = "InnovateTech Education",
               СreationDate = DateTime.Now,
               Beginning = DateTime.Parse("2024-11-1"),
               Duration = 70
            },
            new()
            {
               Name = "Cybersecurity Fundamentals",
               Description = "This course is designed for individuals who aspire to build" +
                " a solid foundation in cybersecurity principles and practices. Over 8 weeks," +
                " you will learn how to protect information systems from various cyber threats" +
                " and attacks.\r\n\r\nYou will explore key topics such as network security," +
                " encryption, risk management, and incident response. The curriculum includes" +
                " hands-on training with essential tools and technologies used in the industry," +
                " allowing you to implement security measures effectively.\r\n\r\nThrough case" +
                " studies and practical exercises, you will understand the importance of cybersecurity" +
                " policies and compliance frameworks. The course will also cover the ethical and" +
                " legal aspects of cybersecurity, preparing you for real-world challenges.\r\n\r\nBy" +
                " the end of this course, you will be equipped with the knowledge and skills needed" +
                " to pursue a career in cybersecurity and contribute to the protection of sensitive" +
                " data in organizations.",
               Author = "NextGen Learning Hub",
               СreationDate = DateTime.Now,
               Beginning = DateTime.Parse("2025-1-10"),
               Duration = 56
            },
            new()
            {
               Name = "Mobile App Development",
               Description = "This course is tailored for individuals eager to dive into the world" +
               " of mobile app development. Over the span of 14 weeks, you will explore the entire" +
               " lifecycle of app development, from conception to deployment on both Android and" +
               " iOS platforms.\r\n\r\nYou will learn essential programming languages such as Kotlin" +
               " for Android and Swift for iOS, alongside frameworks like Flutter for cross-platform" +
               " development. The curriculum includes user interface design, integration with APIs," +
               " and database management using SQLite and Firebase.\r\n\r\nHands-on projects will" +
               " be a key component of the course, allowing you to build functional apps that solve" +
               " real-world problems. You will also gain insights into app store submission processes," +
               " user experience (UX) best practices, and performance optimization.\r\n\r\nBy the end" +
               " of this course, you will have the confidence and expertise to create, publish, and" +
               " maintain your own mobile applications, setting you on a path toward a successful career" +
               " in mobile development.",
               Author = "Mastery Labs",
               СreationDate = DateTime.Now,
               Beginning = DateTime.Parse("2025-2-13"),
               Duration = 98
            }
        };
    }
    private List<User> GetUsers()
    {
        return new List<User> {
            new User { Name = "John", LastName = "Doe", Age = 22 },
            new User { Name = "Jane", LastName = "Smith", Age = 28 },
            new User { Name = "Emily", LastName = "Johnson", Age = 24 },
            new User { Name = "Michael", LastName = "Brown", Age = 30 },
            new User { Name = "Sarah", LastName = "Williams", Age = 26 }
        };
    }
}
