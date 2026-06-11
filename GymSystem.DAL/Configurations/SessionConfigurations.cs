using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Collections.Specialized.BitVector32;

namespace GymSystem.DAL.Configurations
{
    internal class SessionConfigurations:IEntityTypeConfiguration<Sessions>

    {
        public void Configure(EntityTypeBuilder<Sessions> builder)
        {
            builder.ToTable(T =>
            {
                T.HasCheckConstraint("SessionCapacityConstraint", "Capacity between 1 and 25");
                T.HasCheckConstraint("SessionEndDateAfterStartDate", "EndDate > StartDate");
            });

            builder.HasOne(X => X.Trainer)
                .WithMany(X => X.sessions)
                .HasForeignKey(X => X.TrainerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(X => X.Category)
                .WithMany(X => X.sessions)
                .HasForeignKey(X => X.CategoryId);


        }
    }
}
