﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Skill
    {
        public Skill()
        {
            this.CandidateSkills = new List<CandidateSkill>();
        }
        public int SkillId { get; set; }
        [Required,StringLength(50), Display(Name = "Skill Name")]
        public string SkillName { get; set; } = default!;

        //nev
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
    }
    public class Candidate
    {
        public int CandidateId { get; set; }
        [Required, StringLength(50), Display(Name = "Candidate Name")]
        public string CandidateName { get; set; } = default!;
        [Required, Display(Name = "Date of Birth"),DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; } = default!;
        public bool Fresher { get; set; }

        //nev
        public virtual ICollection<CandidateSkill>? CandidateSkills { get; set; }
    }
    public class CandidateSkill
    {
        public int CandidateSkillId { get; set; }
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        //nev
        public virtual Candidate? Candidate { get; set; }
        public virtual Skill? Skill { get; set; }
    }

    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options):base(options)
        {
            
        }
        public DbSet<Candidate> Candidates { get; set;}
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData
                (
                    new Skill { SkillId = 1, SkillName = "C#" },
                    new Skill { SkillId = 2, SkillName = "SQL" },
                    new Skill { SkillId = 3, SkillName = "ASP" },
                    new Skill { SkillId = 4, SkillName = "Angular" },
                    new Skill { SkillId = 5, SkillName = "MVC" },
                    new Skill { SkillId = 6, SkillName = "React" }
                );
        }
        internal object Find(int CandidateId)
        {
            throw new NotImplementedException();
        }
    }
}
