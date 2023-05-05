using System;
using System.Collections.Generic;

namespace SGI_WebApi_Pauny.Models
{
    public partial class User
    {
        public User()
        {
            EstadoIncidencias = new HashSet<EstadoIncidencia>();
            RefreshTokens = new HashSet<RefreshToken>();
            SurveyAnswers = new HashSet<SurveyAnswer>();
            Surveys = new HashSet<Survey>();
        }

        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public Guid? IdRole { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid? IdDealer { get; set; }

        public virtual Dealer IdDealerNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<EstadoIncidencia> EstadoIncidencias { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
