using C971.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Services
{
    //Interface to specify what the datastores must implement
    public interface IDataStore
    {
        Task<bool> AddTermAsync(SchoolTerm item);
        Task<bool> AddCourseAsync(SchoolCourse item);
        Task<bool> AddInstructorAsync(Instructor item);
        Task<bool> AddAssessmentAsync(Assessment item);
        Task<bool> UpdateTermAsync(SchoolTerm item);
        Task<bool> UpdateCourseAsync(SchoolCourse item);
        Task<bool> UpdateInstructorAsync(Instructor item);
        Task<bool> UpdateAssessmentAsync(Assessment item);
        Task<bool> DeleteTermAsync(string id);
        Task<bool> DeleteCourseAsync(string id);
        Task<bool> DeleteInstructorAsync(string id);
        Task<bool> DeleteAssessmentAsync(string id);
        Task<SchoolTerm> GetTermAsync(string id);
        Task<SchoolCourse> GetCourseAsync(string id);
        Task<Instructor> GetInstructorAsync(string id);
        Task<Assessment> GetAssessmentAsync(string id);
        Task<IEnumerable<SchoolTerm>> GetTermsAsync(bool forceRefresh = false);
        Task<IEnumerable<SchoolCourse>> GetCoursesAsync(bool forceRefresh = false);
        Task<IEnumerable<Instructor>> GetInstructorsAsync(bool forceRefresh = false);
        Task<IEnumerable<Assessment>> GetAssessmentsAsync(bool forceRefresh = false);
    }
}
