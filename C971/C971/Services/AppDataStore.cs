using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C971.Models;
using Plugin.LocalNotifications;

namespace C971.Services
{
    public class AppDataStore : IDataStore
    {
        readonly List<SchoolTerm> schoolTerms;
        readonly List<SchoolCourse> schoolCourses;
        readonly List<Instructor> instructors;
        readonly List<Assessment> assessments;

        //Used to keep track of the notification Ids
        private Int32 AssessmentNotifyNextId;
        private Int32 CourseNotifyNextId;
        private Int32 TermNotifyNextId;

        public AppDataStore()
        {
            TermNotifyNextId = 10000; //Terms have notifications from 10000 to 19999
            CourseNotifyNextId = 20000; //Courses have notifications from 20000 to 39999
            AssessmentNotifyNextId = 40000; //Assessments have notifications from 40000+
            //Load the app with some default instructors
            instructors = new List<Instructor>()
            {
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "Daniel Harper", Email = "dharp41@my.wgu.edu", PhoneNumber = "801-651-2376"},
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "John Doe", Email = "jondoe@my.wgu.edu", PhoneNumber = "555-555-5555"},
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "Jane Doe", Email = "janedoe@my.wgu.edu", PhoneNumber = "555-555-5556"},
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "Will Smith", Email = "wsmith@my.wgu.edu", PhoneNumber = "555-555-5557"},
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "Samuel L. Jackson", Email = "sjackson@my.wgu.edu", PhoneNumber = "555-555-5558"},
                new Instructor { Id = Guid.NewGuid().ToString(), Name = "Jamie Lee Curtis", Email = "jcurtis@my.wgu.edu", PhoneNumber = "555-555-5559"},
            };

            //Load the app with some default terms
            schoolTerms = new List<SchoolTerm>()
            {
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Summer 2020", StartDate = DateTime.Parse("May 20, 2020"), EndDate = DateTime.Parse("August 20, 2020"), NotificationID = TermNotifyNextId++},
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Fall 2020", StartDate = DateTime.Parse("August 20, 2020"), EndDate = DateTime.Parse("December 20, 2020"), NotificationID = TermNotifyNextId++},
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Spring 2021", StartDate = DateTime.Parse("January 2, 2021"), EndDate = DateTime.Parse("May 19, 2021"), NotificationID = TermNotifyNextId++},
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Summer 2021", StartDate = DateTime.Parse("May 20, 2021"), EndDate = DateTime.Parse("August 20, 2021"), NotificationID = TermNotifyNextId++},
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Fall 2021", StartDate = DateTime.Parse("August 20, 2021"), EndDate = DateTime.Parse("December 20, 2021"), NotificationID = TermNotifyNextId++},
                new SchoolTerm { Id = Guid.NewGuid().ToString(), TermName = "Spring 2022", StartDate = DateTime.Parse("January 2, 2022"), EndDate = DateTime.Parse("May 19, 2022"), NotificationID = TermNotifyNextId++},
            };

            //Load the app with some default courses
            schoolCourses = new List<SchoolCourse>()
            {
                new SchoolCourse { Id = Guid.NewGuid().ToString(), Instructor = instructors.Where((Instructor arg) => arg.Name == "Daniel Harper").FirstOrDefault(), CourseName = "C971", 
                    CourseDescription = "I hate mobile Development", CourseStatus = CourseStatusFlags.Course_In_Progress, StartDate = DateTime.Parse("January 20,2020"), EndDate = DateTime.Now,
                    AssociatedTermId = schoolTerms.Where((SchoolTerm arg) => arg.TermName == "Summer 2020").FirstOrDefault().Id, NotificationID = CourseNotifyNextId++},
                new SchoolCourse { Id = Guid.NewGuid().ToString(), Instructor = instructors.Where((Instructor arg) => arg.Name == "John Doe").FirstOrDefault(), CourseName = "C900",
                    CourseDescription = "Agile Development Fundamentalss", CourseStatus = CourseStatusFlags.Course_In_Progress, StartDate = DateTime.Parse("January 20,2020"), EndDate = DateTime.Now,
                    AssociatedTermId = schoolTerms.Where((SchoolTerm arg) => arg.TermName == "Summer 2020").FirstOrDefault().Id, NotificationID = CourseNotifyNextId++},
                new SchoolCourse { Id = Guid.NewGuid().ToString(), Instructor = instructors.Where((Instructor arg) => arg.Name == "Daniel Harper").FirstOrDefault(), CourseName = "C973",
                    CourseDescription = "I hate mobile Development even more", CourseStatus = CourseStatusFlags.Course_Plan_To_Take, StartDate = DateTime.Parse("August 20,2020"), EndDate = DateTime.Parse("December 20,2020"),
                    AssociatedTermId = schoolTerms.Where((SchoolTerm arg) => arg.TermName == "Fall 2020").FirstOrDefault().Id, NotificationID = CourseNotifyNextId++},
            };

            //Load the app with some default assessments
            assessments = new List<Assessment>()
            { 
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Objective, Name = "Final", Description = "Final test of the course", StartDate = DateTime.Parse("May 20, 2020"), EndDate = DateTime.Parse("May 30, 2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C971").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Performance, Name = "Mid-Term", Description = "Middle of the term exam", StartDate = DateTime.Parse("March 20, 2020"), EndDate = DateTime.Parse("March 30, 2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C971").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Objective, Name = "Final", Description = "Final test of the course", StartDate = DateTime.Parse("May 20, 2020"), EndDate = DateTime.Parse("May 30, 2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C900").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Performance, Name = "Mid-Term", Description = "Middle of the term exam", StartDate = DateTime.Parse("March 20, 2020"), EndDate = DateTime.Parse("March 30, 2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C900").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Objective, Name = "Final", Description = "Final test of the course", StartDate = DateTime.Parse("August 20,2020"), EndDate = DateTime.Parse("December 20,2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C973").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
                new Assessment { Id = Guid.NewGuid().ToString(), AssessmentType = AssessmentFlags.Assessment_Performance, Name = "Mid-Term", Description = "Mid-Term", StartDate = DateTime.Parse("August 20,2020"), EndDate = DateTime.Parse("December 20,2020"),
                    AssociatedCourseId = schoolCourses.Where((SchoolCourse arg) => arg.CourseName == "C973").FirstOrDefault().Id, NotificationID = AssessmentNotifyNextId++},
            };

            //Link each new default assessment to the course so it's a two way link.
            foreach(SchoolCourse course in schoolCourses)
            {
                foreach(Assessment assessment in assessments)
                {
                    if(course.Id == assessment.AssociatedCourseId)
                    {
                        switch(assessment.AssessmentType)
                        {
                            case AssessmentFlags.Assessment_Objective:
                            {
                                    course.ObjectiveAssessment = assessment;
                                    break;
                            }
                            case AssessmentFlags.Assessment_Performance:
                            {
                                    course.PerformanceAsssessment = assessment;
                                    break;
                            }
                        }    
                    }
                    var tmpCourse = schoolCourses.Where((SchoolCourse arg) => arg.Id == assessment.AssociatedCourseId).FirstOrDefault();  //Quick copy and paste to get the full associated course.
                    assessment.AssociatedCourse = tmpCourse;
                    CrossLocalNotifications.Current.Show(assessment.Name, assessment.Name + " starts today.", assessment.NotificationID, assessment.StartDate); //Set up local notifications on all assessments
                }
                CrossLocalNotifications.Current.Show(course.CourseName, course.CourseName + " starts today.", course.NotificationID, course.StartDate); //Set up local notifications on all courses
            }    

            foreach(SchoolTerm term in schoolTerms)
            {
                CrossLocalNotifications.Current.Show(term.TermName, term.TermName + " starts today.", term.NotificationID, term.StartDate); //Set up local notifications on all terms
            }
        }



        //Add a new SchoolTerm to the datastore
        public async Task<bool> AddTermAsync(SchoolTerm term)
        {
            if(term.Id is null)
            {
                term.Id = Guid.NewGuid().ToString();
            }    
            schoolTerms.Add(term);
            CrossLocalNotifications.Current.Show(term.TermName, term.TermName + " begins today", term.NotificationID++, term.StartDate);
            return await Task.FromResult(true);
        }

        //Add a new SchoolCourse to the datastore
        public async Task<bool> AddCourseAsync(SchoolCourse course)
        {
            var tmpTerm = schoolTerms.Where((SchoolTerm arg) => arg.Id == course.AssociatedTermId).FirstOrDefault();
            tmpTerm.SchoolCourses.AddLast(course);
            if(course.Id is null)
            {
                course.Id = Guid.NewGuid().ToString();
            }
            schoolCourses.Add(course);
            CrossLocalNotifications.Current.Show(course.CourseName, course.CourseName + " starts today.", course.NotificationID++, course.StartDate); //Set up local notifications on all courses
            return await Task.FromResult(true);
        }

        //Add a new Instructor to the datastore
        public async Task<bool> AddInstructorAsync(Instructor teacher)
        {
            if(teacher.Id is null)
            {
                teacher.Id = Guid.NewGuid().ToString();
            }    
            instructors.Add(teacher);
            return await Task.FromResult(true);
        }

        //Add a new Assessment to the datastore
        public async Task<bool> AddAssessmentAsync(Assessment test)
        {
            var tmpCourse = schoolCourses.Where((SchoolCourse arg) => arg.Id == test.AssociatedCourseId).FirstOrDefault();
            test.AssociatedCourse = tmpCourse;
            switch (test.AssessmentType)
            {
                case AssessmentFlags.Assessment_Objective:
                {
                        tmpCourse.ObjectiveAssessment = test;
                        test.AssociatedCourseId = tmpCourse.Id;
                        break;
                }
                case AssessmentFlags.Assessment_Performance:
                {
                        tmpCourse.PerformanceAsssessment = test;
                        test.AssociatedCourseId = tmpCourse.Id;
                        break;
                }
            }
            if(test.Id is null)
            {
                test.Id = Guid.NewGuid().ToString();
            }
            assessments.Add(test);
            CrossLocalNotifications.Current.Show(test.Name, test.Name + " starts today.", test.NotificationID++, test.StartDate); //Set up local notifications on all assessments
            return await Task.FromResult(true);
        }

        //Update a SchoolTerm to the datastore
        public async Task<bool> UpdateTermAsync(SchoolTerm term)
        {
            if(term != null)
            {
                var oldItem = schoolTerms.Where((SchoolTerm arg) => arg.Id == term.Id).FirstOrDefault();
                schoolTerms.Remove(oldItem);
                schoolTerms.Add(term);
                //Delete the notification and readd in case the startdate changed.
                CrossLocalNotifications.Current.Cancel(term.NotificationID);
                CrossLocalNotifications.Current.Show(term.TermName, term.TermName + " begins today", term.NotificationID, term.StartDate);
            }
            return await Task.FromResult(true);
        }

        //Update a SchoolCourse to the datastore
        public async Task<bool> UpdateCourseAsync(SchoolCourse course)
        {
            if(course != null)
            {
                var oldItem = schoolCourses.Where((SchoolCourse arg) => arg.Id == course.Id).FirstOrDefault();
                schoolCourses.Remove(oldItem);
                schoolCourses.Add(course);
                //Delete the notification and readd in case the startdate changed.
                CrossLocalNotifications.Current.Cancel(course.NotificationID);
                CrossLocalNotifications.Current.Show(course.CourseName, course.CourseName + " begins today", course.NotificationID, course.StartDate);
            }
            return await Task.FromResult(true);
        }

        //Update a Instructor to the datastore
        public async Task<bool> UpdateInstructorAsync(Instructor teacher)
        {
            var oldItem = instructors.Where((Instructor arg) => arg.Id == teacher.Id).FirstOrDefault();
            instructors.Remove(oldItem);
            instructors.Add(teacher);
            return await Task.FromResult(true);
        }

        //Update a Assessment to the datastore
        public async Task<bool> UpdateAssessmentAsync(Assessment test)
        {
            var oldItem = assessments.Where((Assessment arg) => arg.Id == test.Id).FirstOrDefault();
            assessments.Remove(oldItem);
            assessments.Add(test);
            //Delete the notification and readd in case the startdate changed.
            CrossLocalNotifications.Current.Cancel(test.NotificationID);
            CrossLocalNotifications.Current.Show(test.Name, test.Name + " begins today", test.NotificationID, test.StartDate);
            return await Task.FromResult(true);
        }

        //Delete a SchoolTerm to the datastore
        public async Task<bool> DeleteTermAsync(string id)
        {
            foreach (SchoolTerm item in schoolTerms.Where((SchoolTerm arg) => arg.Id == id).ToList())
            {
                CrossLocalNotifications.Current.Cancel(item.NotificationID);
                foreach (SchoolCourse course in schoolCourses.Where((SchoolCourse arg) => arg.AssociatedTermId == item.Id).ToList())
                {
                    DeleteCourse(course.Id);
                }
                schoolTerms.Remove(item);
            }
            return await Task.FromResult(true);
        }

        //Delete a SchoolTerm to the datastore
        public async Task<bool> DeleteCourseAsync(string id)
        {
            DeleteCourse(id);
            return await Task.FromResult(true);
        }

        //Delete a Instructor to the datastore
        public async Task<bool> DeleteInstructorAsync(string id)
        {
            var oldItem = instructors.Where((Instructor arg) => arg.Id == id).FirstOrDefault();
            instructors.Remove(oldItem);

            return await Task.FromResult(true);
        }

        //Delete a Assessment to the datastore
        public async Task<bool> DeleteAssessmentAsync(string id)
        {
            DeleteAssessment(id);
            return await Task.FromResult(true);
        }

        //Get a SchoolTerm based on the id
        public async Task<SchoolTerm> GetTermAsync(string id)
        {
            return await Task.FromResult(schoolTerms.FirstOrDefault(s => s.Id == id));
        }

        //Get a SchoolCourse based on the id
        public async Task<SchoolCourse> GetCourseAsync(string id)
        {
            return await Task.FromResult(schoolCourses.FirstOrDefault(s => s.Id == id));
        }

        //Get a Instructor based on the id
        public async Task<Instructor> GetInstructorAsync(string id)
        {
            return await Task.FromResult(instructors.FirstOrDefault(s => s.Id == id));
        }

        //Get a Assessment based on the id
        public async Task<Assessment> GetAssessmentAsync(string id)
        {
            return await Task.FromResult(assessments.FirstOrDefault(s => s.Id == id));
        }

        //Get all the Terms
        public async Task<IEnumerable<SchoolTerm>> GetTermsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(schoolTerms);
        }

        //Get all the Courses
        public async Task<IEnumerable<SchoolCourse>> GetCoursesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(schoolCourses);
        }

        //Get all the Instructors
        public async Task<IEnumerable<Instructor>> GetInstructorsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(instructors);
        }

        //Get all the Assessments
        public async Task<IEnumerable<Assessment>> GetAssessmentsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(assessments);
        }

        //Separated these out into their own functions since they were going to be used outside of their own areas.  Also to solve a concurrency issue I was running into.
        private void DeleteCourse(String id)
        {
            foreach(SchoolCourse item in schoolCourses.Where((SchoolCourse arg) => arg.Id == id).ToList())
            {
                CrossLocalNotifications.Current.Cancel(item.NotificationID);
                var tmpTerm = schoolTerms.Where((SchoolTerm arg) => arg.Id == item.AssociatedTermId).FirstOrDefault();
                tmpTerm.SchoolCourses.Remove(item);
                if (item.ObjectiveAssessment != null)
                {
                    DeleteAssessment(item.ObjectiveAssessment.Id);
                }
                if (item.PerformanceAsssessment != null)
                {
                    DeleteAssessment(item.PerformanceAsssessment.Id);
                }
                schoolCourses.Remove(item);
            }

        }

        private void DeleteAssessment(String id)
        {
            foreach(Assessment assessment in assessments.Where((Assessment arg) => arg.Id == id).ToList())
            {
                CrossLocalNotifications.Current.Cancel(assessment.NotificationID);
                var tmpCourse = schoolCourses.Where((SchoolCourse arg) => arg.Id == assessment.AssociatedCourseId).FirstOrDefault();
                if(assessment.AssessmentType == AssessmentFlags.Assessment_Performance)
                {
                    tmpCourse.PerformanceAsssessment = null;
                }
                else if (assessment.AssessmentType == AssessmentFlags.Assessment_Objective)
                {
                    tmpCourse.ObjectiveAssessment = null;
                }
                assessments.Remove(assessment);
            }

        }
    }
}