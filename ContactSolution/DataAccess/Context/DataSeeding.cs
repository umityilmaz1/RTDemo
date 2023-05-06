using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>().HasData(
            //    new Course { ID = 1, Code = "CSI101", Name = "Introduction to Computer Science", CreatedDate = DateTimeHelper.NowTurkey },
            //    new Course { ID = 2, Code = "CSI102", Name = "Algorithms", CreatedDate = DateTimeHelper.NowTurkey },
            //    new Course { ID = 3, Code = "MAT101", Name = "Calculus", CreatedDate = DateTimeHelper.NowTurkey },
            //    new Course { ID = 4, Code = "PHY101", Name = "Physics", CreatedDate = DateTimeHelper.NowTurkey });
        }
    }
}