﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.frockett.Models;

namespace Movies.frockett.Data;

public class MoviesfrockettContext : IdentityDbContext
{
    public MoviesfrockettContext(DbContextOptions<MoviesfrockettContext> options)
        : base(options)
    {

    }

    public DbSet<Movies.frockett.Models.Movie> Movie { get; set; } = default!;
}

