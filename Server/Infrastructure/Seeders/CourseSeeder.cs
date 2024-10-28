using Application.Seeders;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

internal class CourseSeeder(StudentRegistrationSystemDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!await dbContext.Courses.AnyAsync())
            {
                var topics = GetCourses();
                dbContext.Courses.AddRange(topics);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Course> GetCourses()
    {
        List<Course> courses = [
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
               IsAvailable = true,
               Beginning = DateTime.Parse("2024-12-1 14:30:00.1234567"),
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
               IsAvailable = true,
               Beginning = DateTime.Parse("2025-1-1 14:30:00.1234567"),
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
               IsAvailable = true,
               Beginning = DateTime.Parse("2024-11-1 14:30:00.1234567"),
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
               IsAvailable = true,
               Beginning = DateTime.Parse("2025-1-10 14:30:00.1234567"),
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
               IsAvailable = true,
               Beginning = DateTime.Parse("2025-2-13 14:30:00.1234567"),
               Duration = 98
            }

        ];

        return courses;
    }
}
