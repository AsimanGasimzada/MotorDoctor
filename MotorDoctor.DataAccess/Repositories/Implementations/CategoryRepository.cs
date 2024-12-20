﻿using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
